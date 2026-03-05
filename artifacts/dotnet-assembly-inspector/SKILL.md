# OpenClaw Skill: dotnet-assembly-inspector

## Use This Skill When
- You need machine-readable API inventory from a `.dll`, `.nupkg`, or package directory.
- You want coding-agent friendly output in `default`, `compact`, or `chunked` mode.
- You need deterministic output layout for prompt templates and downstream automation.

## Invocation Template

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  {{input_path}} {{output_dir=output}} \
  [--tfm {{tfm}} | --all-tfms] \
  [--compact-json|--compact] \
  [--chunk namespace|type]
```

## Parameter Mapping (OpenClaw Wrapper)
- `input_path` (required): one of `.dll`, `.nupkg`, directory
- `output_dir` (optional): output root directory (default: `output`)
- `tfm` (optional): target TFM for `.nupkg` inspection
- `all_tfms` (optional): inspect all discovered TFMs (overrides `tfm`)
- `compact` (optional): enable compact schema (`--compact-json` or `--compact`)
- `chunk` (optional): `namespace` or `type`

## Resolution Rules
- `.dll` input: inspect one assembly.
- `.nupkg` input: extract and inspect `lib/**/*.dll`.
- directory input:
  - top-level `*.nupkg` exists -> process nupkg batch
  - otherwise -> process recursive `*.dll` batch
- TFM selection for `.nupkg`:
  - no `--tfm`/`--all-tfms` -> first discovered TFM
  - `--tfm <TFM>` -> requested TFM only
  - `--all-tfms` -> all discovered TFMs

## Output Contract
- Always emits for each analyzed DLL:
  - `api-index.json`
  - `api-summary.md`
- Path layout:
  - `.dll`: `<output>/api-index.json`, `<output>/api-summary.md`
  - `.nupkg`: `<output>/<tfm>/<assembly>/api-index.json`, `<output>/<tfm>/<assembly>/api-summary.md`
  - directory (`*.dll`): `<output>/<assembly>/api-index.json`, `<output>/<assembly>/api-summary.md`
  - directory (`*.nupkg`): `<output>/<package>/<tfm>/<assembly>/api-index.json`, `<output>/<package>/<tfm>/<assembly>/api-summary.md`
- Mode behavior:
  - `default`: legacy full schema (backward compatible)
  - `compact`: compact schema (`compact-v1`) in base/chunk JSON
  - `chunked`: additional files under `chunks/` while base outputs remain
    - namespace: `<base>/chunks/namespaces/<NNNN>-<sanitized-namespace>.json`
    - type: `<base>/chunks/types/<NNNN>-<sanitized-type>.json`

## Invocation Examples

Default (single DLL):
```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/AssemblyInspector.Cli.dll output/sample-dll
```

Compact + selected TFM (single nupkg):
```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui/Tizen.UI.1.0.0-rc.4.nupkg output/tizen-ui-compact \
  --tfm net8.0-tizen10.0 --compact
```

Compact + chunked + all TFMs (nupkg directory):
```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui output/tizen-ui-chunked \
  --all-tfms --compact --chunk type
```

## Notes
- If host only has newer runtime (for example .NET 10), keep `DOTNET_ROLL_FORWARD=Major`.
- Without `--chunk`, no `chunks/` directory is produced.
