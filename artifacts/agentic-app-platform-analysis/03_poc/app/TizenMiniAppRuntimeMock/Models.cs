namespace TizenMiniAppRuntimeMock;

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

public class RuntimeStore
{
    public MiniApp? Draft { get; set; }
    public MiniApp? Live { get; set; }
    public MiniApp? PreviousLive { get; set; }
    public UiScreenState Ui { get; set; } = new();
}
