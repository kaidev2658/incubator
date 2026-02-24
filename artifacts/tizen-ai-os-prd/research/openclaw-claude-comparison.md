# OpenClaw vs Claude Cowork

두 플랫폼을 비교하여 Tizen AI-OS 적응 전략을 정리합니다.

| 항목 | OpenClaw | Claude Cowork | Tizen 적용 방안 |
| --- | --- | --- | --- |
| Agent 모델 | Heartbeat + agent orchestration | Toolchain orchestration | `tizen-ai-agentd` heartbeat 기반 모델 통합 |
| Tools | Shared JS skill modules | Tool manifest + skill graph | Tizen native / cloud tool manifest 병렬 지원 |
| Memory | Vector + metadata store | Context windows, dynamic tool history | Local SQLite + cloud sync, Tizen security vault |

다음 과제: Claude의 tool planning + OpenClaw의 heartbeat monitor를 결합한 hybrid scheduler 설계.
