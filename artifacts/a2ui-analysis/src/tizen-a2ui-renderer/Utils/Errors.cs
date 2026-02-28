namespace TizenA2uiRenderer.Utils;

public static class ErrorCodes
{
    // Parser
    public const string ParseLine = "E_PARSE_LINE";
    public const string ParseOverflow = "E_PARSE_OVERFLOW";
    public const string ParseJsonTooLarge = "E_PARSE_JSON_TOO_LARGE";
    public const string ParseIncompleteJson = "E_PARSE_INCOMPLETE_JSON";

    // Normalizer
    public const string UnsupportedVersion = "E_UNSUPPORTED_VERSION";
    public const string UnsupportedMessageForVersion = "E_UNSUPPORTED_MESSAGE_FOR_VERSION";
    public const string UnknownMessage = "E_UNKNOWN_MESSAGE";
    public const string PatchInvalid = "E_PATCH_INVALID";
    public const string PatchPathRequired = "E_PATCH_PATH_REQUIRED";
    public const string PatchesRequired = "E_PATCHES_REQUIRED";
    public const string ComponentsRequired = "E_COMPONENTS_REQUIRED";
    public const string SurfaceIdRequired = "E_SURFACE_ID_REQUIRED";
    public const string FunctionCallIdRequired = "E_FUNCTION_CALL_ID_REQUIRED";
    public const string FunctionResponseValueRequired = "E_FUNCTION_RESPONSE_VALUE_REQUIRED";

    // Controller surface/pending
    public const string Controller = "E_CONTROLLER";
    public const string SurfaceNotFound = "E_SURFACE_NOT_FOUND";
    public const string SurfaceDeleted = "E_SURFACE_DELETED";
    public const string SurfaceAlreadyExists = "E_SURFACE_ALREADY_EXISTS";
    public const string SurfaceAlreadyDeleted = "E_SURFACE_ALREADY_DELETED";
    public const string PendingOverflow = "E_PENDING_OVERFLOW";
    public const string PendingExpired = "E_PENDING_EXPIRED";

    // Controller function runtime
    public const string FunctionNameRequired = "E_FUNCTION_NAME_REQUIRED";
    public const string FunctionCallDuplicate = "E_FUNCTION_CALL_DUPLICATE";
    public const string FunctionRetryTargetMissing = "E_FUNCTION_RETRY_TARGET_MISSING";
    public const string FunctionRetryInvalidState = "E_FUNCTION_RETRY_INVALID_STATE";
    public const string FunctionCancelTargetMissing = "E_FUNCTION_CANCEL_TARGET_MISSING";
    public const string FunctionCancelInvalidState = "E_FUNCTION_CANCEL_INVALID_STATE";
    public const string FunctionCancelled = "E_FUNCTION_CANCELLED";
    public const string FunctionResponseOrphan = "E_FUNCTION_RESPONSE_ORPHAN";
    public const string FunctionResponseLate = "E_FUNCTION_RESPONSE_LATE";
    public const string FunctionSurfaceMismatch = "E_FUNCTION_SURFACE_MISMATCH";
    public const string FunctionTimeout = "E_FUNCTION_TIMEOUT";
    public const string FunctionStateTransitionInvalid = "E_FUNCTION_STATE_TRANSITION_INVALID";

    // Render
    public const string FunctionCallFailed = "E_FUNCTION_CALL_FAILED";
    public const string RenderFailed = "E_RENDER_FAILED";

    public static bool TryExtractCodePrefix(string text, out string code, out string detail)
    {
        code = ParseLine;
        detail = text;

        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        var idx = text.IndexOf(':');
        if (idx <= 0)
        {
            return false;
        }

        var prefix = text[..idx].Trim();
        if (!prefix.StartsWith("E_", StringComparison.Ordinal))
        {
            return false;
        }

        code = prefix;
        detail = text[(idx + 1)..].Trim();
        if (string.IsNullOrWhiteSpace(detail))
        {
            detail = text;
        }

        return true;
    }
}
