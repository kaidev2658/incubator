# Phase-0 Execution Checklist

목표: Gradle 기반 최소 실행 경로를 만들고, 환경 전제조건을 명시적으로 검증한다.

## A. Scaffolding
- [x] 루트 멀티모듈 Gradle 설정 추가 (`settings.gradle.kts`, `build.gradle.kts`, `gradle.properties`)
- [x] `shared-core` KMP 모듈 생성 (공통 로직 + 테스트)
- [x] `runtime-spike` JVM 실행 모듈 생성 (`main` 엔트리)

## B. Local Commands
- [x] 사전 점검 스크립트 추가: `scripts/check-prereqs.sh`
- [x] `./gradlew :shared-core:check`
- [x] `./gradlew :runtime-spike:run`

## C. Tizen SDK Placeholder (No Fake Integration)
- [ ] `tizen version` 확인
- [ ] `sdkmanager --list` 또는 조직 표준 설치 경로 점검
- [ ] 디바이스/에뮬레이터 연결 커맨드 검증 (`sdb devices` 등)

## D. Exit Criteria (Phase-0)
- [ ] 공통 모듈 테스트 통과
- [ ] 런타임 스파이크 JVM 실행 출력 확보
- [ ] Tizen SDK 준비 상태를 PASS/FAIL로 기록
