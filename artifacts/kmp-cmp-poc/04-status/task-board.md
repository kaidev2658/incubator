# Task Board

## TODO
- [ ] 디바이스/에뮬레이터 연결 확인 (`sdb devices`)
- [ ] 첫 렌더링 부팅 로그 확보
- [ ] 리모컨 키 매핑 테스트
- [ ] runtime-spike adapter를 Compose runtime frame clock/invalidations 브리지로 확장

## DOING
- [ ] Tizen SDK 설치 대기 상태에서 mock adapter 이벤트를 Compose 접점으로 연결하는 설계 구체화

## DONE
- [x] JDK 설치 및 JVM 런타임 정상화 (`java -version`)
- [x] Gradle 설치 및 wrapper 생성 (`gradle wrapper`)
- [x] `:shared-core` 테스트 실행 결과 확보
- [x] `:runtime-spike` 실행 로그 확보
- [x] runtime-spike/shared-core Gradle 스캐폴드 추가
- [x] shared-core 샘플 유스케이스 + 테스트 추가
- [x] runtime-spike 최소 실행 엔트리 추가
- [x] runtime-spike JVM-only runtime adapter skeleton 추가 (`RuntimeAdapter`, `RuntimeHost`, 이벤트 모델)
- [x] `MockRuntimeHost`/`MockHostScenario` 기반 lifecycle/input/frame 시뮬레이션 추가
- [x] `./gradlew :runtime-spike:run`으로 adapter demo 로그 확인
- [x] Tizen CLI/SDB 설치 및 버전 점검 (`tizen 2.5.25`, `sdb 4.2.36`)
- [x] `check-prereqs.sh` sdkman/gradlew/tizen 경로 감지 보완
- [x] root runbook/prereq 문서화
- [x] Phase-0 실행 체크리스트 문서화
