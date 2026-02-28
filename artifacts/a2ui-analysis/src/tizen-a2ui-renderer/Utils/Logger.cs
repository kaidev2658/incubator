using System.Text.Json;

namespace TizenA2uiRenderer.Utils;

public interface ILogger
{
    void Info(string message, IReadOnlyDictionary<string, object?>? fields = null);
    void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null);
}

public static class StructuredLogFields
{
    public const string Source = "source";
    public const string ErrorCode = "error_code";
    public const string ErrorMessage = "error_message";
    public const string SurfaceId = "surface_id";
    public const string FunctionCallId = "function_call_id";
    public const string Operation = "operation";
    public const string IntegrationPath = "integration_path";
    public const string AdapterType = "adapter_type";
    public const string RawLine = "raw_line";
}

public sealed class NullLogger : ILogger
{
    public void Info(string message, IReadOnlyDictionary<string, object?>? fields = null) { }
    public void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null) { }
}

public sealed class ConsoleLogger : ILogger
{
    public void Info(string message, IReadOnlyDictionary<string, object?>? fields = null)
        => Write("INFO", message, fields, null);

    public void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null)
        => Write("ERROR", message, fields, ex);

    private static void Write(string level, string message, IReadOnlyDictionary<string, object?>? fields, Exception? ex)
    {
        var payload = new Dictionary<string, object?>
        {
            ["level"] = level,
            ["message"] = message
        };

        if (fields is not null)
        {
            foreach (var kv in fields)
            {
                payload[kv.Key] = kv.Value;
            }
        }

        if (ex is not null)
        {
            payload["exception_type"] = ex.GetType().Name;
            payload["exception_message"] = ex.Message;
        }

        Console.WriteLine(JsonSerializer.Serialize(payload));
    }
}
