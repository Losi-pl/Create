package com.losi.create;

public class Startup {
    static void main() {
        IO.println("Welcome to Create!");

        IO.println("Create: " + Version.getVersion());
        IO.println("LWJGL: " + Version.GetLWJGLVersion());
        IO.println("JOML: " + Version.GetJOMLVersion());
        IO.println("JOML Primitives: " + Version.GetJOMLPrimVersion());
        IO.println("Steamworks: " + Version.GetSteamworksVersion());
        IO.println("Steamworks Server: " + Version.GetSteamworksServerVersion());

        for (int i = 1; i <= 5; i++) {
            IO.println("i = " + i);
        }
    }
}
