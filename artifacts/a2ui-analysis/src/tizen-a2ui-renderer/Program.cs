using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Transport;
using TizenA2uiRenderer.Utils;
using System.Runtime.InteropServices;

var logger = new ConsoleLogger();
var runtimeMode = ResolveRuntimeMode(args);
var runtimeAdapter = CreateRuntimeAdapter(runtimeMode, logger, out var selectedRuntimeMode);

var transport = new TransportAdapter();
var controller = new SurfaceController();

using var pipeline = new A2uiRuntimePipeline(
    transport,
    controller,
    runtimeAdapter,
    new RuntimePipelineOptions
    {
        IntegrationPath = $"tizen-runtime-adapter:{selectedRuntimeMode}"
    },
    logger);

var status = pipeline.RuntimeAdapterStatus;
Console.WriteLine($"Runtime mode selected: {selectedRuntimeMode}");
Console.WriteLine($"Runtime adapter: {status.AdapterType}");
Console.WriteLine($"Runtime initialized: {status.Capabilities.IsInitialized}");
Console.WriteLine($"Runtime capabilities: render={status.Capabilities.SupportsRender}, remove={status.Capabilities.SupportsRemove}, realBinding={status.Capabilities.SupportsRealTizenBinding}");

if (pipeline.StartupDiagnostics.Count > 0)
{
    Console.WriteLine("Startup diagnostics:");
    foreach (var diagnostic in pipeline.StartupDiagnostics)
    {
        Console.WriteLine($"- {diagnostic.Code}: {diagnostic.Message}");
    }
}

Console.WriteLine("Tizen A2UI Renderer initialized.");
pipeline.AddMessage(new NormalMessage("v0.10", NormalMessageType.CreateSurface, "main"));

static string ResolveRuntimeMode(string[] appArgs)
{
    const string envName = "A2UI_RUNTIME_MODE";
    var argMode = appArgs
        .FirstOrDefault(arg => arg.StartsWith("--runtime-mode=", StringComparison.OrdinalIgnoreCase))?
        .Split('=', 2, StringSplitOptions.TrimEntries)[1];
    var envMode = Environment.GetEnvironmentVariable(envName);

    if (!string.IsNullOrWhiteSpace(argMode))
    {
        return argMode;
    }

    if (!string.IsNullOrWhiteSpace(envMode))
    {
        return envMode;
    }

    return "auto";
}

static ITizenRuntimeAdapter CreateRuntimeAdapter(string requestedMode, ILogger logger, out string selectedMode)
{
    selectedMode = NormalizeMode(requestedMode);

    if (selectedMode == "auto")
    {
        selectedMode = RuntimeInformation.IsOSPlatform(OSPlatform.Create("TIZEN"))
            ? "tizen-poc"
            : "renderer-bridge";
    }

    return selectedMode switch
    {
        "null" => new NullTizenRuntimeAdapter(),
        "in-memory" => new InMemoryRuntimeAdapter(),
        "renderer-bridge" => new RendererBridgeRuntimeAdapter(new NullRendererBridge()),
        "tizen-poc" => new TizenRuntimeAdapter(new NullTizenBindingHooks()),
        _ => FallbackForUnknownMode(requestedMode, logger, out selectedMode)
    };
}

static ITizenRuntimeAdapter FallbackForUnknownMode(string requestedMode, ILogger logger, out string selectedMode)
{
    selectedMode = "renderer-bridge";
    logger.Error(
        $"Unknown runtime mode '{requestedMode}', falling back to '{selectedMode}'.",
        fields: new Dictionary<string, object?>
        {
            [StructuredLogFields.Source] = "program.runtime_mode",
            [StructuredLogFields.ErrorComponent] = "runtime",
            [StructuredLogFields.ErrorKind] = "integration",
            [StructuredLogFields.ErrorCode] = ErrorCodes.RuntimeAdapterIntegrationInvalid,
            [StructuredLogFields.ErrorMessage] = $"Unknown runtime mode '{requestedMode}'.",
            [StructuredLogFields.RuntimeMode] = requestedMode
        });
    return new RendererBridgeRuntimeAdapter(new NullRendererBridge());
}

static string NormalizeMode(string mode)
{
    return mode.Trim().ToLowerInvariant() switch
    {
        "inmemory" => "in-memory",
        "bridge" => "renderer-bridge",
        "rendererbridge" => "renderer-bridge",
        "tizen" => "tizen-poc",
        "tizen-binding" => "tizen-poc",
        _ => mode.Trim().ToLowerInvariant()
    };
}
