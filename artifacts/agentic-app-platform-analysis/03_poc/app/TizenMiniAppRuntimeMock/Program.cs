using TizenMiniAppRuntimeMock;
using TizenMiniAppRuntimeMock.Modules;
using TizenMiniAppRuntimeMock.Runtime;

var store = new RuntimeStore();
var executor = new ActionExecutor(
    new PromptModule(),
    new AppStateModule(store),
    new RuntimeRenderModule(),
    new PolicyBridge(),
    new SyncClient(),
    new KpiLogger());

var renderer = new RuntimeRenderModule();
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
