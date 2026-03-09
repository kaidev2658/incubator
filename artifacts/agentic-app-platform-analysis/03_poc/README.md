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

## Platform / Runtime Decisions
- Target: Tizen 10 (TV 또는 Public Tizen IoT headed, 단일기기 우선)
- Architecture: 서버 중심 오케스트레이션 우선
- App model (v1): .NET (C#) 우선
- Out of scope (v1): 오프라인 필수 요구, 민감권한 심화정책, camera/microphone/bluetooth/calling

## Runtime Structure (Mock)
- `PromptModule`: 프롬프트 기반 draft 생성/partial update
- `AppStateModule`: draft/live/previous_live 상태 전이
- `RuntimeRenderModule`: CLI 렌더링 및 상태 출력
- `ActionExecutor`: generate/update/deploy/rollback/validate orchestration
- `PolicyBridge`: 정책 검증 mock bridge
- `SyncClient`: 서버 sync publish mock
- `KpiLogger`: KPI 집계 및 출력

## Run
```bash
cd artifacts/agentic-app-platform-analysis/03_poc/app/TizenMiniAppRuntimeMock
dotnet build
dotnet run
```

## CLI Commands
- `generate <prompt>`: draft 생성
- `update <prompt>`: partial update로 draft 버전 증가
- `deploy`: 정책 검증 + sync mock 후 live 반영
- `rollback`: 이전 live로 복구
- `validate`: rollback 포함 E2E 검증 시나리오 1회 실행
- `kpi`: KPI JSON 출력
- `show`: 현재 runtime state 출력
- `exit`: 종료

## Validation (Rollback + KPI)
런타임에서 아래 순서 실행:
```text
validate
kpi
show
```

예상 KPI 확인 포인트:
- `GenerateSuccessRatePercent` > 0
- `E2ESuccessRatePercent` > 0
- `AverageDeployLatencyMs` > 0
- `RollbackSuccessRatePercent` > 0

## Done Criteria
- 생성 성공률 KPI 측정 가능
- 생성→실행→관리(E2E) 성공 여부 측정 가능
- Partial Update + Rollback 검증 가능
- 재실행 가능한 CLI 기반 절차 제공
