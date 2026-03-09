using System.Text.Json;

namespace TizenMiniAppRuntimeMock.Runtime;

public sealed class KpiLogger
{
    private int _generateAttempts;
    private int _generateSuccesses;
    private int _e2eAttempts;
    private int _e2eSuccesses;
    private int _rollbackAttempts;
    private int _rollbackSuccesses;
    private readonly List<long> _deployLatenciesMs = new();

    public void MarkGenerate(bool success)
    {
        _generateAttempts++;
        if (success)
        {
            _generateSuccesses++;
        }
    }

    public void MarkE2E(bool success)
    {
        _e2eAttempts++;
        if (success)
        {
            _e2eSuccesses++;
        }
    }

    public void MarkDeployLatency(long latencyMs)
    {
        _deployLatenciesMs.Add(latencyMs);
    }

    public void MarkRollback(bool success)
    {
        _rollbackAttempts++;
        if (success)
        {
            _rollbackSuccesses++;
        }
    }

    public KpiSnapshot Snapshot()
    {
        return new KpiSnapshot(
            generate_success: Ratio(_generateSuccesses, _generateAttempts),
            e2e_success: Ratio(_e2eSuccesses, _e2eAttempts),
            deploy_latency_ms: _deployLatenciesMs.Count == 0 ? 0 : Math.Round(_deployLatenciesMs.Average(), 2),
            rollback_success: Ratio(_rollbackSuccesses, _rollbackAttempts),
            generate_attempts: _generateAttempts,
            e2e_attempts: _e2eAttempts,
            deploy_count: _deployLatenciesMs.Count,
            rollback_attempts: _rollbackAttempts);
    }

    public string RenderJson()
    {
        return JsonSerializer.Serialize(Snapshot(), new JsonSerializerOptions { WriteIndented = true });
    }

    private static double Ratio(int successes, int attempts)
    {
        if (attempts == 0)
        {
            return 0;
        }

        return Math.Round((double)successes / attempts, 4);
    }
}

public sealed record KpiSnapshot(
    double generate_success,
    double e2e_success,
    double deploy_latency_ms,
    double rollback_success,
    int generate_attempts,
    int e2e_attempts,
    int deploy_count,
    int rollback_attempts);
