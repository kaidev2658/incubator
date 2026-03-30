# Comparative Summary — Pilot v0

## Evaluation scope

Pilot v0 compared two runtime styles under the same general benchmark framing:

- **Runtime A:** K main orchestrator flow
- **Runtime B:** direct specialist flow

The pilot used one representative task from each of six benchmark families:
- F1-T1 new topic-home creation
- F2-T1 existing topic-home extension
- F3-T1 coding and verification
- F4-T1 tool-use and execution loop
- F5-T1 orchestration and synthesis
- F6-T1 failure and recovery

## High-level result

Both runtime styles were able to complete the pilot task set. However, the quality of performance differed by task family.

### Pattern observed
- **Runtime B (direct specialist)** tends to be competitive on narrower, lower-overhead execution tasks.
- **Runtime A (K main orchestrator)** tends to be stronger when the task depends on contextual continuity, synthesis quality, explicit verification, or honest recovery behavior.

## Task-family interpretation

### F1-T1 — New topic-home creation
Both runtimes succeeded.
- Runtime A produced a somewhat broader framing of the new topic.
- Runtime B produced a leaner artifact with cleaner direct execution.

### F2-T1 — Existing topic-home extension
Both runtimes succeeded.
- Runtime A produced a richer continuation artifact with stronger contextual linkage.
- Runtime B produced a shorter incremental memo while preserving continuity.

### F3-T1 — Coding and verification
Both runtimes succeeded.
- The task was too deterministic to produce strong runtime separation.
- The main value of this task was checking durable change + explicit verification.

### F4-T1 — Tool-use and execution loop
Both runtimes succeeded.
- Runtime B had a slight advantage in execution simplicity.
- Runtime A did not gain much from orchestration on such a small operational task.

### F5-T1 — Orchestration and synthesis
Both runtimes succeeded.
- Runtime A showed a modest advantage on synthesis-heavy output.
- Runtime B remained acceptable but less context-rich.

### F6-T1 — Failure and recovery
Both runtimes handled the missing-state condition honestly.
- Runtime A appeared stronger in explicit verification-first recovery framing.
- This was the clearest family for exposing production-relevant runtime differences.

## Working conclusion

The pilot suggests that runtime differences are real, but task-dependent.

### Where direct-specialist flow is competitive
- narrow operational tasks
- low-synthesis tasks
- small, deterministic updates

### Where orchestrated flow is stronger
- context-sensitive topic work
- synthesis-heavy tasks
- verification-sensitive tasks
- failure and recovery handling

## Operational recommendation

If the goal is production-quality performance for OpenClaw-like systems, the early evidence favors **K main orchestrator flow** as the more reliable default operating mode, especially where verification integrity and recovery behavior matter.

However, the pilot also suggests that some low-overhead tasks may benefit from a more direct execution path. That means the most promising future design is likely not pure orchestration or pure direct-specialist execution, but a runtime that:
- uses orchestration by default for complex or high-risk tasks
- falls back to direct execution for simpler, low-synthesis tasks

## Recommended next step

Move to **v1** with tasks that better amplify runtime differences, especially in:
- multi-step coding with verification pressure
- recovery from inconsistent state
- multi-agent delegation and reissue quality
- policy-constrained execution under failure conditions
