# A2UI 호환 Tizen Renderer — 모듈별 상세 설계서

> 목표: GenUI 구조/코드 패턴을 참고하여 Tizen 환경에서 A2UI(v0.9~v0.10)를 안정적으로 렌더링하는 런타임을 설계한다.

## 1) 모듈 개요 및 책임

### Parser (`/src/transport/a2ui_parser.cs`)
**책임**
- 스트리밍 입력에서 JSON 블록 추출
- JSONL, Markdown code block, balanced JSON 지원
- 파싱 실패 시 텍스트 fallback

**입출력**
- 입력: `string` chunk stream
- 출력: `GenerationEvent` (`text` | `message`)

```csharp
public abstract record GenerationEvent;
public sealed record TextEvent(string Text) : GenerationEvent;
public sealed record A2uiMessageEvent(A2uiMessage Message) : GenerationEvent;

public sealed class A2uiParser
{
    public A2uiParser(ParserOptions? options = null) { }
    public IReadOnlyList<GenerationEvent> AddChunk(string chunk) => Array.Empty<GenerationEvent>();
    public IReadOnlyList<GenerationEvent> Flush() => Array.Empty<GenerationEvent>();
}
```

**동시성/메모리/복구**
- 순서 보장 단일 큐
- 버퍼 상한(예: 1MB)
- 파싱 오류 시 텍스트 emit 후 계속 진행

---

### TransportAdapter (`/src/transport/transport_adapter.cs`)
**책임**
- Parser 래핑
- chunk/raw message 주입
- 이벤트 구독 관리

```csharp
public interface ITransportAdapter
{
    void AddChunk(string chunk);
    void AddMessage(A2uiMessage message);
    IDisposable OnMessage(Action<A2uiMessage> callback);
    IDisposable OnText(Action<string> callback);
}
```

**포인트**
- backpressure 큐 제한
- parser 에러 별도 `onError`

---

### SurfaceController (`/src/controller/surface_controller.cs`)
**책임**
- surface lifecycle 관리
- data model update 처리
- surface 생성 전 update 큐잉

```csharp
public sealed class SurfaceController
{
    public SurfaceController(ControllerOptions options) { }
    public void HandleMessage(A2uiMessage message) { }
    public IDisposable OnSurfaceUpdate(Action<SurfaceUpdate> callback) => Disposable.Empty;
    public IDisposable OnError(Action<A2uiError> callback) => Disposable.Empty;
}
```

**상태 전이**
- NONE → READY (`createSurface`)
- READY → UPDATED (`updateComponents`/`updateDataModel`)
- ANY → NONE (`deleteSurface`)

**복구 전략**
- validation 실패: error 이벤트
- pending timeout(기본 60s) 후 폐기

---

### RendererBridge (`/src/renderer/renderer_bridge.cs`)
**책임**
- SurfaceUpdate를 Tizen UI로 매핑
- diff 기반 부분 렌더
- render queue 스케줄링

```csharp
public interface IRendererBridge
{
    void Render(string surfaceId, SurfaceDefinition definition, DataModel dataModel);
    void Remove(string surfaceId);
}
```

**성능 포인트**
- 동일 surface 업데이트 coalesce
- layout recalculation 최소화

---

### StateStore (`/src/controller/state_store.cs`)
**책임**
- surface별 snapshot/patch 적용
- observer 기반 변경 통지

```csharp
public sealed class DataModel
{
    public object? Get(string path) => null;
    public void Set(string path, object? value) { }
    public void Delete(string path) { }
    public IDisposable OnChange(Action<string> callback) => Disposable.Empty;
}
```

**삭제 semantics**
- v0.9: undefined/omit 삭제
- v0.10: null 삭제 (Normalizer 선처리)

---

## 2) 디렉터리 구조 권장
```text
tizen-a2ui-renderer/
├─ src/
│  ├─ transport/
│  ├─ controller/
│  ├─ model/
│  ├─ renderer/
│  ├─ utils/
│  └─ Program.cs
├─ tests/
│  ├─ unit/
│  ├─ integration/
│  └─ e2e/
└─ docs/
```

## 3) 에러/관측성
**에러코드**
- E_PARSE_LINE
- E_UNSUPPORTED_VERSION
- E_SURFACE_NOT_FOUND
- E_FUNCTION_CALL_FAILED
- E_RENDER_FAILED

**로그 필드**
- ts, surfaceId, messageType, functionCallId, errorCode, payloadHash

## 4) 성능/메모리 정책
- parser buffer 상한
- pending queue 상한/TTL
- component/data model 캐시 상한
- surface 삭제 시 즉시 리소스 회수

## 5) 테스트 포인트
- Unit: parser/validator/patch
- Integration: lifecycle + function call
- E2E: 실제 Tizen 장치 24h soak
