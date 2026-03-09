using System.Diagnostics;
using TizenMiniAppRuntimeMock.Runtime;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class ActionExecutor
{
    private readonly PromptModule _promptModule;
    private readonly AppStateModule _stateModule;
    private readonly RuntimeRenderModule _renderModule;
    private readonly PolicyBridge _policyBridge;
    private readonly ISyncClient _syncClient;
    private readonly KpiLogger _kpiLogger;

    public ActionExecutor(
        PromptModule promptModule,
        AppStateModule stateModule,
        RuntimeRenderModule renderModule,
        PolicyBridge policyBridge,
        ISyncClient syncClient,
        KpiLogger kpiLogger)
    {
        _promptModule = promptModule;
        _stateModule = stateModule;
        _renderModule = renderModule;
        _policyBridge = policyBridge;
        _syncClient = syncClient;
        _kpiLogger = kpiLogger;
    }

    public bool Generate(string prompt)
    {
        try
        {
            var draft = _promptModule.Generate(prompt);
            _stateModule.SetDraft(draft);
            _kpiLogger.MarkGenerate(success: true);
            _renderModule.Print($"generated: {draft.AppId} from prompt='{prompt}'");
            return true;
        }
        catch
        {
            _kpiLogger.MarkGenerate(success: false);
            _renderModule.Print("generation failed");
            return false;
        }
    }

    public bool Update(string partialPrompt)
    {
        var ok = _stateModule.TryUpdateDraft(d => _promptModule.PartialUpdate(d, partialPrompt), out var updated);
        if (!ok || updated is null)
        {
            _renderModule.Print("no draft. run generate first");
            return false;
        }

        _renderModule.Print($"updated draft -> version {updated.Version} (partial update: '{partialPrompt}')");
        return true;
    }

    public bool Deploy()
    {
        var store = _stateModule.Snapshot();
        if (store.Draft is null)
        {
            _renderModule.Print("no draft to deploy");
            return false;
        }

        var policy = _policyBridge.Validate(store.Draft);
        if (!policy.IsAllowed)
        {
            _renderModule.Print($"deploy blocked: {policy.Reason}");
            return false;
        }

        var stopwatch = Stopwatch.StartNew();
        var publish = _syncClient.Publish(store.Draft);
        stopwatch.Stop();
        _kpiLogger.MarkDeployLatency(stopwatch.ElapsedMilliseconds);

        if (!publish.IsSuccess)
        {
            _renderModule.Print($"deploy failed on sync publish: {publish.Message}");
            return false;
        }

        var deployed = _stateModule.TryDeployDraft(out var live) && live is not null;
        _renderModule.Print(deployed
            ? $"deployed live: {live!.AppId}@{live.Version} ({policy.Reason}, {publish.Message})"
            : "deploy failed: no draft");
        return deployed;
    }

    public bool Rollback()
    {
        var restored = _stateModule.TryRollback(out var live) && live is not null;
        _kpiLogger.MarkRollback(restored);

        _renderModule.Print(restored
            ? $"rollback restored: {live!.AppId}@{live.Version}"
            : "no previous_live to restore");
        return restored;
    }

    public void Show()
    {
        _renderModule.ShowState(_stateModule.Snapshot());
    }

    public void ShowKpi()
    {
        _renderModule.Print(_kpiLogger.RenderJson());
    }

    public bool ValidateRollbackScenario()
    {
        _renderModule.Print("validate: step 1/5 generate");
        if (!Generate("validation-base"))
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: generate failed");
            return false;
        }

        _renderModule.Print("validate: step 2/5 deploy v1");
        if (!Deploy())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: first deploy failed");
            return false;
        }

        var firstLive = _stateModule.Snapshot().Live;
        if (firstLive is null)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: first deploy missing live");
            return false;
        }

        _renderModule.Print("validate: step 3/5 update draft");
        if (!Update("validation-partial-update"))
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: update failed");
            return false;
        }

        _renderModule.Print("validate: step 4/5 deploy v2");
        if (!Deploy())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: second deploy failed");
            return false;
        }

        var secondLive = _stateModule.Snapshot().Live;
        if (secondLive is null || secondLive.Version == firstLive.Version)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: second deploy did not advance version");
            return false;
        }

        _renderModule.Print("validate: step 5/5 rollback to v1");
        if (!Rollback())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: rollback failed");
            return false;
        }

        var afterRollback = _stateModule.Snapshot().Live;
        var rollbackOk = afterRollback is not null && afterRollback.Version == firstLive.Version;
        if (!rollbackOk)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: rollback did not restore previous live");
            return false;
        }

        _kpiLogger.MarkE2E(success: true);
        _renderModule.Print("validate success: generate/update/deploy/rollback scenario passed");
        return true;
    }

    public bool RunScn01()
    {
        _renderModule.Print("scn01: step 1/5 generate");
        if (!Generate("scn01-base"))
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: generate failed");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 2/5 deploy baseline v1");
        if (!Deploy())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: first deploy failed");
            PrintScn01Kpi();
            return false;
        }

        var firstLive = _stateModule.Snapshot().Live;
        if (firstLive is null)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: first deploy missing live");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 3/5 update draft");
        if (!Update("scn01-partial-update"))
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: update failed");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 4/5 deploy updated v2");
        if (!Deploy())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: second deploy failed");
            PrintScn01Kpi();
            return false;
        }

        var secondLive = _stateModule.Snapshot().Live;
        if (secondLive is null || secondLive.Version == firstLive.Version)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: second deploy did not advance version");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: step 5/5 rollback to v1");
        if (!Rollback())
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: rollback failed");
            PrintScn01Kpi();
            return false;
        }

        var afterRollback = _stateModule.Snapshot().Live;
        var rollbackOk = afterRollback is not null && afterRollback.Version == firstLive.Version;
        if (!rollbackOk)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("scn01 failed: rollback did not restore previous live");
            PrintScn01Kpi();
            return false;
        }

        _renderModule.Print("scn01: fail-safe check blocked actions (camera, microphone)");
        var blockedExamplesOk = RunBlockedActionExamples();
        _kpiLogger.MarkE2E(blockedExamplesOk);

        _renderModule.Print(blockedExamplesOk
            ? "scn01 success: generate/update/deploy/rollback scenario passed"
            : "scn01 failed: forbidden action was unexpectedly allowed");
        PrintScn01Kpi();
        return blockedExamplesOk;
    }

    private bool RunBlockedActionExamples()
    {
        var camera = _policyBridge.ValidateAction("camera");
        var cameraBlocked = !camera.IsAllowed;
        _renderModule.Print(cameraBlocked
            ? $"scn01: expected block camera -> {camera.Reason}"
            : "scn01: unexpected allow camera");

        var microphone = _policyBridge.ValidateAction("microphone");
        var microphoneBlocked = !microphone.IsAllowed;
        _renderModule.Print(microphoneBlocked
            ? $"scn01: expected block microphone -> {microphone.Reason}"
            : "scn01: unexpected allow microphone");

        return cameraBlocked && microphoneBlocked;
    }

    private void PrintScn01Kpi()
    {
        _renderModule.Print($"SCN01_KPI_JSON={_kpiLogger.RenderCompactJson()}");
    }
}
