# Why Model-Controlled Comparisons Matter

When benchmarking agent runtimes, model-controlled comparison is necessary because the goal is to isolate runtime behavior rather than raw model capability.

If two systems use different models, differences in task completion, tool use, recovery quality, or artifact quality may reflect model quality as much as runtime design. That makes it difficult to determine whether observed gains come from orchestration, memory policy, retry logic, tool execution discipline, or simply from a stronger underlying model.

Holding the model constant reduces this ambiguity. It allows evaluators to attribute differences more confidently to runtime characteristics such as:
- task decomposition quality
- specialist routing quality
- tool-call accuracy and sequencing
- verification discipline
- recovery behavior under failure
- reporting and synthesis quality

Model-controlled comparison is therefore the minimum requirement for a fair runtime benchmark. After that baseline is established, additional experiments can vary the model to study interaction effects between model quality and runtime design.
