package poc.runtime.mock

import poc.runtime.FrameEvent
import poc.runtime.InputEvent
import poc.runtime.LifecycleEvent
import poc.runtime.RuntimeHost

class MockRuntimeHost(
    private val name: String,
) : RuntimeHost {
    val events: MutableList<String> = mutableListOf()

    override fun onLifecycle(event: LifecycleEvent) {
        record("lifecycle:$event")
    }

    override fun onInput(event: InputEvent): Boolean {
        val handled = when (event) {
            is InputEvent.Key -> event.code in setOf(19, 20, 21, 22, 23)
            is InputEvent.Touch -> event.action != "CANCEL"
        }
        record("input:$event -> handled=$handled")
        return handled
    }

    override fun onFrame(event: FrameEvent) {
        record("frame:${event.number} reason=${event.reason}")
    }

    override fun onLog(message: String) {
        record("log:$message")
    }

    private fun record(message: String) {
        events += "[$name] $message"
    }
}
