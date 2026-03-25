#!/usr/bin/env bash
set -euo pipefail

# Optional local PATH bootstrap for common installs
if [[ -d "$HOME/tizen-studio" ]]; then
  export TIZEN_STUDIO="$HOME/tizen-studio"
  export PATH="$TIZEN_STUDIO/tools/ide/bin:$TIZEN_STUDIO/tools:$TIZEN_STUDIO/platform-tools:$PATH"
fi
if [[ -x "$HOME/.sdkman/candidates/gradle/current/bin/gradle" ]]; then
  export PATH="$HOME/.sdkman/candidates/gradle/current/bin:$PATH"
fi

echo "[kmp-cmp-poc] prerequisite check"

missing_required=()
missing_optional=()

check_cmd() {
  local cmd="$1"
  local kind="$2" # required|optional
  if command -v "$cmd" >/dev/null 2>&1; then
    echo "- OK: $cmd -> $(command -v "$cmd")"
  else
    echo "- MISSING: $cmd ($kind)"
    if [[ "$kind" == "required" ]]; then
      missing_required+=("$cmd")
    else
      missing_optional+=("$cmd")
    fi
  fi
}

# Required for current Phase-0 local validation
check_cmd "java" "required"

# Prefer gradle wrapper; standalone gradle is useful but optional after wrapper exists
if [[ -x "./gradlew" ]]; then
  echo "- OK: gradlew -> ./gradlew"
else
  check_cmd "gradle" "required"
fi

# Tizen CLI is required for Tizen phase
check_cmd "tizen" "required"
check_cmd "sdb" "required"

# sdkmanager is optional (Android toolchain), not required for Tizen-only path
check_cmd "sdkmanager" "optional"

echo
if ((${#missing_required[@]} > 0)); then
  echo "Missing required tools: ${missing_required[*]}"
  echo "Install required tools before running full checks."
  exit 1
fi

if ((${#missing_optional[@]} > 0)); then
  echo "Optional tools missing: ${missing_optional[*]}"
fi

echo "All required prerequisites found. You can run: ./gradlew check"
