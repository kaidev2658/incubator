namespace TizenMiniAppRuntimeMock.Modules;

public interface ISyncClient
{
    SyncPublishResult Publish(MiniApp app);
}

public sealed record SyncPublishResult(bool IsSuccess, string Message);

public sealed class MockSyncClient : ISyncClient
{
    public SyncPublishResult Publish(MiniApp app)
    {
        _ = app;
        Thread.Sleep(50);
        return new SyncPublishResult(true, "sync publish ok (mock)");
    }
}

// Skeleton for future real connector integration.
public sealed class RemoteSyncClientSkeleton : ISyncClient
{
    public SyncPublishResult Publish(MiniApp app)
    {
        _ = app;
        return new SyncPublishResult(false, "remote sync client not implemented");
    }
}
