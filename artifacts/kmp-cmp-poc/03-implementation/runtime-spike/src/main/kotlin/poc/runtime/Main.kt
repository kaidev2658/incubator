package poc.runtime

import poc.runtime.mock.MockHostScenario
import poc.runtime.mock.MockRuntimeHost

fun main() {
    val host = MockRuntimeHost(name = "pre-device-jvm-host")
    val adapter = RuntimeAdapterEngine(host = host)
    val scenario = MockHostScenario(adapter = adapter)

    scenario.run()

    println("---- mock host event log (${host.events.size} events) ----")
    host.events.forEach(::println)
}
