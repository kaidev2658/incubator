using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Transport;

public static class A2uiNormalizer
{
    public static NormalMessage Normalize(JsonObject raw)
    {
        var version = DetectVersion(raw);

        if (TryGetObject(raw, "createSurface", out var createSurface))
        {
            return new NormalMessage(version, NormalMessageType.CreateSurface,
                SurfaceId: createSurface["surfaceId"]?.GetValue<string>(),
                Payload: createSurface.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "updateComponents", out var updateComponents))
        {
            return new NormalMessage(version, NormalMessageType.UpdateComponents,
                SurfaceId: updateComponents["surfaceId"]?.GetValue<string>(),
                Payload: updateComponents.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "updateDataModel", out var updateDataModel))
        {
            NormalizeDataModelDeleteSemantics(version, updateDataModel);
            return new NormalMessage(version, NormalMessageType.UpdateDataModel,
                SurfaceId: updateDataModel["surfaceId"]?.GetValue<string>(),
                Payload: updateDataModel.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "deleteSurface", out var deleteSurface))
        {
            return new NormalMessage(version, NormalMessageType.DeleteSurface,
                SurfaceId: deleteSurface["surfaceId"]?.GetValue<string>(),
                Payload: deleteSurface.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "callFunction", out var callFunction))
        {
            return new NormalMessage(version, NormalMessageType.CallFunction,
                SurfaceId: callFunction["surfaceId"]?.GetValue<string>(),
                FunctionCallId: raw["functionCallId"]?.GetValue<string>(),
                Payload: callFunction.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "functionResponse", out var functionResponse))
        {
            return new NormalMessage(version, NormalMessageType.FunctionResponse,
                SurfaceId: functionResponse["surfaceId"]?.GetValue<string>(),
                FunctionCallId: raw["functionCallId"]?.GetValue<string>(),
                Payload: functionResponse.DeepClone() as JsonObject);
        }

        if (TryGetObject(raw, "error", out var error))
        {
            return new NormalMessage(version, NormalMessageType.Error,
                SurfaceId: raw["surfaceId"]?.GetValue<string>(),
                FunctionCallId: raw["functionCallId"]?.GetValue<string>(),
                Payload: error.DeepClone() as JsonObject);
        }

        throw new JsonException("Unknown A2UI message shape.");
    }

    private static void NormalizeDataModelDeleteSemantics(string version, JsonObject updateDataModel)
    {
        if (updateDataModel["patches"] is not JsonArray patches) return;

        foreach (var node in patches)
        {
            if (node is not JsonObject patch) continue;

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
}
