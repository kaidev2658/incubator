using System.Text;
using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.App;

public sealed class MarkdownReportWriter
{
    public Task WriteAsync(ApiIndex index, string outputPath)
    {
        return File.WriteAllTextAsync(outputPath, Render(index));
    }

    public string Render(ApiIndex index)
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

        builder.AppendLine("## Extension Methods");
        builder.AppendLine();

        if (index.ExtensionMethods.Count == 0)
        {
            builder.AppendLine("- _(none)_");
            builder.AppendLine();
        }
        else
        {
            foreach (var extensionMethodGroup in index.ExtensionMethods.GroupBy(method => method.TargetType))
            {
                builder.AppendLine($"### Target `{extensionMethodGroup.Key}`");
                builder.AppendLine();

                foreach (var extensionMethod in extensionMethodGroup)
                {
                    builder.AppendLine($"- `{extensionMethod.Signature}`");
                    builder.AppendLine($"  - Declared in: `{extensionMethod.DeclaringType}` (`{extensionMethod.DeclaringNamespace}`)");
                }

                builder.AppendLine();
            }
        }

        return builder.ToString();
    }
}
