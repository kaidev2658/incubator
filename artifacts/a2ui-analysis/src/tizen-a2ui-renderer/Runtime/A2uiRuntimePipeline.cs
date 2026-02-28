using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Transport;
using TizenA2uiRenderer.Utils;

namespace TizenA2uiRenderer.Runtime;

public sealed class RuntimePipelineOptions
{
    public bool EnforceProductionReadiness { get; init; }
    public bool ThrowOnRuntimeAdapterError { get; init; }
    public string IntegrationPath { get; init; } = "tizen-runtime-adapter";
}

public sealed class A2uiRuntimePipeline : IDisposable
{
    private readonly ITransportAdapter _transport;
    private readonly SurfaceController _controller;
    private readonly ITizenRuntimeAdapter _runtimeAdapter;
    private readonly RuntimePipelineOptions _options;
    private readonly ILogger _logger;
    private readonly List<A2uiError> _startupDiagnostics = [];
    private readonly IDisposable _messageSubscription;
    private readonly IDisposable _textSubscription;
    private readonly IDisposable _parseErrorSubscription;
    private readonly Action<SurfaceUpdate> _surfaceUpdatedHandler;
    private readonly Action<string> _surfaceDeletedHandler;
    private readonly Action<A2uiError> _controllerErrorHandler;

    public event Action<string>? TextReceived;
    public event Action<ParseErrorEvent>? ParseError;
    public event Action<A2uiError>? ControllerError;
    public IReadOnlyList<A2uiError> StartupDiagnostics => _startupDiagnostics;

    public A2uiRuntimePipeline(
        ITransportAdapter? transport = null,
        SurfaceController? controller = null,
        ITizenRuntimeAdapter? runtimeAdapter = null,
        RuntimePipelineOptions? options = null,
        ILogger? logger = null)
    {
        _transport = transport ?? new TransportAdapter();
        _controller = controller ?? new SurfaceController();
        _runtimeAdapter = runtimeAdapter ?? new NullTizenRuntimeAdapter();
        _options = options ?? new RuntimePipelineOptions();
        _logger = logger ?? new NullLogger();

        ValidateRuntimeAdapterReadiness();

        _messageSubscription = _transport.OnMessage(_controller.HandleMessage);
        _textSubscription = _transport.OnText(text => TextReceived?.Invoke(text));
        _parseErrorSubscription = _transport.OnError(HandleParseError);

        _surfaceUpdatedHandler = update => ExecuteRuntimeOperation(
            RuntimeOperationType.Render,
            update.SurfaceId,
            () => _runtimeAdapter.Render(update.SurfaceId, update.Definition, update.DataModel));
        _surfaceDeletedHandler = surfaceId => ExecuteRuntimeOperation(
            RuntimeOperationType.Remove,
            surfaceId,
            () => _runtimeAdapter.Remove(surfaceId));
        _controllerErrorHandler = error =>
        {
            LogControllerError(error);
            ControllerError?.Invoke(error);
        };

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

    private void ValidateRuntimeAdapterReadiness()
    {
        var diagnostics = new List<A2uiError>();

        if (_runtimeAdapter is NullTizenRuntimeAdapter)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterNotConfigured,
                "Runtime adapter is NullTizenRuntimeAdapter. Use RendererBridgeRuntimeAdapter for Tizen integration."));
        }

        if (_runtimeAdapter is RendererBridgeRuntimeAdapter bridgeAdapter && bridgeAdapter.IsNoopBridge)
        {
            diagnostics.Add(new A2uiError(
                ErrorCodes.RuntimeAdapterIntegrationInvalid,
                "RendererBridgeRuntimeAdapter is bound to NullRendererBridge. Replace with real Tizen bridge."));
        }

        foreach (var diagnostic in diagnostics)
        {
            _startupDiagnostics.Add(diagnostic);
            _logger.Error(
                diagnostic.Message,
                fields: CreateErrorFields(
                    source: "runtime.startup",
                    code: diagnostic.Code,
                    message: diagnostic.Message));
        }

        if (_options.EnforceProductionReadiness && diagnostics.Count > 0)
        {
            var first = diagnostics[0];
            throw new InvalidOperationException($"{first.Code}: {first.Message}");
        }
    }

    private void HandleParseError(ParseErrorEvent error)
    {
        _logger.Error(
            error.Message,
            fields: CreateErrorFields(
                source: "transport.parse",
                code: error.Code,
                message: error.Message,
                rawLine: error.RawLine));
        ParseError?.Invoke(error);
    }

    private void LogControllerError(A2uiError error)
    {
        _logger.Error(
            error.Message,
            fields: CreateErrorFields(
                source: "controller",
                code: error.Code,
                message: error.Message,
                surfaceId: error.SurfaceId,
                functionCallId: error.FunctionCallId));
    }

    private void ExecuteRuntimeOperation(RuntimeOperationType operation, string surfaceId, Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            var message = $"runtime adapter {operation.ToString().ToLowerInvariant()} failed for surface '{surfaceId}'.";
            var runtimeError = new A2uiError(ErrorCodes.RuntimeOperationFailed, message, surfaceId);

            _logger.Error(
                message,
                ex,
                CreateErrorFields(
                    source: "runtime.adapter",
                    code: runtimeError.Code,
                    message: runtimeError.Message,
                    surfaceId: surfaceId,
                    operation: operation.ToString().ToLowerInvariant(),
                    adapterType: _runtimeAdapter.GetType().Name,
                    bridgeType: GetBridgeType()));
            ControllerError?.Invoke(runtimeError);

            if (_options.ThrowOnRuntimeAdapterError)
            {
                throw;
            }
        }
    }

    private Dictionary<string, object?> CreateErrorFields(
        string source,
        string code,
        string message,
        string? surfaceId = null,
        string? functionCallId = null,
        string? operation = null,
        string? adapterType = null,
        string? bridgeType = null,
        string? rawLine = null)
    {
        return new Dictionary<string, object?>
        {
            [StructuredLogFields.Source] = source,
            [StructuredLogFields.ErrorComponent] = ErrorCodes.ClassifyComponent(code),
            [StructuredLogFields.ErrorKind] = ErrorCodes.ClassifyKind(code),
            [StructuredLogFields.ErrorCode] = code,
            [StructuredLogFields.ErrorMessage] = message,
            [StructuredLogFields.SurfaceId] = surfaceId,
            [StructuredLogFields.FunctionCallId] = functionCallId,
            [StructuredLogFields.Operation] = operation,
            [StructuredLogFields.IntegrationPath] = _options.IntegrationPath,
            [StructuredLogFields.AdapterType] = adapterType,
            [StructuredLogFields.BridgeType] = bridgeType,
            [StructuredLogFields.RawLine] = rawLine
        };
    }

    private string? GetBridgeType()
        => _runtimeAdapter is RendererBridgeRuntimeAdapter bridge ? bridge.BridgeTypeName : null;
}
