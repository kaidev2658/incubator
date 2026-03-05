namespace AssemblyInspector.Cli.App;

public enum ChunkingStrategy
{
    None,
    Namespace,
    Type
}

public sealed record InspectorOptions(
    string InputPath,
    string OutputDirectory,
    string? Tfm,
    bool AllTfms,
    bool CompactJson,
    ChunkingStrategy Chunking = ChunkingStrategy.None);
