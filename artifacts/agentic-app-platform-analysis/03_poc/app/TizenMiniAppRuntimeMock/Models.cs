namespace TizenMiniAppRuntimeMock;

public record MiniApp(string AppId, string Name, string Version, string Status, string[] Permissions, string Layout);

public class RuntimeStore
{
    public MiniApp? Draft { get; set; }
    public MiniApp? Live { get; set; }
    public MiniApp? PreviousLive { get; set; }
}
