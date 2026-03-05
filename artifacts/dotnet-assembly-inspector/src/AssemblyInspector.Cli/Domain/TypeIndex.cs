namespace AssemblyInspector.Cli.Domain;

public sealed record TypeIndex(
    string Name,
    string FullName,
    string Kind,
    string? BaseType,
    IReadOnlyList<string> Interfaces,
    IReadOnlyList<MemberSignature> Members);
