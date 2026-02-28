using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Utils;

namespace TizenA2uiRenderer.Runtime;

public sealed class RendererBridgeRuntimeAdapter(IRendererBridge rendererBridge) : ITizenRuntimeAdapter
{
    private readonly IRendererBridge _rendererBridge = rendererBridge ?? throw new ArgumentNullException(nameof(rendererBridge));
    private RuntimeAdapterStatus _status = new(
        nameof(RendererBridgeRuntimeAdapter),
        RuntimeMode: "renderer-bridge",
        new RuntimeAdapterCapabilities(
            SupportsRender: true,
            SupportsRemove: true,
            SupportsRealTizenBinding: false,
            IsInitialized: false),
        []);

    public string BridgeTypeName => _rendererBridge.GetType().Name;
    public bool IsNoopBridge => _rendererBridge is NullRendererBridge;

    public RuntimeAdapterStatus Initialize()
    {
        var diagnostics = new List<A2uiError>();
        if (IsNoopBridge)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterIntegrationInvalid,
                "RendererBridgeRuntimeAdapter is bound to NullRendererBridge. Replace with real Tizen bridge."));
        }

        _status = new RuntimeAdapterStatus(
            nameof(RendererBridgeRuntimeAdapter),
            RuntimeMode: "renderer-bridge",
            new RuntimeAdapterCapabilities(
                SupportsRender: true,
                SupportsRemove: true,
                SupportsRealTizenBinding: !IsNoopBridge,
                IsInitialized: diagnostics.Count == 0),
            diagnostics);

        return _status;
    }

    public RuntimeAdapterStatus GetStatus() => _status;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
        => _rendererBridge.Render(surfaceId, definition, dataModel);

    public void Remove(string surfaceId)
        => _rendererBridge.Remove(surfaceId);
}
