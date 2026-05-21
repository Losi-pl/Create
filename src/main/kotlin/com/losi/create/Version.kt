package com.losi.create

object Version
{
    private var SteamworksServer: String = null!!
    private var Create: String = null!!
    private var JOML: String = null!!
    private var JOMLPrimitives: String = null!!

    init {
        try {
            var stream = Version::class.java.module.getResourceAsStream("version.properties")
            var prop = java.util.Properties()
            prop.load(stream);
            SteamworksServer = prop["steamworks4j-server-version"] as String
            Create = prop["create-version"] as String
            JOML = prop["joml-version"] as String
            JOMLPrimitives = prop["joml-primitives-version"] as String
        }
        catch (e: Exception) {
            Create = Create ?: "unknown"
            SteamworksServer = SteamworksServer ?: "unknown"
            JOML = JOML ?: "unknown"
            JOMLPrimitives = JOMLPrimitives ?: "unknown"
        }
    }

    @JvmStatic
    val version: String get() = Create
    @JvmStatic
    val LWJGLVersion: String get() = org.lwjgl.Version.getVersion()
    @JvmStatic
    val JOMLVersion: String get() {
        try
        {
            val pack = org.joml.Math::class.java.`package`;
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
