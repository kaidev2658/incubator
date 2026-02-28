using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Transport;

namespace TizenA2uiRenderer.Runtime;

public sealed class A2uiRuntimePipeline : IDisposable
{
    private readonly ITransportAdapter _transport;
    private readonly SurfaceController _controller;
    private readonly ITizenRuntimeAdapter _runtimeAdapter;
    private readonly IDisposable _messageSubscription;
    private readonly IDisposable _textSubscription;
    private readonly IDisposable _parseErrorSubscription;
    private readonly Action<SurfaceUpdate> _surfaceUpdatedHandler;
    private readonly Action<string> _surfaceDeletedHandler;
    private readonly Action<A2uiError> _controllerErrorHandler;

    public event Action<string>? TextReceived;
    public event Action<ParseErrorEvent>? ParseError;
    public event Action<A2uiError>? ControllerError;

    public A2uiRuntimePipeline(
        ITransportAdapter? transport = null,
        SurfaceController? controller = null,
        ITizenRuntimeAdapter? runtimeAdapter = null)
    {
        _transport = transport ?? new TransportAdapter();
        _controller = controller ?? new SurfaceController();
        _runtimeAdapter = runtimeAdapter ?? new NullTizenRuntimeAdapter();

        _messageSubscription = _transport.OnMessage(_controller.HandleMessage);
        _textSubscription = _transport.OnText(text => TextReceived?.Invoke(text));
        _parseErrorSubscription = _transport.OnError(error => ParseError?.Invoke(error));

        _surfaceUpdatedHandler = update => _runtimeAdapter.Render(update.SurfaceId, update.Definition, update.DataModel);
        _surfaceDeletedHandler = surfaceId => _runtimeAdapter.Remove(surfaceId);
        _controllerErrorHandler = error => ControllerError?.Invoke(error);

        _controller.SurfaceUpdated += _surfaceUpdatedHandler;
        _controller.SurfaceDeleted += _surfaceDeletedHandler;
        _controller.Error += _controllerErrorHandler;
    }

    public void AddChunk(string chunk) => _transport.AddChunk(chunk);

    public void AddMessage(NormalMessage message) => _transport.AddMessage(message);

    public IReadOnlyList<GenerationEvent> Flush() => _transport.Flush();

    public void Dispose()
    {
        _controller.SurfaceUpdated -= _surfaceUpdatedHandler;
        _controller.SurfaceDeleted -= _surfaceDeletedHandler;
        _controller.Error -= _controllerErrorHandler;
        _messageSubscription.Dispose();
        _textSubscription.Dispose();
        _parseErrorSubscription.Dispose();
    }
}
