# WORKLOG

멀티 에이전트 협업 로그.

규칙:
- 작업 시작 시 1줄 기록
- 작업 완료/중단 시 상태 업데이트
- 충돌 가능 작업은 미리 선언

---

## Template

- [YYYY-MM-DD HH:MM KST] agent:<id> branch:<name> status:<started|done|blocked>
  - task:
  - scope:
  - note:

---

## Entries

- [2026-03-01 14:13 KST] agent:coordinator branch:chore/collab-governance status:started
  - task: Add collaboration governance docs for incubator
  - scope: CONTRIBUTING.md, BRANCH_POLICY.md, WORKLOG.md
  - note: Establish baseline process for multi-agent shared repo

- [2026-03-01 14:13 KST] agent:coordinator branch:chore/collab-governance status:done
  - task: Added baseline governance docs
  - scope: CONTRIBUTING.md, BRANCH_POLICY.md, WORKLOG.md
  - note: Ready for team review and iteration

## 운영 원칙 (Coordinator / ACPX / Incubator)

- Coordinator: 요구사항 정제, 우선순위/플랜, 검증, 사용자 보고를 담당한다.
- ACPX: 실제 코드 구현/수정/테스트 실행의 1차 실행 주체다.
- Incubator agent: 공용 repo(`~/workspace/github/incubator`) 산출물 정리, 파일 반영, 커밋/푸시 파이프라인을 담당한다.

### 실행 규칙
- 구현 작업은 기본적으로 ACPX에 위임한다.
- 공용 repo 반영 작업은 Incubator 컨텍스트를 우선 사용한다.
- Coordinator가 직접 파일 수정/커밋을 수행하는 경우, 사전에 예외 사유를 사용자에게 고지한다.
- 중간보고는 10~15분 주기로 고정하며, 진행이 없어도 상태/막힘/다음 액션을 보고한다.

## 실행 모니터링 SOP (재발 방지)

- 백그라운드 작업 시작 시 `sessionId`를 즉시 기록하고 첫 보고에 포함한다.
- 시스템 완료 이벤트(`Exec completed ...`) 수신 후 1분 내 아래 3종을 반드시 확인/보고한다.
  1) `process log` 마지막 출력 요약
  2) `git status --short` 변경 파일
  3) 테스트/종료 코드 요약
- 진행 중에는 10~15분 주기로 상태를 고정 보고한다.
  - 진행 없음도 보고한다: `진행없음 / 현재막힘 / 다음액션`
- ACP/ACPX 세션에서 히스토리 공백이 발생하면, 로그 의존 대신 파일 시스템 기준으로 확정한다.
  - `git status`, `git diff --name-only`, 테스트 재실행 결과로 상태를 판단한다.
- 보고 포맷 고정:
  - `[단계] 상태`
  - `[결과] 변경파일/테스트`
  - `[다음] 즉시 액션`
