using System.IO.Compression;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed record NupkgInspectionResult(
    string Tfm,
    string AssemblyPath,
    ApiIndex ApiIndex);

public sealed class NugetPackageInspector
{
    private readonly IAssemblyInspector _inspector;

    public NugetPackageInspector(IAssemblyInspector inspector)
    {
        _inspector = inspector;
    }

    public Task<IReadOnlyList<NupkgInspectionResult>> InspectAsync(
        string nupkgPath,
        string? tfm,
        bool allTfms,
        Action<string>? log = null)
    {
        if (string.IsNullOrWhiteSpace(nupkgPath))
        {
            throw new ArgumentException("nupkg path is required.", nameof(nupkgPath));
        }

        var fullPath = Path.GetFullPath(nupkgPath);
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"NuGet package not found: {fullPath}", fullPath);
        }

        if (!string.Equals(Path.GetExtension(fullPath), ".nupkg", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException($"Only .nupkg is supported: {fullPath}");
        }

        var tempDir = Path.Combine(Path.GetTempPath(), "dotnet-assembly-inspector", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            ZipFile.ExtractToDirectory(fullPath, tempDir);

            var dlls = Directory.Exists(Path.Combine(tempDir, "lib"))
                ? Directory.GetFiles(Path.Combine(tempDir, "lib"), "*.dll", SearchOption.AllDirectories).OrderBy(path => path).ToList()
                : new List<string>();

            if (dlls.Count == 0)
            {
                log?.Invoke($"No DLL found in nupkg lib/: {fullPath}");
                return Task.FromResult<IReadOnlyList<NupkgInspectionResult>>(Array.Empty<NupkgInspectionResult>());
            }

            var groupedByTfm = dlls
                .GroupBy(ResolveTfmFromPath)
                .OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
                .ToList();

            IEnumerable<IGrouping<string, string>> selectedGroups;
            if (!string.IsNullOrWhiteSpace(tfm))
            {
                selectedGroups = groupedByTfm.Where(group => string.Equals(group.Key, tfm, StringComparison.OrdinalIgnoreCase));
                if (!selectedGroups.Any())
                {
                    log?.Invoke($"No DLL found for requested TFM '{tfm}' in {fullPath}");
                    return Task.FromResult<IReadOnlyList<NupkgInspectionResult>>(Array.Empty<NupkgInspectionResult>());
                }
            }
            else if (allTfms)
            {
                selectedGroups = groupedByTfm;
            }
            else
            {
                selectedGroups = groupedByTfm.Take(1);
                log?.Invoke($"No TFM option provided. Using first discovered TFM: {selectedGroups.First().Key}");
            }

            var results = new List<NupkgInspectionResult>();
            foreach (var tfmGroup in selectedGroups)
            {
                var dependencySearchPaths = BuildDependencySearchPaths(
                    tfmGroup.Select(Path.GetDirectoryName)
                        .Concat(GetTfmSpecificDependencyDirectories(tempDir, tfmGroup.Key)));

                foreach (var dll in tfmGroup)
                {
                    var apiIndex = _inspector.Inspect(dll, dependencySearchPaths);
                    results.Add(new NupkgInspectionResult(tfmGroup.Key, dll, apiIndex));
                }
            }

            return Task.FromResult<IReadOnlyList<NupkgInspectionResult>>(results);
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

    private static IEnumerable<string> GetTfmSpecificDependencyDirectories(string packageRoot, string tfm)
    {
        yield return Path.Combine(packageRoot, "lib", tfm);
        yield return Path.Combine(packageRoot, "ref", tfm);

        var runtimesRoot = Path.Combine(packageRoot, "runtimes");
        if (!Directory.Exists(runtimesRoot))
        {
            yield break;
        }

        foreach (var runtimeDir in Directory.GetDirectories(runtimesRoot))
        {
            yield return Path.Combine(runtimeDir, "lib", tfm);
        }
    }
}
