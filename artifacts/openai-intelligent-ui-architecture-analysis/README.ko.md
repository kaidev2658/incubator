# OpenAI Intelligent UI 아키텍처 분석

이 topic-home은 OpenAI가 발표한 `Intelligent UI`를 기술 아키텍처 관점에서 분석한 문서 모음이다. 초점은 GPT-6 자체 성능 소개가 아니라, 모델이 대화 응답 안에서 인터랙티브 UI를 생성하고, 서버와 클라이언트가 이를 안전하게 컴파일, 스트리밍, 렌더링하는 구조에 있다.

## 요약

OpenAI의 공식 발표에서 Intelligent UI는 ChatGPT가 질문에 따라 텍스트, 시각 요소, 버튼, 폼, 차트, 인터랙티브 경험을 조합해 응답할 수 있게 하는 기능으로 설명된다. 공식 글은 이를 위해 native, streamable component library와 compiler를 구축했다고 밝힌다.

X 분석 포스트는 이를 더 낮은 수준에서 분해한다. 해당 분석에 따르면 ChatGPT는 모델이 작성한 UI를 그대로 브라우저에서 실행하지 않고, DIL이라는 중간 표현, 서버 컴파일, 샌드박스 런타임, native component renderer, component catalog와 design token 체계로 나누어 처리한다. 이 부분은 공식 문서가 아니라 관찰 기반 분석이므로 본 문서에서는 "확정 사실"과 "구현 추정"을 분리한다.

핵심 결론은 다음과 같다.

- Intelligent UI는 단순히 더 예쁜 답변 UI가 아니라, AI-native UI runtime에 가깝다.
- 모델은 화면을 직접 그리는 renderer가 아니라, 제한된 UI 언어와 컴포넌트 카탈로그 안에서 인터페이스 의도를 생성한다.
- 서버 compiler는 불완전한 스트리밍 출력을 검증, 보정, 컴파일하고, 클라이언트 runtime은 이를 안전하게 평가해 native component operation으로 바꾼다.
- Tizen/native 관점에서 그대로 복제해야 할 것은 웹 구현 세부가 아니라 component catalog, streaming UI IR, renderer bridge, sandboxed local state runtime이라는 구조다.

## 문서 목록

- [intelligent-ui-technical-report.ko.md](intelligent-ui-technical-report.ko.md): 공식 발표와 제품/기술 의미를 종합한 메인 보고서.
- [architecture-deep-dive.ko.md](architecture-deep-dive.ko.md): Model, compiler, runtime, renderer, component catalog 관점의 아키텍처 분석.
- [dil-and-component-runtime-analysis.ko.md](dil-and-component-runtime-analysis.ko.md): X 분석 포스트의 DIL, 컴파일 결과, 런타임 구조 해석.
- [security-and-sandbox-analysis.ko.md](security-and-sandbox-analysis.ko.md): 모델 생성 UI를 안전하게 실행하기 위한 보안 경계 분석.
- [native-implementation-feasibility.ko.md](native-implementation-feasibility.ko.md): Tizen C/C++ 또는 native UI 환경에서 유사 구조를 구현할 수 있는지 검토.
- [strategy-and-implications.ko.md](strategy-and-implications.ko.md): AI OS, 앱 플랫폼, generative UI 관점의 전략적 의미.
- [poc-design.ko.md](poc-design.ko.md): 최소 Intelligent UI runtime PoC 설계안.
- [references.ko.md](references.ko.md): 공식 자료, 관찰 기반 분석, 보조 기술 참고 링크.

## 폴더명

선택한 topic-home:

```text
artifacts/openai-intelligent-ui-architecture-analysis/
```

선택 이유:

- 주제가 GPT-6 전체가 아니라 Intelligent UI의 기술 구조 분석이다.
- 공식 발표와 관찰 기반 구현 분석을 함께 담을 수 있다.
- 기존 `a2ui-analysis/`, `gsap-animation-engine-analysis/`, `tizen-ai-os-prd/`와 연결되지만 겹치지 않는다.
- 나중에 native renderer 또는 AI-native UI runtime PoC로 확장하기 좋다.

## 관련 내부 문서

- `artifacts/a2ui-analysis/`: A2UI, renderer, protocol, Tizen runtime 관련 기존 분석과 구현 작업.
- `artifacts/gsap-animation-engine-analysis/`: UI property runtime, timeline, renderer abstraction 관점에서 비교 가능한 애니메이션 엔진 분석.
- `artifacts/tizen-ai-os-prd/`: Tizen AI OS 관점의 제품/아키텍처 문맥.

