using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Transport;

public static class A2uiNormalizer
{
    public static NormalMessage Normalize(JsonObject raw)
    {
        var version = DetectVersion(raw);
        if (version is not ("v0.9" or "v0.10"))
        {
            throw new JsonException($"E_UNSUPPORTED_VERSION: {version}");
        }

        if (TryGetObject(raw, "createSurface", out var createSurface))
        {
            var surfaceId = RequireSurfaceId(createSurface, "createSurface");
            return new NormalMessage(version, NormalMessageType.CreateSurface,
                SurfaceId: surfaceId,
                Payload: createSurface.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "updateComponents", out var updateComponents))
        {
            var surfaceId = RequireSurfaceId(updateComponents, "updateComponents");
            return new NormalMessage(version, NormalMessageType.UpdateComponents,
                SurfaceId: surfaceId,
                Payload: updateComponents.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "updateDataModel", out var updateDataModel))
        {
            var surfaceId = RequireSurfaceId(updateDataModel, "updateDataModel");
            NormalizeDataModelDeleteSemantics(version, updateDataModel);
            return new NormalMessage(version, NormalMessageType.UpdateDataModel,
                SurfaceId: surfaceId,
                Payload: updateDataModel.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "deleteSurface", out var deleteSurface))
        {
            var surfaceId = RequireSurfaceId(deleteSurface, "deleteSurface");
            return new NormalMessage(version, NormalMessageType.DeleteSurface,
                SurfaceId: surfaceId,
                Payload: deleteSurface.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "callFunction", out var callFunction))
        {
            if (version != "v0.10")
            {
                throw new JsonException("E_UNSUPPORTED_MESSAGE_FOR_VERSION: callFunction requires v0.10");
            }

            var functionCallId = GetOptionalString(raw["functionCallId"]);
            if (string.IsNullOrWhiteSpace(functionCallId))
            {
                throw new JsonException("E_FUNCTION_CALL_ID_REQUIRED: functionCallId is required");
            }

            return new NormalMessage(version, NormalMessageType.CallFunction,
                SurfaceId: GetOptionalString(callFunction["surfaceId"]),
                FunctionCallId: functionCallId,
                Payload: callFunction.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "functionResponse", out var functionResponse))
        {
            if (version != "v0.10")
            {
                throw new JsonException("E_UNSUPPORTED_MESSAGE_FOR_VERSION: functionResponse requires v0.10");
            }

            var functionCallId = GetOptionalString(raw["functionCallId"]);
            if (string.IsNullOrWhiteSpace(functionCallId))
            {
                throw new JsonException("E_FUNCTION_CALL_ID_REQUIRED: functionCallId is required");
            }

            if (!functionResponse.ContainsKey("value"))
            {
                throw new JsonException("E_FUNCTION_RESPONSE_VALUE_REQUIRED: value is required");
            }

            return new NormalMessage(version, NormalMessageType.FunctionResponse,
                SurfaceId: GetOptionalString(functionResponse["surfaceId"]),
                FunctionCallId: functionCallId,
                Payload: functionResponse.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "error", out var error))
        {
            return new NormalMessage(version, NormalMessageType.Error,
                SurfaceId: GetOptionalString(raw["surfaceId"]),
                FunctionCallId: GetOptionalString(raw["functionCallId"]),
                Payload: error.DeepClone() as JsonObject);
        }

        throw new JsonException("E_UNKNOWN_MESSAGE: Unknown A2UI message shape");
    }

    private static void NormalizeDataModelDeleteSemantics(string version, JsonObject updateDataModel)
    {
        if (updateDataModel["patches"] is not JsonArray patches)
        {
            throw new JsonException("E_PATCHES_REQUIRED: patches array is required");
        }

        foreach (var node in patches)
        {
            if (node is not JsonObject patch)
            {
                throw new JsonException("E_PATCH_INVALID: patch must be an object");
            }

            if (patch["path"] is not JsonArray path || path.Count == 0)
            {
                throw new JsonException("E_PATCH_PATH_REQUIRED: patch path must be non-empty array");
            }

            if (version == "v0.9")
            {
                if (!patch.ContainsKey("value") || patch["value"] is null)
                {
                    patch["deleteOp"] = true;
                }
            }
            else if (version == "v0.10")
            {
                if (patch["value"] is null)
                {
                    patch["deleteOp"] = true;
                }
            }
        }
    }

    private static bool TryGetObject(JsonObject node, string key, out JsonObject value)
    {
        if (node[key] is JsonObject obj)
        {
            value = obj;
            return true;
        }

        value = null!;
        return false;
    }

    private static string DetectVersion(JsonObject raw)
    {
        if (raw["version"] is JsonValue versionVal && versionVal.TryGetValue<string>(out var explicitVersion))
        {
            return NormalizeVersion(explicitVersion);
        }

        // Heuristic fallback
        if (raw.ContainsKey("callFunction") || raw.ContainsKey("functionResponse"))
        {
            return "v0.10";
        }

        return "v0.9";
    }

    private static string NormalizeVersion(string raw)
    {
        var v = raw.Trim().ToLowerInvariant();
        if (v is "0.9" or "v0.9") return "v0.9";
        if (v is "0.10" or "v0.10") return "v0.10";
        return v.StartsWith('v') ? v : $"v{v}";
    }

    private static string RequireSurfaceId(JsonObject node, string messageType)
    {
        var surfaceId = GetOptionalString(node["surfaceId"]);
        if (string.IsNullOrWhiteSpace(surfaceId))
        {
            throw new JsonException($"E_SURFACE_ID_REQUIRED: surfaceId is required for {messageType}");
        }

        return surfaceId;
    }

    private static string? GetOptionalString(JsonNode? node)
    {
        if (node is null) return null;
        if (node is JsonValue value && value.TryGetValue<string>(out var str)) return str;
        return null;
    }
}
