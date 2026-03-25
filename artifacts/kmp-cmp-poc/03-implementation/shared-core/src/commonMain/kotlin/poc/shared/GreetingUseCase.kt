package poc.shared

class GreetingUseCase {
    fun greet(target: String): String {
        val safeTarget = target.trim().ifEmpty { "Tizen Runtime Spike" }
        return "Hello, $safeTarget"
    }
}
