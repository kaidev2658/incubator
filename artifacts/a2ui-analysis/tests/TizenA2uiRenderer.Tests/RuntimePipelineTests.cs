using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Runtime;
using Xunit;

namespace TizenA2uiRenderer.Tests;

public class RuntimePipelineTests
{
    [Fact]
    public void Pipeline_Emits_Runtime_Trace_From_Mixed_Stream_Input()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(
            controller: new SurfaceController(new ControllerOptions
            {
                FunctionPendingTtl = TimeSpan.FromMinutes(5)
            }),
            runtimeAdapter: adapter);
        var parseErrors = new List<string>();
        var controllerErrors = new List<string>();

        pipeline.ParseError += error => parseErrors.Add(error.Code);
        pipeline.ControllerError += error => controllerErrors.Add(error.Code);

        var mixed = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "mixed_versions_realworld.jsonl"));
        var corrupted = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "corrupted_partial_stream.txt"));
        var stream = $"{mixed}\n{corrupted}\n";

        FeedInChunks(pipeline, stream, [9, 1, 17, 5, 23, 3]);
        pipeline.Flush();

        Assert.Contains("E_PARSE_LINE", parseErrors);
        Assert.Contains("E_PARSE_INCOMPLETE_JSON", parseErrors);
        Assert.DoesNotContain("E_FUNCTION_TIMEOUT", controllerErrors);

        var operations = adapter.Operations;
        Assert.Equal(5, operations.Count);
        Assert.Equal([1, 2, 3, 4, 5], operations.Select(o => o.Sequence).ToArray());
        Assert.Equal(
            ["legacy-main", "legacy-main", "legacy-main", "main", "main"],
            operations.Select(o => o.SurfaceId).ToArray());

        Assert.Equal(RuntimeOperationType.Render, operations[0].Type);
        Assert.Equal("Legacy live", operations[1].DataModel!["status"]!.GetValue<string>());
        Assert.Equal("Unified stream", operations[2].Components!["status"]!["props"]!["text"]!.GetValue<string>());
        Assert.Equal(RuntimeOperationType.Render, operations[4].Type);
        Assert.Equal("ok", operations[4].DataModel!["status"]!.GetValue<string>());
    }

    [Fact]
    public void Pipeline_Handles_Large_Stream_Batches()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);
        var parseErrors = new List<string>();
        var controllerErrors = new List<string>();

        pipeline.ParseError += error => parseErrors.Add(error.Code);
        pipeline.ControllerError += error => controllerErrors.Add(error.Code);

        pipeline.AddMessage(CreateSurface("main"));
        for (var i = 0; i < 5_000; i++)
        {
            pipeline.AddMessage(UpdateData("main", "counter", i));
        }

        Assert.Empty(parseErrors);
        Assert.Empty(controllerErrors);

        Assert.Equal(5_001, adapter.Operations.Count);
        var last = adapter.Operations[^1];
        Assert.Equal(RuntimeOperationType.Render, last.Type);
        Assert.Equal(4_999, last.DataModel!["counter"]!.GetValue<int>());
    }

    [Fact]
    public void Pipeline_Processes_Function_Cancellation_Path()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);
        var errors = new List<A2uiError>();

        pipeline.ControllerError += errors.Add;

        pipeline.AddMessage(CreateSurface("main"));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-cancel",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","name":"confirm","args":{"message":"ok?"}}""").DeserializeAsObject()));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-cancel",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","cancel":true}""").DeserializeAsObject()));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.FunctionResponse,
            "main",
            "fn-cancel",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","value":{"ok":true}}""").DeserializeAsObject()));

        Assert.Contains(errors, e => e.Code == "E_FUNCTION_CANCELLED" && e.FunctionCallId == "fn-cancel");
        Assert.Contains(errors, e => e.Code == "E_FUNCTION_RESPONSE_LATE" && e.FunctionCallId == "fn-cancel");
        Assert.NotEmpty(adapter.Operations);
    }

    private static void FeedInChunks(A2uiRuntimePipeline pipeline, string text, int[] chunkSizes)
    {
        var offset = 0;
        var idx = 0;
        while (offset < text.Length)
        {
            var size = chunkSizes[idx % chunkSizes.Length];
            var length = Math.Min(size, text.Length - offset);
            pipeline.AddChunk(text.Substring(offset, length));
            offset += length;
            idx++;
        }
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

    private static NormalMessage UpdateData(string surfaceId, string key, int value)
        => new(
            "v0.10",
            NormalMessageType.UpdateDataModel,
            surfaceId,
            Payload: new JsonObject
            {
                ["surfaceId"] = surfaceId,
                ["patches"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["path"] = new JsonArray(key),
                        ["value"] = value
                    }
                }
            });
}

internal static class JsonElementExtensions
{
    public static System.Text.Json.Nodes.JsonObject DeserializeAsObject(this JsonElement element)
        => System.Text.Json.Nodes.JsonNode.Parse(element.GetRawText())!.AsObject();
}
