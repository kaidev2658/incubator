# Target Architecture

## Core Components
- Planner Agent: 요청 해석, 작업 분해
- Worker Agent(s): 태스크 실행
- Tool Gateway: 외부 도구/API 호출 추상화
- Memory Layer: 세션 메모리 + 장기 컨텍스트
- Guardrail Layer: 권한/정책/검증
- Execution Runtime: 미니앱 생성 및 실행 엔진

## Key Flows
1. User Intent -> Planner
2. Plan -> Worker + Tool calls
3. Result synthesis -> App draft generation
4. Human approval -> Execution/Publish

## Open Questions
- Tizen 상 실행 모델(WebView/Native bridge) 선택
- 모델 추론 위치(클라우드/온디바이스 하이브리드)
- 앱 샌드박스 및 권한 정책
