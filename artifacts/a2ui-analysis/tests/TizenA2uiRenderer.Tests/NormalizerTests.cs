using TizenA2uiRenderer.Model;

namespace TizenA2uiRenderer.Tests;

public class NormalizerTests
{
    [Fact]
    public void NormalMessage_CanBeCreated()
    {
        var msg = new NormalMessage("v0.10", NormalMessageType.CallFunction, "main", "fn-1");
        Assert.Equal("v0.10", msg.Version);
    }
}
