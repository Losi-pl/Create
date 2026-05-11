package com.losi.create;

import org.jetbrains.annotations.Contract;
import org.jetbrains.annotations.NotNull;

import java.util.Objects;
import java.util.Properties;

public class Version
{
    private static String SteamworksServer = null;
    private static String Create = null;
    static
    {
        try
        {
            var stream = Objects.requireNonNull(Thread.currentThread().getContextClassLoader().getResource("version.properties")).openStream();
            var prop = new Properties();
            prop.load(stream);
            Create = (String) prop.get("version");
            SteamworksServer = (String) prop.get("steamworks4j-serverVersion");
        }
        catch (Exception e)
        {
            if(SteamworksServer == null)
                SteamworksServer = "unknown";
            if(Create == null)
                Create = "unknown";
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
            return var == null ? "unknown" : var;
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
            return var == null ? "unknown" : var;
        }
        catch (Exception e)
        { return "error"; }
    }
    @NotNull @Contract(pure = true)
    public static String GetSteamworksVersion() { return com.codedisaster.steamworks.Version.getVersion(); }
    @NotNull @Contract(pure = true)
    public static String GetSteamworksServerVersion() { return SteamworksServer; }
}
