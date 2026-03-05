# dotnet-assembly-inspector

NuGet/DLL assembly API inspection workspace using **Mono.Cecil**.

## Purpose
Analyze .NET assemblies without runtime loading and produce API inventory outputs for development/AI-assisted coding.

## Structure
- `input/` — target DLLs / nupkg samples
- `output/` — generated analysis artifacts
- `scripts/` — extraction and helper scripts
- `notes/` — findings and ad-hoc notes
- `WORKPLAN.md` — project plan and phase checklist
- `STATUS.md` — current progress and next actions

## CLI Usage

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  <input-path(.dll|.nupkg|dir)> [output-dir] [--tfm <TFM>] [--all-tfms] [--compact-json|--compact] [--chunk <namespace|type>]
```

### Input modes
- `.dll`: analyze a single assembly
- `.nupkg`: extract package and analyze `lib/**/*.dll`
- `directory`: batch mode
  - if directory contains `*.nupkg` (top-level), those are processed
  - otherwise all `*.dll` under directory are processed

### TFM options (for `.nupkg` processing)
- `--tfm <TFM>`: analyze only the selected TFM (e.g. `net8.0-tizen10.0`)
- `--all-tfms`: analyze all discovered TFMs
- default (no option): first discovered TFM only

### JSON output mode
- default: existing verbose `api-index.json` schema (backward compatible)
- `--compact-json` (alias: `--compact`): writes compact `api-index.json` (`compact-v1`) with shortened keys for lower token usage in AI workflows
- `--chunk <namespace|type>`: writes additional chunk JSON files under `chunks/` while always keeping base `api-index.json`

### Output file patterns
- single `.dll` input:
  - `<output>/api-index.json`
  - `<output>/api-summary.md`
- directory input (`*.dll` batch):
  - `<output>/<assembly>/api-index.json`
  - `<output>/<assembly>/api-summary.md`
- `.nupkg` input:
  - `<output>/<tfm>/<assembly>/api-index.json`
  - `<output>/<tfm>/<assembly>/api-summary.md`
- chunk output (when `--chunk` is used):
  - namespace mode: `<base>/chunks/namespaces/<NNNN>-<sanitized-namespace>.json`
  - type mode: `<base>/chunks/types/<NNNN>-<sanitized-type>.json`

### Coding-agent prompt templates
- See [`notes/coding-agent-prompt-templates.md`](notes/coding-agent-prompt-templates.md) for practical prompts:
  - API 탐색/질의
  - 확장 메서드 찾기
  - 타입/네임스페이스 영향도 분석

## MCP Tooling (Phase 5-1, 5-2)

`inspect_assembly` and `inspect_nuget_package` are available as MCP-facing tool entries that reuse the existing Mono.Cecil analyzer pipeline.

### Command entry

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  --mcp-tool inspect_assembly \
  --request <request.json> \
  [--response <response.json>]
```

- If `--response` is omitted, JSON response is printed to stdout.
- Existing CLI mode is unchanged and still starts with `<input-path(.dll|.nupkg|dir)>`.
- Supported MCP tool names: `inspect_assembly`, `inspect_nuget_package`

### Request contract (`inspect_assembly`)

```json
{
  "assemblyPath": "input/AssemblyInspector.Cli.dll",
  "dependencySearchPaths": [
    "input/deps"
  ]
}
```

- `assemblyPath` (required): target `.dll` path.
- `dependencySearchPaths` (optional): additional resolver directories; non-existing paths are ignored.

### Response contract (`inspect_assembly`)

```json
{
  "apiIndex": {
    "assemblyName": "AssemblyInspector.Cli",
    "sourcePath": "/abs/path/AssemblyInspector.Cli.dll",
    "generatedAtUtc": "2026-03-05T08:00:00.0000000+00:00",
    "namespaces": [],
    "extensionMethods": []
  },
  "apiSummaryMarkdown": "# API Summary: AssemblyInspector.Cli\n..."
}
```

- `apiIndex`: same full schema as existing `api-index.json` default output.
- `apiSummaryMarkdown`: same content shape as `api-summary.md`.

### Request contract (`inspect_nuget_package`)

```json
{
  "nupkgPath": "input/sample.nupkg",
  "tfm": "net8.0",
  "allTfms": false
}
```

- `nupkgPath` (required): target `.nupkg` path.
- `tfm` (optional): inspect only this TFM.
- `allTfms` (optional): inspect all discovered TFMs. If `true`, `tfm` is ignored.
- default behavior (without `tfm` and `allTfms`): first discovered TFM only.

### Response contract (`inspect_nuget_package`)

```json
{
  "nupkgPath": "/abs/path/sample.nupkg",
  "assemblies": [
    {
      "tfm": "net8.0",
      "assemblyName": "Sample",
      "apiIndex": {
        "assemblyName": "Sample",
        "sourcePath": "/tmp/.../Sample.dll",
        "generatedAtUtc": "2026-03-05T08:30:00.0000000+00:00",
        "namespaces": [],
        "extensionMethods": []
      },
      "apiSummaryMarkdown": "# API Summary: Sample\n..."
    }
  ]
}
```

- `assemblies`: per-analyzed DLL result grouped by selected TFM set.
- `apiIndex`/`apiSummaryMarkdown`: same content shapes as existing CLI outputs.

## Examples

Analyze one DLL:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/AssemblyInspector.Cli.dll output/sample-dll
```

Analyze one nupkg, specific TFM:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui/Tizen.UI.1.0.0-rc.4.nupkg output/tizen-ui-single --tfm net8.0-tizen10.0
```

Analyze nupkg directory, all TFMs:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui output/tizen-ui-all --all-tfms
```

## Outputs
For each analyzed DLL:
- `api-index.json` — structured API metadata
- `api-summary.md` — readable summary
