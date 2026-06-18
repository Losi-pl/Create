package com.losi.create.world.geometry

import com.losi.create.math.collections.*
import org.joml.*
import org.lwjgl.system.MemoryStack
import java.nio.*

/**Used to fill the model data used in [BlockFacet.draw()][com.losi.create.graphics.BlockFacet.draw]*/
fun interface FillModelData {
    @OptIn(ExperimentalUnsignedTypes::class)
    fun fill(positions: PositionStorage, uvs: UVsStorage, elements: ElementIndexes)

    @JvmInline @Suppress("unused")
    value class PositionStorage(val buffer: FloatBuffer) {
        constructor(size: Int, stack: MemoryStack): this(stack.mallocFloat(size * 3))

        operator fun set(index: Int, value: Vector3f) {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            buffer.putVector3f(index * 3, value)
        }
        operator fun get(index: Int): Vector3f {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            return buffer.getVector3f(index * 3)
        }
        fun get(index: Int, dest: Vector3f): Vector3f {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            return buffer.getVector3f(index * 3, dest)
        }
        operator fun iterator(): Iterator<Vector3f> = object : Iterator<Vector3f> {
            private var index = 0
            override fun hasNext(): Boolean = index < capacity
            override fun next(): Vector3f {
                if (!hasNext()) throw NoSuchElementException()
                return this@PositionStorage[index++]
            }
        }
        fun iterator(reuseObj: Boolean): Iterator<Vector3f> = if(reuseObj) object : Iterator<Vector3f> {
            private var index = 0
            private val temp = Vector3f()
            override fun hasNext(): Boolean = index < capacity
            override fun next(): Vector3f {
                if (!hasNext()) throw NoSuchElementException()
                temp.get(index * 3, buffer)
                return temp
            }
        } else iterator()

        fun put(value: Vector3f): PositionStorage { buffer.putVector3f(value); return this }
        fun rewind(): PositionStorage { buffer.rewind(); return this }
        fun clear(): PositionStorage { buffer.clear(); return this }
        fun flip(): PositionStorage { buffer.flip(); return this }

        val capacity: Int get() = buffer.capacity() / 3

        fun toArray() = Vector3fArray.around(FloatArray(capacity).apply { buffer.get(this) })
    }

    @JvmInline @Suppress("unused")
    value class UVsStorage(val buffer: FloatBuffer) {
        constructor(size: Int, stack: MemoryStack): this(stack.mallocFloat(size * 2))

        operator fun set(index: Int, value: Vector2f) {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            buffer.putVector2f(index * 2, value)
        }
        operator fun get(index: Int): Vector2f {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            return buffer.getVector2f(index * 2)
        }
        fun get(index: Int, dest: Vector3f): Vector3f {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            return buffer.getVector3f(index * 2, dest)
        }
        operator fun iterator(): Iterator<Vector2f> = object : Iterator<Vector2f> {
            private var index = 0
            override fun hasNext(): Boolean = index < capacity
            override fun next(): Vector2f {
                if (!hasNext()) throw NoSuchElementException()
                return this@UVsStorage[index++]
            }
        }
        fun iterator(reuseObj: Boolean): Iterator<Vector2f> = if(reuseObj) object : Iterator<Vector2f> {
            private var index = 0
            private val temp = Vector2f()
            override fun hasNext(): Boolean = index < capacity
            override fun next(): Vector2f {
                if (!hasNext()) throw NoSuchElementException()
                temp.get(index * 2, buffer)
                return temp
            }
        } else iterator()

        fun put(value: Vector2f): UVsStorage { buffer.putVector2f(value); return this }
        fun rewind(): UVsStorage { buffer.rewind(); return this }
        fun clear(): UVsStorage { buffer.clear(); return this }
        fun flip(): UVsStorage { buffer.flip(); return this }

        val capacity: Int get() = buffer.capacity() / 2

        fun toArray() = Vector2fArray.around(FloatArray(capacity).apply { buffer.get(this) })
    }

    @JvmInline
    value class ElementIndexes(val buffer: IntBuffer) {
        constructor(size: Int, stack: MemoryStack): this(stack.mallocInt(size))

        operator fun set(index: Int, value: UInt) {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            buffer.put(index, value.toInt())
        }
        operator fun get(index: Int): UInt {
            require(index in 0..capacity) { "Index $index out of bounds, capacity: $capacity" }
            return buffer.get(index).toUInt()
        }
        operator fun iterator(): IntIterator = object : IntIterator() {
            private var index = 0
            override fun hasNext(): Boolean = index < capacity
            override fun nextInt(): Int {
                if (!hasNext()) throw NoSuchElementException()
                return this@ElementIndexes[index++].toInt()
            }
        }

        val indices: IntRange get() = 0..<capacity

        val capacity: Int get() = buffer.capacity()
    }
}