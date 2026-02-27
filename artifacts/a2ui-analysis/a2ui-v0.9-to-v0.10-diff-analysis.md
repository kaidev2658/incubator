# A2UI v0.9 → v0.10 비교 분석

작성일: 2026-02-27

## 1) v0.10 상태/범위
- v0.10은 현재 **in development** 상태.
- `evolution_guide.md`가 TBD로 남아 있어, 공식 변경요약은 미완.

근거:
- https://github.com/google/A2UI/blob/main/specification/v0_10/README.md
- https://github.com/google/A2UI/blob/main/specification/v0_10/docs/evolution_guide.md

## 2) v0.9 대비 변경점

### 확정(스키마/문서 기반)
1. 서버→클라이언트 메시지에 `callFunction` 추가
   - `functionCallId`, `wantResponse`, `callFunction`
2. 클라이언트→서버 메시지에 `functionResponse` 추가
3. error 메시지에 `surfaceId`/`functionCallId` 연계 확장
4. `FunctionCall` 타입 확장
   - `callableFrom` 추가
   - `returnType`에서 `any` 제거
   - `CallId` 타입 추가
5. dataModel 삭제 의미 변경
   - v0.10: `null`일 때 삭제

근거 경로:
- `specification/v0_10/json/server_to_client.json`
- `specification/v0_10/json/client_to_server.json`
- `specification/v0_10/json/common_types.json`
- `specification/v0_10/docs/a2ui_protocol.md`

### 추정(공식 가이드 미완)
- v0.10의 전체 진화 방향은 함수호출 왕복(call/response) 강화로 보이나,
  evolution guide가 TBD 상태이므로 확정 단정은 지양.

## 3) 브레이킹 가능 포인트
- v0.9 렌더러는 `callFunction`/`functionResponse` 미지원 시 파싱·처리 실패 가능
- `returnType=any` 의존 구현은 v0.10에서 스키마 불일치
- dataModel 삭제 semantics 차이로 상태 불일치 가능

## 4) OpenClaw(v0.8) 영향
OpenClaw는 현재 v0.8 수용, v0.9 `createSurface` 미지원.
따라서 v0.10 기능은 직접 연동 불가하며 변환 레이어 필요.

매핑 예시:
- `createSurface` -> `surfaceUpdate + beginRendering`
- `updateComponents` -> `surfaceUpdate`
- `updateDataModel` -> `dataModelUpdate`
- `callFunction/functionResponse` -> 별도 액션/툴 채널로 우회

근거:
- https://docs.openclaw.ai/platforms/mac/canvas

## 5) Tizen Renderer 설계 영향
- `callFunction`/`functionResponse` 왕복 지원 필요
- `functionCallId` 매칭/타임아웃/재시도 정책 필요
- `callableFrom`(clientOnly/remoteOnly/clientOrRemote) 실행 정책 분리 필요
- dataModel null-delete semantics 반영 필요

## 6) 권장 마이그레이션 전략
1. 버전 감지 + 디스패처 분기(v0.9/v0.10)
2. dataModel 삭제 규칙을 버전별로 분리
3. function call 경로를 렌더러 코어에서 독립 모듈화
4. OpenClaw 연동용 다운캐스트(v0.10→v0.8) 유지
5. 회귀 테스트 세트 분리(v0.9 goldens / v0.10 function-call cases)

## 참고 링크
- v0.10 README: https://github.com/google/A2UI/blob/main/specification/v0_10/README.md
- v0.10 evolution guide: https://github.com/google/A2UI/blob/main/specification/v0_10/docs/evolution_guide.md
- OpenClaw Canvas docs: https://docs.openclaw.ai/platforms/mac/canvas
