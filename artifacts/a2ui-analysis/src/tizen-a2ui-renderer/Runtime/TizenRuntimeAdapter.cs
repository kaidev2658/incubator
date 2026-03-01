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
    Image,
    Button,
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

public sealed class NuiControlObject
{
    public NuiControlObject(
        string controlId,
        string componentId)
    {
        ControlId = controlId;
        ComponentId = componentId;
        ApplyContainer();
    }

    public string ControlId { get; }
    public string ComponentId { get; }
    public NuiComponentContractKind ContractKind { get; private set; }
    public string Type { get; private set; } = "Container";
    public string? Text { get; private set; }
    public string? Source { get; private set; }
    public string? Color { get; private set; }
    public string? Font { get; private set; }
    public string? Alignment { get; private set; }
    public string? Fit { get; private set; }
    public string? ResizeMode { get; private set; }
    public bool? Enabled { get; private set; }
    public string? Variant { get; private set; }

    public void ApplyText(string? text, string? color, string? font, string? alignment)
    {
        ContractKind = NuiComponentContractKind.Text;
        Type = "Text";
        Text = text;
        Source = null;
        Color = color;
        Font = font;
        Alignment = alignment;
        Fit = null;
        ResizeMode = null;
        Enabled = null;
        Variant = null;
    }

    public void ApplyImage(string? source, string? fit, string? resizeMode)
    {
        ContractKind = NuiComponentContractKind.Image;
        Type = "Image";
        Text = null;
        Source = source;
        Color = null;
        Font = null;
        Alignment = null;
        Fit = fit;
        ResizeMode = resizeMode;
        Enabled = null;
        Variant = null;
    }

    public void ApplyButton(string? text, bool? enabled, string? variant)
    {
        ContractKind = NuiComponentContractKind.Button;
        Type = "Button";
        Text = text;
        Source = null;
        Color = null;
        Font = null;
        Alignment = null;
        Fit = null;
        ResizeMode = null;
        Enabled = enabled;
        Variant = variant;
    }

    public void ApplyContainer()
    {
        ContractKind = NuiComponentContractKind.Container;
        Type = "Container";
        Text = null;
        Source = null;
        Color = null;
        Font = null;
        Alignment = null;
        Fit = null;
        ResizeMode = null;
        Enabled = null;
        Variant = null;
    }
}

public sealed record NuiMaterializedNode(
    string ComponentId,
    string ControlId,
    NuiComponentContractKind ContractKind,
    string Type,
    string? Text,
    string? Source,
    string? Color,
    string? Font,
    string? Alignment,
    string? Fit,
    string? ResizeMode,
    bool? Enabled,
    string? Variant);

public sealed record NuiTextBindingValues(
    string? Text,
    string? Color,
    string? Font,
    string? Alignment);

public sealed record NuiImageBindingValues(
    string? Source,
    string? Fit,
    string? ResizeMode);

public sealed record NuiButtonBindingValues(
    string? Label,
    bool? Enabled,
    string? Variant);

public sealed record NuiSkippedContract(
    string ComponentId,
    string ComponentType,
    string Reason);

public sealed class UnsupportedNuiContractException(
    string componentId,
    string componentType,
    string reason) : InvalidOperationException(reason)
{
    public string ComponentId { get; } = componentId;
    public string ComponentType { get; } = componentType;
}

public sealed record NuiSurfaceRenderState(
    string SurfaceId,
    string RootId,
    IReadOnlyDictionary<string, NuiMaterializedNode> Nodes,
    IReadOnlyList<NuiSkippedContract> SkippedContracts);

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
    private readonly Dictionary<string, Dictionary<string, NuiControlObject>> _surfaceControls = [];
    private readonly Dictionary<string, List<NuiSkippedContract>> _surfaceSkippedContracts = [];
    private readonly Dictionary<string, string> _surfaceRoots = [];
    private bool _initialized;

    public NuiBindingHooks(
        bool hostSupportsNativeBinding,
        bool strictUnsupportedContracts = false,
        string bindingName = "tizen-nui-binding-scaffold")
    {
        HostSupportsNativeBinding = hostSupportsNativeBinding;
        StrictUnsupportedContracts = strictUnsupportedContracts;
        BindingName = string.IsNullOrWhiteSpace(bindingName)
            ? "tizen-nui-binding-scaffold"
            : bindingName.Trim();
    }

    public bool HostSupportsNativeBinding { get; }
    public bool StrictUnsupportedContracts { get; }
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

        if (!_surfaceControls.TryGetValue(surfaceId, out var controls))
        {
            controls = new Dictionary<string, NuiControlObject>(StringComparer.Ordinal);
            _surfaceControls[surfaceId] = controls;
        }

        var touchedComponentIds = new HashSet<string>(StringComparer.Ordinal);
        var skippedContracts = new List<NuiSkippedContract>();

        foreach (var component in LastBindingPlan.Components)
        {
            switch (component.ContractKind)
            {
                case NuiComponentContractKind.Text:
                    var textValues = ResolveTextValues(component.ComponentId, definition, dataModel);
                    GetOrCreateControl(controls, surfaceId, component.ComponentId)
                        .ApplyText(
                            textValues.Text,
                            textValues.Color,
                            textValues.Font,
                            textValues.Alignment);
                    touchedComponentIds.Add(component.ComponentId);
                    break;
                case NuiComponentContractKind.Image:
                    var imageValues = ResolveImageValues(component.ComponentId, definition, dataModel);
                    GetOrCreateControl(controls, surfaceId, component.ComponentId)
                        .ApplyImage(
                            imageValues.Source,
                            imageValues.Fit,
                            imageValues.ResizeMode);
                    touchedComponentIds.Add(component.ComponentId);
                    break;
                case NuiComponentContractKind.Button:
                    var buttonValues = ResolveButtonValues(component.ComponentId, definition, dataModel);
                    GetOrCreateControl(controls, surfaceId, component.ComponentId)
                        .ApplyButton(
                            buttonValues.Label,
                            buttonValues.Enabled,
                            buttonValues.Variant);
                    touchedComponentIds.Add(component.ComponentId);
                    break;
                case NuiComponentContractKind.Container:
                    GetOrCreateControl(controls, surfaceId, component.ComponentId).ApplyContainer();
                    touchedComponentIds.Add(component.ComponentId);
                    break;
                default:
                    var reason = $"Unsupported component contract '{component.ComponentType}'.";
                    skippedContracts.Add(new NuiSkippedContract(
                        component.ComponentId,
                        component.ComponentType,
                        reason));

                    if (StrictUnsupportedContracts)
                    {
                        throw new UnsupportedNuiContractException(
                            component.ComponentId,
                            component.ComponentType,
                            reason);
                    }
                    break;
            }
        }

        var staleNodeIds = controls.Keys
            .Where(componentId => !touchedComponentIds.Contains(componentId))
            .ToList();
        foreach (var staleNodeId in staleNodeIds)
        {
            controls.Remove(staleNodeId);
        }

        _surfaceRoots[surfaceId] = definition.RootId;
        _surfaceSkippedContracts[surfaceId] = skippedContracts;
    }

    public void Remove(string surfaceId)
    {
        EnsureInitialized();
        _surfaceControls.Remove(surfaceId);
        _surfaceSkippedContracts.Remove(surfaceId);
        _surfaceRoots.Remove(surfaceId);
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
            "image" => NuiComponentContractKind.Image,
            "button" => NuiComponentContractKind.Button,
            "column" or "row" or "container" => NuiComponentContractKind.Container,
            _ => NuiComponentContractKind.Unsupported
        };
    }

    public NuiSurfaceRenderState? GetSurfaceRenderState(string surfaceId)
    {
        if (!_surfaceControls.TryGetValue(surfaceId, out var controls))
        {
            return null;
        }

        var rootId = _surfaceRoots.GetValueOrDefault(surfaceId, string.Empty);
        var skipped = _surfaceSkippedContracts.GetValueOrDefault(surfaceId) ?? [];
        var nodesSnapshot = controls
            .OrderBy(entry => entry.Key, StringComparer.Ordinal)
            .ToDictionary(
                entry => entry.Key,
                entry => new NuiMaterializedNode(
                    entry.Key,
                    entry.Value.ControlId,
                    entry.Value.ContractKind,
                    entry.Value.Type,
                    entry.Value.Text,
                    entry.Value.Source,
                    entry.Value.Color,
                    entry.Value.Font,
                    entry.Value.Alignment,
                    entry.Value.Fit,
                    entry.Value.ResizeMode,
                    entry.Value.Enabled,
                    entry.Value.Variant),
                StringComparer.Ordinal);
        var skippedSnapshot = skipped
            .OrderBy(entry => entry.ComponentId, StringComparer.Ordinal)
            .ToList();

        return new NuiSurfaceRenderState(
            surfaceId,
            rootId,
            nodesSnapshot,
            skippedSnapshot);
    }

    private static NuiTextBindingValues ResolveTextValues(string componentId, SurfaceDefinition definition, DataModel dataModel)
    {
        JsonObject? componentObject = null;
        if (definition.Components.TryGetPropertyValue(componentId, out var componentNode)
            && componentNode is JsonObject resolvedComponentObject)
        {
            componentObject = resolvedComponentObject;
            return new NuiTextBindingValues(
                ResolveStringField(componentId, componentObject, dataModel, "text"),
                ResolveStringField(componentId, componentObject, dataModel, "color"),
                ResolveStringField(componentId, componentObject, dataModel, "font"),
                ResolveStringField(componentId, componentObject, dataModel, "alignment"));
        }

        return new NuiTextBindingValues(
            ResolveStringField(componentId, componentObject, dataModel, "text"),
            ResolveStringField(componentId, componentObject, dataModel, "color"),
            ResolveStringField(componentId, componentObject, dataModel, "font"),
            ResolveStringField(componentId, componentObject, dataModel, "alignment"));
    }

    private static NuiImageBindingValues ResolveImageValues(string componentId, SurfaceDefinition definition, DataModel dataModel)
    {
        JsonObject? componentObject = null;
        if (definition.Components.TryGetPropertyValue(componentId, out var componentNode)
            && componentNode is JsonObject resolvedComponentObject)
        {
            componentObject = resolvedComponentObject;
            return new NuiImageBindingValues(
                ResolveStringField(componentId, componentObject, dataModel, "src"),
                ResolveStringField(componentId, componentObject, dataModel, "fit"),
                ResolveStringField(componentId, componentObject, dataModel, "resizeMode"));
        }

        return new NuiImageBindingValues(
            ResolveStringField(componentId, componentObject, dataModel, "src"),
            ResolveStringField(componentId, componentObject, dataModel, "fit"),
            ResolveStringField(componentId, componentObject, dataModel, "resizeMode"));
    }

    private static NuiButtonBindingValues ResolveButtonValues(string componentId, SurfaceDefinition definition, DataModel dataModel)
    {
        JsonObject? componentObject = null;
        if (definition.Components.TryGetPropertyValue(componentId, out var componentNode)
            && componentNode is JsonObject resolvedComponentObject)
        {
            componentObject = resolvedComponentObject;
            return new NuiButtonBindingValues(
                ResolveButtonLabelField(componentId, componentObject, dataModel),
                ResolveBooleanField(componentId, componentObject, dataModel, "enabled"),
                ResolveStringField(componentId, componentObject, dataModel, "variant"));
        }

        return new NuiButtonBindingValues(
            ResolveButtonLabelField(componentId, componentObject, dataModel),
            ResolveBooleanField(componentId, componentObject, dataModel, "enabled"),
            ResolveStringField(componentId, componentObject, dataModel, "variant"));
    }

    private static string? TryReadText(JsonNode? value)
        => value?.ToString();

    private static string? ResolveStringField(
        string componentId,
        JsonObject? componentObject,
        DataModel dataModel,
        string field)
    {
        var propsField = TryReadText(componentObject?["props"]?[field]);
        if (propsField is not null)
        {
            return propsField;
        }

        var directField = TryReadText(componentObject?[field]);
        if (directField is not null)
        {
            return directField;
        }

        var modelField = TryReadText(dataModel.Get($"{componentId}.{field}"));
        if (modelField is not null)
        {
            return modelField;
        }

        if (field == "text")
        {
            return TryReadText(dataModel.Get(componentId));
        }

        return null;
    }

    private static string? ResolveButtonLabelField(
        string componentId,
        JsonObject? componentObject,
        DataModel dataModel)
    {
        var propsText = TryReadText(componentObject?["props"]?["text"]);
        if (propsText is not null)
        {
            return propsText;
        }

        var propsLabel = TryReadText(componentObject?["props"]?["label"]);
        if (propsLabel is not null)
        {
            return propsLabel;
        }

        var directText = TryReadText(componentObject?["text"]);
        if (directText is not null)
        {
            return directText;
        }

        var directLabel = TryReadText(componentObject?["label"]);
        if (directLabel is not null)
        {
            return directLabel;
        }

        return TryReadText(dataModel.Get($"{componentId}.text"))
            ?? TryReadText(dataModel.Get($"{componentId}.label"))
            ?? TryReadText(dataModel.Get(componentId));
    }

    private static bool? ResolveBooleanField(
        string componentId,
        JsonObject? componentObject,
        DataModel dataModel,
        string field)
    {
        var propsField = TryReadBool(componentObject?["props"]?[field]);
        if (propsField.HasValue)
        {
            return propsField;
        }

        var directField = TryReadBool(componentObject?[field]);
        if (directField.HasValue)
        {
            return directField;
        }

        return TryReadBool(dataModel.Get($"{componentId}.{field}"));
    }

    private static bool? TryReadBool(JsonNode? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is JsonValue jsonValue)
        {
            if (jsonValue.TryGetValue<bool>(out var boolValue))
            {
                return boolValue;
            }
        }

        if (bool.TryParse(value.ToString(), out var parsed))
        {
            return parsed;
        }

        return null;
    }

    private static NuiControlObject GetOrCreateControl(
        IDictionary<string, NuiControlObject> controls,
        string surfaceId,
        string componentId)
    {
        if (controls.TryGetValue(componentId, out var existing))
        {
            return existing;
        }

        var created = new NuiControlObject(
            BuildControlId(surfaceId, componentId),
            componentId);
        controls[componentId] = created;
        return created;
    }

    private static string BuildControlId(string surfaceId, string componentId)
        => $"{surfaceId}:{componentId}";
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

        try
        {
            _bindingHooks.Render(surfaceId, definition, dataModel);
        }
        catch (UnsupportedNuiContractException ex)
        {
            throw new InvalidOperationException(
                $"{ErrorCodes.RuntimeAdapterCapabilityMissing}: " +
                $"Tizen binding '{_bindingHooks.BindingName}' cannot render unsupported contract " +
                $"'{ex.ComponentType}' for component '{ex.ComponentId}'.",
                ex);
        }
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
