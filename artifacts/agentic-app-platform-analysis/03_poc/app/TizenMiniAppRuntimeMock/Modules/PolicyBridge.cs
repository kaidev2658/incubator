namespace TizenMiniAppRuntimeMock.Modules;

public sealed class PolicyBridge
{
    public PolicyResult Validate(MiniApp app)
    {
        var isAllowed = app.Permissions.All(p => !string.Equals(p, "microphone", StringComparison.OrdinalIgnoreCase));
        var reason = isAllowed ? "policy pass (mock)" : "policy blocked permission (mock)";

        return new PolicyResult(isAllowed, reason);
    }
}

public sealed record PolicyResult(bool IsAllowed, string Reason);
