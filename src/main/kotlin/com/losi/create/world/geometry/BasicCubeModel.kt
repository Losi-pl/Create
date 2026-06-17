package com.losi.create.world.geometry

import com.losi.create.assets.BlockDirection
import org.apache.commons.io.function.IOTriConsumer
import org.joml.Vector2f
import org.joml.Vector3f
import org.joml.Vector3l

/**Used to generate a standard model of a cube*/
@OptIn(ExperimentalUnsignedTypes::class)
class BasicCubeModel: IOTriConsumer<Array<Vector3f>, Array<Vector2f>, UIntArray> {
    /**Specifies the side of the block to be drawn*/
    var direction = BlockDirection.South
    /**The position of the block in the world*/
    lateinit var position: Vector3l

    override fun accept(positions: Array<Vector3f>, uvs: Array<Vector2f>, elements: UIntArray) {
        fun Vector3f.add(vec: Vector3l) = this.add(vec.x.toFloat(), vec.y.toFloat(), vec.z.toFloat())

        when (direction) {
            BlockDirection.South -> {
                positions[0] = Vector3f(0f, 1f, 0f).add(position)
                positions[1] = Vector3f(1f, 1f, 0f).add(position)
                positions[2] = Vector3f(1f, 0f, 0f).add(position)
                positions[3] = Vector3f(0f, 0f, 0f).add(position)
            }
            BlockDirection.North -> {
                positions[0] = Vector3f(1f,  1f, 1f).add(position)
                positions[1] = Vector3f(0f,  1f, 1f).add(position)
                positions[2] = Vector3f(0f,  0f, 1f).add(position)
                positions[3] = Vector3f(1f,  0f, 1f).add(position)
            }
            BlockDirection.East -> {
                positions[0] = Vector3f(1f, 1f, 0f).add(position)
                positions[1] = Vector3f(1f, 1f, 1f).add(position)
                positions[2] = Vector3f(1f, 0f, 1f).add(position)
                positions[3] = Vector3f(1f, 0f, 0f).add(position)
            }
            BlockDirection.West -> {
                positions[0] = Vector3f(0f, 1f, 1f).add(position)
                positions[1] = Vector3f(0f, 1f, 0f).add(position)
                positions[2] = Vector3f(0f, 0f, 0f).add(position)
                positions[3] = Vector3f(0f, 0f, 1f).add(position)
            }
            BlockDirection.Top -> {
                positions[0] = Vector3f(0f, 1f, 1f).add(position)
                positions[1] = Vector3f(1f, 1f, 1f).add(position)
                positions[2] = Vector3f(1f, 1f, 0f).add(position)
                positions[3] = Vector3f(0f, 1f, 0f).add(position)
            }
            BlockDirection.Bottom -> {
                positions[0] = Vector3f(0f, 0f, 0f).add(position)
                positions[1] = Vector3f(1f, 0f, 0f).add(position)
                positions[2] = Vector3f(1f, 0f, 1f).add(position)
                positions[3] = Vector3f(0f, 0f, 1f).add(position)
            }
        }

        uvs[0] = Vector2f(0f, 1f)
        uvs[1] = Vector2f(1f, 1f)
        uvs[2] = Vector2f(1f, 0f)
        uvs[3] = Vector2f(0f, 0f)

        elements[0] = 0u
        elements[1] = 1u
        elements[2] = 3u
        elements[3] = 1u
        elements[4] = 2u
        elements[5] = 3u
    }
}