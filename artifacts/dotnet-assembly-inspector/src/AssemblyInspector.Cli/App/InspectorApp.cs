using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class InspectorApp
{
    private readonly IAssemblyInspector _inspector;
    private readonly JsonReportWriter _jsonWriter;
    private readonly MarkdownReportWriter _markdownWriter;
    private readonly NugetPackageInspector _nugetPackageInspector;

    public InspectorApp(IAssemblyInspector inspector, JsonReportWriter jsonWriter, MarkdownReportWriter markdownWriter)
    {
        _inspector = inspector;
        _jsonWriter = jsonWriter;
        _markdownWriter = markdownWriter;
        _nugetPackageInspector = new NugetPackageInspector(inspector);
    }

    public async Task RunAsync(InspectorOptions options)
    {
        Directory.CreateDirectory(options.OutputDirectory);

        if (File.Exists(options.InputPath))
        {
            await ProcessFileAsync(options.InputPath, options.OutputDirectory, options);
            return;
        }

        if (Directory.Exists(options.InputPath))
        {
            await ProcessDirectoryAsync(options.InputPath, options.OutputDirectory, options);
            return;
        }

        throw new FileNotFoundException($"Input not found: {options.InputPath}", options.InputPath);
    }

    private async Task ProcessDirectoryAsync(string inputDirectory, string outputDirectory, InspectorOptions options)
    {
        var nupkgs = Directory.GetFiles(inputDirectory, "*.nupkg", SearchOption.TopDirectoryOnly)
            .OrderBy(path => path)
            .ToList();

        if (nupkgs.Count > 0)
        {
            foreach (var nupkg in nupkgs)
            {
                var packageDir = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(nupkg));
                await ProcessNupkgAsync(nupkg, packageDir, options);
            }

            return;
        }

        var dlls = Directory.GetFiles(inputDirectory, "*.dll", SearchOption.AllDirectories)
            .OrderBy(path => path)
            .ToList();

        var dependencySearchPaths = BuildDependencySearchPaths(dlls.Select(Path.GetDirectoryName));

        foreach (var dll in dlls)
        {
            var dllDir = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(dll));
            await ProcessDllAsync(dll, dllDir, options.CompactJson, options.Chunking, dependencySearchPaths);
        }
    }

    private async Task ProcessFileAsync(string inputFile, string outputDirectory, InspectorOptions options)
    {
        var extension = Path.GetExtension(inputFile).ToLowerInvariant();
        if (extension == ".nupkg")
        {
            await ProcessNupkgAsync(inputFile, outputDirectory, options);
            return;
        }

        if (extension == ".dll")
        {
            var assemblyDirectory = Path.GetDirectoryName(Path.GetFullPath(inputFile));
            var dependencySearchPaths = BuildDependencySearchPaths(EnumerateNearbyDependencyDirectories(assemblyDirectory));
            await ProcessDllAsync(inputFile, outputDirectory, options.CompactJson, options.Chunking, dependencySearchPaths);
            return;
        }

        throw new NotSupportedException($"Unsupported input file: {inputFile}");
    }

    private async Task ProcessNupkgAsync(string nupkgPath, string outputDirectory, InspectorOptions options)
    {
        var inspectedAssemblies = await _nugetPackageInspector.InspectAsync(
            nupkgPath,
            options.Tfm,
            options.AllTfms,
            Console.WriteLine);

        foreach (var inspectedAssembly in inspectedAssemblies)
        {
            var dllName = Path.GetFileNameWithoutExtension(inspectedAssembly.AssemblyPath);
            var outDir = Path.Combine(outputDirectory, inspectedAssembly.Tfm, dllName);
            await WriteReportsAsync(inspectedAssembly.ApiIndex, outDir, options.CompactJson, options.Chunking);
        }
    }

    private async Task ProcessDllAsync(
        string dllPath,
        string outputDirectory,
        bool compactJson,
        ChunkingStrategy chunking,
        IReadOnlyList<string>? dependencySearchPaths = null)
    {
        ApiIndex apiIndex = _inspector.Inspect(dllPath, dependencySearchPaths);
        await WriteReportsAsync(apiIndex, outputDirectory, compactJson, chunking);
    }

    private async Task WriteReportsAsync(
        ApiIndex apiIndex,
        string outputDirectory,
        bool compactJson,
        ChunkingStrategy chunking)
    {
        Directory.CreateDirectory(outputDirectory);

        var jsonPath = Path.Combine(outputDirectory, "api-index.json");
        var markdownPath = Path.Combine(outputDirectory, "api-summary.md");

        await _jsonWriter.WriteAsync(apiIndex, jsonPath, compactJson);
        await _markdownWriter.WriteAsync(apiIndex, markdownPath);

        if (chunking != ChunkingStrategy.None)
        {
            var chunkOutputDirectory = Path.Combine(outputDirectory, "chunks");
            await _jsonWriter.WriteChunksAsync(apiIndex, chunkOutputDirectory, chunking, compactJson);
        }

        Console.WriteLine($"Wrote {jsonPath}");
        Console.WriteLine($"Wrote {markdownPath}");
    }

    private static IReadOnlyList<string> BuildDependencySearchPaths(IEnumerable<string?> directories)
    {
        return directories
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => Path.GetFullPath(path!))
            .Where(Directory.Exists)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static IEnumerable<string> EnumerateNearbyDependencyDirectories(string? assemblyDirectory)
    {
        if (string.IsNullOrWhiteSpace(assemblyDirectory) || !Directory.Exists(assemblyDirectory))
        {
            yield break;
        }

        yield return assemblyDirectory;

        foreach (var sibling in Directory.GetDirectories(assemblyDirectory))
        {
            yield return sibling;
        }
    }
}
