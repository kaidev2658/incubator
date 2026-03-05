using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Domain;
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

    [Fact]
    public void Inspect_FormatsNestedTypeSignaturesWithDeclaringType()
    {
        var inspector = new CecilAssemblyInspector();
        var assemblyPath = typeof(CecilAssemblyInspectorTests).Assembly.Location;

        var index = inspector.Inspect(assemblyPath);
        var methods = GetTypeMembers(index, "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.NestedTypeConsumer");
        var method = methods.Single(member => member.Name == nameof(SignatureFixtureTypes.NestedTypeConsumer.Wrap));

        Assert.Equal(
            "public SignatureContainer<String>.Nested Wrap(SignatureContainer<String>.Nested value)",
            method.Signature);
    }

    [Fact]
    public void Inspect_FormatsGenericMethodConstraints()
    {
        var inspector = new CecilAssemblyInspector();
        var assemblyPath = typeof(CecilAssemblyInspectorTests).Assembly.Location;

        var index = inspector.Inspect(assemblyPath);
        var methods = GetTypeMembers(index, "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.ConstraintType");
        var method = methods.Single(member => member.Name == nameof(SignatureFixtureTypes.ConstraintType.Transform));

        Assert.Equal(
            "public TResult Transform<TResult, TInput>(TInput input) where TResult : class, new() where TInput : IEnumerable<TResult>",
            method.Signature);
    }

    [Fact]
    public void Inspect_FormatsExplicitInterfaceMembersWithoutVisibility()
    {
        var inspector = new CecilAssemblyInspector();
        var assemblyPath = typeof(CecilAssemblyInspectorTests).Assembly.Location;

        var index = inspector.Inspect(assemblyPath);
        var members = GetTypeMembers(index, "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.ExplicitImplementationType");

        var method = members.Single(member => member.Kind == "method" && member.Name == "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Run");
        var property = members.Single(member => member.Kind == "property" && member.Name == "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Title");
        var @event = members.Single(member => member.Kind == "event" && member.Name == "AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Changed");

        Assert.Equal("Void AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Run()", method.Signature);
        Assert.Equal("String AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Title { get; }", property.Signature);
        Assert.Equal("event EventHandler AssemblyInspector.Cli.Tests.SignatureFixtureTypes.IExplicitContract.Changed", @event.Signature);
    }

    private static IReadOnlyList<MemberSignature> GetTypeMembers(ApiIndex index, string fullTypeName)
    {
        return index.Namespaces
            .SelectMany(@namespace => @namespace.Types)
            .Single(type => type.FullName == fullTypeName)
            .Members;
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
