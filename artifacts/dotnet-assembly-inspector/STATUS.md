# STATUS — dotnet-assembly-inspector

Last updated: 2026-03-05 11:03 KST

## Current Status
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
1. Phase 3-1: Compact index format 설계/적용 (AI 소비 토큰 절감용).
2. Phase 3-2: Namespace/type chunking 출력 추가 (`--chunk` 계열 옵션 검토 포함).
3. Phase 3-3: 코딩 에이전트용 프롬프트 템플릿 초안 작성 및 README/문서 반영.
4. 회귀 검증: 기존 JSON/MD 출력 호환성 테스트 보강.

## Blockers
- 치명적 blocker 없음.
- 참고: 프로젝트 타깃이 `net8.0`이라 실행 시 호스트에 .NET 8 런타임이 없으면 `DOTNET_ROLL_FORWARD=Major`가 필요함(현재는 .NET 10만 설치).

## Update Rules
- Update this file whenever a phase starts/completes.
- Keep entries short and timestamped.
- Reflect major decisions in `WORKPLAN.md` Decision Log.
