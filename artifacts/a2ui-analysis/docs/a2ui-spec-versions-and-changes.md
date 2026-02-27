# A2UI 스펙 버전 및 변경사항 분석

작성일: 2026-02-27

## 1) 버전 현황

| 버전 | 상태 | 근거 |
|---|---|---|
| v0.8.2 | 이전 버전(활성 개발 아님) | https://github.com/google/A2UI/blob/main/specification/v0_8/README.md |
| v0.9 | 게시됨(활성 개발 아님) | https://github.com/google/A2UI/blob/main/specification/v0_9/README.md |
| v0.10 | 개발 중 | https://github.com/google/A2UI/blob/main/specification/v0_10/README.md |

## 2) v0.8 -> v0.9 핵심 변경
근거 문서: https://github.com/google/A2UI/blob/main/specification/v0_9/docs/evolution_guide.md

- 메시지 타입 개편
  - v0.8: `beginRendering`, `surfaceUpdate`, `dataModelUpdate`
  - v0.9: `createSurface`, `updateComponents`, `updateDataModel`, `deleteSurface`
- Surface 생성 방식 전환
  - v0.8은 beginRendering 중심
  - v0.9는 createSurface 중심
- 컴포넌트 표현 변경
  - v0.8: key-wrapper 형태
  - v0.9: discriminator/property 기반
- Data model 구조 변경
  - v0.8: key-value 배열 성격
  - v0.9: 표준 JSON 오브젝트 중심
- Catalog 구조
  - v0.8: 분리형
  - v0.9: unified catalog

## 3) v0.9 -> v0.10 관찰 포인트
v0.10은 개발 중 상태이므로 확정 스펙으로 단정하기 어렵다. 구현 시에는 아래 원칙을 권장:
- 버전 고정(v0.8 또는 v0.9) 운용
- 어댑터 계층으로 상위 버전 메시지 대응
- 회귀 테스트로 버전별 동작 분리

## 4) OpenClaw 맥락 보강
OpenClaw Canvas 문서 기준:
- 현재 A2UI v0.8 수용
- v0.9의 `createSurface` 미지원
근거: https://docs.openclaw.ai/platforms/mac/canvas

따라서 OpenClaw 연동에서는 아래 매핑이 필요하다.
- v0.9 `createSurface` -> v0.8 `surfaceUpdate + beginRendering`
- v0.9 `updateComponents` -> v0.8 `surfaceUpdate`
- v0.9 `updateDataModel` -> v0.8 `dataModelUpdate`

## 5) 버전별 수정사항 정리(실무 체크리스트)
1. 메시지 스키마 버전 식별 로직 추가
2. 파서 레벨에서 버전별 디스패치 분기
3. 컴포넌트 표현 변환기(v0.9 -> v0.8) 구현
4. data model 변환기 구현
5. 미지원 메시지 graceful-fail (`UNSUPPORTED_VERSION`)
6. 테스트 세트 버전 분리
   - v0.8 goldens
   - v0.9 compatibility

## 6) 참고 링크
- A2UI repo: https://github.com/google/A2UI
- v0.8: https://github.com/google/A2UI/blob/main/specification/v0_8/README.md
- v0.9: https://github.com/google/A2UI/blob/main/specification/v0_9/README.md
- v0.10: https://github.com/google/A2UI/blob/main/specification/v0_10/README.md
- Evolution Guide: https://github.com/google/A2UI/blob/main/specification/v0_9/docs/evolution_guide.md
- OpenClaw Canvas(A2UI): https://docs.openclaw.ai/platforms/mac/canvas
