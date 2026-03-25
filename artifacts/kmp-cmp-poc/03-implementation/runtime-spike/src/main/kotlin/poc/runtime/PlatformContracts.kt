package poc.runtime

enum class LifecycleEvent {
    Start,
    Resume,
    Pause,
    Stop,
}

sealed interface InputEvent {
    data class Key(val code: Int, val action: String) : InputEvent
    data class Touch(val x: Float, val y: Float, val action: String) : InputEvent
}

data class FrameEvent(
    val number: Int,
    val reason: String,
)

interface RuntimeHost {
    fun onLifecycle(event: LifecycleEvent)
    fun onInput(event: InputEvent): Boolean
    fun onFrame(event: FrameEvent)
    fun onLog(message: String)
}

interface RuntimeAdapter {
    fun start()
    fun resume()
    fun pause()
    fun stop()
    fun dispatchInput(event: InputEvent): Boolean
    fun requestFrame(reason: String = "manual")
}
