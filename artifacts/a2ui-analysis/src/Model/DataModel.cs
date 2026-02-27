namespace TizenA2uiRenderer.Model;

public sealed class DataModel
{
    private readonly Dictionary<string, object?> _values = new();

    public object? Get(string path) => _values.TryGetValue(path, out var value) ? value : null;

    public void Set(string path, object? value) => _values[path] = value;

    public void Delete(string path) => _values.Remove(path);

    public IReadOnlyDictionary<string, object?> Snapshot() => _values;
}
