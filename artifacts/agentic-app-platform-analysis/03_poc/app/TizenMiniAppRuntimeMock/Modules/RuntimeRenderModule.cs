using System.Text.Json;

namespace TizenMiniAppRuntimeMock.Modules;

public sealed class RuntimeRenderModule
{
    public void Header()
    {
        Console.WriteLine("[SCN-01] Agentic Mini-App Platform on Tizen (Mock Runtime)");
        Console.WriteLine("Commands: generate <prompt> | update <prompt> | deploy | rollback | validate | kpi | show | exit");
    }

    public void Print(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowState(RuntimeStore store)
    {
        Console.WriteLine(JsonSerializer.Serialize(store, new JsonSerializerOptions { WriteIndented = true }));
    }
}
