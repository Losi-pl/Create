package com.losi.create.world.geometry

import com.losi.create.assets.BlockDirection
import org.joml.Vector2f
import org.joml.Vector3f
import org.joml.Vector3l

/**Used to generate a standard model of a cube*/
@OptIn(ExperimentalUnsignedTypes::class)
class BasicCubeModel: AutoModelFill {
    /**Specifies the side of the block to be drawn*/
    var direction = BlockDirection.South
    /**The position of the block in the world*/
    var position: Vector3l
    /**For positions*/
    val tmp = Vector3f()
    /**For UVs*/
    val tmp2 = Vector2f()

    constructor(position: Vector3l) { this.position = position }

    override fun fill(positions: FillModelData.PositionStorage, uvs: FillModelData.UVsStorage, elements: FillModelData.ElementIndexes) {
        fun Vector3f.add(vec: Vector3l) = this.add(vec.x.toFloat(), vec.y.toFloat(), vec.z.toFloat())

        when (direction) {
            BlockDirection.South -> {
                positions[0] = tmp.set(0f, 1f, 0f).add(position)
                positions[1] = tmp.set(1f, 1f, 0f).add(position)
                positions[2] = tmp.set(1f, 0f, 0f).add(position)
                positions[3] = tmp.set(0f, 0f, 0f).add(position)
            }
            BlockDirection.North -> {
                positions[0] = tmp.set(1f,  1f, 1f).add(position)
                positions[1] = tmp.set(0f,  1f, 1f).add(position)
                positions[2] = tmp.set(0f,  0f, 1f).add(position)
                positions[3] = tmp.set(1f,  0f, 1f).add(position)
            }
            BlockDirection.East -> {
                positions[0] = tmp.set(1f, 1f, 0f).add(position)
                positions[1] = tmp.set(1f, 1f, 1f).add(position)
                positions[2] = tmp.set(1f, 0f, 1f).add(position)
                positions[3] = tmp.set(1f, 0f, 0f).add(position)
            }
            BlockDirection.West -> {
                positions[0] = tmp.set(0f, 1f, 1f).add(position)
                positions[1] = tmp.set(0f, 1f, 0f).add(position)
                positions[2] = tmp.set(0f, 0f, 0f).add(position)
                positions[3] = tmp.set(0f, 0f, 1f).add(position)
            }
            BlockDirection.Top -> {
                positions[0] = tmp.set(0f, 1f, 1f).add(position)
                positions[1] = tmp.set(1f, 1f, 1f).add(position)
                positions[2] = tmp.set(1f, 1f, 0f).add(position)
                positions[3] = tmp.set(0f, 1f, 0f).add(position)
            }
            BlockDirection.Bottom -> {
                positions[0] = tmp.set(0f, 0f, 0f).add(position)
                positions[1] = tmp.set(1f, 0f, 0f).add(position)
                positions[2] = tmp.set(1f, 0f, 1f).add(position)
                positions[3] = tmp.set(0f, 0f, 1f).add(position)
            }
        }

        uvs[0] = tmp2.set(0f, 1f)
        uvs[1] = tmp2.set(1f, 1f)
        uvs[2] = tmp2.set(1f, 0f)
        uvs[3] = tmp2.set(0f, 0f)

        elements[0] = 0u
        elements[1] = 1u
        elements[2] = 3u
        elements[3] = 1u
        elements[4] = 2u
        elements[5] = 3u
    }

    override val elementCount: UInt get() = 2u
    override val vertexCount: UInt get() = 4u
}