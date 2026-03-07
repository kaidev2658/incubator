#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ARTIFACT_HOME="$(cd "$SCRIPT_DIR/../../.." && pwd)"
CALLER_DIR="$(pwd)"

export PATH="/usr/local/share/dotnet:$PATH"
export DOTNET_ROLL_FORWARD="${DOTNET_ROLL_FORWARD:-Major}"

if [[ $# -lt 1 ]]; then
  echo "Usage: $0 <input_path> [output_dir] [extra flags...]"
  echo "Example: $0 input/nupkg/tizen-ui output/local --all-tfms --compact"
  exit 1
fi

resolve_from_caller() {
  local p="$1"
  if [[ "$p" = /* ]]; then
    printf '%s\n' "$p"
  else
    printf '%s\n' "$CALLER_DIR/$p"
  fi
}

INPUT_PATH="$(resolve_from_caller "$1")"
OUTPUT_DIR="$(resolve_from_caller "${2:-output/local-skill}")"

if [[ $# -ge 2 ]]; then
  shift 2
else
  shift 1
fi

cd "$ARTIFACT_HOME"

if [[ ! -e "$INPUT_PATH" ]]; then
  echo "[error] input path not found: $INPUT_PATH"
  exit 2
fi

mkdir -p "$OUTPUT_DIR"

echo "[info] artifact home : $ARTIFACT_HOME"
echo "[info] input         : $INPUT_PATH"
echo "[info] output        : $OUTPUT_DIR"

dotnet run \
  --project src/AssemblyInspector.Cli \
  --no-build \
  -c Release \
  -- \
  "$INPUT_PATH" "$OUTPUT_DIR" "$@"

echo "[ok] inspection complete"
