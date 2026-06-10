package com.losi.create

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.utility.*
import org.lwjgl.glfw.GLFW
import kotlin.concurrent.thread

internal object App {
    /**The main game window */
    private var main: Window? = null

    /**The context allowing for the OpenGL operations during the construction of the resources */
    private var context: GLContext? = null

    /**The starting methods of the game starting all other processes
     * @param args The parameters of the game startup
     */
    @JvmStatic fun main(vararg args: String) {
        println("Welcome to Create!")

        args.find { it.equals("--version", true) }?.let {
            println("Create: ${Version.version}")
            println("LWJGL: ${Version.LWJGLVersion}")
            println("JOML: ${Version.JOMLVersion}")
            println("JOML Primitives: ${Version.JOMLPrimVersion}")
            println("Steamworks: ${Version.SteamworksVersion}")
        }

        OnMainThread.mainThread = Thread.currentThread()

        main = Window()
        main.mustRun {
            //Configure the window icon and title
            icon = Version::class.java.module.getResourceAsStream("Icon.ico")
            title = "Create: ${Version.version}"

            //Connect OpenGL logic and thread logic
            create()
            threadBind()
            registerLogic(OnMainThread::callAction)

            //Create Assets loading thread
            context = GLContext(this)
            thread(name = "Assets loading", block = App::buildAssets)

            run()
        }

        GLFW.glfwTerminate()
        GLFW.glfwSetErrorCallback(null)?.free()
    }

    /**The method executed in the parallel thread, it is meant to load all resources of the game.
     *
     * At the moment including:
     * - Type: [Shader]: Assets: `shaders/` */
    fun buildAssets() = context?.use { context ->
        context.threadBind()
        AssetManager.constructAssetLoader()
        AssetManager.registerProcessor(ShaderProcessor, "shaders")
        AssetManager.registerProcessor(BlockTexture.BlockAtlasProcessor, "textures/blocks")
        AssetManager.processAssets()
    }
}