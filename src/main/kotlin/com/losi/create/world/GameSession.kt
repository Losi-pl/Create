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

    val shader = AssetManager.get<Shader>("create:blocks/single-texture").orElse { throw RuntimeException("Required shader not found (create:single-texture)") }.apply {
        val view = Matrix4f().lookAtLH(Vector3f(0f, 0f, -10f), Vector3f(0f, 0f, 0f), Vector3f(0f, 1f, 0f))

        setUniform("view", view)
        setUniform("model", Matrix4f())

        setUniform("atlas", BlockTexture.atlas)
    }
    val model = run {
        val ch = Realms.Earth.world.loadChunk(ChunkPos(0, 0))
        val dirt = PlacedBlock(Blocks.Dirt)
        val dirt2 = PlacedBlock(Blocks.Dirt)
        ch[0, 0, 0] = dirt
        ch[0, 0, 1] = dirt
        ch[1, 0, 0] = dirt
        ch[1, 0, 1] = dirt

        ch[0, 1, 0] = dirt2
        ch[1, 1, 1] = dirt2

        ChunkModeler(Realms.Earth, ChunkPos(0, 0)).generate()
    }

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

        val projection = Matrix4f().perspectiveLH(Math.toRadians(45f), windowSize.x / windowSize.y.toFloat(), 0.1f, 100f)
        shader.setUniform("projection", projection)

        //Placeholder
        ElementRegister.loadElementUuids(sequenceOf())

        //Temporary
        GL11.glEnable(GL11.GL_DEPTH_TEST)
    }

    override fun logicUpdate(timeDelta: Float) {
        if(isRot)
            rot += timeDelta
        shader.setUniform("model", Matrix4f().setRotationXYZ(Math.toRadians(-45f), rot, 0f).translate(-1f, -1f, -1f))
    }

    override fun renderUpdate(timeDelta: Float) {
        model.draw()
    }
}