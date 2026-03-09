namespace TizenMiniAppRuntimeMock.Modules;

public sealed class PolicyBridge
{
    private readonly ApiMetadataIndex _apiMetadataIndex;

    public PolicyBridge(ApiMetadataIndex apiMetadataIndex)
    {
        _apiMetadataIndex = apiMetadataIndex;
    }

    public PolicyResult Validate(MiniApp app)
    {
        return ValidateActions(app.Permissions);
    }

    public PolicyResult ValidateAction(string action)
    {
        return ValidateActions(new[] { action });
    }

    private PolicyResult ValidateActions(IEnumerable<string> actions)
    {
        var normalized = actions
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim().ToLowerInvariant())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var blocked = normalized.Where(x => !_apiMetadataIndex.IsAllowed(x)).ToArray();
        if (blocked.Length > 0)
        {
            var blockedList = string.Join(", ", blocked);
            var allowedList = _apiMetadataIndex.RenderAllowedActions();
            return new PolicyResult(
                IsAllowed: false,
                Reason: $"policy blocked: action(s) [{blockedList}] not in api-index ({_apiMetadataIndex.IndexPath}); allowed=[{allowedList}]");
        }

        return new PolicyResult(
            IsAllowed: true,
            Reason: $"policy pass: actions allowed by api-index ({_apiMetadataIndex.IndexPath})");
    }
}

public sealed record PolicyResult(bool IsAllowed, string Reason);
