using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Utils;

namespace TizenA2uiRenderer.Transport;

public abstract record GenerationEvent;
public sealed record TextEvent(string Text) : GenerationEvent;
public sealed record MessageEvent(NormalMessage Message) : GenerationEvent;
public sealed record ParseErrorEvent(string Code, string Message, string? RawLine = null) : GenerationEvent;

public sealed class ParserOptions
{
    public int MaxBufferChars { get; init; } = 1_000_000;
    public int MaxJsonCandidateChars { get; init; } = 256_000;
}

public sealed class A2uiParser
{
    private readonly ParserOptions _options;
    private readonly StringBuilder _buffer = new();
    private readonly StringBuilder _jsonCandidate = new();
    private bool _inCodeFence;
    private int _jsonBalance;
    private bool _jsonInString;
    private bool _jsonEscape;
    private bool _jsonStarted;

    public A2uiParser(ParserOptions? options = null) => _options = options ?? new ParserOptions();

    public IReadOnlyList<GenerationEvent> AddChunk(string chunk)
    {
        _buffer.Append(chunk);
        if (_buffer.Length > _options.MaxBufferChars)
        {
            _buffer.Clear();
            return [new ParseErrorEvent(ErrorCodes.ParseOverflow, "Parser buffer overflow")];
        }

        return DrainCompleteLines();
    }

    public IReadOnlyList<GenerationEvent> Flush()
    {
        var events = new List<GenerationEvent>();
        events.AddRange(DrainCompleteLines());

        if (_jsonCandidate.Length > 0)
        {
            events.Add(new ParseErrorEvent(ErrorCodes.ParseIncompleteJson, "Incomplete JSON object at end of stream", _jsonCandidate.ToString()));
            ResetCandidate();
        }

        if (_buffer.Length > 0)
        {
            var rem = _buffer.ToString().Trim();
            _buffer.Clear();
            if (!string.IsNullOrWhiteSpace(rem))
            {
                events.AddRange(ParseLine(rem));
            }
        }

        if (_jsonCandidate.Length > 0)
        {
            events.Add(new ParseErrorEvent(ErrorCodes.ParseIncompleteJson, "Incomplete JSON object at end of stream", _jsonCandidate.ToString()));
            ResetCandidate();
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
            {
                continue;
            }

            events.AddRange(ParseLine(line));
        }

        return events;
    }

    private IReadOnlyList<GenerationEvent> ParseLine(string line)
    {
        line = line.TrimEnd();
        var trimmed = line.Trim();

        if (trimmed.StartsWith("```", StringComparison.Ordinal))
        {
            _inCodeFence = !_inCodeFence;
            return [];
        }

        if (_jsonCandidate.Length > 0)
        {
            return ContinueJsonCandidate(line);
        }

        if (trimmed.StartsWith("{", StringComparison.Ordinal))
        {
            return StartJsonCandidate(line);
        }

        if (_inCodeFence && string.IsNullOrWhiteSpace(trimmed))
        {
            return [];
        }

        if (_inCodeFence)
        {
            return [new TextEvent(trimmed)];
        }

        if (!trimmed.StartsWith("{", StringComparison.Ordinal))
        {
            return [new TextEvent(trimmed)];
        }

        return [];
    }

    private IReadOnlyList<GenerationEvent> StartJsonCandidate(string line)
    {
        _jsonCandidate.Clear();
        _jsonBalance = 0;
        _jsonInString = false;
        _jsonEscape = false;
        _jsonStarted = false;

        _jsonCandidate.Append(line);
        UpdateJsonState(line);

        if (_jsonCandidate.Length > _options.MaxJsonCandidateChars)
        {
            ResetCandidate();
            return [new ParseErrorEvent(ErrorCodes.ParseJsonTooLarge, "JSON payload exceeded max size", line)];
        }

        if (_jsonStarted && _jsonBalance == 0)
        {
            return ParseJsonCandidateAndReset();
        }

        return [];
    }

    private IReadOnlyList<GenerationEvent> ContinueJsonCandidate(string line)
    {
        _jsonCandidate.Append('\n');
        _jsonCandidate.Append(line);
        UpdateJsonState(line);

        if (_jsonCandidate.Length > _options.MaxJsonCandidateChars)
        {
            var raw = _jsonCandidate.ToString();
            ResetCandidate();
            return [new ParseErrorEvent(ErrorCodes.ParseJsonTooLarge, "JSON payload exceeded max size", raw)];
        }

        if (_jsonStarted && _jsonBalance == 0)
        {
            return ParseJsonCandidateAndReset();
        }

        return [];
    }

    private IReadOnlyList<GenerationEvent> ParseJsonCandidateAndReset()
    {
        var raw = _jsonCandidate.ToString().Trim();
        ResetCandidate();

        try
        {
            var node = JsonNode.Parse(raw) as JsonObject;
            if (node is null)
            {
                return [new ParseErrorEvent(ErrorCodes.ParseLine, "JSON payload is not an object", raw)];
            }

            var normalized = A2uiNormalizer.Normalize(node);
            return [new MessageEvent(normalized)];
        }
        catch (Exception ex)
        {
            return [MapException(ex, raw)];
        }
    }

    private static ParseErrorEvent MapException(Exception ex, string raw)
    {
        if (ex is JsonException && ErrorCodes.TryExtractCodePrefix(ex.Message, out var code, out var detail))
        {
            return new ParseErrorEvent(code, detail, raw);
        }

        return new ParseErrorEvent(ErrorCodes.ParseLine, ex.Message, raw);
    }

    private void UpdateJsonState(string text)
    {
        foreach (var ch in text)
        {
            if (_jsonInString)
            {
                if (_jsonEscape)
                {
                    _jsonEscape = false;
                    continue;
                }

                if (ch == '\\')
                {
                    _jsonEscape = true;
                    continue;
                }

                if (ch == '"')
                {
                    _jsonInString = false;
                }
                continue;
            }

            if (ch == '"')
            {
                _jsonInString = true;
                continue;
            }

            if (ch == '{')
            {
                _jsonStarted = true;
                _jsonBalance++;
                continue;
            }

            if (ch == '}')
            {
                _jsonBalance--;
            }
        }
    }

    private void ResetCandidate()
    {
        _jsonCandidate.Clear();
        _jsonBalance = 0;
        _jsonInString = false;
        _jsonEscape = false;
        _jsonStarted = false;
    }
}
