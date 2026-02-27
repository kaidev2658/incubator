# A2UI 기능/구조 분석 및 최신 동향 리포트

작성일: 2026-02-27

## 1) 개요
A2UI는 에이전트가 UI를 직접 생성·갱신할 수 있도록 설계된 구조적 UI 프로토콜이다. OpenClaw에서는 Canvas와 결합해 에이전트가 `a2ui_push` 메시지(JSONL)를 전달하면, Canvas가 이를 렌더링하는 방식으로 동작한다. 이는 기존의 브라우저 조작형(Computer Use) 접근과 달리, UI를 “직접 조립”한다는 점에서 결정성과 재현성이 높다.

## 2) OpenClaw 기준 기능 구조
- **호스팅 계층**: Gateway가 A2UI 호스트를 제공 (`/__openclaw__/a2ui/` 경로)
- **렌더링 계층**: Canvas 패널이 A2UI 호스트를 로드해 UI 표시
- **프로토콜 계층**: 현재 문서 기준 A2UI v0.8 흐름 중심
- **도구 계층**:
  - `canvas.a2ui_push`: JSONL/UI 이벤트 전송
  - `canvas.a2ui_reset`: 상태 초기화

요약하면, `Agent -> Gateway(Canvas/A2UI host) -> Canvas render` 구조다.

## 3) 내부 동작 흐름
1. Gateway가 Canvas/A2UI 호스트를 제공한다.
2. 노드/클라이언트가 Canvas를 열고 A2UI 호스트에 접속한다.
3. 에이전트가 `canvas.a2ui_push`로 UI 이벤트(JSONL)를 전송한다.
4. Canvas가 이벤트를 반영해 surface/data model을 갱신한다.
5. 필요 시 `a2ui_reset`으로 초기화한다.

## 4) 장점·한계·리스크
### 장점
- **결정성**: 직접 UI 조작보다 재현성이 높음
- **테스트 용이성**: 이벤트 기반이라 상태 추적이 명확
- **업무형 UI 자동화 적합**: 폼/대시보드/워크플로우 구성에 유리

### 한계/리스크
- 프로토콜 버전 호환성 관리 필요(v0.8 중심)
- Gateway 노출/권한 설정 미흡 시 보안 리스크 증가
- 임의 UI 렌더링 통로는 정책/검증(입력 스키마, 허용 컴포넌트) 필수

## 5) 최신 동향 (2024~2026)
- **Computer Use 확산**: Anthropic 컴퓨터 유즈 툴 등 UI 조작형 에이전트가 빠르게 상용화
- **브라우저 에이전트 제품화**: OpenAI Operator/Agent 계열이 브라우저 기반 업무 자동화 시장을 가속
- **멀티모달 실시간 상호작용**: Google DeepMind Project Astra 등 화면/음성/상황 인지를 결합한 비서형 인터페이스 강화
- **평가 표준화**: OSWorld 등 실제 컴퓨터 환경 벤치마크가 중요 기준으로 자리잡는 중

핵심 추세는 "UI 조작형"과 "UI 생성형(A2UI)"의 병행 발전이며, 엔터프라이즈 업무 자동화에서는 통제성과 감사가능성이 높은 생성형 UI 브리지가 선호될 가능성이 크다.

## 6) 전략 제안
1. **A2UI 템플릿화**: 반복 업무(리포트, 승인, 체크리스트) UI 컴포넌트 표준화
2. **보안 우선 정책**: gateway/auth/노출 범위/입력 검증 정책 기본 적용
3. **하이브리드 구조**: 정형 업무는 A2UI, 비정형 웹업무는 Computer Use 보조
4. **관측성 강화**: push 이벤트/렌더링 실패/사용자 액션 로그를 운영 지표화
5. **버전 추상화**: 프로토콜 업그레이드(v0.9+) 대비 변환 레이어 준비

## 7) 참고 소스
- OpenClaw Canvas 문서: https://docs.openclaw.ai/platforms/mac/canvas
- OpenClaw Gateway 구성 참고: https://docs.openclaw.ai/gateway/configuration-reference
- Anthropic Computer Use Tool: https://platform.claude.com/docs/en/agents-and-tools/tool-use/computer-use-tool
- OpenAI Operator: https://openai.com/index/introducing-operator/
- Google DeepMind Project Astra: https://deepmind.google/models/project-astra/
- OSWorld: https://os-world.github.io/
- OSWorld 논문: https://arxiv.org/abs/2404.07972
