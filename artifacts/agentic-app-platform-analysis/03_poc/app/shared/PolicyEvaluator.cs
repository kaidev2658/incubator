namespace TizenMiniApp.Shared.Scn01;

public sealed class PolicyEvaluator
{
    private readonly HashSet<string> _allowedActions;

    public PolicyEvaluator(string indexPath, IEnumerable<string> allowedActions)
    {
        IndexPath = indexPath;
        _allowedActions = new HashSet<string>(
            allowedActions.Select(Normalize).Where(x => !string.IsNullOrWhiteSpace(x)),
            StringComparer.OrdinalIgnoreCase);
    }

    public string IndexPath { get; }

    public IReadOnlyCollection<string> AllowedActions => _allowedActions.OrderBy(x => x).ToArray();

    public bool IsAllowed(string action)
    {
        return _allowedActions.Contains(Normalize(action));
    }

    public string RenderAllowedActions()
    {
        return _allowedActions.Count == 0
            ? "(none)"
            : string.Join(", ", _allowedActions.OrderBy(x => x));
    }

    public PolicyResult ValidatePermissions(IEnumerable<string> actions)
    {
        var normalized = actions
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(Normalize)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var blocked = normalized.Where(x => !_allowedActions.Contains(x)).ToArray();
        if (blocked.Length > 0)
        {
            var blockedList = string.Join(", ", blocked);
            return new PolicyResult(
                IsAllowed: false,
                Reason: $"policy blocked: action(s) [{blockedList}] not in api-index ({IndexPath}); allowed=[{RenderAllowedActions()}]");
        }

        return new PolicyResult(
            IsAllowed: true,
            Reason: $"policy pass: actions allowed by api-index ({IndexPath})");
    }

    public PolicyResult ValidateAction(string action)
    {
        return ValidatePermissions(new[] { action });
    }

    private static string Normalize(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
    }
}
