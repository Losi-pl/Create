package com.losi.create.graphics

import org.eclipse.collections.impl.multimap.list.FastListMultimap

/**A list of all [Mesh]es that go into the construction of the shader*/
class ChunkModel {
    val content: FastListMultimap<Shader, Mesh>

    internal constructor(content: FastListMultimap<Shader, Mesh>) { this.content = content }

    /**Draws the model*/
    fun draw() {
        content.valuesView().forEach {
            it.draw()
        }
    }

    /**Ensures that all meshes constructing this model are properly bound to the main thread*/
    fun threadBind() {
        content.valuesView().forEach {
            it.threadBind()
        }
    }
}