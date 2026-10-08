# PoC Design: GSAP-Inspired Mini Animation Engine

## 1. Goal

Design a small native animation engine inspired by GSAP's core ideas:

- Tween,
- Timeline,
- Ticker,
- Ease,
- typed property adapters,
- explicit lifecycle ownership.

The PoC should prove whether GSAP's development model can be approximated in a native UI environment without copying GSAP's web-specific API surface.

## 2. Non-Goals

The PoC will not attempt:

- DOM selector targeting,
- CSS parsing,
- ScrollTrigger,
- SVG morphing,
- text splitting,
- plugin marketplace-style extensibility,
- full parity with GSAP.

## 3. Core Types

```cpp
class Animation {
public:
  virtual void render(double localTime) = 0;
  virtual double duration() const = 0;
  virtual void play() = 0;
  virtual void pause() = 0;
  virtual void reverse() = 0;
  virtual void seek(double seconds) = 0;
  virtual ~Animation() = default;
};
```

```cpp
class Tween : public Animation {
public:
  Tween(TargetHandle target, TweenVars vars);
  void render(double localTime) override;

private:
  std::vector<PropertyTrack> tracks;
  EaseFn ease;
  double durationSeconds;
};
```

```cpp
class Timeline : public Animation {
public:
  Timeline& to(TargetHandle target, TweenVars vars);
  Timeline& add(std::unique_ptr<Animation> child, Position position);
  void render(double localTime) override;

private:
  std::vector<TimelineChild> children;
  std::unordered_map<std::string, double> labels;
};
```

```cpp
class AnimationContext {
public:
  Timeline& timeline();
  void cancelAll();
  void tick(double nowSeconds);

private:
  Timeline root;
};
```

## 4. Property Model

Use typed properties at first:

```cpp
enum class PropertyId {
  X,
  Y,
  Opacity,
  Scale,
  Rotation
};
```

```cpp
struct PropertyTrack {
  TargetHandle target;
  PropertyId property;
  float startValue;
  float endValue;
  PropertyAdapter* adapter;

  float valueAt(double easedProgress) const {
    return startValue + (endValue - startValue) * easedProgress;
  }
};
```

## 5. Property Adapter

```cpp
class PropertyAdapter {
public:
  virtual bool read(TargetHandle target, PropertyId property, float* out) = 0;
  virtual bool write(TargetHandle target, PropertyId property, float value) = 0;
  virtual bool isAlive(TargetHandle target) const = 0;
  virtual ~PropertyAdapter() = default;
};
```

For Tizen/EFL, an adapter might map:

- `X`, `Y` to object geometry,
- `Opacity` to alpha/color,
- `Scale`/`Rotation` to transform/map mechanisms where supported.

## 6. TweenVars Builder

```cpp
class TweenVars {
public:
  TweenVars& x(float value);
  TweenVars& y(float value);
  TweenVars& opacity(float value);
  TweenVars& scale(float value);
  TweenVars& rotation(float value);
  TweenVars& duration(double seconds);
  TweenVars& ease(EaseFn fn);
  TweenVars& onComplete(std::function<void()> cb);
};
```

Example:

```cpp
context.timeline()
  .to(card, TweenVars()
    .x(80)
    .opacity(1.0f)
    .duration(0.35)
    .ease(Ease::Power2Out))
  .to(title, TweenVars()
    .y(0)
    .opacity(1.0f)
    .duration(0.25),
    Position::overlap(0.10));
```

## 7. Ticker Integration

The ticker should be a small bridge to the platform event loop.

```cpp
class Ticker {
public:
  void start();
  void stop();
  void onFrame(double nowSeconds);

private:
  AnimationContext* context;
  double lastTime = 0.0;
};
```

The engine should not assume a specific platform timer. The platform layer calls `onFrame()`.

## 8. Timeline Position Model

Support minimal position forms:

```cpp
struct Position {
  enum class Kind {
    End,
    Absolute,
    RelativeToEnd,
    OverlapPrevious
  };

  Kind kind;
  double value;
};
```

Examples:

```cpp
Position::end()
Position::absolute(1.25)
Position::after(0.50)
Position::overlap(0.20)
```

Labels can be added after the core model works.

## 9. Playback Controls

Required controls:

- play,
- pause,
- reverse,
- seek(seconds),
- progress(0..1),
- kill/cancel.

Optional for later:

- repeat,
- yoyo,
- repeat delay,
- time scale,
- callback scopes,
- overwrite modes.

## 10. Unit Tests

Deterministic tests should not require a real UI.

Test fake target:

```cpp
struct FakeTarget {
  float x = 0;
  float opacity = 0;
};
```

Test cases:

- linear interpolation at 0%, 50%, 100%,
- ease output monotonicity for basic eases,
- timeline child offset calculation,
- reverse playback,
- seek behavior,
- target destroyed before completion,
- overwrite policy for same target/property.

## 11. Milestone Plan

### Milestone 1: Core math

- Ease functions.
- Tween progress calculation.
- Fake target adapter.
- Unit tests.

### Milestone 2: Timeline

- append,
- absolute position,
- overlap previous,
- seek,
- reverse.

### Milestone 3: Platform adapter

- write x/y/opacity to real UI object.
- cancel on screen destroy.
- basic demo: focus card enter/exit motion.

### Milestone 4: Interaction driver

- focus transition progress driver,
- optional scroll progress driver if needed.

## 12. Success Criteria

The PoC succeeds if:

- a screen can define multiple coordinated animations without manual timers,
- animations can be paused, reversed, and seeked,
- target destruction does not crash,
- common UI motion uses compact code,
- the engine remains smaller and clearer than copying a web animation model wholesale.

