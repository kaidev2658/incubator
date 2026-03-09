namespace TizenMiniAppRuntimeMock.Modules;

public sealed class PromptModule
{
    public MiniApp Generate(string prompt)
    {
        var appId = $"miniapp-{Guid.NewGuid():N}";

        return new MiniApp(
            AppId: appId,
            Name: string.IsNullOrWhiteSpace(prompt) ? "Generated MiniApp" : "Generated MiniApp - Prompted",
            Version: "1.0.0",
            Status: "draft",
            Permissions: new[] { "location", "calendar.read", "contacts.read" },
            Layout: "4x2");
    }

    public MiniApp PartialUpdate(MiniApp draft, string partialPrompt)
    {
        var currentVersion = Version.Parse(draft.Version);
        var nextVersion = new Version(currentVersion.Major, currentVersion.Minor, currentVersion.Build + 1).ToString();

        return draft with
        {
            Version = nextVersion,
            Name = string.IsNullOrWhiteSpace(partialPrompt) ? draft.Name : $"{draft.Name} (updated)"
        };
    }
}
