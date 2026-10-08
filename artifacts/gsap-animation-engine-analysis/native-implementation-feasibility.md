# Native Implementation Feasibility

## 1. Question

Can GSAP-like animation concepts be implemented using native C/C++ UI components on Tizen?

Short answer:

```text
Yes for the core animation model.
Partially for the full GSAP web feature set.
Not cheaply for ScrollTrigger/SVG/text/plugin breadth.
```

The transferable part is the engine pattern: ticker, tween, timeline, easing, interpolation, and property adapters. The non-transferable part is much of the web-specific surface: DOM selectors, CSS transform syntax, browser layout behavior, SVG path manipulation, and scroll-driven page semantics.

## 2. Tizen Native Context

Tizen native UI work commonly involves EFL-family components and lower-level drawing/timing concepts:

- Elementary widgets for UI components.
- Evas canvas objects and rendering primitives.
- Edje layout/theme/state-based behavior.
- Ecore animator/timer/event loop support.
- Cairo/OpenGL ES when custom rendering is needed.

The exact stack depends on target profile, device generation, and application type. For the purposes of this report, the important point is that Tizen native has a frame/event loop and object properties that can be driven over time. That is enough for a GSAP-like core.

## 3. Feature Mapping

| GSAP concept | Native equivalent idea | Feasibility |
| --- | --- | --- |
| Tween | object property interpolation | High |
| Timeline | sequencer over tween objects | High |
| Ticker | Ecore animator or platform frame callback | High |
| Ease | math functions | High |
| CSS transform | Evas object geometry/transform/map | Medium |
| Opacity | object color/alpha/effect property | High |
| DOM selector target | explicit object handles | High if explicit, low if selector-like |
| CSSPlugin | native property adapter | High for typed subset |
| ScrollTrigger | scroll progress adapter | Medium to low |
| Pin/scrub/snap | custom layout/scroll coordination | Low to medium |
| SVG morph/draw | custom vector/path engine | Low |
| SplitText | text layout and glyph-level handling | Low to medium |
| React lifecycle helpers | native view/context ownership | Medium |

## 4. What Is Straightforward

### 4.1 Basic Property Animation

Animating native UI properties is feasible:

- x/y position,
- width/height,
- scale,
- rotation if supported by the rendering layer,
- opacity,
- color,
- focus highlight intensity,
- shadow/elevation-like values if available.

### 4.2 Timeline Sequencing

A native timeline object can sequence tweens without relying on the UI framework. It only needs:

- duration,
- child offsets,
- playhead,
- render method.

This is independent of Tizen and can be written as portable C/C++.

### 4.3 Easing

Easing is pure math. It is trivial to implement and test.

### 4.4 Focus/Remote-Control Motion

Tizen TV-style UI often benefits from focus animation:

- card scale on focus,
- focus ring movement,
- side panel slide,
- list item fade/shift,
- carousel snapping.

These are better first targets than scroll storytelling because they match native product UI interaction.

## 5. What Is Difficult

### 5.1 ScrollTrigger-Like Behavior

ScrollTrigger is difficult because it combines animation with layout, viewport, scroll state, refresh timing, pin spacing, and event throttling.

Native equivalent challenges:

- scroll container metrics differ by widget,
- virtualized lists may not expose all item geometry,
- pinned layouts need custom layout ownership,
- resize/orientation changes need refresh,
- remote control and focus navigation differ from pointer scroll.

### 5.2 Web-Like Selectors

GSAP can target `.class` or `#id` in the DOM. Native C/C++ UI should avoid selector-based lookup in the first version.

Use explicit handles:

```cpp
animator.to(cardView, TweenVars{}.x(100).opacity(1));
```

### 5.3 SVG and Text Effects

SVG path morph/draw and text splitting rely on web-specific text/vector abstractions. Native equivalents require:

- path parser,
- path normalization,
- glyph layout access,
- per-glyph rendering or separate objects,
- text shaping awareness.

These are not appropriate for a first native animation engine milestone.

### 5.4 Memory Safety

GSAP benefits from JavaScript garbage collection. C/C++ needs explicit ownership.

Common risk:

```text
animation callback fires after the target widget has been destroyed
```

The engine must solve this with context ownership, weak handles, cancellation, or target invalidation checks.

## 6. Recommended Native Architecture

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

First milestone:

- animate explicit object handles,
- support x/y/opacity/scale,
- support `to()` and `fromTo()`,
- support play/pause/reverse/seek,
- support timeline append and overlap,
- support 5 to 8 easing functions,
- support target invalidation,
- support deterministic unit tests for interpolation and timing.

Do not include in first milestone:

- scroll pinning,
- SVG morph,
- text splitting,
- string property parsing,
- selector system,
- full plugin marketplace-style extension.

## 9. When Native Is the Right Choice

Native is the better route when:

- the UI is a product interface, not a web storytelling page,
- performance and memory predictability matter,
- remote-control/focus UX is central,
- the app already uses native Tizen UI,
- animations are part of component behavior rather than marketing effects.

## 10. When Tizen Web App + GSAP Is Better

Web App + GSAP is better when:

- the experience is visually rich and page-like,
- designers need rapid iteration,
- ScrollTrigger-like storytelling is required,
- the same artifact should run on web and Tizen,
- existing GSAP examples can be adapted quickly.

## 11. Decision Matrix

| Requirement | Recommendation |
| --- | --- |
| simple UI transitions | native primitives |
| focus card motion | native engine or native primitives |
| complex timeline choreography | GSAP or custom native timeline |
| scroll storytelling | GSAP in web runtime |
| strict device performance | native engine |
| frequent motion design iteration | GSAP/web |
| reusable native product UI motion | custom native tween/timeline layer |

## 12. Final Feasibility Judgment

Building the GSAP core idea in Tizen C/C++ is feasible. Building the full GSAP ecosystem is not a practical first goal.

The right target is:

```text
GSAP-inspired native animation core
not
GSAP clone
```

That means focusing on time, interpolation, sequencing, and property adapters.

