using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using Xunit;

namespace AssemblyInspector.Cli.Tests;

public sealed class CecilAssemblyInspectorTests
{
    [Fact]
    public void Inspect_WhenGivenAssembly_ReturnsNamespacesAndTypes()
    {
        var inspector = new CecilAssemblyInspector();
        var assemblyPath = typeof(CecilAssemblyInspector).Assembly.Location;

        var index = inspector.Inspect(assemblyPath);

        Assert.False(string.IsNullOrWhiteSpace(index.AssemblyName));
        Assert.NotEmpty(index.Namespaces);

        var cliNamespace = index.Namespaces.FirstOrDefault(n => n.Name == "AssemblyInspector.Cli.App");
        Assert.NotNull(cliNamespace);
        Assert.Contains(cliNamespace!.Types, t => t.Name == "CecilAssemblyInspector");
    }

    [Fact]
    public async Task RunAsync_WithRequestedTfm_WritesOnlyThatTfmLayout()
    {
        using var workspace = new TestWorkspace();
        var nupkgPath = workspace.CreatePackageWithTfms("net6.0", "net8.0");

        var app = CreateApp();
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, "net8.0", AllTfms: false);

        await app.RunAsync(options);

        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-summary.md")));
        Assert.False(Directory.Exists(Path.Combine(workspace.OutputDirectory, "net6.0")));
    }

    [Fact]
    public async Task RunAsync_WithUnknownRequestedTfm_WritesNothing()
    {
        using var workspace = new TestWorkspace();
        var nupkgPath = workspace.CreatePackageWithTfms("net6.0", "net8.0");

        var app = CreateApp();
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, "net7.0", AllTfms: false);

        await app.RunAsync(options);

        Assert.True(Directory.Exists(workspace.OutputDirectory));
        Assert.Empty(Directory.EnumerateFileSystemEntries(workspace.OutputDirectory));
    }

    [Fact]
    public async Task RunAsync_WithAllTfms_WritesPerTfmLayout()
    {
        using var workspace = new TestWorkspace();
        var nupkgPath = workspace.CreatePackageWithTfms("net6.0", "net8.0");

        var app = CreateApp();
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, Tfm: null, AllTfms: true);

        await app.RunAsync(options);

        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net6.0", "AssemblyInspector.Cli", "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net6.0", "AssemblyInspector.Cli", "api-summary.md")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-summary.md")));
    }

    private static InspectorApp CreateApp()
    {
        return new InspectorApp(new CecilAssemblyInspector(), new JsonReportWriter(), new MarkdownReportWriter());
    }

    private sealed class TestWorkspace : IDisposable
    {
        private readonly string _rootDirectory = Path.Combine(Path.GetTempPath(), "assembly-inspector-cli-tests", Guid.NewGuid().ToString("N"));

        public TestWorkspace()
        {
            Directory.CreateDirectory(_rootDirectory);
            OutputDirectory = Path.Combine(_rootDirectory, "out");
        }

        public string OutputDirectory { get; }

        public string CreatePackageWithTfms(params string[] tfms)
        {
            var packageLayout = Path.Combine(_rootDirectory, "pkg");
            foreach (var tfm in tfms)
            {
                var targetDir = Path.Combine(packageLayout, "lib", tfm);
                Directory.CreateDirectory(targetDir);
                File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, Path.Combine(targetDir, "AssemblyInspector.Cli.dll"), overwrite: true);
            }

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
