# Wabi Product & Tech Brief

## 1) Product Definition
Wabi는 "personal software" 컨셉의 AI 기반 미니앱 생성/공유 플랫폼으로 포지셔닝된다. 사용자는 자연어로 앱을 생성하고, 생성된 결과를 리믹스/공유할 수 있다.

- 핵심 문구: "Create, discover, and remix any mini-app in minutes"
- ToS 정의: AI-powered app generation platform, Mini-App 중심 구조

## 2) Core Capabilities
- 자연어 기반 앱 생성
- 공개 앱 리믹스/재배포
- 모바일 앱 중심의 빠른 제작/실행 UX

## 3) Architecture Clues (Public)
공개 문서와 관련 콘텐츠 기준으로 다음 3계층 가설이 유효하다.
- Feed layer: 미니앱 탐색/공유
- AI engine layer: 프롬프트 -> 앱 생성
- User context layer("You OS" 관점): 사용자 맥락/선호 기반 개인화

> 주의: 상기 3계층 표현은 공식 메인 문서보다는 관련 발표/노트 기반 단서이며, 상세 런타임 구현은 공개되지 않음.

## 4) Integrations & Data Scope
정책 문서에 통합 가능 서비스/데이터 범주가 비교적 넓게 제시됨.
- Gmail, Google Calendar, Notion, LinkedIn, Reddit, X
- Apple Health, 수면/건강 데이터 등

## 5) Business Model / Pricing (Observed)
- App Store 표시: 무료
- ToS 단서: 구독/크레딧 기반 과금 가능 구조
- 투자: Pre-seed(외부 발표 기준)

## 6) Differentiators
- "앱을 콘텐츠처럼" 만들고 소비/리믹스하는 구조
- 코딩 진입장벽을 낮춘 생성 중심 UX
- 개인 맥락을 활용한 소프트웨어 생성 비전

## 7) Risks / Open Questions
- 민감 데이터 취급 범위 확장 시 프라이버시/거버넌스 리스크
- 생성 품질/보안/실행 격리 정책의 공개정보 부족
- 과금 모델/유료 전환 조건 불명확

## 8) Source Pointers
- https://wabi.ai/
- https://wabi.ai/terms
- https://wabi.ai/privacy
- https://a16z.com/announcement/investing-in-wabi/
- https://apps.apple.com/app/id6747768928
- (참고/비공식) https://wabiai.ai/4-wabis-product-shape-mini-apps-on-top-of-a-you-os.html
