# Intelligent UI 보안 및 샌드박스 분석

## 1. 문제 정의

모델 생성 UI는 일반 UI보다 위험하다. 일반 앱 UI는 개발자가 작성하고 리뷰하고 배포한다. Intelligent UI는 사용자의 질문에 따라 매번 새롭게 만들어진다. 따라서 다음 위험을 기본값으로 봐야 한다.

- prompt injection이 UI source에 섞일 수 있다.
- 모델이 의도치 않은 code path를 만들 수 있다.
- 생성된 UI가 네트워크, storage, credential, DOM에 접근할 수 있다.
- 사용자가 UI를 신뢰하고 민감 정보를 입력할 수 있다.
- 외부 data와 결합될 때 phishing-like UI가 만들어질 수 있다.

따라서 Intelligent UI의 보안 목표는 "모델이 안전한 UI만 생성한다"가 아니다. 더 정확한 목표는 "모델이 잘못 생성해도 runtime과 renderer가 피해를 제한한다"다.

## 2. 권한 최소화

모델 생성 UI runtime은 최소 권한 원칙을 따라야 한다.

허용할 수 있는 것:

- 제한된 component 생성
- local state update
- 제한된 derived expression 계산
- renderer로 operation emit
- 미리 허용된 data binding 읽기

기본적으로 막아야 하는 것:

- arbitrary network request
- cookie, localStorage, sessionStorage 접근
- DOM 직접 조작
- parent window 접근
- dynamic import 또는 eval 확장
- timer abuse
- clipboard, file, camera, microphone 접근
- 임의 navigation

## 3. Web 환경의 격리 수단

웹에서는 다음 기술이 주요 격리 수단이다.

### 3.1 iframe sandbox

sandboxed iframe은 embedded document의 capability를 제한할 수 있다. Intelligent UI의 경우 runner iframe을 별도로 두고, 본문 renderer와 분리하는 구조가 합리적이다.

### 3.2 Content Security Policy

CSP는 script, worker, frame, image, connection 같은 resource loading 위치를 제한할 수 있다. 예를 들어 `default-src 'none'`에 가까운 정책은 기본 resource access를 닫고 필요한 것만 열도록 만든다.

### 3.3 Web Worker

Web Worker는 main thread와 분리된 실행 환경을 제공한다. DOM을 직접 조작할 수 없고, main thread와는 message passing으로 통신한다. 다만 worker 자체도 fetch 등 일부 API를 사용할 수 있으므로 runtime hardening이 추가로 필요하다.

### 3.4 Watchdog

모델 생성 program이 infinite loop에 빠지거나 응답하지 않을 수 있다. timeout watchdog, worker restart, quarantine이 필요하다.

## 4. Schema validation

샌드박스만으로 충분하지 않다. 실행 전에 component schema validation이 필요하다.

검증 대상:

- component type
- prop name
- prop type
- literal range
- event handler shape
- child constraints
- style/design token constraints
- data binding source

검증 실패 시 정책:

- invalid prop은 제거
- invalid component는 fallback
- 위험한 handler는 무시
- diagnostics에 기록
- 전체 UI가 위험하면 fallback markdown으로 대체

## 5. Renderer whitelist

renderer는 허용된 component만 생성해야 한다.

```text
allowed: box, text, title, button, slider, chart
blocked: script, iframe, raw-html, style, form-submit, external-link-without-policy
```

raw CSS도 주의해야 한다. design token 대신 임의 CSS를 허용하면 다음 문제가 생긴다.

- clickjacking-like overlay
- invisible text or button
- brand spoofing
- mobile layout breakage
- accessibility regression

초기 구현에서는 raw CSS를 금지하고 token만 허용하는 것이 안전하다.

## 6. Data boundary

Intelligent UI는 text만 다루지 않는다. 이미지, 검색 결과, tool output, user data가 결합될 수 있다. 따라서 data boundary가 필요하다.

원칙:

- 모델 생성 UI source와 외부 data를 분리한다.
- data binding은 명시적 key를 통해서만 접근한다.
- tool result는 renderer-safe shape로 normalize한다.
- 민감 data는 component에 전달되기 전에 redaction 또는 policy check를 거친다.
- UI source 안에 secret을 inline하지 않는다.

## 7. Interaction boundary

사용자 interaction은 local state update와 external action으로 나뉜다.

Local-only:

- slider 변경
- checkbox toggle
- tab switch
- local chart filter
- 계산식 재평가

External action:

- 결제
- 이메일 전송
- 캘린더 수정
- 파일 삭제
- 외부 API 호출
- 개인정보 제출

External action은 Intelligent UI component handler에서 바로 실행하면 안 된다. 별도 confirmation, tool permission, audit trail을 거쳐야 한다.

## 8. AppBlock 리스크

AppBlock처럼 raw HTML/CSS/JS 기반 mini app을 허용하는 escape hatch는 강력하지만, 보안 부담이 크다.

필요한 방어:

- 별도 origin 또는 강한 sandbox
- network deny by default
- storage deny
- strict CSP
- allowed API bridge만 노출
- size/time/resource limit
- user-visible trust boundary
- dangerous action confirmation

가능하면 일반 Intelligent UI path는 AppBlock 없이 component catalog만으로 처리해야 한다. AppBlock은 component catalog가 감당하지 못하는 표현형을 위한 예외 경로여야 한다.

## 9. Native/Tizen 보안 모델

Native 환경에서는 iframe/worker/CSP가 그대로 없을 수 있다. 대신 다음 설계가 필요하다.

- UI IR을 data로 처리하고 code execution을 피한다.
- expression은 제한된 AST evaluator로만 처리한다.
- renderer adapter가 native component whitelist를 강제한다.
- file/network/device access는 runtime에서 제공하지 않는다.
- external action은 OS permission과 별도 confirmation 계층을 거친다.
- crash isolation을 위해 generated UI runtime을 별도 process로 둘 수 있다.

Native PoC의 첫 단계에서는 generated code execution을 아예 넣지 않는 것이 좋다. 상태와 계산은 미리 정의한 operation만 허용한다.

## 10. 보안 결론

Intelligent UI의 보안은 모델 safety만으로 해결되지 않는다. 제품 아키텍처 차원의 방어가 필요하다.

핵심 원칙:

- model output is untrusted
- compile before render
- validate before execute
- execute with minimal capability
- render only whitelisted components
- keep data and code separate
- require confirmation for external actions
- provide fallback instead of failing open

