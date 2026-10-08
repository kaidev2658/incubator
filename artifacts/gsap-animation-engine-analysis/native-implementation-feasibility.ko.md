# Native 구현 가능성 검토

## 1. 질문

Tizen의 native C/C++ UI component로 GSAP-like animation concept를 구현할 수 있는가?

짧은 답:

```text
Core animation model은 가능하다.
Full GSAP web feature set은 부분적으로만 가능하다.
ScrollTrigger/SVG/text/plugin breadth까지 싸게 재현하기는 어렵다.
```

이전 가능한 부분은 ticker, tween, timeline, easing, interpolation, property adapter 같은 engine pattern이다. 이전하기 어려운 부분은 DOM selector, CSS transform syntax, browser layout behavior, SVG path manipulation, scroll-driven page semantics 같은 web-specific surface다.

## 2. Tizen Native Context

Tizen native UI 작업은 보통 EFL 계열 component와 low-level drawing/timing 개념을 사용한다.

- Elementary widget
- Evas canvas object와 rendering primitive
- Edje layout/theme/state-based behavior
- Ecore animator/timer/event loop
- 필요 시 Cairo/OpenGL ES

정확한 stack은 target profile, device generation, application type에 따라 달라진다. 이 보고서에서 중요한 것은 Tizen native에도 frame/event loop와 시간에 따라 변경할 수 있는 object property가 있다는 점이다. 이는 GSAP-like core 구현에 충분한 기반이다.

## 3. Feature Mapping

| GSAP concept | Native equivalent idea | Feasibility |
| --- | --- | --- |
| Tween | object property interpolation | High |
| Timeline | tween object sequencer | High |
| Ticker | Ecore animator 또는 platform frame callback | High |
| Ease | math function | High |
| CSS transform | Evas object geometry/transform/map | Medium |
| Opacity | object color/alpha/effect property | High |
| DOM selector target | explicit object handle | explicit이면 High, selector-like이면 Low |
| CSSPlugin | native property adapter | typed subset이면 High |
| ScrollTrigger | scroll progress adapter | Medium to Low |
| Pin/scrub/snap | custom layout/scroll coordination | Low to Medium |
| SVG morph/draw | custom vector/path engine | Low |
| SplitText | text layout와 glyph-level handling | Low to Medium |
| React lifecycle helper | native view/context ownership | Medium |

## 4. 쉬운 영역

### 4.1 Basic Property Animation

Native UI property animation은 충분히 가능하다.

- x/y position
- width/height
- scale
- rotation, rendering layer가 지원하는 경우
- opacity
- color
- focus highlight intensity
- shadow/elevation-like value, available한 경우

### 4.2 Timeline Sequencing

Native timeline object는 UI framework에 강하게 의존하지 않고 tween을 sequence할 수 있다. 필요한 것은 다음뿐이다.

- duration
- child offset
- playhead
- render method

이 부분은 Tizen과 독립적인 portable C/C++로 작성할 수 있다.

### 4.3 Easing

Easing은 pure math다. 구현과 test가 쉽다.

### 4.4 Focus/Remote-Control Motion

Tizen TV-style UI는 focus animation의 이점이 크다.

- card scale on focus
- focus ring movement
- side panel slide
- list item fade/shift
- carousel snapping

이는 scroll storytelling보다 좋은 첫 target이다. Native product UI interaction과 더 잘 맞기 때문이다.

## 5. 어려운 영역

### 5.1 ScrollTrigger-like Behavior

ScrollTrigger가 어려운 이유는 animation을 layout, viewport, scroll state, refresh timing, pin spacing, event throttling과 결합하기 때문이다.

Native equivalent challenge:

- scroll container metric이 widget마다 다름
- virtualized list는 모든 item geometry를 노출하지 않을 수 있음
- pinned layout은 custom layout ownership이 필요함
- resize/orientation change 시 refresh가 필요함
- remote control과 focus navigation은 pointer scroll과 다름

### 5.2 Web-like Selector

GSAP는 `.class`, `#id` 같은 DOM selector를 target으로 사용할 수 있다. Native C/C++ UI는 첫 버전에서 selector-based lookup을 피하는 것이 좋다.

명시적 handle을 사용한다.

```cpp
animator.to(cardView, TweenVars{}.x(100).opacity(1));
```

### 5.3 SVG와 Text Effect

SVG path morph/draw와 text splitting은 web-specific text/vector abstraction에 의존한다. Native equivalent에는 다음이 필요하다.

- path parser
- path normalization
- glyph layout access
- per-glyph rendering 또는 separate object
- text shaping awareness

첫 native animation engine milestone에 넣기에는 적절하지 않다.

### 5.4 Memory Safety

GSAP는 JavaScript garbage collection의 도움을 받는다. C/C++에서는 explicit ownership이 필요하다.

흔한 risk:

```text
target widget이 destroy된 뒤 animation callback이 실행됨
```

Engine은 context ownership, weak handle, cancellation, target invalidation check 중 하나로 이를 해결해야 한다.

## 6. 추천 Native Architecture

```text
AnimationContext
  owns timelines and tweens for a screen/component

Ticker
  called from platform animator/event loop

Timeline
  sequences child animations

Tween
  owns property tracks

PropertyAdapter
  writes typed values to platform objects

Ease
  pure functions
```

## 7. Minimal Native API

### C++ style

```cpp
auto context = AnimationContext::create();

context->timeline()
  .to(card, TweenVars()
    .x(80.0f)
    .opacity(1.0f)
    .duration(0.35)
    .ease(Ease::Power2Out))
  .to(title, TweenVars()
    .y(0.0f)
    .opacity(1.0f)
    .duration(0.25),
    "-=0.10");
```

### C style

```c
anim_context_t* ctx = anim_context_create();
anim_timeline_t* tl = anim_timeline_create(ctx);

anim_tween_t* t = anim_tween_create(card);
anim_tween_prop_float(t, ANIM_PROP_X, 80.0f);
anim_tween_prop_float(t, ANIM_PROP_OPACITY, 1.0f);
anim_tween_duration(t, 0.35f);
anim_tween_ease(t, ANIM_EASE_POWER2_OUT);

anim_timeline_add(tl, t, ANIM_AT_END);
```

## 8. Native PoC Scope

첫 milestone:

- explicit object handle animate
- x/y/opacity/scale 지원
- `to()`와 `fromTo()` 지원
- play/pause/reverse/seek 지원
- timeline append와 overlap 지원
- 5-8개 easing function 지원
- target invalidation 지원
- interpolation/timing deterministic unit test

첫 milestone 제외:

- scroll pinning
- SVG morph
- text splitting
- string property parsing
- selector system
- full plugin marketplace-style extension

## 9. Native가 맞는 경우

Native route가 더 좋은 경우:

- UI가 web storytelling page가 아니라 product interface일 때
- performance와 memory predictability가 중요할 때
- remote-control/focus UX가 핵심일 때
- app이 이미 native Tizen UI를 사용할 때
- animation이 marketing effect가 아니라 component behavior의 일부일 때

## 10. Tizen Web App + GSAP가 나은 경우

Web App + GSAP가 더 좋은 경우:

- visually rich하고 page-like한 experience일 때
- designer가 빠르게 iteration해야 할 때
- ScrollTrigger-like storytelling이 필요할 때
- 같은 artifact를 web과 Tizen에서 돌리고 싶을 때
- 기존 GSAP example을 빠르게 적용할 수 있을 때

## 11. Decision Matrix

| Requirement | Recommendation |
| --- | --- |
| simple UI transitions | native primitives |
| focus card motion | native engine 또는 native primitives |
| complex timeline choreography | GSAP 또는 custom native timeline |
| scroll storytelling | GSAP in web runtime |
| strict device performance | native engine |
| frequent motion design iteration | GSAP/web |
| reusable native product UI motion | custom native tween/timeline layer |

## 12. 최종 Feasibility 판단

Tizen C/C++에서 GSAP core idea를 만드는 것은 가능하다. Full GSAP ecosystem을 만드는 것은 첫 목표로 현실적이지 않다.

올바른 target은 다음이다.

```text
GSAP-inspired native animation core
not
GSAP clone
```

즉 시간, interpolation, sequencing, property adapter에 집중해야 한다.

