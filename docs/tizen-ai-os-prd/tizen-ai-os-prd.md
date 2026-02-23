# Tizen AI-OS PRD

## 1. 개요 & 배경
* **배경**: Tizen TV는 기존 UI/OS 체계는 안정적이나 에이전트 기반 상호작용/콘텍스트 추적이 부족합니다. OpenClaw의 agent+tool+memory 구조를 가져와서 Tizen을 AI-native 플랫폼으로 재정의합니다.
* **목표**: 에이전트가 TV(리모컨·음성·캔버스·IoT 기기 포함) 전반을 감지하고 조치하며, Tizen의 시스템 서비스/권한 구조에서 자체 에이전트를 실행하는 **AI-OS**를 구현합니다.
* **참고**: OpenClaw PRD와 Claude Cowork(멀티툴 워크플로 + skill/heartbeat)에 기반해 native 영역에 맞춘 agent framework를 설계합니다.

## 2. 비전 & Success Criteria
| 항목 | 목표 |
| --- | --- |
| 에이전트 신뢰성 | 에이전트 처리 실패율 < 1% (Tizen watchdog 기준) |
| 사용자 셋업 | 자주 묻는 질문/조작 중 60% 이상을 AI-에이전트가 직접 해결 |
| 사용자 수용 | 에이전트 행동에 대한 사용자 승인율 75% 이상 |
| 성능 | 메모리/CPU 예산을 기존 Tizen 서비스 대비 15% 이내로 유지 |

## 3. 사용자 & 사용 사례
1. **일반 사용자**: 방송 중 주변 기기 컨텍스트, 스케줄, 퀵 액션을 에이전트가 자동화. 리모컨/음성 → 자연어/액션 → 오버레이 응답.
2. **B2B (숙박·교육)**: AI OS 정책/매크로 모드, 중앙 Admin Console을 통한 skill 배포 및 행위감사.
3. **플랫폼 개발자/파트너**: Tizen agent SDK(OpenClaw tool manifest, memory API) + CLI(`taoctl`)을 이용한 tool 정의 및 테스트.

## 4. Feature Inventory
* **Agent Framework**: OpenClaw 스타일 heartbeat scheduling, agent orchestration, Tizen daemon `tizen-ai-agentd`.
* **Tools**: Input (리모컨/음성), Display Overlay, Device Control (IR/Bluetooth), Cloud Search, Calendar/Reminder, Content Recommender – 각각 Tizen Service API wrapper.
* **Memory & Context**: 로컬 SQLite + vector embeddings(사용자 프리퍼런스 + 방송 메타). 보안 vault와 결합된 점.
* **Workflows**: 명령 → agent pipeline → tool 호출 → fallback + prompt injection 필터.
* **Security**: Tizen key attestation, sandboxed tools, heartbeat 모니터링.
* **Integrations**: Fathom-style 회의/미팅 데이터, OpenClaw cron-heartbeat analog.
* **Analytics**: agent success/failure, intent logs, tool latency, memory usage (OpenClaw memory tables 참고).

## 5. Architecture & Integration
* **Runtime**: `tizen-ai-agentd` 데몬이 heartbeat 기반 agent 시작.
* **Tools Registry**: JSON manifest + `tizen-ai-tools/` (Tizen service wrappers, security policy metadata).
* **Memory Backend**: SQLite/embedded DB + embedding vectors per profile. Sync to secure cloud with hashed keys.
* **Communication Layers**: Local overlay(WebLayer), remote AI (Anthropic/Gemini via secure microservice), developer CLI (`taoctl`).

## 6. Implementation Roadmap
1. **Phase 0 – Foundation (1m)**: Heartbeat scheduler, agent scaffold, logging, overlay prototype.
2. **Phase 1 – Skill Parity (2m)**: OpenClaw tools 포팅(Input, Content Search, Device Control), memory store + tool registry, security gating.
3. **Phase 2 – AI Collaboration (3m)**: Claude/OpenClaw multi-hop planning 서비스, partner console for reviewing logs/actions.
4. **Phase 3 – Productization (2m)**: Performance tuning, OTA QA (Tizen emulator + real hardware), autop-run scheduling.
5. **Phase 4 – Launch & Iterate**: Gradual rollout, usage telemetry, new skills (Home IoT, remote kiosk macros).

## 7. Risks & Mitigations
* **리소스 제약**: CPU/메모리 부족. → heartbeat throttling, queued tool calls, 캐싱.
* **보안/프라이버시**: 음성/콘텐츠 민감. → security manager 통합, encrypted memory store, opt-in gating.
* **신뢰성**: 잘못된 스마트 조치. → action confirmation, audit logs, revert tool, admin override.

## 8. 다음 단계
* Tool manifest + Tizen service API contract 정의 (`docs/agents/tool-manifest.md`).
* GitHub Pages로 PRD/연구 문서 공개, 파트너 피드백 루프.
* PoC agent module 빌드 → `docs/research`에 결과 수집.
