package com.losi.create.assets

import com.losi.create.utility.joml.Vector3l
import org.joml.Vector3L
import org.joml.Vector3Lc
import org.joml.Vector3l
import java.nio.ByteBuffer
import java.nio.LongBuffer
import java.text.NumberFormat

enum class BlockDirection(val offset: DirectionVector, private val lazyInverse: Lazy<BlockDirection>) {
    /**`Y++`*/
    Top(DirectionVector(0, +1, 0), lazy { Bottom }),
    /**`Y--`*/
    Bottom(DirectionVector(0, -1, 0), lazy { Top }),
    /**`Z++`*/
    North(DirectionVector(0, 0, +1), lazy { South }),
    /**`X++`*/
    East(DirectionVector(+1, 0, 0), lazy { West }),
    /**`Z--`*/
    South(DirectionVector(0, 0, -1), lazy { North }),
    /**`X--`*/
    West(DirectionVector(-1, 0, 0), lazy { East }),
    ;

    val inverse get() = lazyInverse.value

    class DirectionVector: Vector3Lc {
        private val vec: Vector3l

        internal constructor(x: Long, y: Long, z: Long) {
            vec = Vector3l(x, y, z)
        }

        val x get() = vec.x
        val y get() = vec.y
        val z get() = vec.z

        override fun x() = vec.x
        override fun y() = vec.y
        override fun z() = vec.z
        override fun get(component: Int) = vec[component]

        override fun get(buffer: LongBuffer): LongBuffer =
            buffer.put(x).put(y).put(z)

        override fun get(index: Int, buffer: LongBuffer): LongBuffer =
            buffer.put(index, x).put(index + 1, y).put(index + 2, z)

        override fun get(buffer: ByteBuffer): ByteBuffer {
            buffer.asLongBuffer().put(x).put(y).put(z)
            return buffer
        }

        override fun get(index: Int, buffer: ByteBuffer): ByteBuffer {
            buffer.putLong(index + Long.SIZE_BYTES,   x)
                .putLong(index + Long.SIZE_BYTES * 2, y)
                .putLong(index + Long.SIZE_BYTES * 3, z)
            return buffer
        }

        override fun getToAddress(address: Long): DirectionVector {
            vec.getToAddress(address)
            return this
        }

        override fun sub(v: Vector3Lc, dest: Vector3L) = vec.sub(v, dest)
        override fun sub(x: Long, y: Long, z: Long, dest: Vector3L) = vec.sub(x, y, z, dest)

        override fun add(v: Vector3Lc, dest: Vector3L) = vec.add(v, dest)
        override fun add(x: Long, y: Long, z: Long, dest: Vector3L) = vec.add(x, y, z, dest)

        override fun mul(scalar: Long, dest: Vector3L) = vec.mul(scalar, dest)
        override fun mul(v: Vector3Lc, dest: Vector3L) = vec.mul(v, dest)
        override fun mul(x: Long, y: Long, z: Long, dest: Vector3L) = vec.mul(x, y, z, dest)

        override fun div(scalar: Float, dest: Vector3L) = vec.div(scalar, dest)
        override fun div(scalar: Long, dest: Vector3L) = vec.div(scalar, dest)

        override fun lengthSquared() = vec.lengthSquared()
        override fun length() = vec.length()

        override fun distance(v: Vector3Lc) = vec.distance(v)
        override fun distance(x: Long, y: Long, z: Long) = vec.distance(x, y, z)

        override fun gridDistance(v: Vector3Lc) = vec.gridDistance(v)
        override fun gridDistance(x: Long, y: Long, z: Long) = vec.gridDistance(x, y, z)

        override fun distanceSquared(v: Vector3Lc) = vec.distanceSquared(v)
        override fun distanceSquared(x: Long, y: Long, z: Long) = vec.distanceSquared(x, y, z)

        override fun toString(formatter: NumberFormat): String {
            if(x != 0L)
                return "(X${if(x > 0) "++" else "--"})"
            if(y != 0L)
                return "(Y${if(y > 0) "++" else "--"})"
            if(z != 0L)
                return "(Y${if(z > 0) "++" else "--"})"
            return "(ERROR)"
        }

        override fun negate(dest: Vector3L) = vec.negate(dest)

        override fun min(v: Vector3Lc, dest: Vector3L) = vec.min(v, dest)
        override fun max(v: Vector3Lc, dest: Vector3L) = vec.max(v, dest)

        override fun maxComponent() = vec.maxComponent()
        override fun minComponent()= vec.minComponent()

        override fun absolute(dest: Vector3L) = vec.absolute()

        override fun equals(x: Long, y: Long, z: Long) = vec.equals(x, y, z)
    }
}