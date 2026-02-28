using Xunit;
using System.Text.Json;
using System.Text.Json.Nodes;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Transport;

namespace TizenA2uiRenderer.Tests;

public class NormalizerTests
{
    [Fact]
    public void Normalizer_V010_NullValue_Becomes_DeleteOp()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v0.10",
          "updateDataModel":{
            "surfaceId":"main",
            "patches":[{"path":["x"],"value":null}]
          }
        }
        """)!.AsObject();

        var msg = A2uiNormalizer.Normalize(raw);

        Assert.Equal(NormalMessageType.UpdateDataModel, msg.Type);
        var patches = msg.Payload!["patches"]!.AsArray();
        Assert.True(patches[0]!["deleteOp"]!.GetValue<bool>());
    }

    [Fact]
    public void Normalizer_V09_MissingValue_Becomes_DeleteOp()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v0.9",
          "updateDataModel":{
            "surfaceId":"main",
            "patches":[{"path":["x"]}]
          }
        }
        """)!.AsObject();

        var msg = A2uiNormalizer.Normalize(raw);
        var patches = msg.Payload!["patches"]!.AsArray();

        Assert.True(patches[0]!["deleteOp"]!.GetValue<bool>());
    }

    [Fact]
    public void Normalizer_Throws_When_SurfaceId_Missing_On_Create()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v0.10",
          "createSurface":{
            "root":"root",
            "components":{"root":{"component":"Column"}}
          }
        }
        """)!.AsObject();

        var ex = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(raw));
        Assert.Contains("E_SURFACE_ID_REQUIRED", ex.Message);
    }

    [Fact]
    public void Normalizer_Throws_For_Unsupported_Version()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v1.0",
          "createSurface":{
            "surfaceId":"main",
            "root":"root",
            "components":{"root":{"component":"Column"}}
          }
        }
        """)!.AsObject();

        var ex = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(raw));
        Assert.Contains("E_UNSUPPORTED_VERSION", ex.Message);
    }

    [Fact]
    public void Normalizer_CallFunction_Requires_FunctionCallId()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v0.10",
          "callFunction":{"surfaceId":"main","name":"confirm"}
        }
        """)!.AsObject();

        var ex = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(raw));
        Assert.Contains("E_FUNCTION_CALL_ID_REQUIRED", ex.Message);
    }

    [Fact]
    public void Normalizer_FunctionResponse_Requires_Value()
    {
        var raw = JsonNode.Parse("""
        {
          "version":"v0.10",
          "functionCallId":"f-1",
          "functionResponse":{"surfaceId":"main"}
        }
        """)!.AsObject();

        var ex = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(raw));
        Assert.Contains("E_FUNCTION_RESPONSE_VALUE_REQUIRED", ex.Message);
    }

    [Fact]
    public void Normalizer_UpdateDataModel_Requires_Patches_And_Path()
    {
        var missingPatches = JsonNode.Parse("""
        {
          "version":"v0.10",
          "updateDataModel":{"surfaceId":"main"}
        }
        """)!.AsObject();
        var missingPath = JsonNode.Parse("""
        {
          "version":"v0.10",
          "updateDataModel":{"surfaceId":"main","patches":[{"value":"x"}]}
        }
        """)!.AsObject();

        var ex1 = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(missingPatches));
        var ex2 = Assert.Throws<JsonException>(() => A2uiNormalizer.Normalize(missingPath));
        Assert.Contains("E_PATCHES_REQUIRED", ex1.Message);
        Assert.Contains("E_PATCH_PATH_REQUIRED", ex2.Message);
    }
}
