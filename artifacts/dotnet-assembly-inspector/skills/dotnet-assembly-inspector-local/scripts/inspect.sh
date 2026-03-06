#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ARTIFACT_HOME="$(cd "$SCRIPT_DIR/../../.." && pwd)"

export PATH="/usr/local/share/dotnet:$PATH"
export DOTNET_ROLL_FORWARD="${DOTNET_ROLL_FORWARD:-Major}"

if [[ $# -lt 1 ]]; then
  echo "Usage: $0 <input_path> [output_dir] [extra flags...]"
  echo "Example: $0 input/nupkg/tizen-ui output/local --all-tfms --compact"
  exit 1
fi

INPUT_PATH="$1"
OUTPUT_DIR="${2:-output/local-skill}"

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
