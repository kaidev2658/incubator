using TizenA2uiRenderer.Transport;

namespace TizenA2uiRenderer.Tests;

public class ParserTests
{
    [Fact]
    public void Flush_ReturnsTextEvent_WhenBufferHasContent()
    {
        var parser = new A2uiParser();
        parser.AddChunk("hello");

        var events = parser.Flush();

        Assert.Single(events);
        Assert.IsType<TextEvent>(events[0]);
    }
}
