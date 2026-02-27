using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Transport;

public interface ITransportAdapter
{
    void AddChunk(string chunk);
    void AddMessage(NormalMessage message);
    IReadOnlyList<GenerationEvent> Flush();
    IDisposable OnMessage(Action<NormalMessage> callback);
    IDisposable OnText(Action<string> callback);
    IDisposable OnError(Action<ParseErrorEvent> callback);
}

public sealed class TransportAdapter : ITransportAdapter
{
    private readonly A2uiParser _parser;
    private readonly List<Action<NormalMessage>> _messageHandlers = [];
    private readonly List<Action<string>> _textHandlers = [];
    private readonly List<Action<ParseErrorEvent>> _errorHandlers = [];

    public TransportAdapter(A2uiParser? parser = null) => _parser = parser ?? new A2uiParser();

    public void AddChunk(string chunk)
    {
        foreach (var evt in _parser.AddChunk(chunk))
            Dispatch(evt);
    }

    public void AddMessage(NormalMessage message)
    {
        foreach (var handler in _messageHandlers) handler(message);
    }

    public IReadOnlyList<GenerationEvent> Flush()
    {
        var events = _parser.Flush();
        foreach (var evt in events)
            Dispatch(evt);
        return events;
    }

    public IDisposable OnMessage(Action<NormalMessage> callback)
    {
        _messageHandlers.Add(callback);
        return new Subscription<Action<NormalMessage>>(_messageHandlers, callback);
    }

    public IDisposable OnText(Action<string> callback)
    {
        _textHandlers.Add(callback);
        return new Subscription<Action<string>>(_textHandlers, callback);
    }

    public IDisposable OnError(Action<ParseErrorEvent> callback)
    {
        _errorHandlers.Add(callback);
        return new Subscription<Action<ParseErrorEvent>>(_errorHandlers, callback);
    }

    private void Dispatch(GenerationEvent evt)
    {
        switch (evt)
        {
            case MessageEvent m:
                foreach (var h in _messageHandlers) h(m.Message);
                break;
            case TextEvent t:
                foreach (var h in _textHandlers) h(t.Text);
                break;
            case ParseErrorEvent e:
                foreach (var h in _errorHandlers) h(e);
                break;
        }
    }

    private sealed class Subscription<T>(List<T> list, T item) : IDisposable where T : class
    {
        public void Dispose() => list.Remove(item);
    }
}
