package com.losi.create.world

import com.losi.create.assets.AssetManager
import com.losi.create.graphics.*
import com.losi.create.registry.ElementRegister
import com.losi.create.utility.orElse
import org.joml.*
import org.lwjgl.opengl.GL11
import java.awt.Color

class GameSession: Scene() {

    val shader = AssetManager.get<Shader>("create:blocks/single-texture").orElse { throw RuntimeException("Required shader not found (create:single-texture)") }
    val mesh = Mesh(shader).apply {
        setAttribute("pos", arrayOf(

            //South
            Vector3f(-1f,1f, -1f),
            Vector3f(1f, 1f,-1f),
            Vector3f(1f, -1f,-1f),
            Vector3f(-1f,-1f, -1f),

            //North
            Vector3f(1f,1f, 1f),
            Vector3f(-1f, 1f,1f),
            Vector3f(-1f, -1f,1f),
            Vector3f(1f,-1f, 1f),

            //East
            Vector3f(1f, 1f,  -1f),
            Vector3f(1f, 1f,  1f),
            Vector3f(1f, -1f, 1f),
            Vector3f(1f, -1f, -1f),

            //West
            Vector3f(-1f, 1f,  1f),
            Vector3f(-1f, 1f,  -1f),
            Vector3f(-1f, -1f, -1f),
            Vector3f(-1f, -1f, 1f),

            //Top
            Vector3f(-1f,1f, 1f),
            Vector3f(1f, 1f,1f),
            Vector3f(1f, 1f,-1f),
            Vector3f(-1f,1f, -1f),

            //Bottom
            Vector3f(-1f,-1f, -1f),
            Vector3f(1f, -1f,-1f),
            Vector3f(1f, -1f,1f),
            Vector3f(-1f,-1f, 1f),
        ))

        setAttribute("uvPos", arrayOf(
            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),

            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),

            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),

            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),

            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),

            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f),
        ))

        triangles(arrayOf(
            //South
            0  + 0,  0  + 1,  0  + 3,
            0  + 1,  0  + 2,  0  + 3,

            //North
            4  + 0,  4  + 1,  4  + 3,
            4  + 1,  4  + 2,  4  + 3,

            //East
            8  + 0,  8  + 1,  8  + 3,
            8  + 1,  8  + 2,  8  + 3,

            //West
            12 + 0,  12 + 1,  12 + 3,
            12 + 1,  12 + 2,  12 + 3,

            //Top
            16 + 0,  16 + 1,  16 + 3,
            16 + 1,  16 + 2,  16 + 3,

            //Bottom
            20 + 0,  20 + 1,  20 + 3,
            20 + 1,  20 + 2,  20 + 3,
        ))

        burnModel()
        flushBuffers()
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

        //Placeholder
        ElementRegister.loadElementUuids(sequenceOf())

        val projection = Matrix4f().perspectiveLH(Math.toRadians(45f), windowSize.x / windowSize.y.toFloat(), 0.1f, 100f)
        val view = Matrix4f().lookAtLH(Vector3f(0f, 0f, -10f), Vector3f(0f, 0f, 0f), Vector3f(0f, 1f, 0f))

        shader.setUniform("projection", projection)
        shader.setUniform("view", view)
        shader.setUniform("model", Matrix4f())

        //Temporary
        GL11.glEnable(GL11.GL_DEPTH_TEST)
    }

    override fun logicUpdate(timeDelta: Float) {
        if(isRot)
            rot += timeDelta
        shader.setUniform("model", Matrix4f().rotateX(Math.toRadians(-45f)).rotateY(rot))
    }

    override fun renderUpdate(timeDelta: Float) {
        mesh.draw()
    }
}