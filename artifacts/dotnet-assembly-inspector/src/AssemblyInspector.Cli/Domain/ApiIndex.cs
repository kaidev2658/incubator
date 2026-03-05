namespace AssemblyInspector.Cli.Domain;

public sealed record ApiIndex(
    string AssemblyName,
    string SourcePath,
    DateTimeOffset GeneratedAtUtc,
    IReadOnlyList<NamespaceIndex> Namespaces,
    IReadOnlyList<ExtensionMethodIndex> ExtensionMethods);