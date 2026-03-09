# API Metadata Index (PoC Starter)

This folder is the local allowlist index used by the runtime policy selector.

## Allowed API surface (starter)
- `location`
- `calendar.read`
- `contacts.read`

Any runtime action that is not listed in `allowed-apis.json` is rejected with a fail-safe policy reason.
