namespace AssemblyInspector.Cli.Mcp;

public sealed record InspectNugetPackageRequest(
    string NupkgPath,
    string? Tfm = null,
    bool AllTfms = false);
