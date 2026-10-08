# GSAP Architecture Notes

## 1. Conceptual Architecture

```text
User API
  gsap.to(), gsap.from(), gsap.timeline(), plugin APIs
        |
        v
Animation Object Layer
  Tween, Timeline, DelayedCall
        |
        v
Timing Layer
  Global timeline, playhead, ticker, lag smoothing
        |
        v
Interpolation Layer
  ease(progress), numeric interpolation, unit parsing, string/color parsing
        |
        v
Property Adapter Layer
  CSSPlugin, AttrPlugin, object property writer, plugin-defined adapters
        |
        v
Rendering Backend
  DOM style, SVG attributes, JS object fields, scroll state, plugin targets
```

This shape is the core architectural lesson. The animation engine should not be coupled directly to a specific UI component type. It should compute time and values, then delegate property application to adapters.

## 2. Main Runtime Objects

### Tween

Responsible for animating one or more properties on one or more targets over a duration.

State:

- targets,
- property tracks,
- start time,
- duration,
- delay,
- local playhead time,
- easing function,
- repeat/yoyo configuration,
- callbacks,
- active/paused/completed flags.

### Timeline

Responsible for arranging child Tweens or nested Timelines.

State:

- child list,
- labels,
- duration derived from children,
- defaults inherited by children,
- local playhead,
- repeat/yoyo/callback configuration,
- parent timeline reference.

### Ticker

Responsible for driving the root timeline.

State:

- time,
- frame number,
- delta time,
- listeners,
- lag smoothing settings,
- optional FPS throttling.

### Plugin

Responsible for extending the core with specialized parsing, lifecycle, or rendering behavior.

Examples:

- CSS transform writer,
- SVG path morph logic,
- scroll progress adapter,
- drag gesture adapter,
- layout transition adapter.

## 3. Tween Lifecycle

```text
create
  -> resolve target(s)
  -> parse vars
  -> select property adapters
  -> attach to timeline
  -> first render reads start values
  -> cache parsed property tracks
  -> update on each tick
  -> fire callbacks
  -> complete or remain controllable
  -> optional removal/cleanup
```

Important implementation point: first render may be delayed until the next tick so the engine can reduce layout read/write thrash. A native engine should similarly avoid interleaving geometry reads and writes across many animated objects.

## 4. Render Pseudo-Code

```cpp
void Ticker::tick(TimePoint now) {
  double delta = computeDelta(now);
  double engineTime = applyLagSmoothing(delta);
  globalTimeline.render(engineTime);
  notifyListeners(engineTime, delta);
}
```

```cpp
void Timeline::render(double parentTime) {
  double localTime = mapParentTimeToLocalTime(parentTime);

  for (Animation* child : children) {
    if (child->isActiveAt(localTime)) {
      child->render(localTime - child->startTime());
    }
  }
}
```

```cpp
void Tween::render(double localTime) {
  double raw = clamp(localTime / duration, 0.0, 1.0);
  double eased = ease(raw);

  for (PropertyTrack& track : tracks) {
    Value value = track.interpolate(eased);
    track.adapter->write(track.target, track.property, value);
  }

  runCallbacksIfNeeded(raw);
}
```

## 5. Time Mapping

Animation systems need at least three time spaces:

- global time: engine clock,
- timeline time: parent-controlled composition time,
- local animation time: tween-specific time after delay/repeat/yoyo mapping.

Example:

```text
global time = 10.0s
timeline start = 8.0s
tween start in timeline = 1.2s
tween local time = 10.0 - 8.0 - 1.2 = 0.8s
```

If the tween duration is `2.0s`, raw progress is `0.4`.

## 6. Easing

Easing should be represented as a pure function:

```cpp
using EaseFn = double (*)(double progress);
```

The function must accept normalized input `0.0..1.0` and return an output normally in or near `0.0..1.0`. Some expressive eases may overshoot.

Recommended first set:

- linear,
- power1/2/3 in/out/inOut,
- sine in/out/inOut,
- back out,
- elastic out only if needed.

## 7. Property Track

A property track is the parsed internal representation of one animated property.

```cpp
struct PropertyTrack {
  TargetHandle target;
  PropertyId property;
  Value start;
  Value end;
  ValueUnit unit;
  PropertyAdapter* adapter;
};
```

In a native engine, keep the first version typed:

- `float x`,
- `float y`,
- `float opacity`,
- `float scale`,
- `float rotation`.

Avoid a string-heavy property system until the engine proves useful.

## 8. Timeline Composition

Timeline insertion should support:

- append,
- absolute position,
- relative position from current end,
- overlap with previous child,
- label-based position.

Minimal API:

```cpp
Timeline& to(TargetHandle target, TweenVars vars);
Timeline& add(Animation* animation, double at);
Timeline& addLabel(std::string name, double at);
Timeline& seek(double time);
Timeline& play();
Timeline& pause();
Timeline& reverse();
```

## 9. Ownership and Cleanup

Web GSAP can rely on garbage collection for many object lifetimes. A C/C++ implementation cannot.

Native design must define:

- who owns Animation objects,
- whether Timeline owns child animations,
- how a target invalidation is detected,
- how callbacks avoid dangling pointers,
- how animations are cancelled on view destruction,
- whether completed tweens auto-remove.

Recommended native rule:

```text
AnimationContext owns all animations created for a screen or component.
Destroying the context cancels and releases all child animations.
```

## 10. Conflict and Overwrite Policy

GSAP supports overwrite behavior to prevent multiple active tweens from fighting over the same target property.

Native PoC should implement a simple policy:

```text
When a new tween starts for (target, property), cancel the older active track for that pair.
```

More advanced policies can come later:

- allow non-conflicting properties,
- blend values,
- priority groups,
- explicit no-overwrite mode.

## 11. ScrollTrigger-Like Architecture

ScrollTrigger can be generalized as an external driver:

```text
External state source -> normalized progress -> animation playhead
```

For scroll:

```text
scrollY, layout metrics, viewport size -> progress 0..1 -> timeline.progress(progress)
```

For native TV focus:

```text
focus index, focus transition progress -> progress 0..1 -> timeline.progress(progress)
```

This suggests that native engines should separate ticker-driven playback from externally driven playback.

## 12. Architecture Risks

- Too much API surface before the core model is stable.
- String property parsing in C/C++ becoming expensive and brittle.
- Hidden ownership causing crashes on view destruction.
- Per-frame callbacks becoming application logic traps.
- Mixing layout mutation and animation rendering without ordering rules.
- Building scroll/pin behavior before a simple timeline engine works.

