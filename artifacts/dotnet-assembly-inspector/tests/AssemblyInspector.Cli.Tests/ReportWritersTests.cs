using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Domain;
using Xunit;

namespace AssemblyInspector.Cli.Tests;

public sealed class ReportWritersTests
{
    [Fact]
    public async Task JsonReportWriter_IncludesExtensionMethods()
    {
        var writer = new JsonReportWriter();
        var index = CreateIndex();
        var outputPath = Path.Combine(Path.GetTempPath(), $"api-index-{Guid.NewGuid():N}.json");

        try
        {
            await writer.WriteAsync(index, outputPath);
            var json = await File.ReadAllTextAsync(outputPath);

            using var document = JsonDocument.Parse(json);
            Assert.True(document.RootElement.TryGetProperty("ExtensionMethods", out var extensionMethods));
            Assert.Equal(1, extensionMethods.GetArrayLength());
        }
        finally
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }

    [Fact]
    public async Task JsonReportWriter_WithCompactMode_WritesCompactShape()
    {
        var writer = new JsonReportWriter();
        var index = CreateIndex();
        var outputPath = Path.Combine(Path.GetTempPath(), $"api-index-compact-{Guid.NewGuid():N}.json");

        try
        {
            await writer.WriteAsync(index, outputPath, compact: true);
            var json = await File.ReadAllTextAsync(outputPath);

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.Equal("compact-v1", root.GetProperty("f").GetString());
            Assert.Equal("Sample", root.GetProperty("a").GetString());
            Assert.True(root.TryGetProperty("n", out var namespaces));

            var firstNamespace = namespaces[0];
            Assert.Equal("Sample.Namespace", firstNamespace.GetProperty("n").GetString());

            var firstType = firstNamespace.GetProperty("t")[0];
            Assert.Equal("Sample.Namespace.SampleType", firstType.GetProperty("f").GetString());

            var firstMember = firstType.GetProperty("m")[0];
            Assert.Equal("method", firstMember.GetProperty("k").GetString());
            Assert.Equal("public void Do()", firstMember.GetProperty("s").GetString());

            Assert.True(root.TryGetProperty("x", out var extensionMethods));
            Assert.Equal("System.String", extensionMethods[0].GetProperty("r").GetString());
            Assert.False(root.TryGetProperty("AssemblyName", out _));
        }
        finally
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }

    [Fact]
    public async Task JsonReportWriter_DefaultMode_KeepsLegacyShape()
    {
        var writer = new JsonReportWriter();
        var index = CreateIndex();
        var outputPath = Path.Combine(Path.GetTempPath(), $"api-index-default-{Guid.NewGuid():N}.json");

        try
        {
            await writer.WriteAsync(index, outputPath);
            var json = await File.ReadAllTextAsync(outputPath);

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.True(root.TryGetProperty("AssemblyName", out _));
            Assert.True(root.TryGetProperty("Namespaces", out _));
            Assert.False(root.TryGetProperty("f", out _));
        }
        finally
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }

    [Fact]
    public async Task MarkdownReportWriter_WritesExtensionMethodsSection()
    {
        var writer = new MarkdownReportWriter();
        var index = CreateIndex();
        var outputPath = Path.Combine(Path.GetTempPath(), $"api-summary-{Guid.NewGuid():N}.md");

        try
        {
            await writer.WriteAsync(index, outputPath);
            var markdown = await File.ReadAllTextAsync(outputPath);

            Assert.Contains("## Extension Methods", markdown);
            Assert.Contains("WithBang", markdown);
            Assert.Contains("Target `System.String`", markdown);
        }
        finally
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }

    private static ApiIndex CreateIndex()
    {
        return new ApiIndex(
            "Sample",
            "/tmp/sample.dll",
            DateTimeOffset.UtcNow,
            [
                new NamespaceIndex(
                    "Sample.Namespace",
                    [
                        new TypeIndex(
                            "SampleType",
                            "Sample.Namespace.SampleType",
                            "class",
                            null,
                            [],
                            [new MemberSignature("method", "Do", "public void Do()")])
                    ])
            ],
            [
                new ExtensionMethodIndex(
                    "Sample.Extensions",
                    "Sample.Extensions.StringExtensions",
                    "System.String",
                    "WithBang",
                    "public static string WithBang(string source)")
            ]);
    }
}
