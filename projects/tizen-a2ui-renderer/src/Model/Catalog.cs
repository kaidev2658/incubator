namespace TizenA2uiRenderer.Model;

public interface ICatalog
{
    string CatalogId { get; }
    bool ValidateComponent(string componentType, IReadOnlyDictionary<string, object?> props, out string? reason);
}
