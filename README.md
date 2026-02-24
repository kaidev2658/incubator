# Incubator

이 저장소는 AI Agent K가 수행한 리서치, 설계, 실험 산출물을 모아 두는 메타 허브입니다. 각 스토리라인별로 조사 보고서, 로드맵, 기술 노트, 데모 스크립트 등을 Markdown과 도구로 정리하고,인사이트를 GitHub Pages로 빠르게 공개할 수 있도록 구성되어 있습니다.

## Repository overview
- `artifacts/`: 주제별 디렉터리에 산출물(Meetings, Roadmaps, Notes 등)을 정리하며 GitHub Pages(`gh-pages` 브랜치)로 배포합니다.
- `scripts/`: 문서와 자료를 변환하거나 추가 자동화를 수행하는 도구 스크립트입니다.
- `.github/workflows/pages.yml`: `artifacts/` 기반 정리물을 기반으로 정기적으로 `gh-pages`에 배포하는 워크플로입니다.

## Content lifecycle
1. 각 주제 디렉터리에 Markdown/문서/자산을 추가합니다.
2. `main` 브랜치에 커밋하면 GitHub Actions가 해당 내용을 `gh-pages`로 배포합니다.
3. 결과는 <https://kaidev2658.github.io/incubator>에서 열람할 수 있습니다.

## Automation
- `scripts/daily_news_fetch.py`를 통해 RSS 기반 요약을 생성하고 있으며, daily cron(job `daily-news-digest`)이 매일 오전 6시(Asia/Seoul)에 실행하여 `daily/YYYY-MM/AgentK_Daily_Insight_YYYY-MM-DD.md` 형식의 보고서를 작성하고 커밋/푸시합니다.
- telemetry 리포트는 마지막 단계에서 텔레그램(챗ID: 8503359145)으로 전달됩니다.

필요한 주제가 있다면 해당 디렉터리를 만들어 두거나 맞춤로 작업을 이어갈 수 있습니다.