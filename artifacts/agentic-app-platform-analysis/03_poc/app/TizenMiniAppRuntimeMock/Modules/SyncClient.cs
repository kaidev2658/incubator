using TizenMiniApp.Shared.Scn01;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class MockSyncPublisher : ISyncPublisher
{
    public SyncPublishResult Publish(MiniApp app)
    {
        _ = app;
        Thread.Sleep(50);
        return new SyncPublishResult(true, "sync publish ok (mock)");
    }
}

// Skeleton for future real connector integration.
public sealed class RemoteSyncPublisherSkeleton : ISyncPublisher
{
    public SyncPublishResult Publish(MiniApp app)
    {
        _ = app;
        return new SyncPublishResult(false, "remote sync client not implemented");
    }
}
