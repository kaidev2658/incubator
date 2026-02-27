# A2UI Normalizer 구현 가이드 (v0.9 ↔ v0.10)

> 목표: v0.9/v0.10 메시지를 NormalForm으로 통합해 컨트롤러 단일 처리 경로를 확보한다.

## 1) NormalForm 정의

```ts
interface NormalMessage {
  version: "v0.9" | "v0.10";
  type:
    | "CreateSurface"
    | "UpdateComponents"
    | "UpdateDataModel"
    | "DeleteSurface"
    | "CallFunction"
    | "FunctionResponse"
    | "Error";
  surfaceId?: string;
}
```

## 2) 변환 규칙표

| 원본 | NormalForm | 규칙 |
|---|---|---|
| v0.9 createSurface | CreateSurface | 그대로 |
| v0.10 createSurface | CreateSurface | 그대로 |
| v0.9 updateComponents | UpdateComponents | 그대로 |
| v0.10 updateComponents | UpdateComponents | 그대로 |
| v0.9 updateDataModel | UpdateDataModel | undefined/omit 삭제 |
| v0.10 updateDataModel | UpdateDataModel | null 삭제 |
| v0.10 callFunction | CallFunction | functionCallId 필수 |
| v0.10 functionResponse | FunctionResponse | functionCallId/value 필수 |
| v0.9 error | Error | surfaceId 중심 |
| v0.10 error | Error | surfaceId/functionCallId |

## 3) callFunction/functionResponse 처리

- `callFunction` 수신
  1) functionCallId 추적
  2) callableFrom 정책 검사
  3) 실행
  4) `functionResponse` 반환

- timeout 시 `E_FUNCTION_TIMEOUT`
- `wantResponse=false`면 응답 생략 가능

## 4) 삭제 semantics 통합
- v0.9: value omitted/undefined -> deleteOp
- v0.10: value null -> deleteOp

Normalizer는 내부적으로 `deleteOp`로 표준화 후 StateStore에 전달한다.

## 5) 의사코드

```ts
function normalize(raw: any): NormalMessage {
  const version = detectVersion(raw);

  if (raw.createSurface) return mapCreateSurface(raw, version);
  if (raw.updateComponents) return mapUpdateComponents(raw, version);
  if (raw.updateDataModel) return mapUpdateDataModel(raw, version);
  if (raw.deleteSurface) return mapDeleteSurface(raw, version);

  if (raw.callFunction) return mapCallFunction(raw, version);
  if (raw.functionResponse) return mapFunctionResponse(raw, version);
  if (raw.error) return mapError(raw, version);

  throw new NormalizerError("E_UNKNOWN_MESSAGE");
}
```

## 6) 테스트 케이스 (22개)

### 정상 케이스
1. v0.9 createSurface
2. v0.10 createSurface
3. v0.9 updateComponents
4. v0.10 updateComponents
5. v0.9 updateDataModel value set
6. v0.9 updateDataModel value omitted delete
7. v0.9 updateDataModel value undefined delete
8. v0.10 updateDataModel value set
9. v0.10 updateDataModel value null delete
10. v0.9 deleteSurface
11. v0.10 deleteSurface
12. v0.10 callFunction
13. v0.10 functionResponse
14. v0.10 error(surfaceId)
15. v0.10 error(functionCallId)

### 오류 케이스
16. createSurface surfaceId 누락
17. updateComponents surfaceId 누락
18. updateDataModel path 누락
19. callFunction functionCallId 누락
20. functionResponse value 누락
21. version 불일치/누락
22. unknown message key

## 7) 권장 구현 정책
- Normalizer는 stateless
- Validator는 별도 모듈
- 모든 오류를 NormalForm `Error`로 통일
- 버전별 회귀 테스트 분리 운영

## 8) 샘플 NormalForm

```json
{
  "version": "v0.10",
  "type": "CallFunction",
  "functionCallId": "f-123",
  "call": "formatDate",
  "args": {"value": "2026-02-02T15:17:00Z"},
  "returnType": "string",
  "callableFrom": "clientOrRemote"
}
```

## 9) 빠른 구현 체크리스트
- [ ] VersionRouter
- [ ] MessageMapper(v0.9)
- [ ] MessageMapper(v0.10)
- [ ] DeleteSemanticsNormalizer
- [ ] FunctionCallCorrelator
- [ ] ErrorNormalizer
- [ ] 22-case 테스트 셋
