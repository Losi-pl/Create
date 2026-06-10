package com.losi.create

/**Manifest of versions of library's in this project*/
object Version
{
    private const val VERSION = "1.0.0-a1"

    val myVersion = lazy {
        try
        {
            val version = Version::class.java.`package`?.implementationVersion ?: VERSION
            return@lazy version
        }
        catch (_: Exception)
        { return@lazy "error" }
    }
    @JvmStatic
    val version: String get() = myVersion.value
    @JvmStatic
    val LWJGLVersion: String get() = org.lwjgl.Version.getVersion()

    @JvmStatic
    val JOMLVersion: String get() = joml.value
    val joml = lazy {
        return@lazy try {
            org.joml.Math::class::class.java.`package`?.implementationVersion ?: "unknown"
        } catch (_: Exception) {
            "error"
        }
    }

    @JvmStatic
    val JOMLPrimVersion: String get() = jomlPrim.value
    val jomlPrim = lazy {
        return@lazy try {
            org.joml.primitives.Planed::class.java.`package`?.implementationVersion ?: "unknown"
        } catch (_: Exception) {
            "error"
        }
    }
    @JvmStatic
    val SteamworksVersion: String get() = com.codedisaster.steamworks.Version.getVersion()
}
