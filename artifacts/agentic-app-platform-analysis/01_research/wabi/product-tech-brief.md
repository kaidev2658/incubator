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

## 2) 핵심 기능
- 자연어 기반 앱 생성 **[Confirmed]**
- 앱 탐색/리믹스/공유 **[Confirmed]**
- 모바일 중심 제작/실행 경험 **[Likely]**

## 3) 구조적 단서 (Public)
공개 정보 기준으로 아래 3층 해석이 유효하다.
- Feed layer: 앱 탐색/공유
- AI engine layer: prompt -> app
- User context layer(You OS 관점): 개인화 컨텍스트
※ 세부 런타임/샌드박스 구현은 미공개. **[Open]**

## 4) Integrations & Data Scope
정책 문서에서 폭넓은 통합/데이터 범주가 보인다.
- Gmail, Google Calendar, Notion, LinkedIn, Reddit, X
- Apple Health(건강/수면 데이터)
=> 개인정보/민감정보 거버넌스가 제품 신뢰의 핵심. **[Confirmed]**

## 5) 비즈니스/가격
- App Store 기준 무료 표시 **[Confirmed]**
- ToS상 구독/크레딧 과금 가능 구조 단서 **[Confirmed]**
- 투자(Pre-seed) 발표 존재 **[Confirmed]**

## 6) 차별점
1. 앱을 콘텐츠처럼 만들고 소비/리믹스하는 네트워크형 구조
2. 코딩 대신 자연어+UI 반복수정 중심
3. 개인 컨텍스트 기반 소프트웨어 생성 비전

## 7) 첨부 이미지 기반 추가 단서 (사용자 제공, 비공식)
다음은 대표님 첨부 캡처에서 관찰된 내용이며, 공식 교차검증 전까지 **[Unofficial]**로 유지한다.

- 내부 모델 비공개 응답
- 현재 React Native 앱만 지원 주장
- RN 선택 이유: iOS/Android 동시 타깃 + 모바일 네이티브 기능 접근
- 기능 허용/제한 단서
  - 가능: 카메라(롤), 위치, 알림, HealthKit, TTS, 타이머
  - 제한: 녹화/스트리밍, 외부 디바이스 연결, 인터랙티브 3D

## 8) Tizen PoC 시사점
- RN 전용 전개라면 Tizen 직접 이식보다 **오케스트레이터 분리 + Tizen 어댑터**가 현실적
- 권한 화이트리스트/점진적 개방 모델이 초기 PoC 안정성에 유리
- "생성 -> 부분 업데이트 -> 복구" 패턴을 핵심 UX로 우선 도입

## 9) 리스크 / 오픈이슈
- 모델 계층, 샌드박스, 보안검증 체계 공개정보 부족 **[Open]**
- 민감데이터 연동 시 보관/삭제/동의체계의 투명성 필요 **[Open]**
- 무료->유료 전환 타이밍/가격정책 불명확 **[Open]**

## 10) Source Pointers
- https://wabi.ai/
- https://wabi.ai/terms
- https://wabi.ai/privacy
- https://a16z.com/announcement/investing-in-wabi/
- https://apps.apple.com/app/id6747768928
- (참고/비공식) https://wabiai.ai/4-wabis-product-shape-mini-apps-on-top-of-a-you-os.html
- (사용자 제공 캡처, 비공식) 2026-03-09 Telegram 첨부 이미지 2종
