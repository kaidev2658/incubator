#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
API_DIR="$ROOT_DIR/app/MockOrchestratorApi"
OUT_DIR="$ROOT_DIR/eval"
TIMESTAMP="$(date +%Y%m%d-%H%M%S)"
API_LOG="$OUT_DIR/local-e2e-api-$TIMESTAMP.log"
SCN01_META_LOG="$OUT_DIR/local-e2e-scn01-$TIMESTAMP.log"
UI_META_LOG="$OUT_DIR/local-e2e-ui-demo-$TIMESTAMP.log"
KPI_JSON="$OUT_DIR/local-e2e-kpi-$TIMESTAMP.json"
API_BASE_URL="${ORCHESTRATOR_API_BASE_URL:-http://127.0.0.1:5081}"

mkdir -p "$OUT_DIR"
export PATH="/usr/local/share/dotnet:$PATH"

cleanup() {
  if [[ -n "${API_PID:-}" ]] && kill -0 "$API_PID" >/dev/null 2>&1; then
    kill "$API_PID" >/dev/null 2>&1 || true
    wait "$API_PID" >/dev/null 2>&1 || true
  fi
}
trap cleanup EXIT

echo "[LOCAL-E2E] dotnet info"
/usr/local/share/dotnet/dotnet --info >/dev/null

echo "[LOCAL-E2E] build mock orchestrator api"
(
  cd "$API_DIR"
  /usr/local/share/dotnet/dotnet build
) >/dev/null

echo "[LOCAL-E2E] start mock orchestrator api -> $API_LOG"
(
  cd "$API_DIR"
  ASPNETCORE_URLS="$API_BASE_URL" /usr/local/share/dotnet/dotnet run --no-launch-profile --no-build
) >"$API_LOG" 2>&1 &
API_PID=$!

READY=0
for _ in $(seq 1 160); do
  if curl -sSf "$API_BASE_URL/apps/scn01-miniapp" >/dev/null 2>&1; then
    READY=1
    break
  fi
  sleep 0.25
done

if [[ "$READY" -ne 1 ]]; then
  echo "[LOCAL-E2E] ERROR: mock orchestrator api did not become ready"
  tail -n 50 "$API_LOG" || true
  exit 1
fi

echo "[LOCAL-E2E] run scn01"
SCN01_OUTPUT="$(ORCHESTRATOR_API_BASE_URL="$API_BASE_URL" "$ROOT_DIR/scripts/run-scn01.sh")"
printf '%s\n' "$SCN01_OUTPUT" | tee "$SCN01_META_LOG"
SCN01_KPI_PATH="$(printf '%s\n' "$SCN01_OUTPUT" | awk -F= '/^kpi_json=/{print $2}' | tail -n 1)"

if [[ -z "$SCN01_KPI_PATH" || ! -f "$SCN01_KPI_PATH" ]]; then
  echo "[LOCAL-E2E] ERROR: scn01 KPI path missing"
  exit 1
fi

echo "[LOCAL-E2E] run ui-demo"
UI_OUTPUT="$(ORCHESTRATOR_API_BASE_URL="$API_BASE_URL" "$ROOT_DIR/scripts/run-ui-demo.sh")"
printf '%s\n' "$UI_OUTPUT" | tee "$UI_META_LOG"
UI_KPI_PATH="$(printf '%s\n' "$UI_OUTPUT" | awk -F= '/^kpi_json=/{print $2}' | tail -n 1)"

if [[ -z "$UI_KPI_PATH" || ! -f "$UI_KPI_PATH" ]]; then
  echo "[LOCAL-E2E] ERROR: ui-demo KPI path missing"
  exit 1
fi

SCN01_KPI_PAYLOAD="$(cat "$SCN01_KPI_PATH")"
UI_KPI_PAYLOAD="$(cat "$UI_KPI_PATH")"

cat > "$KPI_JSON" <<EOF_JSON
{
  "scenario_id": "SCN-01",
  "execution_mode": "full-local-e2e-with-mock-orchestrator-api",
  "api_base_url": "$API_BASE_URL",
  "scn01_kpi": $SCN01_KPI_PAYLOAD,
  "ui_demo_kpi": $UI_KPI_PAYLOAD,
  "artifacts": {
    "api_log": "$API_LOG",
    "scn01_meta_log": "$SCN01_META_LOG",
    "ui_meta_log": "$UI_META_LOG",
    "scn01_kpi": "$SCN01_KPI_PATH",
    "ui_demo_kpi": "$UI_KPI_PATH"
  }
}
EOF_JSON

echo "[LOCAL-E2E] done"
echo "api_log=$API_LOG"
echo "scn01_meta_log=$SCN01_META_LOG"
echo "ui_meta_log=$UI_META_LOG"
echo "kpi_json=$KPI_JSON"
