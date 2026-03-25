# Runtime Architecture (Draft)

## High-Level

```text
Compose Runtime
   │
   ├─ UI Tree / State / Recomposition
   │
Platform Abstraction Layer
   ├─ Rendering Adapter (Surface, Frame Clock)
   ├─ Input Adapter (Remote/Touch/Key)
   ├─ Lifecycle Adapter (Start/Pause/Resume/Stop)
   └─ System Adapter (File/Network/Font/IME)
   │
Tizen Host Layer
   ├─ App Lifecycle Hooks
   ├─ Native Window / EGL / GL(Vulkan optional)
   └─ Packaging/Signing/Launch
```

## 모듈 제안
- `runtime-core`: compose runtime 접점, 추상 인터페이스
- `runtime-tizen`: tizen 실제 바인딩 구현
- `sample-app`: 검증용 데모 앱
- `tooling`: 빌드/실행/로그 수집 스크립트

## 핵심 인터페이스 (초안)
- `RenderHost`: surface init, frame pump, invalidate
- `InputHost`: key/touch -> compose event
- `LifecycleHost`: lifecycle transition callback
- `SystemHost`: file/network/font/resource bridge

## Runtime Adapter Layer (JVM Spike)

`03-implementation/runtime-spike`에는 디바이스 비의존 개발을 위한 JVM-only 어댑터 골격을 추가했다.

- `RuntimeAdapter`
  - 런타임 제어 API: `start/resume/pause/stop`
  - 입력/프레임 API: `dispatchInput`, `requestFrame`
- `RuntimeHost`
  - 호스트가 제공해야 하는 콜백: `onLifecycle`, `onInput`, `onFrame`, `onLog`
- 이벤트 모델
  - `LifecycleEvent` (`Start/Resume/Pause/Stop`)
  - `InputEvent` (`Key`, `Touch`)
  - `FrameEvent` (`number`, `reason`)
- 구현체
  - `RuntimeAdapterEngine`: 호스트와 이벤트를 연결하는 코디네이터
  - `MockRuntimeHost`: JVM 로그 기반 mock host
  - `MockHostScenario`: lifecycle/input/frame 이벤트 시퀀스 재생

이 레이어는 실제 Tizen 바인딩 없이도 런타임 흐름(상태 전이, 입력 처리, 프레임 요청)을 먼저 검증하기 위한 목적이다.

## 설계 원칙
1. Tizen 종속 코드는 `runtime-tizen`에 격리
2. 실험단계에서는 기능보다 관측성(로그/메트릭) 우선
3. 실패 시 Option2(KMP shared core + 별도 UI)로 즉시 전환 가능 구조 유지
