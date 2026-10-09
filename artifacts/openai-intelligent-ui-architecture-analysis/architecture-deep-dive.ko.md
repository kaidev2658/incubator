# Intelligent UI 아키텍처 Deep Dive

## 1. 전체 구조

Intelligent UI를 구현 가능한 시스템으로 재구성하면 다음 파이프라인이 된다.

```text
User intent
  -> Model planning
  -> Generated UI source or intermediate representation
  -> Server compiler
  -> Structured message patches
  -> Sandboxed client runtime
  -> UI operations
  -> Native renderer
  -> Interactive response
```

각 단계는 단순한 변환기가 아니라 서로 다른 위험을 줄이는 경계다.

- Model boundary: 어떤 UI가 유용한지 결정한다.
- Compiler boundary: 모델 출력이 유효한지 검증한다.
- Runtime boundary: 모델 생성 program을 격리해 평가한다.
- Renderer boundary: 허용된 native component만 실제 화면에 반영한다.

## 2. Model layer

모델의 역할은 UI를 pixel 단위로 그리는 것이 아니다. 모델은 다음 결정을 한다.

- 답변이 text-only로 충분한가?
- 비교표, 차트, slider, form, diagram 중 어떤 표현이 적합한가?
- 사용자가 조작할 수 있어야 하는 값은 무엇인가?
- 상호작용 결과는 로컬 계산으로 충분한가, 아니면 tool/model 재호출이 필요한가?
- 모바일 화면에서도 이해 가능한 layout인가?

공식 발표는 GPT-6가 content, layout, visuals, interaction에 대한 판단을 하도록 훈련되었다고 설명한다. 이 말은 모델이 "UI 작성 능력"뿐 아니라 "언제 UI를 쓰지 말아야 하는지"도 학습해야 한다는 뜻이다.

## 3. UI source 또는 IR layer

모델이 생성하는 UI 표현은 다음 조건을 만족해야 한다.

- 모델이 안정적으로 쓸 수 있어야 한다.
- token streaming 중 partial state에서도 회복 가능해야 한다.
- 서버가 schema validation을 할 수 있어야 한다.
- 텍스트와 컴포넌트가 함께 표현되어야 한다.
- state, event handler, derived value를 제한적으로 표현할 수 있어야 한다.
- native renderer로 옮길 수 있어야 한다.

X 분석 포스트는 OpenAI 구현이 DIL이라는 형식을 사용한다고 설명한다. 이 포맷은 Markdown, JSX-like component tag, JavaScript state/logic 표현을 섞은 것으로 관찰되었다. 이 구조가 맞다면, DIL은 개발자 친화적 언어라기보다 model-friendly UI IR에 가깝다.

## 4. Server compiler

compiler는 Intelligent UI에서 가장 중요한 안전장치 중 하나다. 하는 일은 다음과 같다.

- incomplete output repair
- unsupported component 제거
- invalid prop 제거
- static text와 dynamic code 분리
- state key 안정화
- fallback markdown 생성
- diagnostics 기록
- 클라이언트가 실행할 수 있는 program과 data document 생성

특히 streaming 환경에서는 "모델 출력이 아직 끝나지 않았다"는 상태가 정상이다. 따라서 compiler는 완성된 문서만 다루는 일반 parser가 아니라, 끊어진 tag와 incomplete statement를 다루는 tolerant compiler여야 한다.

## 5. Structured streaming

일반 텍스트 스트리밍은 append-only로 충분하다.

```text
"Here" -> "Here is" -> "Here is the answer"
```

하지만 UI 스트리밍은 append-only만으로 부족하다. 같은 답변 안에서 raw source, compiled code, constants, fallback markdown, component data가 함께 갱신되어야 한다.

따라서 합리적인 구조는 다음과 같다.

```json
{
  "message": {
    "content": {
      "parts": ["...fallback text..."]
    },
    "metadata": {
      "ui": {
        "source": "...partial UI source...",
        "compiled": "...compiled program...",
        "constants": {},
        "diagnostics": []
      }
    }
  }
}
```

X 분석 포스트는 ChatGPT가 JSON-Patch-style update로 raw DIL text, compiled code, constants, fallbackMarkdown를 함께 갱신한다고 설명한다. 이 방식은 partial UI가 계속 재컴파일되는 구조와 잘 맞는다.

## 6. Client runtime

클라이언트 runtime은 compiled program을 직접 DOM에 그리지 않는다. 역할은 다음과 같다.

- program evaluation
- local state hook 관리
- event handler registry 관리
- 이전 render tree와 새 render tree diff
- renderer가 적용할 operation 생성
- 실패 시 마지막 정상 render 유지

이 구조는 React reconciler와 유사하다. 하지만 중요한 차이가 있다.

React는 신뢰된 application code를 실행한다. Intelligent UI runtime은 모델에서 유래한 code 또는 program을 실행한다. 따라서 runtime은 더 강한 isolation, watchdog, capability restriction이 필요하다.

## 7. Renderer layer

renderer는 runtime이 만든 operation을 실제 화면에 적용한다.

```text
CREATE title
SET title.size = "lg"
PLACE title under root
CREATE slider
SET slider.value = 8
REGISTER handler fn#1
```

이때 renderer가 허용하는 것은 component catalog에 있는 타입뿐이다. 모델이 임의 HTML tag, style, script를 만들 수 있으면 renderer boundary가 깨진다.

Native renderer의 장점은 다음과 같다.

- 플랫폼별 component fidelity를 유지한다.
- 접근성, focus, gesture 처리를 공통 component에서 보장한다.
- design token으로 일관된 스타일을 적용한다.
- 모바일과 웹의 rendering target을 다르게 가져갈 수 있다.

## 8. Component catalog

component catalog는 모델이 사용할 수 있는 UI vocabulary다. 예시는 다음과 같다.

```text
layout: box, stack, grid, row, column
text: title, text, caption, badge
input: button, slider, checkbox, select, textfield
data: chart, table, stat, timeline
media: image, map, icon
special: app block, embedded tool result
```

catalog는 단순 목록이 아니라 schema여야 한다.

```json
{
  "component": "slider",
  "props": {
    "min": "number",
    "max": "number",
    "value": "state<number>",
    "onChange": "handler<number>"
  }
}
```

schema가 있어야 compiler가 invalid prop을 제거하고, renderer가 안전하게 component를 만들 수 있다.

## 9. Local interaction loop

좋은 Intelligent UI는 모든 상호작용마다 모델을 다시 부르지 않는다. 예를 들어 slider로 가격을 계산하는 경우는 로컬 state update만으로 충분하다.

```text
user moves slider
  -> renderer sends event handler id + value
  -> sandbox runtime updates state
  -> runtime re-renders virtual tree
  -> diff operations sent to renderer
  -> UI updates locally
```

모델 재호출이 필요한 경우는 다음처럼 더 무거운 작업이다.

- 사용자가 새로운 자연어 질문을 던진다.
- 외부 데이터 fetch나 tool call이 필요하다.
- 생성된 UI의 범위를 넘어서는 새 작업이 생긴다.

## 10. Failure model

Intelligent UI는 실패를 기본값으로 가정해야 한다.

- 모델이 invalid component를 생성할 수 있다.
- expression이 runtime error를 낼 수 있다.
- streaming 중 incomplete source일 수 있다.
- sandbox가 timeout될 수 있다.
- renderer가 어떤 component를 지원하지 않을 수 있다.
- data dependency가 늦게 도착할 수 있다.

따라서 필요한 fallback 계층은 다음과 같다.

1. expression-level fallback
2. component-level fallback
3. render tree-level fallback
4. message-level fallback markdown
5. product-level "plain answer" fallback

## 11. Native/Tizen으로 추상화한 아키텍처

Tizen 또는 C/C++ native UI에 적용하려면 다음 구조가 된다.

```text
LLM output
  -> UI IR normalizer
  -> schema validator
  -> state/runtime engine
  -> operation diff
  -> Tizen renderer adapter
  -> NUI/EFL component tree
```

웹의 iframe/worker는 Tizen에서는 그대로 대응되지 않을 수 있다. 대신 다음 중 하나를 선택해야 한다.

- JavaScriptCore/V8 같은 embedded JS sandbox
- Lua/WASM 같은 제한된 scripting runtime
- code execution 없는 JSON IR + native expression evaluator
- 서버에서 모든 compilation을 끝내고 native는 pure operation stream만 적용

가장 현실적인 PoC는 code execution을 최소화한 JSON IR 방식이다.

