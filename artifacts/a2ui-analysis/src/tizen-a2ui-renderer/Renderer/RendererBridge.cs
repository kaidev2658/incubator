using System.Text;
using System.Text.Json.Nodes;
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

public enum RenderOperationType
{
    Render,
    Remove
}

public sealed record RenderOperation(
    int Sequence,
    RenderOperationType Type,
    string SurfaceId,
    string? RootId,
    JsonObject? Components,
    JsonObject? DataModel);

public sealed class RecordingRendererBridge : IRendererBridge
{
    private readonly List<RenderOperation> _operations = [];
    private int _sequence;

    public IReadOnlyList<RenderOperation> Operations => _operations;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
    {
        lock (_operations)
        {
            _sequence++;
            _operations.Add(new RenderOperation(
                _sequence,
                RenderOperationType.Render,
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
            _operations.Add(new RenderOperation(
                _sequence,
                RenderOperationType.Remove,
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

                if (op.Type == RenderOperationType.Render)
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
