using Xunit;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Transport;

namespace TizenA2uiRenderer.Tests;

public class ParserTests
{
    [Fact]
    public void Parser_Parses_Jsonl_Lines_Into_NormalMessages()
    {
        var parser = new A2uiParser();
        var input = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "sample_v010.jsonl"));

        var events = parser.AddChunk(input);
        var messages = events.OfType<MessageEvent>().Select(e => e.Message).ToList();

        Assert.Equal(2, messages.Count);
        Assert.Equal(NormalMessageType.CreateSurface, messages[0].Type);
        Assert.Equal(NormalMessageType.UpdateDataModel, messages[1].Type);
        Assert.Equal("main", messages[0].SurfaceId);
        Assert.Equal("v0.10", messages[0].Version);
    }

    [Fact]
    public void Parser_Emits_Error_For_Invalid_Json_Line()
    {
        var parser = new A2uiParser();

        var events = parser.AddChunk("{invalid}\n");

        Assert.Single(events);
        Assert.IsType<ParseErrorEvent>(events[0]);
    }
}
