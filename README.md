# Incubator

이 저장소는 Tizen AI-OS뿐 아니라 대표님이 지휘하시는 다양한 리서치, 설계, 실험 산출물을 한곳에 모으는 메타 허브입니다. 각 주제에 대해 조사한 레포트, PRD, 로드맵, 기술 노트, 시연 스크립트를 Markdown과 도구로 정리하고, GitHub Pages로 빠르게 공개할 수 있도록 설계했습니다.

## 구조
- `docs/`: 주제별로 디렉터리를 나눠서 정리되는 문서와 자료. GitHub Pages(`gh-pages`)로 배포됩니다.
- `scripts/`: 문서/자료 관련 빌드·전환 스크립트. 필요하면 추가 자동화를 더할 수 있습니다.
- `.github/workflows/pages.yml`: `docs/` 기반 공개 사이트를 `gh-pages` 브랜치로 배포합니다.

## 운영 플로우
1. `docs/` 아래에 주제별 디렉터리를 생성하고 Markdown/자산을 추가합니다.
2. `main` 브랜치에 커밋 → GitHub Actions가 `gh-pages`로 배포.
3. 결과 사이트는 `https://kaidev2658.github.io/incubator`에서 확인.

필요한 주제가 있다면 말씀만 주세요. 문서 템플릿, 리서치 로그, 데모 시나리오를 하나씩 만들어 채워나갈게요.
