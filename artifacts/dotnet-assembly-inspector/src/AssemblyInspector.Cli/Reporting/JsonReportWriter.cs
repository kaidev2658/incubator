using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class JsonReportWriter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    public Task WriteAsync(ApiIndex index, string outputPath, bool compact = false)
    {
        object payload = compact ? BuildCompactPayload(index) : index;
        return WritePayloadAsync(payload, outputPath);
    }

    public async Task WriteChunksAsync(ApiIndex index, string outputDirectory, ChunkingStrategy strategy, bool compact = false)
    {
        if (strategy == ChunkingStrategy.None)
        {
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        switch (strategy)
        {
            case ChunkingStrategy.Namespace:
                await WriteNamespaceChunksAsync(index, outputDirectory, compact);
                return;
            case ChunkingStrategy.Type:
                await WriteTypeChunksAsync(index, outputDirectory, compact);
                return;
            default:
                throw new NotSupportedException($"Unsupported chunking strategy: {strategy}");
        }
    }

    private static CompactApiIndex BuildCompactPayload(ApiIndex index)
    {
        return new CompactApiIndex(
            Format: "compact-v1",
            Assembly: index.AssemblyName,
            Source: index.SourcePath,
            GeneratedAtUtc: index.GeneratedAtUtc,
            Namespaces: index.Namespaces
                .Select(@namespace => new CompactNamespaceIndex(
                    Name: @namespace.Name,
                    Types: @namespace.Types.Select(type => new CompactTypeIndex(
                        Name: type.Name,
                        FullName: type.FullName,
                        Kind: type.Kind,
                        BaseType: type.BaseType,
                        Interfaces: type.Interfaces,
                        Members: type.Members
                            .Select(member => new CompactMemberSignature(member.Kind, member.Name, member.Signature))
                            .ToArray()))
                        .ToArray()))
                .ToArray(),
            ExtensionMethods: index.ExtensionMethods
                .Select(method => new CompactExtensionMethod(
                    method.DeclaringNamespace,
                    method.DeclaringType,
                    method.TargetType,
                    method.MethodName,
                    method.Signature))
                .ToArray());
    }

    private sealed record CompactApiIndex(
        [property: JsonPropertyName("f")] string Format,
        [property: JsonPropertyName("a")] string Assembly,
        [property: JsonPropertyName("s")] string Source,
        [property: JsonPropertyName("g")] DateTimeOffset GeneratedAtUtc,
        [property: JsonPropertyName("n")] IReadOnlyList<CompactNamespaceIndex> Namespaces,
        [property: JsonPropertyName("x")] IReadOnlyList<CompactExtensionMethod> ExtensionMethods);

    private sealed record CompactNamespaceIndex(
        [property: JsonPropertyName("n")] string Name,
        [property: JsonPropertyName("t")] IReadOnlyList<CompactTypeIndex> Types);

    private sealed record CompactTypeIndex(
        [property: JsonPropertyName("n")] string Name,
        [property: JsonPropertyName("f")] string FullName,
        [property: JsonPropertyName("k")] string Kind,
        [property: JsonPropertyName("b")] string? BaseType,
        [property: JsonPropertyName("i")] IReadOnlyList<string> Interfaces,
        [property: JsonPropertyName("m")] IReadOnlyList<CompactMemberSignature> Members);

    private sealed record CompactMemberSignature(
        [property: JsonPropertyName("k")] string Kind,
        [property: JsonPropertyName("n")] string Name,
        [property: JsonPropertyName("s")] string Signature);

    private sealed record CompactExtensionMethod(
        [property: JsonPropertyName("d")] string DeclaringNamespace,
        [property: JsonPropertyName("t")] string DeclaringType,
        [property: JsonPropertyName("r")] string TargetType,
        [property: JsonPropertyName("n")] string MethodName,
        [property: JsonPropertyName("s")] string Signature);

    private static Task WritePayloadAsync(object payload, string outputPath)
    {
        var json = JsonSerializer.Serialize(payload, SerializerOptions);
        return File.WriteAllTextAsync(outputPath, json);
    }

    private async Task WriteNamespaceChunksAsync(ApiIndex index, string outputDirectory, bool compact)
    {
        var namespacesDirectory = Path.Combine(outputDirectory, "namespaces");
        Directory.CreateDirectory(namespacesDirectory);

        var orderedNamespaces = index.Namespaces
            .OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        for (var i = 0; i < orderedNamespaces.Length; i++)
        {
            var @namespace = orderedNamespaces[i];
            var chunk = new ApiIndex(
                index.AssemblyName,
                index.SourcePath,
                index.GeneratedAtUtc,
                [@namespace],
                index.ExtensionMethods
                    .Where(method => string.Equals(method.DeclaringNamespace, @namespace.Name, StringComparison.Ordinal))
                    .ToArray());

            var fileName = $"{i + 1:D4}-{SanitizeFileSegment(@namespace.Name)}.json";
            var outputPath = Path.Combine(namespacesDirectory, fileName);
            object payload = compact ? BuildCompactPayload(chunk) : chunk;
            await WritePayloadAsync(payload, outputPath);
        }
    }

    private async Task WriteTypeChunksAsync(ApiIndex index, string outputDirectory, bool compact)
    {
        var typesDirectory = Path.Combine(outputDirectory, "types");
        Directory.CreateDirectory(typesDirectory);

        var orderedTypes = index.Namespaces
            .SelectMany(@namespace => @namespace.Types.Select(type => (@namespace.Name, Type: type)))
            .OrderBy(item => item.Type.FullName, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        for (var i = 0; i < orderedTypes.Length; i++)
        {
            var item = orderedTypes[i];
            var chunkNamespace = new NamespaceIndex(item.Name, [item.Type]);
            var chunk = new ApiIndex(
                index.AssemblyName,
                index.SourcePath,
                index.GeneratedAtUtc,
                [chunkNamespace],
                index.ExtensionMethods
                    .Where(method =>
                        string.Equals(method.DeclaringType, item.Type.FullName, StringComparison.Ordinal)
                        || string.Equals(method.TargetType, item.Type.FullName, StringComparison.Ordinal))
                    .ToArray());

            var fileName = $"{i + 1:D4}-{SanitizeFileSegment(item.Type.FullName)}.json";
            var outputPath = Path.Combine(typesDirectory, fileName);
            object payload = compact ? BuildCompactPayload(chunk) : chunk;
            await WritePayloadAsync(payload, outputPath);
        }
    }

    private static string SanitizeFileSegment(string value)
    {
        Span<char> buffer = stackalloc char[value.Length];
        var count = 0;
        var previousWasDash = false;

        foreach (var ch in value.ToLowerInvariant())
        {
            var isAllowed = (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9');
            if (isAllowed)
            {
                buffer[count++] = ch;
                previousWasDash = false;
                continue;
            }

            if (previousWasDash)
            {
                continue;
            }

            buffer[count++] = '-';
            previousWasDash = true;
        }

        var sanitized = new string(buffer[..count]).Trim('-');
        return string.IsNullOrWhiteSpace(sanitized) ? "chunk" : sanitized;
    }
}
