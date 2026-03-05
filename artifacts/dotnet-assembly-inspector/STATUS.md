# STATUS — dotnet-assembly-inspector

Last updated: 2026-03-04 23:07 KST

## Current Status
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
1. Extension method 전용 인덱스(대상 타입/선언 네임스페이스) 모델 추가.
2. 중첩 타입/제네릭 제약 조건/명시적 인터페이스 구현 시그니처 정확도 보강.
3. Extension method 전용 리포트 섹션(JSON/MD) 추가.
4. TFM 선택 동작 관련 단위 테스트 추가.

## Blockers
- 치명적 blocker 없음.
- 참고: 프로젝트 타깃이 `net8.0`이라 실행 시 호스트에 .NET 8 런타임이 없으면 `DOTNET_ROLL_FORWARD=Major`가 필요함(현재는 .NET 10만 설치).

## Update Rules
- Update this file whenever a phase starts/completes.
- Keep entries short and timestamped.
- Reflect major decisions in `WORKPLAN.md` Decision Log.
