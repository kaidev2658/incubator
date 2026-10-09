# Intelligent UI 기술 Q&A

이 문서는 OpenAI Intelligent UI, A2UI, JSON Render, Tizen/native renderer 관점에서 나온 후속 질문과 답변을 정리한 것이다. 목적은 대화형 설명을 추후 기술 판단에 바로 참고할 수 있는 Q&A 형태로 남기는 것이다.

## Q1. Intelligent UI에서 말하는 native component renderer는 웹인가?

반드시 웹만 뜻하지 않는다. 여기서 renderer는 모델이 만든 UI 설명을 실제 플랫폼의 정해진 UI 컴포넌트로 바꿔 그리는 계층을 의미한다.

웹 ChatGPT에서는 결과적으로 React/DOM 계열의 내부 컴포넌트로 렌더링될 가능성이 크다. 예를 들면 `slider`, `box`, `title` 같은 추상 컴포넌트가 ChatGPT 웹앱의 Slider, layout, typography component로 매핑되는 식이다.

모바일 앱이나 native 환경에서는 같은 UI 표현이 iOS/Android native view, WebView 기반 renderer, 또는 하이브리드 renderer로 구현될 수 있다. Tizen으로 옮기면 같은 개념은 다음과 같다.

```text
UI IR -> Tizen renderer -> NUI/EFL/C++ component tree
```

핵심은 모델이 HTML/JS/CSS를 직접 실행하지 않고, 플랫폼이 신뢰하는 컴포넌트 catalog로 번역해 렌더링한다는 점이다.

## Q2. ChatGPT 웹, iPhone 앱, 데스크탑 앱은 같은 renderer를 쓰는가?

공식적으로 플랫폼별 내부 renderer 구현은 공개되어 있지 않다. 다만 구조적으로는 공통 계층과 플랫폼별 계층을 나누어 보는 것이 적절하다.

```text
공통 계층:
Model output -> DIL/IR -> server compiler -> UI metadata/patch stream

플랫폼별 계층:
Web -> web runtime/component renderer -> DOM
iOS/Android -> native renderer 또는 WebView/hybrid renderer
Desktop -> Electron/WebView 기반 재사용 또는 별도 native renderer
```

즉 겉으로는 같은 Intelligent UI 경험을 제공하더라도, 실제 화면에 그리는 client renderer는 플랫폼마다 다를 수 있다. 중요한 것은 Intelligent UI의 본체가 특정 웹 UI가 아니라, 공통 UI 표현을 각 플랫폼 renderer가 자기 방식으로 그리는 구조라는 점이다.

## Q3. A2UI와 Intelligent UI는 거의 같은 개념인가?

컨셉 레벨에서는 같은 계열이다. 둘 다 agent/model이 UI를 직접 픽셀로 그리는 대신, 구조화된 UI 표현을 보내고, 클라이언트/런타임이 이를 실제 컴포넌트로 렌더링한다.

```text
A2UI:
Agent -> A2UI message/protocol -> Canvas/Renderer -> UI

OpenAI Intelligent UI:
Model -> DIL/IR -> Server compiler -> Client runtime/Renderer -> UI
```

차이는 목적과 계층 위치다. A2UI는 agent가 renderer에게 UI 상태를 선언적으로 전달하기 위한 protocol/IR에 가깝다. Intelligent UI는 ChatGPT 답변 자체를 interactive UI로 만드는 제품 기능/런타임에 가깝다.

따라서 "OpenAI가 A2UI를 썼다"고 단정하면 안 된다. 더 정확한 표현은 "A2UI와 같은 계열의 agent/model-to-UI protocol 아이디어를 ChatGPT 답변 안에 제품 기능으로 구현한 것이 Intelligent UI"다.

## Q4. "Descriptive Generative UI"라고 불러도 되는가?

의미는 통하지만 기술적으로는 `Declarative Generative UI`가 더 정확하다. 핵심은 모델이 "어떻게 그려라"를 픽셀/명령형으로 지시하는 것이 아니라, "무엇을 어떤 구조로 배치할지"를 선언적으로 설명한다는 점이다.

더 적절한 표현은 다음과 같다.

```text
Declarative Generative UI
Model-generated declarative UI
AI-native declarative UI runtime
Catalog-based Generative UI
Component-constrained Generative UI
```

특히 Intelligent UI와 A2UI-like architecture는 무한 자유 생성이 아니라, 허용된 component catalog와 schema 안에서 UI tree를 생성하는 구조다. 그래서 `Catalog-based Generative UI`라는 표현도 유용하다.

## Q5. 컴포넌트가 정의되어 있다면, 배치는 누가 결정하는가?

모델이 동적으로 결정한다. 다만 완전 자유 배치가 아니라 미리 정의된 layout primitive와 component catalog 안에서 조합한다.

예를 들어 catalog가 다음 컴포넌트를 제공한다고 가정할 수 있다.

```text
box, row, column, grid, stack
title, text, button, slider, chart, card
```

모델은 질문 맥락에 따라 다음을 결정한다.

- 어떤 컴포넌트를 쓸지
- 어떤 순서로 둘지
- row/column/grid/box 중 어떤 layout을 쓸지
- 제목, 설명, 입력, 결과를 어떻게 grouping할지
- 어떤 값이 state에 연결될지
- 어떤 interaction이 필요한지

반면 runtime/platform은 다음을 강제한다.

- 사용 가능한 컴포넌트 종류
- prop type과 허용 범위
- spacing, color, radius 같은 design token
- 모바일/웹/TV별 layout 제약
- 보안상 금지된 action
- 접근성, focus, navigation 규칙

즉 모델은 layout tree를 구성하지만, catalog/schema/design token이라는 울타리 안에서만 구성한다.

## Q6. ChatGPT Intelligent UI의 전체 component 목록은 공개되어 있는가?

공식적으로 공개된 전체 component catalog, prop schema, layout primitive 목록은 없다. OpenAI 공식 발표는 범주 수준만 언급한다.

```text
graphics
buttons
forms
charts
interactive experiences
```

관찰 기반 X 분석 포스트에서는 다음 component가 예시로 등장한다.

```text
title
text
bold
box
slider
icon
image
product
AppBlock
```

또한 해당 분석은 ChatGPT client code 안에 약 70개 native component가 있고, 캡처된 응답에서는 그중 39개가 나타났다고 설명한다. 하지만 이는 공식 specification이 아니라 reverse engineering/observation 기반으로 봐야 한다.

Tizen 또는 자체 renderer 구현에서는 OpenAI 내부 목록을 기다리기보다 필요한 catalog를 직접 정의하는 것이 현실적이다.

```text
Layout: box, row, column, grid, stack, card
Text: title, text, caption, badge
Input: button, slider, checkbox, select
Data: stat, table, chart, progress
Media: image, icon
System: loading, error, empty-state, confirmation
```

## Q7. 모델이 HTML을 더 잘 생성할 텐데, 왜 구조화된 UI IR을 쓰는가?

모델은 HTML/CSS/JS를 잘 생성할 수 있다. 하지만 제품 관점에서 중요한 것은 "잘 생성하는가"보다 "안전하고 일관되게 실행되는가"다.

HTML/JS 직접 생성 방식의 문제는 다음과 같다.

- 보안 위험이 크다.
- 스타일 일관성이 깨지기 쉽다.
- 모바일/데스크탑/native 이식이 어렵다.
- 접근성 보장이 어렵다.
- 제품 디자인 시스템과 충돌한다.
- 부분 실패 복구가 어렵다.
- 임의 JS 실행 위험이 있다.

구조화 UI IR 방식은 표현력은 줄어들지만 다음 장점이 있다.

- schema validation이 가능하다.
- 허용 component만 렌더링할 수 있다.
- design token을 강제할 수 있다.
- 플랫폼별 renderer 교체가 가능하다.
- 부분 실패 복구가 가능하다.
- sandbox/runtime boundary를 만들기 쉽다.
- Tizen/native로 이식하기 쉽다.

따라서 모델이 구조화 데이터를 항상 완벽하게 만든다고 믿는 것이 아니라, 다음 계층들이 흔들림을 흡수하도록 설계한다.

```text
model training/prompting
-> constrained UI DSL/IR
-> schema validation
-> compiler repair/normalization
-> runtime safe evaluation
-> component whitelist renderer
-> fallback markdown
```

## Q8. A2UI는 실제 제품에 많이 적용되었는가? 생성형 UI의 대세인가?

A2UI는 유망하지만 아직 업계 절대 표준이라고 보기는 이르다. 다만 Google/Flutter/agent ecosystem 쪽에서 진지하게 밀고 있는 중요한 공개 protocol 후보인 것은 맞다.

현재 생성형 UI 생태계는 여러 축으로 갈라져 있다.

```text
A2UI:
agent/model-to-UI protocol 표준 후보

JSON Render:
JSON schema/component catalog 기반으로 UI를 렌더링하는 실전 framework

Vercel AI SDK Generative UI:
tool result를 React component로 연결하는 웹/React 중심 접근

MCP Apps / OpenAI Apps SDK:
tool/app UI를 iframe/resource 형태로 붙이는 platform 접근

ChatGPT Intelligent UI:
OpenAI 제품 내부의 end-to-end generated UI runtime

Flutter GenUI:
Flutter 앱에서 생성형 UI를 구현하는 SDK, A2UI와 강하게 연결
```

생성형 UI라는 방향 자체는 상승세가 강하지만, A2UI 하나가 모든 생태계를 장악했다고 보기는 어렵다. 웹/React 제품이라면 Vercel AI SDK, JSON Render, CopilotKit 계열이 더 빠를 수 있고, ChatGPT app/tool integration이라면 MCP Apps/OpenAI Apps SDK가 더 직접적이다.

Tizen/TV/native AI OS 관점에서는 A2UI-like protocol + native renderer가 가장 적합한 방향이다.

## Q9. JSON Render는 이 생태계에서 어떤 위치인가?

JSON Render는 별도 축으로 보는 것이 맞다. A2UI가 protocol/spec 느낌이 강하다면, JSON Render는 JSON UI를 실제 React/component registry로 렌더링하는 framework/renderer에 가깝다.

```text
A2UI = UI message format/protocol
JSON Render = 그 규격 또는 유사 JSON UI를 실제 화면에 그리는 renderer/framework
```

둘은 경쟁이라기보다 조합 가능하다.

```text
Agent -> A2UI JSON -> JSON Render -> React components
```

또는:

```text
Agent -> custom JSON UI schema -> JSON Render -> React components
```

Tizen에서는 JSON Render 자체를 그대로 쓰기보다 설계 패턴을 가져오는 것이 맞다.

```text
JSON Render for Web:
JSON UI -> schema validation -> React component registry -> DOM

Tizen JSON Renderer:
JSON UI -> schema validation -> Tizen component registry -> NUI/EFL/C++ UI
```

따라서 Tizen/AI OS 방향에서는 `A2UI-like protocol + JSON-render-style native renderer` 조합이 적절하다.

## Q10. Intelligent UI는 템플릿 기반으로 볼 수도 있지 않은가?

그렇다. Intelligent UI와 A2UI-like generated UI는 완전한 무에서 UI를 만드는 방식이라기보다, 미리 정의된 component와 layout primitive를 모델이 상황에 맞게 조합하는 방식이다. 넓게 보면 template 기반이고, 좁게 보면 dynamic component composition 기반의 generative UI다.

스펙트럼은 다음과 같이 볼 수 있다.

```text
fixed template
-> parameter-filled template
-> component-composed template
-> model-composed declarative UI
-> raw HTML/JS generation
```

전통적 template 기반은 개발자가 화면 구조를 미리 정하고 모델이나 서버는 빈칸만 채운다.

```text
reservation card template
title = "La Luna Bistro"
date = "Apr 12"
guests = 2
```

Intelligent UI/A2UI-like 방식은 모델이 template 선택뿐 아니라 component 조합과 layout 구성까지 결정한다.

```text
이 질문은 text-only보다 slider + chart + stat 조합이 낫다.
세로 box 안에 title, 설명, slider, 결과값, chart를 배치한다.
```

따라서 이분법으로 "template인가 generative인가"를 나누기보다, "template/component catalog로 강하게 제한된 generative UI"라고 보는 것이 정확하다.

## Q11. ChatGPT Intelligent UI의 핵심 technical insight는 무엇인가?

핵심은 모델이 HTML/JS를 직접 실행하는 것이 아니라, 제한된 UI 언어/IR로 "어떤 component를 어떻게 배치할지"를 생성하고, server compiler와 client runtime이 이를 검증, 보정, streaming rendering하는 구조라는 점이다.

진짜 기술 포인트는 generative UI 자체보다 component catalog, schema validation, sandbox runtime, native/component renderer, fallback을 결합해 모델 출력의 불안정성을 제품 수준 UX로 흡수한 데 있다.

Tizen/AI OS 관점에서는 OpenAI 구현을 그대로 복제하기보다, A2UI-like protocol + JSON-render-style native renderer로 재해석하는 것이 핵심이다.

