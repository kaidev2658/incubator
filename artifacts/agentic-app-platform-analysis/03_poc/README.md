# PoC

## Objective
Tizen 10에서 Wabi/Essential Apps와 유사한 Agentic mini-app 플랫폼의 생성/실행/관리 흐름을 빠르게 검증

## Transition Stage (2026-03-09)
- 기존 콘솔 런타임 mock을 유지하면서, Tizen UI 연동 전단계로 `TizenMiniAppUiScaffold`를 추가했다.
- SCN-01 공통 도메인 로직(generate/update/deploy/rollback + KPI + policy check)은 `app/shared/`로 추출했다.
- 런타임 mock과 UI scaffold는 동일한 shared 로직을 소비한다.

## Demo Scenario v1
- Scenario ID: `SCN-01 Agentic Mini-App Platform on Tizen`
- Goal: "Tizen에서 Wabi 같은 에이전트 미니앱 플랫폼 데모"

## Runtime Structure (Transition)
- `app/shared/`
  - `PromptEngine`: draft 생성/partial update
  - `PolicyEvaluator`: API index 기반 허용/차단 검증
  - `Scn01LifecycleService`: generate/update/deploy/rollback 상태 전이 orchestration
  - `KpiTracker`: KPI 집계/JSON 출력
  - `ApiMetadataIndexLoader`: `agent-core/api-index/allowed-apis.json` 로딩
- `app/TizenMiniAppRuntimeMock/`
  - CLI 실행/검증용 런타임
  - 기존 SCN-01 시나리오 로그(`run-scn01`, `ui-demo`) 유지
- `app/TizenMiniAppUiScaffold/`
  - Tizen UI 연동 준비용 스캐폴드
  - `ScreenStatePresenter`가 구조화된 화면 상태 객체 출력
  - 출력 타입: `PromptInput`, `DraftPreview`, `LiveView`, `ValidationPanel`

## Environment (Required)
```bash
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet --info
```

## Run Commands (Exact)

### 1) Runtime Mock (interactive)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run
```

### 2) Runtime Mock SCN-01 (non-interactive)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run -- run-scn01
```

### 3) Runtime Mock UI Demo (non-interactive)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run -- ui-demo
```

### 4) Tizen UI Scaffold (structured state output)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppUiScaffold
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run
```

## Script Wrappers
- `./scripts/run-scn01.sh`
- `./scripts/run-ui-demo.sh`

## Done Criteria
- 생성 성공률 KPI 측정 가능
- 생성→실행→관리(E2E) 성공 여부 측정 가능
- Partial Update + Rollback 검증 가능
- API index 외 액션(camera/microphone) 차단 로그 검증 가능
- Runtime mock + UI scaffold 모두 shared domain 로직 재사용
