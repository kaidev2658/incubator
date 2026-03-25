package poc.runtime.mock

import poc.runtime.InputEvent
import poc.runtime.RuntimeAdapter

class MockHostScenario(
    private val adapter: RuntimeAdapter,
) {
    fun run() {
        adapter.start()
        adapter.resume()

        adapter.dispatchInput(InputEvent.Key(code = 22, action = "DOWN"))
        adapter.dispatchInput(InputEvent.Key(code = 100, action = "DOWN"))
        adapter.dispatchInput(InputEvent.Touch(x = 920f, y = 540f, action = "DOWN"))
        adapter.dispatchInput(InputEvent.Touch(x = 920f, y = 540f, action = "UP"))

        adapter.pause()
        adapter.resume()
        adapter.requestFrame(reason = "manual-debug")
        adapter.stop()
    }
}
