# PoC

## Objective
Tizen 10에서 Wabi/Essential Apps와 유사한 Agentic mini-app 플랫폼의 생성/실행/관리 흐름을 빠르게 검증

## Demo Scenario v1
- Scenario ID: `SCN-01 Agentic Mini-App Platform on Tizen`
- Goal: "Tizen에서 Wabi 같은 에이전트 미니앱 플랫폼 데모"

### E2E Flow
1. 사용자 자연어 입력으로 미니앱 생성 요청
2. 에이전트가 앱 초안 생성
3. 부분 수정(Partial Update) 반영
4. 배포 후 실행
5. 버전 롤백(복구) 확인

## Platform / Runtime Decisions
- Target: Tizen 10 (TV 또는 Public Tizen IoT headed, 단일기기 우선)
- Architecture: 서버 중심 오케스트레이션 우선
- App model (v1): .NET (C#) 우선
- Out of scope (v1): 오프라인 필수 요구, 민감권한 심화정책

## Done Criteria
- 생성 성공률 KPI 충족(우선 지표)
- 생성→실행→관리(E2E) 성공
- Partial Update + Rollback 검증
- 실패 케이스 3개 이상 기록
- 재실행 가능한 스크립트 제공
