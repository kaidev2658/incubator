# Local usage for Claude / Cursor / Cline (no MCP)

## Common execution rule

When assembly inspection is needed, execute:

```bash
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <input_path> [output_dir] [flags]
```

Typical flags:
- `--tfm <TFM>`
- `--all-tfms`
- `--compact`
- `--chunk namespace|type`

## Agent instruction snippet

Use this in project rules/instructions:

```text
For .NET assembly/package inspection, run:
skills/dotnet-assembly-inspector-local/scripts/inspect.sh <INPUT> output/local-agent --compact
Then analyze generated api-index.json and api-summary.md files to summarize public API, target TFMs, and compatibility risks.
```

## Notes

- Run from artifact home: `artifacts/dotnet-assembly-inspector`.
- Script sets:
  - `PATH=/usr/local/share/dotnet:$PATH`
  - `DOTNET_ROLL_FORWARD=Major`
- Ensure project is built once (`dotnet build -c Release`) before `--no-build` runs.
