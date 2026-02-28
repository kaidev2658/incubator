namespace TizenA2uiRenderer.Utils;

public static class ErrorCodes
{
    public const string ParseLine = "E_PARSE_LINE";
    public const string ParseOverflow = "E_PARSE_OVERFLOW";
    public const string ParseJsonTooLarge = "E_PARSE_JSON_TOO_LARGE";
    public const string ParseIncompleteJson = "E_PARSE_INCOMPLETE_JSON";
    public const string UnsupportedVersion = "E_UNSUPPORTED_VERSION";
    public const string SurfaceNotFound = "E_SURFACE_NOT_FOUND";
    public const string SurfaceDeleted = "E_SURFACE_DELETED";
    public const string SurfaceIdRequired = "E_SURFACE_ID_REQUIRED";
    public const string FunctionCallIdRequired = "E_FUNCTION_CALL_ID_REQUIRED";
    public const string FunctionNameRequired = "E_FUNCTION_NAME_REQUIRED";
    public const string FunctionCallDuplicate = "E_FUNCTION_CALL_DUPLICATE";
    public const string FunctionResponseOrphan = "E_FUNCTION_RESPONSE_ORPHAN";
    public const string FunctionSurfaceMismatch = "E_FUNCTION_SURFACE_MISMATCH";
    public const string FunctionTimeout = "E_FUNCTION_TIMEOUT";
    public const string FunctionCallFailed = "E_FUNCTION_CALL_FAILED";
    public const string RenderFailed = "E_RENDER_FAILED";
}
