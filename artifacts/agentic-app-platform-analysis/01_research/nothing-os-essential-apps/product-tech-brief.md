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

## 2) 현재 제품 형태 (Beta 기준)
### 2.1 Builder + Playground
- Builder는 Playground 안에서 동작하며, 자연어 설명으로 앱을 생성한다. **[Confirmed]**
- 업데이트 시 전체 리셋이 아니라 요청한 부분만 변경하고, 이전 버전 복원(원클릭) 흐름을 제공한다. **[Confirmed]**
- 웹 Playground에서 탐색/공유/배포하고, 향후 네이티브 Playground 앱 출시 계획이 언급됨. **[Confirmed]**

### 2.2 배포/실행 UX
- 앱 준비 후 탭 한 번으로 폰에 배포되고 홈스크린에 즉시 표시되는 흐름을 제공한다. **[Confirmed]**
- Essential Apps는 "앱을 열어 탐색"보다 "작은 도구가 홈에서 바로 반응"하는 사용성을 지향한다. **[Confirmed]**

## 3) 권한/기능 지원 범위 (Beta 시점)
### 3.1 현재 완전 지원 권한
- Location
- Calendar (read-only)
- Contacts
→ 위치 기반 리마인더, 아젠다 뷰, 미팅 카운트다운, 원탭 연락처 위젯 등 시나리오 가능. **[Confirmed]**

### 3.2 준비 중(공개 예고)
- camera/microphone, network fetching, notifications, vibration, calling, Bluetooth 등은 존재하지만 안정화 후 공개 예정으로 안내. **[Confirmed]**
- OS 업데이트로 activity recognition, usage statistics, sensor data, Weather API 일부 개방 예정 언급. **[Confirmed]**

## 4) 디바이스/롤아웃 전략
- Early Beta는 Phone (3) 우선 제공(성능/안정화 이유). **[Confirmed]**
- 안정화 후 Nothing/CMF + Nothing OS 4.0 이상으로 확장 계획. **[Confirmed]**
- 접근은 waitlist + batch 확대 방식. **[Confirmed]**

## 5) UI/디자인 시스템 관찰 포인트
- 현재 위젯 크기: 2x2, 4x2 제공 / 1x2, 4x4 예정. **[Confirmed]**
- 이미지 업로드: JPEG/PNG/GIF/WebP 지원, 아이콘/오디오/커스텀 폰트는 추후. **[Confirmed]**
- Playground 상에서 커뮤니티 앱 카드(제목/작성자/반응수) 및 정렬(Newest) 제공. **[Confirmed]**

## 6) 기술적 해석 (Agentic App Platform 관점)
1. **자연어-기반 앱 생성 + 부분 패치 업데이트**
   - 생성 품질보다 "안정적 반복 편집"을 먼저 해결하려는 방향.
2. **홈스크린 실행 레이어 우선**
   - 전통적 앱 탐색 UX 대신 컨텍스트 위젯형 미니앱 접근.
3. **권한 단계적 개방**
   - 초기에 안전한 권한만 개방하고, 고위험 권한은 Beta 후반으로 지연.

## 7) Tizen PoC에 대한 시사점
### 7.1 바로 가져올 패턴
- Prompt -> App draft 생성
- Partial update (diff-style) + rollback
- One-tap deploy(또는 quick publish) 개념
- 권한 화이트리스트(초기 최소권한)

### 7.2 Tizen형 초기 권한 세트 제안
- 위치, 캘린더 읽기, 연락처 읽기(가능 범위 내)
- 네트워크 fetch는 별도 게이트웨이 통해 통제
- 카메라/마이크/블루투스는 2단계 활성화

### 7.3 최소 E2E 시나리오
- "내일 일정+이동시간 위젯 만들어줘" -> draft 생성 -> 수정(색/레이아웃/알림) -> 배포 -> 홈에서 갱신 확인

## 8) 리스크 / 오픈이슈
- 생성된 앱의 실행 샌드박스, 데이터 저장 위치, 감사로그 수준은 공개정보로 제한적. **[Open]**
- 모델 스택/추론 경로(온디바이스 vs 클라우드 상세)는 본 범위에서 확정 불가. **[Open]**
- Beta 단계 특성상 API/권한/지원 디바이스가 빠르게 변동될 가능성 높음. **[Likely]**

## 9) Source Pointers
- https://nothing.community/en/d/52739-essential-apps-enters-beta
- https://playground.nothing.tech/apps
- https://www.youtube.com/watch?v=lgMkWKLbmbM
- https://nothing.tech/pages/privacy-policy
- https://kr.nothing.tech/pages/privacy-policy
