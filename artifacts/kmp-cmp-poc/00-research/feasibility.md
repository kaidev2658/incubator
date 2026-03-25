# Feasibility Research — Compose/KMP on Tizen

## 결론 (초기)
- **기술적으로 완전 불가능은 아님**
- 다만 **공식 런타임 수준**은 대형 투자 필요
- 단기 POC는 **Experimental Runtime** 또는 **KMP 로직 공유 + Tizen 별도 UI**가 현실적

## 왜 어려운가
1. Compose Multiplatform의 공식 Tizen 타깃 부재
2. 렌더링/입력/생명주기 브릿지 직접 구현 필요
3. Kotlin/Native 타깃 및 툴체인 정합성 리스크
4. 패키징/서명/스토어/디바이스 파편화 대응 부담

## 검증 축
- A. Rendering: 프레임 루프/화면 출력 가능한가
- B. Input: 리모컨/터치/포커스 처리가 안정적인가
- C. Lifecycle: background/foreground 복귀 안정성
- D. Tooling: 빌드-실행-디버그 파이프라인

## 대안 비교
### Option 1) Compose Runtime 직접 포팅
- 장점: UI 공유 극대화
- 단점: 난이도/비용/유지보수 매우 큼

### Option 2) KMP 공유 + Tizen UI 별도
- 장점: 실현 가능성 높음, 제품화 속도 빠름
- 단점: UI 100% 공유 불가

### Option 3) Web 우회 (Compose Web/Wasm)
- 장점: Tizen Web app 채널 활용 가능
- 단점: 성능/입력/런타임 제약 확인 필요

## POC 권장 방향
1. Option 2로 즉시 사업성 확보
2. 병행으로 Option 1의 최소 스파이크 진행
3. 6주 후 Go/No-Go

## 확인 필요 항목
- 타깃 디바이스 세대/OS 버전 매트릭스
- Tizen SDK/CLI/서명 인증서 준비 상태
- 실제 배포 채널(스토어/사내배포) 제약
