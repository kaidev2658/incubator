using System.Text;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class MarkdownReportWriter
{
    public Task WriteAsync(ApiIndex index, string outputPath)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"# API Summary: {index.AssemblyName}");
        builder.AppendLine();
        builder.AppendLine($"Source: `{index.SourcePath}`");
        builder.AppendLine($"Generated (UTC): {index.GeneratedAtUtc:O}");
        builder.AppendLine();

        foreach (var ns in index.Namespaces)
        {
            builder.AppendLine($"## Namespace `{ns.Name}`");
            builder.AppendLine();

            foreach (var type in ns.Types)
            {
                builder.AppendLine($"### {type.Kind} `{type.Name}`");
                builder.AppendLine();

                if (!string.IsNullOrWhiteSpace(type.BaseType))
                {
                    builder.AppendLine($"- Base: `{type.BaseType}`");
                }

                if (type.Interfaces.Count > 0)
                {
                    builder.AppendLine($"- Interfaces: {string.Join(", ", type.Interfaces.Select(i => $"`{i}`"))}");
                }

                builder.AppendLine("- Members:");

                foreach (var member in type.Members)
                {
                    builder.AppendLine($"  - `{member.Signature}`");
                }

                builder.AppendLine();
            }
        }

        return File.WriteAllTextAsync(outputPath, builder.ToString());
    }
}
