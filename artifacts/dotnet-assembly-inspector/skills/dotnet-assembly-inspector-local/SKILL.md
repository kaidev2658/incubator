---
name: dotnet-assembly-inspector-local
description: Run .NET assembly inspection locally (without MCP) from coding agents like Claude, Cursor, and Cline by executing the bundled script against .dll, .nupkg, or folder inputs and then summarizing generated JSON/Markdown outputs.
---

# dotnet-assembly-inspector-local

Use this skill to inspect assemblies directly via local shell execution.

This skill bundles a published `AssemblyInspector.Cli` payload under `bin/net8.0`, so users only need a compatible .NET runtime (no local source build required).

## Execute

From the artifact home (`dotnet-assembly-inspector`), run:

> Path rule: both scripts resolve `<input_path>` and `[output_dir]` relative to the directory where you executed the command (current working directory). Absolute paths are supported.

macOS/Linux:
```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <input_path> [output_dir] [extra flags]
```

Windows PowerShell:
```powershell
powershell -ExecutionPolicy Bypass -File .\skills\dotnet-assembly-inspector-local\scripts\inspect.ps1 <input_path> [output_dir] [extra flags]
```

Examples:

```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/nupkg/tizen-ui output/local-run --all-tfms --compact --chunk type
skills/dotnet-assembly-inspector-local/scripts/inspect.sh input/extracted/tizen-ui output/local-dll
```

## Expected output

Read these outputs per analyzed assembly:
- `api-index.json`
- `api-summary.md`

Then provide:
1. assembly/package scope
2. TFM coverage
3. key public API highlights
4. notable dependencies or compatibility risks

## Fallback

If the script fails due to SDK/runtime mismatch, keep `DOTNET_ROLL_FORWARD=Major` and retry once.

For details and copy-paste agent rule text, read `references/usage.md`.
