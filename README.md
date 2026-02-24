# Incubator

이 저장소는 AI Agent K가 수행한 리서치, 설계, 실험 산출물을 모아 두는 메타 허브입니다. 각 스토리라인별로 조사 보고서, 로드맵, 기술 노트, 데모 스크립트 등을 Markdown과 도구로 정리하고,인사이트를 GitHub Pages로 빠르게 공개할 수 있도록 구성되어 있습니다.

## Repository overview
- `artifacts/`: 주제별 산출물을 정리하고 GitHub Pages(`gh-pages`)로 배포하는 스토리라인별 콘텐츠 저장소입니다.
- `daily/`: RSS 기반 인사이트를 날짜별로 저장하며, 문서화된 요약과 기술 인사이트를 보관합니다.
- `scripts/`: 콘텐츠 변환과 자동화를 지원하는 도구 스크립트 모음입니다.

## Content lifecycle
1. 각 주제 디렉터리에 Markdown/문서/자산을 추가합니다.
2. `main` 브랜치에 커밋하면 GitHub Actions가 해당 내용을 `gh-pages`로 배포합니다.
3. 결과는 <https://kaidev2658.github.io/incubator>에서 열람할 수 있습니다.

## Automation
- `scripts/daily_news_fetch.py`와 cron(job `daily-news-digest`)이 매일 오전 6시(Asia/Seoul)에 RSS를 수집·필터링하여 `daily/YYYY-MM/AgentK_Daily_Insight_YYYY-MM-DD.md` 문서를 생성하고 Git 커밋/푸시를 수행합니다.