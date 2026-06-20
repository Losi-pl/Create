package com.losi.create.graphics

import org.eclipse.collections.impl.multimap.list.FastListMultimap
import org.joml.Matrix4f

/**A list of all [Mesh]es that go into the construction of the shader*/
class ChunkModel {
    val content: FastListMultimap<Shader, Mesh>

    internal constructor(content: FastListMultimap<Shader, Mesh>) { this.content = content }

    /**Draws the model*/
    fun draw(model: Matrix4f, view: Matrix4f, projection: Matrix4f) {
        content.keysView().forEach { shader ->
            val models = content[shader]

            if (shader.hasModelMatrix)
                shader.setModelMatrix(model)
            if (shader.hasViewMatrix)
                shader.setViewMatrix(view)
            if (shader.hasProjectionMatrix)
                shader.setProjectionMatrix(projection)

            models.forEach { it.draw() }
        }
    }

    /**Ensures that all meshes constructing this model are properly bound to the main thread*/
    fun threadBind() {
        content.valuesView().forEach {
            it.threadBind()
        }
    }
}