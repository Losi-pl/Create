package com.losi.create.internal;

import com.losi.create.Version;
import com.losi.create.graphics.Window;
import com.losi.create.utility.CArrays;
import java.util.Objects;
import static org.lwjgl.glfw.GLFW.*;

public class Start {
    static void main(String[] args)
    {
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
        window.create();
        window.threadBind();
        window.run();

        glfwTerminate();
        Objects.requireNonNull(glfwSetErrorCallback(null)).free();
    }
}
