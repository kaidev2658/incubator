plugins {
    kotlin("jvm")
    application
}

dependencies {
    implementation(project(":shared-core"))
    testImplementation(kotlin("test"))
}

application {
    mainClass.set("poc.runtime.MainKt")
}
