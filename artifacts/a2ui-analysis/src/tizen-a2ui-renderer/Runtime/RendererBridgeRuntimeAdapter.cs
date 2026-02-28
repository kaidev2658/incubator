using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;

namespace TizenA2uiRenderer.Runtime;

public sealed class RendererBridgeRuntimeAdapter(IRendererBridge rendererBridge) : ITizenRuntimeAdapter
{
    private readonly IRendererBridge _rendererBridge = rendererBridge ?? throw new ArgumentNullException(nameof(rendererBridge));

    public string BridgeTypeName => _rendererBridge.GetType().Name;
    public bool IsNoopBridge => _rendererBridge is NullRendererBridge;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
        => _rendererBridge.Render(surfaceId, definition, dataModel);

    public void Remove(string surfaceId)
        => _rendererBridge.Remove(surfaceId);
}
