package com.losi.create.world.geometry

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.utility.*
import org.eclipse.collections.impl.list.mutable.primitive.*
import org.lwjgl.system.MemoryStack
import java.awt.Color

class ColoredTextureFaced: BlockFacet {
    val texture: BlockTexture
    val color: Color

    constructor(texture: BlockTexture, color: Color) {
        this.texture = texture
        this.color = color
    }

    context(modeler: WorldModeler)
    override fun draw(vertexCount: UInt, elementCount: UInt, specifier: FillModelData) {
        val byteCount = vertexCount.toInt() * (/*Position*/3 + /*UV*/2) * /*32-bit*/4 +
                elementCount.toInt() * /*Triangle*/3 * /*32-bit*/4

        @Suppress("DuplicatedCode")
        MemoryStack.create(byteCount).push().use { stack ->
            val uvs = stack.mallocUVs(vertexCount)
            val elem = stack.mallocTriangles(elementCount)
            val pos = stack.mallocPositions(vertexCount)
            specifier.fill(pos, uvs, elem)

            val allInd = atlasIndexes
            elem.apply {
                val count = allInd.size().toUInt()
                indices.forEach {
                    elem[it] += count
                }
            }

            allElements.addAll(elem.buffer)
            allPositions.addAll(pos.buffer)
            allUvs.addAll(uvs.buffer)
            allInd.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(texture.index.toInt())
                }
            }
            textureColors.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(color.red / 255f)
                    list.add(color.green / 255f)
                    list.add(color.blue / 255f)
                }
            }
        }
    }

    companion object: FacetModeler() {
        val shader = lazy { AssetManager.get<Shader>("create:blocks/colored-texture").orElse { throw RuntimeException("Required shader not found (create:colored-texture)") }}
        private context(_: WorldModeler) val allPositions get() = getObject("positions") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val allUvs get() = getObject("uvs") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val atlasIndexes get() = getObject("atlas") { IntArrayList.newListWith()!! }
        private context(_: WorldModeler) val textureColors get() = getObject("colors") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val allElements get() = getObject("elements") { IntArrayList.newListWith()!! }

        @OptIn(ExperimentalUnsignedTypes::class)
        context(modeler: WorldModeler)
        override fun finish(): Mesh {
            val mesh = Mesh(shader.value)

            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            mesh.setAttribute("uvPos", allUvs.toArray().asVector2Array())
            mesh.setAttribute("color", textureColors.toArray().asVector3Array())
            mesh.setAttribute("pos", allPositions.toArray().asVector3Array())
            mesh.setAttribute("atlasInd", atlasIndexes.toArray().asUIntArray())
            mesh.triangles(allElements.toArray().asUIntArray())

            return mesh.apply { burnModel(); flushBuffers(); }
        }
    }
}