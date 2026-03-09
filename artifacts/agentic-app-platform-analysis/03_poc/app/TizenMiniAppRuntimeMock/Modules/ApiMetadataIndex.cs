using System.Text.Json;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class ApiMetadataIndex
{
    private readonly HashSet<string> _allowedActions;

    private ApiMetadataIndex(string indexPath, IEnumerable<string> allowedActions)
    {
        IndexPath = indexPath;
        _allowedActions = new HashSet<string>(allowedActions.Select(Normalize).Where(x => !string.IsNullOrWhiteSpace(x)), StringComparer.OrdinalIgnoreCase);
    }

    public string IndexPath { get; }

    public IReadOnlyCollection<string> AllowedActions => _allowedActions.OrderBy(x => x).ToArray();

    public static ApiMetadataIndex LoadDefault()
    {
        var indexPath = LocateDefaultIndexPath();
        if (indexPath is null)
        {
            return new ApiMetadataIndex("missing: agent-core/api-index/allowed-apis.json", Array.Empty<string>());
        }

        try
        {
            using var fs = File.OpenRead(indexPath);
            var doc = JsonSerializer.Deserialize<ApiIndexDocument>(
                fs,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var allowed = doc?.AllowedApis?.Select(x => x.Id ?? string.Empty) ?? Enumerable.Empty<string>();
            return new ApiMetadataIndex(indexPath, allowed);
        }
        catch
        {
            return new ApiMetadataIndex($"invalid: {indexPath}", Array.Empty<string>());
        }
    }

    public bool IsAllowed(string action)
    {
        return _allowedActions.Contains(Normalize(action));
    }

    public string RenderAllowedActions()
    {
        if (_allowedActions.Count == 0)
        {
            return "(none)";
        }

        return string.Join(", ", _allowedActions.OrderBy(x => x));
    }

    private static string Normalize(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
    }

    private static string? LocateDefaultIndexPath()
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (current is not null)
        {
            var candidate = Path.Combine(current.FullName, "agent-core", "api-index", "allowed-apis.json");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            current = current.Parent;
        }

        return null;
    }

    private sealed class ApiIndexDocument
    {
        public ApiEntry[]? AllowedApis { get; init; }
    }

    private sealed class ApiEntry
    {
        public string? Id { get; init; }
    }
}
