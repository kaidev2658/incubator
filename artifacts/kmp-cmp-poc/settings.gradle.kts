rootProject.name = "kmp-cmp-poc"

include(":runtime-spike")
project(":runtime-spike").projectDir = file("03-implementation/runtime-spike")

include(":shared-core")
project(":shared-core").projectDir = file("03-implementation/shared-core")
