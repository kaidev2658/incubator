using System.Text.Json;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class RuntimeRenderModule
{
    public void Header()
    {
        Console.WriteLine("[SCN-01] Agentic Mini-App Platform on Tizen (Mock Runtime)");
        Console.WriteLine("Commands: generate <prompt> | update <prompt> | deploy | rollback | validate | ui-demo | kpi | show | exit");
    }

    public void Print(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowUiStage(UiView view, int stageIndex, int stageTotal, string message)
    {
        Console.WriteLine($"[UI {stageIndex}/{stageTotal}] {view}: {message}");
    }

    public void ShowValidationPanelLine(string message)
    {
        Console.WriteLine($"[ValidationPanel] {message}");
    }

    public void ShowState(RuntimeStore store)
    {
        Console.WriteLine(JsonSerializer.Serialize(store, new JsonSerializerOptions { WriteIndented = true }));
    }
}
