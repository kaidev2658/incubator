using AssemblyInspector.Cli.Domain;

namespace AssemblyInspector.Cli.Mcp;

public sealed record InspectAssemblyResponse(
    ApiIndex ApiIndex,
    string ApiSummaryMarkdown);
