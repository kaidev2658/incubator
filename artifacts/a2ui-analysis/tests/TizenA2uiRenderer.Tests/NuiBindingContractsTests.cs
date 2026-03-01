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
    [InlineData("Image", NuiComponentContractKind.Image)]
    [InlineData("Button", NuiComponentContractKind.Button)]
    [InlineData("Column", NuiComponentContractKind.Container)]
    [InlineData("Row", NuiComponentContractKind.Container)]
    [InlineData("Container", NuiComponentContractKind.Container)]
    [InlineData("Video", NuiComponentContractKind.Unsupported)]
    public void NuiBindingHooks_ResolveContractKind_Maps_Deterministically(
        string componentType,
        NuiComponentContractKind expected)
    {
        var actual = NuiBindingHooks.ResolveContractKind(componentType);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NuiBindingHooks_BuildBindingPlan_Extracts_Deterministic_Contracts()
    {
        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["title"] = new JsonObject { ["component"] = "Text" },
                ["avatar"] = new JsonObject { ["component"] = "Image" },
                ["cta"] = new JsonObject { ["component"] = "Button" },
                ["legacy"] = new JsonObject { ["component"] = "Video" }
            });

        var plan = NuiBindingHooks.BuildBindingPlan("main", definition);

        Assert.Equal("main", plan.SurfaceId);
        Assert.Equal("root", plan.RootId);
        Assert.Equal(5, plan.Components.Count);
        Assert.Equal(NuiComponentContractKind.Container, plan.Components[0].ContractKind);
        Assert.Equal(NuiComponentContractKind.Text, plan.Components[1].ContractKind);
        Assert.Equal(NuiComponentContractKind.Image, plan.Components[2].ContractKind);
        Assert.Equal(NuiComponentContractKind.Button, plan.Components[3].ContractKind);
        Assert.Equal(NuiComponentContractKind.Unsupported, plan.Components[4].ContractKind);
    }

    [Fact]
    public void NuiBindingHooks_Render_Materializes_And_Updates_Text_Image_Button_And_Container_Nodes()
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
                },
                ["avatar"] = new JsonObject
                {
                    ["component"] = "Image",
                    ["props"] = new JsonObject { ["src"] = "boot.png" }
                },
                ["cta"] = new JsonObject
                {
                    ["component"] = "Button",
                    ["props"] = new JsonObject { ["label"] = "Start" }
                }
            });
        var initialModel = new DataModel();

        hooks.Render("main", initialDefinition, initialModel);

        var initialState = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(initialState);
        Assert.Equal("root", initialState!.RootId);
        Assert.Equal(4, initialState.Nodes.Count);
        Assert.Equal("Container", initialState.Nodes["root"].Type);
        Assert.Null(initialState.Nodes["root"].Text);
        Assert.Null(initialState.Nodes["root"].Source);
        Assert.Equal("Text", initialState.Nodes["title"].Type);
        Assert.Equal("Boot", initialState.Nodes["title"].Text);
        Assert.Null(initialState.Nodes["title"].Source);
        Assert.Equal("Image", initialState.Nodes["avatar"].Type);
        Assert.Null(initialState.Nodes["avatar"].Text);
        Assert.Equal("boot.png", initialState.Nodes["avatar"].Source);
        Assert.Equal("Button", initialState.Nodes["cta"].Type);
        Assert.Equal("Start", initialState.Nodes["cta"].Text);
        Assert.Null(initialState.Nodes["cta"].Source);
        Assert.Empty(initialState.SkippedContracts);

        var updatedDefinition = new SurfaceDefinition(
            "main",
            "root-v2",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["title"] = new JsonObject { ["component"] = "Text" },
                ["avatar"] = new JsonObject { ["component"] = "Image" },
                ["cta"] = new JsonObject { ["component"] = "Button" }
            });
        var updatedModel = new DataModel();
        updatedModel.Set("title.text", "FromModel");
        updatedModel.Set("avatar.src", "from-model.png");
        updatedModel.Set("cta.label", "ModelLabel");

        hooks.Render("main", updatedDefinition, updatedModel);

        var updatedState = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(updatedState);
        Assert.Equal("root-v2", updatedState!.RootId);
        Assert.Equal(4, updatedState.Nodes.Count);
        Assert.Equal("Container", updatedState.Nodes["root"].Type);
        Assert.Equal("Text", updatedState.Nodes["title"].Type);
        Assert.Equal("FromModel", updatedState.Nodes["title"].Text);
        Assert.Equal("Image", updatedState.Nodes["avatar"].Type);
        Assert.Equal("from-model.png", updatedState.Nodes["avatar"].Source);
        Assert.Equal("Button", updatedState.Nodes["cta"].Type);
        Assert.Equal("ModelLabel", updatedState.Nodes["cta"].Text);
        Assert.Empty(updatedState.SkippedContracts);
    }

    [Fact]
    public void NuiBindingHooks_Render_Uses_Component_Props_First_For_Image_And_Button()
    {
        var hooks = new NuiBindingHooks(hostSupportsNativeBinding: true);
        hooks.Initialize();

        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["avatar"] = new JsonObject
                {
                    ["component"] = "Image",
                    ["props"] = new JsonObject { ["src"] = "props.png" }
                },
                ["cta"] = new JsonObject
                {
                    ["component"] = "Button",
                    ["props"] = new JsonObject { ["text"] = "PropsText", ["label"] = "PropsLabel" }
                }
            });
        var model = new DataModel();
        model.Set("avatar.src", "model.png");
        model.Set("cta.text", "ModelText");
        model.Set("cta.label", "ModelLabel");

        hooks.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Equal("props.png", state!.Nodes["avatar"].Source);
        Assert.Equal("PropsText", state.Nodes["cta"].Text);
    }

    [Fact]
    public void NuiBindingHooks_Render_Falls_Back_To_DataModel_For_Image_And_Button()
    {
        var hooks = new NuiBindingHooks(hostSupportsNativeBinding: true);
        hooks.Initialize();

        var definition = new SurfaceDefinition(
            "main",
            "root",
            new JsonObject
            {
                ["root"] = new JsonObject { ["component"] = "Column" },
                ["avatar"] = new JsonObject { ["component"] = "Image" },
                ["cta"] = new JsonObject { ["component"] = "Button" }
            });
        var model = new DataModel();
        model.Set("avatar.src", "model.png");
        model.Set("cta.label", "ModelLabel");

        hooks.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Equal("model.png", state!.Nodes["avatar"].Source);
        Assert.Equal("ModelLabel", state.Nodes["cta"].Text);
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
                ["avatar"] = new JsonObject { ["component"] = "Video" }
            });
        var model = new DataModel();

        hooks.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Single(state!.Nodes);
        Assert.True(state.Nodes.ContainsKey("root"));
        Assert.Single(state.SkippedContracts);
        Assert.Equal("avatar", state.SkippedContracts[0].ComponentId);
        Assert.Equal("Video", state.SkippedContracts[0].ComponentType);
        Assert.Equal("Unsupported component contract 'Video'.", state.SkippedContracts[0].Reason);
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
                ["avatar"] = new JsonObject { ["component"] = "Video" }
            });
        var model = new DataModel();

        var ex = Assert.Throws<InvalidOperationException>(() => adapter.Render("main", definition, model));

        Assert.StartsWith($"{ErrorCodes.RuntimeAdapterCapabilityMissing}:", ex.Message);
        Assert.Contains("'Video'", ex.Message);
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
                ["avatar"] = new JsonObject { ["component"] = "Video" }
            });
        var model = new DataModel();

        adapter.Render("main", definition, model);

        var state = hooks.GetSurfaceRenderState("main");
        Assert.NotNull(state);
        Assert.Single(state!.Nodes);
        Assert.True(state.Nodes.ContainsKey("root"));
        Assert.Single(state.SkippedContracts);
        Assert.Equal("avatar", state.SkippedContracts[0].ComponentId);
        Assert.Equal("Video", state.SkippedContracts[0].ComponentType);
    }
}
