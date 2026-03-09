namespace TizenMiniApp.Shared.Scn01;

public record MiniApp(string AppId, string Name, string Version, string Status, string[] Permissions, string Layout);

public enum UiView
{
    PromptInput,
    DraftPreview,
    LiveView,
    ValidationPanel
}

public sealed class UiScreenState
{
    public UiView CurrentView { get; set; } = UiView.PromptInput;
    public int StageIndex { get; set; }
    public int StageTotal { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class Scn01RuntimeState
{
    public MiniApp? Draft { get; set; }
    public MiniApp? Live { get; set; }
    public MiniApp? PreviousLive { get; set; }
    public UiScreenState Ui { get; set; } = new();
}

public sealed record PolicyResult(bool IsAllowed, string Reason);
public sealed record SyncPublishResult(bool IsSuccess, string Message);

public interface ISyncPublisher
{
    SyncPublishResult Publish(MiniApp app);
}
