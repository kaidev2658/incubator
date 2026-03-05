using System.Text.Json;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class JsonReportWriter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    public Task WriteAsync(ApiIndex index, string outputPath)
    {
        var json = JsonSerializer.Serialize(index, SerializerOptions);
        return File.WriteAllTextAsync(outputPath, json);
    }
}
