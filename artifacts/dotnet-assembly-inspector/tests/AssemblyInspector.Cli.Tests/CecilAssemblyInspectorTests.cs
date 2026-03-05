using System.Linq;
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
}
