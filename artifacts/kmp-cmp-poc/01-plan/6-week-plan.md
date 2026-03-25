# 6-Week Execution Plan

## Week 1 — Architecture Spike
- 런타임 레이어 분해 (Compose Runtime / Platform Adapter / Tizen Host)
- 리스크 식별 + 중단 조건 정의
- 산출물: 아키텍처 다이어그램, 리스크 레지스터 v1

## Week 2 — Render Boot
- 최소 윈도우/서피스 부팅
- 단일 Compose 화면 렌더링 시도
- 산출물: 부팅 로그, 렌더링 성공/실패 리포트

## Week 3 — Input & Focus
- 리모컨 키 이벤트 매핑
- 포커스 이동/선택 동작 검증
- 산출물: 입력 테스트 스위트 초안

## Week 4 — Lifecycle & Platform IO
- lifecycle 전환(백/포그라운드) 처리
- 파일/네트워크 최소 브릿지
- 산출물: lifecycle 안정성 리포트

## Week 5 — Demo App
- 목록/상세 + 네트워크 호출 데모
- 성능(시작시간, 반응지연) 측정
- 산출물: 데모 패키지/영상/측정치

## Week 6 — Go/No-Go Review
- 유지보수 비용 추정
- 기술 부채/갭 분석
- 산출물: 의사결정 문서 + 다음 분기 제안

## KPI
- 부팅 안정성: 10분 무크래시
- 입력 신뢰성: 주요 키 이벤트 누락 체감상 0
- 상호작용 지연: 200ms 내 목표

## 중단 조건
- 2주차 종료 시 렌더링 부팅 실패
- 4주차 종료 시 lifecycle 구조적 불안정 지속
