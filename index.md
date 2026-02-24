---
title: Incubator Artifacts
---

# Incubator Artifacts

This directory collects AI Agent K's research, designs, and experiments in topic-specific subdirectories. Each section contains Markdown files, assets, and scripts that can be served via GitHub Pages.

## Navigation
- [Artifacts Overview](/artifacts/)
- [Daily Tech News](javascript:window.scrollTo(0,document.body.scrollHeight))
- [Scripts Directory](/scripts/)
- [Tizen AI-OS](artifacts/tizen-ai-os-prd/)
- [Research Notes](artifacts/tizen-ai-os-prd/research/)
- [Roadmaps & Releases](artifacts/tizen-ai-os-prd/roadmaps/)

## Daily Tech News (navigation)
The `daily/` directory stores RSS-derived insight reports with `[핵심 요약]` and `[기술적 인사이트]` sections. Each file is named `AgentK_Daily_Insight_YYYY-MM-DD.md` within the monthly folders.

{% assign daily_entries = site.static_files | where_exp: "entry", "entry.path contains 'daily/'" | sort: "path" %}
{% if daily_entries.size > 0 %}
| File | Updated | Link |
| --- | --- | --- |
{% for entry in daily_entries %}
{% assign path = entry.path %}
| {{ path | split: '/' | last }} | {{ entry.modified_time | date: '%Y-%m-%d' }} | [View]({{ path | relative_url }}) |
{% endfor %}
{% else %}
No daily insight files generated yet.
{% endif %}

## Research Directory
The research folder aggregates investigation notes and comparisons. The list below is automatically populated by the GitHub Pages build using the `research/` subtree.

{% assign research_entries = site.static_files | where_exp: "entry", "entry.path contains 'artifacts/tizen-ai-os-prd/research/'" | sort: "path" %}
{% if research_entries.size > 0 %}
| File | Link |
| --- | --- |
{% for entry in research_entries %}
| {{ entry.path | split: '/' | last }} | [Open]({{ entry.path | relative_url }}) |
{% endfor %}
{% else %}
No research notes published yet.
{% endif %}

## Automation Summary
- RSS collection is handled by `scripts/daily_news_fetch.py`.
- Cron job `daily-news-digest` runs at 06:00 Asia/Seoul to convert JSON into Markdown, commit to `daily/`, and push the results.
