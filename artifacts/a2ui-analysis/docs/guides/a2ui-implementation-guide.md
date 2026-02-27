# A2UI 구현 가이드 (실무 중심)

## 1. 권장 구현 패턴

### 패턴 A: 초기 렌더 + 부분 갱신
1) `surfaceUpdate` + `beginRendering`으로 초기 UI 구성
2) 이후 상태 변화는 `dataModelUpdate`로 최소 갱신

장점:
- 렌더링 비용 절감
- 상태 추적 간결
- 장애 복구가 쉬움

### 패턴 B: 작업 단계형 UI
- 단계별 surface를 분리(`step-1`, `step-2`, `result`)
- 단계 전환 시 `beginRendering` 대상 변경

장점:
- 복잡도 분리
- 사용성 향상

## 2. 실패 모드와 대응

### 실패 모드 1: 화면이 안 뜸
원인:
- `beginRendering` 누락
- 잘못된 `surfaceId`
- Canvas host 접근 실패

대응:
- 가장 단순한 텍스트 push로 스모크 테스트
- `surfaceId` 일관성 검사
- `a2ui_reset` 후 재시도

### 실패 모드 2: 일부 컴포넌트만 깨짐
원인:
- JSONL 라인 문법 오류
- data model path 불일치

대응:
- JSONL 라인 단위 검증
- 변경 전/후 스키마 diff 확인

### 실패 모드 3: 버전 호환 오류
원인:
- v0.9 전용 메시지 사용

대응:
- v0.8 스키마 고정
- 메시지 생성기에서 버전 분기

## 3. 디버깅 루틴
1) `a2ui_reset`
2) 단일 텍스트 메시지 테스트
3) `surfaceUpdate + beginRendering` 최소 페이로드
4) `dataModelUpdate` 단계적 추가
5) 마지막으로 복합 컴포넌트 적용

## 4. 보안 체크포인트
- Gateway 바인딩 범위(기본 loopback 권장)
- 인증 토큰/노출 범위 점검
- capability URL TTL/권한 범위 제한
- 외부 노출 시 reverse proxy + trusted proxy 설정
- 렌더링 페이로드 입력 검증(허용 스키마 기반)

## 5. 버전 전략
- 현재 운용은 A2UI v0.8을 기준으로 표준화
- 향후 v0.9+ 대비 메시지 어댑터 레이어 도입
- 내부 DSL(도메인 UI 선언) -> 버전별 변환기로 분리

## 6. 운영 제안
- 템플릿 카탈로그화(리포트, 승인, 상태보드)
- 실패 이벤트 관측성(에러율, 렌더링 실패율, 복구시간)
- 배포 전 테스트 시나리오 자동화(JSONL replay)

## 7. 참고
- https://docs.openclaw.ai/platforms/mac/canvas
- https://docs.openclaw.ai/gateway/configuration-reference
