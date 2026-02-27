using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Renderer;

public interface IRendererBridge
{
    void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel);
    void Remove(string surfaceId);
}

public sealed class NullRendererBridge : IRendererBridge
{
    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel) { }
    public void Remove(string surfaceId) { }
}
