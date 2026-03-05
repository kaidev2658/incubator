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