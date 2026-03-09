using TizenMiniApp.Shared.Scn01;

namespace TizenMiniAppUiScaffold.Infrastructure;

public sealed class MockSyncPublisher : ISyncPublisher
{
    public SyncPublishResult Publish(MiniApp app)
    {
        _ = app;
        Thread.Sleep(50);
        return new SyncPublishResult(true, "sync publish ok (ui scaffold mock)");
    }
}
