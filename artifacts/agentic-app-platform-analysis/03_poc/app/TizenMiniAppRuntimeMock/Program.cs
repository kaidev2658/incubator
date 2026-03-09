using TizenMiniApp.Shared.Scn01;
using TizenMiniAppRuntimeMock.Modules;

var renderer = new RuntimeRenderModule();
var policy = ApiMetadataIndexLoader.LoadDefaultPolicyEvaluator();
var service = new Scn01LifecycleService(
    new PromptEngine(),
    policy,
    new MockSyncPublisher(),
    new KpiTracker());

var executor = new ActionExecutor(service, renderer);

renderer.Print($"api-index loaded: {service.PolicyIndexPath()}; allowed=[{service.RenderAllowedActions()}]");

if (args.Length > 0)
{
    var startupCommand = args[0].ToLowerInvariant();
    switch (startupCommand)
    {
        case "run-scn01":
            Environment.ExitCode = executor.RunScn01() ? 0 : 1;
            return;
        case "ui-demo":
            Environment.ExitCode = executor.RunUiDemo() ? 0 : 1;
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
        case "ui-demo":
            executor.RunUiDemo();
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
