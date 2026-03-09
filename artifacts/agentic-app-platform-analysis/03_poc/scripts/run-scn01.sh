#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
RUNTIME_DIR="$ROOT_DIR/app/TizenMiniAppRuntimeMock"
OUT_DIR="$ROOT_DIR/eval"
TIMESTAMP="$(date +%Y%m%d-%H%M%S)"
BUILD_LOG="$OUT_DIR/scn01-build-$TIMESTAMP.log"
RUN_LOG="$OUT_DIR/scn01-run-$TIMESTAMP.log"
KPI_JSON="$OUT_DIR/scn01-kpi-$TIMESTAMP.json"

mkdir -p "$OUT_DIR"

export PATH="/usr/local/share/dotnet:$PATH"

echo "[SCN-01] dotnet info"
/usr/local/share/dotnet/dotnet --info >/dev/null

echo "[SCN-01] build -> $BUILD_LOG"
(
  cd "$RUNTIME_DIR"
  /usr/local/share/dotnet/dotnet build
) | tee "$BUILD_LOG"

echo "[SCN-01] run scenario -> $RUN_LOG"
(
  cd "$RUNTIME_DIR"
  /usr/local/share/dotnet/dotnet run -- run-scn01
) | tee "$RUN_LOG"

SCN01_KPI_LINE="$(grep -E '^SCN01_KPI_JSON=' "$RUN_LOG" | tail -n 1 || true)"
if [[ -z "$SCN01_KPI_LINE" ]]; then
  echo "[SCN-01] ERROR: KPI JSON line not found in runtime output."
  exit 1
fi

SCN01_KPI_PAYLOAD="${SCN01_KPI_LINE#SCN01_KPI_JSON=}"
printf '%s\n' "$SCN01_KPI_PAYLOAD" >"$KPI_JSON"

echo "[SCN-01] done"
echo "build_log=$BUILD_LOG"
echo "run_log=$RUN_LOG"
echo "kpi_json=$KPI_JSON"
