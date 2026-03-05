using AssemblyInspector.Cli.App;

namespace AssemblyInspector.Cli.Mcp;

public sealed class InspectNugetPackageTool
{
    private readonly NugetPackageInspector _packageInspector;
    private readonly MarkdownReportWriter _markdownWriter;

    public InspectNugetPackageTool(NugetPackageInspector packageInspector, MarkdownReportWriter markdownWriter)
    {
        _packageInspector = packageInspector;
        _markdownWriter = markdownWriter;
    }

    public async Task<InspectNugetPackageResponse> ExecuteAsync(InspectNugetPackageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NupkgPath))
        {
            throw new ArgumentException("NupkgPath is required.", nameof(request));
        }

        var nupkgPath = Path.GetFullPath(request.NupkgPath);
        var inspectedAssemblies = await _packageInspector.InspectAsync(
            nupkgPath,
            request.Tfm,
            request.AllTfms);

        var assemblyResults = inspectedAssemblies
            .Select(item => new InspectNugetPackageAssemblyResult(
                item.Tfm,
                item.ApiIndex.AssemblyName,
                item.ApiIndex,
                _markdownWriter.Render(item.ApiIndex)))
            .ToArray();

        return new InspectNugetPackageResponse(nupkgPath, assemblyResults);
    }
}
