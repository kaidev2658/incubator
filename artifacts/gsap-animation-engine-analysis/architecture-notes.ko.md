# GSAP 아키텍처 노트

## 1. 개념 아키텍처

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

이 구조가 핵심 교훈이다. Animation engine은 특정 UI component type에 직접 묶이면 안 된다. Engine은 시간과 값을 계산하고, property 적용은 adapter에 위임해야 한다.

## 2. 주요 Runtime Object

### Tween

하나 이상의 target에 있는 하나 이상의 property를 duration 동안 animate한다.

상태:

- targets
- property tracks
- start time
- duration
- delay
- local playhead time
- easing function
- repeat/yoyo configuration
- callbacks
- active/paused/completed flags

### Timeline

Tween이나 nested Timeline을 배치한다.

상태:

- child list
- labels
- child에서 파생된 duration
- child가 inherit하는 defaults
- local playhead
- repeat/yoyo/callback configuration
- parent timeline reference

### Ticker

Root timeline을 구동한다.

상태:

- time
- frame number
- delta time
- listeners
- lag smoothing settings
- optional FPS throttling

### Plugin

Core를 specialized parsing, lifecycle, rendering behavior로 확장한다.

예:

- CSS transform writer
- SVG path morph logic
- scroll progress adapter
- drag gesture adapter
- layout transition adapter

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

중요한 구현 포인트: first render는 다음 tick까지 지연될 수 있다. 이를 통해 여러 animated object에서 geometry read/write가 뒤섞이는 layout thrash를 줄일 수 있다. Native engine도 비슷하게 read와 write ordering을 관리해야 한다.

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

Animation system에는 최소 세 가지 time space가 필요하다.

- global time: engine clock
- timeline time: parent-controlled composition time
- local animation time: delay/repeat/yoyo mapping 이후 tween-specific time

예:

```text
global time = 10.0s
timeline start = 8.0s
tween start in timeline = 1.2s
tween local time = 10.0 - 8.0 - 1.2 = 0.8s
```

Tween duration이 `2.0s`라면 raw progress는 `0.4`다.

## 6. Easing

Easing은 pure function으로 표현하는 것이 좋다.

```cpp
using EaseFn = double (*)(double progress);
```

입력은 `0.0..1.0` normalized progress이고, 출력은 보통 `0.0..1.0` 근처다. 일부 expressive ease는 overshoot할 수 있다.

첫 버전 추천:

- linear
- power1/2/3 in/out/inOut
- sine in/out/inOut
- back out
- 필요할 경우 elastic out

## 7. Property Track

Property track은 하나의 animated property에 대한 parsed internal representation이다.

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

Native engine 첫 버전은 typed property를 유지하는 것이 좋다.

- `float x`
- `float y`
- `float opacity`
- `float scale`
- `float rotation`

String-heavy property system은 engine의 유용성이 확인된 뒤에 추가한다.

## 8. Timeline Composition

Timeline insertion은 최소한 다음을 지원해야 한다.

- append
- absolute position
- current end 기준 relative position
- previous child와 overlap
- label-based position

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

## 9. Ownership과 Cleanup

Web GSAP는 많은 object lifetime을 garbage collection에 맡길 수 있다. C/C++ 구현은 그럴 수 없다.

Native design은 다음을 정의해야 한다.

- Animation object 소유자
- Timeline이 child animation을 소유하는지 여부
- target invalidation 감지 방법
- callback dangling pointer 방지
- view destruction 시 animation cancel 방식
- 완료된 tween의 auto-remove 여부

추천 native rule:

```text
AnimationContext owns all animations created for a screen or component.
Destroying the context cancels and releases all child animations.
```

## 10. Conflict와 Overwrite Policy

GSAP는 같은 target property를 여러 tween이 동시에 건드리는 문제를 해결하기 위해 overwrite behavior를 제공한다.

Native PoC는 단순한 정책부터 시작한다.

```text
새 tween이 (target, property)에 시작되면, 같은 pair의 기존 active track을 cancel한다.
```

추후 확장:

- non-conflicting property 허용
- value blending
- priority group
- explicit no-overwrite mode

## 11. ScrollTrigger-like Architecture

ScrollTrigger는 external driver로 일반화할 수 있다.

```text
External state source -> normalized progress -> animation playhead
```

Scroll의 경우:

```text
scrollY, layout metrics, viewport size -> progress 0..1 -> timeline.progress(progress)
```

Native TV focus의 경우:

```text
focus index, focus transition progress -> progress 0..1 -> timeline.progress(progress)
```

따라서 native engine은 ticker-driven playback과 externally driven playback을 분리하는 것이 좋다.

## 12. Architecture Risk

- core model 안정 전 API surface를 너무 넓히는 것
- C/C++에서 string property parsing이 비싸고 취약해지는 것
- hidden ownership으로 view destruction 시 crash가 나는 것
- per-frame callback에 application logic이 과하게 들어가는 것
- layout mutation과 animation rendering의 ordering rule 없이 섞이는 것
- 단순 timeline engine이 안정되기 전에 scroll/pin behavior부터 만드는 것

