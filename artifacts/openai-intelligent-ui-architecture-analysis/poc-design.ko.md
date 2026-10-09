# Intelligent UI Runtime PoC 설계

## 1. 목표

이 PoC의 목표는 OpenAI Intelligent UI와 유사한 구조를 작은 범위에서 검증하는 것이다. 목표는 완전한 ChatGPT UI 복제가 아니라, generated UI를 안전하게 native-like component로 렌더링하는 최소 runtime을 만드는 것이다.

검증할 질문:

- 모델 또는 서버가 제한된 UI IR을 만들면 native renderer가 안전하게 그릴 수 있는가?
- local state와 interaction을 모델 재호출 없이 처리할 수 있는가?
- invalid component/prop을 fallback할 수 있는가?
- streaming update를 나중에 붙일 수 있는 구조인가?

## 2. Non-goals

초기 PoC에서 하지 않을 것:

- arbitrary JavaScript 실행
- raw HTML/CSS 렌더링
- AppBlock 구현
- 외부 API/tool action 실행
- 완전한 layout engine
- production-grade accessibility
- model training 또는 fine-tuning

## 3. 시스템 구성

```text
sample prompt/result
  -> UI IR JSON
  -> parser
  -> schema validator
  -> normalizer
  -> runtime state store
  -> renderer adapter
  -> demo UI surface
```

## 4. UI IR 예시

```json
{
  "version": "0.1",
  "state": {
    "seats": 8
  },
  "root": {
    "type": "box",
    "props": {
      "padding": 3,
      "gap": 2,
      "border": true
    },
    "children": [
      {
        "type": "title",
        "props": { "size": "lg" },
        "text": "Team plan estimate"
      },
      {
        "type": "text",
        "text": "Drag the slider to see the monthly price for your team."
      },
      {
        "type": "slider",
        "props": {
          "min": 1,
          "max": 50,
          "value": { "state": "seats" },
          "onChange": { "setState": "seats" }
        }
      },
      {
        "type": "stat",
        "props": {
          "label": "Monthly price",
          "value": {
            "expr": {
              "op": "mul",
              "args": [{ "state": "seats" }, 29]
            }
          },
          "prefix": "$",
          "suffix": "/mo"
        }
      }
    ]
  },
  "fallbackMarkdown": "Team plan estimate: adjust seats to estimate monthly price."
}
```

## 5. Component catalog v0.1

| Component | Props | Notes |
| --- | --- | --- |
| `box` | `padding`, `gap`, `border`, `direction` | layout container |
| `title` | `size` | text heading |
| `text` | none | plain text |
| `button` | `label`, `onPress` | local event only |
| `slider` | `min`, `max`, `value`, `onChange` | numeric state |
| `checkbox` | `checked`, `onChange`, `label` | boolean state |
| `stat` | `label`, `value`, `prefix`, `suffix` | derived value display |
| `table` | `columns`, `rows` | static only in v0.1 |

## 6. Expression model

초기 PoC는 문자열 expression을 실행하지 않는다. 대신 작은 AST만 허용한다.

```json
{
  "op": "add",
  "args": [{ "state": "a" }, { "state": "b" }]
}
```

지원 연산:

- `add`
- `sub`
- `mul`
- `div`
- `round`
- `min`
- `max`
- `concat`

이 방식은 JavaScript runtime 없이도 native에서 안전하게 계산할 수 있다.

## 7. Validation rules

```text
validateDocument(doc)
  check version
  check root exists
  validateNode(root)
  validateStateReferences(root, state)
  validateExpressionAst(root)
  collect diagnostics
```

정책:

- unknown component: fallback node로 대체
- unknown prop: 제거
- invalid prop type: 기본값 적용 또는 제거
- invalid expression: null 표시
- unsafe action: 제거

## 8. Runtime state

```text
StateStore:
  get(key)
  set(key, value)
  subscribe(key, callback)
```

event 처리:

```text
on slider change:
  action = { setState: "seats" }
  state.set("seats", newValue)
  render()
```

v0.1은 full rerender로 충분하다. v0.2에서 node key와 diff operation을 추가한다.

## 9. Renderer adapter

renderer adapter interface:

```text
create(type, props) -> ViewHandle
setProp(handle, name, value)
append(parent, child)
replace(old, next)
remove(handle)
bindEvent(handle, eventName, handlerId)
```

Tizen adapter는 이 interface 아래에서 NUI/EFL widget을 생성한다. Web demo adapter는 HTML component로 대체 구현할 수 있다.

## 10. Streaming 확장 v0.2

v0.2에서는 full document replace 대신 patch를 도입한다.

```json
[
  { "op": "replace", "path": "/root/children/1/text", "value": "..." },
  { "op": "add", "path": "/root/children/3", "value": { "type": "stat" } }
]
```

필요한 기능:

- patch validation
- state preservation
- stable node key
- last known good UI
- fallback markdown append

## 11. Milestones

### Milestone 1: Static renderer

- JSON IR parse
- schema validation
- static component render
- fallback markdown

### Milestone 2: Local state

- state store
- slider/checkbox event
- derived expression AST
- full rerender

### Milestone 3: Operation renderer

- virtual tree
- diff operation
- renderer adapter operation application

### Milestone 4: Streaming

- patch protocol
- partial update
- last known good UI
- diagnostics

### Milestone 5: Native adapter

- Tizen/NUI or EFL mapping
- focus navigation
- remote-control input
- TV-safe layout constraints

## 12. Success criteria

PoC 성공 기준:

- invalid input이 crash를 내지 않는다.
- unknown component가 fallback된다.
- slider 변경이 모델 호출 없이 derived value를 업데이트한다.
- renderer와 runtime이 분리되어 있다.
- 같은 IR을 web demo와 native adapter에 모두 적용할 수 있다.

