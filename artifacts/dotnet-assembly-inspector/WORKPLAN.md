# WORKPLAN — dotnet-assembly-inspector

Last updated: 2026-03-05 19:00 KST
Owner: Coordinator

## Goal
NuGet package/DLL assembly metadata inspection tool based on **Mono.Cecil** (no runtime loading), producing machine-readable and human-readable API indexes.

## Scope (MVP)
- Input: dll folder (later .nupkg support)
- Output:
  - `api-index.json` (full structure)
  - `api-summary.md` (readable summary)
- Extract:
  - namespaces/types
  - constructors/properties/methods/events signatures
  - inheritance/interface hierarchy
  - extension methods + declaring namespace

## Phase Plan

### Phase 1 — CLI MVP (in progress)
- [x] Create .NET solution scaffold (`src`, `tests`)
- [x] Add Mono.Cecil + resolver pipeline
- [x] Build metadata domain model (`ApiIndex`)
- [x] Implement signature formatter (C#-friendly)
- [x] Export JSON/Markdown
- [x] Add sample input/output + smoke test

### Phase 2 — NuGet package ingestion
- [x] Read `.nupkg` directly
- [x] TFM selection (`--tfm`, `--all-tfms`)
- [x] Dependency path handling

### Phase 3 — AI-friendly output optimization
- [x] Compact index format
- [x] Namespace/type chunking outputs
- [x] Prompt templates for coding agents

### Phase 4 — OpenClaw Skill
- [x] Skill wrapper + `SKILL.md` (Phase 4-1 skeleton)
- [x] Input/Output contract (`dll` / `.nupkg` / `dir`, `default` / `compact` / `chunked`)
- [x] Example workflows (CLI invocation samples)

### Phase 5 — MCP tooling
- [x] `inspect_assembly`
- [x] `inspect_nuget_package`
- [x] `find_extension_methods`

## Risks / Notes
- Obfuscated assemblies reduce semantic readability.
- Missing dependencies may reduce type resolution quality.
- Multi-target packages may expose different signatures per TFM.

## Decision Log
- 2026-03-04: Project directory standardized to `artifacts/dotnet-assembly-inspector`.
- 2026-03-04: Analyzer backend selected: **Mono.Cecil** (not System.Reflection).
- 2026-03-04: Phase 1 kickoff scaffolded with manual `.sln`/`.csproj` creation because `dotnet` CLI was not on PATH in initial agent run.
- 2026-03-04: Build/test execution standardized with explicit path `/usr/local/share/dotnet/dotnet` and `DOTNET_ROLL_FORWARD=Major` in this host.
- 2026-03-04: CLI input modes extended to support `.dll`, `.nupkg`, and directory batch input.
- 2026-03-04: Added TFM controls for nupkg processing (`--tfm <value>`, `--all-tfms`; default: first discovered TFM).
- 2026-03-05 11:54 KST: Phase 4-1 started with OpenClaw skill wrapper draft (`SKILL.md`) including executable entrypoint, I/O contract, and output file layout patterns.
- 2026-03-05 16:50 KST: Phase 5-1 완료. MCP-facing tool entry(`--mcp-tool inspect_assembly`)를 추가하고 request/response contract를 README에 문서화. 기존 CLI 인자 경로는 그대로 유지.
- 2026-03-05 17:40 KST: Phase 5-2 완료. `NugetPackageInspector`를 도입해 기존 `.nupkg` 처리 로직을 공용화하고 MCP `inspect_nuget_package`가 같은 파이프라인을 재사용하도록 정리.
- 2026-03-05 19:00 KST: Phase 5-3 완료. `find_extension_methods` MCP 도구를 추가하고 기존 `ApiIndex.ExtensionMethods`를 재사용해 target type/namespace/method contains 필터 검색을 지원.
