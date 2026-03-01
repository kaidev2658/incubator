using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Utils;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class RuntimeModeSelectionTests
{
    [Fact]
    public void RuntimeModeSelector_Prefers_Argument_Over_Environment()
    {
        var selection = RuntimeModeSelector.Select(
            ["--runtime-mode=inmemory"],
            envModeProvider: () => "tizen-nui",
            isTizenHostProvider: () => true);

        Assert.Equal("inmemory", selection.RequestedMode);
        Assert.Equal("in-memory", selection.SelectedMode);
        Assert.False(selection.IsFallback);
    }

    [Fact]
    public void RuntimeModeSelector_Auto_Mode_Is_Deterministic_With_Injected_Host()
    {
        var tizenSelection = RuntimeModeSelector.Select(
            [],
            envModeProvider: () => "auto",
            isTizenHostProvider: () => true);
        var nonTizenSelection = RuntimeModeSelector.Select(
            [],
            envModeProvider: () => "auto",
            isTizenHostProvider: () => false);

        Assert.Equal("tizen-nui", tizenSelection.SelectedMode);
        Assert.Equal("renderer-bridge", nonTizenSelection.SelectedMode);
    }

    [Fact]
    public void RuntimeModeSelector_Unknown_Mode_Falls_Back_Deterministically()
    {
        var selection = RuntimeModeSelector.Select(
            ["--runtime-mode=unknown-mode"],
            envModeProvider: () => null,
            isTizenHostProvider: () => true);

        Assert.Equal("unknown-mode", selection.NormalizedMode);
        Assert.Equal("renderer-bridge", selection.SelectedMode);
        Assert.True(selection.IsFallback);
    }

    [Fact]
    public void RuntimeModeSelector_CreateRuntimeAdapter_Falls_Back_To_RendererBridge_On_Non_Tizen_Host()
    {
        var selection = RuntimeModeSelector.Select(
            ["--runtime-mode=tizen-nui"],
            envModeProvider: () => null,
            isTizenHostProvider: () => false);
        var logger = new TestLogger();

        var adapter = RuntimeModeSelector.CreateRuntimeAdapter(selection, logger, out var selectedMode);

        Assert.Equal("renderer-bridge", selectedMode);
        Assert.IsType<RendererBridgeRuntimeAdapter>(adapter);
        Assert.Contains(logger.Errors, e =>
            e.Fields[StructuredLogFields.RuntimeMode]?.ToString() == "tizen-nui");
    }
}
