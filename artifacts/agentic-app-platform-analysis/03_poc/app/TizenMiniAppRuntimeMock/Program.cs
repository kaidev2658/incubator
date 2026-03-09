using TizenMiniAppRuntimeMock;
using TizenMiniAppRuntimeMock.Modules;
using TizenMiniAppRuntimeMock.Runtime;

var store = new RuntimeStore();
var renderer = new RuntimeRenderModule();
var apiIndex = ApiMetadataIndex.LoadDefault();

var executor = new ActionExecutor(
    new PromptModule(),
    new AppStateModule(store),
    renderer,
    new PolicyBridge(apiIndex),
    new MockSyncClient(),
    new KpiLogger());

renderer.Print($"api-index loaded: {apiIndex.IndexPath}; allowed=[{apiIndex.RenderAllowedActions()}]");

if (args.Length > 0)
{
    var startupCommand = args[0].ToLowerInvariant();
    switch (startupCommand)
    {
        case "run-scn01":
            var scn01Passed = executor.RunScn01();
            Environment.ExitCode = scn01Passed ? 0 : 1;
            return;
        default:
            renderer.Print($"unknown startup command: {args[0]}");
            Environment.ExitCode = 2;
            return;
    }
}

renderer.Header();

while (true)
{
    Console.Write("> ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }

    var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    var command = parts[0].ToLowerInvariant();
    var arg = parts.Length > 1 ? parts[1] : string.Empty;

    switch (command)
    {
        case "generate":
            executor.Generate(arg);
            break;
        case "update":
            executor.Update(arg);
            break;
        case "deploy":
            executor.Deploy();
            break;
        case "rollback":
            executor.Rollback();
            break;
        case "validate":
            executor.ValidateRollbackScenario();
            break;
        case "kpi":
            executor.ShowKpi();
            break;
        case "show":
            executor.Show();
            break;
        case "exit":
            return;
        default:
            renderer.Print("unknown command");
            break;
    }
}
