#!/usr/bin/env bash
set -euo pipefail
BASE_DIR="$(cd "$(dirname "$0")/.." && pwd)"
OUT_DIR="$BASE_DIR/input/nupkg/tizen-ui"
VERSION="1.0.0-rc.4"
PACKAGES=(
  "Tizen.UI.Layouts"
  "Tizen.UI"
  "Tizen.UI.Primitives2D"
  "Tizen.UI.Widget"
  "Tizen.UI.Components"
  "Tizen.UI.Tools"
  "Tizen.UI.WindowBorder"
  "Tizen.UI.Components.Material"
  "Tizen.UI.Visuals"
  "Tizen.UI.Scene3D"
  "Tizen.UI.Skia"
)

mkdir -p "$OUT_DIR"
for id in "${PACKAGES[@]}"; do
  lid="$(echo "$id" | tr '[:upper:]' '[:lower:]')"
  url="https://api.nuget.org/v3-flatcontainer/${lid}/${VERSION}/${lid}.${VERSION}.nupkg"
  out="$OUT_DIR/${id}.${VERSION}.nupkg"
  echo "Downloading $id ..."
  curl -fsSL "$url" -o "$out"
done

echo "Done. Files in: $OUT_DIR"
