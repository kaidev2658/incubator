using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public interface IAssemblyInspector
{
    ApiIndex Inspect(string assemblyPath, IEnumerable<string>? dependencySearchPaths = null);
}
