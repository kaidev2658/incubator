using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.Mcp;

public sealed record FindExtensionMethodsResponse(
    string AssemblyPath,
    string? TargetTypeContains,
    string? DeclaringNamespaceContains,
    string? MethodNameContains,
    IReadOnlyList<ExtensionMethodIndex> Matches);
