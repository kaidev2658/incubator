# Intelligent UI 기술 분석 리포트

## 1. 배경

OpenAI는 GPT-6 발표에서 Intelligent UI를 ChatGPT가 텍스트, 시각 요소, 인터랙티브 컴포넌트를 질문에 맞게 조합해 응답하는 기능으로 설명했다. 예시는 레시피와 일정이 함께 보이는 요리 계획, 지도 위의 여행 경로, 입력값을 바꿔가며 이해하는 학습 도구, 계산기나 게임 같은 즉석 도구를 포함한다.

중요한 지점은 "모델이 UI를 만든다"는 표현을 너무 문자 그대로 해석하면 안 된다는 점이다. 실제로 유용하고 안전한 시스템이 되려면 모델이 arbitrary HTML, CSS, JavaScript를 마음대로 생성해서 실행하는 구조는 곤란하다. 신뢰할 수 있는 제품 UI로 만들려면 제한된 컴포넌트, 검증 가능한 속성, 점진적 스트리밍, fallback, 샌드박스, 상태 유지가 필요하다.

따라서 Intelligent UI는 다음과 같이 보는 것이 적절하다.

```text
natural language answer
  -> generated interface description
  -> validated/compiled UI program or IR
  -> sandboxed runtime evaluation
  -> native component operations
  -> interactive rendered response
```

## 2. 공식 발표에서 확인되는 내용

OpenAI 공식 글에서 확인되는 주요 사실은 다음과 같다.

- GPT-6는 답변을 텍스트, visuals, interactive elements로 구성할 수 있다.
- UI 형식은 질문에 따라 달라진다. 비교는 side-by-side, 설명은 interactive diagram, 단순 답변은 text-only가 될 수 있다.
- ChatGPT는 graphics, tappable buttons, forms, charts, interactive experiences를 응답 안에 포함할 수 있다.
- OpenAI는 native, streamable components library와 compiler를 구축했다고 설명한다.
- compiler는 모델이 생성하는 동안 interface를 처리해, 전체 응답이 끝나기 전에 점진적으로 나타나게 한다.
- 모델은 content, layout, visuals, interaction에 대한 판단을 하도록 훈련되었다.

공식 글만 놓고 보면 가장 중요한 단어는 `native`, `streamable`, `component library`, `compiler`다. 이 네 단어가 Intelligent UI의 기술 방향을 거의 설명한다.

## 3. 제품 관점의 의미

기존 Chat UI는 대부분 다음 형태였다.

```text
user question -> model answer -> markdown/text rendering
```

Intelligent UI는 이 구조를 다음 형태로 넓힌다.

```text
user goal -> model chooses response modality -> interactive artifact inside conversation
```

즉 사용자는 "앱을 열고 기능을 찾는" 대신, 목표를 말하면 그 목표에 맞는 임시 인터페이스가 대화 안에 만들어진다. 이것은 앱 UI의 완전한 대체라기보다, 다음 영역에서 특히 강하다.

- 한 번 쓰고 버리는 task-specific tool
- 설명을 위한 interactive visualization
- 비교표, 시나리오 계산, 계획표처럼 구조화된 정보
- 사용자의 입력에 따라 바로 재계산되는 lightweight UI
- 기존 앱을 열기에는 과한 작은 작업

## 4. 기술 관점의 의미

Intelligent UI는 모델 품질만으로 성립하지 않는다. 최소한 다음 시스템 계층이 필요하다.

| 계층 | 역할 |
| --- | --- |
| Model | 질문을 이해하고 어떤 UI가 적합한지 결정한다. |
| UI IR/DSL | 모델이 출력할 수 있는 제한된 인터페이스 표현이다. |
| Compiler | partial output을 검증, 보정, 컴파일한다. |
| Runtime | 컴파일된 UI program을 격리 환경에서 평가하고 상태를 관리한다. |
| Renderer | runtime 결과를 실제 UI component tree로 반영한다. |
| Component catalog | 모델이 사용할 수 있는 컴포넌트와 속성을 정의한다. |
| Design system | spacing, color, radius, typography 등 일관된 표현 규칙을 제공한다. |
| Fallback | UI 생성 실패 시 일반 markdown/text 답변으로 회복한다. |

이 구조는 frontend framework와 agent runtime의 중간에 있다. React와 비슷한 reconciler 개념이 필요하지만, 입력이 개발자 코드가 아니라 모델 생성물이라는 점이 다르다. 그래서 validation, sandbox, fallback, streaming repair가 훨씬 중요하다.

## 5. 왜 raw code가 아닌가

모델이 HTML/JS/CSS를 직접 생성하고 브라우저에서 실행하는 방식은 빠른 데모에는 좋아 보인다. 하지만 제품 수준에서는 문제가 많다.

- 보안: 임의 네트워크 요청, DOM 접근, storage 접근, credential 노출 위험이 있다.
- 안정성: streaming 중에는 대부분의 코드가 incomplete 상태다.
- UX 일관성: 모델이 매번 다른 UI 스타일을 만들면 제품 경험이 깨진다.
- 접근성: 컴포넌트 수준에서 keyboard, screen reader, focus 처리를 보장하기 어렵다.
- 모바일/네이티브: raw web markup은 native iOS/Android/Tizen renderer로 이식하기 어렵다.
- 검증: arbitrary code는 schema validation이 어렵다.

따라서 제대로 된 Intelligent UI는 "모델이 UI를 직접 그린다"가 아니라 "모델이 제한된 catalog 안에서 UI 의도를 작성하고, 시스템이 그것을 검증된 native UI로 렌더링한다"에 가깝다.

## 6. X 분석 포스트의 의미

Rabi Shanker Guha의 X 포스트는 OpenAI의 Intelligent UI를 관찰 기반으로 분해한다. 해당 포스트는 다음 구조를 제시한다.

- 모델은 DIL이라는 형식으로 interface를 작성한다.
- DIL은 Markdown, JSX-like tag, JavaScript state/logic 표현을 섞은 언어로 설명된다.
- 서버는 partial response를 JavaScript program과 JSON document로 컴파일한다.
- 클라이언트는 sandboxed runtime에서 program을 실행한다.
- runtime은 UI operation을 생성하고, ChatGPT client는 이를 native components에 적용한다.
- component catalog와 design token이 모델의 UI 표현 범위를 제한한다.

이 분석은 공식 문서가 아니므로 구현 세부는 검증된 사실로 단정하면 안 된다. 하지만 아키텍처 방향은 공식 발표의 `component library + compiler + streamable UI` 설명과 잘 맞는다.

## 7. 기존 기술과의 비교

### Markdown rendering

Markdown은 텍스트 구조화에는 충분하지만, interactive state와 component lifecycle을 표현하기 어렵다.

### HTML/JS sandbox

HTML/JS sandbox는 표현력이 높지만, 보안과 UX 일관성 비용이 크다. 또한 native component로 옮기기 어렵다.

### React Server Components

서버가 UI tree를 stream한다는 점에서 일부 유사하다. 그러나 Intelligent UI는 UI source가 개발자 코드가 아니라 모델 출력이라는 점, 그리고 partial generation repair가 중요하다는 점이 다르다.

### ChatGPT Apps 또는 Plugin UI

Apps/Plugin UI는 개발자가 만든 도구와 UI를 ChatGPT에 붙이는 방향이다. Intelligent UI는 모델이 그 순간의 답변 형식 자체를 구성한다는 점에서 다르다. 다만 둘은 component embedding, tool result, sandbox, UI guideline 관점에서 연결될 수 있다.

### A2UI

A2UI 관점에서 보면 Intelligent UI는 "agent가 UI protocol을 통해 렌더러에게 화면을 요청한다"는 큰 방향과 맞닿아 있다. 차이는 Intelligent UI가 ChatGPT 대화 응답 안의 generated UI에 초점을 두고, A2UI는 agent-to-UI protocol 또는 renderer contract 관점으로 일반화될 수 있다는 점이다.

## 8. 결론

Intelligent UI의 핵심은 "모델이 예쁜 화면을 만든다"가 아니다. 핵심은 모델 생성물을 제품 UI로 만들기 위한 안전한 변환 파이프라인이다.

가장 중요한 설계 원칙은 다음과 같다.

- 모델 출력은 신뢰하지 않는다.
- 모델에게 raw platform power를 주지 않는다.
- 컴포넌트 카탈로그와 schema로 표현 공간을 제한한다.
- streaming 중에도 유효한 partial UI를 만들 수 있어야 한다.
- 실패한 UI는 전체 응답 실패가 아니라 부분 fallback으로 처리한다.
- 상호작용은 가능한 한 로컬 상태 업데이트로 처리하고, 매번 모델 호출을 요구하지 않는다.

Tizen/native 관점에서 이 구조는 매우 중요하다. AI OS가 진짜로 "상황에 맞는 UI"를 만들려면, LLM 위에 바로 앱 화면을 붙이는 것이 아니라 native component catalog, UI IR, renderer bridge, runtime state model, sandbox boundary를 먼저 설계해야 한다.

