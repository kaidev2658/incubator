#!/bin/bash
set -euo pipefail
rsync -a --delete artifacts/ pages-latest/
