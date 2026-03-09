# Target Architecture (PoC v1)

## 1) 설계 목표
- Tizen 10 단일기기에서 **생성 -> 실행 -> 관리** E2E를 안정적으로 검증
- partial update + rollback 필수
- 서버 중심 오케스트레이션, 클라이언트 경량화

## 2) 시스템 구성도 (Logical)
1. **Tizen Client (.NET C#)**
   - Prompt 입력 UI
   - Draft/Live 상태 뷰
   - Deploy / Update / Rollback 트리거
2. **API Gateway**
   - 인증/요청검증/레이트리밋
3. **Orchestrator**
   - Planner: 요청 해석, 작업 계획
   - Builder Worker: app schema 생성/수정
   - Validator: 스키마/권한/정책 검증
4. **Policy Engine**
   - 권한 화이트리스트
   - 민감 액션 승인 훅
5. **Artifact Store**
   - 앱 정의(manifest/ui_schema/actions/state_policy)
   - 버전(draft/live), 변경이력
6. **Runtime Adapter (Tizen)**
   - 앱 스키마를 Tizen UI/행동으로 해석
   - 권한 브로커 + 이벤트 루프
7. **Observability**
   - 생성성공률, E2E 성공률, 지연, rollback 성공률

## 3) 데이터 모델 (v1)
### 3.1 AppDefinition
```json
{
  "appId": "miniapp-uuid",
  "name": "Agenda Companion",
  "version": "1.0.0",
  "status": "draft",
  "manifest": {
    "permissions": ["location", "calendar.read", "contacts.read"],
    "minRuntime": "tizen10"
  },
  "ui_schema": {
    "layout": "4x2",
    "components": []
  },
  "actions": [],
  "state_policy": {
    "cache": "session",
    "sync": "server"
  },
  "meta": {
    "createdAt": "ISO8601",
    "updatedAt": "ISO8601"
  }
}
```

### 3.2 Version 모델
- `draft`: 편집 중
- `live`: 배포됨
- `previous_live`: rollback 대상

## 4) 핵심 API 계약 (v1)
- `POST /v1/apps/generate`
  - input: prompt, constraints
  - output: appId, draft version
- `POST /v1/apps/{appId}/update`
  - input: patch prompt or structured diff
  - output: new draft version
- `POST /v1/apps/{appId}/deploy`
  - input: draft version
  - output: live version + deployedAt
- `POST /v1/apps/{appId}/rollback`
  - output: restored live version
- `GET /v1/apps/{appId}`
  - output: latest app definition + states

## 5) 시퀀스 (SCN-01)
1. 사용자 prompt 입력
2. Generate 호출 -> draft 생성
3. 사용자 수정 요청 -> Update 호출
4. Validator(권한/스키마) 통과
5. Deploy 호출 -> live 반영
6. 오류 유도 시 Rollback -> previous_live 복구

## 6) 정책/보안 경계
- allow: location, calendar.read, contacts.read
- deny(v1): camera, microphone, bluetooth, calling
- 정책 위반 시 실행 차단 + 에러코드 반환
- 모든 배포/롤백 이벤트 감사로그 저장

## 7) 비기능 요구사항 (PoC 기준)
- 가용성보다 재현성 우선
- 평균 생성+배포 <= 12초 (PoC 환경)
- rollback 성공률 100%
- 장애 원인 추적 가능한 로그 필수

## 8) 오픈 이슈
- Tizen 런타임 렌더링 방식(네이티브 UI vs 웹뷰 혼합) 최종 선택
- auth 방식(로컬 토큰 vs 외부 OAuth) 단순화 여부
- 스키마 마이그레이션 전략(v2)
