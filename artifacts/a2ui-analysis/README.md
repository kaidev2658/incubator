# a2ui-analysis

A2UI 관련 문서와 C#/.NET(Tizen) 구현 자산의 홈 디렉토리.

## 구조
- `docs/` : 분석/설계/PRD/가이드 문서
- `src/` : Tizen A2UI renderer 소스(.cs, .csproj 포함)
- `tests/` : 테스트 프로젝트 및 테스트 코드

## 메모
- 기존 `a2ui-function-structure-trends-2026-02-27/REPORT.md`는
  `docs/a2ui-function-structure-trends-report.md`로 이동됨.
- 기존 `projects/tizen-a2ui-renderer` 내용은 본 디렉토리(`artifacts/a2ui-analysis`) 하위로 통합됨.


## 실행 표준
- 솔루션: `artifacts/a2ui-analysis/a2ui-analysis.slnx`
- 루트 표준 명령: `./scripts/test-a2ui.sh`

직접 실행이 필요하면 아래도 동일하게 동작합니다.
```bash
/usr/local/share/dotnet/dotnet test artifacts/a2ui-analysis/a2ui-analysis.slnx -v minimal
```
