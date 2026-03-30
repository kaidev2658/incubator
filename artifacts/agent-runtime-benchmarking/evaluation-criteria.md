# Evaluation Criteria for Agent Runtimes

## Core principle

If two agents use the same underlying model, performance differences usually come from runtime design rather than model capability. Evaluation therefore needs to target runtime behavior directly.

## Recommended evaluation dimensions

### 1. Task completion rate
- Percentage of tasks fully completed according to benchmark rules.
- This should remain the primary top-line metric.

### 2. Quality of completion
- Whether the produced output is correct, robust, and usable.
- For coding tasks, include tests passed, regressions avoided, and patch validity.

### 3. Tool-use correctness
- Whether the agent selected the right tool
- Whether it called tools with valid parameters
- Whether it recovered cleanly from tool failure

### 4. Efficiency
- Number of turns
- Number of tool calls
- Wall-clock time
- Token consumption
- Human interventions required

A runtime that reaches the same answer with fewer retries, fewer tokens, and fewer human interruptions is materially better.

### 5. Reliability and stability
- Variance across repeated runs
- Failure mode consistency
- Susceptibility to dead loops, premature stopping, or malformed outputs

### 6. Recovery behavior
- Ability to detect failure
- Ability to retry with a sensible strategy
- Ability to recover from partial progress without restarting from zero

### 7. Policy and constraint adherence
- Whether the agent obeys instructions, safety constraints, formatting requirements, and environment boundaries.
- This matters for real production systems even if raw benchmark scores look good.

### 8. Observability and auditability
- Can the runtime explain what it did?
- Are decisions, tool calls, and artifacts inspectable?
- Can failures be diagnosed without guesswork?

For production agents, observability is part of performance because an un-debuggable runtime is expensive even when it scores well.

## Runtime-specific metrics worth adding

For OpenClaw-like systems, benchmark scores should be augmented with runtime telemetry such as:
- task routing accuracy
- specialist selection quality
- retry count per task
- sandbox or permission escalation frequency
- artifact creation success rate
- post-run verification success rate
- callback-to-artifact consistency

## Recommended scoring view

Use a layered scorecard instead of a single scalar score:
- **Outcome:** did the agent complete the task?
- **Quality:** was the output correct and usable?
- **Efficiency:** what did it cost in time, tokens, and tool calls?
- **Reliability:** how repeatable was the result?
- **Operational quality:** was the run observable, policy-compliant, and recoverable?

## Working conclusion

A strong runtime is not just the one that solves the most tasks. It is the one that solves them reliably, efficiently, and in a way that can be operated in production.
