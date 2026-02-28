using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Utils;

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

    private enum FunctionCallState
    {
        Pending,
        RetryScheduled,
        Cancelled,
        Completed,
        TimedOut
    }

    private readonly ControllerOptions _options;
    private readonly Func<DateTimeOffset> _utcNow;
    private readonly Dictionary<string, SurfaceDefinition> _surfaces = new();
    private readonly Dictionary<string, DataModel> _dataModels = new();
    private readonly Dictionary<string, Queue<PendingUpdate>> _pending = new();
    private readonly Dictionary<string, SurfaceState> _surfaceStates = new();
    private readonly Dictionary<string, FunctionCallRuntime> _functionCalls = new();

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
            Error?.Invoke(new A2uiError(ErrorCodes.Controller, ex.Message, message.SurfaceId, message.FunctionCallId));
        }
    }

    public bool HasSurface(string surfaceId) => _surfaces.ContainsKey(surfaceId);

    private void HandleCreateSurface(NormalMessage message)
    {
        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceIdRequired, "surfaceId is required for createSurface."));
            return;
        }

        if (_surfaceStates.TryGetValue(surfaceId, out var state) && state == SurfaceState.Active)
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceAlreadyExists, $"surface '{surfaceId}' already exists.", surfaceId));
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
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceIdRequired, "surfaceId is required for updateComponents."));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceDeleted, $"surface '{surfaceId}' was deleted.", surfaceId));
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
            Error?.Invoke(new A2uiError(ErrorCodes.ComponentsRequired, "components object is required.", surfaceId));
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
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceIdRequired, "surfaceId is required for updateDataModel."));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceDeleted, $"surface '{surfaceId}' was deleted.", surfaceId));
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
            Error?.Invoke(new A2uiError(ErrorCodes.PatchesRequired, "patches array is required.", surfaceId));
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
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceNotFound, $"surface '{surfaceId}' not found.", surfaceId));
            return;
        }

        if (state == SurfaceState.Deleted)
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceAlreadyDeleted, $"surface '{surfaceId}' already deleted.", surfaceId));
            return;
        }

        CancelFunctionCallsForSurface(surfaceId);

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
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionCallIdRequired, "functionCallId is required for callFunction.", message.SurfaceId));
            return;
        }

        var surfaceId = message.SurfaceId;
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceIdRequired, "surfaceId is required for callFunction.", FunctionCallId: functionCallId));
            return;
        }

        if (IsDeleted(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceDeleted, $"surface '{surfaceId}' was deleted.", surfaceId, functionCallId));
            return;
        }

        if (!_surfaces.ContainsKey(surfaceId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.SurfaceNotFound, $"surface '{surfaceId}' not found for callFunction.", surfaceId, functionCallId));
            return;
        }

        var payload = message.Payload ?? new JsonObject();
        var isRetry = payload["retry"]?.GetValue<bool>() == true;
        var isCancel = payload["cancel"]?.GetValue<bool>() == true;

        if (isRetry && isCancel)
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionRetryInvalidState, "callFunction cannot set both retry and cancel.", surfaceId, functionCallId));
            return;
        }

        if (isCancel)
        {
            CancelFunctionCall(functionCallId, surfaceId);
            return;
        }

        var name = GetOptionalString(payload["name"]);
        if (string.IsNullOrWhiteSpace(name))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionNameRequired, "callFunction.name is required.", surfaceId, functionCallId));
            return;
        }

        if (_functionCalls.TryGetValue(functionCallId, out var existing))
        {
            if (!isRetry)
            {
                Error?.Invoke(new A2uiError(ErrorCodes.FunctionCallDuplicate, $"functionCallId '{functionCallId}' is already tracked.", surfaceId, functionCallId));
                return;
            }

            if (!string.Equals(existing.SurfaceId, surfaceId, StringComparison.Ordinal))
            {
                Error?.Invoke(new A2uiError(
                    ErrorCodes.FunctionSurfaceMismatch,
                    $"retry surfaceId '{surfaceId}' does not match call surface '{existing.SurfaceId}'.",
                    surfaceId,
                    functionCallId));
                return;
            }

            if (!TryTransition(existing, FunctionCallState.RetryScheduled, out var retryScheduled))
            {
                Error?.Invoke(new A2uiError(
                    ErrorCodes.FunctionRetryInvalidState,
                    $"functionCallId '{functionCallId}' cannot retry from state '{existing.State}'.",
                    surfaceId,
                    functionCallId));
                return;
            }

            if (!TryTransition(retryScheduled, FunctionCallState.Pending, out var pendingRetry))
            {
                Error?.Invoke(new A2uiError(
                    ErrorCodes.FunctionStateTransitionInvalid,
                    $"retry transition failed for functionCallId '{functionCallId}'.",
                    surfaceId,
                    functionCallId));
                return;
            }

            _functionCalls[functionCallId] = pendingRetry with
            {
                Name = name,
                EnqueueTime = _utcNow(),
                Attempt = existing.Attempt + 1
            };
            return;
        }

        if (isRetry)
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionRetryTargetMissing, $"retry target '{functionCallId}' is not found.", surfaceId, functionCallId));
            return;
        }

        _functionCalls[functionCallId] = new FunctionCallRuntime(
            functionCallId,
            surfaceId,
            name,
            FunctionCallState.Pending,
            _utcNow(),
            Attempt: 1);
    }

    private void HandleFunctionResponse(NormalMessage message)
    {
        var functionCallId = message.FunctionCallId;
        if (string.IsNullOrWhiteSpace(functionCallId))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionCallIdRequired, "functionCallId is required for functionResponse.", message.SurfaceId));
            return;
        }

        if (!_functionCalls.TryGetValue(functionCallId, out var tracked))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionResponseOrphan, $"functionResponse has no call for '{functionCallId}'.", message.SurfaceId, functionCallId));
            return;
        }

        var responseSurfaceId = message.SurfaceId;
        if (!string.IsNullOrWhiteSpace(responseSurfaceId) && !string.Equals(responseSurfaceId, tracked.SurfaceId, StringComparison.Ordinal))
        {
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionSurfaceMismatch,
                $"functionResponse surfaceId '{responseSurfaceId}' does not match call surface '{tracked.SurfaceId}'.",
                responseSurfaceId,
                functionCallId));
            return;
        }

        if (tracked.State != FunctionCallState.Pending)
        {
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionResponseLate,
                $"functionResponse arrived after call '{functionCallId}' reached state '{tracked.State}'.",
                tracked.SurfaceId,
                functionCallId));
            return;
        }

        if (!TryTransition(tracked, FunctionCallState.Completed, out var completed))
        {
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionStateTransitionInvalid,
                $"cannot complete functionCallId '{functionCallId}' from state '{tracked.State}'.",
                tracked.SurfaceId,
                functionCallId));
            return;
        }

        _functionCalls[functionCallId] = completed;
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
            Error?.Invoke(new A2uiError(ErrorCodes.PendingOverflow, $"pending queue overflow for surface '{surfaceId}'", surfaceId));
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
                Error?.Invoke(new A2uiError(ErrorCodes.PendingExpired, $"pending update expired for surface '{surfaceId}'", surfaceId));
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
        if (_functionCalls.Count == 0)
        {
            return;
        }

        var now = _utcNow();
        var pendingIds = _functionCalls.Values
            .Where(call => call.State == FunctionCallState.Pending && now - call.EnqueueTime > _options.FunctionPendingTtl)
            .Select(call => call.FunctionCallId)
            .ToList();

        foreach (var callId in pendingIds)
        {
            var call = _functionCalls[callId];
            if (!TryTransition(call, FunctionCallState.TimedOut, out var timedOut))
            {
                Error?.Invoke(new A2uiError(
                    ErrorCodes.FunctionStateTransitionInvalid,
                    $"cannot timeout functionCallId '{callId}' from state '{call.State}'.",
                    call.SurfaceId,
                    callId));
                continue;
            }

            _functionCalls[callId] = timedOut;
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionTimeout,
                $"function call '{call.Name}' timed out.",
                call.SurfaceId,
                call.FunctionCallId));
        }
    }

    private void CancelFunctionCall(string functionCallId, string surfaceId)
    {
        if (!_functionCalls.TryGetValue(functionCallId, out var tracked))
        {
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionCancelTargetMissing, $"functionCallId '{functionCallId}' is not tracked.", surfaceId, functionCallId));
            return;
        }

        if (!string.Equals(tracked.SurfaceId, surfaceId, StringComparison.Ordinal))
        {
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionSurfaceMismatch,
                $"cancel surfaceId '{surfaceId}' does not match call surface '{tracked.SurfaceId}'.",
                surfaceId,
                functionCallId));
            return;
        }

        if (!TryTransition(tracked, FunctionCallState.Cancelled, out var cancelled))
        {
            Error?.Invoke(new A2uiError(
                ErrorCodes.FunctionCancelInvalidState,
                $"functionCallId '{functionCallId}' cannot be cancelled from state '{tracked.State}'.",
                surfaceId,
                functionCallId));
            return;
        }

        _functionCalls[functionCallId] = cancelled;
        Error?.Invoke(new A2uiError(ErrorCodes.FunctionCancelled, $"functionCallId '{functionCallId}' was cancelled.", surfaceId, functionCallId));
    }

    private void CancelFunctionCallsForSurface(string surfaceId)
    {
        var ids = _functionCalls.Values
            .Where(call => string.Equals(call.SurfaceId, surfaceId, StringComparison.Ordinal))
            .Select(call => call.FunctionCallId)
            .ToList();

        foreach (var id in ids)
        {
            var call = _functionCalls[id];
            if (!TryTransition(call, FunctionCallState.Cancelled, out var cancelled))
            {
                continue;
            }

            _functionCalls[id] = cancelled;
            Error?.Invoke(new A2uiError(ErrorCodes.FunctionCancelled, $"functionCallId '{id}' was cancelled because surface '{surfaceId}' was deleted.", surfaceId, id));
        }
    }

    private static bool TryTransition(FunctionCallRuntime current, FunctionCallState next, out FunctionCallRuntime updated)
    {
        if (!IsValidTransition(current.State, next))
        {
            updated = current;
            return false;
        }

        updated = current with { State = next };
        return true;
    }

    private static bool IsValidTransition(FunctionCallState from, FunctionCallState to)
        => from switch
        {
            FunctionCallState.Pending => to is FunctionCallState.RetryScheduled or FunctionCallState.Cancelled or FunctionCallState.Completed or FunctionCallState.TimedOut,
            FunctionCallState.RetryScheduled => to is FunctionCallState.Pending or FunctionCallState.Cancelled,
            FunctionCallState.Cancelled => false,
            FunctionCallState.Completed => false,
            FunctionCallState.TimedOut => to is FunctionCallState.RetryScheduled,
            _ => false
        };

    private static string? GetOptionalString(JsonNode? node)
    {
        if (node is null) return null;
        return node is JsonValue value && value.TryGetValue<string>(out var str) ? str : null;
    }

    private sealed record PendingUpdate(NormalMessage Message, DateTimeOffset EnqueueTime);

    private sealed record FunctionCallRuntime(
        string FunctionCallId,
        string SurfaceId,
        string Name,
        FunctionCallState State,
        DateTimeOffset EnqueueTime,
        int Attempt);
}
