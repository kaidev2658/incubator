# GSAP Technical Report

## 1. Purpose

This report studies GSAP from an engineering perspective:

- what GSAP is,
- how it is structured,
- how it runs animations,
- what the key concepts are,
- where it differs from native browser animation primitives,
- and what lessons can be carried into a native C/C++ UI animation engine.

The report treats GSAP as a runtime system for time-based property animation rather than as a collection of visual effects.

## 2. What GSAP Is

GSAP, the GreenSock Animation Platform, is a JavaScript animation library for creating high-control animation on the web. Its official documentation describes the core as capable of fast, responsive animation across browsers, with additional capabilities separated into plugins such as Draggable, ScrollTrigger, and MorphSVG.

GSAP is framework-agnostic. Official installation guidance states that it can be used with React, Webflow, WordPress, and other JavaScript or web frameworks because the core and plugins are JavaScript files.

Since the Webflow-supported change, the GSAP pricing page states that GSAP is now free for all users and that the original GSAP team maintains the library while working full time at Webflow.

## 3. Mental Model

The most useful mental model is:

```text
GSAP = time axis + property interpolation + target adapters + plugin extensions
```

GSAP is not tied to a single rendering backend. It can animate:

- DOM/CSS properties,
- CSS transforms,
- SVG attributes,
- scroll-linked progress,
- plain JavaScript object properties,
- and plugin-specific abstractions.

The core runtime answers one repeated question:

```text
At time T, what should each animated property value be?
```

After computing the answer, it writes the result through the appropriate adapter: CSS, DOM, SVG, object property, or plugin-managed effect.

## 4. Core Concepts

### 4.1 Target

A target is the object or collection of objects to animate.

Examples:

```js
gsap.to(".card", { opacity: 1 });
gsap.to(elementRef, { x: 100 });
gsap.to([el1, el2], { scale: 1.1 });
gsap.to(counterObject, { value: 100 });
```

Targets can be selector strings, DOM elements, arrays, or ordinary objects. For selector strings, GSAP resolves matching elements before animating them.

### 4.2 Vars Object

The vars object contains the animated properties and control options.

```js
gsap.to(".box", {
  x: 100,
  opacity: 0.5,
  duration: 1,
  delay: 0.2,
  ease: "power2.out",
  onComplete: done
});
```

This mixes domain properties (`x`, `opacity`) with runtime options (`duration`, `delay`, `ease`, `onComplete`). That design makes the common case concise, but an engine implementation should internally separate property specs from playback metadata.

### 4.3 Tween

A Tween is the smallest unit of animation work. It records:

- target reference,
- properties to animate,
- start values,
- end values,
- duration,
- easing function,
- callbacks,
- repeat/yoyo/overwrite/lifecycle flags.

Official GSAP documentation describes a Tween as a high-performance property setter: when its playhead moves, it figures out the correct values at that time and applies them.

Typical constructors:

```js
gsap.to(target, vars);
gsap.from(target, vars);
gsap.fromTo(target, fromVars, toVars);
```

The important distinction:

- `to()`: current value to specified value.
- `from()`: specified value to current value.
- `fromTo()`: explicit start value to explicit end value.

### 4.4 Timeline

A Timeline is a container for Tweens and nested Timelines. It composes animations on a shared time axis.

```js
const tl = gsap.timeline();

tl.to(".logo", { opacity: 1, y: 0, duration: 0.5 })
  .to(".title", { opacity: 1, y: 0, duration: 0.6 }, "-=0.2")
  .to(".button", { scale: 1, opacity: 1, duration: 0.4 });
```

Timeline solves the "delay chain" problem. Without a timeline, each subsequent animation needs manually recalculated delays. With a timeline, relative placement is expressed in a sequence model.

Timeline position parameters are a key GSAP concept:

- number: absolute time in seconds,
- `+=1`: after the current end by 1 second,
- `-=0.3`: overlap by 0.3 seconds,
- `<`: align with the start of the previous animation,
- `>`: align with the end of the previous animation,
- label: place at a named timeline position.

### 4.5 Playhead

Every Tween and Timeline has a playhead. The playhead is the current time position within that animation.

```js
tl.pause();
tl.play();
tl.reverse();
tl.seek(1.2);
tl.progress(0.5);
```

This makes GSAP animations controllable runtime objects. They are not just declarations that run once and disappear.

### 4.6 Ease

Easing maps normalized time progress to perceived motion progress.

```text
linear progress: 0.0 -> 0.5 -> 1.0
eased progress:  0.0 -> 0.78 -> 1.0
```

Basic interpolation:

```text
rawProgress = elapsed / duration
progress = ease(rawProgress)
value = start + (end - start) * progress
```

Ease selection often matters more than duration for perceived quality. A technically correct animation with poor easing feels mechanical; a simple transform with good easing can feel polished.

### 4.7 Ticker

The ticker is GSAP's frame driver. Official documentation describes `gsap.ticker` as the heartbeat of the GSAP engine. It updates the global timeline on every `requestAnimationFrame` event and allows custom listeners.

The conceptual loop:

```js
function tick(now) {
  globalTimeline.render(now);
  requestAnimationFrame(tick);
}

requestAnimationFrame(tick);
```

The real implementation has more machinery:

- frame number,
- elapsed time,
- delta time,
- optional FPS limiting,
- lag smoothing,
- hidden-tab throttling inherited from browser behavior,
- listener priority,
- fallback behavior where needed.

The architectural point is that all active animations are coordinated through a shared time source.

### 4.8 Plugin

Plugins extend the core engine into specific domains. Official GSAP guidance says plugins are JavaScript files like the core and should usually be registered with `gsap.registerPlugin()` so they work cleanly with the core and bundlers.

Example:

```js
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";

gsap.registerPlugin(ScrollTrigger);
```

Plugins let the core remain focused on timing and interpolation while domain-specific logic lives elsewhere.

Important plugins:

- ScrollTrigger: scroll-linked trigger/progress/pin/scrub/snap behavior.
- Draggable: drag gestures and inertial interaction.
- Flip: layout transition animation.
- MotionPath: movement along a path.
- MorphSVG: shape interpolation for SVG paths.
- DrawSVG: line-drawing effects.
- SplitText/Text/ScrambleText: text animation utilities.

## 5. Runtime Flow

The runtime flow for a simple tween:

```text
User calls gsap.to(target, vars)
  -> target is resolved
  -> tween object is created
  -> property plugins/adapters are selected
  -> initial render reads starting values
  -> end values and units are parsed
  -> tween is attached to a timeline
  -> ticker advances the global timeline
  -> tween receives a local playhead time
  -> raw progress is calculated
  -> ease function maps raw progress
  -> interpolated values are calculated
  -> values are written to target properties
  -> callbacks fire when applicable
  -> completed disposable children may be removed
```

This is the core lifecycle a native implementation should reproduce.

## 6. Property Interpolation

GSAP's engineering strength is its property handling. A property may be:

- numeric,
- unit-bearing (`px`, `%`, `deg`),
- color-like,
- transform component,
- SVG attribute,
- string with embedded numeric values,
- plugin-defined property.

The general pipeline is:

```text
parse current value
parse target value
normalize units if needed
cache start/end values
compute progress each frame
interpolate value
serialize value
write value
```

For native C/C++, the equivalent should avoid string parsing for core UI properties where possible. Prefer typed properties:

```cpp
Property::X(float)
Property::Opacity(float)
Property::Scale(float)
Property::Rotation(float)
```

String-based properties should be treated as an extension layer, not the initial core.

## 7. CSS Transform and Performance Model

For DOM animation, GSAP commonly encourages transform and opacity-based animation:

- `x`, `y` map to transform translation,
- `scale` maps to transform scale,
- `rotation` maps to transform rotation,
- `opacity` changes visual alpha.

This matters because transform and opacity often avoid layout recalculation compared with `left`, `top`, width, and height changes. They are more likely to stay in compositor-friendly paths.

Practical guideline:

- Prefer transform/opacity for frequent frame-by-frame animation.
- Avoid repeatedly animating layout-heavy properties unless the design requires it.
- Batch reads and writes to reduce layout thrash.

GSAP has features such as lazy rendering that aim to avoid inefficient read/write interleaving on first render.

## 8. ScrollTrigger

ScrollTrigger is best understood as a scroll-to-time adapter.

It maps scroll state into animation state:

```text
scroll position -> normalized progress -> tween/timeline playhead
```

Example:

```js
gsap.to(".box", {
  x: 500,
  scrollTrigger: {
    trigger: ".box",
    start: "top 80%",
    end: "top 20%",
    scrub: true
  }
});
```

Key concepts:

- `trigger`: element used as the reference.
- `start`: scroll position at which the trigger begins.
- `end`: scroll position at which the trigger ends.
- `scrub`: links scroll progress to animation progress.
- `pin`: keeps an element fixed during a scroll range.
- `snap`: moves progress to nearby defined points.
- callbacks: `onEnter`, `onLeave`, `onUpdate`, `onRefresh`, etc.
- refresh/recalculation: updates start/end positions on resize and layout changes.

The hard part is not "detect scroll." The hard part is keeping scroll position, layout, viewport changes, pinned spacing, refresh timing, and animation progress consistent.

## 9. Comparison With Native Browser Options

### 9.1 CSS Transitions and Animations

CSS is excellent for:

- simple hover/focus transitions,
- basic enter/exit effects,
- repeated keyframe loops,
- low-JavaScript UI polish.

Limitations:

- complex sequencing becomes delay-heavy,
- runtime reversal/seek/control is awkward,
- dynamic value calculation is limited,
- cross-element choreography is harder.

### 9.2 Web Animations API

MDN describes the Web Animations API as combining a timing model and an animation model for DOM elements. It exposes objects such as `Animation`, `KeyframeEffect`, and `AnimationTimeline`.

WAAPI is closer to GSAP than CSS alone because it exposes playback objects. However, GSAP still provides broader authoring ergonomics, plugin ecosystem, target abstraction, timeline composition, and battle-tested higher-level features.

### 9.3 Raw requestAnimationFrame

MDN describes `requestAnimationFrame()` as asking the browser to call a callback before the next repaint. It normally tracks display refresh rate and is paused in many browsers for background tabs.

Raw rAF gives maximum control, but implementing a production animation system on top of it requires:

- time normalization,
- delta handling,
- easing functions,
- property adapters,
- lifecycle management,
- pause/resume/reverse/seek,
- sequencing,
- overwrite/conflict behavior,
- cleanup,
- scroll synchronization,
- responsive recalculation.

GSAP can be viewed as the mature runtime layer that sits above rAF.

## 10. Framework Integration

### 10.1 React and Next.js

GSAP works in React/Next.js, but lifecycle matters:

- create animations after DOM refs exist,
- scope selectors to components,
- clean up animations on unmount,
- avoid duplicate animation creation under strict/dev rendering,
- avoid server-side DOM access.

GSAP provides React-specific helper patterns such as `useGSAP`, but the architectural concern is broader: animation objects must have the same lifecycle as the component tree.

### 10.2 Webflow

GSAP has become especially relevant to Webflow because Webflow now supports GSAP's wider availability and the official GSAP pricing page says the full library is free for all users thanks to Webflow's support.

For design-heavy sites, this strengthens GSAP's role as a bridge between designer-controlled layout and code-driven advanced motion.

### 10.3 Tizen Web App

For a Tizen Web App, GSAP can be used like a browser-side dependency if the runtime supports the required JavaScript and rendering features. This can be attractive for:

- promotional UI,
- animated TV experiences,
- web-authored landing-like screens,
- rapid iteration with design changes.

The main concerns are:

- device browser engine version,
- memory and CPU limits,
- remote-control focus UX,
- GPU/compositing behavior,
- startup time,
- certification/platform constraints.

## 11. Strengths

- Strong compositional model via Timeline.
- High-level control over pause, play, reverse, seek, repeat, and callbacks.
- Mature easing and property interpolation.
- Works across many target types, not just CSS.
- Rich plugin system.
- Strong ScrollTrigger ecosystem.
- Good authoring ergonomics for complex motion.
- Mature documentation and community examples.
- Current public availability removes the older paid-plugin barrier.

## 12. Limitations and Risks

- It can encourage over-animation if no motion design discipline exists.
- Scroll-driven effects can become fragile when page layout changes frequently.
- Complex timelines can become hard to maintain without naming and structure.
- Framework lifecycle misuse can cause duplicated animations or memory leaks.
- Heavy animation can expose low-end device performance problems.
- GSAP does not remove the need for accessibility practices such as reduced-motion alternatives.
- For native UI environments, GSAP's web API cannot be copied directly; only the architectural model transfers cleanly.

## 13. Engineering Takeaways

For native engine design, copy these ideas:

- Treat animation as time-controlled objects.
- Use a single frame driver.
- Keep sequencing separate from property interpolation.
- Use typed property adapters.
- Make playhead control first-class.
- Make cleanup and ownership explicit.
- Support callbacks but avoid making business logic depend on per-frame callbacks.
- Add plugin-like extensions only after the core Tween/Timeline model is stable.

Avoid copying these too early:

- full selector-based targeting,
- string-heavy property parsing,
- ScrollTrigger-scale behavior,
- broad plugin surface,
- complex SVG/text features.

## 14. Recommended Adoption Guidance

For web:

- Use CSS for simple UI transitions.
- Use WAAPI when browser-native animation objects are enough.
- Use GSAP when timelines, ScrollTrigger, advanced SVG/text/path effects, or designer iteration speed matter.

For Tizen/native:

- Use native UI primitives for standard product motion.
- Build a small GSAP-like abstraction only if many screens need coordinated animation.
- Start with a small engine PoC before attempting a full GSAP-style runtime.
- Keep ScrollTrigger-like behavior out of the first milestone unless scroll-driven storytelling is a hard requirement.

## 15. Short Verdict

GSAP's key value is not that it can move boxes. The key value is that it turns motion into a controllable, composable runtime model. That model can inform native C/C++ UI engine design, but reproducing the development experience requires building an animation layer above the platform widgets.

