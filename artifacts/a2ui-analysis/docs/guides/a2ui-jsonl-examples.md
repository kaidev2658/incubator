# A2UI JSONL 예시 모음

## 예시 1) 최소 렌더링
```jsonl
{"surfaceUpdate":{"surfaceId":"main","components":[
  {"id":"root","component":{"Column":{"children":{"explicitList":["title"]}}}},
  {"id":"title","component":{"Text":{"text":{"literalString":"Hello A2UI"},"usageHint":"h1"}}}
]}}
{"beginRendering":{"surfaceId":"main","root":"root"}}
```

## 예시 2) 상태 업데이트
```jsonl
{"dataModelUpdate":{"surfaceId":"main","patches":[
  {"path":["title","text"],"value":{"literalString":"Status: Running"}}
]}}
```

## 예시 3) 복합 레이아웃
```jsonl
{"surfaceUpdate":{"surfaceId":"dashboard","components":[
  {"id":"root","component":{"Column":{"children":{"explicitList":["header","body","footer"]}}}},
  {"id":"header","component":{"Text":{"text":{"literalString":"A2UI Dashboard"},"usageHint":"h2"}}},
  {"id":"body","component":{"Text":{"text":{"literalString":"Task: Build report"},"usageHint":"body"}}},
  {"id":"footer","component":{"Text":{"text":{"literalString":"Updated: 14:30"},"usageHint":"caption"}}}
]}}
{"beginRendering":{"surfaceId":"dashboard","root":"root"}}
```

## 예시 4) surface 삭제
```jsonl
{"deleteSurface":{"surfaceId":"dashboard"}}
```

## 팁
- JSONL은 “한 줄 = 한 메시지” 규칙을 지킬 것
- 먼저 최소 예시로 동작 확인 후 복합 예시로 확장
- 버전 호환(v0.8) 범위 내 메시지 사용
