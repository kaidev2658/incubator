# Progress Log

## 2026-03-19

### Completed
- 루트 멀티모듈 Gradle 스캐폴딩 추가
  - `settings.gradle.kts`
  - `build.gradle.kts`
  - `gradle.properties`
- `shared-core` KMP 모듈 생성
  - `GreetingUseCase` 및 공통 테스트 추가
- `runtime-spike` JVM 실행 모듈 생성
  - `main` 엔트리와 `shared-core` 연결
- 환경 점검 스크립트 추가: `scripts/check-prereqs.sh`
- Phase-0 실행 체크리스트 작성: `01-plan/phase-0-execution-checklist.md`
- runtime-spike pre-device 런타임 어댑터 골격 추가
  - `RuntimeAdapter` / `RuntimeHost` 인터페이스 도입
  - `LifecycleEvent`, `InputEvent`, `FrameEvent` 이벤트 모델 도입
  - `RuntimeAdapterEngine` 코디네이터 구현 (lifecycle/input/frame 흐름 연결)
  - `MockRuntimeHost`, `MockHostScenario` 추가 (JVM-only 이벤트 시뮬레이션)
  - `main` 데모 러너를 mock scenario 기반 로그 출력으로 교체
- 아키텍처 문서 보강: `02-architecture/runtime-architecture.md`에 adapter layer 상세 추가

### Local Validation
실행 커맨드와 결과:

1. `java -version`
- 결과: Temurin OpenJDK 25.0.2 확인
- Exit code: `0`

2. `gradle -v` (sdkman init 후)
- 결과: Gradle 9.4.0 확인
- Exit code: `0`

3. `gradle wrapper`
- 결과: wrapper 생성 성공
- Exit code: `0`

4. `./gradlew :shared-core:check`
- 결과: 성공 (테스트 포함 통과)
- Exit code: `0`

5. `./gradlew :runtime-spike:run`
- 결과: 성공, 출력 확인
  - `Hello, Runtime Spike`
  - `TODO: wire Compose runtime host once platform prerequisites are ready`
- Exit code: `0`

6. `./scripts/check-prereqs.sh`
- 결과: `java`만 검출됨. 비대화형 셸에서는 sdkman 경로 미적용으로 `gradle` 미검출, `sdkmanager`/`tizen` 미설치
- Exit code: `1`

7. `./gradlew :runtime-spike:run` (adapter skeleton 반영 후 재실행)
- 결과: 성공, mock host 시나리오 로그 출력 확인
  - `---- mock host event log (18 events) ----`
  - `[pre-device-jvm-host] lifecycle:Start`
  - `[pre-device-jvm-host] log:Hello, Runtime Adapter`
  - `[pre-device-jvm-host] frame:1 reason=initial-boot`
  - `[pre-device-jvm-host] ...`
  - `[pre-device-jvm-host] lifecycle:Stop`
- Exit code: `0`

8. `tizen version && sdb version && sdb devices`
- 결과:
  - `Tizen CLI 2.5.25`
  - `Smart Development Bridge version 4.2.36`
  - 현재 연결 디바이스 없음
- Exit code: `1` (`sdb devices`에서 no device 상태)

9. `./scripts/check-prereqs.sh` (개선 후)
- 결과: required 통과 (`java`, `gradlew`, `tizen`, `sdb`)
- optional만 미설치 (`sdkmanager`)
- Exit code: `0`

### In Progress
- Tizen SDK 설치 전 placeholder 단계 유지
- `check-prereqs.sh`를 sdkman/gradlew 친화적으로 개선 필요
- mock adapter를 Compose runtime bridge 형태로 확장하는 다음 단계 설계

### Next
1. 집/개발환경에서 Tizen SDK 설치
2. `tizen version`, `sdb version`, `sdb devices` 결과 기록
3. 필요 시 `check-prereqs.sh`에 sdkman/`./gradlew` 감지 로직 추가
4. runtime-spike adapter 이벤트를 Compose frame clock/invalidations와 연결하는 bridge 초안 추가
