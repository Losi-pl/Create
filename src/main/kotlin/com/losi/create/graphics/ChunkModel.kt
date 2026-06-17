package com.losi.create.graphics

/**A list of all [Mesh]es that go into the construction of the shader*/
class ChunkModel {
    val content: Map<Shader, List<Mesh>>

    internal constructor(content: Map<Shader, List<Mesh>>) { this.content = content }

    /**Draws the model*/
    fun draw() {
        content.values.forEach { list ->
            list.forEach { it.draw() }
        }
    }
}