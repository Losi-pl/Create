package com.losi.create;

import org.jetbrains.annotations.Contract;
import org.jetbrains.annotations.NotNull;

import java.util.Properties;

public class Version
{
    private static String SteamworksServer = null, Create = null, JOML = null, JOMLPrimitives;
    static
    {
        try
        {
            var stream = Version.class.getModule().getResourceAsStream("version.properties");
            var prop = new Properties();
            prop.load(stream);
            SteamworksServer = (String) prop.get("steamworks4j-server-version");
            Create = (String) prop.get("create-version");
            JOML = (String) prop.get("joml-version");
            JOMLPrimitives = (String) prop.get("joml-primitives-version");
        }
        catch (Exception e)
        {
            if(SteamworksServer == null)
                SteamworksServer = "unknown";
            if(Create == null)
                Create = "unknown";
            if(JOML == null)
                JOML = "unknown";
            if(JOMLPrimitives == null)
                JOMLPrimitives = "unknown";
        }
    }

    @NotNull @Contract(pure = true)
    public static String getVersion() { return Create; }
    @NotNull @Contract(pure = true)
    public static String GetLWJGLVersion() { return org.lwjgl.Version.getVersion(); }
    @NotNull @Contract(pure = true)
    public static String GetJOMLVersion() {
        try
        {
            var pack = org.joml.Math.class.getPackage();
            var var = pack.getImplementationVersion();
            return var == null ? JOML : var;
        }
        catch (Exception e)
        { return "error"; }
    }
    @NotNull @Contract(pure = true)
    public static String GetJOMLPrimVersion() {
        try
        {
            var pack = org.joml.primitives.Rayf.class.getPackage();
            var var = pack.getImplementationVersion();
            return var == null ? JOMLPrimitives : var;
        }
        catch (Exception e)
        { return "error"; }
    }
    @NotNull @Contract(pure = true)
    public static String GetSteamworksVersion() { return com.codedisaster.steamworks.Version.getVersion(); }
    @NotNull @Contract(pure = true)
    public static String GetSteamworksServerVersion() { return SteamworksServer; }
}
