using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Runtime;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class NuiBindingContractsTests
{
    [Theory]
    [InlineData("Text", NuiComponentContractKind.Text)]
    [InlineData("text", NuiComponentContractKind.Text)]
    [InlineData("Column", NuiComponentContractKind.Container)]
    [InlineData("Row", NuiComponentContractKind.Container)]
    [InlineData("Container", NuiComponentContractKind.Container)]
    [InlineData("Image", NuiComponentContractKind.Unsupported)]
    public void NuiBindingHooks_ResolveContractKind_Maps_Deterministically(
        string componentType,
        NuiComponentContractKind expected)
    {
        var actual = NuiBindingHooks.ResolveContractKind(componentType);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NuiBindingHooks_BuildBindingPlan_Extracts_Text_And_Container_Contracts()
    {
        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["title"] = new JsonObject { ["component"] = "Text" },
                ["avatar"] = new JsonObject { ["component"] = "Image" }
            });

        var plan = NuiBindingHooks.BuildBindingPlan("main", definition);

        Assert.Equal("main", plan.SurfaceId);
        Assert.Equal("root", plan.RootId);
        Assert.Equal(3, plan.Components.Count);
        Assert.Equal(NuiComponentContractKind.Container, plan.Components[0].ContractKind);
        Assert.Equal(NuiComponentContractKind.Text, plan.Components[1].ContractKind);
        Assert.Equal(NuiComponentContractKind.Unsupported, plan.Components[2].ContractKind);
    }
}
