# Tizen Adaptation Strategy (PoC v1)

## 1) 대상 환경
- Target: Tizen 10 (TV 또는 Public Tizen IoT headed)
- Language: .NET (C#) 우선
- Single-device first

## 2) 클라이언트 구조 (경량)
### 2.1 Modules
1. `PromptModule`
   - 사용자 입력/수정 입력 수집
2. `AppStateModule`
   - draft/live/previous_live 상태 관리
3. `RuntimeRenderModule`
   - ui_schema -> 화면 구성
4. `ActionExecutor`
   - actions 실행(허용된 API만)
5. `PolicyBridge`
   - 권한 확인/승인 프롬프트/차단 처리
6. `SyncClient`
   - generate/update/deploy/rollback API 호출

### 2.2 이유
- Tizen 단에서는 "해석/실행"에 집중
- 복잡한 생성 추론/검증은 서버로 위임

## 3) Tizen API 맵핑 (v1)
- Location -> 허용
- Calendar read -> 허용
- Contacts read -> 허용
- Camera/Microphone/Bluetooth/Calling -> 차단(phase-1)

## 4) 렌더링 전략
- v1: 선언형 `ui_schema`를 표준 컴포넌트로 매핑
  - Text, List, Button, Timer, Badge 등 최소 셋
- 레이아웃:
  - 우선 2x2, 4x2 위젯형 레이아웃 지원

## 5) 배포/업데이트/복구 전략
1. Generate 결과는 항상 draft
2. Deploy 시 draft -> live 승격
3. Update는 diff 기반 draft 갱신
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
- [ ] C# Tizen 프로젝트 생성
- [ ] API Client 4종(generate/update/deploy/rollback)
- [ ] ui_schema 렌더러 최소 컴포넌트 세트
- [ ] policy bridge(allow/deny)
- [ ] rollback 버튼/흐름
- [ ] 이벤트 로깅(성공/실패/시간)
