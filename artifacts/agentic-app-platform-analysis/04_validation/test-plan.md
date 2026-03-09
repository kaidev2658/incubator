# Test Plan (PoC v2: SCN-01)

## 1) Goal
`03_poc` 런타임에서 SCN-01(Generate -> Update -> Deploy -> Rollback) E2E를 비대화형으로 실행하고 결과를 정량 검증한다.

## 2) Test Command
```bash
cd artifacts/agentic-app-platform-analysis/03_poc
./scripts/run-scn01.sh
```

## 3) Required Pass Conditions
아래 항목이 모두 참이면 PASS, 하나라도 실패하면 FAIL.

1. Exit code
   - `run-scn01.sh` 종료 코드가 `0` 이어야 한다.
2. Runtime step logs
   - `scn01: step 1/5 generate`
   - `scn01: step 2/5 deploy baseline v1`
   - `scn01: step 3/5 update draft`
   - `scn01: step 4/5 deploy updated v2`
   - `scn01: step 5/5 rollback to v1`
   - `scn01 success: generate/update/deploy/rollback scenario passed`
3. KPI JSON line exists
   - 실행 로그에 `SCN01_KPI_JSON=` 접두 라인이 최소 1개 존재해야 한다.
4. KPI JSON exact/threshold checks
   - `generate_success == 1`
   - `e2e_success == 1`
   - `rollback_success == 1`
   - `generate_attempts == 1`
   - `e2e_attempts == 1`
   - `deploy_count == 2`
   - `rollback_attempts == 1`
   - `deploy_latency_ms > 0`

## 4) Fail Diagnostics
FAIL 시 원인 분류:

1. Build failure
   - `dotnet build` 실패 또는 경고/오류 확인 필요
2. Scenario logic failure
   - step 로그 누락, 버전 증가/롤백 검증 실패
3. KPI emission failure
   - `SCN01_KPI_JSON=` 미출력 또는 JSON 파싱 실패
4. Environment failure
   - `/usr/local/share/dotnet/dotnet --info` 실패

## 5) Artifacts
`03_poc/eval/`에 아래 파일이 생성되어야 한다.

1. `scn01-build-<timestamp>.log`
2. `scn01-run-<timestamp>.log`
3. `scn01-kpi-<timestamp>.json`
