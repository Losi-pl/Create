package com.losi.create;

import com.losi.create.internal.WindowInternals;

public class Startup {
    static void main() {
        IO.println("Welcome to Create!");

        IO.println("Create: " + Version.getVersion());
        IO.println("LWJGL: " + Version.GetLWJGLVersion());
        IO.println("JOML: " + Version.GetJOMLVersion());
        IO.println("JOML Primitives: " + Version.GetJOMLPrimVersion());
        IO.println("Steamworks: " + Version.GetSteamworksVersion());
        IO.println("Steamworks Server: " + Version.GetSteamworksServerVersion());

        WindowInternals.createWindow.run();
    }
}
