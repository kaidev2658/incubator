# Nothing OS Essential Apps Product & Tech Brief

## 0) 범위 / 신뢰도 기준
- 범위: 대표님 지정 3개 소스 기준
  1. https://nothing.community/en/d/52739-essential-apps-enters-beta
  2. https://playground.nothing.tech/apps
  3. https://www.youtube.com/watch?v=lgMkWKLbmbM
- 신뢰도 라벨
  - **[Confirmed]**: 공식 공지/공식 서비스 화면에서 직접 확인
  - **[Likely]**: 공식 문맥상 강하게 추정 가능
  - **[Open]**: 현재 공개정보만으로 불명확

## 1) 제품 정의
Essential Apps는 Nothing이 제시하는 **개인 맞춤형 소형 앱(홈스크린 중심) 생성/배포 경험**이다. 사용자가 자연어로 앱을 만들고, 폰 홈스크린에 배치해 맥락형 도구처럼 쓰는 접근이다. **[Confirmed]**

## 2) 적용 기술(관찰 기반)
### 2.1 Builder 기술 패턴
- 자연어 -> 앱 생성 흐름 제공 **[Confirmed]**
- 업데이트는 전체 재생성이 아니라 **부분 반영(partial update)** 강조 **[Confirmed]**
- 잘못된 업데이트 시 이전 버전 복구(rollback) 제공 **[Confirmed]**

### 2.2 배포/실행 기술 패턴
- 웹 Playground에서 빌드/배포를 수행하고 디바이스 홈스크린에 빠르게 반영 **[Confirmed]**
- 이는 "원격 빌더 + 디바이스 런타임" 분리 구조를 강하게 시사 **[Likely]**

### 2.3 권한 단계 개방 모델
- 현재 3권한(위치/캘린더읽기/연락처) 완전 지원 **[Confirmed]**
- 고위험/복잡 권한은 후속 릴리스로 지연 **[Confirmed]**
- 안정성 우선의 capability rollout 전략으로 해석 가능 **[Likely]**

## 3) 플랫폼 요소 관점 (런타임/패키징/앱모델)
### 3.1 앱모델
- Essential Apps는 전통 네이티브 앱보다 "위젯성 마이크로앱"에 가까움.
- 앱 정의는 선언형(레이아웃/데이터/액션)을 중심으로 구성될 가능성이 높음. **[Likely]**

### 3.2 패키징
- Beta 공지상 "live/draft state", 변경 이력, 복구 기능이 강조됨.
- 따라서 패키징/버전 단위는 최소 아래를 포함할 가능성:
  - manifest(version, permissions)
  - UI spec
  - action spec
  - deployment state(live/draft)

### 3.3 런타임
- 홈스크린 상시성, 빠른 배포, 부분 업데이트를 감안하면
  - 경량 렌더러
  - 상태 동기화
  - 권한 브로커
  - 버전 관리기
  가 핵심 구성요소로 추정 가능.

## 4) 권한/기능 지원 범위 (Beta 시점)
### 4.1 현재 완전 지원 권한
- Location
- Calendar (read-only)
- Contacts
→ 위치 기반 리마인더, 아젠다 뷰, 미팅 카운트다운, 원탭 연락처 위젯 등 시나리오 가능. **[Confirmed]**

### 4.2 준비 중(공개 예고)
- camera/microphone, network fetching, notifications, vibration, calling, Bluetooth 등은 존재하지만 안정화 후 공개 예정으로 안내. **[Confirmed]**
- OS 업데이트로 activity recognition, usage statistics, sensor data, Weather API 일부 개방 예정 언급. **[Confirmed]**

## 5) 기술 장벽 (Engineering Barriers)
1. **부분 업데이트 안정성**: 기존 동작을 깨지 않으면서 변경 범위만 안전 반영
2. **권한 UX/보안**: 사용자 피로 없이 최소권한·명시동의·차단정책 유지
3. **디바이스 파편화 대응**: 성능/OS/권한 차이를 흡수하는 런타임 추상화
4. **운영 가시성**: draft/live 상태, 실패 원인, 복구이력 관리
5. **품질 통제**: 커뮤니티 생성 앱의 신뢰/안전성 검증 체계

## 6) 디바이스/롤아웃 전략
- Early Beta는 Phone (3) 우선 제공(성능/안정화 이유). **[Confirmed]**
- 안정화 후 Nothing/CMF + Nothing OS 4.0 이상으로 확장 계획. **[Confirmed]**
- 접근은 waitlist + batch 확대 방식. **[Confirmed]**

## 7) Tizen PoC에 대한 시사점 (구체)
### 7.1 바로 가져올 패턴
- Prompt -> App draft 생성
- Partial update + rollback
- live/draft 상태 구분
- one-tap deploy 개념

### 7.2 Tizen용 플랫폼 요소 권고
- App model: 선언형 스키마(JSON/DSL)
- Runtime: 경량 렌더러 + 권한브로커 + 상태 동기화기
- Packaging: manifest + ui_schema + actions + state_policy

### 7.3 최소 E2E 시나리오
- "내일 일정+이동시간 위젯 만들어줘" -> draft 생성 -> 수정 -> 배포 -> 홈 반영 -> rollback 검증

## 8) 리스크 / 오픈이슈
- 실행 샌드박스, 데이터 저장 위치, 감사로그 수준은 공개정보로 제한적. **[Open]**
- 모델 스택/추론 경로(온디바이스 vs 클라우드 상세)는 본 범위에서 확정 불가. **[Open]**
- Beta 단계 특성상 API/권한/지원 디바이스가 빠르게 변동될 가능성 높음. **[Likely]**

## 9) Source Pointers
- https://nothing.community/en/d/52739-essential-apps-enters-beta
- https://playground.nothing.tech/apps
- https://www.youtube.com/watch?v=lgMkWKLbmbM
- https://nothing.tech/pages/privacy-policy
- https://kr.nothing.tech/pages/privacy-policy
