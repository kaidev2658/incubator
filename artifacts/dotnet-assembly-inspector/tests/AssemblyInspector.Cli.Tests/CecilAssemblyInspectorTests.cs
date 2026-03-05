using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
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
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, "net8.0", AllTfms: false, CompactJson: false);

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
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, "net7.0", AllTfms: false, CompactJson: false);

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
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, Tfm: null, AllTfms: true, CompactJson: false);

        await app.RunAsync(options);

        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net6.0", "AssemblyInspector.Cli", "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net6.0", "AssemblyInspector.Cli", "api-summary.md")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "net8.0", "AssemblyInspector.Cli", "api-summary.md")));
    }

    [Fact]
    public async Task RunAsync_WithDllInput_PassesNearbyDependencyDirectories()
    {
        using var workspace = new TestWorkspace();
        var assemblyPath = workspace.CreateStandaloneAssembly();
        var siblingDependencyDirectory = workspace.CreateSiblingDependencyDirectory(Path.GetDirectoryName(assemblyPath)!);
        var inspector = new RecordingAssemblyInspector();
        var app = CreateApp(inspector);
        var options = new InspectorOptions(assemblyPath, workspace.OutputDirectory, Tfm: null, AllTfms: false, CompactJson: false);

        await app.RunAsync(options);

        var invocation = Assert.Single(inspector.Invocations);
        Assert.Equal(Path.GetFullPath(assemblyPath), Path.GetFullPath(invocation.AssemblyPath));
        Assert.Contains(Path.GetFullPath(siblingDependencyDirectory), invocation.DependencySearchPaths, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RunAsync_WithNupkgInput_PassesTfmDependencyDirectories()
    {
        using var workspace = new TestWorkspace();
        var nupkgPath = workspace.CreatePackageWithTfms("net8.0");
        var inspector = new RecordingAssemblyInspector();
        var app = CreateApp(inspector);
        var options = new InspectorOptions(nupkgPath, workspace.OutputDirectory, "net8.0", AllTfms: false, CompactJson: false);

        await app.RunAsync(options);

        var invocation = Assert.Single(inspector.Invocations);
        Assert.Contains(invocation.DependencySearchPaths, path => path.EndsWith(Path.Combine("lib", "net8.0"), StringComparison.OrdinalIgnoreCase));
        Assert.Contains(invocation.DependencySearchPaths, path => path.EndsWith(Path.Combine("ref", "net8.0"), StringComparison.OrdinalIgnoreCase));
        Assert.Contains(invocation.DependencySearchPaths, path => path.EndsWith(Path.Combine("runtimes", "any", "lib", "net8.0"), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task RunAsync_WithCompactJson_WritesCompactApiIndex()
    {
        using var workspace = new TestWorkspace();
        var assemblyPath = workspace.CreateStandaloneAssembly();

        var app = CreateApp();
        var options = new InspectorOptions(assemblyPath, workspace.OutputDirectory, Tfm: null, AllTfms: false, CompactJson: true);

        await app.RunAsync(options);

        var jsonPath = Path.Combine(workspace.OutputDirectory, "api-index.json");
        Assert.True(File.Exists(jsonPath));

        using var document = JsonDocument.Parse(await File.ReadAllTextAsync(jsonPath));
        var root = document.RootElement;
        Assert.True(root.TryGetProperty("f", out _));
        Assert.False(root.TryGetProperty("AssemblyName", out _));
    }

    [Fact]
    public async Task RunAsync_WithNamespaceChunking_WritesPredictableChunkFileNames()
    {
        using var workspace = new TestWorkspace();
        var assemblyPath = workspace.CreateStandaloneAssembly();
        var app = CreateApp(new StaticAssemblyInspector(CreateChunkFixtureIndex(assemblyPath)));
        var options = new InspectorOptions(
            assemblyPath,
            workspace.OutputDirectory,
            Tfm: null,
            AllTfms: false,
            CompactJson: false,
            Chunking: ChunkingStrategy.Namespace);

        await app.RunAsync(options);

        var chunkDirectory = Path.Combine(workspace.OutputDirectory, "chunks", "namespaces");
        var files = Directory.GetFiles(chunkDirectory, "*.json", SearchOption.TopDirectoryOnly)
            .Select(Path.GetFileName)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(new[] { "0001-alpha-core.json", "0002-zeta-utils.json" }, files);
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "api-index.json")));
        Assert.True(File.Exists(Path.Combine(workspace.OutputDirectory, "api-summary.md")));
    }

    [Fact]
    public async Task RunAsync_WithTypeChunking_WritesPredictableChunkFileNames()
    {
        using var workspace = new TestWorkspace();
        var assemblyPath = workspace.CreateStandaloneAssembly();
        var app = CreateApp(new StaticAssemblyInspector(CreateChunkFixtureIndex(assemblyPath)));
        var options = new InspectorOptions(
            assemblyPath,
            workspace.OutputDirectory,
            Tfm: null,
            AllTfms: false,
            CompactJson: false,
            Chunking: ChunkingStrategy.Type);

        await app.RunAsync(options);

        var chunkDirectory = Path.Combine(workspace.OutputDirectory, "chunks", "types");
        var files = Directory.GetFiles(chunkDirectory, "*.json", SearchOption.TopDirectoryOnly)
            .Select(Path.GetFileName)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(new[] { "0001-alpha-core-widget.json", "0002-zeta-utils-helper.json" }, files);
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

    private static InspectorApp CreateApp(IAssemblyInspector? inspector = null)
    {
        return new InspectorApp(inspector ?? new CecilAssemblyInspector(), new JsonReportWriter(), new MarkdownReportWriter());
    }

    private static ApiIndex CreateChunkFixtureIndex(string sourcePath)
    {
        return new ApiIndex(
            "ChunkFixture",
            sourcePath,
            DateTimeOffset.UtcNow,
            [
                new NamespaceIndex(
                    "Zeta.Utils",
                    [
                        new TypeIndex(
                            "Helper",
                            "Zeta.Utils.Helper",
                            "class",
                            null,
                            [],
                            [])
                    ]),
                new NamespaceIndex(
                    "Alpha.Core",
                    [
                        new TypeIndex(
                            "Widget",
                            "Alpha.Core.Widget",
                            "class",
                            null,
                            [],
                            [])
                    ])
            ],
            []);
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

                var refDir = Path.Combine(packageLayout, "ref", tfm);
                Directory.CreateDirectory(refDir);
                File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, Path.Combine(refDir, "AssemblyInspector.Ref.dll"), overwrite: true);

                var runtimeDir = Path.Combine(packageLayout, "runtimes", "any", "lib", tfm);
                Directory.CreateDirectory(runtimeDir);
                File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, Path.Combine(runtimeDir, "AssemblyInspector.Runtime.dll"), overwrite: true);
            }

            var nupkgPath = Path.Combine(_rootDirectory, "fixture.nupkg");
            if (File.Exists(nupkgPath))
            {
                File.Delete(nupkgPath);
            }

            ZipFile.CreateFromDirectory(packageLayout, nupkgPath);
            return nupkgPath;
        }

        public string CreateStandaloneAssembly()
        {
            var assemblyDir = Path.Combine(_rootDirectory, "standalone");
            Directory.CreateDirectory(assemblyDir);

            var assemblyPath = Path.Combine(assemblyDir, "AssemblyInspector.Cli.dll");
            File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, assemblyPath, overwrite: true);
            return assemblyPath;
        }

        public string CreateSiblingDependencyDirectory(string assemblyDirectory)
        {
            var dependencyDir = Path.Combine(assemblyDirectory, "deps");
            Directory.CreateDirectory(dependencyDir);
            File.Copy(typeof(CecilAssemblyInspector).Assembly.Location, Path.Combine(dependencyDir, "Dependency.dll"), overwrite: true);
            return dependencyDir;
        }

        public void Dispose()
        {
            if (Directory.Exists(_rootDirectory))
            {
                Directory.Delete(_rootDirectory, recursive: true);
            }
        }
    }

    private sealed class RecordingAssemblyInspector : IAssemblyInspector
    {
        public List<InspectionInvocation> Invocations { get; } = new();

        public ApiIndex Inspect(string assemblyPath, IEnumerable<string>? dependencySearchPaths = null)
        {
            var capturedPaths = dependencySearchPaths?.ToList() ?? new List<string>();
            Invocations.Add(new InspectionInvocation(assemblyPath, capturedPaths));

            var assemblyName = Path.GetFileNameWithoutExtension(assemblyPath);
            return new ApiIndex(
                assemblyName,
                assemblyPath,
                DateTimeOffset.UtcNow,
                Array.Empty<NamespaceIndex>(),
                Array.Empty<ExtensionMethodIndex>());
        }
    }

    private sealed record InspectionInvocation(string AssemblyPath, IReadOnlyList<string> DependencySearchPaths);

    private sealed class StaticAssemblyInspector : IAssemblyInspector
    {
        private readonly ApiIndex _index;

        public StaticAssemblyInspector(ApiIndex index)
        {
            _index = index;
        }

        public ApiIndex Inspect(string assemblyPath, IEnumerable<string>? dependencySearchPaths = null)
        {
            return _index with { SourcePath = assemblyPath };
        }
    }
}
