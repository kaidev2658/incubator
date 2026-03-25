plugins {
    kotlin("multiplatform") version "2.3.0" apply false
    kotlin("jvm") version "2.3.0" apply false
}

allprojects {
    group = "dev.incubator.kmpcmp"
    version = "0.1.0"

    repositories {
        mavenCentral()
        google()
    }
}
