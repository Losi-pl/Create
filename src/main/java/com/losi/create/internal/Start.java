package com.losi.create.internal;

import com.losi.create.Version;
import com.losi.create.assets.AssetsManager;
import com.losi.create.assets.ShaderProcessor;
import com.losi.create.graphics.GLContext;
import com.losi.create.graphics.Shader;
import com.losi.create.graphics.Window;
import com.losi.create.utility.CArrays;
import com.losi.create.utility.OnMainThread;
import java.io.IOException;
import java.util.Objects;

import static org.lwjgl.glfw.GLFW.*;

public class Start {
    static Window main;
    static GLContext context;

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

        OnMainThread.INSTANCE.setMainThread$create(Thread.currentThread());
        main = new Window();
        try { main.setIcon(Version.class.getModule().getResourceAsStream("Icon.ico")); } catch (IOException ignored) { }
        main.setTitle("Create: " + Version.getVersion());
        main.create();
        main.threadBind();
        main.registerLogic(OnMainThread.INSTANCE::callAction$create);
        context = new GLContext(main);
        var register = new Thread(Start::BuildAssets);
        register.start();
        main.run();

        glfwTerminate();
        Objects.requireNonNull(glfwSetErrorCallback(null)).free();
    }

    public static void BuildAssets()
    {
        context.threadBind();
        AssetsManager.constructAssetLoader$create();
        AssetsManager.registerProcessor(ShaderProcessor.INSTANCE, Shader.class, "shaders");
        AssetsManager.INSTANCE.processAssets$create();
        context.close();
    }
}
