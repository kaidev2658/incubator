# Tizen Adaptation Strategy

## Constraints
- 디바이스별 런타임/권한 차이
- 네트워크/저장소/백그라운드 제약
- UI 프레임워크/배포 정책 차이

## Fast PoC Approach
- 최소 기능 우선: 생성 -> 실행 -> 결과 저장
- 서버 중심 오케스트레이션 + Tizen 클라이언트 경량화
- 로컬 캐시 + 재시도 전략

## Candidate Stack (PoC)
- Front: Tizen Web App (또는 .NET 기반 가능성 검토)
- Backend: Agent orchestrator + tool adapters
- Storage: lightweight state store

## Validation Checklist
- 기동 시간
- 기본 시나리오 성공률
- 오류 복구(재시도/타임아웃)
- 사용자 승인 흐름 동작
