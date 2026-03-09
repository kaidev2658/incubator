using System.Diagnostics;

namespace TizenMiniApp.Shared.Scn01;

public sealed class Scn01LifecycleService
{
    private readonly PromptEngine _promptEngine;
    private readonly PolicyEvaluator _policyEvaluator;
    private readonly ISyncPublisher _syncPublisher;

    public Scn01LifecycleService(
        PromptEngine promptEngine,
        PolicyEvaluator policyEvaluator,
        ISyncPublisher syncPublisher,
        KpiTracker kpiTracker)
    {
        _promptEngine = promptEngine;
        _policyEvaluator = policyEvaluator;
        _syncPublisher = syncPublisher;
        Kpi = kpiTracker;
    }

    public Scn01RuntimeState State { get; } = new();

    public KpiTracker Kpi { get; }

    public GenerateResult Generate(string prompt)
    {
        try
        {
            var draft = _promptEngine.Generate(prompt);
            State.Draft = draft;
            Kpi.MarkGenerate(success: true);
            return new GenerateResult(true, draft, string.Empty);
        }
        catch
        {
            Kpi.MarkGenerate(success: false);
            return new GenerateResult(false, null, "generation failed");
        }
    }

    public UpdateResult Update(string partialPrompt)
    {
        if (State.Draft is null)
        {
            return new UpdateResult(false, null, "no draft. run generate first");
        }

        var updated = _promptEngine.PartialUpdate(State.Draft, partialPrompt);
        State.Draft = updated;
        return new UpdateResult(true, updated, string.Empty);
    }

    public DeployResult Deploy()
    {
        if (State.Draft is null)
        {
            return new DeployResult(false, null, null, null, "no draft to deploy");
        }

        var policy = _policyEvaluator.ValidatePermissions(State.Draft.Permissions);
        if (!policy.IsAllowed)
        {
            return new DeployResult(false, null, policy, null, $"deploy blocked: {policy.Reason}");
        }

        var stopwatch = Stopwatch.StartNew();
        var publish = _syncPublisher.Publish(State.Draft);
        stopwatch.Stop();
        Kpi.MarkDeployLatency(stopwatch.ElapsedMilliseconds);

        if (!publish.IsSuccess)
        {
            return new DeployResult(false, null, policy, publish, $"deploy failed on sync publish: {publish.Message}");
        }

        if (State.Live is not null)
        {
            State.PreviousLive = State.Live;
        }

        State.Live = State.Draft with { Status = "live" };
        return new DeployResult(true, State.Live, policy, publish, string.Empty);
    }

    public RollbackResult Rollback()
    {
        if (State.PreviousLive is null)
        {
            Kpi.MarkRollback(false);
            return new RollbackResult(false, null, "no previous_live to restore");
        }

        State.Live = State.PreviousLive with { Status = "live" };
        Kpi.MarkRollback(true);
        return new RollbackResult(true, State.Live, string.Empty);
    }

    public PolicyResult ValidateAction(string action)
    {
        return _policyEvaluator.ValidateAction(action);
    }

    public string RenderAllowedActions()
    {
        return _policyEvaluator.RenderAllowedActions();
    }

    public string PolicyIndexPath()
    {
        return _policyEvaluator.IndexPath;
    }
}

public sealed record GenerateResult(bool IsSuccess, MiniApp? Draft, string ErrorMessage);
public sealed record UpdateResult(bool IsSuccess, MiniApp? Draft, string ErrorMessage);
public sealed record DeployResult(bool IsSuccess, MiniApp? Live, PolicyResult? PolicyResult, SyncPublishResult? PublishResult, string ErrorMessage);
public sealed record RollbackResult(bool IsSuccess, MiniApp? Live, string ErrorMessage);
