# A2UI 호환 Tizen Renderer 구현 실행계획 (GenUI 패턴 이식)

작성일: 2026-02-27

## 1) 목표/범위
**목표**
- GenUI의 구조 패턴(파서/컨트롤러/상태스토어)을 Tizen에 이식
- A2UI v0.9~v0.10 메시지를 안정적으로 렌더링
- 스트리밍 파싱, surface lifecycle, data model 반영, function call까지 포함

**범위**
- A2UI 메시지 처리 파이프라인
- 렌더러 브리지(UI 프레임워크 연결)
- 상태관리/관측성/에러처리/테스트

## 2) 권장 모듈 구조
```text
tizen-a2ui-renderer/
├─ src/
│  ├─ transport/
│  │  ├─ a2ui_parser.cs
│  │  └─ transport_adapter.cs
│  ├─ controller/
│  │  ├─ surface_controller.cs
│  │  └─ surface_registry.cs
│  ├─ model/
│  │  ├─ a2ui_messages.cs
│  │  ├─ data_model.cs
│  │  └─ catalog.cs
│  ├─ renderer/
│  │  ├─ renderer_bridge.cs
│  │  ├─ components/
│  │  └─ theme.cs
│  ├─ utils/
│  │  ├─ logger.cs
│  │  └─ errors.cs
│  └─ Program.cs
├─ tests/
│  ├─ unit/
│  ├─ integration/
│  └─ e2e/
└─ docs/
```

## 3) 핵심 컴포넌트 설계
| 컴포넌트 | 역할 | GenUI 참조 패턴 |
|---|---|---|
| Parser | 스트리밍 JSON/블록 파싱, 메시지 이벤트화 | A2uiParserTransformer |
| TransportAdapter | chunk 입력 -> parser -> event stream | A2uiTransportAdapter |
| SurfaceController | surface lifecycle + data model 반영 | SurfaceController |
| RendererBridge | A2UI 컴포넌트 -> Tizen UI 매핑 | 위젯 매핑 레이어 |
| StateStore | surface별 스냅샷/patch 관리 | DataModelStore |

핵심 포인트:
- surface 생성 전 update는 pending buffer 큐잉
- timeout 기반 pending 정리
- 상태 반영은 patch 중심으로 최소화

## 4) 버전 호환 전략(v0.9~v0.10)
- 입력 메시지 버전 감지 후 VersionRouter 분기
- 내부는 NormalForm으로 통일해 컨트롤러 단일 처리

주의:
- v0.10의 function-call 계열 메시지 추가 처리 필수
- dataModel 삭제 semantics 버전 차이 분리

## 5) callFunction / functionResponse 설계
처리 흐름:
1. `callFunction` 수신
2. `functionCallId` 등록
3. callableFrom 정책 검사
4. 함수 실행(타임아웃 포함)
5. `functionResponse` 반환
6. 실패 시 구조화 error 전송

체크리스트:
- [ ] functionCallId 매칭
- [ ] async timeout/retry
- [ ] 실행 실패 에러코드 표준화

## 6) 오류/관측성 설계
에러코드 예시:
- E_PARSE_LINE
- E_UNSUPPORTED_VERSION
- E_SURFACE_NOT_FOUND
- E_FUNCTION_CALL_FAILED
- E_RENDER_FAILED

로그 필드 권장:
| 필드 | 설명 |
|---|---|
| ts | timestamp |
| surfaceId | surface 식별자 |
| messageType | 메시지 타입 |
| functionCallId | 함수 호출 식별 |
| errorCode | 에러 코드 |
| payloadHash | 추적용 해시 |

## 7) 테스트 전략
- Unit: parser, validation, patch apply
- Integration: lifecycle + function call 왕복
- E2E: 실제 Tizen 디바이스 24h soak

## 8) 6주 개발 백로그
| 주차 | 작업 |
|---|---|
| 1주차 | Parser/TransportAdapter 기본 구현 |
| 2주차 | SurfaceController/StateStore 구현 |
| 3주차 | RendererBridge + 기본 컴포넌트 매핑 |
| 4주차 | v0.9/v0.10 호환 + function call 처리 |
| 5주차 | 관측성/에러체계/로그 정리 |
| 6주차 | 통합+E2E 안정화 및 문서화 |

## 9) MVP 완료 기준 (DoD)
- [ ] v0.9/v0.10 메시지 정상 처리
- [ ] surface lifecycle 정상
- [ ] data model patch 반영
- [ ] callFunction/functionResponse 지원
- [ ] 기본 컴포넌트 렌더링
- [ ] 표준 로그/에러코드 적용
- [ ] E2E 통과

## 참고 링크
- GenUI README: https://github.com/flutter/genui/blob/main/README.md
- Parser transformer:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_parser_transformer.dart
- Transport adapter:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/transport/a2ui_transport_adapter.dart
- Surface controller:
  https://github.com/flutter/genui/blob/main/packages/genui/lib/src/engine/surface_controller.dart
- A2UI repo: https://github.com/google/A2UI
