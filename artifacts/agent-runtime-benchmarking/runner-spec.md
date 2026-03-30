# Benchmark Runner Specification

## Purpose

This document defines how to run the agent-runtime benchmark suite in other environments, including different machines and OpenClaw-like runtimes.

## Goal

Enable reproducible benchmark execution across environments without depending on one specific local setup.

## Execution model

The benchmark suite is currently designed as a **document-driven benchmark kit**. That means:
- task definitions live in `task-specs/`
- scoring guidance lives in the scorecard and rubric documents
- run artifacts live under `run-results/`

A compatible environment should be able to reproduce the benchmark if it can:
1. read the task spec
2. execute the task through its own runtime
3. write durable outputs to a designated repository or workspace
4. preserve logs or equivalent evidence
5. score the result against the same rubric

## Minimum environment requirements

A benchmark environment should provide:
- an agent runtime capable of file operations and shell execution when required
- a durable workspace or repository for artifact output
- a way to preserve execution traces, logs, or command history
- enough isolation to attribute results to the runtime under test

## Portable run procedure

For each task:
1. select the runtime under test
2. fix the model where runtime comparison is intended
3. provide the task prompt exactly as defined in the task spec
4. constrain the output root to the benchmark workspace or topic home
5. capture:
   - prompt used
   - runtime identity
   - model identity
   - artifacts produced
   - logs or equivalent traces
   - verification evidence
6. store results in a normalized run folder

## Recommended run folder layout

```text
run-results/
  <suite-name>/
    <runtime-id>/
      <task-id>.md
      artifacts/
      logs/
```

If the runtime cannot export logs directly, a human evaluator should record a structured run note containing the same essential evidence.

## Required metadata per run

Each run record should include:
- benchmark suite name
- task id
- runtime id
- runtime version
- model id
- execution date/time
- environment identifier (machine or host label)
- artifact paths
- verification method
- final status

## Cross-environment fairness rules

To compare runtimes fairly:
- keep the task prompt identical
- keep the model identical when evaluating runtime quality
- keep tool permissions as close as possible
- note any environment-specific constraints explicitly
- do not hide human intervention

## Output normalization

When comparing different systems, normalize outputs into a common evaluation view:
- success / partial / failure
- artifact quality
- verification integrity
- efficiency metrics
- policy compliance
- reporting quality

## Current limitation

This specification does not yet define an automatic runner binary or protocol. It defines a reproducible process and file layout so that different environments can run the same tasks in a controlled way.

## Recommended next step

If repeated cross-machine evaluations become frequent, implement a lightweight benchmark runner that:
- loads `task-specs/`
- records metadata in a standard schema
- writes run artifacts into the standard layout
- exports score-ready summaries
