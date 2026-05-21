package com.losi.create

object Version
{
    private var SteamworksServer: String
    private var Create: String
    private var JOML: String
    private var JOMLPrimitives: String

    init {
        var steam: String? = null
        var create: String? = null
        var joml: String? = null
        var jomlPrim: String? = null

        try {
            var stream = Version::class.java.module.getResourceAsStream("version.properties")
            var prop = java.util.Properties()
            prop.load(stream)
            steam = prop["steamworks4j-server-version"] as String
            create = prop["create-version"] as String
            joml = prop["joml-version"] as String
            jomlPrim = prop["joml-primitives-version"] as String
        }
        catch (_: Exception) { }

        Create = create ?: "unknown"
        SteamworksServer = steam ?: "unknown"
        JOML = joml ?: "unknown"
        JOMLPrimitives = jomlPrim ?: "unknown"
    }

    @JvmStatic
    val version: String get() = Create
    @JvmStatic
    val LWJGLVersion: String get() = org.lwjgl.Version.getVersion()
    @JvmStatic
    val JOMLVersion: String get() {
        try
        {
            val pack = org.joml.Math::class.java.`package`
            return pack.implementationVersion ?: JOML
        }
        catch (_: Exception)
        { return "error" }
    }
    @JvmStatic
    val JOMLPrimVersion: String get() {
        try
        {
            val pack = org.joml.primitives.Rayf::class.java.`package`
            return pack.implementationVersion ?: JOMLPrimitives
        }
        catch (_: Exception)
        { return "error"; }
    }
    @JvmStatic
    val SteamworksVersion: String get() = com.codedisaster.steamworks.Version.getVersion()
    @JvmStatic
    val SteamworksServerVersion: String get() = SteamworksServer
}
