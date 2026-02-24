# OpenClaw Overview (DeepWiki Notes)

관련 자료: https://deepwiki.com/openclaw/openclaw

## 핵심 아키텍처
- **Gateway & Agent 분리**: Gateway가 모델/세션/채널을 라우팅하고, 각 에이전트는 자신만의 workspace/agentDir/session스토어를 갖고 작동한다. 이 구조 덕분에 여러 persona를 하나의 인스턴스에서 운영할 수 있다.
- **Configuration System**: `~/.openclaw/openclaw.json`을 루트로 하며 agents.list/bindings/channels/tools 등으로 구성한다. multi-agent, gateway, channel, nodes, plugins, cron 등 모든 동작이 단일 파일에서 조율된다.

## 실행 흐름
- **Agent Execution Flow**: inbound 메시지를 Gateway가 수신 → routing 바인딩을 평가 → 해당 agent workspace에서 run → tool/skill/exec가 허용된 상태로 결과를 생성 → session 상태 업데이트 → 필요시 tools.agentToAgent를 통해 다른 agent와 교신.
- **Session & Memory**: 세션은 `agent:<agentId>:...` 키로 구분되며 heartbeat/cron/system event로 리셋/정리된다. memory는 기본 `memory/` + agent 별 long-term `MEMORY.md`로 구성.

## Tools & Skills
- **Tools**: read/edit/write/exec/process 등 node 기능과 cron/tools.agentToAgent/stat 등 다양한 CLI 툴이 tool policy로 제어된다. Sandbox/Tool Policy/Tool Groups를 통해 각 agent에 허용된 도구를 제한.
- **Skills**: agent workspace에 `skills/` 폴더를 두거나 shared `~/.openclaw/skills`를 이용해 agent가 구동 중 호출할 수 있다.

## Channels & Routing
- 각 채널(telegram, whatsapp, discord 등)은 `channels.<channel>`로 정의되고 직렬화된 바인딩(binding.match)을 통해 agent와 연결된다. 특정 peer/group/guild, accountId, teamId로 agent 매핑이 가능.
- **Routing Order**: peer -> parent -> guild+roles -> guild -> team -> account -> channel -> default. 바인딩 순서가 중요.

## Automation & Operations
- **Cron + Heartbeat**: Cron jobs와 Heartbeat(즉시/예약)로 agent를 깨어나게 할 수 있으며, `daily-news-digest`처럼 정기 job을 등록하여 인사이트 파이프라인을 자동화.
- **High-level commands**: `openclaw agent`, `openclaw message`, `openclaw sessions_send`, `openclaw nodes` 등 CLI가 각 agent/session에 직접 명령을 보낸다.

## 적용 방향
- Tizen AI-OS PRD에서는 multi-agent/workflow, tool policy, channel routing 구성도를 명시하여 OpenClaw 스타일 multi-workspace 구조를 반영할 것.
- Daily Tech News와 신규 artifacts는 `artifacts/tizen-ai-os-prd/research/` 아래에 `openclaw-knowledge.md` 같은 문서로 정리하여 온보딩/참조용으로 활용한다.
