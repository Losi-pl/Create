package com.losi.create.world

import com.losi.create.assets.AssetManager
import com.losi.create.graphics.*
import com.losi.create.registry.ElementRegister
import com.losi.create.utility.orElse
import org.joml.*
import java.awt.Color

class GameSession: Scene() {

    val shader = AssetManager.get<Shader>("create:blocks/single-texture").orElse { throw RuntimeException("Required shader not found (create:single-texture)") }
    val mesh = Mesh(shader).apply {
        setAttribute("pos", arrayOf(
            Vector3f(-1f,1f, 0f),
            Vector3f(1f, 1f,0f),
            Vector3f(1f,-1f,0f),
            Vector3f(-1f, -1f, 0f)))

        setAttribute("uvPos", arrayOf(
            Vector2f(0f, 1f),
            Vector2f(1f, 1f),
            Vector2f(1f, 0f),
            Vector2f(0f, 0f)))

        triangles(arrayOf(
                  0,1,3,
                  1,2,3))

        burnModel()
        flushBuffers()
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
    }

    override fun renderUpdate(timeDelta: Float) {
        mesh.draw()
    }
}