using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Transport;
using Xunit;

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

    [Fact]
    public void Parser_Parses_Markdown_Fenced_Multiline_Json_And_Text()
    {
        var parser = new A2uiParser();
        var input = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "realistic_stream_v010.txt"));

        var events = parser.AddChunk(input);
        var messages = events.OfType<MessageEvent>().Select(e => e.Message).ToList();
        var texts = events.OfType<TextEvent>().Select(e => e.Text).ToList();

        Assert.Equal(3, messages.Count);
        Assert.Equal(NormalMessageType.CreateSurface, messages[0].Type);
        Assert.Equal(NormalMessageType.UpdateDataModel, messages[1].Type);
        Assert.Equal(NormalMessageType.CallFunction, messages[2].Type);
        Assert.Contains("status: generating", texts);
    }

    [Fact]
    public void Parser_Flush_Reports_Incomplete_Json()
    {
        var parser = new A2uiParser();

        parser.AddChunk("{\"version\":\"v0.10\",\"createSurface\":{");
        var events = parser.Flush();

        var error = Assert.IsType<ParseErrorEvent>(Assert.Single(events));
        Assert.Equal("E_PARSE_INCOMPLETE_JSON", error.Code);
    }

    [Fact]
    public void Parser_Handles_Chunked_Multiline_Json()
    {
        var parser = new A2uiParser();

        var first = parser.AddChunk("{\"version\":\"v0.10\",\n\"createSurface\":");
        var second = parser.AddChunk("{\"surfaceId\":\"main\",\"root\":\"r\",\"components\":{\"r\":{\"component\":\"Text\"}}}}\n");

        Assert.Empty(first);
        var message = Assert.IsType<MessageEvent>(Assert.Single(second)).Message;
        Assert.Equal(NormalMessageType.CreateSurface, message.Type);
        Assert.Equal("main", message.SurfaceId);
    }

    [Fact]
    public void Parser_Emits_Error_When_Json_Candidate_Too_Large()
    {
        var parser = new A2uiParser(new ParserOptions
        {
            MaxBufferChars = 1_000_000,
            MaxJsonCandidateChars = 40
        });

        var events = parser.AddChunk("{\"version\":\"v0.10\",\"createSurface\":{\"surfaceId\":\"main\"}}\n");

        var error = Assert.IsType<ParseErrorEvent>(Assert.Single(events));
        Assert.Equal("E_PARSE_JSON_TOO_LARGE", error.Code);
    }

    [Fact]
    public void Parser_Parses_Mixed_Versions_And_Function_Roundtrip()
    {
        var parser = new A2uiParser();
        var input = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "mixed_versions_realworld.jsonl"));

        var events = parser.AddChunk(input);
        var messages = events.OfType<MessageEvent>().Select(e => e.Message).ToList();
        var text = Assert.IsType<TextEvent>(Assert.Single(events.OfType<TextEvent>()));

        Assert.Equal("status: preparing dashboard", text.Text);
        Assert.Equal(5, messages.Count);
        Assert.Equal(["v0.9", "v0.9", "v0.10", "v0.10", "v0.10"], messages.Select(m => m.Version).ToArray());
        Assert.Equal(NormalMessageType.CallFunction, messages[3].Type);
        Assert.Equal(NormalMessageType.FunctionResponse, messages[4].Type);
        Assert.Equal("fn-mixed-1", messages[3].FunctionCallId);
    }

    [Fact]
    public void Parser_Continues_After_Corrupted_And_Partial_Stream()
    {
        var parser = new A2uiParser();
        var input = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", "corrupted_partial_stream.txt"));

        var events = parser.AddChunk(input).Concat(parser.Flush()).ToList();
        var messages = events.OfType<MessageEvent>().Select(e => e.Message).ToList();
        var errors = events.OfType<ParseErrorEvent>().ToList();

        Assert.Equal(2, messages.Count);
        Assert.Equal(NormalMessageType.CreateSurface, messages[0].Type);
        Assert.Equal(NormalMessageType.UpdateDataModel, messages[1].Type);
        Assert.Contains(errors, e => e.Code == "E_PARSE_LINE");
        Assert.Contains(errors, e => e.Code == "E_PARSE_INCOMPLETE_JSON");
    }

    [Fact]
    public void Parser_Maps_Normalizer_Error_Code_Without_Collapsing_To_Parse_Line()
    {
        var parser = new A2uiParser();

        var events = parser.AddChunk("{\"version\":\"v0.10\",\"createSurface\":{\"root\":\"r\",\"components\":{\"r\":{\"component\":\"Text\"}}}}\n");

        var error = Assert.IsType<ParseErrorEvent>(Assert.Single(events));
        Assert.Equal("E_SURFACE_ID_REQUIRED", error.Code);
    }
}
