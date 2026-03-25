# kmp-cmp-poc

Kotlin Multiplatform + Compose Multiplatform의 Tizen 확장 가능성을 검증하는 POC 워크스페이스.

## Current Build Scope (Phase-0)
- `:shared-core` - Kotlin Multiplatform 공통 로직 모듈 (현재 `jvm()` 타깃 + 공통 테스트)
- `:runtime-spike` - JVM 실행 스파이크 모듈 (`shared-core` 의존)

## Runbook

### 1) Prerequisites check
```bash
./scripts/check-prereqs.sh
```

### 2) Gradle wrapper bootstrap (once)
로컬에 Gradle 설치 후 1회 실행:
```bash
gradle wrapper
```

### 3) Build/test commands
```bash
./gradlew :shared-core:check
./gradlew :runtime-spike:run
./gradlew check
```

## Tizen SDK Placeholder Checks (intentional)
Phase-0에서는 SDK 통합을 구현하지 않고 준비 상태만 확인한다.

```bash
tizen version
sdkmanager --list
sdb devices
```

## Folder Map
- `00-research/` 기술 조사
- `01-plan/` 실행 계획/체크리스트
- `02-architecture/` 아키텍처 설계
- `03-implementation/` 구현 스파이크
- `04-status/` 진행 현황
- `05-decisions/` ADR
- `06-risks/` 리스크 레지스터
