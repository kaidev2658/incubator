# Tizen A2UI Renderer (C#/.NET)

Tizen용 A2UI 스트림 파서/노멀라이저/컨트롤러의 실험 구현입니다.

## Build
```bash
/usr/local/share/dotnet/dotnet build artifacts/a2ui-analysis/src/tizen-a2ui-renderer/TizenA2uiRenderer.csproj
```

## Run
```bash
/usr/local/share/dotnet/dotnet run --project artifacts/a2ui-analysis/src/tizen-a2ui-renderer/TizenA2uiRenderer.csproj
```

## Test
```bash
/usr/local/share/dotnet/dotnet test artifacts/a2ui-analysis/tests/TizenA2uiRenderer.Tests/TizenA2uiRenderer.Tests.csproj
```

## Input Rules (Current)
- 입력은 JSONL, fenced markdown JSON block, plain text 혼합 스트림을 허용한다.
- 버전은 `v0.9`/`v0.10`만 허용한다. 생략 시 휴리스틱으로 감지한다.
- `callFunction`/`functionResponse`는 `v0.10`에서만 허용한다.
- `callFunction`은 `functionCallId`, `callFunction.name`, `callFunction.surfaceId`가 필요하다.
- `functionResponse`는 `functionCallId`, `functionResponse.value`가 필요하다.
- 파서는 malformed line을 에러로 보고하고 이후 라인은 계속 처리한다.
- flush 시 미완성 JSON 후보가 남아 있으면 `E_PARSE_INCOMPLETE_JSON`을 발생시킨다.

## Runtime Function Validation
- 컨트롤러는 pending function call을 `functionCallId`로 추적한다.
- 중복 `functionCallId`는 `E_FUNCTION_CALL_DUPLICATE`.
- 대응 call 없이 온 `functionResponse`는 `E_FUNCTION_RESPONSE_ORPHAN`.
- response의 `surfaceId`가 호출 surface와 다르면 `E_FUNCTION_SURFACE_MISMATCH`.
- pending TTL 초과 시 `E_FUNCTION_TIMEOUT`.

## Error Codes (Primary)
- Parser: `E_PARSE_LINE`, `E_PARSE_OVERFLOW`, `E_PARSE_JSON_TOO_LARGE`, `E_PARSE_INCOMPLETE_JSON`
- Normalizer: `E_UNSUPPORTED_VERSION`, `E_UNKNOWN_MESSAGE`, `E_SURFACE_ID_REQUIRED`, `E_FUNCTION_CALL_ID_REQUIRED`, `E_FUNCTION_RESPONSE_VALUE_REQUIRED`
- Controller: `E_SURFACE_NOT_FOUND`, `E_SURFACE_DELETED`, `E_PENDING_OVERFLOW`, `E_PENDING_EXPIRED`, `E_FUNCTION_NAME_REQUIRED`, `E_FUNCTION_CALL_DUPLICATE`, `E_FUNCTION_RESPONSE_ORPHAN`, `E_FUNCTION_SURFACE_MISMATCH`, `E_FUNCTION_TIMEOUT`

## Debugging Workflow
1. 테스트 fixture(`tests/.../Fixtures`)로 재현 스트림을 만든다.
2. parser 단위 테스트로 parse error 코드/line recovery를 먼저 확정한다.
3. normalizer 테스트로 버전별 필수 필드 검증을 확정한다.
4. controller 테스트로 pending/timeout/correlation 에러를 확정한다.
5. 마지막으로 integration 테스트에서 chunked stream end-to-end를 검증한다.

## Production Integration Checklist (Tizen Runtime Wiring)
- `Runtime` 경계는 `ITizenRuntimeAdapter`로 고정하고, Tizen UI 바인딩은 어댑터 구현체에서만 수행한다.
- 앱 부트 시 `A2uiRuntimePipeline` 하나를 생성하고, 파이프라인 수명주기를 앱 수명주기(`OnCreate`/`OnTerminate`)에 맞춘다.
- 실제 런타임에서는 `RendererBridgeRuntimeAdapter`를 통해 `IRendererBridge`(Tizen UI 스레드 바인딩 구현)와 연결한다.
- 테스트/시뮬레이션 환경에서는 `InMemoryRuntimeAdapter`를 사용해 render/remove operation trace를 검증한다.
- `ParseError`/`ControllerError` 이벤트를 반드시 로깅 파이프라인에 연결하고, `surfaceId`/`functionCallId`를 함께 기록한다.
- UI 렌더 실행은 반드시 Tizen 메인(UI) 스레드에서 처리하고, 백그라운드 입력 스레드와 분리한다.
- `ControllerOptions`는 프로덕션 워크로드 기준으로 조정한다:
  - `PendingTtl`: 생성 전 업데이트 허용 시간
  - `FunctionPendingTtl`: function call 타임아웃 임계값
  - `MaxPendingPerSurface`: surface별 큐 상한
- surface delete 시 잔여 function call이 취소(`E_FUNCTION_CANCELLED`)되는 경로를 운영 모니터링 항목에 포함한다.
- 대용량 스트림에서는 chunk 단위 입력을 유지하고, 파서 상한(`MaxBufferChars`, `MaxJsonCandidateChars`)을 운영 안전값으로 명시한다.
- 배포 전 최소 검증:
  - mixed version/corrupted stream e2e trace 테스트
  - large batch 처리 테스트
  - function cancellation/late response 경로 테스트
