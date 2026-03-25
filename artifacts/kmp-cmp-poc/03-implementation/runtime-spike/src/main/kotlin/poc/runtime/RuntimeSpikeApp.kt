package poc.runtime

import poc.shared.GreetingUseCase

class RuntimeAdapterEngine(
    private val host: RuntimeHost,
    private val greetingUseCase: GreetingUseCase = GreetingUseCase(),
) : RuntimeAdapter {
    private var frameCounter: Int = 0
    private var started: Boolean = false

    override fun start() {
        if (started) {
            host.onLog("start ignored: already started")
            return
        }
        started = true
        host.onLifecycle(LifecycleEvent.Start)
        host.onLog(greetingUseCase.greet("Runtime Adapter"))
        requestFrame(reason = "initial-boot")
    }

    override fun resume() {
        ensureStarted()
        host.onLifecycle(LifecycleEvent.Resume)
        requestFrame(reason = "resume")
    }

    override fun pause() {
        ensureStarted()
        host.onLifecycle(LifecycleEvent.Pause)
    }

    override fun stop() {
        if (!started) {
            host.onLog("stop ignored: adapter not started")
            return
        }
        host.onLifecycle(LifecycleEvent.Stop)
        started = false
    }

    override fun dispatchInput(event: InputEvent): Boolean {
        ensureStarted()
        val handled = host.onInput(event)
        if (handled) {
            requestFrame(reason = "input:${event.javaClass.simpleName}")
        } else {
            host.onLog("input dropped: $event")
        }
        return handled
    }

    override fun requestFrame(reason: String) {
        ensureStarted()
        frameCounter += 1
        host.onFrame(FrameEvent(number = frameCounter, reason = reason))
    }

    private fun ensureStarted() {
        check(started) { "RuntimeAdapterEngine is not started. Call start() first." }
    }
}
