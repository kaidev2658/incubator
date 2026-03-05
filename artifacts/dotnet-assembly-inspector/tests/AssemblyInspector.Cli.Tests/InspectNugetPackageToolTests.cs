using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Mcp;
using Xunit;

namespace AssemblyInspector.Cli.Tests;

public sealed class InspectNugetPackageToolTests
{
    [Fact]
    public async Task ExecuteAsync_WithSingleTfmPackage_ReturnsAssemblyResults()
    {
        using var workspace = new TestWorkspace();
        var nupkgPath = workspace.CreatePackageWithSingleTfm("net8.0");
        var tool = new InspectNugetPackageTool(
            new NugetPackageInspector(new CecilAssemblyInspector()),
            new MarkdownReportWriter());

        var response = await tool.ExecuteAsync(new InspectNugetPackageRequest(nupkgPath));

        var item = Assert.Single(response.Assemblies);
        Assert.Equal("net8.0", item.Tfm);
        Assert.False(string.IsNullOrWhiteSpace(item.ApiIndex.AssemblyName));
        Assert.Contains("# API Summary:", item.ApiSummaryMarkdown);
    }

    private sealed class TestWorkspace : IDisposable
    {
        private readonly string _rootDirectory = Path.Combine(Path.GetTempPath(), "assembly-inspector-mcp-tests", Guid.NewGuid().ToString("N"));

        public TestWorkspace()
        {
            Directory.CreateDirectory(_rootDirectory);
        }

        public string CreatePackageWithSingleTfm(string tfm)
        {
            var packageLayout = Path.Combine(_rootDirectory, "pkg");
            var targetDir = Path.Combine(packageLayout, "lib", tfm);
            Directory.CreateDirectory(targetDir);
            File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, Path.Combine(targetDir, "AssemblyInspector.Cli.dll"), overwrite: true);

            var nupkgPath = Path.Combine(_rootDirectory, "fixture.nupkg");
            if (File.Exists(nupkgPath))
            {
                File.Delete(nupkgPath);
            }

            ZipFile.CreateFromDirectory(packageLayout, nupkgPath);
            return nupkgPath;
        }

        public void Dispose()
        {
            if (Directory.Exists(_rootDirectory))
            {
                Directory.Delete(_rootDirectory, recursive: true);
            }
        }
    }
}
