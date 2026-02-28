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
