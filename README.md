# Incubator

이 저장소는 AI Agent K가 생성한 다양한 리서치, 설계, 실험 산출물을 한곳에 모으는 메타 허브입니다. 각 주제에 대해 조사한 레포트, 로드맵, 기술 노트, 시연 스크립트 등 산출물을 Markdown과 도구로 정리하고, GitHub Pages로 빠르게 공개할 수 있도록 설계했습니다.

## 구조
- `artifacts/`: 주제별 디렉터리를 나눠서 정리되는 문서와 자료. GitHub Pages(`gh-pages`)로 배포됩니다.
- `scripts/`: 문서/자료 관련 빌드·전환 스크립트. 필요하면 추가 자동화를 더할 수 있습니다.
- `.github/workflows/pages.yml`: `artifacts/` 기반 공개 사이트를 `gh-pages` 브랜치로 배포합니다.

## 운영 플로우
또한 매일 오전 6시(UTC+9) `dailiy/` 아래에 자동으로 RSS 인사이트 파일을 생성하는 스케줄링을 설정해두었습니다. 해당 작업은 GitHub Actions와 RSS 파싱 스크립트로 수행되며, 생성된 파일은 커밋/푸시 후 텔레그램 리포트(수동 전송)로 공유하시면 됩니다.

1. `artifacts/` 아래에 주제별 디렉터리를 생성하고 Markdown/자산을 추가합니다.
2. `main` 브랜치에 커밋 → GitHub Actions가 `gh-pages`로 배포.
3. 결과 사이트는 [https://kaidev2658.github.io/incubator](https://kaidev2658.github.io/incubator)에서 확인합니다.

필요한 주제가 있으면 말씀만 주세요. 새로운 디렉터리가 필요하면 제가 바로 만들어드릴게요.
