#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
A2UI_DIR="$ROOT_DIR/artifacts/a2ui-analysis"
SOLUTION="$A2UI_DIR/a2ui-analysis.slnx"

DOTNET_BIN="${DOTNET_BIN:-/usr/local/share/dotnet/dotnet}"

if [[ ! -x "$DOTNET_BIN" ]]; then
  DOTNET_BIN="dotnet"
fi

if ! command -v "$DOTNET_BIN" >/dev/null 2>&1; then
  echo "[ERROR] dotnet not found. Install .NET SDK or set DOTNET_BIN." >&2
  exit 1
fi

if [[ ! -f "$SOLUTION" ]]; then
  echo "[ERROR] solution not found: $SOLUTION" >&2
  exit 1
fi

echo "[INFO] Running A2UI tests via solution: $SOLUTION"
exec "$DOTNET_BIN" test "$SOLUTION" -v minimal "$@"
