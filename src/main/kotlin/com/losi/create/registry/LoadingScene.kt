package com.losi.create.registry

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.utility.OnMainThread
import com.losi.create.utility.joml.*
import com.losi.create.world.GameSession
import org.joml.*

internal class LoadingScene: Scene() {
    lateinit var shader:Shader
    lateinit var mesh: Mesh
    var atlasUsed = false
    lateinit var loadingThread: Thread
    var loadingError: Throwable? = null

    override fun onSceneInit() {
        findResource("Icon.ico").use { icon = it }
        title = "Create: Loading"


        shader = run {
            val vertex = findResource("assets/create/shaders/basic.vert")
            val fragment = findResource("assets/create/shaders/basic.frag")
            val xml = findResource("assets/create/shaders/basic.xml")

            Shader(vertex, fragment, xml).apply {
                setUniform("model", Matrix4f())
                setUniform("view", Matrix4f())

                setUniform("projection", Matrix4f().setOrtho(windowSize.x, windowSize.y))

                val texture = findResource("assets/create/textures/blocks/debug3.svg").use { Texture2D(it) }
                setUniform("image", texture)

                vertex.close()
                fragment.close()
                xml.close()
            }
        }

        mesh = Mesh(shader).apply {
            setAttribute("position", arrayOf(
                Vector3f(-1f,1f, 0f),
                Vector3f(1f, -1f,0f),
                Vector3f(-1f,-1f,0f),
                Vector3f(-1f,1f, 0f),
                Vector3f(1f, -1f,0f),
                Vector3f(1f, 1f, 0f)))

            burnModel()
            flushBuffers()
        }

        loadingThread = thread(start = false, name = "Element Loading") {
            RegisterOrder.registerAssetPrecesses()
            RegisterOrder.precesses().forEach {
                OnMainThread.schedule { title = "Create: Loading: ${it.second}" }
                it.first.run()
            }
        }.apply {
            uncaughtExceptionHandler = { _, e -> loadingError = e }
            start()
        }
    }

    override fun onSceneDispose() {
        mesh.release()
        shader.release()
        System.gc()
    }

    override fun onKeyDown(key: Key, mods: KeyMods) {
        if(key isKey KeyCode.Escape)
            close()
        else if(key isKey KeyCode.D)
            shader.release()

        if(AssetManager.isLoaded){
            if(key isKey KeyCode.E)
                shader.setUniform("useAtlas", true)
            if(key isKey KeyCode.Q)
                shader.setUniform("useAtlas", false)
        }
    }

    override fun onResize(width: Int, height: Int) {
        shader.setUniform("projection", Matrix4f().setOrtho(width, height))
    }

    override fun logicUpdate(timeDelta: Float) {
        if(!atlasUsed && AssetManager.isLoaded)
        {
            shader.setUniform("atlas", BlockTexture.atlas)
            shader.setUniform("useAtlas", true)
            shader.setUniform("textureInd", BlockTexture.NOT_FOUND.index)
            atlasUsed = true
        }

        if(!loadingThread.isAlive)
        {
            loadingError?.printStackTrace(System.err)
            goTo(GameSession())
        }
    }

    override fun renderUpdate(timeDelta: Float) {
        mesh.draw()
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

        /*Loading of all game elements and their logic*/
        registerProcess(ElementRegister.loadingGameElements) {
            dependsOn(AssetManager.assetParsers)
        }

        /*Closes all ElementRegister's making them faster and read-only*/
        registerProcess(ElementRegister.completeElementRegisters) {
            dependsOn(ElementRegister.loadingGameElements)
        }
    }

    private fun findResource(path: String) = LoadingScene::class.java.module.getResourceAsStream(path)?: throw java.io.IOException("Unable to find $path")
}