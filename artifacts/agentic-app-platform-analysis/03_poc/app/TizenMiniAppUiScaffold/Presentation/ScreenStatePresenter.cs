using System.Text.Json;
using TizenMiniApp.Shared.Scn01;

namespace TizenMiniAppUiScaffold.Presentation;

public sealed class ScreenStatePresenter
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public PromptInput PresentPromptInput(string prompt, string message)
    {
        return new PromptInput("PromptInput", prompt, message);
    }

    public DraftPreview PresentDraftPreview(Scn01RuntimeState state)
    {
        var draft = state.Draft;
        return new DraftPreview(
            "DraftPreview",
            draft?.AppId ?? string.Empty,
            draft?.Version ?? string.Empty,
            draft?.Permissions ?? Array.Empty<string>(),
            draft is not null ? "draft ready" : "draft missing");
    }

    public LiveView PresentLiveView(Scn01RuntimeState state)
    {
        var live = state.Live;
        return new LiveView(
            "LiveView",
            live?.AppId ?? string.Empty,
            live?.Version ?? string.Empty,
            live?.Status ?? "unknown",
            live is not null ? "live app rendered" : "live app unavailable");
    }

    public ValidationPanel PresentValidationPanel(PolicyResult camera, PolicyResult microphone)
    {
        return new ValidationPanel(
            "ValidationPanel",
            camera.IsAllowed,
            camera.Reason,
            microphone.IsAllowed,
            microphone.Reason);
    }

    public void Emit(object state)
    {
        Console.WriteLine(JsonSerializer.Serialize(state, _jsonOptions));
    }
}

public sealed record PromptInput(string Type, string Prompt, string Message);
public sealed record DraftPreview(string Type, string AppId, string Version, string[] Permissions, string Message);
public sealed record LiveView(string Type, string AppId, string Version, string Status, string Message);
public sealed record ValidationPanel(string Type, bool CameraAllowed, string CameraReason, bool MicrophoneAllowed, string MicrophoneReason);
