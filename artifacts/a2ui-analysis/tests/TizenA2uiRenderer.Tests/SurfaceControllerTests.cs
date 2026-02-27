using System.Text.Json.Nodes;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Tests;

public class SurfaceControllerTests
{
    [Fact]
    public void Controller_Applies_Create_And_DataModel_Patches()
    {
        var controller = new SurfaceController();
        SurfaceUpdate? last = null;
        controller.SurfaceUpdated += u => last = u;

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CreateSurface,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}""")!.AsObject()));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"Running"}]}""")!.AsObject()));

        Assert.NotNull(last);
        Assert.Equal("Running", last!.DataModel.Get("status")!.GetValue<string>());
    }
}
