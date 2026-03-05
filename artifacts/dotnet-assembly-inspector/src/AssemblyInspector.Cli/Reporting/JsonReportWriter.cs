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
        var json = JsonSerializer.Serialize(payload, SerializerOptions);
        return File.WriteAllTextAsync(outputPath, json);
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
}
