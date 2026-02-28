using System.Text;
using System.Text.Json;
using Xunit;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Transport;

namespace TizenA2uiRenderer.Tests;

public class IntegrationPipelineTests
{
    [Fact]
    public void Integration_Processes_Large_Chunked_Jsonl_Stream()
    {
        var transport = new TransportAdapter();
        var controller = new SurfaceController();
        var renderer = new RecordingRendererBridge();
        var parseErrors = new List<ParseErrorEvent>();
        var controllerErrors = new List<A2uiError>();

        transport.OnMessage(controller.HandleMessage);
        transport.OnError(parseErrors.Add);
        controller.Error += controllerErrors.Add;
        controller.SurfaceUpdated += update => renderer.Render(update.SurfaceId, update.Definition, update.DataModel);
        controller.SurfaceDeleted += renderer.Remove;

        var stream = new StringBuilder();
        stream.AppendLine(ToJson(new
        {
            version = "v0.10",
            createSurface = new
            {
                surfaceId = "main",
                root = "root",
                components = new
                {
                    root = new { component = "Column", children = new[] { "status", "counter" } },
                    status = new { component = "Text", text = "idle" },
                    counter = new { component = "Text", text = "0" }
                }
            }
        }));

        var updateComponentCount = 0;
        for (var i = 0; i < 200; i++)
        {
            stream.AppendLine(ToJson(new
            {
                version = "v0.10",
                updateDataModel = new
                {
                    surfaceId = "main",
                    patches = new object[]
                    {
                        new { path = new[] { "status" }, value = $"running-{i}" },
                        new { path = new[] { "metrics", "lastIndex" }, value = i }
                    }
                }
            }));

            if (i % 25 == 0)
            {
                updateComponentCount++;
                stream.AppendLine(ToJson(new
                {
                    version = "v0.10",
                    updateComponents = new
                    {
                        surfaceId = "main",
                        components = new
                        {
                            counter = new { component = "Text", text = $"render-{i}" }
                        }
                    }
                }));
            }
        }

        FeedInChunks(transport, stream.ToString(), [1, 2, 3, 5, 8, 13, 21, 34]);
        transport.Flush();

        Assert.Empty(parseErrors);
        Assert.Empty(controllerErrors);

        var expectedRenderCount = 1 + 200 + updateComponentCount;
        Assert.Equal(expectedRenderCount, renderer.Operations.Count);

        var last = Assert.IsType<RenderOperation>(renderer.Operations[^1]);
        Assert.Equal(RenderOperationType.Render, last.Type);
        Assert.Equal("main", last.SurfaceId);
        Assert.Equal(199, last.DataModel!["metrics"]!["lastIndex"]!.GetValue<int>());
        Assert.Equal("running-199", last.DataModel!["status"]!.GetValue<string>());
    }

    [Fact]
    public void Integration_Recovers_After_Malformed_Line_And_Continues()
    {
        var transport = new TransportAdapter();
        var controller = new SurfaceController();
        var renderer = new RecordingRendererBridge();
        var parseErrors = new List<ParseErrorEvent>();

        transport.OnMessage(controller.HandleMessage);
        transport.OnError(parseErrors.Add);
        controller.SurfaceUpdated += update => renderer.Render(update.SurfaceId, update.Definition, update.DataModel);
        controller.SurfaceDeleted += renderer.Remove;

        var stream = string.Join('\n', new[]
        {
            ToJson(new
            {
                version = "v0.10",
                createSurface = new
                {
                    surfaceId = "main",
                    root = "root",
                    components = new { root = new { component = "Column" } }
                }
            }),
            "{invalid}",
            ToJson(new
            {
                version = "v0.10",
                updateDataModel = new
                {
                    surfaceId = "main",
                    patches = new object[] { new { path = new[] { "status" }, value = "ok" } }
                }
            }),
            ToJson(new
            {
                version = "v0.10",
                deleteSurface = new { surfaceId = "main" }
            }),
            string.Empty
        });

        FeedInChunks(transport, stream, [7, 4, 11, 2, 19, 3]);
        transport.Flush();

        var parseError = Assert.Single(parseErrors);
        Assert.Equal("E_PARSE_LINE", parseError.Code);

        Assert.Equal(3, renderer.Operations.Count);
        Assert.Equal(RenderOperationType.Render, renderer.Operations[0].Type);
        Assert.Equal(RenderOperationType.Render, renderer.Operations[1].Type);
        Assert.Equal("ok", renderer.Operations[1].DataModel!["status"]!.GetValue<string>());
        Assert.Equal(RenderOperationType.Remove, renderer.Operations[2].Type);
    }

    [Fact]
    public void Integration_Processes_Mixed_Versions_With_Corrupted_Tail()
    {
        var transport = new TransportAdapter();
        var controller = new SurfaceController(new ControllerOptions
        {
            FunctionPendingTtl = TimeSpan.FromMinutes(5)
        });
        var renderer = new RecordingRendererBridge();
        var parseErrors = new List<ParseErrorEvent>();
        var controllerErrors = new List<A2uiError>();

        transport.OnMessage(controller.HandleMessage);
        transport.OnError(parseErrors.Add);
        controller.Error += controllerErrors.Add;
        controller.SurfaceUpdated += update => renderer.Render(update.SurfaceId, update.Definition, update.DataModel);

        var mixed = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "mixed_versions_realworld.jsonl"));
        var corrupted = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "corrupted_partial_stream.txt"));
        var stream = $"{mixed}\n{corrupted}\n";

        FeedInChunks(transport, stream, [9, 1, 17, 5, 23, 3]);
        transport.Flush();

        Assert.Contains(parseErrors, e => e.Code == "E_PARSE_LINE");
        Assert.Contains(parseErrors, e => e.Code == "E_PARSE_INCOMPLETE_JSON");
        Assert.DoesNotContain(controllerErrors, e => e.Code == "E_FUNCTION_TIMEOUT");

        Assert.NotEmpty(renderer.Operations);
        var lastRender = Assert.IsType<RenderOperation>(renderer.Operations[^1]);
        Assert.Equal(RenderOperationType.Render, lastRender.Type);
        Assert.Equal("ok", lastRender.DataModel!["status"]!.GetValue<string>());
    }

    private static void FeedInChunks(ITransportAdapter transport, string text, int[] chunkSizes)
    {
        var offset = 0;
        var idx = 0;
        while (offset < text.Length)
        {
            var size = chunkSizes[idx % chunkSizes.Length];
            var length = Math.Min(size, text.Length - offset);
            transport.AddChunk(text.Substring(offset, length));
            offset += length;
            idx++;
        }
    }

    private static string ToJson(object value) => JsonSerializer.Serialize(value);
}
