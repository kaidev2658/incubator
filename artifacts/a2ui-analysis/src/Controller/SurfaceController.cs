using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Controller;

public sealed class ControllerOptions
{
    public TimeSpan PendingTtl { get; init; } = TimeSpan.FromSeconds(60);
}

public sealed class SurfaceController
{
    private readonly Dictionary<string, SurfaceDefinition> _surfaces = new();
    private readonly Dictionary<string, DataModel> _dataModels = new();

    public event Action<SurfaceUpdate>? SurfaceUpdated;
    public event Action<A2uiError>? Error;

    public void HandleMessage(NormalMessage message)
    {
        switch (message.Type)
        {
            case NormalMessageType.CreateSurface:
            case NormalMessageType.UpdateComponents:
            case NormalMessageType.UpdateDataModel:
                if (string.IsNullOrWhiteSpace(message.SurfaceId))
                {
                    Error?.Invoke(new A2uiError("E_SURFACE_NOT_FOUND", "surfaceId is required"));
                    return;
                }
                EnsureSurface(message.SurfaceId);
                var def = _surfaces[message.SurfaceId];
                var model = _dataModels[message.SurfaceId];
                SurfaceUpdated?.Invoke(new SurfaceUpdate(message.SurfaceId, def, model));
                break;
            case NormalMessageType.DeleteSurface:
                if (!string.IsNullOrWhiteSpace(message.SurfaceId))
                {
                    _surfaces.Remove(message.SurfaceId);
                    _dataModels.Remove(message.SurfaceId);
                }
                break;
        }
    }

    private void EnsureSurface(string surfaceId)
    {
        if (!_surfaces.ContainsKey(surfaceId))
            _surfaces[surfaceId] = new SurfaceDefinition(surfaceId, "root", new Dictionary<string, object?>());
        if (!_dataModels.ContainsKey(surfaceId))
            _dataModels[surfaceId] = new DataModel();
    }
}
