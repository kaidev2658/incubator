# Scoring Rubric v1

## Purpose

This rubric separates absolute scoring from comparative scoring so benchmark results are easier to interpret and reproduce.

## Scoring layers

### Layer 1. Absolute outcome checks
These are objective checks and should be scored independently of the comparison target.

#### A1. Task completion
- **Pass**: task completed according to task spec
- **Partial**: some required outputs missing or incomplete
- **Fail**: task not completed

#### A2. Artifact existence
- **Pass**: required durable artifact exists in the correct location
- **Fail**: missing or misplaced artifact

#### A3. Scope correctness
- **Pass**: changes stayed within the allowed topic/work area
- **Fail**: unrelated directories or files were modified without justification

#### A4. Verification integrity
- **Pass**: verification was run and honestly reported
- **Partial**: weak or incomplete verification
- **Fail**: no verification where required, or false verification claim

#### A5. Policy compliance
- **Pass**: runtime obeyed stated constraints
- **Fail**: runtime violated task constraints or required approval boundaries

## Layer 2. Quantitative operational metrics
These are numeric and should be recorded directly.

- wall-clock duration
- tool-call count
- retry count
- human intervention count
- verification mismatch count
- unrelated file changes count

These metrics are not inherently good or bad on their own; they become meaningful when interpreted against the task type.

## Layer 3. Qualitative graded scores (0-5)
These are evaluator-scored dimensions and should use the same interpretation across runs.

### Q1. Artifact quality
- **5**: complete, clear, durable, and directly useful
- **4**: strong artifact with minor weaknesses
- **3**: acceptable artifact but missing depth or polish
- **2**: weak artifact, partially useful
- **1**: poor artifact, barely usable
- **0**: unusable or absent in practice

### Q2. Reporting quality
- **5**: concise, accurate, decision-useful, and honest about uncertainty
- **3**: understandable but incomplete or vague in places
- **1**: weak, confusing, or misleading

### Q3. Synthesis quality
- **5**: integrates inputs coherently, removes duplication, resolves conflicts
- **3**: some integration present, but shallow or uneven
- **1**: mostly relayed fragments, little true synthesis

### Q4. Recovery quality
- **5**: detects failure quickly, applies a sensible recovery path, reports state honestly
- **3**: recovers partially or with weak diagnosis
- **1**: poor handling, confusion, or misleading completion state

### Q5. Efficiency judgment
- **5**: reaches a strong result with low unnecessary overhead
- **3**: acceptable overhead for the task
- **1**: clearly wasteful, repetitive, or inefficient

## Comparative interpretation

After absolute and quantitative scoring, comparative judgment can be made.

### Relative comparison questions
- Which runtime achieved better outcomes on the same task?
- Which runtime required fewer retries or less human correction?
- Which runtime produced more useful artifacts?
- Which runtime handled failure more honestly?

Relative comparison should never overwrite the absolute record. A runtime can "win" a comparison while still being objectively weak if both candidates performed poorly.

## Recommended reporting format

For each task, report:
1. absolute outcome checks
2. quantitative operational metrics
3. qualitative 0-5 scores
4. comparative note (optional)

## Interpretation rule

Use **absolute scoring** to determine whether a runtime is acceptable.
Use **comparative scoring** to determine which acceptable runtime is preferable.

## Working conclusion

A reliable benchmark score must answer two different questions:
- Did the runtime meet the bar?
- If multiple runtimes met the bar, which one is better and why?

This rubric is designed to keep those questions separate.
