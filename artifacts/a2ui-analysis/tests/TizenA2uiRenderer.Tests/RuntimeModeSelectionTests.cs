using TizenA2uiRenderer.Runtime;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class RuntimeModeSelectionTests
{
    [Fact]
    public void RuntimeModeSelector_Prefers_Argument_Over_Environment()
    {
        var selection = RuntimeModeSelector.Select(
            ["--runtime-mode=inmemory"],
            envModeProvider: () => "tizen-poc",
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

        Assert.Equal("tizen-poc", tizenSelection.SelectedMode);
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
}
