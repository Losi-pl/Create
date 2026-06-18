@file:Suppress("SpellCheckingInspection", "GrazieInspectionRunner")

import org.gradle.internal.os.OperatingSystem

plugins {
    id("java")
    id("application")
    kotlin("jvm") version "2.4.0"
}

group = "com.losi.create"
version = "1.0.0-a1"

var lwjglVersion  = "3.4.1"
var jomlVersion = "1.10.9"
var jomlPrimitivesVersion = "1.10.0"
var steamworks4jVersion = "1.10.0"
var steamworks4jServerVersion = "1.10.0"
var querzNBTversion = "6.1"
var twelvemonkeysVersion = "3.13.1"

var lwjglNatives = when (OperatingSystem.current()) {
    OperatingSystem.LINUX -> {
        var natives = "natives-linux"
        var osArch = System.getProperty("os.arch")
        when
        {
            (osArch.startsWith("arm") || osArch.startsWith("aarch64")) ->
            { natives += if (osArch.contains("64") || osArch.startsWith("armv8")) "-arm64" else "-arm32" }
            osArch.startsWith("ppc") -> natives += "-ppc64le"
            osArch.startsWith("riscv") -> natives += "-riscv64"
        }
        natives
    }
    OperatingSystem.MAC_OS -> {
        if (System.getProperty("os.arch").startsWith("aarch64")) "natives-macos-arm64" else "natives-macos"
    }
    OperatingSystem.WINDOWS -> {
        var osArch = System.getProperty("os.arch")
        if (osArch.contains("64")) "natives-windows${if (osArch.startsWith("aarch64")) "-arm64" else ""}" else "natives-windows-x86"
    }

    else -> "unknown-platform"
}

dependencies {
    // =================== Junit ===================
    testImplementation(platform("org.junit:junit-bom:6.0.0"))
    testImplementation("org.junit.jupiter:junit-jupiter")
    testRuntimeOnly("org.junit.platform:junit-platform-launcher")

    // =================== LWJGL ===================
    if(lwjglNatives == "unknown-platform")
        throw Error("Unrecognized or unsupported platform.")
    implementation(platform("org.lwjgl:lwjgl-bom:$lwjglVersion"))
    listOf("-egl", "-fmod", "-jawt", "-odbc", "-opencl", "-renderdoc").forEach {
        implementation("org.lwjgl:lwjgl$it")
    }
    listOf("", "-assimp", "-bgfx", "-freetype", "-glfw", "-harfbuzz", "-hwloc", "-jemalloc", "-ktx", "-llvm", "-lmdb", "-lz4",
        "-meshoptimizer", "-msdfgen", "-nanovg", "-nfd", "-nuklear", "-openal", "-opengl", "-opengles", "-openxr", "-opus",
        "-par", "-remotery", "-rpmalloc", "-sdl", "-shaderc", "-spng", "-spvc", "-stb", "-tinyexr", "-tinyfd", "-vma", "-vulkan",
        "-xxhash", "-yoga", "-zstd").forEach {
        implementation("org.lwjgl:lwjgl$it")
        if (it != "-vulkan" || lwjglVersion.startsWith("natives-macos"))
            implementation("org.lwjgl:lwjgl$it::$lwjglNatives")
    }

    // =================== OpenGL Math ===================
    implementation("org.joml:joml:$jomlVersion")
    implementation("org.joml:joml-primitives:$jomlPrimitivesVersion")

    // =================== Steam ===================
    implementation("com.code-disaster.steamworks4j:steamworks4j:$steamworks4jVersion")
    implementation("com.code-disaster.steamworks4j:steamworks4j-server:$steamworks4jServerVersion")

    // =================== NBT ===================
    implementation("com.github.Querz:NBT:$querzNBTversion")

    // =================== Annotations ===================
    compileOnly("org.jetbrains:annotations:26.0.2")

    // =================== Collections ===================
    implementation("org.eclipse.collections:eclipse-collections-api:13.0.0")
    implementation("org.eclipse.collections:eclipse-collections:13.0.0") //TODO: Make an API to be more Kotlin frendly

    // =================== TwelveMonkeys ===================
    //https://github.com/haraldk/TwelveMonkeys
    listOf("lang", "io", "image").forEach {
        implementation("com.twelvemonkeys.common:common-$it:$twelvemonkeysVersion")
    }
    listOf("core", "metadata", "bmp", "dds", "hdr", "icns", "iff", "jpeg", "pcx", "pict", "pnm", "psd", "sgi", "tga", "thumbsdb", "tiff", "webp", "xwd", "batik").forEach {
        implementation("com.twelvemonkeys.imageio:imageio-$it:$twelvemonkeysVersion")
    }
    //https://xmlgraphics.apache.org/batik/
    implementation("org.apache.xmlgraphics:batik-transcoder:1.19")

    // =================== Kunion ===================
    implementation("com.github.renatoathaydes:kunion:0bd9cbfe38")

    // =================== Kotlin ===================
    implementation(kotlin("stdlib"))
    implementation(kotlin("reflect"))
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.11.0")
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-swing:1.11.0")

    // =================== Guava ===================
    implementation("com.google.guava:guava:33.6.0-jre")
}
kotlin { jvmToolchain(26) }
//TODO: https://openjdk.org/projects/valhalla/
tasks.jar {
    manifest {
        attributes(
            "Implementation-Title" to "Create",
            "Implementation-Version" to project.version
        )
    }
}
tasks.compileJava {
    options.forkOptions.jvmArgs = listOf("--enable-native-access=ALL-UNNAMED")
    options.compilerArgs.addAll(listOf("-Xlint:-incubating", "--enable-preview"))
}

tasks.test { jvmArgs = listOf("--enable-native-access=ALL-UNNAMED", "--enable-preview"); useJUnitPlatform() }
tasks.run { args = listOf("--version") }

/* TODO: See about including JRE into the game compilation
* Non-modular app: https://github.com/beryx/badass-runtime-plugin
* Modular apps:    https://github.com/beryx/badass-jlink-plugin
* */

application {
    applicationDefaultJvmArgs = listOf(
        "--enable-native-access=ALL-UNNAMED", "--enable-preview", "--sun-misc-unsafe-memory-access=allow",
        "-XX:+UseZGC", "-XX:+UseCompactObjectHeaders", "-XX:+AlwaysPreTouch")
    mainClass = "com.losi.create.App"
}