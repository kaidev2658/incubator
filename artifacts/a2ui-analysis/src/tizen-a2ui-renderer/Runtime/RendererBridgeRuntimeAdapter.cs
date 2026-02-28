using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;

namespace TizenA2uiRenderer.Runtime;

public sealed class RendererBridgeRuntimeAdapter(IRendererBridge rendererBridge) : ITizenRuntimeAdapter
{
    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
        => rendererBridge.Render(surfaceId, definition, dataModel);

    public void Remove(string surfaceId)
        => rendererBridge.Remove(surfaceId);
}
