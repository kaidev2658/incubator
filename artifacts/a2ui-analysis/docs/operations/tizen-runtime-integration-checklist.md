# Tizen Runtime Integration Checklist (Operators)

## Scope
- 대상: `A2uiRuntimePipeline`를 실제 Tizen UI 런타임에 연결/배포/검증하는 운영자.
- 목표: `Null` wiring, 스레드 경계 오류, 관측성 누락, 복원력 설정 미흡을 배포 전 차단.

## Preflight
1. 런타임 어댑터 검증:
- `ITizenRuntimeAdapter`는 반드시 `RendererBridgeRuntimeAdapter` 또는 동등한 실구현이어야 한다.
- `NullTizenRuntimeAdapter` 사용 시 startup diagnostics에 `E_RUNTIME_ADAPTER_NOT_CONFIGURED`가 발생해야 한다.
2. 브리지 구현 검증:
- `RendererBridgeRuntimeAdapter` 사용 시 내부 bridge가 `NullRendererBridge`가 아니어야 한다.
- `E_RUNTIME_ADAPTER_INTEGRATION_INVALID`가 보이면 배포 차단.
3. Readiness gate:
- 프로덕션 환경에서 `RuntimePipelineOptions.EnforceProductionReadiness=true`를 기본값으로 유지.

## Runtime Wiring
1. 앱 수명주기:
- 앱 시작 시 파이프라인 1회 생성, 종료 시 `Dispose()` 호출.
2. 스레드 분리:
- 입력 스트림 처리와 UI 렌더 실행을 분리.
- `Render`/`Remove`는 Tizen UI 스레드로 마샬링.
3. 에러 이벤트 연결:
- `ParseError`, `ControllerError` 이벤트를 반드시 운영 로깅 파이프라인으로 연결.

## Structured Logging Contract
1. 필수 필드:
- `source`
- `error_component`
- `error_kind`
- `error_code`
- `error_message`
- `integration_path`
2. 조건부 필드:
- `surface_id`, `function_call_id` (문맥 존재 시)
- `operation`, `adapter_type`, `bridge_type` (runtime.adapter 에러 시)
- `raw_line` (parse 계열 에러 시)
3. 검증 규칙:
- `error_component`는 `parser|normalizer|controller|runtime` 중 하나여야 한다.
- `error_kind`는 `parse|validation|state|resilience|control_flow|runtime_operation|integration|internal` 중 하나여야 한다.

## Resilience Configuration
1. 파서 상한:
- `ParserOptions.MaxBufferChars`, `MaxJsonCandidateChars`를 실제 트래픽 최대치 + 여유로 설정.
2. 컨트롤러 상한:
- `PendingTtl`, `FunctionPendingTtl`, `MaxPendingPerSurface`를 서비스 SLO에 맞춰 조정.
3. 지표/알람:
- `E_PARSE_LINE`, `E_PARSE_INCOMPLETE_JSON`, `E_RUNTIME_OPERATION_FAILED`, `E_FUNCTION_TIMEOUT`, `E_FUNCTION_RESPONSE_LATE` 알람 임계치 지정.

## Release Verification
1. 테스트:
- `/usr/local/share/dotnet/dotnet test` 통과.
2. 시나리오:
- long stream + malformed 복구
- cancel + late response
- timeout + late response
3. 롤백 기준:
- startup diagnostics에 integration 오류 존재
- runtime adapter 실패율이 릴리즈 기준 초과
