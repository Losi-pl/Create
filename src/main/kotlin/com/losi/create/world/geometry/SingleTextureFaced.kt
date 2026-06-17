package com.losi.create.world.geometry

import com.losi.create.assets.AssetManager
import com.losi.create.assets.BlockTexture
import com.losi.create.graphics.BlockFacet
import com.losi.create.graphics.Mesh
import com.losi.create.graphics.Shader
import org.apache.commons.io.function.IOTriConsumer
import org.eclipse.collections.impl.list.mutable.primitive.IntArrayList
import org.joml.*
import com.losi.create.utility.*

/**A standard texture type holding only the index of the texture in atlas with no additional modifiers*/
class SingleTextureFaced: BlockFacet {
    val texture: BlockTexture
    constructor(texture: BlockTexture) { this.texture = texture }

    @OptIn(ExperimentalUnsignedTypes::class)
    context(modeler: WorldModeler)
    override fun draw(vertexCount: UInt, elementCount: UInt, specifier: IOTriConsumer<Array<Vector3f>, Array<Vector2f>, UIntArray>) {
        val positions = Array(vertexCount.toInt()) { Vector3f() }
        val uvs = Array(vertexCount.toInt()) { Vector2f() }
        val elements = UIntArray(elementCount.toInt() * 3)
        specifier.accept(positions, uvs, elements)

        val allInd = atlasIndexes

        elements.apply {
            val count = allInd.size().toUInt()
            indices.forEach {
                elements[it] += count
            }
        }

        allElements.ensureCapacity(allElements.size() + elements.size)
        allElements.addAll(elements.toIntArray())

        allInd.addAll(IntArray(vertexCount.toInt()) { texture.index.toInt() })
        allPositions.addAll(positions)
        allUvs.addAll(uvs)
    }

    companion object: FacetModeler() {
        val shader = lazy { AssetManager.get<Shader>("create:blocks/single-texture").orElse { throw RuntimeException("Required shader not found (create:single-texture)") }}

        context(_: WorldModeler) val allPositions get() = getObject("positions") { mutableListOf<Vector3f>() }
        context(_: WorldModeler) val allUvs get() = getObject("uvs") { mutableListOf<Vector2f>() }
        context(_: WorldModeler) val atlasIndexes get() = getObject("atlas") { IntArrayList.newListWith()!! }
        context(_: WorldModeler) val allElements get() = getObject("elements") { IntArrayList.newListWith()!! }

        context(modeler: WorldModeler)
        @OptIn(ExperimentalUnsignedTypes::class)
        override fun finish(): Mesh {
            val mesh = Mesh(shader.value)

            mesh.setAttribute("pos", allPositions)
            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            mesh.setAttribute("uvPos", allUvs)
            mesh.setAttribute("atlasInd", atlasIndexes.toArray().asUIntArray())
            mesh.triangles(allElements.toArray().asUIntArray())

            return mesh.apply { burnModel(); flushBuffers(); }
        }
    }
}