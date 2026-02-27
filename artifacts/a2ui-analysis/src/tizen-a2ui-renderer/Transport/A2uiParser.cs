using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Transport;

public abstract record GenerationEvent;
public sealed record TextEvent(string Text) : GenerationEvent;
public sealed record MessageEvent(NormalMessage Message) : GenerationEvent;

public sealed class ParserOptions
{
    public int MaxBufferChars { get; init; } = 1_000_000;
}

public sealed class A2uiParser
{
    private readonly ParserOptions _options;
    private readonly StringBuilder _buffer = new();

    public A2uiParser(ParserOptions? options = null) => _options = options ?? new ParserOptions();

    public IReadOnlyList<GenerationEvent> AddChunk(string chunk)
    {
        _buffer.Append(chunk);
        if (_buffer.Length > _options.MaxBufferChars)
        {
            _buffer.Clear();
            return [new TextEvent(string.Empty)];
        }
        return [];
    }

    public IReadOnlyList<GenerationEvent> Flush()
    {
        if (_buffer.Length == 0) return [];
        var text = _buffer.ToString();
        _buffer.Clear();
        return [new TextEvent(text)];
    }
}
