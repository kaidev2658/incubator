# A2UI 아키텍처 상세 분석 (OpenClaw + Tizen Renderer)

> 범위: OpenClaw A2UI(v0.8) + Tizen Renderer 관점의 전체 구조/흐름/데이터 모델/보안 경계

## 1) 시스템 컨텍스트

```mermaid
flowchart LR
  Agent["Agent Runtime\n(LLM + Tools)"] -->|A2UI JSONL| Gateway["OpenClaw Gateway\n(Canvas Host)"]
  Gateway -->|HTTP: /__openclaw__/a2ui/| A2UIHost["A2UI Host (Web)"]
  Gateway -->|WS/Capability URL| Node["Node App\n(iOS/Android/mac)"]
  A2UIHost -->|Render| Canvas["Canvas Panel\n(WebView)"]
  Gateway -->|A2UI Stream| Tizen["Tizen Renderer\n(Embedded)"]
  Agent <--> State["State Store\n(surface/dataModel)"]
```

## 2) 컴포넌트 구조

```mermaid
flowchart TB
  subgraph AgentSide["Agent Side"]
    Tool["canvas.a2ui_push/reset"]
    Planner["Task Planner"]
  end

  subgraph GatewaySide["Gateway"]
    A2UIHost["A2UI Host\n/__openclaw__/a2ui/"]
    Dispatcher["A2UI Dispatcher\n(JSONL stream)"]
    Cap["Capability URL\n(TTL/Session)"]
  end

  subgraph Renderers["Renderers"]
    Canvas["Canvas WebView"]
    Tizen["Tizen Renderer\n(Embedded UI)"]
  end

  subgraph Storage["State Store"]
    Surface["Surface Store"]
    DataModel["Data Model Store"]
  end

  Planner --> Tool --> Dispatcher
  Dispatcher --> A2UIHost
  Dispatcher --> Canvas
  Dispatcher --> Tizen
  Dispatcher --> Surface
  Dispatcher --> DataModel
  Cap --> Canvas
```

## 3) 시퀀스 (초기 렌더/업데이트/리셋/오류복구)

```mermaid
sequenceDiagram
  participant Agent
  participant Gateway
  participant Renderer as Renderer(Canvas/Tizen)

  Agent->>Gateway: surfaceUpdate + beginRendering
  Gateway->>Renderer: stream JSONL (v0.8)
  Renderer-->>Gateway: render OK

  Agent->>Gateway: dataModelUpdate (patch)
  Gateway->>Renderer: apply patches
  Renderer-->>Gateway: updated OK

  Agent->>Gateway: a2ui_reset
  Gateway->>Renderer: reset surface
  Renderer-->>Gateway: reset OK

  alt 오류 발생
    Renderer-->>Gateway: error (line/parse)
    Gateway-->>Agent: error event
    Agent->>Gateway: resend / reset / fallback
  end
```

## 4) 데이터 모델

```mermaid
classDiagram
  class Surface {
    surfaceId: string
    root: componentId
  }
  class Component {
    id: string
    type: enum (Text, Column, Row, ...)
    props: object
    children: list
  }
  class DataModel {
    surfaceId: string
    patches: Patch[]
  }
  class Patch {
    path: string[]
    value: any
  }

  Surface "1" --> "many" Component
  Surface "1" --> "many" DataModel
  DataModel "1" --> "many" Patch
```

핵심은 `surfaceId` 단위 상태 관리다.
- `surfaceUpdate`: 컴포넌트 트리 정의/갱신
- `beginRendering`: 루트 렌더 시작
- `dataModelUpdate`: 패치 기반 부분 업데이트
- `deleteSurface`: surface 제거

## 5) 운영/보안 경계

```mermaid
flowchart LR
  subgraph Trusted["Trusted Zone"]
    Agent
    Gateway
    StateStore["State Store"]
  end
  subgraph SemiTrusted["Semi-trusted"]
    Canvas["Canvas WebView"]
    Tizen["Tizen Renderer"]
  end
  subgraph Untrusted["Untrusted Network"]
    External["External HTTP"]
  end

  Agent --> Gateway
  Gateway --> Canvas
  Gateway --> Tizen
  External -. optional nav .-> Canvas
```

보안 권장사항:
- 기본 loopback 바인딩
- 외부 노출 시 인증/프록시 신뢰 설정
- capability URL TTL/세션 제한
- 입력 JSONL 라인 검증 필수

## 6) 참고
- OpenClaw Canvas/A2UI: https://docs.openclaw.ai/platforms/mac/canvas
- Gateway config reference: https://docs.openclaw.ai/gateway/configuration-reference
