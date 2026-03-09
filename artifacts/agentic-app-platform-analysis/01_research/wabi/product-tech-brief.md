# Wabi Product & Tech Brief

## 0) 범위 / 신뢰도 기준
- 소스: wabi.ai(공식), ToS/Privacy, a16z 발표, App Store, 사용자 제공 캡처
- 신뢰도 라벨
  - **[Confirmed]** 공식 문서/공식 채널
  - **[Likely]** 공식 정보 조합으로 강하게 추정
  - **[Open]** 공개정보 부족으로 미확정
  - **[Unofficial]** 사용자 제공 캡처/비공식 자료

## 1) 제품 정의
Wabi는 "personal software" 관점의 AI 기반 미니앱 생성/공유 플랫폼이다. 사용자는 자연어 요청으로 미니앱을 만들고 리믹스할 수 있다. **[Confirmed]**

## 2) 적용 기술(추정 포함)과 역할
### 2.1 생성 계층 (LLM + 제약기반 생성)
- 자연어 입력을 미니앱 구조로 변환하는 생성 파이프라인 존재. **[Confirmed/Likely]**
- 실서비스 안정성을 위해서는 보통 아래 단계가 필요:
  1) Intent 파싱
  2) 앱 스키마 생성(JSON/DSL)
  3) UI/액션 바인딩
  4) 정책 검사(권한/금지 API)
  5) 실행 아티팩트 생성
- 내부 모델/오케스트레이션 상세는 미공개. **[Open]**

### 2.2 실행 계층 (Mini-app Runtime)
- Wabi는 미니앱을 생성 후 즉시 사용/리믹스하는 UX를 제공하므로, 앱 실행을 위한 표준 런타임 계층이 필수. **[Likely]**
- 런타임에서 중요한 요소:
  - 앱 상태(state) 관리
  - 권한 브로커
  - 이벤트 루프(타이머/알림 등)
  - 렌더링 추상화(위젯/스크린)

### 2.3 배포 계층 (Publish/Remix)
- 앱을 콘텐츠처럼 소비·리믹스하려면 버전/메타데이터/호환성 정보를 포함한 배포 모델이 필요. **[Likely]**
- 핵심 메타데이터 예:
  - app_id, version, required_permissions, schema_version, author/remix lineage

## 3) 플랫폼 요소 관점 정리 (런타임/패키징/앱모델)
### 3.1 앱 모델 (권장 해석)
- "코드 직접 배포"보다 "선언형 앱 모델(DSL/JSON) + 런타임 해석"이 유력. **[Likely]**
- 이유: 생성/수정/리믹스 속도와 안정성 확보에 유리.

### 3.2 패키징 모델
- 미니앱 패키지는 일반적으로 아래 구성으로 단순화 가능:
  - `manifest` (권한/버전/호환성)
  - `ui_schema` (레이아웃/컴포넌트)
  - `actions` (트리거/툴 호출)
  - `state_policy` (캐시/보관/동기화)
- Wabi의 구체 포맷은 공개되지 않음. **[Open]**

### 3.3 런타임 경계
- 생성 계층(서버)과 실행 계층(클라이언트/앱)이 분리되어야 운영 안정성이 높음.
- 미니앱 플랫폼은 특히 "툴 호출 권한 경계"가 핵심.

## 4) Integrations & Data Scope
정책 문서에서 폭넓은 통합/데이터 범주가 보인다.
- Gmail, Google Calendar, Notion, LinkedIn, Reddit, X
- Apple Health(건강/수면 데이터)
=> 개인정보/민감정보 거버넌스가 제품 신뢰의 핵심. **[Confirmed]**

## 5) 기술 장벽 (Engineering Barriers)
1. **생성 안정성**: 자유자연어를 실행 가능한 앱 구조로 일관되게 변환
2. **권한 보안성**: 미니앱 단위 최소권한/승인/감사로그 구현
3. **상태 일관성**: partial update 시 기존 상태/데이터 깨짐 방지
4. **버전 호환성**: schema migration + rollback 보장
5. **운영 비용**: 생성/검증/실행 단계별 추론비용 관리

## 6) 비즈니스/가격
- App Store 기준 무료 표시 **[Confirmed]**
- ToS상 구독/크레딧 과금 가능 구조 단서 **[Confirmed]**
- 투자(Pre-seed) 발표 존재 **[Confirmed]**

## 7) 실행 플로우 단서 (사용자 제공, 비공식)
대표님 공유 정보 기준 Wabi 실행 시나리오는 아래와 같다. (공식 교차검증 전까지 **[Unofficial]**)

1. Prompt 입력(원하는 앱 설명)
2. React Native 코드 자동 생성
3. Wabi 플랫폼 내 빌드/즉시 실행
4. APK/IPA 개별 패키징 후 스토어 배포가 아니라, Wabi 앱 내부 샌드박스 실행

### 기술적 해석
- "코드 생성 -> 샌드박스 런타임 실행" 파이프라인을 갖는 구조로 해석 가능
- 배포 단위가 전통 앱스토어 패키지가 아닌 플랫폼 내부 아티팩트일 가능성 높음
- 따라서 검증 포인트는 스토어 배포보다는 런타임 격리/권한경계/버전관리

## 8) 첨부 이미지 기반 추가 단서 (사용자 제공, 비공식)
다음은 대표님 첨부 캡처에서 관찰된 내용이며, 공식 교차검증 전까지 **[Unofficial]**로 유지한다.
- 내부 모델 비공개 응답
- 현재 React Native 앱만 지원 주장
- RN 선택 이유: iOS/Android 동시 타깃 + 모바일 네이티브 기능 접근
- 기능 허용/제한 단서
  - 가능: 카메라(롤), 위치, 알림, HealthKit, TTS, 타이머
  - 제한: 녹화/스트리밍, 외부 디바이스 연결, 인터랙티브 3D

## 9) Tizen PoC 시사점 (구체)
- RN 전용 전개라면 Tizen 직접 이식보다 **오케스트레이터 분리 + Tizen 어댑터**가 현실적
- Tizen 쪽 앱모델은 `manifest + ui_schema + actions` 형태로 단순화 권장
- partial update/rollback은 런타임 핵심 기능으로 반드시 포함
- 권한 화이트리스트(초기 3권한) + 승인 훅 + 감사로그를 기본 내장

## 10) 리스크 / 오픈이슈
- 모델 계층, 샌드박스, 보안검증 체계 공개정보 부족 **[Open]**
- 민감데이터 연동 시 보관/삭제/동의체계의 투명성 필요 **[Open]**
- 무료->유료 전환 타이밍/가격정책 불명확 **[Open]**

## 11) Source Pointers
- https://wabi.ai/
- https://wabi.ai/terms
- https://wabi.ai/privacy
- https://a16z.com/announcement/investing-in-wabi/
- https://apps.apple.com/app/id6747768928
- (참고/비공식) https://wabiai.ai/4-wabis-product-shape-mini-apps-on-top-of-a-you-os.html
- (사용자 제공 캡처, 비공식) 2026-03-09 Telegram 첨부 이미지 2종
