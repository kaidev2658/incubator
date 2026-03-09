using TizenMiniApp.Shared.Scn01;
using TizenMiniAppUiScaffold.Infrastructure;
using TizenMiniAppUiScaffold.Presentation;

var policy = ApiMetadataIndexLoader.LoadDefaultPolicyEvaluator();
var service = new Scn01LifecycleService(
    new PromptEngine(),
    policy,
    new SyncClient(),
    new KpiTracker());
var presenter = new ScreenStatePresenter();

Console.WriteLine("[TizenMiniAppUiScaffold] SCN-01 transition demo (UI scaffold)");
Console.WriteLine($"api-index loaded: {service.PolicyIndexPath()}; allowed=[{service.RenderAllowedActions()}]");

presenter.Emit(presenter.PresentPromptInput(
    "scn01-base",
    "Prompt received. Transition to draft generation."));

var generate = service.Generate("scn01-base");
if (!generate.IsSuccess)
{
    Console.WriteLine("ui-scaffold failed: generate step");
    service.Kpi.MarkE2E(false);
    Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
    Environment.Exit(1);
}

var deployV1 = service.Deploy();
if (!deployV1.IsSuccess)
{
    Console.WriteLine($"ui-scaffold failed: {deployV1.ErrorMessage}");
    service.Kpi.MarkE2E(false);
    Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
    Environment.Exit(1);
}

var update = service.Update("scn01-partial-update");
if (!update.IsSuccess)
{
    Console.WriteLine($"ui-scaffold failed: {update.ErrorMessage}");
    service.Kpi.MarkE2E(false);
    Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
    Environment.Exit(1);
}

var deployV2 = service.Deploy();
if (!deployV2.IsSuccess)
{
    Console.WriteLine($"ui-scaffold failed: {deployV2.ErrorMessage}");
    service.Kpi.MarkE2E(false);
    Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
    Environment.Exit(1);
}

var rollback = service.Rollback();
if (!rollback.IsSuccess)
{
    Console.WriteLine($"ui-scaffold failed: {rollback.ErrorMessage}");
    service.Kpi.MarkE2E(false);
    Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
    Environment.Exit(1);
}

presenter.Emit(presenter.PresentDraftPreview(service.State));
presenter.Emit(presenter.PresentLiveView(service.State));

var camera = service.ValidateAction("camera");
var microphone = service.ValidateAction("microphone");
presenter.Emit(presenter.PresentValidationPanel(camera, microphone));

var policyCheckPass = !camera.IsAllowed && !microphone.IsAllowed;
service.Kpi.MarkE2E(policyCheckPass);
Console.WriteLine(policyCheckPass
    ? "ui-scaffold success: structured screen states emitted"
    : "ui-scaffold failed: policy check mismatch");
Console.WriteLine($"SCN01_KPI_JSON={service.Kpi.RenderCompactJson()}");
Environment.Exit(policyCheckPass ? 0 : 1);
