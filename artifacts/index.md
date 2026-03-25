---
title: Incubator Artifacts
---

# Incubator Artifacts

`artifacts/` is the topic-oriented area of the incubator repository.
Each subdirectory under this path should represent an independent work unit with its own durable context, documents, and follow-up history.

## Operating Rule

- For a new user-requested topic, create a new topic home under `artifacts/<topic-home>/`.
- For revisions or extensions to an existing topic, continue inside the existing topic home.
- Keep research, specifications, implementation notes, status logs, and supporting documents together within the same topic home.

## Current Topic Homes

### `a2ui-analysis/`
Research and implementation material related to A2UI analysis and renderer/test work.

### `agentic-app-platform-analysis/`
Research material focused on agentic application platform analysis.

### `dotnet-assembly-inspector/`
Topic home for the .NET assembly inspector work, including skill, status, and planning documents.

### `galaxy-s26-ai/`
Research documents related to Galaxy S26 AI and intelligent OS positioning.

### `kmp-cmp-poc/`
Kotlin Multiplatform / Compose Multiplatform proof-of-concept workspace, including planning, architecture, implementation spikes, and status tracking.

### `tizen-ai-os-prd/`
Product and architecture documentation for the Tizen AI-OS topic.

## Expectations for New Topic Homes

A new topic home does not need a rigid template, but it should usually make room for:
- a primary README or overview
- research and reference notes
- implementation or experiment outputs, when applicable
- progress, status, or decision records for longer-running topics

## Maintenance Notes

- Prefer updating an existing topic home over scattering related files across multiple directories.
- Add or revise this index when a new durable topic home is introduced.
- Keep descriptions short and factual so the index remains usable as the repository grows.
