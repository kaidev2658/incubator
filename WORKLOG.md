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
