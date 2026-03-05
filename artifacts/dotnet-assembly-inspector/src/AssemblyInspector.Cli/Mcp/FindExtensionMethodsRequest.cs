namespace AssemblyInspector.Cli.Mcp;

public sealed record FindExtensionMethodsRequest(
    string AssemblyPath,
    string? TargetTypeContains = null,
    string? DeclaringNamespaceContains = null,
    string? MethodNameContains = null,
    IReadOnlyList<string>? DependencySearchPaths = null);
