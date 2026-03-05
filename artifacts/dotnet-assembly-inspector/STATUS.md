# STATUS — dotnet-assembly-inspector

Last updated: 2026-03-05 19:00 KST

## Current Status
- 2026-03-05 19:00 KST: Phase 5-3 `find_extension_methods` MCP tooling 구현 완료.
  - MCP 엔트리 추가: `--mcp-tool find_extension_methods --request <request.json> [--response <response.json>]`
  - 요청/응답 계약 추가: `FindExtensionMethodsRequest`, `FindExtensionMethodsResponse`
  - 기존 extension index 경로 재사용: `IAssemblyInspector` 결과(`ApiIndex.ExtensionMethods`)를 필터링해 응답 생성
  - 필터 지원:
    - `TargetTypeContains` (대상 타입명 contains)
    - `DeclaringNamespaceContains` (선언 네임스페이스 contains, optional)
    - `MethodNameContains` (메서드명 contains, optional)
  - 테스트 추가: `FindExtensionMethodsToolTests` (happy path + 복합 필터 동작)
- 2026-03-05 17:40 KST: Phase 5-2 `inspect_nuget_package` MCP tooling 구현 완료.
  - MCP 엔트리 추가: `--mcp-tool inspect_nuget_package --request <request.json> [--response <response.json>]`
  - 요청/응답 계약 추가: `InspectNugetPackageRequest`, `InspectNugetPackageResponse`
  - nupkg 파이프라인 공용화: `NugetPackageInspector`를 도입해 기존 CLI nupkg 처리 로직과 MCP 도구가 동일 경로 재사용
  - 문서화: README MCP 섹션에 `inspect_nuget_package` contract/동작(default/`tfm`/`allTfms`) 추가
  - 테스트 추가: `InspectNugetPackageToolTests.ExecuteAsync_WithSingleTfmPackage_ReturnsAssemblyResults`
- 2026-03-05 16:50 KST: Phase 5-1 `inspect_assembly` MCP tooling 구현 완료.
  - MCP 엔트리 추가: `--mcp-tool inspect_assembly --request <request.json> [--response <response.json>]`
  - 구현 파일: `src/AssemblyInspector.Cli/Mcp/InspectAssemblyRequest.cs`, `InspectAssemblyResponse.cs`, `InspectAssemblyTool.cs`
  - 기존 analyzer 파이프라인 재사용: `CecilAssemblyInspector` + `MarkdownReportWriter` 기반 응답 생성
  - 문서화: README에 request/response contract 및 사용 예시 추가
  - 테스트 추가: `InspectAssemblyToolTests.ExecuteAsync_WithValidAssembly_ReturnsApiIndexAndMarkdown` (happy path)
- 2026-03-05 16:45 KST: Phase 4-1 후속 조치 완료 (SKILL 템플릿 정렬 + compact/chunk 회귀 테스트 보강).
  - `SKILL.md` 문구/호출 예시를 OpenClaw 사용 템플릿 형태(Invocation Template + Parameter Mapping)로 정리
  - 테스트 추가: `RunAsync_WithNamespaceChunkingAndCompactJson_WritesCompactBaseAndChunkPayloads`
  - 테스트 추가: `RunAsync_WithTypeChunkingWithoutCompact_KeepsLegacyBaseAndChunkPayloads`
  - 검증 실행: `dotnet build`, `dotnet test` 모두 성공 (Passed 18, Failed 0)
- 2026-03-05 11:54 KST: Phase 4-1 OpenClaw Skill wrapper 착수/초안 작성 진행.
  - 문서 추가: `SKILL.md` (OpenClaw skill entry)
  - 포함 항목: 목적, 엔트리포인트, 입력 계약(dll/nupkg/dir), 출력 계약(default/compact/chunked), 예시 워크플로
  - CLI 정합성 반영: `--tfm`, `--all-tfms`, `--compact-json|--compact`, `--chunk <namespace|type>`
  - 출력 경로 패턴 정합성 반영:
    - `.dll`: `<output>/api-index.json`, `api-summary.md`
    - `.nupkg`: `<output>/<tfm>/<assembly>/...`
    - `dir(nupkg batch)`: `<output>/<package>/<tfm>/<assembly>/...`
    - `dir(dll batch)`: `<output>/<assembly>/...`
- 2026-03-05 11:56 KST: Phase 3-3 coding-agent 프롬프트 템플릿 문서화 완료.
  - 문서 추가: `notes/coding-agent-prompt-templates.md`
  - 포함 항목: API 탐색/질의, 확장 메서드 탐색, 타입/네임스페이스 영향도 분석 템플릿
  - 출력 모드/파일 패턴 명시: default(full), compact(`compact-v1`), chunked(`chunks/namespaces|types`)
  - README 사용법 섹션 업데이트: `--chunk` 옵션, 파일 패턴, 템플릿 문서 링크 반영
- 2026-03-05 11:54 KST: Phase 3-2 namespace/type chunking 출력 구현 완료.
  - CLI 옵션 추가: `--chunk <namespace|type>` (기본값 `none`, 기존 출력 완전 호환)
  - chunk 출력 경로 규칙:
    - namespace: `chunks/namespaces/<NNNN>-<sanitized-namespace>.json`
    - type: `chunks/types/<NNNN>-<sanitized-type>.json`
  - 구현 동작: 기존 `api-index.json`, `api-summary.md`는 항상 생성되고, chunk 옵션 지정 시 추가 JSON 분할 출력 생성
  - 테스트 보강: chunk 파일 생성/명명 규칙 검증 2건 추가
- 2026-03-05 11:11 KST: Phase 3-1 compact index format 최적화 구현 완료.
  - CLI 옵션 추가: `--compact-json` (alias `--compact`), 기본 출력은 기존 JSON 스키마 유지
  - `JsonReportWriter` compact 모드(`compact-v1`) 추가: 축약 키(`f/a/s/g/n/x`) 및 namespace/type/member/extension 구조 경량화
  - 테스트 보강: compact 출력 shape 검증, 기본 모드 호환성 검증, 앱 경로(CompactJson 옵션) 통합 검증
  - 문서 갱신: README 사용법에 compact JSON 옵션 추가
  - 검증 실행: `dotnet build`, `dotnet test` 모두 성공 (Passed 14, Failed 0)
- 2026-03-05 11:03 KST: Phase 2 잔여 항목(의존성 경로 처리) 구현 완료.
  - `IAssemblyInspector`에 선택적 dependency search path 전달 지원 추가, `InspectorApp`에서 DLL/nupkg 시나리오별 주변 경로(`lib/<tfm>`, `ref/<tfm>`, `runtimes/*/lib/<tfm>`, 인접 디렉터리) 수집/전달
  - 테스트 추가: dependency path 전달 동작 검증 2건
  - 검증 실행: `dotnet build`, `dotnet test` 모두 성공 (Passed 11, Failed 0)
- 2026-03-05 10:58 KST: 시그니처 정확도 보강(중첩 타입/제네릭 제약/명시적 인터페이스 구현) 완료.
  - `SignatureFormatter` 개선: nested type, generic constraints, explicit interface implementation 서명 표현 정확도 향상
  - 테스트 픽스처/케이스 추가 후 검증: `dotnet test` → Passed 9, Failed 0
- 2026-03-05 10:26 KST: `--tfm`/`--all-tfms` 선택 동작 테스트 보강 완료.
  - 테스트 추가: 요청 TFM만 출력(`RunAsync_WithRequestedTfm_WritesOnlyThatTfmLayout`), 미존재 TFM 요청 시 출력 없음(`RunAsync_WithUnknownRequestedTfm_WritesNothing`), 전체 TFM 출력(`RunAsync_WithAllTfms_WritesPerTfmLayout`) 검증
  - 출력 구조 검증: `<out>/<tfm>/<assembly>/api-index.json` 및 `api-summary.md`
  - 검증 실행: `DOTNET_ROLL_FORWARD=Major dotnet test` → Passed 6, Failed 0
- 2026-03-05 10:24 KST: Extension method 인덱스/리포트 섹션 구현 완료.
  - 모델 추가: `ExtensionMethodIndex`, `ApiIndex.ExtensionMethods`
  - JSON: 기존 스키마 유지 + `ExtensionMethods` 필드 확장
  - Markdown: `## Extension Methods` 섹션 추가 (대상 타입별 그룹)
  - 테스트 보강: extension method 인덱싱 및 JSON/Markdown 리포트 출력 검증
- 2026-03-04 19:39 KST: Phase 1 kickoff implemented.
- 2026-03-04 20:08 KST: `/usr/local/share/dotnet/dotnet` 기준 restore/build 성공 (오류/경고 0).
- 2026-03-04 20:08 KST: 샘플 실행 성공 (`DOTNET_ROLL_FORWARD=Major` 사용) 및 결과 파일 생성 확인.
- 2026-03-04 20:11 KST: 테스트 프로젝트 구성 및 스모크 테스트 1건 통과 (`Passed 1, Failed 0`).
- 2026-03-04 20:16 KST: NuGet `Tags:"Tizen.UI"` 대상 11개 패키지 수집/적재 완료.
- 2026-03-04 21:09 KST: **nupkg 직접입력 모드 구현 완료**.
  - 입력 지원: `.dll` / `.nupkg` / 디렉토리(batch)
  - 디렉토리 입력 시: `*.nupkg` 우선 일괄 처리, 없으면 `*.dll` 일괄 처리
  - `.nupkg` 처리 시: 임시해제 후 `lib/**.dll` 자동 탐색 및 TFM 하위 경로로 출력
- 2026-03-04 21:10 KST: end-to-end 재검증 완료.
  - 빌드: 성공 (경고 0, 오류 0)
  - 테스트: 성공 (1/1)
  - 실실행: `input/nupkg/tizen-ui`(11개) → `output/tizen-ui-direct/` 결과 생성 완료
- 2026-03-04 23:05 KST: TFM 옵션 구현/검증 완료.
  - CLI 옵션 추가: `--tfm <value>`, `--all-tfms`
  - 구현 파일: `src/AssemblyInspector.Cli/Program.cs`, `src/AssemblyInspector.Cli/App/InspectorApp.cs`, `src/AssemblyInspector.Cli/App/InspectorOptions.cs`
  - 검증 실행:
    - `... run ... input/nupkg/tizen-ui output/tizen-ui-tfm-net8 --tfm net8.0-tizen10.0`
    - `... run ... input/nupkg/tizen-ui output/tizen-ui-tfm-all --all-tfms`
  - 빌드/테스트: 성공 (경고 0, 오류 0 / 테스트 1 통과)
- 2026-03-04 23:07 KST: README 사용법 보강 완료.
  - 입력 모드(.dll/.nupkg/dir), TFM 옵션 동작, 실행 예시 추가

## Next Immediate Actions
1. MCP 응답 최소화 옵션(예: compact/chunk 대응) 확장 여부 결정.
2. MCP tool 에러 응답 표준 스키마(입력 검증/파일 미존재) 도입 여부 검토.
3. `find_extension_methods` README contract/샘플 보강 여부 검토.

## Blockers
- 치명적 blocker 없음.
- 참고: 프로젝트 타깃이 `net8.0`이라 실행 시 호스트에 .NET 8 런타임이 없으면 `DOTNET_ROLL_FORWARD=Major`가 필요함(현재는 .NET 10만 설치).

## Update Rules
- Update this file whenever a phase starts/completes.
- Keep entries short and timestamped.
- Reflect major decisions in `WORKPLAN.md` Decision Log.
