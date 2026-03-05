namespace AssemblyInspector.Cli.Domain;

public sealed record ExtensionMethodIndex(
    string DeclaringNamespace,
    string DeclaringType,
    string TargetType,
    string MethodName,
    string Signature);