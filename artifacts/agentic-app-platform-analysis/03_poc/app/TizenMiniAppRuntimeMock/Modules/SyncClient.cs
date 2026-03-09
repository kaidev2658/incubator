namespace TizenMiniAppRuntimeMock.Modules;

public sealed class SyncClient
{
    public bool Publish(MiniApp app)
    {
        _ = app;
        Thread.Sleep(50);
        return true;
    }
}
