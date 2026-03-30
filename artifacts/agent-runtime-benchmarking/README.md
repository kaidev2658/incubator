# Agent Runtime Benchmarking

This topic home collects research on how to evaluate the performance of agent systems, with a focus on cases where the underlying model may be the same but the surrounding runtime, tool loop, execution policy, memory strategy, and orchestration design differ.

## Objective

The primary goal is to establish a practical evaluation framework for comparing agents under development, especially when raw model quality alone does not explain observed performance differences.

## Scope

This topic covers:
- benchmark landscape for agent systems
- evaluation criteria for agent runtimes
- benchmark-to-use-case mapping
- recommendations for building an internal evaluation stack

## Documents

- [benchmark-landscape.md](benchmark-landscape.md): major benchmark categories, what they measure, and where they fit
- [evaluation-criteria.md](evaluation-criteria.md): practical criteria for evaluating agent runtimes beyond raw model performance
- [recommendations.md](recommendations.md): recommended benchmark portfolio and adoption path for OpenClaw-like systems
- [openclaw-benchmark-cases.md](openclaw-benchmark-cases.md): benchmark cases and benchmark-like evaluation setups directly relevant to OpenClaw or OpenClaw-style systems
- [internal-benchmark-spec.md](internal-benchmark-spec.md): internal benchmark design for comparing OpenClaw-like runtimes on realistic workflows
- [scorecard-template.md](scorecard-template.md): reusable evaluation scorecard for runtime comparison runs
- [benchmark-suite-v0.md](benchmark-suite-v0.md): first compact experiment design for runtime comparison under a fixed-model setup
- [task-catalog.md](task-catalog.md): initial 12-task catalog for the evaluation suite
- [task-specs/](task-specs/): per-task specifications for the pilot benchmark slice
- [comparative-summary-template.md](comparative-summary-template.md): template for comparing two runtime conditions after a pilot run
