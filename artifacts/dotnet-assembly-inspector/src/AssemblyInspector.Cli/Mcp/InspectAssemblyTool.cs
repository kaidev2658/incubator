using AssemblyInspector.Cli.App;

namespace AssemblyInspector.Cli.Mcp;

public sealed class InspectAssemblyTool
{
    private readonly IAssemblyInspector _inspector;
    private readonly MarkdownReportWriter _markdownWriter;

    public InspectAssemblyTool(IAssemblyInspector inspector, MarkdownReportWriter markdownWriter)
    {
        _inspector = inspector;
        _markdownWriter = markdownWriter;
    }

    public Task<InspectAssemblyResponse> ExecuteAsync(InspectAssemblyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AssemblyPath))
        {
            throw new ArgumentException("AssemblyPath is required.", nameof(request));
        }

        var assemblyPath = Path.GetFullPath(request.AssemblyPath);
        if (!File.Exists(assemblyPath))
        {
            throw new FileNotFoundException($"Assembly not found: {assemblyPath}", assemblyPath);
        }

        if (!string.Equals(Path.GetExtension(assemblyPath), ".dll", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException($"inspect_assembly supports only .dll inputs: {assemblyPath}");
        }

        var dependencySearchPaths = NormalizeDependencySearchPaths(request.DependencySearchPaths);
        var apiIndex = _inspector.Inspect(assemblyPath, dependencySearchPaths);
        var markdown = _markdownWriter.Render(apiIndex);

        return Task.FromResult(new InspectAssemblyResponse(apiIndex, markdown));
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
