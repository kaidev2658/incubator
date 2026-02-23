#!/bin/bash
set -euo pipefail
rsync -a --delete docs/ pages-latest/
