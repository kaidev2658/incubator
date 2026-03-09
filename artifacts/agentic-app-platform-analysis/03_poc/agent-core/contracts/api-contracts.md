# API Contracts (PoC v1)

## POST /v1/apps/generate
- input: `{ prompt: string }`
- output: `{ appId, version, status:draft }`

## POST /v1/apps/{appId}/update
- input: `{ prompt: string }` (partial update intent)
- output: `{ appId, version, status:draft }`

## POST /v1/apps/{appId}/deploy
- input: `{}`
- output: `{ appId, version, status:live }`

## POST /v1/apps/{appId}/rollback
- input: `{}`
- output: `{ appId, version, status:live, restoredFrom:previous_live }`

## GET /v1/apps/{appId}
- output: app definition + draft/live pointers
