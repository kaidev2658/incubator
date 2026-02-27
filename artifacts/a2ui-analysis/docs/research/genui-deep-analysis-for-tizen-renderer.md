# GenUI(flutter/genui) 심층 분석 — Tizen Renderer 구현 참고

작성일: 2026-02-27

## 1) 레포 구조 개요
근거: https://github.com/flutter/genui/blob/main/README.md

주요 패키지:
- `packages/genui`: 코어 런타임/엔진
- `packages/genui_a2a`: A2A 연결 브리지
- `packages/genai_primitives`: 공통 타입/프리미티브
- `packages/json_schema_builder`: 스키마/검증 유틸

핵심 시사점: **모듈 분리(파서/엔진/트랜스포트)** 구조가 명확해 이식성이 높다.

## 2) 핵심 렌더링 파이프라인
### 2.1 스트림 파싱
- `A2uiParserTransformer`가 텍스트 스트림에서 JSON 블록 추출
- 파싱 실패 시 fallback 동작 설계
근거:
- https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_parser_transformer.dart

### 2.2 트랜스포트 어댑터
- `A2uiTransportAdapter`가 chunk 스트림을 메시지 이벤트로 변환
근거:
- https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_transport_adapter.dart

### 2.3 Surface 상태 엔진
- `SurfaceController`가 surface lifecycle 처리
- create/update/delete/data model 업데이트 수행
- surface 생성 전 업데이트를 pending buffer에 저장하는 패턴 포함
근거:
- https://github.com/flutter/genui/blob/main/packages/genui/lib/src/engine/surface_controller.dart

## 3) Tizen renderer 구현 시 재사용할 패턴
### 패턴 A: 파서/엔진 분리
권장 구조:
1) Parser Layer (JSONL 유효성)
2) Protocol Dispatcher (버전/메시지 분기)
3) Render Engine (Tizen UI 트리 반영)

효과:
- 버전 확장 용이
- 디버깅 경계 명확
- 성능 병목 지점 분리 가능

### 패턴 B: Pending Update Buffer
surface 생성 전에 update가 들어오는 레이스 컨디션 방어.
- `surfaceId` 키로 큐잉
- 생성 완료 시 플러시
- TTL/최대 큐 크기 제한

### 패턴 C: 구조화된 오류 전파
- 파싱/검증/렌더 단계별 에러코드 분리
- 에이전트/상위 레이어에 기계 판독 가능한 오류 구조 전달

## 4) Tizen 특화 설계 제안
### 4.1 어댑터 계층
OpenClaw v0.8과 GenUI 중심(v0.9 흐름) 사이 어댑터 권장.

예시 매핑:
- `createSurface(v0.9)` -> `surfaceUpdate + beginRendering(v0.8)`
- `updateComponents(v0.9)` -> `surfaceUpdate(v0.8)`
- `updateDataModel(v0.9)` -> `dataModelUpdate(v0.8)`

### 4.2 렌더 트리 모델
- 내부 공통 IR(Intermediate Representation) 도입
- IR -> Tizen Native Widget 트리 변환
- 버전 차이는 Parser/Adapter에서 흡수

### 4.3 성능/안정성
- patch 적용 최소화(diff)
- frame budget 기반 업데이트 배치
- 장시간 실행 시 메모리 회수(lifecycle cleanup)

## 5) 테스트 전략 (GenUI에서 차용)
1. Unit
- JSONL 파싱
- 메시지 유효성
- 상태 전이

2. Integration
- 스트림 입력 -> surface 상태 일관성
- pending update flush 검증

3. E2E
- 실제 Tizen 디바이스 렌더 품질/지연
- 24h soak test
- 네트워크 재연결 복원

## 6) 구현 체크리스트 (Tizen Renderer 착수용)
- [ ] v0.8 메시지 파서 완성
- [ ] version-detection + adapter(v0.9 ingress 대비)
- [ ] surface registry + pending queue
- [ ] data model patch 엔진
- [ ] 렌더 실패 복구(reset/fallback)
- [ ] 구조화 에러코드/로그
- [ ] 성능 계측(초기 렌더/업데이트)
- [ ] 통합/E2E 테스트 파이프라인

## 7) 참고 링크
- GenUI repo: https://github.com/flutter/genui
- GenUI README: https://github.com/flutter/genui/blob/main/README.md
- Parser Transformer:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_parser_transformer.dart
- Transport Adapter:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_transport_adapter.dart
- Surface Controller:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/engine/surface_controller.dart
- A2UI repo: https://github.com/google/A2UI
- OpenClaw Canvas docs: https://docs.openclaw.ai/platforms/mac/canvas
