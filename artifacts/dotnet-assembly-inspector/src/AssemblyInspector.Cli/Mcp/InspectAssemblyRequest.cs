namespace AssemblyInspector.Cli.Mcp;

public sealed record InspectAssemblyRequest(
    string AssemblyPath,
    IReadOnlyList<string>? DependencySearchPaths = null);
