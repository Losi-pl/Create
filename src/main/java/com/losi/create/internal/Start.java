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
            IO.println("LWJGL: " + Version.getLWJGLVersion());
            IO.println("JOML: " + Version.getJOMLVersion());
            IO.println("JOML Primitives: " + Version.getJOMLPrimVersion());
            IO.println("Steamworks: " + Version.getSteamworksVersion());
            IO.println("Steamworks Server: " + Version.getSteamworksServerVersion());
        }

        var window = new Window();
        try { window.setIcon(Version.class.getModule().getResourceAsStream("Icon.ico")); } catch (IOException ignored) { }
        window.setTitle("Create: " + Version.getVersion());
        window.create();
        window.threadBind();
        window.registerLogic(OnMainThread.INSTANCE::callAction$create);
        window.run();

        glfwTerminate();
        Objects.requireNonNull(glfwSetErrorCallback(null)).free();
    }
}
