# App Runtime (PoC v1)

## What this contains
- `shared/`: SCN-01 reusable domain logic (generate/update/deploy/rollback + KPI + policy)
- `TizenMiniAppRuntimeMock/`: CLI/runtime behavior mock for SCN-01
- `TizenMiniAppUiScaffold/`: Tizen-ready UI scaffold with screen-state presenter

## Required environment
```bash
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet --info
```

## Run runtime mock
```bash
cd TizenMiniAppRuntimeMock
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run
```

## Run runtime mock SCN-01
```bash
cd TizenMiniAppRuntimeMock
/usr/local/share/dotnet/dotnet run -- run-scn01
```

## Run UI scaffold
```bash
cd TizenMiniAppUiScaffold
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run
```

## Notes
- `TizenMiniAppUiScaffold` currently prints structured screen-state objects for `PromptInput`, `DraftPreview`, `LiveView`, `ValidationPanel`.
- Real Tizen view binding can replace the presenter output layer later without changing shared scenario logic.
