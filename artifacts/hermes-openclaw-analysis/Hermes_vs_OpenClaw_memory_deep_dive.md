# Hermes vs OpenClaw 메모리 구조 딥다이브

> 대상 Hermes는 **Nous Research `hermes-agent`** 기준입니다.

## 결론 먼저

- **Hermes 메모리**는 더 "agent-native"합니다.
  - 에이전트가 직접 기억을 큐레이션하고
  - 필요하면 외부 memory provider를 붙이고
  - session search와 user modeling까지 확장합니다.
- **OpenClaw 메모리**는 더 "workspace-native"합니다.
  - 사람이 읽고 편집하기 쉬운 파일이 중심이고
  - memory search는 그 파일과 세션 히스토리를 검색하는 식입니다.
- 짧게 말하면:
  - **Hermes** = 기억 시스템을 적극적으로 진화시키는 구조
  - **OpenClaw** = 기억을 파일/검색/지식 레이어로 투명하게 운영하는 구조

---

## 1. 메모리 철학 차이

### Hermes
Hermes는 메모리를 에이전트의 핵심 정체성으로 다룹니다.

- `MEMORY.md` + `USER.md`를 system prompt에 주입
- agent가 `add / replace / remove`로 직접 관리
- session search로 과거 대화까지 탐색
- 외부 memory provider로 semantic memory / user modeling / graph형 기억 확장
- 문서 메시지 자체가 "The agent that grows with you"에 맞춰져 있음

즉 **기억이 agent behavior evolution의 일부**입니다.

### OpenClaw
OpenClaw는 메모리를 오히려 더 소박하게 정의합니다.

- 숨겨진 기억은 없고, 디스크에 쓴 것만 기억한다
- `MEMORY.md`, `memory/YYYY-MM-DD.md`, 필요시 `DREAMS.md`
- 기억은 plain markdown files
- 검색은 `memory_search`, 읽기는 `memory_get`
- 장기기억으로 올릴지 여부를 compaction 전 flush / dreaming으로 관리 가능

즉 **기억이 workspace 운영체계의 일부**입니다.

이 차이가 꽤 큽니다.
- Hermes는 메모리를 agent capability의 전면에 둡니다.
- OpenClaw는 메모리를 audit 가능한 파일 기반 state로 둡니다.

---

## 2. 기본 메모리 구조 비교

## 2.1 Hermes 기본 구조

Hermes 공식 문서 기준 기본 persistent memory는 두 파일입니다.

- `MEMORY.md`
  - 환경 사실, 규칙, 교훈, 프로젝트 관례
- `USER.md`
  - 사용자 성향, 선호, 커뮤니케이션 스타일

특징:
- 둘 다 char limit가 엄격함
  - `MEMORY.md`: 2,200 chars
  - `USER.md`: 1,375 chars
- session start 시 frozen snapshot으로 system prompt에 삽입
- 세션 도중 메모리를 바꿔도 prompt 내용은 즉시 갱신되지 않음
- 대신 tool 응답은 live state를 보여줌

이 설계의 의미:
- prompt cache 안정성에는 유리
- 하지만 긴 세션에서 freshly learned fact를 즉시 중심 맥락으로 반영하는 데는 약간 둔함

## 2.2 OpenClaw 기본 구조

OpenClaw 기본 메모리 구조는 더 넓습니다.

- `MEMORY.md`
  - 장기기억, 결정, 선호, 지속 맥락
- `memory/YYYY-MM-DD.md`
  - 일일 노트, 최근 맥락
- `DREAMS.md` (optional)
  - 백그라운드 메모리 정리/승격 결과

특징:
- 파일 기반이라 사람이 직접 보고 수정하기 쉬움
- 메인 세션에서는 `MEMORY.md`를 읽고
- 오늘/어제 daily note를 함께 읽음
- 기억은 검색 기반으로도 다시 불러올 수 있음

의미:
- 기본 persistent store가 Hermes보다 더 "운영 일지"에 가깝습니다.
- 장기/단기/검토 계층이 파일로 분리돼 있어 추적성이 좋습니다.

---

## 3. 검색/회상 구조 비교

## 3.1 Hermes

Hermes는 기본 memory 외에 **session search**를 강하게 밀고 있습니다.

- 모든 CLI / messaging session이 SQLite에 저장
- FTS5 full-text search 사용
- 과거 대화를 찾아 recall 가능
- 메모리와 세션 검색을 분리
  - memory = 항상 들고 가야 할 사실
  - session search = 과거에 논의했던 구체 사항 찾기

장점:
- 구조가 명확함
- 기억을 prompt 상주 영역과 아카이브 영역으로 분리함
- 대화 로그가 크더라도 memory를 억지로 비대화하지 않아도 됨

단점:
- 메모리와 세션 검색 사이 오케스트레이션 품질이 중요함
- 실전에서는 provider/tool 호출 품질이 recall 품질에 영향 큼

## 3.2 OpenClaw

OpenClaw는 `memory_search`가 더 넓은 범위를 대상으로 합니다.

- `MEMORY.md`
- `memory/*.md`
- indexed session transcripts
- 필요시 wiki 보조 corpus

특징:
- hybrid search(semantic + keyword)
- 검색 후 `memory_get`으로 필요한 줄만 정밀 조회
- workspace memory와 session transcript가 같은 recall 흐름 안에 들어옴

장점:
- 사용자가 "메모리 파일"과 "과거 대화"를 별개 도구로 덜 의식해도 됨
- 사실상 단일 recall 인터페이스처럼 쓸 수 있음

단점:
- 설계 개념상 Hermes처럼 구획이 선명하진 않음
- 잘못 쓰면 무엇이 durable memory고 무엇이 transcript hit인지 흐려질 수 있음

---

## 4. 메모리 업데이트 방식 비교

## 4.1 Hermes: tool-managed curation

Hermes는 메모리 업데이트가 더 명시적입니다.

- add
- replace
- remove

즉 에이전트가 메모리를 하나의 작은 데이터셋처럼 다룹니다.

좋은 점:
- 메모리 용량을 강제로 작게 유지함
- 에이전트가 "뭘 남길지" 고민하도록 유도함
- user profile과 environment memory가 분리됨

아쉬운 점:
- 메모리 예산이 너무 작으면 과도한 압축/합치기가 잦아질 수 있음
- 자주 바뀌는 운영 규칙/워크플로를 담다 보면 큐레이션 부담이 커질 수 있음

## 4.2 OpenClaw: file-first memory maintenance

OpenClaw는 파일 자체가 1차 진실원천입니다.

- agent가 파일에 쓴다
- 사람이 파일을 읽는다
- `memory_search`가 그 파일들을 인덱싱한다
- compaction 전 memory flush가 중요 정보를 저장하게 돕는다
- dreaming은 short-term signal을 장기기억으로 승격하는 보조 장치다

좋은 점:
- 사람과 agent가 같은 기억 표면을 봄
- 장기기억과 최근 메모가 파일로 남아 디버깅이 쉬움
- memory wiki까지 붙이면 knowledge-base화 가능

아쉬운 점:
- 잘 관리하지 않으면 markdown memory가 점점 누적 문서가 될 수 있음
- 구조화가 덜 강제되므로 운영 습관에 따라 품질 편차가 날 수 있음

---

## 5. 확장성 비교

## 5.1 Hermes 확장성

Hermes는 외부 memory provider가 핵심 강점입니다.

문서상 예시:
- Honcho
- OpenViking
- Mem0
- Hindsight
- Holographic
- RetainDB
- ByteRover
- Supermemory

이건 단순 플러그인 개수 문제가 아니라,
**"기억을 외부 전문 시스템으로 위임할 수 있다"**는 의미입니다.

가능해지는 것:
- semantic profile building
- graph memory
- long-horizon user modeling
- cross-session latent preference extraction

즉 대형 memory R&D를 하기엔 Hermes가 확실히 더 흥미롭습니다.

## 5.2 OpenClaw 확장성

OpenClaw도 메모리 백엔드/보조 레이어가 있습니다.

- builtin SQLite memory
- QMD sidecar
- Honcho plugin
- LanceDB plugin
- memory-wiki

다만 포지셔닝은 조금 다릅니다.
- Hermes는 memory evolution이 제품 정체성 중앙에 있고
- OpenClaw는 memory를 **assistant 운영의 투명한 기반 시설**처럼 다룹니다.

특히 `memory-wiki`는 인상적입니다.
- deterministic page structure
- claim/evidence
- contradiction/freshness tracking
- dashboards
- compiled digests

즉 OpenClaw는 그냥 기억 저장소를 넘어서 **검증 가능한 지식 레이어**로 갈 수 있습니다.
이건 enterprise/ops 문맥에서는 꽤 강점입니다.

---

## 6. 아키텍처적 장단점

## Hermes 메모리 장점

1. **에이전트 적응성에 최적화**
2. **session search와 durable memory 역할 분리가 선명**
3. **external providers로 공격적 확장 가능**
4. **user modeling 서사가 강함**

## Hermes 메모리 단점

1. **기본 memory budget가 작음**
2. **frozen snapshot은 장기 세션 적응성에 약간 불리**
3. **실전 품질이 provider ecosystem과 curation 품질에 좌우될 수 있음**
4. **상태가 분산되면 인간 운영자가 전체 기억 지형을 직관적으로 보기 어려울 수 있음**

## OpenClaw 메모리 장점

1. **plain markdown 중심이라 투명함**
2. **사람이 직접 inspection/edit하기 매우 쉬움**
3. **memory_search가 파일 + 세션을 같이 엮어 recall하기 좋음**
4. **dreaming / wiki로 점진적 구조화 가능**

## OpenClaw 메모리 단점

1. **초기 상태에선 Hermes보다 더 수공예적**
2. **자동 user modeling 서사는 약함**
3. **규율 없이 쓰면 파일형 메모리가 커지고 중복되기 쉬움**
4. **지능형 메모리 behavior보다 운영 명시성이 우선이라, '알아서 학습' 느낌은 덜함**

---

## 7. 어떤 메모리 구조가 어떤 상황에 맞나

### Hermes가 유리한 경우

- agent가 사용자/환경을 점점 더 잘 학습해야 함
- long-term personalization이 핵심 기능임
- 외부 memory system을 붙여 실험해야 함
- 연구적으로 "기억이 agent behavior를 어떻게 바꾸는가"를 보고 싶음

### OpenClaw가 유리한 경우

- 인간 운영자가 기억을 쉽게 감사/수정해야 함
- 메모리가 실험 대상보다 운영 자산에 가까움
- 파일/문서 기반 워크플로와 잘 연결돼야 함
- durable memory를 knowledge wiki처럼 굴리고 싶음

---

## 8. 제 판단

메모리만 떼어 놓고 보면:

- **Hermes가 더 야심차고 AI-native**합니다.
- **OpenClaw가 더 투명하고 운영 친화적**입니다.

제가 강하게 느끼는 차이는 이겁니다.

> Hermes는 "에이전트가 무엇을 기억하며 어떻게 더 똑똑해질까"에 집중하고,
> OpenClaw는 "그 기억을 사람이 어떻게 믿고 운영할까"에 더 강합니다.

그래서
- **agent learning 연구**에는 Hermes 쪽 메모리가 더 흥미롭고,
- **실전 개인비서/운영체계**에는 OpenClaw 메모리가 더 안심됩니다.

---

## 9. 빠른 비교표

| 항목 | Hermes | OpenClaw |
|---|---|---|
| 기본 저장소 | `MEMORY.md` + `USER.md` | `MEMORY.md` + `memory/YYYY-MM-DD.md` + optional `DREAMS.md` |
| 메모리 크기 정책 | 엄격한 char limit | 파일 기반, 상대적으로 유연 |
| 세션 중 반영 | persisted but prompt snapshot frozen | 파일 저장 + 검색/재조회 기반 |
| 과거 대화 회상 | session search 분리 | memory_search로 memory + sessions 통합 조회 |
| 외부 provider | 매우 공격적 | 있음, 하지만 더 infra-like |
| 사용자 모델링 | 강함 | 상대적으로 약함 |
| 인간 감사 가능성 | 중간 | 매우 높음 |
| 지식베이스화 | provider 의존 | memory-wiki로 강함 |
| 적합한 방향 | adaptive agent memory | auditable assistant memory |

---

## 10. 참고 소스

### Hermes
- Docs: https://hermes-agent.nousresearch.com/docs/
- Memory: https://hermes-agent.nousresearch.com/docs/user-guide/features/memory
- Architecture: https://hermes-agent.nousresearch.com/docs/developer-guide/architecture

### OpenClaw
- Memory overview: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/concepts/memory.md`
- Architecture: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/concepts/architecture.md`
- Agent loop: `/Users/clawdev/.nvm/versions/node/v24.13.1/lib/node_modules/openclaw/docs/concepts/agent-loop.md`
