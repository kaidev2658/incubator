# GSAP 기술 보고서

## 1. 목적

이 문서는 GSAP를 엔지니어링 관점에서 분석한다.

- GSAP가 무엇인지
- 어떤 구조로 되어 있는지
- animation이 어떻게 구동되는지
- 핵심 개념이 무엇인지
- 브라우저 native animation primitive와 무엇이 다른지
- native C/C++ UI animation engine에 어떤 아이디어를 가져갈 수 있는지

이 보고서는 GSAP를 시각 효과 모음이 아니라 시간 기반 property animation runtime으로 다룬다.

## 2. GSAP란 무엇인가

GSAP, GreenSock Animation Platform은 웹에서 높은 제어력을 가진 animation을 만들기 위한 JavaScript animation library다. 공식 문서 기준으로 core는 다양한 브라우저에서 빠르고 responsive한 animation을 만들기 위한 기반을 제공하며, Draggable, ScrollTrigger, MorphSVG 같은 추가 기능은 plugin으로 분리되어 있다.

GSAP는 framework-agnostic이다. 공식 설치 문서는 GSAP core와 plugin이 JavaScript file이기 때문에 React, Webflow, WordPress 등 다양한 JavaScript/web framework에서 사용할 수 있다고 설명한다.

Webflow 지원 이후 GSAP pricing page는 전체 GSAP library가 모든 사용자에게 무료라고 안내한다. 또한 원래 GSAP team이 Webflow에서 full-time으로 library를 유지한다고 설명한다.

## 3. 핵심 mental model

가장 유용한 mental model은 다음과 같다.

```text
GSAP = time axis + property interpolation + target adapters + plugin extensions
```

GSAP는 하나의 rendering backend에 묶여 있지 않다. 다음 대상을 animate할 수 있다.

- DOM/CSS property
- CSS transform
- SVG attribute
- scroll-linked progress
- 일반 JavaScript object property
- plugin이 관리하는 추상 대상

core runtime은 매 frame마다 같은 질문에 답한다.

```text
시간 T에서 각 animated property 값은 무엇이어야 하는가?
```

값을 계산한 뒤, CSS, DOM, SVG, object property, plugin-managed effect 같은 적절한 adapter를 통해 결과를 반영한다.

## 4. 핵심 개념

### 4.1 Target

Target은 animation 대상 object 또는 object collection이다.

```js
gsap.to(".card", { opacity: 1 });
gsap.to(elementRef, { x: 100 });
gsap.to([el1, el2], { scale: 1.1 });
gsap.to(counterObject, { value: 100 });
```

Target은 selector string, DOM element, array, 일반 object가 될 수 있다. Selector string은 GSAP가 matching element로 resolve한 뒤 animation한다.

### 4.2 Vars Object

Vars object는 animated property와 control option을 함께 담는 설정 객체다.

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

여기에는 domain property인 `x`, `opacity`와 runtime option인 `duration`, `delay`, `ease`, `onComplete`가 함께 들어간다. 사용성은 좋지만 engine 내부 구현에서는 property specification과 playback metadata를 분리하는 편이 좋다.

### 4.3 Tween

Tween은 animation work의 가장 작은 단위다. Tween은 다음 정보를 기록한다.

- target reference
- animate할 property
- start value
- end value
- duration
- easing function
- callback
- repeat/yoyo/overwrite/lifecycle flag

GSAP 공식 문서는 Tween을 high-performance property setter에 가깝게 설명한다. playhead가 이동하면 해당 시점의 올바른 property 값을 계산하고 target에 적용한다.

주요 생성 방식:

```js
gsap.to(target, vars);
gsap.from(target, vars);
gsap.fromTo(target, fromVars, toVars);
```

차이는 다음과 같다.

- `to()`: 현재값에서 지정값으로 이동
- `from()`: 지정값에서 현재값으로 이동
- `fromTo()`: 명시적 시작값에서 명시적 끝값으로 이동

### 4.4 Timeline

Timeline은 Tween과 중첩 Timeline을 담는 container다. 여러 animation을 하나의 time axis 위에 배치한다.

```js
const tl = gsap.timeline();

tl.to(".logo", { opacity: 1, y: 0, duration: 0.5 })
  .to(".title", { opacity: 1, y: 0, duration: 0.6 }, "-=0.2")
  .to(".button", { scale: 1, opacity: 1, duration: 0.4 });
```

Timeline은 delay chain 문제를 해결한다. Timeline이 없으면 뒤따르는 animation마다 delay를 수동으로 재계산해야 한다. Timeline이 있으면 relative placement를 sequence model로 표현할 수 있다.

Timeline position parameter는 GSAP의 핵심 문법이다.

- number: 초 단위 absolute time
- `+=1`: 현재 끝점에서 1초 뒤
- `-=0.3`: 0.3초 overlap
- `<`: 직전 animation 시작점에 맞춤
- `>`: 직전 animation 끝점에 맞춤
- label: 이름 붙은 timeline position

### 4.5 Playhead

Tween과 Timeline은 모두 playhead를 가진다. Playhead는 해당 animation 내부의 현재 시간 위치다.

```js
tl.pause();
tl.play();
tl.reverse();
tl.seek(1.2);
tl.progress(0.5);
```

따라서 GSAP animation은 한 번 실행하고 끝나는 선언이 아니라 runtime에서 제어 가능한 object다.

### 4.6 Ease

Easing은 normalized time progress를 실제 motion progress로 변환한다.

```text
linear progress: 0.0 -> 0.5 -> 1.0
eased progress:  0.0 -> 0.78 -> 1.0
```

기본 보간:

```text
rawProgress = elapsed / duration
progress = ease(rawProgress)
value = start + (end - start) * progress
```

실제 사용자에게 느껴지는 품질은 duration보다 ease에 더 크게 좌우되는 경우가 많다.

### 4.7 Ticker

Ticker는 GSAP의 frame driver다. 공식 문서는 `gsap.ticker`를 GSAP engine의 heartbeat라고 설명한다. 브라우저 `requestAnimationFrame` event마다 global timeline을 update하고 custom listener도 붙일 수 있다.

개념적 loop:

```js
function tick(now) {
  globalTimeline.render(now);
  requestAnimationFrame(tick);
}

requestAnimationFrame(tick);
```

실제 구현에는 더 많은 요소가 있다.

- frame number
- elapsed time
- delta time
- optional FPS limiting
- lag smoothing
- browser hidden-tab throttling
- listener priority
- fallback behavior

아키텍처 관점에서 중요한 것은 모든 active animation이 하나의 shared time source로 조정된다는 점이다.

### 4.8 Plugin

Plugin은 core engine을 특정 domain으로 확장한다. 공식 GSAP guidance는 plugin도 core와 같은 JavaScript file이며, `gsap.registerPlugin()`으로 등록하면 core와 bundler에서 안정적으로 동작한다고 설명한다.

```js
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";

gsap.registerPlugin(ScrollTrigger);
```

Plugin 구조 덕분에 core는 timing과 interpolation에 집중하고, domain-specific logic은 별도 layer에 둘 수 있다.

주요 plugin:

- ScrollTrigger: scroll-linked trigger/progress/pin/scrub/snap
- Draggable: drag gesture와 inertia interaction
- Flip: layout transition animation
- MotionPath: path를 따라 이동
- MorphSVG: SVG path shape interpolation
- DrawSVG: line drawing effect
- SplitText/Text/ScrambleText: text animation utility

## 5. Runtime Flow

간단한 tween의 runtime flow:

```text
User calls gsap.to(target, vars)
  -> target resolved
  -> tween object created
  -> property plugin/adapter selected
  -> initial render reads starting values
  -> end values and units parsed
  -> tween attached to a timeline
  -> ticker advances global timeline
  -> tween receives local playhead time
  -> raw progress calculated
  -> ease function maps raw progress
  -> interpolated values calculated
  -> values written to target properties
  -> callbacks fired when applicable
  -> completed disposable children may be removed
```

Native 구현에서도 이 lifecycle을 재현하는 것이 핵심이다.

## 6. Property Interpolation

GSAP의 강점 중 하나는 property 처리다. Property는 다음 형태일 수 있다.

- numeric
- unit-bearing (`px`, `%`, `deg`)
- color-like
- transform component
- SVG attribute
- embedded number를 포함한 string
- plugin-defined property

일반 pipeline:

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

Native C/C++에서는 core UI property에 대해서 문자열 parsing을 피하는 편이 좋다. typed property를 우선해야 한다.

```cpp
Property::X(float)
Property::Opacity(float)
Property::Scale(float)
Property::Rotation(float)
```

String-based property는 core가 아니라 extension layer로 미루는 것이 안전하다.

## 7. CSS Transform과 성능 모델

DOM animation에서 GSAP는 보통 transform/opacity 기반 animation을 권장한다.

- `x`, `y`: transform translation
- `scale`: transform scale
- `rotation`: transform rotation
- `opacity`: visual alpha

이는 transform과 opacity가 `left`, `top`, width, height 변경보다 layout recalculation을 덜 유발하고 compositor-friendly path를 탈 가능성이 크기 때문이다.

실무 guideline:

- frame-by-frame animation에는 transform/opacity를 우선한다.
- layout-heavy property animation은 꼭 필요할 때만 사용한다.
- layout thrash를 줄이기 위해 read/write를 batch 처리한다.

GSAP에는 first render 시점의 비효율적 read/write interleaving을 줄이기 위한 lazy rendering 같은 기능도 있다.

## 8. ScrollTrigger

ScrollTrigger는 scroll-to-time adapter로 이해하는 것이 가장 좋다.

```text
scroll position -> normalized progress -> tween/timeline playhead
```

예:

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

핵심 개념:

- `trigger`: 기준 element
- `start`: trigger가 시작되는 scroll position
- `end`: trigger가 끝나는 scroll position
- `scrub`: scroll progress와 animation progress 연결
- `pin`: scroll range 동안 element 고정
- `snap`: progress를 가까운 지점으로 이동
- callbacks: `onEnter`, `onLeave`, `onUpdate`, `onRefresh` 등
- refresh/recalculation: resize와 layout change 시 start/end position 갱신

어려운 것은 scroll detection이 아니다. 어려운 것은 scroll position, layout, viewport change, pinned spacing, refresh timing, animation progress를 일관되게 유지하는 것이다.

## 9. 브라우저 Native Option과 비교

### 9.1 CSS Transition/Animation

CSS는 다음에 좋다.

- 간단한 hover/focus transition
- 기본 enter/exit effect
- 반복 keyframe loop
- JavaScript가 적은 UI polish

한계:

- 복잡한 sequencing이 delay-heavy해진다.
- runtime reverse/seek/control이 불편하다.
- dynamic value calculation이 제한적이다.
- cross-element choreography가 어렵다.

### 9.2 Web Animations API

MDN은 Web Animations API를 DOM element animation을 위한 timing model과 animation model의 조합으로 설명한다. `Animation`, `KeyframeEffect`, `AnimationTimeline` 같은 object를 노출한다.

WAAPI는 CSS보다 GSAP에 가깝다. playback object를 제공하기 때문이다. 다만 GSAP는 더 넓은 authoring ergonomics, plugin ecosystem, target abstraction, timeline composition, 고수준 기능을 제공한다.

### 9.3 Raw requestAnimationFrame

MDN은 `requestAnimationFrame()`을 다음 repaint 전에 callback을 호출해 달라고 browser에 요청하는 method로 설명한다. 일반적으로 display refresh rate에 맞춰지고 background tab에서는 pause/throttle된다.

Raw rAF는 최대 자유도를 주지만 production animation system을 만들려면 다음을 직접 구현해야 한다.

- time normalization
- delta handling
- easing functions
- property adapters
- lifecycle management
- pause/resume/reverse/seek
- sequencing
- overwrite/conflict behavior
- cleanup
- scroll synchronization

GSAP는 rAF 위에 올라간 mature runtime layer로 볼 수 있다.

## 10. Framework Integration

### 10.1 React와 Next.js

GSAP는 React/Next.js에서 사용할 수 있지만 lifecycle 관리가 중요하다.

- DOM ref가 존재한 뒤 animation 생성
- selector scope를 component 내부로 제한
- unmount 시 animation cleanup
- strict/dev rendering에서 duplicate animation 방지
- server-side DOM access 방지

GSAP는 React helper pattern도 제공하지만, 핵심은 animation object lifecycle이 component tree lifecycle과 맞아야 한다는 점이다.

### 10.2 Webflow

Webflow가 GSAP의 접근성을 높이면서 GSAP는 design-heavy site에서 더 중요해졌다. 공식 pricing page는 Webflow support 덕분에 전체 library가 free for all users라고 안내한다.

디자인 중심 site에서는 GSAP가 designer-controlled layout과 code-driven advanced motion을 연결하는 bridge 역할을 한다.

### 10.3 Tizen Web App

Tizen Web App에서는 runtime이 필요한 JavaScript/rendering feature를 지원한다면 GSAP를 browser-side dependency처럼 사용할 수 있다.

적합한 경우:

- promotional UI
- animated TV experience
- web-authored landing-like screen
- design 변경이 잦은 빠른 iteration

주요 고려사항:

- device browser engine version
- memory/CPU limit
- remote-control focus UX
- GPU/compositing behavior
- startup time
- certification/platform constraint

## 11. 장점

- Timeline 기반 compositional model이 강하다.
- pause/play/reverse/seek/repeat/callback 제어가 풍부하다.
- easing과 property interpolation이 성숙하다.
- CSS뿐 아니라 다양한 target type을 animate할 수 있다.
- plugin system이 강력하다.
- ScrollTrigger ecosystem이 크다.
- 복잡한 motion authoring ergonomics가 좋다.
- 문서와 community example이 풍부하다.
- 현재는 과거 유료 plugin 장벽이 사라졌다.

## 12. 한계와 리스크

- motion discipline이 없으면 과한 animation을 유도할 수 있다.
- Scroll-driven effect는 layout change가 많을 때 취약해질 수 있다.
- 복잡한 timeline은 naming/structure 없이 유지보수가 어려워진다.
- framework lifecycle을 잘못 쓰면 duplicated animation이나 memory leak이 생긴다.
- heavy animation은 low-end device 성능 문제를 드러낸다.
- GSAP를 쓴다고 reduced-motion 같은 접근성 고려가 사라지지 않는다.
- Native UI 환경에서는 web API를 그대로 복사할 수 없고 architectural model만 깔끔하게 이전된다.

## 13. Engineering Takeaways

Native engine design에 가져갈 아이디어:

- animation을 time-controlled object로 다룬다.
- single frame driver를 둔다.
- sequencing과 property interpolation을 분리한다.
- typed property adapter를 사용한다.
- playhead control을 first-class로 만든다.
- cleanup과 ownership을 명확히 한다.
- callback을 지원하되 per-frame callback에 business logic이 과하게 의존하지 않게 한다.
- core Tween/Timeline model이 안정된 뒤 plugin-like extension을 추가한다.

초기에 피해야 할 것:

- full selector-based targeting
- string-heavy property parsing
- ScrollTrigger급 behavior
- 넓은 plugin surface
- 복잡한 SVG/text feature

## 14. 도입 판단 기준

Web:

- 단순 UI transition은 CSS.
- browser-native animation object만 필요하면 WAAPI.
- timeline, ScrollTrigger, advanced SVG/text/path effect, 빠른 design iteration이 중요하면 GSAP.

Tizen/native:

- 표준 제품 UI motion은 native primitive.
- 여러 화면에서 coordinated animation이 필요할 때만 작은 GSAP-like abstraction.
- full runtime 전에 작은 engine PoC.
- scroll-driven storytelling이 필수 요구가 아니라면 ScrollTrigger-like behavior는 첫 milestone에서 제외.

## 15. 최종 판단

GSAP의 가치는 box를 움직이는 데 있지 않다. 핵심 가치는 motion을 controllable, composable runtime model로 바꾸는 데 있다. 이 모델은 native C/C++ UI engine 설계에 유용하지만, 같은 개발 경험을 만들려면 platform widget 위에 별도 animation layer를 만들어야 한다.
