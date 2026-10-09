# DIL 및 Component Runtime 분석

## 1. 전제

이 문서는 Rabi Shanker Guha의 X 분석 포스트를 바탕으로 Intelligent UI 내부 구조를 해석한다. 해당 포스트는 공식 OpenAI 문서가 아니라 ChatGPT web app traffic, client JavaScript, 계정에서 관찰한 결과에 기반한 분석이라고 밝힌다.

따라서 본 문서의 표현은 다음 기준을 따른다.

- OpenAI 공식 발표에 나온 내용: 확정 사실로 표기
- X 포스트에 나온 구현 세부: 관찰 기반 분석 또는 추정으로 표기
- 본 문서의 구조화: 구현 가능성을 이해하기 위한 재구성

## 2. DIL이 해결하려는 문제

관찰 기반 분석에 따르면 ChatGPT Intelligent UI는 DIL이라는 중간 표현을 사용한다. DIL은 대략 다음 성격을 가진 것으로 설명된다.

- Markdown prose
- JSX-like component tags
- JavaScript-like state and derived logic
- model-friendly syntax
- streaming-friendly partial compilation

이런 형식이 필요한 이유는 명확하다. 모델은 token by token으로 출력한다. 즉 UI source는 대부분의 시간 동안 incomplete 상태다. 일반 JavaScript/JSX라면 중간 상태를 실행할 수 없지만, DIL 같은 tolerant source는 마지막 complete construct까지만 잘라서 컴파일하거나, 열린 element를 자동으로 닫아 partial render를 만들 수 있다.

## 3. 예시 구조

X 분석 포스트가 제시한 예시는 다음 구조다.

```text
## Team plan estimate
Drag the slider to see the monthly price for your team.
{@body const [seats,setSeats] = DIL.useState(8)}
{@body const price = seats*29}
<box border padding={3} gap={2}>
  <slider min={1} max={50} value={seats} onChange={setSeats}/>
  <title size="xl">${price}/mo</title>
</box>
```

여기서 Markdown은 prose이고, `box`, `slider`, `title`은 catalog component이며, `DIL.useState`는 local state를 표현한다. 이 구조는 raw app code가 아니라 "답변 안에 들어가는 작은 reactive UI"에 최적화되어 있다.

## 4. 서버 컴파일 결과의 성격

분석 포스트에 따르면 서버는 DIL을 다음 두 산출물로 컴파일한다.

- JavaScript program
- JSON document

JSON document에는 static text constants와 data binding 정보가 들어가고, JavaScript program은 runtime이 평가할 render function에 가깝다. 중요한 최적화와 안전장치는 다음이다.

### 4.1 Static text constants

텍스트를 code에서 분리하면 streaming 중 텍스트 증가를 program 변경이 아니라 data 변경으로 처리할 수 있다.

```json
{
  "constants": {
    "0": "Team plan estimate",
    "1": "Drag the slider..."
  }
}
```

### 4.2 Stable state key

streaming 중 source가 계속 재컴파일되어도 slider state가 사라지면 사용자 경험이 깨진다. 따라서 state slot에는 안정적인 key가 필요하다.

```text
useState(8, { key: "seats" })
```

### 4.3 Safe expression wrapper

모델이 만든 expression은 실패할 수 있다. expression 하나가 실패했다고 전체 UI를 날리면 안 된다. 그래서 expression-level error isolation이 필요하다.

```text
safe(() => seats * 29, undefined)
```

### 4.4 Validation diagnostics

component schema에 없는 prop이나 잘못된 literal은 제거하고 diagnostics로 기록해야 한다. 이 구조가 있어야 모델 출력 품질을 나중에 분석하고 개선할 수 있다.

## 5. Client runtime의 역할

관찰 기반 분석에서 client runtime은 직접 drawing을 하지 않는다. runtime은 compiled program을 sandbox 안에서 평가하고, 결과 tree를 이전 tree와 비교해 operation list를 만든다.

```text
render program
  -> virtual component tree
  -> diff previous tree
  -> operation list
```

operation 예시는 다음처럼 표현할 수 있다.

```text
CREATE #1 title
SET #1 size = "lg"
PLACE #1 root 0
CREATE #2 slider
SET #2 value = 8
REGISTER #2 onChange = fn#1
```

이 operation stream은 renderer에게 전달된다. renderer는 known component type만 생성한다.

## 6. Event handler model

중요한 점은 function이 renderer로 직접 넘어가지 않는다는 것이다. handler는 identifier로 표현되어야 한다.

```text
slider.onChange = fn#1
```

사용자가 slider를 움직이면 renderer는 다음 메시지를 runtime으로 보낸다.

```json
{
  "handler": "fn#1",
  "args": [9]
}
```

runtime은 sandbox 안에서 handler를 실행하고, state update 후 다시 render한다. 이 구조는 UI 상호작용을 로컬에서 처리하면서도 function capability가 sandbox 밖으로 새지 않게 한다.

## 7. AppBlock의 의미

X 분석 포스트는 일부 요청에서 AppBlock이라는 escape hatch가 사용된다고 설명한다. 이는 native component catalog로 표현하기 어려운 drum machine 같은 mini app을 HTML/CSS/JS로 embedding하는 방식으로 보인다.

이런 escape hatch는 강력하지만 위험하다.

- 표현력은 크게 올라간다.
- native UX 일관성은 약해진다.
- sandbox와 CSP 의존도가 높아진다.
- accessibility와 mobile fidelity를 보장하기 어렵다.
- 검증 가능한 component schema 밖으로 벗어난다.

따라서 제품 구조상 AppBlock은 일반 Intelligent UI component path가 아니라 예외 경로로 보는 것이 맞다.

## 8. DIL이 주는 설계 교훈

DIL이 실제 이름과 형태 그대로 쓰이는지와 별개로, 설계 교훈은 분명하다.

- 모델이 안정적으로 쓰는 syntax는 사람 개발자에게 좋은 syntax와 다를 수 있다.
- streaming-friendly grammar가 필요하다.
- Markdown과 component tree를 하나의 render tree로 합쳐야 한다.
- state key는 source 위치 변화에 강해야 한다.
- compiler는 repair와 validation을 동시에 해야 한다.
- runtime은 renderer가 아니라 operation generator여야 한다.
- renderer는 native component catalog에만 접근해야 한다.

## 9. Native 구현 관점

Tizen/native에서 DIL 같은 DSL을 그대로 만들 필요는 없다. 오히려 초기 PoC는 JSON IR이 더 안전하다.

```json
{
  "type": "box",
  "props": { "padding": 3, "gap": 2 },
  "children": [
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
      "type": "title",
      "children": [
        { "text": "$" },
        { "expr": "seats * 29" },
        { "text": "/mo" }
      ]
    }
  ]
}
```

초기 native renderer는 arbitrary JavaScript expression도 피하고, 제한된 expression AST 또는 predefined formula만 허용하는 편이 낫다.

