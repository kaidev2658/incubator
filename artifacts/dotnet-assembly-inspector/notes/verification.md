# Verification Plan — Mono.Cecil Extraction

Date: 2026-03-04
Project: `dotnet-assembly-inspector`

## 1) Verification strategy for Mono.Cecil extraction

1. Build a deterministic fixture-first validation loop.
- Use a fixed set of known DLL fixtures in `input/fixtures/`.
- For each fixture, run extractor and compare generated `api-index.json` to a checked-in expected snapshot.
- Keep snapshot diffs human-reviewable (stable ordering by namespace/type/member full name).

2. Validate at three levels.
- Structural: JSON schema and required fields exist.
- Semantic: extracted metadata matches fixture source intent (names, signatures, inheritance, extension methods).
- Regression: unchanged fixtures produce byte-identical JSON/Markdown outputs.

3. Cross-check with an independent source of truth.
- For selected fixtures, compare Cecil output to Roslyn/source definitions used to compile fixture.
- Use targeted assertions (not full text compare) for signatures involving generics/nullability/ref kinds.

4. Exercise resolver behavior explicitly.
- Run fixture sets with full dependency graph and with intentionally missing dependencies.
- Verify tool degrades gracefully (partial type names, warnings) rather than failing silently.

5. Enforce deterministic formatting and ordering.
- Canonicalize type/member ordering.
- Normalize signature output rules (generic parameter notation, nested type naming, fully qualified namespaces where required).

## 2) Extraction checklist

## Namespaces
- [ ] All namespaces from exported and non-exported types captured (including global namespace handling).
- [ ] Namespace grouping is stable and deterministic.
- [ ] Nested namespace names are not truncated or merged.

## Types
- [ ] Class/struct/interface/enum/delegate kind is correct.
- [ ] Nested types represented with proper containing type link.
- [ ] Generic arity and generic parameter names captured.
- [ ] Accessibility flags captured (`public`, `internal`, etc.).
- [ ] Abstract/sealed/static records represented as applicable metadata flags.

## Signatures (constructors/properties/methods/events)
- [ ] Method return type, name, generic args, parameter list, and modifiers (`ref`, `out`, `in`, `params`) are correct.
- [ ] Constructor signatures include accessibility and parameter metadata.
- [ ] Property getter/setter presence and indexer signatures extracted.
- [ ] Event handler type and add/remove accessibility captured.
- [ ] Explicit interface implementations rendered correctly.
- [ ] Generic constraints (`where T : class, new()`, interface constraints) captured.
- [ ] Nullable annotations and array/pointer/byref forms handled consistently.

## Inheritance and interfaces
- [ ] Base type is correct (including generic closed/open types).
- [ ] Implemented interfaces include inherited generic substitutions.
- [ ] Interface list ordering deterministic.
- [ ] Cycle-safe traversal for deep or unusual metadata graphs.

## Extension methods
- [ ] Method marked as extension only when method is static and first param has `this` modifier.
- [ ] Declaring static class + namespace recorded.
- [ ] Extended target type rendered correctly for generic extension methods.
- [ ] Duplicate extension signatures across assemblies/namespaces are disambiguated.

## 3) Known edge cases and test dataset suggestions

1. Multi-target framework packages.
- Dataset: same API with netstandard2.0 + net8.0 variants.
- Verify per-TFM signature differences are surfaced, not overwritten.

2. Missing dependency references.
- Dataset: fixture compiled against an extra library, then run without that dependency.
- Verify warning path and partial resolution behavior.

3. Nested generics + constraints.
- Dataset: `Outer<T>.Inner<U>` with method constraints and interface constraints.

4. Explicit interface implementations.
- Dataset: class implementing same method name from multiple interfaces.

5. Ref-like and unsafe signatures.
- Dataset: `Span<T>`, pointers, function pointers, `ref struct`.

6. Extension methods overload matrix.
- Dataset: same extension method name across namespaces and generic/non-generic overloads.

7. Attributes affecting API surface interpretation.
- Dataset: `[Obsolete]`, `[EditorBrowsable]`, nullable context attributes.
- Verify metadata capture or intentional omission policy is explicit.

8. Compiler-generated and special-name members.
- Dataset: records, auto-properties, events.
- Verify filtering policy (include/exclude backing fields, synthesized methods) is deterministic.

9. Global namespace and nested types.
- Dataset: types with no namespace and deeply nested type declarations.

10. Large assembly performance sanity.
- Dataset: one medium-to-large real-world DLL (non-obfuscated) to verify runtime/memory bounds.

## 4) Concrete quality gates for current WORKPLAN/STATUS

1. Gate: scaffold completeness (Phase 1 entry gate)
- Required: `src/` + `tests/` projects build cleanly on `dotnet build`.
- Required: CLI accepts input folder and output path flags with `--help` docs.

2. Gate: extractor correctness baseline
- Required: at least 5 fixture assemblies covering classes/interfaces/enums/delegates/extensions.
- Required: snapshot tests pass for JSON output with deterministic ordering.
- Required: Markdown summary generated and validated for key sections.

3. Gate: signature fidelity
- Required: dedicated tests for generics, constraints, explicit interface impl, ref/out/in/params.
- Required: expected signature strings reviewed and locked via snapshots.

4. Gate: inheritance/interface integrity
- Required: tests proving base type + interface extraction for open and closed generic cases.
- Required: no duplicate or missing interface entries in output.

5. Gate: extension method accuracy
- Required: positive and negative tests (true extension vs plain static method).
- Required: output includes declaring namespace/type and target type.

6. Gate: failure behavior and diagnostics
- Required: missing dependency scenario returns non-crashing result + structured warnings.
- Required: exit code policy documented (success with warnings vs hard failure).

7. Gate: output contract stability
- Required: JSON schema file (`notes/api-index.schema.json` or equivalent) exists.
- Required: CI check validates produced JSON against schema.

8. Gate: operational readiness for next phase
- Required: sample run artifact checked into `output/` with reproducible command in README.
- Required: STATUS updated with timestamps and pass/fail against the above gates.

## Suggested immediate updates to process docs

- In `WORKPLAN.md` Phase 1, add explicit test and schema tasks.
- In `STATUS.md`, add a `Quality Gates` section with gate IDs and current state (`Not Started/In Progress/Passed/Failed`).
