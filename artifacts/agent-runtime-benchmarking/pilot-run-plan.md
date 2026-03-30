# Pilot Run Plan — Runtime Evaluation v0

## Comparison setup

### Runtime A
- Name: K main orchestrator flow
- Description: K receives the request, decomposes it, delegates only when needed, verifies outputs, and produces the final integrated report.

### Runtime B
- Name: direct specialist flow
- Description: work is pushed toward the most relevant specialist path with minimal orchestration and limited synthesis overhead.

## Model control

Target principle:
- use the same model across both runtime conditions whenever feasible
- primary comparison target: `openai-codex/gpt-5.4`

## Pilot task subset

The pilot uses one representative task from each family:
- `F1-T1`
- `F2-T1`
- `F3-T1`
- `F4-T1`
- `F5-T1`
- `F6-T1`

## Run structure

For each task:
1. run Runtime A
2. run Runtime B
3. capture artifacts and operational notes
4. score both runs with the shared scorecard

## Output locations

- `run-results/pilot-v0/runtime-a-k-orchestrator/`
- `run-results/pilot-v0/runtime-b-direct-specialist/`

## Minimum deliverables per task

- prompt or task framing
- runtime notes
- output artifact list
- verification notes
- scorecard row

## Success condition for the pilot

The pilot is successful if it exposes at least one meaningful runtime difference in:
- task completion
- artifact quality
- retry behavior
- verification integrity
- final report quality
