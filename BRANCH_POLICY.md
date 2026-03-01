# BRANCH_POLICY

## 목적
멀티 에이전트 동시 작업 시 충돌/덮어쓰기/맥락 유실을 줄이기 위한 브랜치 운영 정책.

## 기본 원칙

- `main`은 항상 배포 가능 상태를 유지.
- 기능/실험/문서 변경은 브랜치에서 수행.
- 브랜치 하나 = 명확한 작업 단위 하나.

## 브랜치 생성

```bash
git switch -c <agent-or-type>/<short-topic>
```

예시:
- `researcher/a2ui-protocol-diff`
- `coordinator/tizen-renderer-plan-v2`
- `fix/test-entrypoint`

## 동시 작업 가이드

- 같은 파일 동시 편집을 피한다.
- 큰 작업은 디렉터리 단위로 담당 분리.
- 장기 브랜치는 주기적으로 `main` 리베이스/머지로 드리프트 방지.

## 병합 방식

- 기본: squash merge
- 예외: 히스토리 보존이 필요한 경우만 merge commit

## 금지 사항

- main 직접 push (긴급 상황 제외)
- unrelated changes를 한 커밋에 혼합
- 테스트 실패 상태 병합
