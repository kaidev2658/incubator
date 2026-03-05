namespace AssemblyInspector.Cli.Domain;

public sealed record NamespaceIndex(
    string Name,
    IReadOnlyList<TypeIndex> Types);
