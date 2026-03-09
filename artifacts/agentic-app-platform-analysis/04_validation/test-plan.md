# Test Plan (PoC v2: SCN-01 + API Index Policy Gate)

## 1) Goal
`03_poc` 런타임에서 SCN-01(Generate -> Update -> Deploy -> Rollback) E2E를 비대화형으로 실행하고,
로컬 API metadata index 기반 정책 셀렉터가 허용 범위 외 액션을 fail-safe로 차단하는지 정량 검증한다.

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
   - `scn01: fail-safe check blocked actions (camera, microphone)`
   - `scn01: expected block camera ->`
   - `scn01: expected block microphone ->`
   - `scn01 success: generate/update/deploy/rollback scenario passed`
3. Policy reason clarity
   - 차단 로그에 `not in api-index` 문구가 포함되어야 한다.
   - 허용 목록(`allowed=[location, ...]`)이 사유 문자열에 포함되어야 한다.
4. KPI JSON line exists
   - 실행 로그에 `SCN01_KPI_JSON=` 접두 라인이 최소 1개 존재해야 한다.
5. KPI JSON exact/threshold checks
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
3. Policy selector failure
   - `camera` 또는 `microphone` 액션이 차단되지 않음
   - 차단 사유가 API index 경로/허용목록을 설명하지 못함
4. KPI emission failure
   - `SCN01_KPI_JSON=` 미출력 또는 JSON 파싱 실패
5. Environment failure
   - `/usr/local/share/dotnet/dotnet --info` 실패

## 5) Artifacts
`03_poc/eval/`에 아래 파일이 생성되어야 한다.

1. `scn01-build-<timestamp>.log`
2. `scn01-run-<timestamp>.log`
3. `scn01-kpi-<timestamp>.json`
