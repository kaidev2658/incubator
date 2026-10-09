# Native 구현 가능성 분석

## 1. 결론 요약

OpenAI Intelligent UI와 유사한 구조는 Tizen C/C++ 또는 native UI framework 위에서도 구현 가능하다. 다만 "모델이 UI 코드를 생성해서 그대로 실행한다"는 방향은 권장하지 않는다. 실용적인 native 구현은 제한된 UI IR, component catalog, streaming parser, validator, renderer bridge를 중심으로 설계해야 한다.

추천 구현 순서는 다음이다.

1. JSON 기반 UI IR 정의
2. native component catalog 작성
3. schema validator 구현
4. one-shot renderer 구현
5. local state와 event handler 추가
6. streaming patch protocol 추가
7. sandboxed expression evaluator 추가
8. external tool/action bridge 추가

## 2. 필요한 구성요소

### 2.1 UI IR

모델 또는 서버가 생성하는 UI 표현이다. 초기에는 JSON이 가장 현실적이다.

```json
{
  "version": "0.1",
  "root": {
    "type": "card",
    "props": { "padding": 3 },
    "children": [
      { "type": "title", "text": "Savings calculator" },
      {
        "type": "slider",
        "props": {
          "min": 0,
          "max": 100,
          "value": { "state": "monthlyDeposit" }
        }
      }
    ]
  }
}
```

### 2.2 Component catalog

Native renderer가 지원하는 component 목록과 prop schema다.

```text
Box
Text
Title
Button
Slider
Checkbox
Select
Table
Chart
Image
```

각 component는 native UI toolkit의 실제 widget으로 mapping된다.

### 2.3 Validator

validator는 IR을 렌더링 전에 검사한다.

- version 지원 여부
- component type 허용 여부
- prop type 검사
- child type 제약
- value range 검사
- event/action 허용 여부
- data binding 권한 확인

### 2.4 Renderer bridge

renderer bridge는 normalized UI tree를 native component tree로 변환한다.

```text
IR node
  -> validated node
  -> native view instance
  -> layout properties
  -> event binding
```

Tizen에서는 NUI, EFL/Evas, 또는 project-specific UI abstraction이 target이 될 수 있다.

### 2.5 Runtime state

Interactive UI에는 local state가 필요하다.

```text
state["seats"] = 8
event slider.onChange(9)
state["seats"] = 9
derived["price"] = seats * 29
rerender affected subtree
```

초기 구현은 full rerender로 충분하다. 성능이 필요해지면 diff operation을 도입한다.

## 3. WebView 기반 vs Native renderer 기반

### WebView 기반

장점:

- HTML/CSS/JS 생태계를 활용할 수 있다.
- 빠른 PoC가 가능하다.
- AppBlock 같은 escape hatch 구현이 쉽다.

단점:

- native UX와 다를 수 있다.
- 성능과 메모리 비용이 크다.
- 보안 경계가 복잡하다.
- TV/embedded 환경에서 WebView 품질 편차가 있다.
- platform component와 accessibility 통합이 약하다.

### Native renderer 기반

장점:

- 플랫폼 UX와 일관된다.
- 성능과 메모리 제어가 쉽다.
- component whitelist를 강제하기 좋다.
- OS/TV input, remote control, focus model에 맞추기 좋다.

단점:

- component catalog를 직접 만들어야 한다.
- layout engine과 renderer bridge 구현 비용이 든다.
- chart, map, rich media 같은 컴포넌트는 별도 구현이 필요하다.
- 표현력은 WebView보다 제한된다.

## 4. Tizen 관점의 구현 난이도

| 영역 | 난이도 | 설명 |
| --- | --- | --- |
| Static text/card rendering | 낮음 | JSON IR을 native widget으로 변환하면 된다. |
| Basic layout | 중간 | row/column/box/grid와 responsive rule이 필요하다. |
| Local input state | 중간 | state store와 event binding이 필요하다. |
| Streaming partial UI | 중간-높음 | incremental patch, fallback, state key 안정성이 필요하다. |
| Chart/table | 중간-높음 | component별 native 구현 품질이 중요하다. |
| Arbitrary mini app | 높음 | sandbox, runtime, resource limit이 필요하다. |
| External actions | 높음 | permission, confirmation, audit가 필요하다. |

## 5. 기존 A2UI 작업과의 연결

기존 `a2ui-analysis/`는 Tizen renderer, protocol, normalizer, runtime pipeline 관점의 기반 자료를 이미 포함한다. Intelligent UI 분석은 이 작업과 다음 지점에서 연결된다.

- UI protocol을 streaming 가능한 message format으로 설계해야 한다.
- renderer는 component catalog와 schema를 기준으로 동작해야 한다.
- normalizer는 모델 출력의 흔들림을 흡수해야 한다.
- runtime은 local state와 event operation을 다뤄야 한다.
- Tizen adapter는 protocol detail과 native widget detail을 분리해야 한다.

즉 Intelligent UI는 A2UI의 좋은 reference case가 될 수 있다. 다만 OpenAI 구현 추정 세부를 그대로 복제하기보다, native target에 맞는 단순화된 protocol을 설계하는 것이 더 낫다.

## 6. 추천 PoC 범위

초기 PoC는 다음 정도가 적절하다.

지원 component:

- `Text`
- `Title`
- `Box`
- `Button`
- `Slider`
- `Stat`

지원 기능:

- JSON IR parse
- schema validation
- one-shot render
- local state
- derived expression 제한 지원
- event handler로 state update
- markdown fallback

지원하지 않을 기능:

- arbitrary JavaScript
- external network
- raw CSS
- AppBlock
- tool action
- model-in-the-loop interaction

## 7. 구현 방향

가장 안전한 구조:

```text
LLM/server
  -> emits JSON UI IR
Native host
  -> parse
  -> validate
  -> normalize
  -> render native components
  -> handle local events
  -> update state
  -> rerender
```

서버가 compiler 역할을 맡고 native client는 compiled operation stream만 받는 방식도 가능하다. 하지만 초기 PoC에서는 client-side validator와 renderer를 만들어 두는 편이 추후 실험에 유리하다.

## 8. 최종 판단

Tizen/native에서 Intelligent UI 스타일 시스템을 구현하는 것은 가능하다. 다만 성공 기준은 "OpenAI와 같은 기능을 다 만든다"가 아니라, "모델 또는 agent가 제한된 protocol로 native UI를 안전하게 요청할 수 있다"가 되어야 한다.

실무적으로 가장 가치 있는 1차 산출물은 다음이다.

- A2UI-compatible UI IR
- Tizen native component catalog
- schema validator
- renderer adapter
- local interaction runtime
- streaming update protocol

