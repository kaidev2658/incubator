package poc.shared

import kotlin.test.Test
import kotlin.test.assertEquals

class GreetingUseCaseTest {
    private val subject = GreetingUseCase()

    @Test
    fun `returns greeting for explicit target`() {
        assertEquals("Hello, Engineer", subject.greet("Engineer"))
    }

    @Test
    fun `falls back when target is blank`() {
        assertEquals("Hello, Tizen Runtime Spike", subject.greet("  \t  "))
    }
}
