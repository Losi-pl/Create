package com.losi.create.internal;

import com.losi.create.Version;
import com.losi.create.graphics.Window;
import com.losi.create.utility.CArrays;
import com.losi.create.utility.OnMainThread;

import java.io.IOException;
import java.util.Objects;

import static org.lwjgl.glfw.GLFW.*;

public class Start {
    static void main(String[] args) {
        IO.println("Welcome to Create!");

        if(CArrays.findAny(args, "--Version", true))
        {
            IO.println("Create: " + Version.getVersion());
            IO.println("LWJGL: " + Version.GetLWJGLVersion());
            IO.println("JOML: " + Version.GetJOMLVersion());
            IO.println("JOML Primitives: " + Version.GetJOMLPrimVersion());
            IO.println("Steamworks: " + Version.GetSteamworksVersion());
            IO.println("Steamworks Server: " + Version.GetSteamworksServerVersion());
        }

        var window = new Window();
        try { window.icon(Version.class.getModule().getResourceAsStream("Icon.ico")); } catch (IOException ignored) { }
        window.title("Create: " + Version.getVersion());
        window.create();
        window.threadBind();
        window.registerLogic(OnMainThread.INSTANCE::callAction$create);
        window.run();

        glfwTerminate();
        Objects.requireNonNull(glfwSetErrorCallback(null)).free();
    }
}
