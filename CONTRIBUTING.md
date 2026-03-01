# CONTRIBUTING (Multi-Agent)

이 저장소는 여러 에이전트가 동시에 접근하는 공용 작업 공간입니다.
목표는 **충돌 최소화 + 추적 가능성 + 재현성**입니다.

## Core Rules

1. `main`에 직접 커밋하지 않습니다. (예외: 긴급 hotfix)
2. 작업은 항상 브랜치에서 시작합니다.
3. 커밋은 작고 명확하게 유지합니다.
4. 테스트 가능한 변경은 테스트를 통과시킵니다.
5. 작업 시작/종료는 `WORKLOG.md`에 기록합니다.

## Branch Naming

- `coordinator/<topic>`
- `researcher/<topic>`
- `monitor/<topic>`
- `communicator/<topic>`
- `orchestrator/<topic>`
- 공통: `chore/<topic>`, `fix/<topic>`, `docs/<topic>`, `feat/<topic>`

## Commit Message Convention

- `feat(scope): ...`
- `fix(scope): ...`
- `docs(scope): ...`
- `chore(scope): ...`
- `refactor(scope): ...`
- `test(scope): ...`

예시:
- `feat(a2ui): add renderer normalization pipeline`
- `docs(plan): add execution roadmap for tizen renderer`
- `chore(workspace): standardize test entrypoint`

## Test Standard

A2UI 테스트는 아래 명령을 표준으로 사용합니다.

```bash
./scripts/test-a2ui.sh
```

직접 실행이 필요하면:

```bash
/usr/local/share/dotnet/dotnet test artifacts/a2ui-analysis/a2ui-analysis.slnx -v minimal
```

## Merge Policy

- 기본: squash merge
- PR/머지 전 체크:
  - 관련 테스트 통과
  - 문서 갱신(필요 시)
  - `WORKLOG.md` 업데이트

## Ownership Hint (권장)

충돌 방지를 위해 영역별 담당을 권장합니다.

- `docs/research/*` → researcher 중심
- `docs/planning/*` / 운영 조율 문서 → coordinator 중심
- `src/*`, `tests/*` → 구현 담당 에이전트 중심
