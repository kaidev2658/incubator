using System.Diagnostics;
using TizenMiniAppRuntimeMock.Runtime;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class ActionExecutor
{
    private readonly PromptModule _promptModule;
    private readonly AppStateModule _stateModule;
    private readonly RuntimeRenderModule _renderModule;
    private readonly PolicyBridge _policyBridge;
    private readonly SyncClient _syncClient;
    private readonly KpiLogger _kpiLogger;

    public ActionExecutor(
        PromptModule promptModule,
        AppStateModule stateModule,
        RuntimeRenderModule renderModule,
        PolicyBridge policyBridge,
        SyncClient syncClient,
        KpiLogger kpiLogger)
    {
        _promptModule = promptModule;
        _stateModule = stateModule;
        _renderModule = renderModule;
        _policyBridge = policyBridge;
        _syncClient = syncClient;
        _kpiLogger = kpiLogger;
    }

    public void Generate(string prompt)
    {
        try
        {
            var draft = _promptModule.Generate(prompt);
            _stateModule.SetDraft(draft);
            _kpiLogger.MarkGenerate(success: true);
            _renderModule.Print($"generated: {draft.AppId} from prompt='{prompt}'");
        }
        catch
        {
            _kpiLogger.MarkGenerate(success: false);
            _renderModule.Print("generation failed");
        }
    }

    public void Update(string partialPrompt)
    {
        var ok = _stateModule.TryUpdateDraft(d => _promptModule.PartialUpdate(d, partialPrompt), out var updated);
        if (!ok || updated is null)
        {
            _renderModule.Print("no draft. run generate first");
            return;
        }

        _renderModule.Print($"updated draft -> version {updated.Version} (partial update: '{partialPrompt}')");
    }

    public void Deploy()
    {
        var store = _stateModule.Snapshot();
        if (store.Draft is null)
        {
            _renderModule.Print("no draft to deploy");
            return;
        }

        var policy = _policyBridge.Validate(store.Draft);
        if (!policy.IsAllowed)
        {
            _renderModule.Print($"deploy blocked: {policy.Reason}");
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var published = _syncClient.Publish(store.Draft);
        stopwatch.Stop();
        _kpiLogger.MarkDeployLatency(stopwatch.ElapsedMilliseconds);

        if (!published)
        {
            _renderModule.Print("deploy failed on sync publish (mock)");
            return;
        }

        var deployed = _stateModule.TryDeployDraft(out var live) && live is not null;
        _renderModule.Print(deployed
            ? $"deployed live: {live!.AppId}@{live.Version} ({policy.Reason})"
            : "deploy failed: no draft");
    }

    public void Rollback()
    {
        var restored = _stateModule.TryRollback(out var live) && live is not null;
        _kpiLogger.MarkRollback(restored);

        _renderModule.Print(restored
            ? $"rollback restored: {live!.AppId}@{live.Version}"
            : "no previous_live to restore");
    }

    public void Show()
    {
        _renderModule.ShowState(_stateModule.Snapshot());
    }

    public void ShowKpi()
    {
        _renderModule.Print(_kpiLogger.RenderJson());
    }

    public void ValidateRollbackScenario()
    {
        Generate("validation-base");
        Deploy();

        var firstLive = _stateModule.Snapshot().Live;
        if (firstLive is null)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: first deploy missing live");
            return;
        }

        Update("validation-partial-update");
        Deploy();

        var secondLive = _stateModule.Snapshot().Live;
        if (secondLive is null || secondLive.Version == firstLive.Version)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: second deploy did not advance version");
            return;
        }

        Rollback();

        var afterRollback = _stateModule.Snapshot().Live;
        var rollbackOk = afterRollback is not null && afterRollback.Version == firstLive.Version;
        if (!rollbackOk)
        {
            _kpiLogger.MarkE2E(success: false);
            _renderModule.Print("validate failed: rollback did not restore previous live");
            return;
        }

        _kpiLogger.MarkE2E(success: true);
        _renderModule.Print("validate success: generate/update/deploy/rollback scenario passed");
    }
}
