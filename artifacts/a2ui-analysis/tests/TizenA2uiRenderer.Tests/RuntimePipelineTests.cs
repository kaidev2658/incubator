using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Transport;
using TizenA2uiRenderer.Utils;
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
    public void Pipeline_EndToEnd_Records_Trace_Through_Tizen_Binding_Adapter_Boundary()
    {
        var hooks = new RecordingTizenBindingHooks();
        var adapter = new TizenRuntimeAdapter(hooks);
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);

        pipeline.AddMessage(CreateSurface("main"));
        pipeline.AddMessage(UpdateData("main", "counter", 7));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.DeleteSurface,
            "main"));

        Assert.Empty(pipeline.StartupDiagnostics);
        Assert.Equal("tizen-binding-poc", pipeline.RuntimeAdapterStatus.RuntimeMode);
        Assert.True(pipeline.RuntimeAdapterStatus.Capabilities.SupportsRealTizenBinding);

        Assert.Equal(4, hooks.Operations.Count);
        Assert.Equal("init:tizen-native-test-binding", hooks.Operations[0]);
        Assert.Equal("render:main:root:counter=0", hooks.Operations[1]);
        Assert.Equal("render:main:root:counter=7", hooks.Operations[2]);
        Assert.Equal("remove:main", hooks.Operations[3]);
    }

    [Fact]
    public void Pipeline_Reports_Runtime_Readiness_Diagnostics_For_Null_Runtime_Adapter()
    {
        var logger = new TestLogger();
        using var pipeline = new A2uiRuntimePipeline(logger: logger);

        Assert.Contains(
            pipeline.StartupDiagnostics,
            diagnostic => diagnostic.Code == ErrorCodes.RuntimeAdapterNotConfigured);
        Assert.Contains(
            logger.Errors,
            entry => entry.Fields[StructuredLogFields.ErrorCode]?.ToString() == ErrorCodes.RuntimeAdapterNotConfigured);
    }

    [Fact]
    public void Pipeline_Enforces_Production_Readiness_When_Configured()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _ = new A2uiRuntimePipeline(
            options: new RuntimePipelineOptions
            {
                EnforceProductionReadiness = true
            }));

        Assert.Contains(ErrorCodes.RuntimeAdapterNotConfigured, ex.Message);
    }

    [Fact]
    public void Pipeline_Logs_Standardized_Fields_For_Parse_Controller_And_Runtime_Errors()
    {
        var logger = new TestLogger();
        using var pipeline = new A2uiRuntimePipeline(
            runtimeAdapter: new ThrowingRuntimeAdapter(),
            logger: logger);

        pipeline.AddChunk("{invalid}\n");
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.DeleteSurface,
            SurfaceId: "missing"));
        pipeline.AddMessage(CreateSurface("main"));

        Assert.Contains(logger.Errors, entry =>
            entry.Fields.TryGetValue(StructuredLogFields.Source, out var source)
            && source?.ToString() == "transport.parse"
            && entry.Fields[StructuredLogFields.ErrorCode]?.ToString() == ErrorCodes.ParseLine
            && entry.Fields[StructuredLogFields.ErrorComponent]?.ToString() == "parser"
            && entry.Fields[StructuredLogFields.ErrorKind]?.ToString() == "parse");

        Assert.Contains(logger.Errors, entry =>
            entry.Fields.TryGetValue(StructuredLogFields.Source, out var source)
            && source?.ToString() == "controller"
            && entry.Fields[StructuredLogFields.ErrorCode]?.ToString() == ErrorCodes.SurfaceNotFound
            && entry.Fields[StructuredLogFields.ErrorComponent]?.ToString() == "controller"
            && entry.Fields[StructuredLogFields.ErrorKind]?.ToString() == "state");

        Assert.Contains(logger.Errors, entry =>
            entry.Fields.TryGetValue(StructuredLogFields.Source, out var source)
            && source?.ToString() == "runtime.adapter"
            && entry.Fields[StructuredLogFields.ErrorCode]?.ToString() == ErrorCodes.RuntimeOperationFailed
            && entry.Fields[StructuredLogFields.ErrorComponent]?.ToString() == "runtime"
            && entry.Fields[StructuredLogFields.ErrorKind]?.ToString() == "runtime_operation"
            && entry.Fields.ContainsKey(StructuredLogFields.Operation)
            && entry.Fields.ContainsKey(StructuredLogFields.AdapterType)
            && entry.Fields.ContainsKey(StructuredLogFields.BridgeType)
            && entry.Fields.ContainsKey(StructuredLogFields.IntegrationPath));
    }

    [Fact]
    public void Pipeline_Recovers_From_Malformed_Segments_During_Large_Batch_Stream()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);
        var parseErrors = new List<ParseErrorEvent>();

        pipeline.ParseError += parseErrors.Add;

        var stream = new List<string>
        {
            """{"version":"v0.10","createSurface":{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}}"""
        };

        for (var i = 0; i < 2_000; i++)
        {
            stream.Add($"{{\"version\":\"v0.10\",\"updateDataModel\":{{\"surfaceId\":\"main\",\"patches\":[{{\"path\":[\"counter\"],\"value\":{i}}}]}}}}");
            if (i % 250 == 0)
            {
                stream.Add("{invalid}");
                stream.Add("""{"version":"v0.11","createSurface":{"surfaceId":"bad","root":"r","components":{"r":{"component":"Text"}}}}""");
            }
        }

        var payload = string.Join('\n', stream) + "\n";
        FeedInChunks(pipeline, payload, [3, 7, 2, 11, 5, 13]);
        pipeline.Flush();

        Assert.True(parseErrors.Count >= 16);
        Assert.Equal(2_001, adapter.Operations.Count);
        var last = adapter.Operations[^1];
        Assert.Equal(1_999, last.DataModel!["counter"]!.GetValue<int>());
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

    [Fact]
    public void Pipeline_Resilience_Long_Stream_Malformed_Cancel_And_Late_Response()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);
        var parseErrors = new List<ParseErrorEvent>();
        var controllerErrors = new List<A2uiError>();

        pipeline.ParseError += parseErrors.Add;
        pipeline.ControllerError += controllerErrors.Add;

        var lines = new List<string>
        {
            """{"version":"v0.10","createSurface":{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}}"""
        };

        for (var i = 0; i < 1_500; i++)
        {
            lines.Add($"{{\"version\":\"v0.10\",\"updateDataModel\":{{\"surfaceId\":\"main\",\"patches\":[{{\"path\":[\"counter\"],\"value\":{i}}}]}}}}");
            if (i % 200 == 0)
            {
                lines.Add("{invalid}");
            }
        }

        lines.Add("""{"version":"v0.10","functionCallId":"fn-composite","callFunction":{"surfaceId":"main","name":"confirm"}}""");
        lines.Add("""{"version":"v0.10","functionCallId":"fn-composite","callFunction":{"surfaceId":"main","cancel":true}}""");
        lines.Add("""{"version":"v0.10","functionCallId":"fn-composite","functionResponse":{"surfaceId":"main","value":{"ok":true}}}""");
        lines.Add("""{"version":"v0.10","updateDataModel":{"surfaceId":"main","patches":[{"path":["postCancel"],"value":"applied"}]}}""");
        lines.Add("{\"version\":\"v0.10\",\"updateDataModel\":{");

        var payload = string.Join('\n', lines) + "\n";
        FeedInChunks(pipeline, payload, [5, 3, 11, 2, 17, 7, 13]);
        pipeline.Flush();

        Assert.Contains(parseErrors, e => e.Code == ErrorCodes.ParseLine);
        Assert.Contains(parseErrors, e => e.Code == ErrorCodes.ParseIncompleteJson);
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionCancelled && e.FunctionCallId == "fn-composite");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionResponseLate && e.FunctionCallId == "fn-composite");

        Assert.Equal(1_502, adapter.Operations.Count);
        var last = adapter.Operations[^1];
        Assert.Equal("applied", last.DataModel!["postCancel"]!.GetValue<string>());
        Assert.Equal(1_499, last.DataModel!["counter"]!.GetValue<int>());
    }

    [Fact]
    public void Pipeline_Resilience_Long_Stream_Delete_Cancel_Late_Response_And_Recreate()
    {
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(runtimeAdapter: adapter);
        var parseErrors = new List<ParseErrorEvent>();
        var controllerErrors = new List<A2uiError>();

        pipeline.ParseError += parseErrors.Add;
        pipeline.ControllerError += controllerErrors.Add;

        var lines = new List<string>
        {
            """{"version":"v0.10","createSurface":{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}}""",
            """{"version":"v0.10","functionCallId":"fn-delete","callFunction":{"surfaceId":"main","name":"longTask"}}"""
        };

        for (var i = 0; i < 900; i++)
        {
            lines.Add($"{{\"version\":\"v0.10\",\"updateDataModel\":{{\"surfaceId\":\"main\",\"patches\":[{{\"path\":[\"beforeDelete\"],\"value\":{i}}}]}}}}");
            if (i % 180 == 0)
            {
                lines.Add("{invalid}");
            }
        }

        lines.Add("""{"version":"v0.10","deleteSurface":{"surfaceId":"main"}}""");
        lines.Add("""{"version":"v0.10","functionCallId":"fn-delete","functionResponse":{"surfaceId":"main","value":{"ok":true}}}""");
        lines.Add("""{"version":"v0.10","createSurface":{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}}""");

        for (var i = 0; i < 400; i++)
        {
            lines.Add($"{{\"version\":\"v0.10\",\"updateDataModel\":{{\"surfaceId\":\"main\",\"patches\":[{{\"path\":[\"afterRecreate\"],\"value\":{i}}}]}}}}");
        }

        lines.Add("""{"version":"v0.10","functionCallId":"fn-recreate","callFunction":{"surfaceId":"main","name":"confirm"}}""");
        lines.Add("""{"version":"v0.10","functionCallId":"fn-recreate","callFunction":{"surfaceId":"main","cancel":true}}""");
        lines.Add("""{"version":"v0.10","functionCallId":"fn-recreate","functionResponse":{"surfaceId":"main","value":{"ok":false}}}""");

        var payload = string.Join('\n', lines) + "\n";
        FeedInChunks(pipeline, payload, [4, 9, 6, 15, 3, 10, 2]);
        pipeline.Flush();

        Assert.Contains(parseErrors, e => e.Code == ErrorCodes.ParseLine);
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionCancelled && e.FunctionCallId == "fn-delete");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionResponseLate && e.FunctionCallId == "fn-delete");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionCancelled && e.FunctionCallId == "fn-recreate");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionResponseLate && e.FunctionCallId == "fn-recreate");

        var removeCount = adapter.Operations.Count(op => op.Type == RuntimeOperationType.Remove);
        Assert.Equal(1, removeCount);
        var last = adapter.Operations[^1];
        Assert.Equal(399, last.DataModel!["afterRecreate"]!.GetValue<int>());
    }

    [Fact]
    public void Pipeline_Resilience_Long_Stream_Malformed_Timeout_Cancel_And_Late_Response_Mix()
    {
        var now = DateTimeOffset.UtcNow;
        var controller = new SurfaceController(new ControllerOptions
        {
            FunctionPendingTtl = TimeSpan.FromSeconds(10)
        }, () => now);
        var adapter = new InMemoryRuntimeAdapter();
        using var pipeline = new A2uiRuntimePipeline(controller: controller, runtimeAdapter: adapter);
        var parseErrors = new List<ParseErrorEvent>();
        var controllerErrors = new List<A2uiError>();

        pipeline.ParseError += parseErrors.Add;
        pipeline.ControllerError += controllerErrors.Add;

        var lines = new List<string>
        {
            """{"version":"v0.10","createSurface":{"surfaceId":"main","root":"root","components":{"root":{"component":"Column"}}}}"""
        };

        for (var i = 0; i < 1_200; i++)
        {
            lines.Add($"{{\"version\":\"v0.10\",\"updateDataModel\":{{\"surfaceId\":\"main\",\"patches\":[{{\"path\":[\"stream\"],\"value\":{i}}}]}}}}");
            if (i % 240 == 0)
            {
                lines.Add("{oops}");
            }
        }

        var payload = string.Join('\n', lines) + "\n";
        FeedInChunks(pipeline, payload, [8, 1, 12, 4, 16, 5]);

        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-timeout",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","name":"slowOp"}""").DeserializeAsObject()));
        now = now.AddSeconds(30);
        pipeline.AddMessage(UpdateData("main", "postTimeoutTick", 1));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.FunctionResponse,
            "main",
            "fn-timeout",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","value":{"ok":true}}""").DeserializeAsObject()));

        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-cancel-2",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","name":"cancelMe"}""").DeserializeAsObject()));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.CallFunction,
            "main",
            "fn-cancel-2",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","cancel":true}""").DeserializeAsObject()));
        pipeline.AddMessage(new NormalMessage(
            "v0.10",
            NormalMessageType.FunctionResponse,
            "main",
            "fn-cancel-2",
            JsonSerializer.Deserialize<JsonElement>("""{"surfaceId":"main","value":{"ok":false}}""").DeserializeAsObject()));

        Assert.Contains(parseErrors, e => e.Code == ErrorCodes.ParseLine);
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionTimeout && e.FunctionCallId == "fn-timeout");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionResponseLate && e.FunctionCallId == "fn-timeout");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionCancelled && e.FunctionCallId == "fn-cancel-2");
        Assert.Contains(controllerErrors, e => e.Code == ErrorCodes.FunctionResponseLate && e.FunctionCallId == "fn-cancel-2");
        Assert.True(adapter.Operations.Count >= 1_202);
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

internal sealed class ThrowingRuntimeAdapter : ITizenRuntimeAdapter
{
    private static readonly RuntimeAdapterStatus Status = new(
        nameof(ThrowingRuntimeAdapter),
        RuntimeMode: "throwing-test",
        new RuntimeAdapterCapabilities(
            SupportsRender: true,
            SupportsRemove: true,
            SupportsRealTizenBinding: false,
            IsInitialized: true),
        []);

    public RuntimeAdapterStatus Initialize() => Status;

    public RuntimeAdapterStatus GetStatus() => Status;

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
        => throw new InvalidOperationException("render failure");

    public void Remove(string surfaceId)
        => throw new InvalidOperationException("remove failure");
}

internal sealed class RecordingTizenBindingHooks : ITizenBindingHooks
{
    public List<string> Operations { get; } = [];

    public string BindingName => "tizen-native-test-binding";
    public bool SupportsRealBinding => true;
    public bool CanRender => true;
    public bool CanRemove => true;

    public bool IsAvailable() => true;

    public void Initialize()
        => Operations.Add($"init:{BindingName}");

    public void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel)
        => Operations.Add($"render:{surfaceId}:{definition.RootId}:counter={dataModel.Snapshot()["counter"]?.GetValue<int>() ?? 0}");

    public void Remove(string surfaceId)
        => Operations.Add($"remove:{surfaceId}");
}

internal sealed class TestLogger : ILogger
{
    public List<TestLogEntry> Infos { get; } = [];
    public List<TestLogEntry> Errors { get; } = [];

    public void Info(string message, IReadOnlyDictionary<string, object?>? fields = null)
        => Infos.Add(new TestLogEntry(message, fields ?? new Dictionary<string, object?>()));

    public void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null)
        => Errors.Add(new TestLogEntry(message, fields ?? new Dictionary<string, object?>(), ex));
}

internal sealed record TestLogEntry(
    string Message,
    IReadOnlyDictionary<string, object?> Fields,
    Exception? Exception = null);

internal static class JsonElementExtensions
{
    public static System.Text.Json.Nodes.JsonObject DeserializeAsObject(this JsonElement element)
        => System.Text.Json.Nodes.JsonNode.Parse(element.GetRawText())!.AsObject();
}
