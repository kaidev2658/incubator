# Internal Benchmark Specification for OpenClaw-like Agents

## Purpose

This document defines an internal benchmark structure for evaluating agent systems where runtime behavior, orchestration quality, tool policy, and verification loops matter as much as raw model capability.

## Design goal

The benchmark should make it possible to compare two agent runtimes that use the same model and still identify meaningful differences in:
- success rate
- artifact quality
- efficiency
- stability
- recovery behavior
- operational burden

## Benchmark principles

1. **Model-controlled comparison**
   - Hold the underlying model constant when the goal is runtime comparison.

2. **Workflow realism**
   - Prefer tasks that resemble actual OpenClaw workflows over synthetic toy tasks.

3. **Artifact-based evaluation**
   - Score outputs based on actual artifacts, not only conversational answers.

4. **Repeatability**
   - Each task should be runnable multiple times under the same setup.

5. **Observability**
   - Runs should preserve logs, tool calls, and result traces for later diagnosis.

## Task families

### Family A. Topic-home creation tasks
The agent receives a new research or implementation request and must:
- determine that the task is a new topic
- create a new topic home under `artifacts/`
- produce a structured initial output
- keep work scoped to the new topic home

Example checks:
- correct topic-home creation
- no spillover into unrelated directories
- clear initial README or summary artifact

### Family B. Existing topic extension tasks
The agent receives a follow-up request against an existing topic and must:
- identify the correct topic home
- update only that topic home unless restructure is explicitly requested
- preserve prior work while extending it

Example checks:
- correct reuse of existing topic home
- minimal unrelated file churn
- continuity of document structure

### Family C. Coding and verification tasks
The agent must:
- inspect a repository or codebase
- modify files
- run tests or validation
- report changes with verification status

Example checks:
- patch correctness
- tests passed
- verification claims match reality

### Family D. Tool-use and execution tasks
The agent must:
- choose tools correctly
- execute a sequence of steps
- recover from command or tool errors
- avoid unnecessary calls

Example checks:
- correct tool sequence
- parameter correctness
- retry discipline
- avoidance of dead loops

### Family E. Multi-agent orchestration tasks
The agent must:
- route work to the right specialist
- integrate returned outputs
- reissue instructions when outputs are incomplete
- produce a final unified report

Example checks:
- routing accuracy
- specialist selection quality
- integration quality
- final answer coherence

### Family F. Failure and recovery tasks
The runtime is intentionally exposed to a partial failure condition, such as:
- stale state
- tool timeout
- missing artifact
- inconsistent callback and result state

Example checks:
- failure detection
- recovery quality
- fallback quality
- escalation clarity

### Family G. Policy and boundary adherence tasks
The agent must operate under explicit constraints, such as:
- workspace boundary
- read-only expectation
- reporting format requirements
- approval requirements for sensitive actions

Example checks:
- no forbidden actions
- proper escalation behavior
- compliance with required reporting format

## Execution protocol

For each benchmark task:
1. define the prompt
2. define the expected artifact or measurable outcome
3. define allowed tools or constraints
4. run the task multiple times
5. capture logs, tool-call traces, timing, and outputs
6. score both outcome and operating behavior

## Minimum metadata to capture per run

- task id
- runtime version
- model id
- specialist agents used
- start/end timestamps
- wall-clock duration
- token usage if available
- tool-call count
- retry count
- human intervention count
- produced artifacts
- verification result
- final status (`success`, `partial`, `failure`)

## Scoring dimensions

Each task should be scored on a 0-5 or pass/fail scale across:
- task completion
- artifact quality
- verification integrity
- efficiency
- recovery quality
- policy compliance
- report quality

## Recommended benchmark suite shape

A practical first internal suite can be small:
- 3 topic-home creation tasks
- 3 existing-topic extension tasks
- 4 coding/verification tasks
- 3 tool-use tasks
- 3 orchestration tasks
- 3 failure-recovery tasks
- 2 policy-adherence tasks

This creates a compact but meaningful benchmark set that is broad enough to expose runtime differences.

## Working conclusion

A useful internal benchmark for OpenClaw-like systems should evaluate not just whether a task was solved, but whether it was solved in the correct place, with the right tools, with verifiable outputs, and with production-worthy operating behavior.
