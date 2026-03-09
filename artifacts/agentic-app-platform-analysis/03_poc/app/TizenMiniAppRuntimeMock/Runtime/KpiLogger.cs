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
            GenerateSuccessRatePercent: Percentage(_generateSuccesses, _generateAttempts),
            E2ESuccessRatePercent: Percentage(_e2eSuccesses, _e2eAttempts),
            AverageDeployLatencyMs: _deployLatenciesMs.Count == 0 ? 0 : _deployLatenciesMs.Average(),
            RollbackSuccessRatePercent: Percentage(_rollbackSuccesses, _rollbackAttempts),
            GenerateAttempts: _generateAttempts,
            E2EAttempts: _e2eAttempts,
            DeployCount: _deployLatenciesMs.Count,
            RollbackAttempts: _rollbackAttempts);
    }

    public string RenderJson()
    {
        return JsonSerializer.Serialize(Snapshot(), new JsonSerializerOptions { WriteIndented = true });
    }

    private static double Percentage(int successes, int attempts)
    {
        if (attempts == 0)
        {
            return 0;
        }

        return Math.Round((double)successes / attempts * 100, 2);
    }
}

public sealed record KpiSnapshot(
    double GenerateSuccessRatePercent,
    double E2ESuccessRatePercent,
    double AverageDeployLatencyMs,
    double RollbackSuccessRatePercent,
    int GenerateAttempts,
    int E2EAttempts,
    int DeployCount,
    int RollbackAttempts);
