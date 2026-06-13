package com.losi.create

import com.losi.create.graphics.*
import com.losi.create.registry.LoadingScene
import com.losi.create.utility.*
import org.lwjgl.glfw.GLFW

private object App {
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

        val window = Window().apply {
            //Set game icon
            icon = Version::class.java.module.getResourceAsStream("Icon.ico")

            //Connect OpenGL logic and thread logic
            create()
            threadBind()
            registerLogic(OnMainThread::callAction)

            scene = LoadingScene()
        }
        window.run()

        GLFW.glfwTerminate()
        GLFW.glfwSetErrorCallback(null)?.free()
    }
}