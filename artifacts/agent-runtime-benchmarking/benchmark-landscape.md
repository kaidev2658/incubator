# Benchmark Landscape for Agent Runtime Evaluation

## Why this topic matters

For agent systems, the model is only one part of the stack. Two systems may use the same model and still produce materially different outcomes because of differences in:
- task decomposition
- tool selection and tool-call formatting
- retry behavior and stopping criteria
- filesystem and terminal execution policy
- context pruning and memory retrieval
- validation and self-check loops
- orchestration across specialist agents

As a result, model-centric benchmarks are not sufficient on their own. Agent evaluation needs benchmarks that stress execution, interaction, and end-to-end task completion.

## Benchmark categories

### 1. Software engineering and coding-agent benchmarks

These are the closest fit when the agent performs repository work, debugging, editing, test execution, or patch generation.

#### SWE-bench family
- Measures whether an agent can resolve real software engineering tasks derived from GitHub issues.
- Strong fit for coding agents that edit code, run tests, and work inside realistic repositories.
- Useful variants include:
  - SWE-bench
  - SWE-bench Verified
  - SWE-bench Lite
  - SWE-bench Multilingual
  - SWE-bench Multimodal
- Strengths:
  - realistic repository context
  - outcome-based scoring
  - broad community adoption
- Limitations:
  - software engineering only
  - less informative for orchestration, messaging, and non-code workflows

#### Terminal-bench
- Measures multi-step terminal workflows such as compiling, debugging, environment setup, and shell-driven task completion.
- Strong fit for agents whose runtime quality depends on shell execution policy and iterative command usage.
- Particularly useful when comparing runtimes that expose different command approval, retry, or sandbox behavior.
- Limitation: narrower domain than a full multi-agent platform.

#### Live software-development benchmarks
Examples often discussed alongside SWE-bench include LiveSWEBench, StackEval, MLE-bench, and related live or workflow-oriented coding evaluations.
- Use these when you need to test agents under more dynamic or production-like coding conditions.
- They are useful as supplements, not as the only benchmark basis.

### 2. Tool-use and function-calling benchmarks

These benchmarks isolate the agent's ability to choose and correctly use tools or APIs.

#### BFCL (Berkeley Function Calling Leaderboard)
- Measures function-calling quality across simple, parallel, and multi-turn interactions.
- Useful for evaluating schema adherence, parameter selection, and tool-use reliability.
- Good for comparing runtimes that differ in tool prompting, parser robustness, or tool-call validation.
- Limitation: can overstate real-world capability if the runtime still struggles with long-horizon planning.

#### ToolBench / API-focused tool benchmarks
- Measures planning and execution over large API or tool spaces.
- Useful when comparing agents that must discover, select, and sequence multiple APIs.
- Limitation: often more synthetic or API-centric than production software-agent workflows.

#### τ-bench (tau-bench)
- Evaluates agents in dynamic, policy-constrained, multi-turn tool-using scenarios.
- Strong fit for production-style assistant agents because it tests not only task completion but also policy adherence and conversational consistency.
- Useful when an agent runtime must handle state, dialogue, and action under business rules.

### 3. General agent and reasoning benchmarks

These measure broader autonomous problem solving rather than only code or isolated tool calls.

#### AgentBench
- Broad benchmark suite for multi-domain agent abilities.
- Good for obtaining a wide, comparative view of general agent competence.
- Useful as a high-level robustness benchmark, but not enough on its own for production runtime evaluation.

#### GAIA
- Focuses on complex, multi-step real-world tasks requiring reasoning, retrieval, and action.
- Useful for evaluating general-purpose assistants and orchestration quality.
- Strong signal for long-horizon competence, though practical runtime details may still require domain-specific benchmarks.

### 4. Web and computer-interaction benchmarks

These are valuable if the runtime performs browser automation, UI navigation, or environment interaction.

#### WebArena / BrowserGym
- Evaluate an agent's ability to complete tasks through web interfaces and browser interactions.
- Useful when comparing agents that operate through browser tooling, navigation loops, and page understanding.
- Less central for repository-centric agent systems unless browser work is part of the product surface.

### 5. MCP and ecosystem-scale tool benchmarks

A newer category evaluates agents against large tool ecosystems and protocol-based tool routing.

#### MCP-oriented benchmarks
Examples include LiveMCPBench and MCP-Universe.
- Useful for evaluating agent behavior across broad tool ecosystems, especially when the runtime exposes many tools under a shared protocol.
- Particularly relevant if the product roadmap includes large-scale MCP interoperability.
- Limitation: still an emerging benchmark area with less standardization than SWE-bench.

## What each benchmark type is good for

| Benchmark family | Best for | Weak for |
|---|---|---|
| SWE-bench | coding-agent quality, repo edits, test-driven task completion | non-code orchestration and chat workflows |
| Terminal-bench | shell loops, command execution behavior, debugging workflows | non-terminal workflows |
| BFCL / function-calling | tool-call correctness, schema fidelity, parameter accuracy | long-horizon end-to-end execution |
| τ-bench | policy-aware multi-turn tool use, assistant realism | repository coding depth |
| AgentBench / GAIA | general agent robustness and long-horizon reasoning | production-specific implementation details |
| WebArena / BrowserGym | browser and UI task execution | repo-centric development tasks |
| MCP benchmarks | protocol/tool-ecosystem evaluation | mature cross-system comparability |

## Working conclusion

For OpenClaw-like systems, there is no single benchmark that fully captures runtime quality. A practical evaluation stack needs at least three layers:
1. coding-task evaluation
2. tool-use and execution evaluation
3. long-horizon or policy-constrained agent evaluation

That is the minimum structure required to separate model quality from runtime quality.
