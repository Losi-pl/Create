package com.losi.create.world.geometry

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.utility.*
import org.eclipse.collections.impl.list.mutable.primitive.*
import org.lwjgl.system.MemoryStack

/**A standard texture type holding only the index of the texture in atlas with no additional modifiers*/
class SingleTextureFaced: BlockFacet {
    val texture: BlockTexture
    constructor(texture: BlockTexture) { this.texture = texture }

    @OptIn(ExperimentalUnsignedTypes::class)
    context(modeler: WorldModeler)
    override fun draw(vertexCount: UInt, elementCount: UInt, specifier: FillModelData) {
        val memNeeded = vertexCount.toInt() * (UInt.SIZE_BYTES * (3 + 2 + 1)) + elementCount.toInt() * 3 * 4
        MemoryStack.create(memNeeded).push().use { stack ->
            val positions = stack.mallocPositions(vertexCount)
            val uvs = stack.mallocUVs(vertexCount)
            val elements = stack.mallocTriangles(elementCount)
            specifier.fill(positions, uvs, elements)

            val allInd = atlasIndexes
            elements.apply {
                val count = allInd.size().toUInt()
                indices.forEach {
                    elements[it] += count
                }
            }

            allElements.addAll(elements.buffer)
            allPositions.addAll(positions.buffer)
            allUvs.addAll(uvs.buffer)
            allInd.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(texture.index.toInt())
                }
            }
        }
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

            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            mesh.setAttribute("uvPos", allUvs.toArray().asVector2Array())
            mesh.setAttribute("pos", allPositions.toArray().asVector3Array())
            mesh.setAttribute("atlasInd", atlasIndexes.toArray().asUIntArray())
            mesh.triangles(allElements.toArray().asUIntArray())

            return mesh.apply { burnModel(); flushBuffers(); }
        }
    }
}