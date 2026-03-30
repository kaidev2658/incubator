# Recommendations for Evaluating OpenClaw-like Agents

## Objective

Establish an evaluation stack that can detect differences caused by runtime design even when the underlying model remains constant.

## Recommended benchmark portfolio

### Tier 1: coding-agent benchmark
Use **SWE-bench Verified** as the primary external coding benchmark.
- Reason: it is outcome-oriented, widely recognized, and close to realistic repository work.
- Use case: compare coding runtimes, patch loops, and repository task completion.

### Tier 2: terminal and execution benchmark
Add **Terminal-bench** or an equivalent terminal-workflow benchmark.
- Reason: OpenClaw-like systems often differ in shell behavior, approvals, retries, and execution loops.
- Use case: isolate runtime differences that SWE-bench alone may blur.

### Tier 3: tool-use benchmark
Add one strong tool-use benchmark such as **BFCL** and one multi-turn policy/tool benchmark such as **τ-bench**.
- Reason: these reveal schema fidelity, tool planning quality, and policy-aware execution.
- Use case: compare tool orchestration quality independent of repository coding.

### Tier 4: general long-horizon benchmark
Use **GAIA** or **AgentBench** selectively.
- Reason: these help measure broader reasoning and multi-step task performance.
- Use case: evaluate general assistant capability and long-horizon robustness.

## Recommended internal benchmark design

External benchmarks are necessary but not sufficient. Build an internal benchmark set that reflects the actual product.

Suggested internal task families:
1. direct coding task
2. repo investigation + patch + verification
3. multi-agent delegation task
4. tool-heavy operational task
5. artifact-generation task
6. failure-recovery task
7. policy-constrained task

Each internal task should record:
- success/failure
- artifact quality
- tool calls used
- retries
- time to completion
- human intervention count
- final verification result

## Recommended evaluation methodology

### Hold the model constant
To isolate runtime quality, compare multiple runtimes with the same model whenever possible.

### Run repeated trials
Run multiple trials per benchmark item or task family to estimate stability, not just best-case performance.

### Track operational metrics
Do not stop at benchmark pass rate. Track cost, time, retries, and intervention burden.

### Separate benchmark layers
Report results in separate views:
- coding
- tool use
- long-horizon reasoning
- operational reliability

## Decision framework

Use the following question when interpreting results:

> If two runtimes use the same model, which one produces better outcomes with fewer retries, less operator effort, and more reliable artifact verification?

That is the benchmark question that matters for agent products.

## Recommended next step

Build a small internal evaluation harness in the incubator repo that combines:
- one external coding benchmark reference
- one external tool-use benchmark reference
- one internal task suite derived from real OpenClaw workflows

This combination will be more useful than relying on any single public benchmark.
