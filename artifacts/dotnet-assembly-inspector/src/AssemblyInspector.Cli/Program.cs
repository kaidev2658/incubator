using System.Text.Json;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Mcp;

if (args.Length > 0 && args[0] == "--mcp-tool")
{
    return await RunMcpToolAsync(args);
}

if (args.Length == 0)
{
    PrintUsage();
    return 1;
}

var options = ParseOptions(args);
if (options is null)
{
    PrintUsage();
    return 1;
}

var app = new InspectorApp(new CecilAssemblyInspector(), new JsonReportWriter(), new MarkdownReportWriter());
await app.RunAsync(options);
return 0;

static async Task<int> RunMcpToolAsync(string[] args)
{
    string? toolName = null;
    string? requestPath = null;
    string? responsePath = null;

    for (var i = 0; i < args.Length; i++)
    {
        var arg = args[i];

        if (arg is "--help" or "-h")
        {
            PrintMcpUsage();
            return 0;
        }

        if (arg == "--mcp-tool")
        {
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Missing value for --mcp-tool");
                PrintMcpUsage();
                return 1;
            }

            toolName = args[++i];
            continue;
        }

        if (arg == "--request")
        {
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Missing value for --request");
                PrintMcpUsage();
                return 1;
            }

            requestPath = args[++i];
            continue;
        }

        if (arg == "--response")
        {
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Missing value for --response");
                PrintMcpUsage();
                return 1;
            }

            responsePath = args[++i];
            continue;
        }

        Console.Error.WriteLine($"Unexpected argument: {arg}");
        PrintMcpUsage();
        return 1;
    }

    if (!string.Equals(toolName, "inspect_assembly", StringComparison.OrdinalIgnoreCase))
    {
        Console.Error.WriteLine($"Unsupported MCP tool: {toolName ?? "(missing)"}");
        PrintMcpUsage();
        return 1;
    }

    if (string.IsNullOrWhiteSpace(requestPath))
    {
        Console.Error.WriteLine("Request file is required. Use --request <path>");
        PrintMcpUsage();
        return 1;
    }

    var requestContent = await File.ReadAllTextAsync(requestPath);
    var request = JsonSerializer.Deserialize<InspectAssemblyRequest>(
        requestContent,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (request is null)
    {
        Console.Error.WriteLine("Invalid request payload.");
        return 1;
    }

    var tool = new InspectAssemblyTool(new CecilAssemblyInspector(), new MarkdownReportWriter());
    var response = await tool.ExecuteAsync(request);
    var responseJson = JsonSerializer.Serialize(
        response,
        new JsonSerializerOptions { WriteIndented = true });

    if (string.IsNullOrWhiteSpace(responsePath))
    {
        Console.WriteLine(responseJson);
        return 0;
    }

    await File.WriteAllTextAsync(responsePath, responseJson);
    Console.WriteLine($"Wrote {responsePath}");
    return 0;
}

static InspectorOptions? ParseOptions(string[] args)
{
    string? inputPath = null;
    string outputDirectory = "output";
    string? tfm = null;
    var allTfms = false;
    var compactJson = false;
    var chunking = ChunkingStrategy.None;

    for (var i = 0; i < args.Length; i++)
    {
        var arg = args[i];

        if (arg is "--help" or "-h")
        {
            return null;
        }

        if (arg == "--all-tfms")
        {
            allTfms = true;
            continue;
        }

        if (arg is "--compact-json" or "--compact")
        {
            compactJson = true;
            continue;
        }

        if (arg == "--tfm")
        {
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Missing value for --tfm");
                return null;
            }

            tfm = args[++i];
            continue;
        }

        if (arg == "--chunk")
        {
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Missing value for --chunk");
                return null;
            }

            var value = args[++i];
            if (!TryParseChunking(value, out chunking))
            {
                Console.Error.WriteLine($"Invalid --chunk value: {value}. Expected one of: namespace, type.");
                return null;
            }

            continue;
        }

        if (inputPath is null)
        {
            inputPath = arg;
            continue;
        }

        if (outputDirectory == "output")
        {
            outputDirectory = arg;
            continue;
        }

        Console.Error.WriteLine($"Unexpected argument: {arg}");
        return null;
    }

    if (string.IsNullOrWhiteSpace(inputPath))
    {
        Console.Error.WriteLine("Input path is required.");
        return null;
    }

    if (allTfms)
    {
        tfm = null;
    }

    return new InspectorOptions(inputPath, outputDirectory, tfm, allTfms, compactJson, chunking);
}

static void PrintUsage()
{
    Console.WriteLine("Usage: assembly-inspector <input-path(.dll|.nupkg|dir)> [output-dir] [--tfm <TFM>] [--all-tfms] [--compact-json|--compact] [--chunk <namespace|type>]");
    Console.WriteLine("MCP tool: assembly-inspector --mcp-tool inspect_assembly --request <request.json> [--response <response.json>]");
}

static void PrintMcpUsage()
{
    Console.WriteLine("Usage: assembly-inspector --mcp-tool inspect_assembly --request <request.json> [--response <response.json>]");
}

static bool TryParseChunking(string value, out ChunkingStrategy chunking)
{
    if (string.Equals(value, "namespace", StringComparison.OrdinalIgnoreCase))
    {
        chunking = ChunkingStrategy.Namespace;
        return true;
    }

    if (string.Equals(value, "type", StringComparison.OrdinalIgnoreCase))
    {
        chunking = ChunkingStrategy.Type;
        return true;
    }

    chunking = ChunkingStrategy.None;
    return false;
}
