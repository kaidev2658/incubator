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
  <input-path(.dll|.nupkg|dir)> [output-dir] [--tfm <TFM>] [--all-tfms] [--compact-json|--compact]
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
