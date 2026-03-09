# Tooling & Runtime Options (for Tizen PoC)

## 1) 목표
PoC 속도와 안정성을 균형 있게 맞추는 실행 조합을 결정한다.

## 2) 권장 기본안 (v1)
- Orchestrator: 서버 측 Planner/Worker
- Model access: 클라우드 API 우선
- Client: Tizen 경량 렌더러/실행기
- State store: 경량 DB (앱 메타/버전/권한 상태)
- Policy gate: 권한 화이트리스트 + 승인 훅

## 3) 옵션 비교
### A. 서버 중심 (권장)
- 장점: 빠른 실험, 모델 교체 쉬움, 보안통제 용이
- 단점: 네트워크 의존

### B. 하이브리드
- 장점: 일부 로컬 처리로 지연 감소
- 단점: 구현 복잡도 증가

### C. 온디바이스 중심
- 장점: 프라이버시/오프라인 강점
- 단점: 초기 PoC 속도 저하, 디바이스 편차 리스크

## 4) v1 권한 정책
- allow: location, calendar(read), contacts(read)
- deny(phase-1): camera, microphone, bluetooth, calling
- 모든 민감 호출: 사용자 승인 required

## 5) 의사결정 권고
- v1은 A(서버 중심)로 진행
- v2에서 하이브리드 전환 여부를 KPI 기준으로 결정
