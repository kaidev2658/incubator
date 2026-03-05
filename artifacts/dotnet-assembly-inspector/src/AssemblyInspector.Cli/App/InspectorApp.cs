using System.IO.Compression;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class InspectorApp
{
    private readonly IAssemblyInspector _inspector;
    private readonly JsonReportWriter _jsonWriter;
    private readonly MarkdownReportWriter _markdownWriter;

    public InspectorApp(IAssemblyInspector inspector, JsonReportWriter jsonWriter, MarkdownReportWriter markdownWriter)
    {
        _inspector = inspector;
        _jsonWriter = jsonWriter;
        _markdownWriter = markdownWriter;
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

        foreach (var dll in dlls)
        {
            var dllDir = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(dll));
            await ProcessDllAsync(dll, dllDir);
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
            await ProcessDllAsync(inputFile, outputDirectory);
            return;
        }

        throw new NotSupportedException($"Unsupported input file: {inputFile}");
    }

    private async Task ProcessNupkgAsync(string nupkgPath, string outputDirectory, InspectorOptions options)
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "dotnet-assembly-inspector", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            ZipFile.ExtractToDirectory(nupkgPath, tempDir);

            var dlls = Directory.Exists(Path.Combine(tempDir, "lib"))
                ? Directory.GetFiles(Path.Combine(tempDir, "lib"), "*.dll", SearchOption.AllDirectories).OrderBy(path => path).ToList()
                : new List<string>();

            if (dlls.Count == 0)
            {
                Console.WriteLine($"No DLL found in nupkg lib/: {nupkgPath}");
                return;
            }

            var groupedByTfm = dlls
                .GroupBy(ResolveTfmFromPath)
                .OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
                .ToList();

            IEnumerable<IGrouping<string, string>> selectedGroups;
            if (!string.IsNullOrWhiteSpace(options.Tfm))
            {
                selectedGroups = groupedByTfm.Where(group => string.Equals(group.Key, options.Tfm, StringComparison.OrdinalIgnoreCase));
                if (!selectedGroups.Any())
                {
                    Console.WriteLine($"No DLL found for requested TFM '{options.Tfm}' in {nupkgPath}");
                    return;
                }
            }
            else if (options.AllTfms)
            {
                selectedGroups = groupedByTfm;
            }
            else
            {
                selectedGroups = groupedByTfm.Take(1);
                Console.WriteLine($"No TFM option provided. Using first discovered TFM: {selectedGroups.First().Key}");
            }

            foreach (var tfmGroup in selectedGroups)
            {
                foreach (var dll in tfmGroup)
                {
                    var dllName = Path.GetFileNameWithoutExtension(dll);
                    var outDir = Path.Combine(outputDirectory, tfmGroup.Key, dllName);
                    await ProcessDllAsync(dll, outDir);
                }
            }
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    private static string ResolveTfmFromPath(string dllPath)
    {
        var segments = dllPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var libIndex = Array.FindIndex(segments, s => string.Equals(s, "lib", StringComparison.OrdinalIgnoreCase));
        if (libIndex >= 0 && libIndex + 1 < segments.Length)
        {
            return segments[libIndex + 1];
        }

        return "unknown-tfm";
    }

    private async Task ProcessDllAsync(string dllPath, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);

        ApiIndex apiIndex = _inspector.Inspect(dllPath);
        var jsonPath = Path.Combine(outputDirectory, "api-index.json");
        var markdownPath = Path.Combine(outputDirectory, "api-summary.md");

        await _jsonWriter.WriteAsync(apiIndex, jsonPath);
        await _markdownWriter.WriteAsync(apiIndex, markdownPath);

        Console.WriteLine($"Wrote {jsonPath}");
        Console.WriteLine($"Wrote {markdownPath}");
    }
}
