# Tooling & Runtime Options (Detailed)

## 1) PoC v1 선택 요약
- Runtime model: **서버 중심 + Tizen 경량 실행기**
- App model: **선언형 스키마(JSON/DSL)**
- Packaging: **manifest + ui_schema + actions + state_policy**
- Language (client): **.NET C#**

## 2) 런타임 옵션 비교 (기술요소 중심)

## A. 서버 중심 (채택)
### 구조
- 서버: 생성/검증/버전/정책
- 클라이언트: 렌더/실행/상태 표시

### 장점
- 생성모델 교체 쉬움
- 보안/정책 중앙집중
- PoC 속도 빠름

### 단점
- 네트워크 의존
- 서버 장애 영향 큼

## B. 하이브리드
### 구조
- 일부 파싱/규칙검증 로컬 수행
- 생성/무거운 추론은 서버

### 장점
- 반응성 개선 가능
- 일부 오프라인 완화

### 단점
- 동기화/버전충돌 복잡
- 구현 복잡도 증가

## C. 온디바이스 중심
### 구조
- 생성/검증/실행 상당 부분 로컬

### 장점
- 프라이버시/오프라인 강점

### 단점
- 성능/메모리 제약
- 디바이스 편차 대응 비용 큼
- v1 일정에 부적합

## 3) 패키징 스펙 초안 (v1)
```json
{
  "manifest": {
    "appId": "miniapp-uuid",
    "version": "1.0.0",
    "permissions": ["location", "calendar.read", "contacts.read"],
    "runtime": "tizen10"
  },
  "ui_schema": {"layout": "4x2", "components": []},
  "actions": [],
  "state_policy": {"cache": "session", "sync": "server"}
}
```

## 4) 정책 엔진 규칙 (v1)
- allow: location, calendar.read, contacts.read
- deny: camera, microphone, bluetooth, calling
- 정책 위반 action은 배포 전 차단
- 민감 동작은 승인훅(required)

## 5) 관측성 최소 세트
- `generate.success_rate`
- `e2e.success_rate`
- `deploy.latency_ms`
- `rollback.success_rate`
- `policy.block.count`

## 6) Go/No-Go 기준 연동
- 생성 성공률 >= 80%
- E2E 성공률 >= 70%
- 생성+배포 평균 <= 12s
- rollback 성공률 100%

## 7) v2 확장 후보
- 하이브리드 로컬 검증기
- camera/microphone/bluetooth 단계적 오픈
- schema migration 자동화
