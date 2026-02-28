using System.Text;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Runtime;

public interface ITizenRuntimeAdapter
{
    void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel);
    void Remove(string surfaceId);
}

public sealed class NullTizenRuntimeAdapter : ITizenRuntimeAdapter
{
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
    private int _sequence;

    public IReadOnlyList<RuntimeOperation> Operations => _operations;

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
