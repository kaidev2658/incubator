# Local Agent Setup & Usage Guide (No MCP)

이 문서는 `dotnet-assembly-inspector`를 **MCP 없이 로컬 스크립트 기반**으로,
각 도구(Claude, Cursor, Cline, Antigravity)에서 **설정하고 실제로 사용하는 방법**을 설명합니다.

---

## 0) 공통 준비

### 작업 위치

```bash
cd /Users/clawdev/workspace/github/incubator/artifacts/dotnet-assembly-inspector
```

### 실행 명령(공통)

macOS/Linux:
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> [output_dir] [flags]
```

Windows PowerShell:
```powershell
powershell -ExecutionPolicy Bypass -File .\skills\dotnet-assembly-inspector-local\scripts\inspect.ps1 <INPUT_PATH> [output_dir] [flags]
```

대표 플래그:
- `--tfm <TFM>`
- `--all-tfms`
- `--compact`
- `--chunk namespace|type`

결과 파일(어셈블리별):
- `api-index.json`
- `api-summary.md`

---

## 1) Claude

## 1-1. 설정 위치
- 프로젝트 지시문(Project instructions) 또는 시스템 규칙 영역

## 1-2. 복붙 규칙
```text
For .NET assembly/package inspection in this repository, run:
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> output/local-agent --compact
Then read api-index.json and api-summary.md and summarize:
(1) scope, (2) TFM coverage, (3) public API highlights, (4) compatibility/dependency risks.
Do not use MCP for this workflow.
```

## 1-3. 사용 예시
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/nupkg/tizen-ui output/local-agent --all-tfms --compact
```

---

## 2) Cursor

## 2-1. 설정 위치
- Cursor Rules (프로젝트 규칙)
- 선택: Custom command

## 2-2. 복붙 규칙 (Rules)
```text
When user asks to inspect .NET DLL/NuPkg APIs:
1) Run: skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> output/local-agent --compact
2) Read generated api-index.json and api-summary.md
3) Return concise findings: scope, TFM coverage, API surface, compatibility/dependency risks
4) Do not use MCP
```

## 2-3. 선택: Custom command
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh "$FILE" output/local-agent --compact
```

## 2-4. 사용 예시
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/extracted/tizen-ui output/local-agent --compact
```

---

## 3) Cline

## 3-1. 설정 위치
- Cline instruction / project instruction

## 3-2. 복붙 규칙
```text
Use local shell workflow for .NET assembly inspection.
Run:
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> output/local-agent --compact
Then parse api-index.json/api-summary.md and report:
- scope
- target TFMs
- API highlights
- compatibility/dependency risks
Do not use MCP in this repository.
```

## 3-3. 사용 예시
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/nupkg/tizen-ui/Tizen.UI.1.0.0-rc.4.nupkg output/local-agent --tfm net8.0-tizen10.0 --compact
```

---

## 4) Antigravity

## 4-1. 설정 위치
- Antigravity 정책/프롬프트 규칙 영역

## 4-2. 복붙 규칙 (권장: 단계형)
```text
Step 1) Run local inspection:
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> output/local-agent --compact

Step 2) Optional flags:
--tfm <TFM> | --all-tfms | --chunk namespace|type

Step 3) Parse outputs:
- api-index.json
- api-summary.md

Step 4) Report:
1. scope
2. target frameworks
3. public API highlights
4. compatibility/dependency risks

Rules:
- no MCP
- run inside artifacts/dotnet-assembly-inspector
- retry once with DOTNET_ROLL_FORWARD=Major on runtime mismatch
```

## 4-3. 사용 예시
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/nupkg/tizen-ui output/local-agent --all-tfms --compact --chunk type
```

---

## 5) 검증 체크리스트 (공통)

한 번 실행 후 아래만 확인하면 설정이 정상입니다.

- `output/local-agent/...` 아래에 `api-index.json` 생성됨
- 같은 경로에 `api-summary.md` 생성됨
- 에이전트 응답에 아래 4개가 포함됨:
  - scope
  - TFM coverage
  - public API highlights
  - compatibility/dependency risks

---

## 6) Windows 빠른 체크

- PowerShell에서 스크립트 실행:

```powershell
powershell -ExecutionPolicy Bypass -File .\skills\dotnet-assembly-inspector-local\scripts\inspect.ps1 input\nupkg\tizen-ui output\local-agent --all-tfms --compact
```

- dotnet 경로 확인:

```powershell
dotnet --info
```

(스크립트가 `C:\Program Files\dotnet`를 PATH에 자동 추가 시도)

---

## 7) 트러블슈팅

### dotnet 명령을 못 찾는 경우
스크립트가 `/usr/local/share/dotnet`를 PATH에 추가하도록 되어 있습니다.
그래도 실패하면 수동 확인:

```bash
export PATH="/usr/local/share/dotnet:$PATH"
dotnet --info
```

### runtime mismatch
재시도:

```bash
DOTNET_ROLL_FORWARD=Major skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT_PATH> output/local-agent --compact
```

### --no-build 관련 실패
릴리스 빌드 1회 수행 후 다시 실행:

```bash
dotnet build -c Release
```
