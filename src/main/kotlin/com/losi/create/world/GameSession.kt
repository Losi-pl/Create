package com.losi.create.world

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.registry.ElementRegister
import com.losi.create.utility.orElse
import com.losi.create.world.geometry.ChunkModeler
import org.joml.*
import org.lwjgl.opengl.GL11
import java.awt.Color

class GameSession: Scene() {

    val view = Matrix4f().lookAtLH(Vector3f(0f, 0f, -10f), Vector3f(0f, 0f, 0f), Vector3f(0f, 1f, 0f))
    val projection = Matrix4f()
    val modelM = Matrix4f()

    var model: ChunkModel? = null
    var rebound = false

    var rot: Float = 0f
    var isRot = true

    override fun onKeyDown(key: Key, mods: KeyMods) {
        if(key isKey KeyCode.F)
            rot = 0f
        if(key isKey KeyCode.D)
            isRot = !isRot
    }

    override fun onSceneInit() {
        title = "Create"
        backgroundColor = Color.ORANGE

        projection.perspectiveLH(Math.toRadians(45f), windowSize.x / windowSize.y.toFloat(), 0.1f, 100f)

        AssetManager.get<Shader>("create:blocks/single-texture")?.setUniform("atlas", BlockTexture.atlas)
        AssetManager.get<Shader>("create:blocks/colored-texture")?.setUniform("atlas", BlockTexture.atlas)

        //Placeholder
        ElementRegister.loadElementUuids(sequenceOf())

        //Temporary
        GL11.glEnable(GL11.GL_DEPTH_TEST)
        GL11.glEnable(GL11.GL_CULL_FACE)
        GL11.glCullFace(GL11.GL_FRONT)

        thread(name = "Initial World Generation") {
            val ch = Realms.Earth.world.loadChunk(ChunkPos(0, 0))

            Realms.Earth.world.loadChunk(ChunkPos(-1, 0))
            Realms.Earth.world.loadChunk(ChunkPos(0, -1))

            val dirt = PlacedBlock(Blocks.Dirt)
            val stone = PlacedBlock(Blocks.Stone)
            val bedrock = PlacedBlock(Blocks.Bedrock)
            ch[1, 0, 1] = dirt
            ch[1, 0, 2] = dirt
            ch[2, 0, 1] = dirt
            ch[2, 0, 2] = dirt

            ch[2, 1, 2] = PlacedBlock(Blocks.GrassBlock)

            ch[1, 0, 0] = stone
            ch[2, 0, 0] = stone
            ch[0, 0, 1] = stone
            ch[0, 0, 2] = stone
            ch[1, 0, 3] = stone
            ch[2, 0, 3] = stone
            ch[3, 0, 1] = stone
            ch[3, 0, 2] = stone

            ch[0, 0, 0] = bedrock
            ch[3, 0, 0] = bedrock
            ch[0, 0, 3] = bedrock
            ch[3, 0, 3] = bedrock

            model = ChunkModeler(Realms.Earth, ChunkPos(0, 0)).generate()
        }
    }

    override fun onResize(width: Int, height: Int) {
        projection.setPerspectiveLH(Math.toRadians(45f), width / height.toFloat(), 0.1f, 100f)
    }

    override fun logicUpdate(timeDelta: Float) {
        if(isRot)
            rot += timeDelta
        modelM.identity().setRotationXYZ(Math.toRadians(-5f), rot, 0f).translate(-2f, -2f, -2f)
    }

    override fun renderUpdate(timeDelta: Float) {
        model?.let {
            if(!rebound) {
                it.threadBind()
                rebound = true
            }
            it.draw(modelM, view, projection)
        }
    }
}