using Xunit;
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

    [Fact]
    public void Controller_Queues_Updates_Before_Create_Then_Replays()
    {
        var controller = new SurfaceController();
        SurfaceUpdate? last = null;
        controller.SurfaceUpdated += u => last = u;

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"Queued"}]}""")!.AsObject()));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CreateSurface,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}""")!.AsObject()));

        Assert.NotNull(last);
        Assert.Equal("Queued", last!.DataModel.Get("status")!.GetValue<string>());
    }

    [Fact]
    public void Controller_Drops_Expired_Pending_Updates()
    {
        var now = new DateTimeOffset(2026, 2, 28, 0, 0, 0, TimeSpan.Zero);
        var controller = new SurfaceController(
            new ControllerOptions { PendingTtl = TimeSpan.FromSeconds(1) },
            () => now);
        A2uiError? lastError = null;
        SurfaceUpdate? lastUpdate = null;
        controller.Error += e => lastError = e;
        controller.SurfaceUpdated += u => lastUpdate = u;

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"Stale"}]}""")!.AsObject()));

        now = now.AddSeconds(2);
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CreateSurface,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_PENDING_EXPIRED", lastError!.Code);
        Assert.NotNull(lastUpdate);
        Assert.Null(lastUpdate!.DataModel.Get("status"));
    }

    [Fact]
    public void Controller_Reports_Pending_Overflow()
    {
        var controller = new SurfaceController(new ControllerOptions { MaxPendingPerSurface = 1 });
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"One"}]}""")!.AsObject()));
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"Two"}]}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_PENDING_OVERFLOW", lastError!.Code);
    }

    [Fact]
    public void Controller_DeleteOp_Removes_Model_Field()
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
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["x"],"value":"v"},{"path":["x"],"deleteOp":true}]}""")!.AsObject()));

        Assert.NotNull(last);
        Assert.Null(last!.DataModel.Get("x"));
    }
}
