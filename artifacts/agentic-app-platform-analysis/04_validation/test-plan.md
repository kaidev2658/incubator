# Test Plan (PoC v3: Full Local E2E with Mock Orchestrator API)

## 1) Goal
`03_poc`에서 로컬 mock orchestrator API를 포함한 Full Local E2E를 실행하고,
SCN-01(Generate -> Update -> Deploy -> Rollback) + UI demo + KPI artifact 수집 경로를 한 번에 검증한다.
정책은 v1 allowlist(`location`, `calendar.read`, `contacts.read`)만 허용하며,
`camera`, `microphone`는 차단되어야 한다.

## 2) Test Command
```bash
cd artifacts/agentic-app-platform-analysis/03_poc
./scripts/run-local-e2e.sh
```

## 3) Required Pass Conditions
아래 항목이 모두 참이면 PASS, 하나라도 실패하면 FAIL.

1. Exit code
   - `run-local-e2e.sh` 종료 코드가 `0` 이어야 한다.
2. Environment check
   - `export PATH="/usr/local/share/dotnet:$PATH"`
   - `/usr/local/share/dotnet/dotnet --info` 성공
3. Mock API ready
   - `/apps/scn01-miniapp` health-like 호출 성공
   - `local-e2e-api-<timestamp>.log` 생성
4. SCN-01 runtime path pass
   - 내부적으로 `run-scn01.sh` 성공
   - `generate/update/deploy/rollback` 단계 로그 존재
   - `camera`, `microphone` 차단 로그 존재
5. UI demo path pass
   - 내부적으로 `run-ui-demo.sh` 성공
   - `PromptInput`, `DraftPreview`, `LiveView`, `ValidationPanel` 출력
6. KPI artifact created
   - `local-e2e-kpi-<timestamp>.json` 생성
   - `scn01_kpi` + `ui_demo_kpi` 모두 포함
   - KPI 필수 키 존재:
     - `generate_success`
     - `e2e_success`
     - `deploy_latency_ms`
     - `rollback_success`
7. Policy gate constraints
   - 허용 action은 allowlist 3개로 제한 (`location`, `calendar.read`, `contacts.read`)
   - camera/microphone 실행 지원이 추가되면 안 됨 (차단 예시만 유지)

## 4) Fail Diagnostics
FAIL 시 원인 분류:

1. Build/runtime failure
   - API 또는 runtime/ui build 실패
2. API availability failure
   - mock orchestrator ready timeout
3. Scenario logic failure
   - SCN-01 단계 누락/실패
4. Policy selector failure
   - `camera`/`microphone` 차단 실패
5. KPI emission failure
   - `SCN01_KPI_JSON=` 누락 또는 local-e2e KPI 파일 누락

## 5) Artifacts
`03_poc/eval/`에 아래 파일이 생성되어야 한다.

1. `local-e2e-api-<timestamp>.log`
2. `local-e2e-scn01-<timestamp>.log`
3. `local-e2e-ui-demo-<timestamp>.log`
4. `local-e2e-kpi-<timestamp>.json`
5. (`run-scn01.sh`, `run-ui-demo.sh` 생성물)
   - `scn01-build-<timestamp>.log`
   - `scn01-run-<timestamp>.log`
   - `scn01-kpi-<timestamp>.json`
   - `ui-demo-build-<timestamp>.log`
   - `ui-demo-run-<timestamp>.log`
   - `ui-demo-kpi-<timestamp>.json`
