using System.Text;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Utils;

namespace TizenA2uiRenderer.Runtime;

public sealed record RuntimeAdapterCapabilities(
    bool SupportsRender,
    bool SupportsRemove,
    bool SupportsRealTizenBinding,
    bool IsInitialized);

public sealed record RuntimeAdapterStatus(
    string AdapterType,
    string RuntimeMode,
    RuntimeAdapterCapabilities Capabilities,
    IReadOnlyList<A2uiError> Diagnostics);

public interface ITizenRuntimeAdapter
{
    RuntimeAdapterStatus Initialize();
    RuntimeAdapterStatus GetStatus();
    void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel);
    void Remove(string surfaceId);
}

public interface ITizenBindingHooks
{
    string BindingName { get; }
    bool SupportsRealBinding { get; }
    bool CanRender { get; }
    bool CanRemove { get; }
    bool IsAvailable();
    void Initialize();
    void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel);
    void Remove(string surfaceId);
}

public enum NuiComponentContractKind
{
    Text,
    Container,
    Unsupported
}

public sealed record NuiComponentBinding(
    string ComponentId,
    string ComponentType,
    NuiComponentContractKind ContractKind);

public sealed record NuiSurfaceBindingPlan(
    string SurfaceId,
    string RootId,
    IReadOnlyList<NuiComponentBinding> Components);

public sealed class NullTizenBindingHooks : ITizenBindingHooks
{
    public string BindingName => nameof(NullTizenBindingHooks);
    public bool SupportsRealBinding => false;
    public bool CanRender => false;
    public bool CanRemove => false;
    public bool IsAvailable() => false;
    public void Initialize() { }
    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel) { }
    public void Remove(string surfaceId) { }
}

public sealed class NuiBindingHooks : ITizenBindingHooks
{
    private bool _initialized;

    public NuiBindingHooks(
        bool hostSupportsNativeBinding,
        string bindingName = "tizen-nui-binding-scaffold")
    {
        HostSupportsNativeBinding = hostSupportsNativeBinding;
        BindingName = string.IsNullOrWhiteSpace(bindingName)
            ? "tizen-nui-binding-scaffold"
            : bindingName.Trim();
    }

    public bool HostSupportsNativeBinding { get; }
    public string BindingName { get; }
    public NuiSurfaceBindingPlan? LastBindingPlan { get; private set; }
    public bool SupportsRealBinding => true;
    public bool CanRender => HostSupportsNativeBinding;
    public bool CanRemove => HostSupportsNativeBinding;
    public bool IsAvailable() => HostSupportsNativeBinding;

    public void Initialize()
    {
        if (!HostSupportsNativeBinding)
        {
            throw new InvalidOperationException(
                $"Binding '{BindingName}' is unavailable for current host semantics.");
        }

        _initialized = true;
    }

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
    {
        EnsureInitialized();
        LastBindingPlan = BuildBindingPlan(surfaceId, definition);
        // TODO(phase-b): Replace NUI binding scaffold with actual DALi/NUI control creation and patching.
    }

    public void Remove(string surfaceId)
    {
        EnsureInitialized();
        // TODO(phase-b): Replace deterministic no-op with actual Tizen native remove binding.
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException(
                $"Binding '{BindingName}' must be initialized before runtime operations.");
        }
    }

    public static NuiSurfaceBindingPlan BuildBindingPlan(string surfaceId, SurfaceDefinition definition)
    {
        var components = new List<NuiComponentBinding>();
        foreach (var component in definition.Components)
        {
            var componentType = component.Value?["component"]?.GetValue<string>() ?? "Unknown";
            components.Add(new NuiComponentBinding(
                component.Key,
                componentType,
                ResolveContractKind(componentType)));
        }

        return new NuiSurfaceBindingPlan(surfaceId, definition.RootId, components);
    }

    public static NuiComponentContractKind ResolveContractKind(string? componentType)
    {
        var normalized = componentType?.Trim().ToLowerInvariant() ?? string.Empty;
        return normalized switch
        {
            "text" => NuiComponentContractKind.Text,
            "column" or "row" or "container" => NuiComponentContractKind.Container,
            _ => NuiComponentContractKind.Unsupported
        };
    }
}

public sealed class TizenRuntimeAdapter(ITizenBindingHooks bindingHooks) : ITizenRuntimeAdapter
{
    private readonly ITizenBindingHooks _bindingHooks = bindingHooks ?? throw new ArgumentNullException(nameof(bindingHooks));
    private RuntimeAdapterStatus _status = new(
        nameof(TizenRuntimeAdapter),
        RuntimeMode: "tizen-nui",
        new RuntimeAdapterCapabilities(
            SupportsRender: false,
            SupportsRemove: false,
            SupportsRealTizenBinding: false,
            IsInitialized: false),
        []);

    public RuntimeAdapterStatus Initialize()
    {
        var diagnostics = new List<A2uiError>();
        var isAvailable = _bindingHooks.IsAvailable();

        if (!isAvailable)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterIntegrationInvalid,
                $"Tizen binding '{_bindingHooks.BindingName}' is not available in current runtime."));
        }

        if (!_bindingHooks.CanRender)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterCapabilityMissing,
                $"Tizen binding '{_bindingHooks.BindingName}' does not support render capability."));
        }

        if (!_bindingHooks.CanRemove)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterCapabilityMissing,
                $"Tizen binding '{_bindingHooks.BindingName}' does not support remove capability."));
        }

        var initialized = false;
        if (diagnostics.Count == 0)
        {
            try
            {
                _bindingHooks.Initialize();
                initialized = true;
            }
            catch (Exception ex)
            {
                diagnostics.Add(new A2uiError(
                    ErrorCodes.RuntimeAdapterInitializationFailed,
                    $"Tizen binding initialization failed: {ex.Message}"));
            }
        }

        _status = new RuntimeAdapterStatus(
            nameof(TizenRuntimeAdapter),
            RuntimeMode: "tizen-nui",
            new RuntimeAdapterCapabilities(
                SupportsRender: _bindingHooks.CanRender,
                SupportsRemove: _bindingHooks.CanRemove,
                SupportsRealTizenBinding: _bindingHooks.SupportsRealBinding,
                IsInitialized: initialized),
            diagnostics);

        return _status;
    }

    public RuntimeAdapterStatus GetStatus() => _status;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
    {
        if (!_status.Capabilities.IsInitialized)
        {
            throw new InvalidOperationException("Tizen runtime adapter is not initialized.");
        }

        _bindingHooks.Render(surfaceId, definition, dataModel);
    }

    public void Remove(string surfaceId)
    {
        if (!_status.Capabilities.IsInitialized)
        {
            throw new InvalidOperationException("Tizen runtime adapter is not initialized.");
        }

        _bindingHooks.Remove(surfaceId);
    }
}

public sealed class NullTizenRuntimeAdapter : ITizenRuntimeAdapter
{
    private readonly RuntimeAdapterStatus _status = new(
        nameof(NullTizenRuntimeAdapter),
        RuntimeMode: "null",
        new RuntimeAdapterCapabilities(
            SupportsRender: false,
            SupportsRemove: false,
            SupportsRealTizenBinding: false,
            IsInitialized: false),
        [
            new A2uiError(
                ErrorCodes.RuntimeAdapterNotConfigured,
                "Runtime adapter is NullTizenRuntimeAdapter. Use RendererBridgeRuntimeAdapter or TizenRuntimeAdapter for integration.")
        ]);

    public RuntimeAdapterStatus Initialize() => _status;

    public RuntimeAdapterStatus GetStatus() => _status;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel) { }

    public void Remove(string surfaceId) { }
}

public enum RuntimeOperationType
{
    Render,
    Remove
}

public sealed record RuntimeOperation(
    int Sequence,
    RuntimeOperationType Type,
    string SurfaceId,
    string? RootId,
    JsonObject? Components,
    JsonObject? DataModel);

public sealed class InMemoryRuntimeAdapter : ITizenRuntimeAdapter
{
    private readonly List<RuntimeOperation> _operations = [];
    private readonly RuntimeAdapterStatus _status = new(
        nameof(InMemoryRuntimeAdapter),
        RuntimeMode: "in-memory",
        new RuntimeAdapterCapabilities(
            SupportsRender: true,
            SupportsRemove: true,
            SupportsRealTizenBinding: false,
            IsInitialized: true),
        []);
    private int _sequence;

    public IReadOnlyList<RuntimeOperation> Operations => _operations;

    public RuntimeAdapterStatus Initialize() => _status;

    public RuntimeAdapterStatus GetStatus() => _status;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
    {
        lock (_operations)
        {
            _sequence++;
            _operations.Add(new RuntimeOperation(
                _sequence,
                RuntimeOperationType.Render,
                surfaceId,
                definition.RootId,
                (JsonObject)definition.Components.DeepClone(),
                dataModel.Snapshot()));
        }
    }

    public void Remove(string surfaceId)
    {
        lock (_operations)
        {
            _sequence++;
            _operations.Add(new RuntimeOperation(
                _sequence,
                RuntimeOperationType.Remove,
                surfaceId,
                RootId: null,
                Components: null,
                DataModel: null));
        }
    }

    public string Diagnostics()
    {
        lock (_operations)
        {
            var sb = new StringBuilder();
            foreach (var op in _operations)
            {
                sb.Append(op.Sequence)
                  .Append('|')
                  .Append(op.Type)
                  .Append('|')
                  .Append(op.SurfaceId);

                if (op.Type == RuntimeOperationType.Render)
                {
                    sb.Append("|root=")
                      .Append(op.RootId)
                      .Append("|components=")
                      .Append(op.Components?.ToJsonString())
                      .Append("|model=")
                      .Append(op.DataModel?.ToJsonString());
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
