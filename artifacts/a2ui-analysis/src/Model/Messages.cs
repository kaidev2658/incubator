namespace TizenA2uiRenderer.Model;

public enum NormalMessageType
{
    CreateSurface,
    UpdateComponents,
    UpdateDataModel,
    DeleteSurface,
    CallFunction,
    FunctionResponse,
    Error
}

public sealed record NormalMessage(
    string Version,
    NormalMessageType Type,
    string? SurfaceId = null,
    string? FunctionCallId = null,
    IReadOnlyDictionary<string, object?>? Payload = null);

public sealed record SurfaceDefinition(string SurfaceId, string RootId, IReadOnlyDictionary<string, object?> Components);

public sealed record SurfaceUpdate(string SurfaceId, SurfaceDefinition Definition, DataModel DataModel);

public sealed record A2uiError(string Code, string Message, string? SurfaceId = null, string? FunctionCallId = null);
