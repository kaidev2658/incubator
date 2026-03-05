using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.Mcp;

public sealed record InspectNugetPackageAssemblyResult(
    string Tfm,
    string AssemblyName,
    ApiIndex ApiIndex,
    string ApiSummaryMarkdown);

public sealed record InspectNugetPackageResponse(
    string NupkgPath,
    IReadOnlyList<InspectNugetPackageAssemblyResult> Assemblies);
