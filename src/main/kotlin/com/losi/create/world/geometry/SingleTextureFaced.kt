package com.losi.create.world.geometry

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.math.collections.*
import com.losi.create.utility.*
import org.eclipse.collections.impl.list.mutable.primitive.IntArrayList
import org.eclipse.collections.impl.list.mutable.primitive.FloatArrayList

/**A standard texture type holding only the index of the texture in atlas with no additional modifiers*/
class SingleTextureFaced: BlockFacet {
    val texture: BlockTexture
    constructor(texture: BlockTexture) { this.texture = texture }

    @OptIn(ExperimentalUnsignedTypes::class)
    context(modeler: WorldModeler)
    override fun draw(vertexCount: UInt, elementCount: UInt, specifier: FillModelData) {
        val positions = Vector3fArray(vertexCount.toInt())
        val uvs = Vector2fArray(vertexCount.toInt())
        val elements = UIntArray(elementCount.toInt() * 3)
        specifier.fill(positions, uvs, elements)

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
        allPositions.addAll(positions.asFloatArray())
        allUvs.addAll(uvs.asFloatArray())
    }

    companion object: FacetModeler() {
        val shader = lazy { AssetManager.get<Shader>("create:blocks/single-texture").orElse { throw RuntimeException("Required shader not found (create:single-texture)") }}

        context(_: WorldModeler) val allPositions get() = getObject("positions") { FloatArrayList.newListWith()!! }
        context(_: WorldModeler) val allUvs get() = getObject("uvs") { FloatArrayList.newListWith()!! }
        context(_: WorldModeler) val atlasIndexes get() = getObject("atlas") { IntArrayList.newListWith()!! }
        context(_: WorldModeler) val allElements get() = getObject("elements") { IntArrayList.newListWith()!! }

        context(modeler: WorldModeler)
        @OptIn(ExperimentalUnsignedTypes::class)
        override fun finish(): Mesh {
            val mesh = Mesh(shader.value)

            mesh.setAttribute("pos", allPositions.toArray().asVector3Array())
            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            mesh.setAttribute("uvPos", allUvs.toArray().asVector2Array())
            mesh.setAttribute("atlasInd", atlasIndexes.toArray().asUIntArray())
            mesh.triangles(allElements.toArray().asUIntArray())

            return mesh.apply { burnModel(); flushBuffers(); }
        }
    }
}