# Model-Controlled Comparison Note

Runtime benchmarking should compare agent systems under the same model whenever the objective is to measure runtime quality rather than model quality.

Without model control, differences in success rate, tool behavior, and output quality become difficult to attribute. A stronger result may come from a better model, a better runtime, or a combination of both. That ambiguity weakens the benchmark.

Keeping the model fixed makes runtime differences easier to interpret. It provides a cleaner basis for evaluating routing, retries, verification, tool sequencing, recovery behavior, and final reporting quality.

For this reason, model-controlled comparison should be treated as the default baseline for runtime evaluation. Model-varying experiments can be added later as a separate layer.
