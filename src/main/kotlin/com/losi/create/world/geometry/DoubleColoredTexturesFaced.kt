package com.losi.create.world.geometry

import com.losi.create.assets.*
import com.losi.create.graphics.*
import com.losi.create.utility.*
import org.eclipse.collections.impl.list.mutable.primitive.*
import org.lwjgl.system.MemoryStack
import java.awt.Color

class DoubleColoredTexturesFaced: BlockFacet {
    val textureBack: BlockTexture
    val textureFront: BlockTexture
    val colorBack: Color
    val colorFront: Color

    constructor(textureBack: BlockTexture, colorBack: Color, textureFront: BlockTexture, colorFront: Color) {
        this.textureBack = textureBack
        this.colorBack = colorBack
        this.textureFront = textureFront
        this.colorFront = colorFront
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

            val allInd1 = atlasIndexes1
            elem.apply {
                val count = allInd1.size().toUInt()
                indices.forEach {
                    elem[it] += count
                }
            }

            allElements.addAll(elem.buffer)
            allPositions.addAll(pos.buffer)
            allUvs.addAll(uvs.buffer)
            allInd1.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(textureBack.index.toInt())
                }
            }
            atlasIndexes2.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(textureFront.index.toInt())
                }
            }

            textureColors1.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(colorBack.red / 255f)
                    list.add(colorBack.green / 255f)
                    list.add(colorBack.blue / 255f)
                }
            }
            textureColors2.let { list ->
                list.ensureCapacity(list.size() + vertexCount.toInt())
                (0u..<vertexCount).forEach { _ ->
                    list.add(colorFront.red / 255f)
                    list.add(colorFront.green / 255f)
                    list.add(colorFront.blue / 255f)
                }
            }
        }
    }

    companion object: FacetModeler() {
        val shader = lazy { AssetManager.get<Shader>("create:blocks/double-colored-textures").orElse { throw RuntimeException("Required shader not found (create:double-colored-textures)") }}
        private context(_: WorldModeler) val allPositions get() = getObject("positions") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val allUvs get() = getObject("uvs") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val atlasIndexes1 get() = getObject("atlas_1") { IntArrayList.newListWith()!! }
        private context(_: WorldModeler) val atlasIndexes2 get() = getObject("atlas_2") { IntArrayList.newListWith()!! }
        private context(_: WorldModeler) val textureColors1 get() = getObject("colors_1") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val textureColors2 get() = getObject("colors_2") { FloatArrayList.newListWith()!! }
        private context(_: WorldModeler) val allElements get() = getObject("elements") { IntArrayList.newListWith()!! }

        @OptIn(ExperimentalUnsignedTypes::class)
        context(modeler: WorldModeler)
        override fun finish(): Mesh {
            val mesh = Mesh(shader.value)

            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            mesh.setAttribute("uvPos", allUvs.toArray().asVector2Array())
            mesh.setAttribute("pos", allPositions.toArray().asVector3Array())
            mesh.setAttribute("color1", textureColors1.toArray().asVector3Array())
            mesh.setAttribute("color2", textureColors2.toArray().asVector3Array())
            mesh.setAttribute("atlas1", atlasIndexes1.toArray().asUIntArray())
            mesh.setAttribute("atlas2", atlasIndexes2.toArray().asUIntArray())
            mesh.triangles(allElements.toArray().asUIntArray())

            return mesh.apply { burnModel(); flushBuffers(); }
        }
    }
}