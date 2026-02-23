# Agent Tool Manifest

OpenClaw/Claude 스타일 agent가 Tizen native 서비스와 상호작용하기 위해 tool manifest를 정의합니다. 각 tool은 다음 메타데이터를 포함합니다:

| 필드 | 설명 |
| --- | --- |
| `name` | tool 고유 이름 (예: `device-control`)
| `description` | 사용 목적 설명 |
| `type` | `local` (Tizen System API) 또는 `cloud` (remote AI/API) |
| `inputs` | required parameters (json schema)
| `outputs` | tool 결과 구조
| `security` | 권한 범위, Tizen privilege, sandbox info |
| `heartbeat` | 사용 빈도, timeout, rate limits |

### 예시 도구
1. `device-control` – IR/Bluetooth/CEC 연결을 추상화. `inputs`: `action`, `targetDevice`, `value`. `security`: `privilege=com.samsung.tv.devicecontrol`.
2. `content-search` – Cloud 기반 콘텐츠 추천. `type`: `cloud`. `inputs`: `query`, `context`. `outputs`: `items` list.
3. `overlay-dialog` – Tizen WebLayer 대화/카드 렌더링. `type`: `local`, UI 요소 설정을 포함.

### 운영 방침
* 모든 tool은 manifest 등록 + security gating 후 `tizen-ai-agentd` scheduler에 노출.
* heartbeats에서 사용량 추적 → `docs/assets/tool-usage.csv` 에 기록하여 성능/지연 분석.
