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
}
