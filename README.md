# Incubator

이 리포지토리는 Tizen AI-OS, 에이전트 플랫폼, 리서치 아티팩트, 구현 계획을 찰나의 순간마다 저장하고 GitHub Pages로 외부 공유하는 통합 허브입니다. 매번 변화하는 조사 결과/기술 문서를 `docs/` 아래에 Markdown으로 정리하며, 필요할 때마다 새로운 버전 태그와 GitHub Pages 페이지를 통해 쉽게 보여줄 수 있도록 설계합니다.

## 구조
- `docs/`: GitHub Pages로 배포되는 모든 Markdown 문서. 누적되는 리서치, 로드맵, PRD, 참고 자료가 모두 여기에 정리됩니다.
- `.github/workflows/pages.yml`: `docs/`를 `gh-pages` 브랜치로 배포하는 GitHub Actions 워크플로.
- `scripts/`: 배포/정리 스크립트와 도구를 모아 자동화를 돕습니다.
- `assets/`: 문서에서 참조하는 다이어그램, 스케치, 로고, 스크린샷.

## 운영 방식
1. `main` 브랜치에 문서를 추가/수정 → 커밋.
2. GitHub Actions가 `docs/`를 `gh-pages` 브랜치로 배포.
3. `https://kaidev2658.github.io/incubator`에서 공개 문서 확인.
4. 주요 산출물은 `docs/INDEX.md`와 `docs/research/`, `docs/roadmaps/` 하위 디렉터리에서 관리.

필요하시면 추가적인 템플릿이나 버전별 릴리즈 노트도 제가 대신 정리해드릴 수 있습니다.
