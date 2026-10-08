# PoC 설계: GSAP-inspired Mini Animation Engine

## 1. 목표

GSAP의 core idea에서 영감을 받은 작은 native animation engine을 설계한다.

- Tween
- Timeline
- Ticker
- Ease
- typed property adapter
- explicit lifecycle ownership

이 PoC는 GSAP의 web-specific API surface를 복사하지 않고도 native UI 환경에서 GSAP식 개발 모델을 어느 정도 재현할 수 있는지 검증한다.

## 2. Non-Goals

이 PoC는 다음을 시도하지 않는다.

- DOM selector targeting
- CSS parsing
- ScrollTrigger
- SVG morphing
- text splitting
- plugin marketplace-style extensibility
- GSAP full parity

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

첫 버전은 typed property를 사용한다.

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

Tizen/EFL adapter라면 다음처럼 mapping할 수 있다.

- `X`, `Y`: object geometry
- `Opacity`: alpha/color
- `Scale`/`Rotation`: 가능한 경우 transform/map mechanism

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

예:

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

Ticker는 platform event loop와 연결하는 작은 bridge여야 한다.

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

Engine은 특정 platform timer를 가정하지 않는다. Platform layer가 `onFrame()`을 호출한다.

## 8. Timeline Position Model

Minimal position form:

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

예:

```cpp
Position::end()
Position::absolute(1.25)
Position::after(0.50)
Position::overlap(0.20)
```

Label은 core model이 동작한 뒤 추가한다.

## 9. Playback Controls

필수 control:

- play
- pause
- reverse
- seek(seconds)
- progress(0..1)
- kill/cancel

추후 option:

- repeat
- yoyo
- repeat delay
- time scale
- callback scopes
- overwrite modes

## 10. Unit Tests

Deterministic test는 실제 UI 없이 가능해야 한다.

Fake target:

```cpp
struct FakeTarget {
  float x = 0;
  float opacity = 0;
};
```

Test case:

- linear interpolation at 0%, 50%, 100%
- basic ease monotonicity
- timeline child offset calculation
- reverse playback
- seek behavior
- target destroyed before completion
- overwrite policy for same target/property

## 11. Milestone Plan

### Milestone 1: Core math

- Ease functions
- Tween progress calculation
- Fake target adapter
- Unit tests

### Milestone 2: Timeline

- append
- absolute position
- overlap previous
- seek
- reverse

### Milestone 3: Platform adapter

- x/y/opacity를 real UI object에 write
- screen destroy 시 cancel
- basic demo: focus card enter/exit motion

### Milestone 4: Interaction driver

- focus transition progress driver
- 필요할 경우 scroll progress driver

## 12. Success Criteria

PoC 성공 기준:

- manual timer 없이 여러 coordinated animation을 정의할 수 있다.
- animation을 pause/reverse/seek할 수 있다.
- target destruction이 crash로 이어지지 않는다.
- common UI motion 코드가 충분히 compact하다.
- web animation model을 통째로 복사하는 것보다 engine이 작고 명확하다.

