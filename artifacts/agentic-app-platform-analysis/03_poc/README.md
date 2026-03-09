# PoC

## Objective
Tizen 10에서 Wabi/Essential Apps와 유사한 Agentic mini-app 플랫폼의 생성/실행/관리 흐름을 빠르게 검증

## Demo Scenario v1
- Scenario ID: `SCN-01 Agentic Mini-App Platform on Tizen`
- Goal: "Tizen에서 Wabi 같은 에이전트 미니앱 플랫폼 데모"

### E2E Flow
1. 사용자 자연어 입력으로 미니앱 생성 요청
2. 에이전트가 앱 초안 생성
3. 부분 수정(Partial Update) 반영
4. 배포 후 실행
5. 버전 롤백(복구) 확인
6. 허용되지 않은 액션(camera/microphone) fail-safe 차단 로그 확인

## Platform / Runtime Decisions
- Target: Tizen 10 (TV 또는 Public Tizen IoT headed, 단일기기 우선)
- Architecture: 서버 중심 오케스트레이션 우선
- App model (v1): .NET (C#) 우선
- Out of scope (v1): 오프라인 필수 요구, 민감권한 심화정책, camera/microphone/bluetooth/calling

## API Metadata Index (Local)
- Location: `agent-core/api-index/`
- Starter index file: `agent-core/api-index/allowed-apis.json`
- Allowed surface:
  - `location`
  - `calendar.read`
  - `contacts.read`
- Starter docs:
  - `agent-core/api-index/location.md`
  - `agent-core/api-index/calendar.read.md`
  - `agent-core/api-index/contacts.read.md`

런타임은 로컬 API 인덱스를 policy selector로 사용하며, 인덱스 외 액션은 명시적인 사유와 함께 차단된다.
(아이디어: `generate-tizen-app`의 선언적 메타데이터/스캐폴딩 접근을 PoC에 맞게 단순화)

## Runtime Structure (Mock)
- `PromptModule`: 프롬프트 기반 draft 생성/partial update
- `AppStateModule`: draft/live/previous_live/UI 상태 전이
- `RuntimeRenderModule`: CLI 렌더링, staged UI 전이 출력, ValidationPanel 출력
- `ActionExecutor`: generate/update/deploy/rollback/validate orchestration + SCN-01 fail-safe checks + `ui-demo`
- `PolicyBridge`: API index 기반 정책 검증 bridge
- `ApiMetadataIndex`: 로컬 허용 API 메타데이터 인덱스 로더/셀렉터
- `ISyncClient` + `MockSyncClient` + `RemoteSyncClientSkeleton`: 서버 sync publish skeleton
- `KpiLogger`: KPI 집계 및 출력

## UI-facing Screen State Layer (Mock)
- `PromptInput`
- `DraftPreview`
- `LiveView`
- `ValidationPanel`

`ui-demo` 실행 시 위 순서대로 staged transition 로그를 출력하며, `ValidationPanel`에서 policy blocked 사유를 함께 노출한다.

## Environment (Required)
```bash
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet --info
```

## Build + SCN-01 (Recommended)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc
./scripts/run-scn01.sh
```

Expected output artifacts:
- `eval/scn01-build-<timestamp>.log`
- `eval/scn01-run-<timestamp>.log`
- `eval/scn01-kpi-<timestamp>.json`

## Build + UI Demo (UI-facing)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc
./scripts/run-ui-demo.sh
```

Expected output artifacts:
- `eval/ui-demo-build-<timestamp>.log`
- `eval/ui-demo-run-<timestamp>.log`
- `eval/ui-demo-kpi-<timestamp>.json`

## Run (Interactive CLI)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run
```

## Run (Non-interactive SCN-01)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run -- run-scn01
```

## Run (Non-interactive UI Demo)
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
export PATH="/usr/local/share/dotnet:$PATH"
/usr/local/share/dotnet/dotnet build
/usr/local/share/dotnet/dotnet run -- ui-demo
```

## CLI Commands
- `generate <prompt>`: draft 생성
- `update <prompt>`: partial update로 draft 버전 증가
- `deploy`: 정책 검증 + sync mock 후 live 반영
- `rollback`: 이전 live로 복구
- `validate`: rollback 포함 E2E 검증 시나리오 1회 실행
- `ui-demo`: SCN-01 + staged UI transition(`PromptInput -> DraftPreview -> LiveView -> ValidationPanel`) 실행
- `kpi`: KPI JSON 출력
- `show`: 현재 runtime state 출력
- `exit`: 종료

## SCN-01 KPI JSON
`run-scn01` 또는 `ui-demo` 실행 시 stdout에 단일 라인이 출력된다.
```text
SCN01_KPI_JSON={"generate_success":1,"e2e_success":1,"deploy_latency_ms":123.45,"rollback_success":1,"generate_attempts":1,"e2e_attempts":1,"deploy_count":2,"rollback_attempts":1}
```

## Validation (Rollback + Policy + KPI)
런타임 실행 후 아래 순서로 검증:
```text
validate
kpi
show
```

`run-scn01` 성공 시 기대 로그:
- `scn01: step 1/5 generate`
- `scn01: step 2/5 deploy baseline v1`
- `scn01: step 3/5 update draft`
- `scn01: step 4/5 deploy updated v2`
- `scn01: step 5/5 rollback to v1`
- `scn01: fail-safe check blocked actions (camera, microphone)`
- `scn01: expected block camera -> ...`
- `scn01: expected block microphone -> ...`
- `scn01 success: generate/update/deploy/rollback scenario passed`

`ui-demo` 성공 시 추가 기대 로그:
- `[UI 1/4] PromptInput: ...`
- `[UI 2/4] DraftPreview: ...`
- `[UI 3/4] LiveView: ...`
- `[UI 4/4] ValidationPanel: ...`
- `[ValidationPanel] blocked camera as expected: policy blocked: ...`
- `[ValidationPanel] blocked microphone as expected: policy blocked: ...`

`kpi` 출력에서 확인할 KPI:
- `generate_success` > 0
- `e2e_success` > 0
- `deploy_latency_ms` > 0
- `rollback_success` > 0

추가 확인 필드:
- `generate_attempts`
- `e2e_attempts`
- `deploy_count`
- `rollback_attempts`

## Done Criteria
- 생성 성공률 KPI 측정 가능
- 생성→실행→관리(E2E) 성공 여부 측정 가능
- Partial Update + Rollback 검증 가능
- API index 외 액션(camera/microphone) 차단 로그 검증 가능
- staged UI transition 로그 검증 가능
- 재실행 가능한 CLI 기반 절차 제공
