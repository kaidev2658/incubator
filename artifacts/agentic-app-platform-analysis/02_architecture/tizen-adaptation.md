# Tizen Adaptation Strategy (PoC v1)

## 1) 대상 환경
- Target: Tizen 10 (TV 또는 Public Tizen IoT headed)
- Language: .NET (C#) 우선
- Single-device first

## 2) 전환 단계 (Console Mock -> UI Scaffold)
- 현재 단계는 **runtime mock + UI scaffold 공존** 단계다.
- SCN-01 핵심 도메인 로직은 `03_poc/app/shared/`로 분리해 재사용한다.
- `TizenMiniAppRuntimeMock`은 시나리오 검증/운영 로그 기준선 역할을 유지한다.
- `TizenMiniAppUiScaffold`는 실제 Tizen view layer로 교체 가능한 presenter 기반 화면 상태 출력 계층을 제공한다.

## 3) 모듈 구조 (Transition)
1. `shared/PromptEngine`
   - 사용자 입력 기반 draft 생성, partial update
2. `shared/Scn01LifecycleService`
   - generate/update/deploy/rollback 상태 전이 orchestration
3. `shared/PolicyEvaluator`
   - API metadata index allow-list 기반 정책 검증
4. `shared/KpiTracker`
   - generate/e2e/deploy latency/rollback KPI 집계
5. `TizenMiniAppUiScaffold/ScreenStatePresenter`
   - `PromptInput`, `DraftPreview`, `LiveView`, `ValidationPanel` 구조화 상태 출력

## 4) Tizen API 맵핑 (v1)
- Location -> 허용
- Calendar read -> 허용
- Contacts read -> 허용
- Camera/Microphone/Bluetooth/Calling -> 차단(phase-1)

## 5) 배포/업데이트/복구 전략
1. Generate 결과는 항상 draft
2. Deploy 시 draft -> live 승격
3. Update는 draft 버전 증가
4. 오류/품질 저하 시 live -> previous_live rollback

## 6) 장애 대응 전략
- 네트워크 실패: 재시도(지수 백오프, 최대 3회)
- 정책 위반: 즉시 차단 + 사용자 안내
- 렌더링 오류: fallback UI + 이벤트 로그

## 7) 성능 목표 (PoC)
- 앱 정의 수신 후 초기 렌더 < 1.5s
- 사용자 액션 반응 < 300ms (로컬 처리)
- 전체 생성+배포 평균 <= 12s

## 8) 구현 체크리스트
- [x] C# runtime mock 프로젝트
- [x] C# UI scaffold 프로젝트
- [x] shared SCN-01 lifecycle domain 분리
- [x] policy bridge(allow/deny)
- [x] rollback 흐름
- [x] KPI 집계
- [ ] 실제 Tizen UI binding
