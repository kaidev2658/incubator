# Decisions (ADR-lite)

## Template
- Date:
- Decision:
- Context:
- Alternatives:
- Rationale:
- Impact:

---

## 2026-03-09
- Decision: 토픽 홈 표준 구조(Research/Architecture/PoC/Validation/Delivery) 채택
- Context: 기술조사와 빠른 Tizen PoC를 병행해야 함
- Rationale: 문서/코드/검증 결과를 분리해 속도와 추적성을 동시에 확보
- Impact: 이후 산출물은 해당 구조 기준으로만 추가

## 2026-03-09 (PoC v1 Scope Lock)
- Decision: 목표는 생성+실행 모두 포함(C)
- Context: 단순 조사보다 데모 가능한 실행 흐름이 필요
- Rationale: 미니앱 플랫폼의 실효성은 생성-실행-관리 E2E로 판단 가능
- Impact: PoC는 생성만이 아닌 실행/관리까지 포함

- Decision: 데모 목표는 "미니앱 생성하고 실행 및 관리 데모"
- Context: 대표님 요구사항
- Impact: Partial update, rollout, rollback 포함

- Decision: 타깃은 Tizen 10 단일기기 우선 (TV 또는 Public Tizen IoT headed)
- Context: 디바이스 타입보다 Tizen 10 동작 검증이 중요
- Impact: 기기 선택은 가용 장비 우선, 다기기 검증은 후속

- Decision: 아키텍처는 서버 중심 우선 접근
- Context: 구현 속도/운영 유연성 확보 필요
- Impact: 클라이언트는 경량 실행기 역할 중심

- Decision: 앱 모델(v1)은 .NET(C#) 우선
- Context: Tizen PoC 속도/안정성/구현 단순성
- Alternatives: Flutter
- Rationale: v1은 UX 완성도보다 파이프라인 검증이 우선
- Impact: 초기 구현은 C# 기준, 필요시 Flutter는 v2 비교

- Decision: 오프라인 요구, 민감권한 심화정책은 v1 범위 제외
- Context: 빠른 검증 우선
- Impact: 네트워크 의존 허용, 정책 심화는 v2로 이관

- Decision: KPI 우선순위는 생성 성공률
- Context: PoC 성공 핵심지표 명확화
- Impact: Go/No-Go 기준
  - 생성 성공률 >= 80%
  - E2E 성공률 >= 70%
  - 평균 생성+배포 <= 12초 (PoC 환경)
  - Rollback 성공률 100%

- Decision: Scenario ID는 `SCN-01 Agentic Mini-App Platform on Tizen`
- Context: 테스트/로그/데모 자산 관리 식별자 필요
- Impact: 검증 산출물 태깅 통일
