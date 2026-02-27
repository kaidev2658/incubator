using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Transport;

public abstract record GenerationEvent;
public sealed record TextEvent(string Text) : GenerationEvent;
public sealed record MessageEvent(NormalMessage Message) : GenerationEvent;
public sealed record ParseErrorEvent(string Code, string Message, string? RawLine = null) : GenerationEvent;

public sealed class ParserOptions
{
    public int MaxBufferChars { get; init; } = 1_000_000;
}

public sealed class A2uiParser
{
    private readonly ParserOptions _options;
    private readonly StringBuilder _buffer = new();
    private bool _inCodeFence;

    public A2uiParser(ParserOptions? options = null) => _options = options ?? new ParserOptions();

    public IReadOnlyList<GenerationEvent> AddChunk(string chunk)
    {
        _buffer.Append(chunk);
        if (_buffer.Length > _options.MaxBufferChars)
        {
            _buffer.Clear();
            return [new ParseErrorEvent("E_PARSE_OVERFLOW", "Parser buffer overflow")];
        }

        return DrainCompleteLines();
    }

    public IReadOnlyList<GenerationEvent> Flush()
    {
        var events = new List<GenerationEvent>();
        events.AddRange(DrainCompleteLines());

        if (_buffer.Length > 0)
        {
            var rem = _buffer.ToString().Trim();
            _buffer.Clear();
            if (!string.IsNullOrWhiteSpace(rem))
            {
                events.AddRange(ParseLine(rem));
            }
        }

        return events;
    }

    private IReadOnlyList<GenerationEvent> DrainCompleteLines()
    {
        var events = new List<GenerationEvent>();
        while (true)
        {
            var text = _buffer.ToString();
            var idx = text.IndexOf('\n');
            if (idx < 0) break;

            var line = text[..idx].TrimEnd('\r');
            _buffer.Remove(0, idx + 1);

            if (string.IsNullOrWhiteSpace(line))
                continue;

            events.AddRange(ParseLine(line));
        }

        return events;
    }

    private IReadOnlyList<GenerationEvent> ParseLine(string line)
    {
        line = line.Trim();

        if (line.StartsWith("```"))
        {
            _inCodeFence = !_inCodeFence;
            return [];
        }

        if (_inCodeFence && !line.StartsWith("{"))
        {
            return [];
        }

        if (!line.StartsWith("{"))
        {
            return [new TextEvent(line)];
        }

        try
        {
            var node = JsonNode.Parse(line) as JsonObject;
            if (node is null)
            {
                return [new ParseErrorEvent("E_PARSE_LINE", "JSON line is not an object", line)];
            }

            var normalized = A2uiNormalizer.Normalize(node);
            return [new MessageEvent(normalized)];
        }
        catch (Exception ex)
        {
            return [new ParseErrorEvent("E_PARSE_LINE", ex.Message, line)];
        }
    }
}
