# Agentic App Platform 통합 기술 브리프 (KO)

## 0) 목적
- Wabi와 Nothing Essential Apps를 기준점으로,
- Tizen용 Agentic mini/micro-app 플랫폼 PoC 방향을 확정하기 위한 통합 분석.

## 1) Executive Summary
1. **Wabi**는 자연어 기반 미니앱 생성/리믹스 플랫폼의 전형적 레퍼런스다.
2. **Nothing Essential Apps**는 홈스크린 중심의 배포/실행 UX와 점진적 권한개방 전략이 강점이다.
3. Tizen PoC는 두 접근을 합쳐 **생성(Builder) + 안전한 실행(권한 화이트리스트) + 빠른 배포(홈 위젯형)** 으로 설계하는 것이 최단 경로다.

## 2) 비교 관점 요약
### 2.1 제품 철학
- Wabi: "personal software"와 생성/리믹스 네트워크
- Nothing Essential Apps: "home-first personal tools" + 실사용 안정화(Beta 운영)

### 2.2 빌더/업데이트 방식
- Wabi: 자연어 생성 중심(세부 구현 공개 제한)
- Nothing: 변경 시 전체 리셋 대신 부분 업데이트 + 롤백 강조

### 2.3 권한/플랫폼 전략
- Wabi: 통합 데이터 범위 넓음(정책상)
- Nothing: 초기 3권한(Location/Calendar read-only/Contacts)부터 시작해 단계적 확장

## 3) Tizen PoC 설계 권고
### 3.1 v1 아키텍처
- Planner/Worker(서버)
- Tool Gateway(권한/정책 게이트)
- Tizen Client(경량 렌더러/실행기)
- State Store(앱 메타/버전/권한 상태)

### 3.2 v1 기능 스코프
- Prompt -> mini-app draft 생성
- Partial update (요청 부위만 수정)
- Version rollback (N-1 복구)
- 홈 위젯형 배포

### 3.3 초기 권한 정책
- 허용: 위치, 캘린더 읽기, 연락처 읽기
- 보류: 카메라/마이크/블루투스/콜링
- 원칙: 최소권한 + 사용자 승인 + 감사로그

## 4) 검증 시나리오 (E2E)
1. 사용자: "내일 일정 + 이동 알림 위젯 만들어줘"
2. 생성: 입력/출력/알림 로직 포함 draft 생성
3. 수정: "주말엔 숨겨줘" 같은 규칙 수정(부분 업데이트)
4. 배포: 홈에 배치 후 실시간 반영
5. 실패 테스트: 권한 거부/네트워크 실패/데이터 누락

## 5) 리스크 및 대응
- 생성오류/환각 -> 템플릿 가드 + 스키마 검증
- 권한 남용 -> 화이트리스트 + 사용자 승인 단계
- 운영 불안정 -> 버전 롤백 + 장애 텔레메트리
- 개인정보 우려 -> 데이터 최소수집 + 보관기간 명시 + 삭제 API

## 6) 다음 액션 (우선순위)
1. `02_architecture/tooling-runtime-options.md` 작성
2. `03_poc/app`에 v1 런타임 뼈대 생성
3. `04_validation/test-plan.md`에서 성공/실패 지표 확정

## 7) Source Pointers
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
- https://nothing.tech/pages/privacy-policy
- https://kr.nothing.tech/pages/privacy-policy
