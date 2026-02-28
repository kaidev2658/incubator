using System.Text.Json.Nodes;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class SurfaceControllerTests
{
    [Fact]
    public void Controller_Applies_Create_And_DataModel_Patches()
    {
        var controller = new SurfaceController();
        SurfaceUpdate? last = null;
        controller.SurfaceUpdated += u => last = u;

        controller.HandleMessage(CreateSurface("main"));

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

        controller.HandleMessage(CreateSurface("main"));

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
        controller.HandleMessage(CreateSurface("main"));

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

        controller.HandleMessage(CreateSurface("main"));
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["x"],"value":"v"},{"path":["x"],"deleteOp":true}]}""")!.AsObject()));

        Assert.NotNull(last);
        Assert.Null(last!.DataModel.Get("x"));
    }

    [Fact]
    public void Controller_Rejects_Duplicate_CreateSurface()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        var create = CreateSurface("main");

        controller.HandleMessage(create);
        controller.HandleMessage(create);

        Assert.NotNull(lastError);
        Assert.Equal("E_SURFACE_ALREADY_EXISTS", lastError!.Code);
    }

    [Fact]
    public void Controller_Rejects_Update_After_Delete()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        controller.HandleMessage(new NormalMessage("v0.10", NormalMessageType.DeleteSurface, "main"));
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"after-delete"}]}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_SURFACE_DELETED", lastError!.Code);
    }

    [Fact]
    public void Controller_FunctionResponse_Without_Pending_Call_Reports_Orphan()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.FunctionResponse,
            "main",
            "fn-orphan",
            JsonNode.Parse("""{"surfaceId":"main","value":{"ok":true}}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_RESPONSE_ORPHAN", lastError!.Code);
        Assert.Equal("fn-orphan", lastError.FunctionCallId);
    }

    [Fact]
    public void Controller_CallFunction_Times_Out_When_Not_Answered()
    {
        var now = new DateTimeOffset(2026, 2, 28, 0, 0, 0, TimeSpan.Zero);
        var controller = new SurfaceController(
            new ControllerOptions
            {
                PendingTtl = TimeSpan.FromMinutes(1),
                FunctionPendingTtl = TimeSpan.FromSeconds(1)
            },
            () => now);
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-timeout",
            JsonNode.Parse("""{"surfaceId":"main","name":"confirm","args":{"message":"ok?"}}""")!.AsObject()));

        now = now.AddSeconds(2);
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["status"],"value":"after-timeout"}]}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_TIMEOUT", lastError!.Code);
        Assert.Equal("fn-timeout", lastError.FunctionCallId);
    }

    [Fact]
    public void Controller_FunctionResponse_Surface_Mismatch_Reports_Error()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-mismatch",
            JsonNode.Parse("""{"surfaceId":"main","name":"confirm","args":{"message":"ok?"}}""")!.AsObject()));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.FunctionResponse,
            "secondary",
            "fn-mismatch",
            JsonNode.Parse("""{"surfaceId":"secondary","value":{"ok":false}}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_SURFACE_MISMATCH", lastError!.Code);
        Assert.Equal("fn-mismatch", lastError.FunctionCallId);
    }

    [Fact]
    public void Controller_Rejects_Duplicate_FunctionCallId()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        var call = new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-dup",
            JsonNode.Parse("""{"surfaceId":"main","name":"confirm","args":{"message":"ok?"}}""")!.AsObject());

        controller.HandleMessage(call);
        controller.HandleMessage(call);

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_CALL_DUPLICATE", lastError!.Code);
    }

    [Fact]
    public void Controller_Handles_Concurrent_FunctionResponses_Out_Of_Order()
    {
        var controller = new SurfaceController();
        var errors = new List<A2uiError>();
        controller.Error += errors.Add;

        controller.HandleMessage(CreateSurface("main"));
        controller.HandleMessage(Call("main", "fn-a", "first"));
        controller.HandleMessage(Call("main", "fn-b", "second"));

        controller.HandleMessage(Response("main", "fn-b", true));
        controller.HandleMessage(Response("main", "fn-a", true));

        Assert.Empty(errors);
    }

    [Fact]
    public void Controller_Late_Response_After_Timeout_Reports_Late()
    {
        var now = new DateTimeOffset(2026, 2, 28, 0, 0, 0, TimeSpan.Zero);
        var controller = new SurfaceController(
            new ControllerOptions { FunctionPendingTtl = TimeSpan.FromSeconds(1) },
            () => now);
        var errors = new List<A2uiError>();
        controller.Error += errors.Add;

        controller.HandleMessage(CreateSurface("main"));
        controller.HandleMessage(Call("main", "fn-late", "confirm"));

        now = now.AddSeconds(2);
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["noop"],"value":1}]}""")!.AsObject()));

        controller.HandleMessage(Response("main", "fn-late", true));

        Assert.Contains(errors, e => e.Code == "E_FUNCTION_TIMEOUT" && e.FunctionCallId == "fn-late");
        Assert.Contains(errors, e => e.Code == "E_FUNCTION_RESPONSE_LATE" && e.FunctionCallId == "fn-late");
    }

    [Fact]
    public void Controller_Cancelled_Call_Then_Response_Reports_Late()
    {
        var controller = new SurfaceController();
        var errors = new List<A2uiError>();
        controller.Error += errors.Add;

        controller.HandleMessage(CreateSurface("main"));
        controller.HandleMessage(Call("main", "fn-cancel", "confirm"));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-cancel",
            JsonNode.Parse("""{"surfaceId":"main","cancel":true}""")!.AsObject()));

        controller.HandleMessage(Response("main", "fn-cancel", true));

        Assert.Contains(errors, e => e.Code == "E_FUNCTION_CANCELLED" && e.FunctionCallId == "fn-cancel");
        Assert.Contains(errors, e => e.Code == "E_FUNCTION_RESPONSE_LATE" && e.FunctionCallId == "fn-cancel");
    }

    [Fact]
    public void Controller_Retry_Rearms_TimedOut_Call_And_Accepts_Response()
    {
        var now = new DateTimeOffset(2026, 2, 28, 0, 0, 0, TimeSpan.Zero);
        var controller = new SurfaceController(
            new ControllerOptions { FunctionPendingTtl = TimeSpan.FromSeconds(1) },
            () => now);
        var errors = new List<A2uiError>();
        controller.Error += errors.Add;

        controller.HandleMessage(CreateSurface("main"));
        controller.HandleMessage(Call("main", "fn-retry", "confirm"));

        now = now.AddSeconds(2);
        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            "main",
            Payload: JsonNode.Parse("""{"surfaceId":"main","patches":[{"path":["tick"],"value":1}]}""")!.AsObject()));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-retry",
            JsonNode.Parse("""{"surfaceId":"main","name":"confirm","retry":true}""")!.AsObject()));

        controller.HandleMessage(Response("main", "fn-retry", true));

        Assert.Contains(errors, e => e.Code == "E_FUNCTION_TIMEOUT" && e.FunctionCallId == "fn-retry");
        Assert.DoesNotContain(errors, e => e.Code == "E_FUNCTION_RESPONSE_LATE" && e.FunctionCallId == "fn-retry");
        Assert.DoesNotContain(errors, e => e.Code == "E_FUNCTION_RETRY_INVALID_STATE" && e.FunctionCallId == "fn-retry");
    }

    [Fact]
    public void Controller_Retry_Without_Target_Reports_Error()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-missing",
            JsonNode.Parse("""{"surfaceId":"main","name":"confirm","retry":true}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_RETRY_TARGET_MISSING", lastError!.Code);
    }

    [Fact]
    public void Controller_Cancel_Without_Target_Reports_Error()
    {
        var controller = new SurfaceController();
        A2uiError? lastError = null;
        controller.Error += e => lastError = e;

        controller.HandleMessage(CreateSurface("main"));

        controller.HandleMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-missing-cancel",
            JsonNode.Parse("""{"surfaceId":"main","cancel":true}""")!.AsObject()));

        Assert.NotNull(lastError);
        Assert.Equal("E_FUNCTION_CANCEL_TARGET_MISSING", lastError!.Code);
    }

    private static NormalMessage CreateSurface(string surfaceId)
        => new(
            "v0.10",
            NormalMessageType.CreateSurface,
            surfaceId,
            Payload: new JsonObject
            {
                ["surfaceId"] = surfaceId,
                ["root"] = "root",
                ["components"] = new JsonObject
                {
                    ["root"] = new JsonObject
                    {
                        ["component"] = "Column"
                    }
                }
            });

    private static NormalMessage Call(string surfaceId, string functionCallId, string name)
        => new(
            "v0.10",
            NormalMessageType.CallFunction,
            surfaceId,
            functionCallId,
            Payload: new JsonObject
            {
                ["surfaceId"] = surfaceId,
                ["name"] = name,
                ["args"] = new JsonObject
                {
                    ["message"] = "ok?"
                }
            });

    private static NormalMessage Response(string surfaceId, string functionCallId, bool ok)
        => new(
            "v0.10",
            NormalMessageType.FunctionResponse,
            surfaceId,
            functionCallId,
            Payload: new JsonObject
            {
                ["surfaceId"] = surfaceId,
                ["value"] = new JsonObject
                {
                    ["ok"] = ok
                }
            });
}
