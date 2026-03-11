# Agentic App Platform 통합 기술 브리프 (KO)

## 0) 목적
- Wabi와 Nothing Essential Apps를 기준점으로,
- Tizen 10용 Agentic mini/micro-app 플랫폼 PoC 방향을 기술적으로 구체화.

## 1) Executive Summary
1. **Wabi**는 자연어 기반 생성 + 리믹스 네트워크 관점에서 강력한 레퍼런스.
2. **Essential Apps**는 앱 생성(프롬프트 기반)→draft/live 배포→partial update/rollback의 운영모델이 강점.
3. **Apps in Toss**는 SDK/API/콘솔/검수/출시/롤백이 결합된 상용 미니앱 운영모델이 강점.
4. Tizen PoC 핵심은 **선언형 앱모델 + 경량 런타임 + 서버 오케스트레이션** 조합.

## 2) 기술 스택 관점 비교
### 2.1 생성 계층 (Builder/LLM)
- 공통: 자연어 -> 앱 초안 생성
- 핵심 난제: 자유질의를 실행 가능한 구조로 안정 변환(스키마 검증 필수)

### 2.2 실행 계층 (Runtime)
- 공통 요구:
  - 상태 관리(state)
  - 권한 브로커(permission broker)
  - 이벤트 처리(타이머/알림)
  - 버전 관리(partial update + rollback)

### 2.3 배포 계층 (Packaging/Deploy)
- 공통 요구:
  - manifest 기반 권한/버전 관리
  - live/draft 상태 구분
  - one-tap 또는 quick publish 흐름

### 2.4 Essential Apps 모델 상세 (Nothing Playground 기준)
- 앱 모델: 홈스크린 중심의 마이크로앱(위젯형 UX에 가까운 경량 앱 경험)
- 생성 방식: 자연어 프롬프트 기반 초안 생성 후 draft 상태에서 반복 수정
- 배포 방식: Playground에서 live 반영, 변경은 partial update 우선
- 구동 방식: 허용된 capability 범위 내 실행 + 정책 기반 권한 단계 개방
- 복구 방식: 문제 시 이전 live 상태로 rollback
- 언어/구현 관점: 런타임 구현 언어와 별도로, 생성 산출물은 JSON/DSL 같은 선언형 중립 포맷 유지가 유리
- PWA 여부: 공개 페이지 정적 관찰 기준으로 manifest/service worker 직접 근거 부족. 현 시점 판정은 **Open**

## 2.5) Wabi 실행 흐름 단서(비공식)
- Prompt 입력 -> RN 코드 생성 -> 플랫폼 내 빌드/실행
- APK/IPA 개별 패키징 배포가 아닌 플랫폼 내부 샌드박스 실행 방식

### Tizen PoC 의미
- 스토어 배포를 목표로 잡기보다 "플랫폼 내부 실행기" 완성도가 우선
- 런타임 격리/권한경계/버전복구가 핵심 검증축

## 3) 미니앱/마이크로앱 플랫폼 필수 요소
1. **App Model(선언형)**
   - `manifest`, `ui_schema`, `actions`, `state_policy`
2. **Runtime Sandbox**
   - 허용 API 화이트리스트
   - 민감 권한 승인 훅
3. **Versioning**
   - partial update
   - rollback
   - schema migration
4. **Observability**
   - 생성 성공률, 배포 성공률, 오류코드, 복구이력

## 4) 기술 장벽 (공통)
1. 생성 안정성(환각/구조 불일치)
2. 권한/보안 경계
3. 디바이스·OS 파편화 대응
4. 버전 호환성/복구 보장
5. 운영비용(추론/검증/배포)

## 5) Tizen 10 PoC 권고 아키텍처
### 5.1 v1 구조
- Server: Planner/Worker + Tool Gateway + Policy Engine
- Client(Tizen): 경량 렌더러 + 실행기 + 상태 캐시
- Storage: app metadata/version state

### 5.2 앱 모델 v1 (권고)
```json
{
  "manifest": {"appId":"...","version":"1.0.0","permissions":["location","calendar.read","contacts.read"]},
  "ui_schema": {...},
  "actions": [...],
  "state_policy": {"cache":"session","sync":"server"}
}
```

### 5.3 배포/수정 정책
- 생성 결과는 `draft`
- 사용자 승인 후 `live`
- 수정은 diff 적용
- 실패 시 즉시 이전 `live`로 rollback

## 6) PoC 성공 기준 (합의 반영)
- 생성 성공률 >= 80%
- E2E 성공률 >= 70%
- 평균 생성+배포 <= 12초 (PoC 환경)
- Rollback 성공률 100%

## 7) 다음 액션 (실행 순서)
1. `app model schema` 초안 고정 (`03_poc/agent-core/schema`)
2. Tizen C# 런타임 뼈대 구현 (`03_poc/app`)
3. `generate/update/deploy/rollback` API 계약 정의
4. `04_validation`에 실패 케이스와 KPI 측정 자동화

## 8) Source Pointers
### Wabi
- https://wabi.ai/
- https://wabi.ai/terms
- https://wabi.ai/privacy
- https://a16z.com/announcement/investing-in-wabi/
- https://apps.apple.com/app/id6747768928
- (참고/비공식) https://wabiai.ai/4-wabis-product-shape-mini-apps-on-top-of-a-you-os.html
- (사용자 제공 캡처, 비공식) 2026-03-09 Telegram 첨부 이미지 2종

### Nothing Essential Apps
- https://nothing.community/en/d/52739-essential-apps-enters-beta
- https://playground.nothing.tech/apps
- https://www.youtube.com/watch?v=lgMkWKLbmbM
- https://developer.android.com/develop/ui/views/appwidgets/overview
- https://nothing.tech/pages/privacy-policy
- https://kr.nothing.tech/pages/privacy-policy

### Apps in Toss
- https://toss.im/apps-in-toss
- https://developers-apps-in-toss.toss.im
- https://github.com/toss/apps-in-toss-examples
- https://developers-apps-in-toss.toss.im/intro/overview.md
- https://developers-apps-in-toss.toss.im/tutorials/webview.md
- https://developers-apps-in-toss.toss.im/tutorials/react-native.md
- https://developers-apps-in-toss.toss.im/development/integration-process.md
- https://developers-apps-in-toss.toss.im/development/deploy.md
