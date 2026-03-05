using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.Mcp;

public sealed class FindExtensionMethodsTool
{
    private readonly IAssemblyInspector _inspector;

    public FindExtensionMethodsTool(IAssemblyInspector inspector)
    {
        _inspector = inspector;
    }

    public Task<FindExtensionMethodsResponse> ExecuteAsync(FindExtensionMethodsRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AssemblyPath))
        {
            throw new ArgumentException("AssemblyPath is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.TargetTypeContains) &&
            string.IsNullOrWhiteSpace(request.DeclaringNamespaceContains) &&
            string.IsNullOrWhiteSpace(request.MethodNameContains))
        {
            throw new ArgumentException(
                "At least one filter is required: TargetTypeContains, DeclaringNamespaceContains, or MethodNameContains.",
                nameof(request));
        }

        var assemblyPath = Path.GetFullPath(request.AssemblyPath);
        if (!File.Exists(assemblyPath))
        {
            throw new FileNotFoundException($"Assembly not found: {assemblyPath}", assemblyPath);
        }

        if (!string.Equals(Path.GetExtension(assemblyPath), ".dll", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException($"find_extension_methods supports only .dll inputs: {assemblyPath}");
        }

        var dependencySearchPaths = NormalizeDependencySearchPaths(request.DependencySearchPaths);
        var apiIndex = _inspector.Inspect(assemblyPath, dependencySearchPaths);

        var matches = apiIndex.ExtensionMethods
            .Where(method => MatchesContains(method.TargetType, request.TargetTypeContains))
            .Where(method => MatchesContains(method.DeclaringNamespace, request.DeclaringNamespaceContains))
            .Where(method => MatchesContains(method.MethodName, request.MethodNameContains))
            .ToArray();

        return Task.FromResult(
            new FindExtensionMethodsResponse(
                assemblyPath,
                request.TargetTypeContains,
                request.DeclaringNamespaceContains,
                request.MethodNameContains,
                matches));
    }

    private static bool MatchesContains(string source, string? filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return true;
        }

        return source.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }

    private static IReadOnlyList<string> NormalizeDependencySearchPaths(IReadOnlyList<string>? dependencySearchPaths)
    {
        if (dependencySearchPaths is null || dependencySearchPaths.Count == 0)
        {
            return Array.Empty<string>();
        }

        return dependencySearchPaths
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => Path.GetFullPath(path))
            .Where(Directory.Exists)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}
