using System.Text.Json.Nodes;

namespace TizenA2uiRenderer.Model;

public sealed class DataModel
{
    private readonly JsonObject _root = [];

    public JsonNode? Get(string path)
    {
        var parts = Split(path);
        JsonNode? current = _root;
        foreach (var part in parts)
        {
            if (current is JsonObject obj)
            {
                if (!obj.TryGetPropertyValue(part, out current))
                {
                    return null;
                }
                continue;
            }

            if (current is JsonArray arr && int.TryParse(part, out var idx) && idx >= 0 && idx < arr.Count)
            {
                current = arr[idx];
                continue;
            }

            return null;
        }

        return current;
    }

    public void Set(string path, JsonNode? value)
    {
        var parts = Split(path);
        if (parts.Count == 0) return;

        JsonNode current = _root;
        for (var i = 0; i < parts.Count - 1; i++)
        {
            var part = parts[i];
            var nextPart = parts[i + 1];
            var nextShouldArray = int.TryParse(nextPart, out _);

            if (current is JsonObject obj)
            {
                if (!obj.TryGetPropertyValue(part, out var child) || child is null)
                {
                    child = nextShouldArray ? new JsonArray() : new JsonObject();
                    obj[part] = child;
                }
                current = child;
                continue;
            }

            if (current is JsonArray arr && int.TryParse(part, out var idx) && idx >= 0)
            {
                EnsureArraySize(arr, idx + 1);
                arr[idx] ??= nextShouldArray ? new JsonArray() : new JsonObject();
                current = arr[idx]!;
                continue;
            }

            return;
        }

        var last = parts[^1];
        if (current is JsonObject targetObj)
        {
            targetObj[last] = value?.DeepClone();
            return;
        }

        if (current is JsonArray targetArr && int.TryParse(last, out var targetIdx) && targetIdx >= 0)
        {
            EnsureArraySize(targetArr, targetIdx + 1);
            targetArr[targetIdx] = value?.DeepClone();
        }
    }

    public void Delete(string path)
    {
        var parts = Split(path);
        if (parts.Count == 0) return;

        JsonNode? current = _root;
        for (var i = 0; i < parts.Count - 1; i++)
        {
            var part = parts[i];
            if (current is JsonObject obj)
            {
                if (!obj.TryGetPropertyValue(part, out current)) return;
                continue;
            }

            if (current is JsonArray arr && int.TryParse(part, out var idx) && idx >= 0 && idx < arr.Count)
            {
                current = arr[idx];
                continue;
            }

            return;
        }

        var last = parts[^1];
        if (current is JsonObject targetObj)
        {
            targetObj.Remove(last);
            return;
        }

        if (current is JsonArray targetArr && int.TryParse(last, out var targetIdx) && targetIdx >= 0 && targetIdx < targetArr.Count)
        {
            targetArr[targetIdx] = null;
        }
    }

    public JsonObject Snapshot() => (JsonObject)_root.DeepClone();

    private static List<string> Split(string path)
        => path.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

    private static void EnsureArraySize(JsonArray arr, int size)
    {
        while (arr.Count < size)
        {
            arr.Add(null);
        }
    }
}
