# Tizen Runtime Incident Playbook (Operators)

## 1. Runtime Adapter Misconfiguration
## Trigger
- `E_RUNTIME_ADAPTER_NOT_CONFIGURED`
- `E_RUNTIME_ADAPTER_INTEGRATION_INVALID`

## Immediate Actions
1. 신규 배포 트래픽 차단 또는 직전 정상 버전으로 롤백.
2. DI/wiring 설정에서 `NullTizenRuntimeAdapter` / `NullRendererBridge` 유입 여부 확인.
3. `EnforceProductionReadiness` 비활성화 여부 확인 후 정책에 맞게 복구.

## Exit Criteria
- startup diagnostics 비어 있음
- 동일 환경 재배포 후 미재현

## 2. Parse/Normalizer Degradation
## Trigger
- `E_PARSE_LINE`, `E_PARSE_JSON_TOO_LARGE`, `E_PARSE_INCOMPLETE_JSON` 급증
- `E_UNSUPPORTED_VERSION`, `E_UNKNOWN_MESSAGE` 급증

## Immediate Actions
1. 최근 5~15분 raw input 샘플 수집(`raw_line` 포함 로그).
2. malformed 비율, payload 크기 분포, version mix 비율 확인.
3. 상류 생성기 릴리즈 여부/스키마 변경 여부 확인.

## Containment
1. 과도한 payload 차단(게이트웨이/프록시 제한).
2. 입력 분할 정책 재조정(chunk size, line framing).
3. 필요 시 parser 상한 임시 상향 후 부하 관측.

## Exit Criteria
- parse 실패율 정상 임계치 복귀
- malformed 샘플 원인 식별 및 차단 룰 반영

## 3. Runtime Operation Failures
## Trigger
- `E_RUNTIME_OPERATION_FAILED`

## Immediate Actions
1. 로그 필드 `operation`, `surface_id`, `adapter_type`, `bridge_type`로 실패 그룹핑.
2. 단일 surface 집중 실패인지, 전역 장애인지 분리.
3. UI 스레드 starvation 또는 bridge exception 패턴 확인.

## Containment
1. 단일 surface 장애 시 delete/recreate 정책 실행.
2. 광범위 장애 시 feature gate로 입력 처리 감속 또는 pause.
3. runtime adapter hotfix 적용 전 트래픽 제한 유지.

## Exit Criteria
- 15분 연속 runtime operation 실패율 정상
- 동일 surface 재발률 기준치 이내

## 4. Function Correlation Incidents
## Trigger
- `E_FUNCTION_RESPONSE_ORPHAN`
- `E_FUNCTION_RESPONSE_LATE`
- `E_FUNCTION_SURFACE_MISMATCH`
- `E_FUNCTION_TIMEOUT`
- `E_FUNCTION_CANCELLED` 비정상 급증

## Immediate Actions
1. `function_call_id`별 call/response/cancel/delete 타임라인 재구성.
2. timeout/cancel 이후 late response 허용 정책과 실제 구현 동작 비교.
3. surface lifecycle(delete/recreate)와 call lifecycle의 동기화 누락 확인.

## Containment
1. 문제 구간에서 function call 재시도 정책(또는 차단 정책) 임시 조정.
2. timeout 기준(`FunctionPendingTtl`)을 트래픽/모델 응답시간에 맞게 조정.

## Exit Criteria
- late/orphan 비율 정상화
- 상류/하류 어느 쪽 correlation fault인지 책임 경계 확정

## 5. Postmortem Template
1. 사고 구간(시작/종료 시각, 영향 surface 수, 영향 사용자 수)
2. error_code 분포와 대표 `function_call_id` 타임라인
3. 근본 원인(입력/컨트롤러/런타임/브리지/운영 설정)
4. 즉시 조치/영구 조치/검증 방법
5. 재발 방지 체크리스트 업데이트 항목
