using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var state = new ConcurrentDictionary<string, AppState>(StringComparer.Ordinal);

app.MapPost("/generate", (GenerateRequest request) =>
{
    var appId = string.IsNullOrWhiteSpace(request.AppId) ? "scn01-miniapp" : request.AppId.Trim();
    var draft = BuildDraft(appId, request.Prompt, "1.0.0");

    var next = state.AddOrUpdate(
        appId,
        _ => new AppState { Draft = draft, Live = null, PreviousLive = null },
        (_, current) => current with { Draft = draft });

    return Results.Ok(new GenerateResponse("SCN-01", next.Draft, "deterministic mock generate"));
});

app.MapPost("/update", (UpdateRequest request) =>
{
    if (!state.TryGetValue(request.AppId, out var current) || current.Draft is null)
    {
        return Results.NotFound(new ErrorResponse("SCN-01", "no draft. call /generate first"));
    }

    var draft = current.Draft;
    var nextVersion = NextPatchVersion(draft.Version);
    var updated = draft with
    {
        Version = nextVersion,
        Name = string.IsNullOrWhiteSpace(request.PartialPrompt) ? draft.Name : $"{draft.Name} (updated)"
    };

    state[request.AppId] = current with { Draft = updated };
    return Results.Ok(new UpdateResponse("SCN-01", updated, "deterministic mock update"));
});

app.MapPost("/deploy", (DeployRequest request) =>
{
    if (!state.TryGetValue(request.AppId, out var current))
    {
        var deterministicDraft = BuildDraft(request.AppId, "scn01-base", "1.0.0");
        current = new AppState { Draft = deterministicDraft, Live = null, PreviousLive = null };
    }

    if (current.Draft is null)
    {
        var deterministicDraft = BuildDraft(request.AppId, "scn01-base", "1.0.0");
        current = current with { Draft = deterministicDraft };
    }

    var live = current.Draft with { Status = "live" };
    var next = current with { PreviousLive = current.Live, Live = live };
    state[request.AppId] = next;

    return Results.Ok(new DeployResponse("SCN-01", live, "deterministic mock deploy"));
});

app.MapPost("/rollback", (RollbackRequest request) =>
{
    if (!state.TryGetValue(request.AppId, out var current) || current.PreviousLive is null)
    {
        return Results.NotFound(new ErrorResponse("SCN-01", "no previous live to rollback"));
    }

    var restored = current.PreviousLive with { Status = "live" };
    var next = current with { Live = restored };
    state[request.AppId] = next;

    return Results.Ok(new RollbackResponse("SCN-01", restored, "deterministic mock rollback"));
});

app.MapGet("/apps/{id}", (string id) =>
{
    if (!state.TryGetValue(id, out var current))
    {
        var baseline = BuildDraft(id, "scn01-base", "1.0.0");
        current = new AppState { Draft = baseline, Live = null, PreviousLive = null };
        state[id] = current;
    }

    return Results.Ok(new AppSnapshotResponse("SCN-01", id, current.Draft, current.Live, current.PreviousLive));
});

app.Run();

static OrchestratorMiniApp BuildDraft(string appId, string? prompt, string version)
{
    var prompted = string.IsNullOrWhiteSpace(prompt) ? "Generated MiniApp" : "Generated MiniApp - Prompted";
    return new OrchestratorMiniApp(
        AppId: appId,
        Name: prompted,
        Version: version,
        Status: "draft",
        Permissions: new[] { "location", "calendar.read", "contacts.read" },
        Layout: "4x2");
}

static string NextPatchVersion(string version)
{
    var current = Version.Parse(version);
    return new Version(current.Major, current.Minor, current.Build + 1).ToString();
}

sealed record AppState
{
    public OrchestratorMiniApp? Draft { get; init; }
    public OrchestratorMiniApp? Live { get; init; }
    public OrchestratorMiniApp? PreviousLive { get; init; }
}

sealed record OrchestratorMiniApp(string AppId, string Name, string Version, string Status, string[] Permissions, string Layout);
sealed record GenerateRequest(string AppId, string? Prompt);
sealed record UpdateRequest(string AppId, string? PartialPrompt);
sealed record DeployRequest(string AppId);
sealed record RollbackRequest(string AppId);
sealed record GenerateResponse(string ScenarioId, OrchestratorMiniApp? Draft, string Message);
sealed record UpdateResponse(string ScenarioId, OrchestratorMiniApp? Draft, string Message);
sealed record DeployResponse(string ScenarioId, OrchestratorMiniApp? Live, string Message);
sealed record RollbackResponse(string ScenarioId, OrchestratorMiniApp? Live, string Message);
sealed record AppSnapshotResponse(string ScenarioId, string AppId, OrchestratorMiniApp? Draft, OrchestratorMiniApp? Live, OrchestratorMiniApp? PreviousLive);
sealed record ErrorResponse(string ScenarioId, string Error);
