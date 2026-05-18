# Hermes 기술 조사 및 OpenClaw 비교

> 조사 대상 Hermes는 **Nous Research의 `hermes-agent`** 기준입니다. 이름이 같은 다른 프로젝트들이 여럿 있어서, 여기서는 OpenClaw와 직접 비교 가능한 **self-hosted AI agent / messaging gateway 계열**만 다뤘습니다.

## 한 줄 요약

- **Hermes**는 "자가학습형 에이전트" 철학이 강합니다. 메모리, 스킬 생성/개선, 외부 메모리 플러그인, 다양한 실행 백엔드를 중심으로 설계되어 있습니다.
- **OpenClaw**는 "로컬-퍼스트 개인 비서 플랫폼" 성격이 강합니다. Gateway 중심 제어면, 멀티채널, 노드(macOS/iOS/Android), 캔버스, 세션/서브에이전트, 세밀한 도구 정책이 강점입니다.
- 쉽게 말하면:
  - **Hermes** = 에이전트의 학습/적응/연구 확장성 쪽이 더 공격적
  - **OpenClaw** = 개인 비서 운영체제 같은 제품 완성도와 제어면이 더 강함

---

## 1. Hermes란 무엇인가

Hermes Agent는 Nous Research가 만든 오픈소스 AI 에이전트입니다. 공식 문서가 강조하는 핵심 포인트는 다음입니다.

- built-in learning loop
- 경험에서 스킬 생성
- 사용 중 스킬 개선
- 세션 간 persistent memory
- 사용자 모델 심화
- 다중 메시징 플랫폼 + 게이트웨이
- MCP 연동
- 서브에이전트/병렬화
- cron 자동화
- 연구/trajectory/RL 학습 친화성

즉, 단순한 챗봇이나 코딩 보조기가 아니라 **장기적으로 사용자를 학습하며 진화하는 에이전트 런타임**을 지향합니다.

---

## 2. Hermes의 핵심 특징

### 2.1 Self-improving / learning loop

Hermes의 가장 큰 차별점은 여깁니다.

- 작업 경험을 바탕으로 스킬을 만든다고 명시
- 기존 스킬을 사용 중 개선한다고 명시
- 메모리 저장을 agent가 스스로 관리
- 사용자에 대한 모델을 깊게 만든다고 설명

이건 일반적인 "툴 콜 + 메모리" 수준보다 한 단계 더 나간 포지셔닝입니다. 제품 메시지도 거의 여기에 올인되어 있습니다.

### 2.2 메모리 시스템

Hermes 기본 메모리는 `MEMORY.md` + `USER.md` 두 파일 기반입니다.

- bounded memory
- 세션 시작 시 system prompt에 frozen snapshot으로 주입
- agent가 `add / replace / remove` 형태로 관리
- 추가로 session search와 외부 memory provider 사용 가능
- Honcho, Mem0, Hindsight 등 외부 메모리 플러그인 제공

장점은 구조가 단순하고 agent가 "무엇을 오래 기억할지" 직접 큐레이션하기 쉽다는 점입니다.
단점은 기본 메모리 자체는 **작고, 세션 중간에 prompt에 즉시 재반영되지 않는 frozen snapshot 설계**라는 점입니다.

### 2.3 툴/실행 환경

Hermes 문서는 대략 다음을 강조합니다.

- 70+ built-in tools
- 28 toolsets
- terminal backend 7종 이상
- local / Docker / SSH / Daytona / Modal / Singularity / Vercel Sandbox 등 다양한 실행 환경
- browser / web / vision / image / TTS / MCP

즉 **에이전트가 여러 런타임에 걸쳐 일하도록** 설계되어 있습니다. 특히 클라우드/서버리스/원격 실행 쪽 확장성이 상당히 강합니다.

### 2.4 Messaging gateway

Hermes도 gateway 구조를 갖고 있습니다.

- Telegram, Discord, Slack, WhatsApp, Signal, Matrix, Mattermost, Email, SMS 등 20+ 플랫폼
- long-running gateway process
- session key 기반 대화 라우팅
- allowlist / DM pairing
- cron delivery
- cross-session mirroring

즉 CLI 전용 도구가 아니라, **멀티채널 assistant**로 쓰도록 설계돼 있습니다.

### 2.5 연구/학습 친화성

Hermes는 이 부분이 유난히 강합니다.

- batch runner
- trajectory export
- RL training / Atropos integration
- environment framework

이건 OpenClaw보다 훨씬 연구 플랫폼 냄새가 납니다. "개인 비서"이면서 동시에 **agent training substrate** 역할도 하려는 구조입니다.

---

## 3. Hermes 아키텍처

공식 문서 기준 상위 구조는 아래처럼 요약할 수 있습니다.

### 3.1 엔트리 포인트

- CLI
- Gateway
- ACP adapter
- Batch runner
- API server
- Python library

즉 하나의 agent core를 여러 진입면에서 공유합니다.

### 3.2 중심 런타임: `AIAgent`

Hermes 핵심은 `run_agent.py`의 `AIAgent`입니다.

주요 하위 축:
- Prompt Builder
- Provider Resolution
- Tool Dispatch
- Compression & Caching
- Session Storage
- Tool Backends

대화 흐름은 대략 이렇습니다.

1. 입력 수신
2. system prompt 조립
3. provider / model runtime 결정
4. LLM 호출
5. tool call dispatch
6. 필요 시 반복
7. 결과 저장

구조상 **agent-first monolith**에 가깝습니다. 중앙 agent가 대부분을 오케스트레이션하고, 게이트웨이는 그 agent를 호출하는 메시징 표면입니다.

### 3.3 저장소 구조

Hermes 세션/상태 저장은 주로 SQLite + FTS5 기반입니다.

- session persistence
- cross-session recall
- lineage tracking
- full-text search

즉 파일 기반 메모리 + SQLite 기반 세션 검색을 함께 씁니다.

### 3.4 게이트웨이 구조

Hermes gateway는 다음 식입니다.

- platform adapter
- message normalization
- authorization
- session resolution
- AIAgent 실행
- delivery back to channel

중간에 running-agent guard, pairing, slash command dispatch가 붙습니다.

### 3.5 플러그인 구조

Hermes는 플러그인으로 다음을 확장합니다.

- tools
- hooks
- CLI commands
- memory providers
- context engines

특히 memory provider / context engine을 별도 플러그인 축으로 둔 점이 특징입니다.

---

## 4. OpenClaw 아키텍처 요약

OpenClaw는 공식 문서상 **Gateway 중심 control plane**이 더 선명합니다.

### 4.1 상위 구조

- 단일 long-lived Gateway가 채널/세션/툴/이벤트를 소유
- macOS app, CLI, web UI, automations, nodes가 같은 Gateway에 연결
- macOS/iOS/Android/headless node가 WebSocket으로 접속
- Canvas host와 A2UI도 Gateway HTTP surface에서 제공

즉 OpenClaw는 agent 하나보다 **제품 전체 제어면**이 먼저 보입니다.

### 4.2 Agent loop

OpenClaw agent loop는 대략:

1. session resolve
2. context assembly
3. model inference
4. tool execution
5. stream events
6. persistence

특징:
- 세션 단위 직렬화
- lifecycle / assistant / tool event streaming
- write lock 기반 transcript consistency
- compaction/retry
- tool hooks / plugin hooks

즉 실사용 운영 중 세션 충돌, long-running run, transcript consistency 같은 **운영 안정성**에 꽤 신경 쓴 구조입니다.

### 4.3 노드/디바이스 아키텍처

OpenClaw는 이 부분이 꽤 독특합니다.

- macOS/iOS/Android node
- node role로 Gateway에 pairing
- canvas, camera, screen record, talk/voice, location 등 device capability
- personal assistant가 여러 디바이스에 걸쳐 행동하는 구조

Hermes도 멀티플랫폼 메시징은 강하지만, **device node + canvas + voicewake/talk mode**까지 제품적으로 엮어낸 건 OpenClaw 쪽 색이 더 강합니다.

### 4.4 툴/스킬/플러그인 분리

OpenClaw는 구조가 비교적 명확합니다.

- tools = 실제 함수 호출면
- skills = system prompt에 들어가는 절차적 사용 가이드
- plugins = 채널/모델/툴/미디어/TTS 등 확장 패키지

여기에 세션 도구, 서브에이전트, 브라우저, 캔버스, 메시징, 이미지/음악/비디오 생성이 first-class로 들어갑니다.

---

## 5. Hermes vs OpenClaw 비교

## 5.1 제품 철학

### Hermes
- 에이전트가 사용자를 학습하며 점점 유능해진다
- self-improving loop가 정체성의 중심
- 연구/실험/플러그인/메모리 확장에 강한 플랫폼

### OpenClaw
- 로컬-퍼스트 personal assistant control plane
- 채널/세션/노드/캔버스/자동화가 통합된 운영 플랫폼
- 실제 생활/업무 surface를 하나의 게이트웨이 아래 묶는 데 강함

짧게 말하면:
- **Hermes는 agent brain 중심**
- **OpenClaw는 assistant operating surface 중심**

## 5.2 아키텍처 성향

### Hermes
- Python 중심 monolith + plugin registry
- `AIAgent`가 중심
- SQLite/FTS5 기반 세션/검색
- 여러 terminal backend와 연구 파이프라인에 강함

### OpenClaw
- Gateway control plane 중심
- typed WS protocol, nodes, channel routing
- 세션 직렬화/락/이벤트 스트림 등 운영 안정성 강조
- 디바이스/메시징/캔버스 통합 강점

## 5.3 메모리

### Hermes
강점:
- agent-curated memory 철학이 명확
- built-in memory + session search + external providers
- user model, long-term memory 확장 서사가 강함

약점:
- 기본 persistent memory 용량이 작음
- 세션 시작 시 frozen snapshot이라 즉시 반영성이 떨어질 수 있음
- 메모리 체계가 강력하지만, 실제 품질은 provider/curation 품질에 많이 좌우될 가능성

### OpenClaw
강점:
- workspace 파일 기반 기억(`SOUL.md`, `USER.md`, `MEMORY.md`, `memory/*.md`)이 매우 직관적
- memory_search / memory_get로 long-term memory + session transcript 검색 가능
- 사람 입장에서 "어디에 뭐가 저장되는지" 이해가 쉬움

약점:
- Hermes처럼 built-in self-improving memory narrative가 강하진 않음
- 외부 memory provider 생태계/철학은 Hermes 쪽이 더 전면적

## 5.4 툴과 실행 환경

### Hermes
강점:
- 다수의 terminal backend
- cloud/serverless/remote 환경으로 옮기기 쉬움
- MCP 및 연구/배치/trajectory 쪽 확장성 강함

약점:
- 너무 많은 backend/options는 운영 복잡도를 키움
- "학습형 agent" 기능이 강한 만큼 behavior predictability가 떨어질 여지도 있음

### OpenClaw
강점:
- browser, canvas, message, sessions, subagents, media generation 등이 제품적으로 잘 엮여 있음
- 개인 비서 시나리오에서 바로 쓸 수 있는 first-class tool들이 강함
- skill과 plugin 경계가 명확해 운영 이해도가 좋음

약점:
- Hermes만큼 연구용 environment/trajectory/RL 확장 서사는 강하지 않음
- 아주 다양한 원격 terminal backend 실험성은 Hermes 쪽이 더 넓어 보임

## 5.5 메시징/개인비서 운영

### Hermes
- 20+ 메시징 플랫폼 지원
- pairing / allowlist / cron / gateway 제공
- 충분히 강력한 멀티채널 assistant 가능

### OpenClaw
- 멀티채널 + device node + voice + canvas까지 묶여 있음
- Gateway가 사실상 personal assistant OS처럼 작동
- "채팅봇"보다 "내 기기와 채널을 묶는 개인 비서 허브" 느낌이 강함

이 축에서는 **OpenClaw가 좀 더 제품 완성형**, Hermes는 **좀 더 agent framework형**이라고 보는 게 맞습니다.

## 5.6 보안 모델

### Hermes
- dangerous command approval
- hardline unrecoverable blocklist
- allowlist/DM pairing
- container isolation
- MCP credential filtering
- prompt injection/context scanning

상당히 신경 쓴 편입니다. 다만 `--yolo`나 approval off 같은 운영 선택지는 강력한 만큼 위험도 분명합니다.

### OpenClaw
- personal assistant trust boundary를 명시적으로 정의
- hostile multi-tenant boundary가 아님을 문서에서 분명히 밝힘
- sandboxing, tool allow/deny, pairing, gateway auth, node pairing, security audit 제공
- 세션/채널/노드별 정책 설명이 매우 명시적

보안 철학만 보면 OpenClaw 문서는 **"무엇이 보안 경계가 아닌지"까지 꽤 정직하게 써둔 편**이라 좋습니다.

---

## 6. Hermes의 장점

1. **차별화된 self-improving story**
   - 단순히 도구를 쓰는 agent가 아니라, 학습하고 스킬화하고 기억을 정리하는 agent라는 메시지가 분명합니다.

2. **메모리/사용자 모델 확장성**
   - built-in memory + session search + external memory provider 조합이 강합니다.

3. **원격/서버리스/연구 친화성**
   - Modal, Daytona, SSH, Docker 등 여러 실행 백엔드가 강점입니다.

4. **연구 플랫폼으로도 매력적**
   - trajectory, RL, batch runner까지 포함해 학습/평가용 기반으로 좋습니다.

5. **Python 생태계 친화적**
   - 에이전트 연구/실험/내부 확장에 익숙한 팀이면 빠르게 만지기 좋습니다.

---

## 7. Hermes의 단점 / 리스크

1. **개념 밀도가 높다**
   - memory providers, plugins, context engines, multiple runtimes, gateway, batch, ACP 등 축이 많아 운영 복잡도가 빠르게 커질 수 있습니다.

2. **self-improving behavior의 예측 가능성 문제**
   - 스킬 자동 생성/개선이 장점이지만, 기업 운영 환경에서는 change control이 까다로울 수 있습니다.

3. **제품보다는 프레임워크/실험 플랫폼 쪽 느낌이 남아 있음**
   - 매우 강력하지만, "생활형 개인 비서 UX"에서는 OpenClaw가 더 자연스러울 수 있습니다.

4. **기본 메모리 한도는 생각보다 보수적**
   - 화려한 memory story 대비 기본 prompt-injected memory는 작습니다. 결국 session search나 external provider 설계가 중요합니다.

---

## 8. OpenClaw의 장점

1. **Gateway 중심 control plane이 명확함**
   - 채널, 세션, 툴, 노드, 이벤트가 한 구조 안에 잘 정리돼 있습니다.

2. **개인 비서 제품성**
   - Telegram/WhatsApp/Slack 같은 채널뿐 아니라 macOS/iOS/Android node, Canvas, voicewake, talk mode까지 이어집니다.

3. **실운영 안정성 설계가 눈에 띔**
   - 세션 직렬화, write lock, lifecycle event, background subagent, status/tool streaming 등이 탄탄합니다.

4. **스킬/플러그인/툴 경계가 깔끔함**
   - 확장 구조를 이해하고 운영하기가 비교적 쉽습니다.

5. **보안 문서가 현실적임**
   - 어디까지가 trust boundary인지 솔직하게 정의합니다.

---

## 9. OpenClaw의 단점 / 한계

1. **Hermes 같은 self-improving narrative는 약함**
   - 자동 스킬 생성/외부 기억 provider 중심 서사는 Hermes가 더 공격적입니다.

2. **연구/RL/trajectory 플랫폼 색은 상대적으로 약함**
   - agent training substrate로는 Hermes가 더 자연스럽습니다.

3. **개인 비서/운영 surface 중심이라, 순수 연구 프레임워크로 보면 다소 제품 지향적**
   - 장점이자 단점입니다. 연구팀이 "최대한 뜯어고치며 실험"하려면 Hermes가 더 편할 수 있습니다.

---

## 10. 어떤 상황에 뭐가 더 맞나

### Hermes가 더 맞는 경우

- 에이전트의 장기 학습/스킬화가 핵심인 경우
- 외부 메모리 provider 실험을 많이 하고 싶은 경우
- Python 기반 agent research stack과 잘 맞춰야 하는 경우
- batch/trajectory/RL 데이터 생성까지 한 플랫폼에서 하고 싶은 경우
- cloud/serverless backend 다양성이 중요한 경우

### OpenClaw가 더 맞는 경우

- 실제 개인 비서/운영 assistant를 바로 굴리고 싶은 경우
- 멀티채널 + 기기 + 음성 + 캔버스까지 하나로 엮고 싶은 경우
- 메시징/디바이스 중심 assistant UX가 중요한 경우
- 세션/채널/권한/게이트웨이 운영을 안정적으로 다루고 싶은 경우
- 로컬-퍼스트 personal assistant control plane이 필요한 경우

---

## 11. 제 결론

제 느낌으로는 이렇습니다.

- **Hermes는 더 야심찬 agent brain**입니다.
  - 학습한다
  - 스킬을 만든다
  - 메모리를 다듬는다
  - 연구 파이프라인까지 간다

- **OpenClaw는 더 완성된 assistant operating system**에 가깝습니다.
  - 채널을 묶고
  - 디바이스를 붙이고
  - 세션을 관리하고
  - 실제 생활/업무 인터페이스로 작동합니다.

만약 관심사가
- "에이전트가 스스로 더 똑똑해지는 구조"를 깊게 파고드는 것이라면 **Hermes 연구 가치가 큽니다**.
- "채널/기기/자동화를 묶는 실전 개인 비서"가 목적이라면 **OpenClaw가 더 바로 쓸 만합니다**.

제 추천은 한 줄로:

> **제품 운영은 OpenClaw, 학습형 에이전트 연구는 Hermes가 더 매력적**입니다.

둘 중 하나가 완전히 우위라기보다, **무게중심이 다릅니다**. OpenClaw는 운영면이 강하고, Hermes는 적응/연구면이 강합니다.

---

## 12. 빠른 비교표

| 항목 | Hermes | OpenClaw |
|---|---|---|
| 정체성 | self-improving agent | local-first personal assistant platform |
| 중심 구조 | `AIAgent` 중심 | Gateway control plane 중심 |
| 메모리 철학 | bounded memory + session search + external providers | workspace memory files + memory search + session recall |
| 메시징 | 20+ 플랫폼 gateway | 20+ 플랫폼 + nodes + canvas + voice |
| 실행 환경 | local/Docker/SSH/Modal/Daytona 등 다양 | host/gateway/node + sandbox + device nodes |
| 서브에이전트 | 지원 | first-class, push-based completion |
| 연구/RL | 강함 | 상대적으로 약함 |
| 제품 UX | framework 성향 강함 | assistant product 성향 강함 |
| 보안 모델 | approvals + sandbox + pairing + scanning | auth + sandbox + tool policy + node/gateway trust model |
| 추천 용도 | 학습형 agent 연구/실험 | 실전 personal assistant 운영 |

---

## 13. 참고 소스

### Hermes 공식 자료
- Docs home: https://hermes-agent.nousresearch.com/docs/
- Architecture: https://hermes-agent.nousresearch.com/docs/developer-guide/architecture
- Persistent Memory: https://hermes-agent.nousresearch.com/docs/user-guide/features/memory
- Security: https://hermes-agent.nousresearch.com/docs/user-guide/security
- Gateway Internals: https://hermes-agent.nousresearch.com/docs/developer-guide/gateway-internals
- GitHub: https://github.com/NousResearch/hermes-agent

### OpenClaw 공식 자료
- Local README: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/README.md`
- Architecture: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/concepts/architecture.md`
- Agent Loop: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/concepts/agent-loop.md`
- Security: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/gateway/security/index.md`
- Tools overview: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/tools/index.md`
- Subagents: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/tools/subagents.md`

---

## 14. 다음 단계 제안

원하시면 바로 이어서 아래 3개 중 하나 해드릴 수 있습니다.

1. **더 깊은 비교**
   - memory 구조만 따로 깊게
   - gateway 구조만 따로 깊게
   - 보안 모델만 따로 깊게

2. **실전 관점 비교**
   - "현재 개인 비서 워크플로 기준으로 Hermes가 OpenClaw를 대체 가능한가?"
   - "둘을 혼합해서 쓰는 구조가 가능한가?"

3. **구현 관점 비교**
   - repo 구조, 확장 포인트, plugin/skill 시스템, 운영 난이도까지 더 기술적으로 파고들기
