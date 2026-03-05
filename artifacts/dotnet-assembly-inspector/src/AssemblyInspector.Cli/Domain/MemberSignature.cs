namespace AssemblyInspector.Cli.Domain;

public sealed record MemberSignature(
    string Kind,
    string Name,
    string Signature);
