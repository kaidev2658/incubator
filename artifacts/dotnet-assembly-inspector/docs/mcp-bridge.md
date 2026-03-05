# MCP stdio bridge (Claude Code / Cline)

This project includes a Node.js stdio MCP bridge at `mcp-bridge/server.js`.
The bridge proxies MCP `tools/call` to the existing CLI MCP entrypoint:

- `inspect_assembly`
- `inspect_nuget_package`
- `find_extension_methods`

No existing CLI behavior is changed.

## Runtime prerequisites

- Node.js 18+
- .NET SDK/runtime capable of running this project
- Build once before first MCP use:

```bash
DOTNET_ROLL_FORWARD=Major /usr/local/share/dotnet/dotnet build dotnet-assembly-inspector.sln
```

## Execution command

```bash
node /ABS/PATH/dotnet-assembly-inspector/mcp-bridge/server.js
```

Optional args:

- `--dotnet /path/to/dotnet` (default: `DOTNET_BIN` env or `/usr/local/share/dotnet/dotnet`)
- `--project /path/to/src/AssemblyInspector.Cli` (default: `src/AssemblyInspector.Cli`)
- `--configuration Release|Debug` (default: `Release`)
- `--no-no-build` to disable default `--no-build` forwarding

## Input/output flow

1. MCP client (Claude Code/Cline) sends `tools/call` over stdio.
2. Bridge writes MCP arguments to a temporary request JSON.
3. Bridge runs:
   `dotnet run --project src/AssemblyInspector.Cli --no-build -c Release -- --mcp-tool <tool> --request <tmp-request.json>`
4. CLI returns JSON to stdout.
5. Bridge returns MCP tool result with:
   - `structuredContent`: raw CLI JSON object
   - `content[0].text`: pretty JSON string

## Claude Code config example

Use your Claude Code MCP config file and register this server:

```json
{
  "mcpServers": {
    "dotnet-assembly-inspector": {
      "command": "node",
      "args": [
        "/ABS/PATH/dotnet-assembly-inspector/mcp-bridge/server.js"
      ],
      "env": {
        "DOTNET_BIN": "/usr/local/share/dotnet/dotnet",
        "DOTNET_ROLL_FORWARD": "Major"
      }
    }
  }
}
```

## Cline config example

In your Cline MCP server settings, register:

```json
{
  "cline.mcpServers": {
    "dotnet-assembly-inspector": {
      "command": "node",
      "args": [
        "/ABS/PATH/dotnet-assembly-inspector/mcp-bridge/server.js"
      ],
      "env": {
        "DOTNET_BIN": "/usr/local/share/dotnet/dotnet",
        "DOTNET_ROLL_FORWARD": "Major"
      }
    }
  }
}
```

## Smoke check

Run an end-to-end MCP stdio smoke check:

```bash
node scripts/smoke_mcp_bridge.js
```

Optional sample assembly path:

```bash
node scripts/smoke_mcp_bridge.js /ABS/PATH/SomeAssembly.dll
```
