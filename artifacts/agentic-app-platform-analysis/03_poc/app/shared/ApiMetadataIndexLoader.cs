using System.Text.Json;

namespace TizenMiniApp.Shared.Scn01;

public static class ApiMetadataIndexLoader
{
    public static PolicyEvaluator LoadDefaultPolicyEvaluator()
    {
        var indexPath = LocateDefaultIndexPath();
        if (indexPath is null)
        {
            return new PolicyEvaluator("missing: agent-core/api-index/allowed-apis.json", Array.Empty<string>());
        }

        try
        {
            using var fs = File.OpenRead(indexPath);
            var doc = JsonSerializer.Deserialize<ApiIndexDocument>(
                fs,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var allowed = doc?.AllowedApis?.Select(x => x.Id ?? string.Empty) ?? Enumerable.Empty<string>();
            return new PolicyEvaluator(indexPath, allowed);
        }
        catch
        {
            return new PolicyEvaluator($"invalid: {indexPath}", Array.Empty<string>());
        }
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
