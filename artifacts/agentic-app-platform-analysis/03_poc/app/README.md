# App Runtime (PoC v1)

## What this contains
- `TizenMiniAppRuntimeMock/`: C# mock runtime for SCN-01
- supports: generate -> update -> deploy -> rollback -> show

## Run (local)
```bash
export PATH="/usr/local/share/dotnet:$PATH"
cd TizenMiniAppRuntimeMock
dotnet run
```

## Notes
- This is a runtime behavior mock, not full Tizen package yet.
- Next step: bind this logic to actual Tizen UI/runtime adapter.
