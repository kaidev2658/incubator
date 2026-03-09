using TizenMiniApp.Shared.Scn01;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class ActionExecutor
{
    private readonly Scn01LifecycleService _service;
    private readonly RuntimeRenderModule _renderModule;

    public ActionExecutor(Scn01LifecycleService service, RuntimeRenderModule renderModule)
    {
        _service = service;
        _renderModule = renderModule;
    }

    public bool Generate(string prompt)
    {
        var generated = _service.Generate(prompt);
        if (!generated.IsSuccess || generated.Draft is null)
        {
            _renderModule.Print("generation failed");
            return false;
        }

        _renderModule.Print($"generated: {generated.Draft.AppId} from prompt='{prompt}'");
        return true;
    }

    public bool Update(string partialPrompt)
    {
        var updated = _service.Update(partialPrompt);
        if (!updated.IsSuccess || updated.Draft is null)
        {
            _renderModule.Print(updated.ErrorMessage);
            return false;
        }

        _renderModule.Print($"updated draft -> version {updated.Draft.Version} (partial update: '{partialPrompt}')");
        return true;
    }

    public bool Deploy()
    {
        var deploy = _service.Deploy();
        if (!deploy.IsSuccess || deploy.Live is null)
        {
            _renderModule.Print(deploy.ErrorMessage);
            return false;
        }

        var policyReason = deploy.PolicyResult?.Reason ?? "policy unavailable";
        var publishMessage = deploy.PublishResult?.Message ?? "sync unavailable";
        _renderModule.Print($"deployed live: {deploy.Live.AppId}@{deploy.Live.Version} ({policyReason}, {publishMessage})");
        return true;
    }

    public bool Rollback()
    {
        var rollback = _service.Rollback();
        if (!rollback.IsSuccess || rollback.Live is null)
        {
            _renderModule.Print(rollback.ErrorMessage);
            return false;
        }

        _renderModule.Print($"rollback restored: {rollback.Live.AppId}@{rollback.Live.Version}");
        return true;
    }

    public void Show()
    {
        _renderModule.ShowState(_service.State);
    }

    public void ShowKpi()
    {
        _renderModule.Print(_service.Kpi.RenderJson());
    }

    public bool ValidateRollbackScenario()
    {
        _renderModule.Print("validate: step 1/5 generate");
        if (!Generate("validation-base"))
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: generate failed");
            return false;
        }

        _renderModule.Print("validate: step 2/5 deploy v1");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: first deploy failed");
            return false;
        }

        var firstLive = _service.State.Live;
        if (firstLive is null)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: first deploy missing live");
            return false;
        }

        _renderModule.Print("validate: step 3/5 update draft");
        if (!Update("validation-partial-update"))
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: update failed");
            return false;
        }

        _renderModule.Print("validate: step 4/5 deploy v2");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: second deploy failed");
            return false;
        }

        var secondLive = _service.State.Live;
        if (secondLive is null || secondLive.Version == firstLive.Version)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: second deploy did not advance version");
            return false;
        }

        _renderModule.Print("validate: step 5/5 rollback to v1");
        if (!Rollback())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: rollback failed");
            return false;
        }

        var afterRollback = _service.State.Live;
        var rollbackOk = afterRollback is not null && afterRollback.Version == firstLive.Version;
        if (!rollbackOk)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("validate failed: rollback did not restore previous live");
            return false;
        }

        _service.Kpi.MarkE2E(success: true);
        _renderModule.Print("validate success: generate/update/deploy/rollback scenario passed");
        return true;
    }

    public bool RunScn01()
    {
        _renderModule.Print("scn01: step 1/5 generate");
        if (!Generate("scn01-base"))
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: generate failed");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 2/5 deploy baseline v1");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: first deploy failed");
            PrintScn01Kpi();
            return false;
        }

        var firstLive = _service.State.Live;
        if (firstLive is null)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: first deploy missing live");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 3/5 update draft");
        if (!Update("scn01-partial-update"))
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: update failed");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 4/5 deploy updated v2");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: second deploy failed");
            PrintScn01Kpi();
            return false;
        }

        var secondLive = _service.State.Live;
        if (secondLive is null || secondLive.Version == firstLive.Version)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: second deploy did not advance version");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 5/5 rollback to v1");
        if (!Rollback())
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: rollback failed");
            PrintScn01Kpi();
            return false;
        }

        var afterRollback = _service.State.Live;
        var rollbackOk = afterRollback is not null && afterRollback.Version == firstLive.Version;
        if (!rollbackOk)
        {
            _service.Kpi.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: rollback did not restore previous live");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: fail-safe check blocked actions (camera, microphone)");
        var blockedExamples = EvaluateBlockedActionExamples();
        _renderModule.Print(blockedExamples.CameraBlocked
            ? $"scn01: expected block camera -> {blockedExamples.CameraReason}"
            : "scn01: unexpected allow camera");
        _renderModule.Print(blockedExamples.MicrophoneBlocked
            ? $"scn01: expected block microphone -> {blockedExamples.MicrophoneReason}"
            : "scn01: unexpected allow microphone");

        var blockedExamplesOk = blockedExamples.CameraBlocked && blockedExamples.MicrophoneBlocked;
        _service.Kpi.MarkE2E(blockedExamplesOk);

        _renderModule.Print(blockedExamplesOk
            ? "scn01 success: generate/update/deploy/rollback scenario passed"
            : "scn01 failed: forbidden action was unexpectedly allowed");
        PrintScn01Kpi();
        return blockedExamplesOk;
    }

    public bool RunUiDemo()
    {
        const int stageTotal = 4;

        SetUiStage(UiView.PromptInput, 1, stageTotal, "SCN-01 prompt received: build mini-app and run lifecycle.");
        _renderModule.Print("ui-demo: execute SCN-01 step 1/5 generate");
        if (!Generate("scn01-base"))
        {
            _service.Kpi.MarkE2E(false);
            _renderModule.ShowValidationPanelLine("SCN-01 failed at generate step.");
            PrintScn01Kpi();
            return false;
        }

        var draft = _service.State.Draft;
        SetUiStage(
            UiView.DraftPreview,
            2,
            stageTotal,
            draft is null
                ? "Draft missing after generate."
                : $"Preview draft {draft.AppId}@{draft.Version} actions=[{string.Join(", ", draft.Permissions)}]");

        _renderModule.Print("ui-demo: execute SCN-01 step 2/5 deploy baseline v1");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(false);
            _renderModule.ShowValidationPanelLine("SCN-01 failed at first deploy.");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("ui-demo: execute SCN-01 step 3/5 update draft");
        if (!Update("scn01-partial-update"))
        {
            _service.Kpi.MarkE2E(false);
            _renderModule.ShowValidationPanelLine("SCN-01 failed at partial update.");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("ui-demo: execute SCN-01 step 4/5 deploy updated v2");
        if (!Deploy())
        {
            _service.Kpi.MarkE2E(false);
            _renderModule.ShowValidationPanelLine("SCN-01 failed at second deploy.");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("ui-demo: execute SCN-01 step 5/5 rollback to v1");
        if (!Rollback())
        {
            _service.Kpi.MarkE2E(false);
            _renderModule.ShowValidationPanelLine("SCN-01 failed at rollback.");
            PrintScn01Kpi();
            return false;
        }

        var live = _service.State.Live;
        SetUiStage(
            UiView.LiveView,
            3,
            stageTotal,
            live is null
                ? "Live app unavailable."
                : $"Live app {live.AppId}@{live.Version} is running after rollback validation.");

        var blockedExamples = EvaluateBlockedActionExamples();
        SetUiStage(UiView.ValidationPanel, 4, stageTotal, "Policy checks for blocked actions.");
        _renderModule.ShowValidationPanelLine(blockedExamples.CameraBlocked
            ? $"blocked camera as expected: {blockedExamples.CameraReason}"
            : "camera was unexpectedly allowed");
        _renderModule.ShowValidationPanelLine(blockedExamples.MicrophoneBlocked
            ? $"blocked microphone as expected: {blockedExamples.MicrophoneReason}"
            : "microphone was unexpectedly allowed");

        var success = blockedExamples.CameraBlocked && blockedExamples.MicrophoneBlocked;
        _service.Kpi.MarkE2E(success);

        _renderModule.Print(success
            ? "ui-demo success: SCN-01 completed with staged UI transitions"
            : "ui-demo failed: validation panel detected policy issue");
        PrintScn01Kpi();
        return success;
    }

    private void SetUiStage(UiView view, int stageIndex, int stageTotal, string message)
    {
        var ui = _service.State.Ui;
        ui.CurrentView = view;
        ui.StageIndex = stageIndex;
        ui.StageTotal = stageTotal;
        ui.Message = message;
        _renderModule.ShowUiStage(view, stageIndex, stageTotal, message);
    }

    private BlockedActionValidation EvaluateBlockedActionExamples()
    {
        var camera = _service.ValidateAction("camera");
        var microphone = _service.ValidateAction("microphone");

        return new BlockedActionValidation(
            CameraBlocked: !camera.IsAllowed,
            CameraReason: camera.Reason,
            MicrophoneBlocked: !microphone.IsAllowed,
            MicrophoneReason: microphone.Reason);
    }

    private void PrintScn01Kpi()
    {
        _renderModule.Print($"SCN01_KPI_JSON={_service.Kpi.RenderCompactJson()}");
    }

    private sealed record BlockedActionValidation(
        bool CameraBlocked,
        string CameraReason,
        bool MicrophoneBlocked,
        string MicrophoneReason);
}
