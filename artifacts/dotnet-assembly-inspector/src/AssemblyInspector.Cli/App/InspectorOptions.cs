namespace AssemblyInspector.Cli.App;

public sealed record InspectorOptions(
    string InputPath,
    string OutputDirectory,
    string? Tfm,
    bool AllTfms);
