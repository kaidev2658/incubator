using System.Net.Http.Json;
using TizenMiniApp.Shared.Scn01;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class SyncClient : ISyncPublisher
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(2) };
    private readonly string _baseUrl;
    private readonly bool _forceMock;

    public SyncClient()
    {
        _baseUrl = Environment.GetEnvironmentVariable("ORCHESTRATOR_API_BASE_URL")?.TrimEnd('/')
            ?? "http://127.0.0.1:5081";
        _forceMock = string.Equals(
            Environment.GetEnvironmentVariable("ORCHESTRATOR_SYNC_MODE"),
            "mock",
            StringComparison.OrdinalIgnoreCase);
    }

    public SyncPublishResult Publish(MiniApp app)
    {
        if (_forceMock)
        {
            return MockPublish("forced mock mode");
        }

        try
        {
            var response = Http.PostAsJsonAsync(
                    $"{_baseUrl}/deploy",
                    new DeployRequest(app.AppId))
                .GetAwaiter()
                .GetResult();

            if (!response.IsSuccessStatusCode)
            {
                return MockPublish($"api status {(int)response.StatusCode}");
            }

            var payload = response.Content
                .ReadFromJsonAsync<DeployResponse>()
                .GetAwaiter()
                .GetResult();

            var message = payload?.Message ?? "deploy accepted";
            return new SyncPublishResult(true, $"sync publish ok (api:{message})");
        }
        catch (Exception ex)
        {
            return MockPublish($"server unavailable: {ex.GetType().Name}");
        }
    }

    private static SyncPublishResult MockPublish(string reason)
    {
        Thread.Sleep(50);
        return new SyncPublishResult(true, $"sync publish ok (fallback mock: {reason})");
    }

    private sealed record DeployRequest(string AppId);
    private sealed record DeployResponse(string ScenarioId, string Message);
}
