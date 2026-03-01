using System.Runtime.InteropServices;
using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Transport;
using TizenA2uiRenderer.Utils;

namespace TizenA2uiRenderer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var selection = RuntimeModeSelector.Select(args);
            var runtimeAdapter = RuntimeModeSelector.CreateRuntimeAdapter(selection, logger, out var selectedRuntimeMode);

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
            pipeline.AddMessage(new("v0.10", NormalMessageType.CreateSurface, "main"));
        }
    }
}

namespace TizenA2uiRenderer.Runtime
{
    public sealed record RuntimeModeSelection(
        string RequestedMode,
        string NormalizedMode,
        string SelectedMode,
        bool IsTizenHost,
        bool IsFallback);

    public static class RuntimeModeSelector
    {
        private const string RuntimeModeEnvName = "A2UI_RUNTIME_MODE";

        public static RuntimeModeSelection Select(
            string[] appArgs,
            Func<string?>? envModeProvider = null,
            Func<bool>? isTizenHostProvider = null)
        {
            var requestedMode = ResolveRequestedMode(appArgs, envModeProvider);
            var normalizedMode = NormalizeMode(requestedMode);
            var selectedMode = normalizedMode;
            var isTizenHost = (isTizenHostProvider ?? IsTizenHost)();

            if (selectedMode == "auto")
            {
                selectedMode = isTizenHost
                    ? "tizen-nui"
                    : "renderer-bridge";
            }

            var isKnownMode = selectedMode is "null" or "in-memory" or "renderer-bridge" or "tizen-nui";
            return new RuntimeModeSelection(
                RequestedMode: requestedMode,
                NormalizedMode: normalizedMode,
                IsTizenHost: isTizenHost,
                SelectedMode: isKnownMode ? selectedMode : "renderer-bridge",
                IsFallback: !isKnownMode);
        }

        public static ITizenRuntimeAdapter CreateRuntimeAdapter(
            RuntimeModeSelection selection,
            ILogger logger,
            out string selectedMode)
        {
            selectedMode = selection.SelectedMode;

            if (selection.IsFallback)
            {
                logger.Error(
                    $"Unknown runtime mode '{selection.RequestedMode}', falling back to '{selectedMode}'.",
                    fields: new Dictionary<string, object?>
                    {
                        [StructuredLogFields.Source] = "program.runtime_mode",
                        [StructuredLogFields.ErrorComponent] = "runtime",
                        [StructuredLogFields.ErrorKind] = "integration",
                        [StructuredLogFields.ErrorCode] = ErrorCodes.RuntimeAdapterIntegrationInvalid,
                        [StructuredLogFields.ErrorMessage] = $"Unknown runtime mode '{selection.RequestedMode}'.",
                        [StructuredLogFields.RuntimeMode] = selection.RequestedMode
                    });
            }

            if (selectedMode == "tizen-nui" && !selection.IsTizenHost)
            {
                logger.Error(
                    "Runtime mode 'tizen-nui' requested on non-Tizen host, falling back to 'renderer-bridge'.",
                    fields: new Dictionary<string, object?>
                    {
                        [StructuredLogFields.Source] = "program.runtime_mode",
                        [StructuredLogFields.ErrorComponent] = "runtime",
                        [StructuredLogFields.ErrorKind] = "integration",
                        [StructuredLogFields.ErrorCode] = ErrorCodes.RuntimeAdapterIntegrationInvalid,
                        [StructuredLogFields.ErrorMessage] = "tizen-nui mode requires a Tizen host.",
                        [StructuredLogFields.RuntimeMode] = "tizen-nui"
                    });
                selectedMode = "renderer-bridge";
            }

            return selectedMode switch
            {
                "null" => new NullTizenRuntimeAdapter(),
                "in-memory" => new InMemoryRuntimeAdapter(),
                "renderer-bridge" => new RendererBridgeRuntimeAdapter(new NullRendererBridge()),
                "tizen-nui" => new TizenRuntimeAdapter(new NuiBindingHooks(
                    hostSupportsNativeBinding: selection.IsTizenHost,
                    bindingName: "tizen-nui-binding-scaffold")),
                _ => new RendererBridgeRuntimeAdapter(new NullRendererBridge())
            };
        }

        public static string ResolveRequestedMode(
            string[] appArgs,
            Func<string?>? envModeProvider = null)
        {
            var argMode = appArgs
                .FirstOrDefault(arg => arg.StartsWith("--runtime-mode=", StringComparison.OrdinalIgnoreCase))?
                .Split('=', 2, StringSplitOptions.TrimEntries)[1];
            var envMode = (envModeProvider ?? (() => Environment.GetEnvironmentVariable(RuntimeModeEnvName)))();

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

        public static string NormalizeMode(string mode)
        {
            var normalized = mode.Trim().ToLowerInvariant();
            return normalized switch
            {
                "inmemory" => "in-memory",
                "bridge" => "renderer-bridge",
                "rendererbridge" => "renderer-bridge",
                "tizen" => "tizen-nui",
                "tizen-binding" => "tizen-nui",
                "tizen-poc" => "tizen-nui",
                _ => normalized
            };
        }

        public static bool IsTizenHost()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Create("TIZEN"));
    }
}
