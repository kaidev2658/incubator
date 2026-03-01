using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Utils;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class RuntimeReadinessTests
{
    [Fact]
    public void Pipeline_Production_Readiness_Requires_Real_Tizen_Binding()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _ = new A2uiRuntimePipeline(
            runtimeAdapter: new InMemoryRuntimeAdapter(),
            options: new RuntimePipelineOptions
            {
                EnforceProductionReadiness = true
            }));

        Assert.Contains(ErrorCodes.RuntimeAdapterIntegrationInvalid, ex.Message);
    }

    [Fact]
    public void Pipeline_Production_Readiness_Allows_Placeholder_Real_Binding_On_Tizen_Host()
    {
        using var pipeline = new A2uiRuntimePipeline(
            runtimeAdapter: new TizenRuntimeAdapter(new PlaceholderRealTizenBindingHooks(
                hostSupportsNativeBinding: true)),
            options: new RuntimePipelineOptions
            {
                EnforceProductionReadiness = true
            });

        Assert.Empty(pipeline.StartupDiagnostics);
        Assert.True(pipeline.RuntimeAdapterStatus.Capabilities.IsInitialized);
        Assert.True(pipeline.RuntimeAdapterStatus.Capabilities.SupportsRealTizenBinding);
    }

    [Fact]
    public void Pipeline_Non_Production_Mode_Does_Not_Require_Real_Binding()
    {
        using var pipeline = new A2uiRuntimePipeline(
            runtimeAdapter: new InMemoryRuntimeAdapter(),
            options: new RuntimePipelineOptions
            {
                EnforceProductionReadiness = false
            });

        Assert.DoesNotContain(
            pipeline.StartupDiagnostics,
            diagnostic => diagnostic.Code == ErrorCodes.RuntimeAdapterIntegrationInvalid);
    }
}
