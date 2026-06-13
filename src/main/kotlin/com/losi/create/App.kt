package com.losi.create

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.registry.LoadingScene
import com.losi.create.registry.RegisterOrder
import com.losi.create.utility.*
import org.lwjgl.glfw.GLFW
import kotlin.concurrent.thread

private object App {
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

        val window = Window().apply {
            //Set game icon
            icon = Version::class.java.module.getResourceAsStream("Icon.ico")

            //Connect OpenGL logic and thread logic
            create()
            threadBind()
            registerLogic(OnMainThread::callAction)

            scene = LoadingScene()

            //Create Assets loading thread
            context = GLContext(this)
            thread(name = "Resource Constructing") {
                context?.use { context ->
                    context.threadBind()
                    RegisterOrder.registerAssetPrecesses()
                    RegisterOrder.precesses().forEach {
                        it.first.run()
                    }
                }
            }
        }
        window.run()

        GLFW.glfwTerminate()
        GLFW.glfwSetErrorCallback(null)?.free()
    }

    /**The manifests of all processes during loading that are part of this project*/
    fun RegisterOrder.registerAssetPrecesses() {
        /*For loading of sources for all file resources*/
        registerProcess(AssetManager.findingAssetSources)

        /*For registering parsers of file assets*/
        registerProcess(AssetManager.assetParsers) {
            dependsOn(AssetManager.findingAssetSources)
        }

        /*Parsing of the file assets*/
        registerProcess(AssetManager.assetParsing) {
            dependsOn(AssetManager.assetParsers)
        }

    }
}