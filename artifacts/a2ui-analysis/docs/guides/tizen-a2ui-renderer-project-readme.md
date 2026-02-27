# Tizen A2UI Renderer (C#/.NET) Skeleton

초기 스캐폴딩 프로젝트입니다.

## Build
```bash
dotnet build projects/tizen-a2ui-renderer/TizenA2uiRenderer.csproj
```

## Run
```bash
dotnet run --project projects/tizen-a2ui-renderer/TizenA2uiRenderer.csproj
```

## Test
```bash
dotnet test projects/tizen-a2ui-renderer/tests/TizenA2uiRenderer.Tests/TizenA2uiRenderer.Tests.csproj
```

## Next Steps
- Parser: JSONL/Markdown/balanced JSON 파싱 완성
- Normalizer: v0.9/v0.10 메시지 통합
- SurfaceController: pending queue, TTL, validation 강화
- RendererBridge: 실제 Tizen UI 매핑 구현
- Observability: structured logging/metrics 추가
