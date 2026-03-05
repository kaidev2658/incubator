using AssemblyInspector.Cli.App;

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

static InspectorOptions? ParseOptions(string[] args)
{
    string? inputPath = null;
    string outputDirectory = "output";
    string? tfm = null;
    var allTfms = false;
    var compactJson = false;

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

    return new InspectorOptions(inputPath, outputDirectory, tfm, allTfms, compactJson);
}

static void PrintUsage()
{
    Console.WriteLine("Usage: assembly-inspector <input-path(.dll|.nupkg|dir)> [output-dir] [--tfm <TFM>] [--all-tfms] [--compact-json|--compact]");
}
