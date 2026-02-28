using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Controller;

public sealed class ControllerOptions
{
    public TimeSpan PendingTtl { get; init; } = TimeSpan.FromSeconds(60);
    public TimeSpan FunctionPendingTtl { get; init; } = TimeSpan.FromSeconds(60);
    public int MaxPendingPerSurface { get; init; } = 100;
}

public sealed class SurfaceController
{
    private enum SurfaceState
    {
        Active,
        Deleted
    }

    private readonly ControllerOptions _options;
    private readonly Func<DateTimeOffset> _utcNow;
    private readonly Dictionary<string, SurfaceDefinition> _surfaces = new();
    private readonly Dictionary<string, DataModel> _dataModels = new();
    private readonly Dictionary<string, Queue<PendingUpdate>> _pending = new();
    private readonly Dictionary<string, SurfaceState> _surfaceStates = new();
    private readonly Dictionary<string, PendingFunctionCall> _pendingFunctionCalls = new();

    public event Action<SurfaceUpdate>? SurfaceUpdated;
    public event Action<string>? SurfaceDeleted;
    public event Action<A2uiError>? Error;

    public SurfaceController(ControllerOptions? options = null, Func<DateTimeOffset>? utcNow = null)
    {
        _options = options ?? new ControllerOptions();
        _utcNow = utcNow ?? (() => DateTimeOffset.UtcNow);
    }

    public void HandleMessage(NormalMessage message)
    {
        try
        {
            ExpirePendingFunctionCalls();
            switch (message.Type)
            {
                case NormalMessageType.CreateSurface:
                    HandleCreateSurface(message);
                    break;
                case NormalMessageType.UpdateComponents:
                    HandleUpdateComponents(message);
                    break;
                case NormalMessageType.UpdateDataModel:
                    HandleUpdateDataModel(message);
                    break;
                case NormalMessageType.DeleteSurface:
                    HandleDeleteSurface(message.SurfaceId);
                    break;
                case NormalMessageType.CallFunction:
                    HandleCallFunction(message);
                    break;
                case NormalMessageType.FunctionResponse:
                    HandleFunctionResponse(message);
                    break;
            }
        }
        catch (Exception ex)
        {
            Error?.Invoke(new A2uiError("E_CONTROLLER", ex.Message, message.SurfaceId, message.FunctionCallId));
        }
    }

    public bool HasSurface(string surfaceId) => _surfaces.ContainsKey(surfaceId);

    private void HandleCreateSurface(NormalMessage message)
    {
        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ID_REQUIRED", "surfaceId is required for createSurface."));
            return;
        }

        if (_surfaceStates.TryGetValue(surfaceId, out var state) && state == SurfaceState.Active)
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ALREADY_EXISTS", $"surface '{surfaceId}' already exists.", surfaceId));
            return;
        }

        var payload = message.Payload ?? new JsonObject();
        var rootId = payload["root"]?.GetValue<string>() ?? "root";
        var components = payload["components"] as JsonObject ?? new JsonObject();

        _surfaces[surfaceId] = new SurfaceDefinition(surfaceId, rootId, (JsonObject)components.DeepClone());
        _dataModels.TryAdd(surfaceId, new DataModel());
        _surfaceStates[surfaceId] = SurfaceState.Active;
        ApplyPending(surfaceId);
        PublishSurface(surfaceId);
    }

    private void HandleUpdateComponents(NormalMessage message)
    {
        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ID_REQUIRED", "surfaceId is required for updateComponents."));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_DELETED", $"surface '{surfaceId}' was deleted.", surfaceId));
            return;
        }

        if (!_surfaces.TryGetValue(surfaceId, out var current))
        {
            EnqueuePending(surfaceId, message);
            return;
        }

        var payload = message.Payload ?? new JsonObject();
        var incoming = payload["components"] as JsonObject;
        if (incoming is null)
        {
            Error?.Invoke(new A2uiError("E_COMPONENTS_REQUIRED", "components object is required.", surfaceId));
            return;
        }

        var merged = (JsonObject)current.Components.DeepClone();
        foreach (var kv in incoming)
        {
            merged[kv.Key] = kv.Value?.DeepClone();
        }

        _surfaces[surfaceId] = current with { Components = merged };
        PublishSurface(surfaceId);
    }

    private void HandleUpdateDataModel(NormalMessage message)
    {
        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ID_REQUIRED", "surfaceId is required for updateDataModel."));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_DELETED", $"surface '{surfaceId}' was deleted.", surfaceId));
            return;
        }

        if (!_surfaces.ContainsKey(surfaceId))
        {
            EnqueuePending(surfaceId, message);
            return;
        }

        var payload = message.Payload ?? new JsonObject();
        var patches = payload["patches"] as JsonArray;
        if (patches is null)
        {
            Error?.Invoke(new A2uiError("E_PATCHES_REQUIRED", "patches array is required.", surfaceId));
            return;
        }

        var model = _dataModels.GetValueOrDefault(surfaceId) ?? new DataModel();
        foreach (var patchNode in patches)
        {
            if (patchNode is not JsonObject patch) continue;
            var pathArray = patch["path"] as JsonArray;
            if (pathArray is null || pathArray.Count == 0) continue;
            var path = string.Join('.', pathArray.Select(n => n?.ToString() ?? string.Empty).Where(v => !string.IsNullOrWhiteSpace(v)));
            if (string.IsNullOrWhiteSpace(path)) continue;

            var isDelete = patch["deleteOp"]?.GetValue<bool>() == true;
            if (isDelete)
            {
                model.Delete(path);
                continue;
            }

            model.Set(path, patch["value"]);
        }

        _dataModels[surfaceId] = model;
        PublishSurface(surfaceId);
    }

    private void HandleDeleteSurface(string? surfaceId)
    {
        if (string.IsNullOrWhiteSpace(surfaceId)) return;

        if (!_surfaceStates.TryGetValue(surfaceId, out var state))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_NOT_FOUND", $"surface '{surfaceId}' not found.", surfaceId));
            return;
        }

        if (state == SurfaceState.Deleted)
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ALREADY_DELETED", $"surface '{surfaceId}' already deleted.", surfaceId));
            return;
        }

        _surfaces.Remove(surfaceId);
        _dataModels.Remove(surfaceId);
        _pending.Remove(surfaceId);
        _surfaceStates[surfaceId] = SurfaceState.Deleted;
        SurfaceDeleted?.Invoke(surfaceId);
    }

    private void HandleCallFunction(NormalMessage message)
    {
        var functionCallId = message.FunctionCallId;
        if (string.IsNullOrWhiteSpace(functionCallId))
        {
            Error?.Invoke(new A2uiError("E_FUNCTION_CALL_ID_REQUIRED", "functionCallId is required for callFunction.", message.SurfaceId));
            return;
        }

        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_ID_REQUIRED", "surfaceId is required for callFunction.", FunctionCallId: functionCallId));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_DELETED", $"surface '{surfaceId}' was deleted.", surfaceId, functionCallId));
            return;
        }

        if (!_surfaces.ContainsKey(surfaceId))
        {
            Error?.Invoke(new A2uiError("E_SURFACE_NOT_FOUND", $"surface '{surfaceId}' not found for callFunction.", surfaceId, functionCallId));
            return;
        }

        var name = GetOptionalString(message.Payload?["name"]);
        if (string.IsNullOrWhiteSpace(name))
        {
            Error?.Invoke(new A2uiError("E_FUNCTION_NAME_REQUIRED", "callFunction.name is required.", surfaceId, functionCallId));
            return;
        }

        if (_pendingFunctionCalls.ContainsKey(functionCallId))
        {
            Error?.Invoke(new A2uiError("E_FUNCTION_CALL_DUPLICATE", $"functionCallId '{functionCallId}' is already pending.", surfaceId, functionCallId));
            return;
        }

        _pendingFunctionCalls[functionCallId] = new PendingFunctionCall(functionCallId, surfaceId, name, _utcNow());
    }

    private void HandleFunctionResponse(NormalMessage message)
    {
        var functionCallId = message.FunctionCallId;
        if (string.IsNullOrWhiteSpace(functionCallId))
        {
            Error?.Invoke(new A2uiError("E_FUNCTION_CALL_ID_REQUIRED", "functionCallId is required for functionResponse.", message.SurfaceId));
            return;
        }

        if (!_pendingFunctionCalls.TryGetValue(functionCallId, out var pending))
        {
            Error?.Invoke(new A2uiError("E_FUNCTION_RESPONSE_ORPHAN", $"functionResponse has no pending call for '{functionCallId}'.", message.SurfaceId, functionCallId));
            return;
        }

        var responseSurfaceId = message.SurfaceId;
        if (!string.IsNullOrWhiteSpace(responseSurfaceId) && !string.Equals(responseSurfaceId, pending.SurfaceId, StringComparison.Ordinal))
        {
            Error?.Invoke(new A2uiError(
                "E_FUNCTION_SURFACE_MISMATCH",
                $"functionResponse surfaceId '{responseSurfaceId}' does not match call surface '{pending.SurfaceId}'.",
                responseSurfaceId,
                functionCallId));
            return;
        }

        _pendingFunctionCalls.Remove(functionCallId);
    }

    private void PublishSurface(string surfaceId)
    {
        if (!_surfaces.TryGetValue(surfaceId, out var definition)) return;
        var model = _dataModels.GetValueOrDefault(surfaceId) ?? new DataModel();
        SurfaceUpdated?.Invoke(new SurfaceUpdate(surfaceId, definition, model));
    }

    private void EnqueuePending(string surfaceId, NormalMessage message)
    {
        if (!_pending.TryGetValue(surfaceId, out var queue))
        {
            queue = new Queue<PendingUpdate>();
            _pending[surfaceId] = queue;
        }

        while (queue.Count > 0 && IsExpired(queue.Peek()))
        {
            queue.Dequeue();
        }

        if (queue.Count >= _options.MaxPendingPerSurface)
        {
            Error?.Invoke(new A2uiError("E_PENDING_OVERFLOW", $"pending queue overflow for surface '{surfaceId}'", surfaceId));
            return;
        }

        queue.Enqueue(new PendingUpdate(message, _utcNow()));
    }

    private void ApplyPending(string surfaceId)
    {
        if (!_pending.TryGetValue(surfaceId, out var queue))
        {
            return;
        }

        var replay = new List<NormalMessage>();
        while (queue.Count > 0)
        {
            var pending = queue.Dequeue();
            if (IsExpired(pending))
            {
                Error?.Invoke(new A2uiError("E_PENDING_EXPIRED", $"pending update expired for surface '{surfaceId}'", surfaceId));
                continue;
            }

            replay.Add(pending.Message);
        }

        _pending.Remove(surfaceId);
        foreach (var msg in replay)
        {
            switch (msg.Type)
            {
                case NormalMessageType.UpdateComponents:
                    HandleUpdateComponents(msg);
                    break;
                case NormalMessageType.UpdateDataModel:
                    HandleUpdateDataModel(msg);
                    break;
            }
        }
    }

    private bool IsExpired(PendingUpdate pending)
        => _utcNow() - pending.EnqueueTime > _options.PendingTtl;

    private bool IsDeleted(string surfaceId)
        => _surfaceStates.TryGetValue(surfaceId, out var state) && state == SurfaceState.Deleted;

    private void ExpirePendingFunctionCalls()
    {
        if (_pendingFunctionCalls.Count == 0)
        {
            return;
        }

        var now = _utcNow();
        var expired = _pendingFunctionCalls.Values
            .Where(call => now - call.EnqueueTime > _options.FunctionPendingTtl)
            .ToList();

        foreach (var call in expired)
        {
            _pendingFunctionCalls.Remove(call.FunctionCallId);
            Error?.Invoke(new A2uiError(
                "E_FUNCTION_TIMEOUT",
                $"function call '{call.Name}' timed out.",
                call.SurfaceId,
                call.FunctionCallId));
        }
    }

    private static string? GetOptionalString(JsonNode? node)
    {
        if (node is null) return null;
        return node is JsonValue value && value.TryGetValue<string>(out var str) ? str : null;
    }

    private sealed record PendingUpdate(NormalMessage Message, DateTimeOffset EnqueueTime);
    private sealed record PendingFunctionCall(string FunctionCallId, string SurfaceId, string Name, DateTimeOffset EnqueueTime);
}
