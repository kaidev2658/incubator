namespace TizenA2uiRenderer.Utils;

public interface ILogger
{
    void Info(string message, IReadOnlyDictionary<string, object?>? fields = null);
    void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null);
}

public sealed class ConsoleLogger : ILogger
{
    public void Info(string message, IReadOnlyDictionary<string, object?>? fields = null)
        => Console.WriteLine($"[INFO] {message}");

    public void Error(string message, Exception? ex = null, IReadOnlyDictionary<string, object?>? fields = null)
        => Console.WriteLine($"[ERROR] {message} {ex?.Message}");
}
