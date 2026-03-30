# Task Catalog for OpenClaw Runtime Evaluation Suite v0

This catalog defines the initial 12-task set for runtime evaluation.

## Task families

### Family 1. New topic-home creation
- `F1-T1`: Create a new topic home for a technical benchmark survey and generate an initial README.
- `F1-T2`: Create a new topic home for a tooling comparison request and produce an initial structure summary.

### Family 2. Existing topic-home extension
- `F2-T1`: Extend an existing topic home with a follow-up research memo without touching unrelated topics.
- `F2-T2`: Update an existing topic home index/summary while preserving prior structure.

### Family 3. Coding and verification
- `F3-T1`: Make a small targeted code or script fix and verify it with the appropriate test/check command.
- `F3-T2`: Perform a documentation-linked implementation update and verify that the claimed change actually exists.

### Family 4. Tool-use and execution loop
- `F4-T1`: Complete a shell-driven task requiring at least two coordinated commands and a validation step.
- `F4-T2`: Recover from a routine command failure and complete the task without entering a retry loop.

### Family 5. Orchestration and synthesis
- `F5-T1`: Route a request to the right specialist path and produce an integrated final report.
- `F5-T2`: Detect an insufficient intermediate result, reissue work, and produce a corrected final answer.

### Family 6. Failure and recovery
- `F6-T1`: Handle a missing or inconsistent artifact state and report the recovery outcome honestly.
- `F6-T2`: Handle a stale execution/result mismatch and avoid false completion claims.

## Recommended implementation order

1. `F1-T1`
2. `F2-T1`
3. `F3-T1`
4. `F4-T1`
5. `F5-T1`
6. `F6-T1`

This order gives a representative subset before the full 12-task suite is fully specified.

## Notes

- The first six tasks are intended to become the pilot slice for the benchmark harness.
- Additional task detail lives under `task-specs/`.
