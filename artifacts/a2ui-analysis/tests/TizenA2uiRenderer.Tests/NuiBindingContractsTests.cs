using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Utils;
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

    [Fact]
    public void NuiBindingHooks_Render_Materializes_And_Updates_Text_And_Container_Nodes()
    {
        var hooks = new NuiBindingHooks(hostSupportsNativeBinding: true);
        hooks.Initialize();

        var initialDefinition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["title"] = new JsonObject
                {
                    ["component"] = "Text",
                    ["props"] = new JsonObject { ["text"] = "Boot" }
                }
            });
        var initialModel = new DataModel();

        hooks.Render("main", initialDefinition, initialModel);

        var initialState = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(initialState);
        Assert.Equal("root", initialState!.RootId);
        Assert.Equal(2, initialState.Nodes.Count);
        Assert.Equal("Container", initialState.Nodes["root"].Type);
        Assert.Null(initialState.Nodes["root"].Text);
        Assert.Equal("Text", initialState.Nodes["title"].Type);
        Assert.Equal("Boot", initialState.Nodes["title"].Text);
        Assert.Empty(initialState.SkippedContracts);

        var updatedDefinition = new SurfaceDefinition(
            "main",
            "root-v2",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["title"] = new JsonObject { ["component"] = "Text" }
            });
        var updatedModel = new DataModel();
        updatedModel.Set("title.text", "FromModel");

        hooks.Render("main", updatedDefinition, updatedModel);

        var updatedState = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(updatedState);
        Assert.Equal("root-v2", updatedState!.RootId);
        Assert.Equal(2, updatedState.Nodes.Count);
        Assert.Equal("Container", updatedState.Nodes["root"].Type);
        Assert.Equal("Text", updatedState.Nodes["title"].Type);
        Assert.Equal("FromModel", updatedState.Nodes["title"].Text);
        Assert.Empty(updatedState.SkippedContracts);
    }

    [Fact]
    public void NuiBindingHooks_Render_Records_Unsupported_Contracts_As_Skipped_In_Non_Strict_Mode()
    {
        var hooks = new NuiBindingHooks(hostSupportsNativeBinding: true);
        hooks.Initialize();

        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["avatar"] = new JsonObject { ["component"] = "Image" }
            });
        var model = new DataModel();

        hooks.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Single(state!.Nodes);
        Assert.True(state.Nodes.ContainsKey("root"));
        Assert.Single(state.SkippedContracts);
        Assert.Equal("avatar", state.SkippedContracts[0].ComponentId);
        Assert.Equal("Image", state.SkippedContracts[0].ComponentType);
        Assert.Equal("Unsupported component contract 'Image'.", state.SkippedContracts[0].Reason);
    }

    [Fact]
    public void TizenRuntimeAdapter_Render_Throws_Deterministic_Capability_Error_In_Strict_Mode()
    {
        var hooks = new NuiBindingHooks(
            hostSupportsNativeBinding: true,
            strictUnsupportedContracts: true);
        var adapter = new TizenRuntimeAdapter(hooks);
        _ = adapter.Initialize();

        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["avatar"] = new JsonObject { ["component"] = "Image" }
            });
        var model = new DataModel();

        var ex = Assert.Throws<InvalidOperationException>(() => adapter.Render("main", definition, model));

        Assert.StartsWith($"{ErrorCodes.RuntimeAdapterCapabilityMissing}:", ex.Message);
        Assert.Contains("'Image'", ex.Message);
        Assert.Contains("'avatar'", ex.Message);
        Assert.IsType<UnsupportedNuiContractException>(ex.InnerException);
    }

    [Fact]
    public void TizenRuntimeAdapter_Render_Skips_Unsupported_Contracts_In_Non_Strict_Mode()
    {
        var hooks = new NuiBindingHooks(
            hostSupportsNativeBinding: true,
            strictUnsupportedContracts: false);
        var adapter = new TizenRuntimeAdapter(hooks);
        _ = adapter.Initialize();

        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["avatar"] = new JsonObject { ["component"] = "Image" }
            });
        var model = new DataModel();

        adapter.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Single(state!.Nodes);
        Assert.True(state.Nodes.ContainsKey("root"));
        Assert.Single(state.SkippedContracts);
        Assert.Equal("avatar", state.SkippedContracts[0].ComponentId);
        Assert.Equal("Image", state.SkippedContracts[0].ComponentType);
    }
}
