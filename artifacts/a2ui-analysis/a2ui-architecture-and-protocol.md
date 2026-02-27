# A2UI 아키텍처 및 프로토콜 상세 분석

## 1. 개요
A2UI는 에이전트가 UI를 구조적으로 생성/갱신하기 위한 프로토콜 계층이다. OpenClaw에서는 Canvas와 결합되어 동작하며, 에이전트가 전송한 JSONL 메시지를 Canvas가 렌더링한다. 브라우저 조작형(Computer Use) 접근과 달리, A2UI는 “UI 상태를 선언적으로 전달”한다는 점에서 결정성과 재현성이 높다.

## 2. 컴포넌트 아키텍처
- **Agent**: `canvas.a2ui_push`/`canvas.a2ui_reset` 호출 주체
- **Gateway**: A2UI Host (`/__openclaw__/a2ui/`) 및 Canvas 엔드포인트 제공
- **Canvas (Node/WebView)**: A2UI 메시지를 수신·렌더링하는 UI 런타임

흐름 요약:
1) Gateway가 A2UI 호스트를 노출
2) Canvas가 해당 호스트 로드
3) Agent가 JSONL 메시지 전송
4) Canvas가 surface/data model 반영

## 3. 메시지 모델(v0.8)
OpenClaw 문서 기준으로 Canvas는 A2UI v0.8 메시지를 수용한다.

핵심 메시지:
- `beginRendering`
- `surfaceUpdate`
- `dataModelUpdate`
- `deleteSurface`

주의:
- v0.9의 `createSurface`는 미지원(거부될 수 있음)
- JSONL은 라인 단위 유효성 검사를 통과해야 함

## 4. 상태 모델
A2UI는 `surfaceId` 단위로 상태를 관리한다.
- `surfaceUpdate`: 컴포넌트 트리 정의/갱신
- `beginRendering`: 특정 surface의 루트 렌더링 시작
- `dataModelUpdate`: 데이터 바인딩/상태 갱신
- `deleteSurface`: surface 제거

실무적으로는 “초기 surface 구성 → dataModel 부분 갱신” 패턴이 안정적이다.

## 5. 조작형 UI 자동화와의 비교
### A2UI(생성형 UI)
- 장점: 결정성, 재현성, 감사 용이성
- 단점: 프로토콜/버전 제약, 컴포넌트 모델 의존

### Computer Use(조작형)
- 장점: 기존 웹/앱에 즉시 적용 가능
- 단점: 비결정성, UI 변경 취약성, 리플레이 난이도

결론: 내부 업무 자동화(정형 워크플로우)는 A2UI가 구조적으로 유리하다.

## 6. 참고
- OpenClaw Canvas/A2UI: https://docs.openclaw.ai/platforms/mac/canvas
- OpenClaw Gateway config reference: https://docs.openclaw.ai/gateway/configuration-reference
