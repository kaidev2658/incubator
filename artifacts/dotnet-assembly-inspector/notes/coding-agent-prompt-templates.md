# Coding-Agent Prompt Templates (Phase 3-3)

Updated: 2026-03-05 11:43 KST

이 문서는 `dotnet-assembly-inspector`가 생성한 산출물을 코딩 에이전트가 바로 소비할 수 있도록, 실전용 프롬프트 템플릿을 제공합니다.

## 1) Output Modes + File Patterns

아래 3가지 출력 모드를 전제로 프롬프트를 작성합니다.

1. `default` (full schema)
- 옵션: 기본값 (추가 옵션 없음)
- 핵심 파일:
  - 단일 DLL 입력: `<OUT>/api-index.json`
  - 디렉터리 DLL 입력: `<OUT>/<AssemblyName>/api-index.json`
  - nupkg 입력: `<OUT>/<TFM>/<AssemblyName>/api-index.json`

2. `compact` (`compact-v1`)
- 옵션: `--compact-json` (alias `--compact`)
- 핵심 파일 패턴은 `default`와 동일, 단 `api-index.json` 내부 키가 축약됨
- 루트 식별 키: `f == "compact-v1"`

3. `chunked` (namespace/type 분할)
- 옵션: `--chunk namespace` 또는 `--chunk type`
- base index(`api-index.json`)는 항상 함께 생성됨
- 추가 파일:
  - namespace chunk: `<...>/chunks/namespaces/<NNNN>-<sanitized-namespace>.json`
  - type chunk: `<...>/chunks/types/<NNNN>-<sanitized-type>.json`
- chunk 파일 내용 스키마는 실행 모드(default/compact)를 그대로 따름

## 2) Template — API 탐색/질의

```text
You are an API exploration coding agent.

Input artifacts:
- Primary index: {INDEX_PATH}
- Optional chunk directory: {CHUNK_DIR_OR_EMPTY}
- Output mode: {default|compact|chunked}

Tasks:
1) Parse the index and list top namespaces/types relevant to this query:
   "{USER_QUERY}"
2) Return exact type/member signatures for matching APIs.
3) If output mode is compact, first map compact keys (f/a/s/g/n/x, etc.) to semantic fields.
4) If chunked mode is provided, use chunk files to reduce scan cost:
   - namespace focus => chunks/namespaces/*
   - type focus => chunks/types/*
5) Include file references used for each answer item.

Response format:
- Short summary (3~5 lines)
- Matched APIs (namespace, type, member signature)
- Not found / ambiguity notes
- Next query suggestions (max 3)
```

## 3) Template — 확장 메서드 찾기

```text
You are an extension-method discovery agent.

Input artifacts:
- Primary index: {INDEX_PATH}
- Optional type chunks: {TYPE_CHUNK_DIR_OR_EMPTY}
- Target type full name: {TARGET_TYPE_FULL_NAME}
- Optional method hint: {METHOD_NAME_HINT}

Tasks:
1) Find extension methods where target type equals or is strongly related to {TARGET_TYPE_FULL_NAME}.
2) Report declaring namespace/type + method signature.
3) If compact mode, read extension list from compact field `x`.
4) In type-chunk mode, prioritize chunks/types/* files that contain:
   - DeclaringType == target type OR
   - TargetType == target type
5) Deduplicate overloaded signatures and keep deterministic ordering.

Response format:
- Target type
- Extension methods (declaring namespace/type, method, signature)
- Overload grouping
- Gaps (if no match)
```

## 4) Template — 타입/네임스페이스 영향도 분석

```text
You are an API impact analysis agent for refactoring and upgrades.

Input artifacts:
- Baseline index: {BASELINE_INDEX_OR_CHUNKS}
- Candidate index: {CANDIDATE_INDEX_OR_CHUNKS}
- Analysis unit: {namespace|type}
- Focus symbols: {SYMBOL_LIST}

Tasks:
1) Compare baseline vs candidate for the requested symbols.
2) Detect:
   - Added/removed namespaces/types
   - Member signature changes
   - Base type/interface changes
   - Extension method additions/removals
3) For chunked inputs:
   - namespace analysis => align files in chunks/namespaces
   - type analysis => align files in chunks/types
4) Classify impact:
   - High: removal or breaking signature change
   - Medium: behavior-affecting additive change
   - Low: non-breaking additive metadata change
5) Provide migration hints for High/Medium changes.

Response format:
- Impact summary table (High/Medium/Low counts)
- Breaking changes list
- Potentially safe changes list
- Suggested migration actions
```

## 5) Recommended Prompt Variables

- `{INDEX_PATH}`: 대상 `api-index.json`
- `{CHUNK_DIR_OR_EMPTY}`: `chunks/` 경로 또는 빈 문자열
- `{BASELINE_INDEX_OR_CHUNKS}`, `{CANDIDATE_INDEX_OR_CHUNKS}`: 비교 대상 루트
- `{SYMBOL_LIST}`: 타입/네임스페이스 FQN 목록
- `{USER_QUERY}`: 실제 질의 문자열

## 6) Practical Usage Notes

- 빠른 전체 탐색: `default` 또는 `compact`의 단일 `api-index.json` 우선 사용
- 토큰 절감: 대형 어셈블리에서는 `--compact --chunk namespace|type` 조합 권장
- 정밀 분석: 영향도/확장 메서드는 `type` chunk가 일반적으로 유리
