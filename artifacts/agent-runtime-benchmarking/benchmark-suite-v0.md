# OpenClaw Runtime Evaluation Suite v0

## Objective

This suite is designed to compare agent runtimes while holding the underlying model constant as much as possible. The aim is to measure how much runtime design changes outcomes even when model quality is not the primary variable.

## Primary evaluation question

> If two agent runtimes use the same model, which runtime delivers better task outcomes with fewer retries, lower operating cost, stronger verification, and better reporting quality?

## Scope

Version 0 is intentionally compact. It is designed to be small enough to run repeatedly while still covering the core workflow surfaces that matter in OpenClaw-like systems.

## Baseline assumptions

- Model should be fixed across compared runtimes whenever possible.
- Each task should be run multiple times to estimate stability.
- Artifact-based evaluation is preferred over answer-only evaluation.
- Runtime behavior should be evaluated separately from raw model quality.

## Candidate runtime comparison modes

### Mode A. Main orchestrator flow
- K as the main orchestrator
- specialist routing when needed
- verification before final reporting

### Mode B. Alternative runtime under test
Examples:
- direct specialist path without orchestration
- coordinator-first flow
- external coding-agent harness path
- simplified single-agent runtime

The suite is designed so that different runtime strategies can be plugged into the same task list.

## Task families and target counts

### Family 1. New topic-home creation (2 tasks)
Goal:
- determine that the request is a new topic
- create a new topic home under `artifacts/`
- generate an initial durable artifact

Scoring focus:
- correct directory creation
- correct scoping
- quality of initial topic summary

### Family 2. Existing topic-home extension (2 tasks)
Goal:
- correctly identify an existing topic home
- update only that topic
- preserve continuity with prior artifacts

Scoring focus:
- directory discipline
- continuity quality
- unrelated file churn

### Family 3. Coding and verification (2 tasks)
Goal:
- inspect code or scripts
- produce a targeted change
- verify with tests or equivalent checks
- report results accurately

Scoring focus:
- patch correctness
- verification integrity
- false completion avoidance

### Family 4. Tool-use and execution loop (2 tasks)
Goal:
- choose and sequence tools correctly
- recover from routine execution friction
- stop without unnecessary loops

Scoring focus:
- tool correctness
- retry discipline
- execution efficiency

### Family 5. Orchestration and synthesis (2 tasks)
Goal:
- route work to the correct specialist(s)
- integrate outputs into a coherent final result
- reissue work if an intermediate result is insufficient

Scoring focus:
- routing quality
- synthesis quality
- final report usefulness

### Family 6. Failure and recovery (2 tasks)
Goal:
- detect inconsistency or failure
- recover safely
- report the state honestly

Scoring focus:
- recovery quality
- escalation quality
- avoidance of false success claims

## Total initial suite size
- 12 tasks
- recommended initial repeats: 3 runs per task per runtime

This yields enough signal to compare runtime behavior without creating an overly expensive first benchmark campaign.

## Required data capture per run

For every run, record:
- runtime name
- runtime version
- model name
- task id
- task family
- start/end time
- duration
- tool-call count
- retry count
- human interventions
- produced artifacts
- verification result
- final outcome (`success`, `partial`, `failure`)
- evaluator notes

## Scoring model

Use a layered score rather than a single number.

### Core dimensions
- Task completion
- Artifact quality
- Verification integrity
- Tool-use correctness
- Efficiency
- Reliability / stability
- Recovery behavior
- Policy compliance
- Reporting quality

### Suggested scales
- pass/fail for hard constraints
- 0-5 for graded dimensions

## Benchmark outputs

The suite should produce at least the following outputs:
- `task-catalog.md`
- `task-specs/<task-id>.md`
- `run-results/<runtime>/<date>/...`
- `scorecard-results.md`
- `comparative-summary.md`

## Evaluation protocol

1. fix the model
2. define the runtime variants
3. run the same task set on each runtime
4. capture artifacts and operational traces
5. score outcomes with the same rubric
6. compare not only success rate but also operating behavior

## Acceptance criteria for v0 usefulness

The suite is considered useful if it can clearly expose at least one of the following between two runtimes:
- different success rates on the same task family
- different artifact quality with the same model
- different retry or recovery behavior
- different verification integrity
- different operator burden

## Recommended next implementation step

After approving this v0 suite, create:
1. `task-catalog.md`
2. the first 12 task specs
3. a run-results folder structure
4. a first comparison between two runtimes using the same model
