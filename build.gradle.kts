@file:Suppress("SpellCheckingInspection")

import org.gradle.internal.os.OperatingSystem
import java.util.Properties

plugins {
    id("java")
    id("application")
    id("org.gradlex.extra-java-module-info") version "1.14"
    kotlin("jvm") version "2.3.21"
}

group = "com.losi.create"
version = "1.0.0-a1"

var lwjglVersion  = "3.4.1"
var jomlVersion = "1.10.8"
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

    // =================== Koloboke ===================
    compileOnly("com.koloboke:koloboke-api-jdk8:1.0.0")
    runtimeOnly("com.koloboke:koloboke-impl-jdk8:1.0.0")

    // =================== TwelveMonkeys ===================
    //https://github.com/haraldk/TwelveMonkeys
    listOf("lang", "io", "image").forEach {
        implementation("com.twelvemonkeys.common:common-$it:$twelvemonkeysVersion")
    }
    listOf("core", "metadata", "bmp", "dds", "hdr", "icns", "iff", "jpeg", "pcx", "pict", "pnm", "psd", "sgi", "tga", "thumbsdb", "tiff", "webp", "xwd").forEach {
        implementation("com.twelvemonkeys.imageio:imageio-$it:$twelvemonkeysVersion")
    }

    // =================== Kotlin ===================
    implementation(kotlin("stdlib"))
    implementation(kotlin("reflect"))
}
extraJavaModuleInfo {
    failOnMissingModuleInfo = false
    automaticModule("com.github.Querz:NBT", "nbt.querz")
    automaticModule("com.koloboke:koloboke-api-jdk8", "koloboke.api.jdk8")
    module("org.jetbrains:annotations", "org.jetbrains.annotations")
    { patchRealModule(); preserveExisting(); exports("org.jetbrains.annotations") }
    automaticModule("com.code-disaster.steamworks4j:steamworks4j", "steamworks4j")
    { mergeJar("com.code-disaster.steamworks4j:steamworks4j-server") }
    module("org.joml:joml", "org.joml")
    { patchRealModule(); preserveExisting(); requires("kotlin.stdlib") }
}
kotlin { jvmToolchain(25) }
java {
    sourceCompatibility = JavaVersion.VERSION_25
    targetCompatibility = JavaVersion.VERSION_25
}

tasks.register("createProperties") {
    dependsOn("processResources")
    var propsFileProvider = layout.buildDirectory.file("resources/main/version.properties")
    outputs.file(propsFileProvider)

    // Declare inputs so Gradle knows when to rerun
    inputs.property("create", version)
    inputs.property("lwjgl", lwjglVersion)
    inputs.property("joml", jomlVersion)
    inputs.property("joml-primitives", jomlPrimitivesVersion)
    inputs.property("steamworks", steamworks4jVersion)
    inputs.property("steamworks-server", steamworks4jServerVersion)
    inputs.property("querz-NBT", querzNBTversion)

    doLast {
        var propsFile = propsFileProvider.get().asFile
        propsFile.parentFile.mkdirs()
        var p = Properties()
        p["create-version"] = inputs.properties["create"]
        p["lwjgl-version"] = inputs.properties["lwjgl"]
        p["joml-version"] = inputs.properties["joml"]
        p["joml-primitives-version"] = inputs.properties["joml-primitives"]
        p["steamworks4j-version"] = inputs.properties["steamworks"]
        p["steamworks4j-server-version"] = inputs.properties["steamworks-server"]
        p["querz-NBT-version"] = inputs.properties["querz-NBT"]
        propsFile.writer().use { p.store(it, null) }
    }
}
tasks.named("classes") { dependsOn(tasks.named("createProperties")) }
tasks.compileJava {
    options.forkOptions.jvmArgs = listOf("--enable-native-access=ALL-UNNAMED")
    options.compilerArgs.add("-Xlint:-incubating")
    options.compilerArgumentProviders.add(
        object : CommandLineArgumentProvider {
            @get:CompileClasspath
            val kotlinClasses = tasks.compileKotlin.flatMap { it.destinationDirectory }

            override fun asArguments(): List<String> = listOf(
                "--patch-module",
                "com.losi.create=${kotlinClasses.get().asFile.absolutePath}"
            )
        }
    )
}

tasks.test { jvmArgs = listOf("--enable-native-access=ALL-UNNAMED"); useJUnitPlatform() }
tasks.run { args = listOf("--version") }

application {
    applicationDefaultJvmArgs = listOf("--enable-native-access=org.lwjgl", "--enable-native-access=org.lwjgl.opengl")
    mainClass = "com.losi.create.internal.Start"
    mainModule = "com.losi.create"
}