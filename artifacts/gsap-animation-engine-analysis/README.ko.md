# GSAP 애니메이션 엔진 분석

언어: [English](README.md) | [한국어](README.ko.md)

이 topic-home은 GSAP를 웹 애니메이션 엔진 관점에서 기술적으로 분석한 문서 모음이다. 초점은 GSAP의 아키텍처, 런타임 구동 방식, 구현 개념, 그리고 Tizen 같은 네이티브 C/C++ UI 환경으로 유사한 구조를 옮길 수 있는지에 있다.

## 요약

GSAP는 단순 DOM 애니메이션 헬퍼가 아니라 시간 기반 property animation engine으로 보는 것이 가장 정확하다. 핵심 모델은 다음 요소로 구성된다.

- `Tween`: 시작값에서 끝값까지 property를 시간에 따라 보간한다.
- `Timeline`: 여러 Tween과 중첩 Timeline을 하나의 제어 가능한 시간축 위에 배치한다.
- `Ticker`: 브라우저 repaint 주기에 맞춰 전역 애니메이션 clock을 진행한다.
- `Ease`: 선형 progress를 실제 모션 감각이 있는 곡선으로 변환한다.
- `Plugins`: 스크롤, SVG, 드래그, 레이아웃 전환, 텍스트, 경로 이동 같은 도메인 기능을 확장한다.

GSAP의 가장 중요한 엔지니어링 아이디어는 "시간 제어"와 "property 적용"을 분리한다는 점이다. 같은 개념 엔진으로 DOM 요소, SVG attribute, CSS transform, 일반 JavaScript object, plugin 관리 대상까지 움직일 수 있다.

Tizen/native C/C++ 관점에서 GSAP의 핵심 교훈은 웹 API 표면이 아니다. 유용한 것은 엔진 형태다. 즉 frame driver, animation object, timeline sequencer, interpolation/easing function, UI property adapter, lifecycle cleanup 구조다.

## 핵심 결론

단순 UI 전환은 네이티브 플랫폼 primitive만으로 충분하다. 하지만 복잡한 choreography, reversible timeline, scroll-linked progress, 디자이너 주도 모션 반복 작업이 필요하다면 GSAP는 이미 성숙한 runtime model을 제공한다. 이를 네이티브에서 재현하려면 별도 애니메이션 엔진 계층이 필요하다.

Tizen 관점에서 정리하면 다음과 같다.

- 기본 제품 UI motion은 native EFL/Evas/Edje/Ecore 계열 animation facility로 구현 가능하다.
- GSAP식 개발 경험을 만들려면 native UI component 위에 추가 abstraction layer가 필요하다.
- 실용적인 native PoC는 ScrollTrigger 같은 고급 기능보다 `Tween`, `Timeline`, `Ticker`, `Ease`, 작은 property-adapter interface부터 시작해야 한다.

## 문서 목록

- [gsap-technical-report.ko.md](gsap-technical-report.ko.md): 메인 기술 보고서와 도입 판단.
- [architecture-notes.ko.md](architecture-notes.ko.md): 런타임 아키텍처, lifecycle, pseudo-code.
- [native-implementation-feasibility.ko.md](native-implementation-feasibility.ko.md): Tizen/native C/C++ UI에서 GSAP식 모델 구현 가능성.
- [poc-design.ko.md](poc-design.ko.md): 작은 GSAP-inspired animation engine PoC 설계.
- [references.ko.md](references.ko.md): 공식 문서와 참고 링크.

영문 원본:

- [README.md](README.md)
- [gsap-technical-report.md](gsap-technical-report.md)
- [architecture-notes.md](architecture-notes.md)
- [native-implementation-feasibility.md](native-implementation-feasibility.md)
- [poc-design.md](poc-design.md)
- [references.md](references.md)

## 폴더명

선택한 topic-home:

```text
artifacts/gsap-animation-engine-analysis/
```

선택 이유:

- 실제 주제가 GSAP 소개가 아니라 animation engine 분석이다.
- 웹 분석과 네이티브 구현성 검토를 모두 담을 수 있다.
- 공개 기술 문서로 쓰기에 중립적이고 명확하다.

