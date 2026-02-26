# The Intelligent OS: AppFunctions & UI Automation Deep Dive

**분석일:** 2026-02-26

## 1. 문제 정의: AI가 앱을 대체하지 않고 연결해야 하는 이유
Android 앱 생태계에서 사용자는 점점 단일 앱을 열어 단계를 밟기보다 AI 에이전트를 통해 결과를 얻으려 한다. 이때 과거처럼 개별 앱을 열어 데이터를 찾는 대신, AI가 앱의 기능을 자연어로 불러올 수 있어야 한다. 따라서 Android는 앱 단에서 AI와 상호작용할 수 있는 _Agent facing interface_를 제공해야 하며, 이 역할을 AppFunctions가 담당한다.

## 2. AppFunctions 구조
1. **Self-describing 함수 선언**
   - Jetpack AppFunctions 라이브러리(https://developer.android.com/ai/appfunctions)와 플랫폼 API를 이용해 앱은 기능을 `@AppFunction` 같은 어노테이션으로 선언.
   - 각 함수는 입력/출력 스키마, 권한, 실행 조건 등을 메타 데이터로 포함하며, agent는 이를 다운로드/캐싱한 뒤 자연어 요청에 매핑.
   - Google이 제시하는 옵셥은 서버 기반 능력을 기술하는 MCP와 유사하나, AppFunctions는 **디바이스 내부에서** Jetpack/Platform API로 바로 선언하고 Gemini와 같은 agent가 직접 실행.
2. **온디바이스 실행**
   - 함수는 Gemini가 단순히 스키마만 보고 API를 직접 호출하는 것이 아니라, Jetpack 라이브러리/플랫폼 API가 함수별로 진입점을 만들어 agent에게 전달.
   - 이는 WebMCP가 웹 ssh 형태로 실행하듯, AppFunctions는 앱이 자체 호스트한 `function handler`를 local process에서 실행시킨다.
3. **Galaxy S26 Gallery 사례**
   - Galaxy Gallery는 AppFunctions를 통해 `searchPhotos(tag)`, `getAlbum(name)` 등 함수를 노출.
   - Gemini가 “Show me pictures of my cat”이라는 명령을 받으면, 함수 리스트를 검색해 가장 적합한 `searchPhotos`를 실행하고 결과 이미지를 리턴.
   - 이 과정은 **멀티모달**: 텍스트/음성 입력, Gemini UI 내부에서 이미지 렌더링, 후속 메시지로 공유(예: 메시지 전송)까지 연결.
   - 이러한 흐름 덕분에 사용자는 Gallery를 열지 않고 Gemini 내에서 바로 작업 완료.

## 3. UI Automation 프레임워크
1. **추가 통합 없이 제어**
   - AppFunctions이 모든 통합을 커버하지 못하므로, Google은 일반 UI를 지능적으로 제어하는 **UI automation framework**도 병행해서 개발.
   - 이 프레임워크는 Android의 접근성/automation API를 활용해 화면 위 버튼/텍스트를 시뮬레이션하지 않고, **앱 구조를 이해하고 적절한 단계로 이동**.
2. **Galaxy S26 / Pixel 10 롱프레스 베타**
   - Galaxy S26과 select Pixel 10에서 파워 버튼을 길게 누르면 Gemini가 UI automation을 트리거.
   - 미국/Korea에서 food delivery, grocery, rideshare 앱에 대해 curated task(예: 특정 옵션 피자 주문, 복수 경유 rideshare, 마지막 장바구니) 자동화 제공.
3. **사용자 제어와 보안**
   - 자동화하는 동안 Gemini는 알림이나 Live View(실시간 UI 모니터)로 진행 상태를 보여주며, 언제든 사용자가 직접 제어로 전환 가능.
   - 민감한 구매/결제 작업은 Gemini가 사용자에게 명시적으로 확인을 요구해 불필요한 결제를 막음.
   - UI automation과 AppFunctions 모두 개인정보 보호를 중심으로 설계: 작업은 디바이스 내부에서만 실행되며, Gemini가 직접 컨텐츠를 획득하지 않고 각 앱이 데이터를 전달.

## 4. Android 17 향후 확장 방향
- 지금은 소수 개발자와 협업하며 high-quality integration을 실험 중이며, 연내 AppFunctions + UI automation을 앱 개발자가 쉽게 활용할 수 있도록 가이드/도구를 공개할 계획.
- Android 17에서 더 넓은 OEM/개발자/기기에서 Intelligent OS 경험을 사용할 수 있도록 API와 보안 모델을 확장.

## 5. 참고
- Android Developers Blog — The Intelligent OS: Making AI agents more helpful for Android apps: https://android-developers.googleblog.com/2026/02/the-intelligent-os-making-ai-agents.html?m=1

*작성자: K*
