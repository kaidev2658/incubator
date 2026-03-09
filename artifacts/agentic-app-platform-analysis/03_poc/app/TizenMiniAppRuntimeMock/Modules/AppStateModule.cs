namespace TizenMiniAppRuntimeMock.Modules;

public sealed class AppStateModule
{
    private readonly RuntimeStore _store;

    public AppStateModule(RuntimeStore store)
    {
        _store = store;
    }

    public RuntimeStore Snapshot() => _store;

    public void SetDraft(MiniApp draft)
    {
        _store.Draft = draft;
    }

    public bool TryUpdateDraft(Func<MiniApp, MiniApp> update, out MiniApp? updated)
    {
        if (_store.Draft is null)
        {
            updated = null;
            return false;
        }

        var next = update(_store.Draft);
        _store.Draft = next;
        updated = next;
        return true;
    }

    public bool TryDeployDraft(out MiniApp? deployed)
    {
        if (_store.Draft is null)
        {
            deployed = null;
            return false;
        }

        if (_store.Live is not null)
        {
            _store.PreviousLive = _store.Live;
        }

        _store.Live = _store.Draft with { Status = "live" };
        deployed = _store.Live;
        return true;
    }

    public bool TryRollback(out MiniApp? restored)
    {
        if (_store.PreviousLive is null)
        {
            restored = null;
            return false;
        }

        _store.Live = _store.PreviousLive with { Status = "live" };
        restored = _store.Live;
        return true;
    }
}
