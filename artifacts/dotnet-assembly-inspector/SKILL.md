# OpenClaw Skill: dotnet-assembly-inspector

## Purpose
OpenClaw wrapper skill for inspecting `.NET` assemblies/packages with `dotnet-assembly-inspector` and producing API index artifacts for coding agents.

## Entrypoint

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  <input-path(.dll|.nupkg|dir)> [output-dir] [--tfm <TFM>] [--all-tfms] [--compact-json|--compact] [--chunk <namespace|type>]
```

## Input Contract
- `input`: required path, one of:
  - single `.dll`
  - single `.nupkg`
  - directory
- `output_dir`: optional, default `output`
- `tfm`: optional, only meaningful for `.nupkg` processing
- `all_tfms`: optional boolean, overrides `tfm` selection and inspects all discovered TFMs
- `mode`: output shape selector
  - `default`: no extra mode flags
  - `compact`: `--compact-json` (alias: `--compact`)
  - `chunked`: `--chunk namespace` or `--chunk type` (can be combined with compact)

## Input Resolution Rules
- `.dll` input: one assembly is analyzed.
- `.nupkg` input: package is extracted and `lib/**/*.dll` is analyzed.
  - no `tfm`/`all_tfms`: first discovered TFM only
  - `--tfm <TFM>`: only matching TFM
  - `--all-tfms`: every discovered TFM
- directory input:
  - if top-level `*.nupkg` exists: process those packages
  - otherwise: process all `*.dll` recursively

## Output Contract
- Always emits per analyzed DLL:
  - `api-index.json`
  - `api-summary.md`
- Path patterns by input type:
  - `.dll`: `<output>/api-index.json`, `<output>/api-summary.md`
  - `.nupkg`: `<output>/<tfm>/<assembly>/api-index.json`, `<output>/<tfm>/<assembly>/api-summary.md`
  - directory (`*.dll` batch): `<output>/<assembly>/api-index.json`, `<output>/<assembly>/api-summary.md`
  - directory (`*.nupkg` batch): `<output>/<package>/<tfm>/<assembly>/api-index.json`, `<output>/<package>/<tfm>/<assembly>/api-summary.md`
- `mode=compact`:
  - `api-index.json` schema becomes compact format (`compact-v1`)
- `mode=chunked`:
  - base files above are still emitted
  - additional chunk files:
    - namespace chunking: `<base>/chunks/namespaces/<NNNN>-<sanitized-namespace>.json`
    - type chunking: `<base>/chunks/types/<NNNN>-<sanitized-type>.json`

## Usage Examples

Single DLL, default mode:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/AssemblyInspector.Cli.dll output/sample-dll
```

Single nupkg, compact mode, selected TFM:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui/Tizen.UI.1.0.0-rc.4.nupkg output/tizen-ui-compact \
  --tfm net8.0-tizen10.0 --compact-json
```

Directory nupkg batch, all TFMs, type-chunked:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet run \
  --project src/AssemblyInspector.Cli --no-build -c Release -- \
  input/nupkg/tizen-ui output/tizen-ui-chunked --all-tfms --chunk type
```

## Notes
- Host without .NET 8 runtime may require `DOTNET_ROLL_FORWARD=Major` on execution.
- If `--chunk` is omitted, no `chunks/` folder is produced.
