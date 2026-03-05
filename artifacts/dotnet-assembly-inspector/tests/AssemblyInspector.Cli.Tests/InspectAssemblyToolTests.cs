using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Mcp;
using Xunit;

namespace AssemblyInspector.Cli.Tests;

public sealed class InspectAssemblyToolTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidAssembly_ReturnsApiIndexAndMarkdown()
    {
        var tool = new InspectAssemblyTool(new CecilAssemblyInspector(), new MarkdownReportWriter());
        var assemblyPath = typeof(CecilAssemblyInspector).Assembly.Location;

        var response = await tool.ExecuteAsync(new InspectAssemblyRequest(assemblyPath));

        Assert.False(string.IsNullOrWhiteSpace(response.ApiIndex.AssemblyName));
        Assert.NotEmpty(response.ApiIndex.Namespaces);
        Assert.Contains("# API Summary:", response.ApiSummaryMarkdown);
        Assert.Contains("## Extension Methods", response.ApiSummaryMarkdown);
    }
}
