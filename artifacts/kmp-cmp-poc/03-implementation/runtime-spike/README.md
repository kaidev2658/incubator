# runtime-spike

Compose -> Tizen 런타임 접점을 검증하기 위한 실행 스파이크 모듈.

## 현재 구현
- JVM 전용 런타임 어댑터 골격 (`RuntimeAdapter`, `RuntimeHost`)
- lifecycle/input/frame 이벤트 모델
- pre-device 검증용 `MockRuntimeHost` + `MockHostScenario`
- `shared-core` `GreetingUseCase`를 어댑터 부트 로그로 연결
- Compose/Tizen 실제 연동은 아직 미포함

## 실행
```bash
./gradlew :runtime-spike:run
```
