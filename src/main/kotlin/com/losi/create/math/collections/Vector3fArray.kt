@file:Suppress("unused", "DuplicatedCode")
package com.losi.create.math.collections

import com.google.common.collect.Streams

private typealias Vector3f = org.joml.Vector3f
@OptIn(ExperimentalStdlibApi::class)
@JvmInline @JvmExposeBoxed value class Vector3fArray private constructor(private val elements: FloatArray)
{
    companion object {
        private val _empty = Vector3fArray(FloatArray(0))
        private const val SIZE = 3
        val empty: Vector3fArray get() = _empty

        private fun FloatArray.isAt(ind: Int, vec: Vector3f) = this[ind * SIZE] == vec.x && this[ind * SIZE + 1] == vec.y && this[ind * SIZE + 2] == vec.z

        @JvmExposeBoxed @JvmStatic fun of(count: Int): Vector3fArray = Vector3fArray(count)
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Vector3f): Vector3fArray {
            val arr = Vector3fArray(elements.size)
            elements.forEachIndexed { index, it -> arr[index] = it }
            return arr
        }
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Triple<Float, Float, Float>): Vector3fArray {
            val arr = Vector3fArray(elements.size)
            elements.forEachIndexed { index, pair -> arr[index] = pair }
            return arr
        }

        /**Wraps around an existing array changes [array] will reflect in created [Vector3fArray] and vice versa*/
        fun around(array: FloatArray) = Vector3fArray(array)
    }

    val size: Int get() = elements.size / SIZE

    constructor(count: Int) : this(FloatArray(count * SIZE))
    constructor(vararg elements: Vector3f): this(FloatArray(elements.size * SIZE))
    { elements.forEachIndexed { index, it -> this[index] = it } }
    constructor(vararg elements: Triple<Float, Float, Float>): this(FloatArray(elements.size * SIZE))
    { elements.forEachIndexed { index, pair -> this[index] = pair }}

    operator fun get(index: Int): Vector3f {
        checkIndex(index)
        return Vector3f(elements, index * SIZE)
    }
    operator fun set(index: Int, value: Vector3f) {
        checkIndex(index)
        value.get(elements, index * SIZE)
    }
    operator fun set(index: Int, value: Triple<Float, Float, Float>) {
        checkIndex(index)
        elements[index * SIZE] = value.first
        elements[index * SIZE + 1] = value.second
        elements[index * SIZE + 2] = value.third
    }

    operator fun iterator(): Iterator = Iterator(this)
    @ConsistentCopyVisibility
    data class Iterator internal constructor(private val array: Vector3fArray, private var cursor: Int = 0):
        kotlin.collections.Iterator<Vector3f> {
        override fun next() = if (hasNext()) array[cursor++] else throw NoSuchElementException()
        override fun hasNext() = cursor < array.size
        override fun equals(other: Any?): Boolean {
            if (this === other) return true
            if (javaClass != other?.javaClass) return false

            other as Iterator

            if (cursor != other.cursor) return false
            if (!array.contentEquals(other.array)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = cursor
            result = 31 * result + array.contentHashCode()
            return result
        }

        /**After using this command do not use that instance of [Iterator]*/
        fun asStream() = Streams.stream(this)
    }

    fun contentEquals(array: Vector3fArray) = this.elements.contentEquals(array.elements)
    fun contentHashCode() = this.elements.contentHashCode()

    fun asSequence(): Sequence<Vector3f> {
        if (isEmpty()) return emptySequence()
        return Sequence { this.iterator() }
    }
    fun asSequence(reuseObjects: Boolean = true): Sequence<Vector3f> {
        if (!reuseObjects)
            return asSequence()
        return object : Sequence<Vector3f> {
            override fun iterator() = object : kotlin.collections.Iterator<Vector3f> {
                val obj = Vector3f()
                val array = this@Vector3fArray
                var index = 0

                override fun next() = obj.set(array.elements, index++ * SIZE)
                override fun hasNext() = index < array.size
            }
        }
    }

    fun isEmpty(): Boolean = size == 0
    fun isNotEmpty(): Boolean = !isEmpty()

    /**Returns the underlying array of this object
     *
     * Warning! Modifying that array will affect this object as well*/
    fun asFloatArray() = elements

    fun asList() = List(this)
    @JvmInline value class List internal constructor(private val array: Vector3fArray):  kotlin.collections.List<Vector3f> {
        override fun contains(element: Vector3f) = array.contains(element)
        override fun containsAll(elements: Collection<Vector3f>) = array.containsAll(elements)
        override fun get(index: Int) = array[index]
        override fun indexOf(element: Vector3f) = array.indexOf(element)
        override fun isEmpty() = array.isEmpty()
        override fun iterator() = array.iterator()
        override fun lastIndexOf(element: Vector3f) = array.lastIndexOf(element)
        override fun listIterator() = ListIterator(array)
        override fun listIterator(index: Int) = ListIterator(array, index)
        override fun toString(): String = array.toString()
        override fun subList(fromIndex: Int, toIndex: Int): SpanList {
            val from = fromIndex.coerceIn(0 until array.size)
            val to = toIndex.coerceIn(0 until array.size)
            return SpanList(array, from, to - from)
        }
        override val size: Int get() = array.elements.size / SIZE
    }

    @ConsistentCopyVisibility
    data class SpanList internal constructor(private val array: Vector3fArray, private val start: Int, private val count: Int): kotlin.collections.List<Vector3f> {
        override fun contains(element: Vector3f): Boolean = indexOf(element) >= 0
        override fun containsAll(elements: Collection<Vector3f>) = array.containsAll(elements, start, start + count)
        override fun get(index: Int): Vector3f { checkIndex(index); return array[index + start] }
        override fun toString(): String = array.toString()
        override fun indexOf(element: Vector3f): Int {
            for (i in start until start + count)
                if(array.elements.isAt(i, element))
                    return i - start
            return -1
        }
        override fun isEmpty(): Boolean = count == 0
        override fun iterator(): kotlin.collections.Iterator<Vector3f> = iterator {
            for (i in start until start + count)
                yield(array[i])
        }
        override fun lastIndexOf(element: Vector3f): Int {
            for (i in start + count - 1 downTo start)
                if(array.elements.isAt(i, element))
                    return i - start
            return -1
        }
        override fun listIterator() = ListIterator(array, start, count)
        override fun listIterator(index: Int) = ListIterator(array, start, count, index)
        override fun subList(fromIndex: Int, toIndex: Int): SpanList {
            val from = fromIndex.coerceIn(0 until count)
            val to = toIndex.coerceIn(0 until count)
            return SpanList(array, start + from, to - from)
        }
        override val size: Int get() = count

        private fun checkIndex(index: Int) { if (index !in 0..<count) throw IndexOutOfBoundsException("Index $index out of bounds for size $size") }
        override fun equals(other: Any?): Boolean {
            if (this === other) return true
            if (javaClass != other?.javaClass) return false

            other as SpanList

            if (start != other.start) return false
            if (count != other.count) return false
            if (!array.elements.contentEquals(other.array.elements)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = start
            result = 31 * result + count
            result = 31 * result + array.elements.contentHashCode()
            return result
        }

        @ConsistentCopyVisibility
        data class ListIterator internal constructor(private val array: Vector3fArray, private val start: Int, private val count: Int, private var index: Int = 0):
                kotlin.collections.ListIterator<Vector3f> {
            fun get(index: Int) = array[index + start]
            override fun next() = get(index++)
            override fun previous() = get(index--)
            override fun hasNext() = index < count
            override fun hasPrevious() = index > 0
            override fun nextIndex() = index
            override fun previousIndex() = index - 1
        }
    }

    @ConsistentCopyVisibility
    data class ListIterator internal constructor(private val array: Vector3fArray, private var index: Int = 0): kotlin.collections.ListIterator<Vector3f> {
        fun get(index: Int) = array[index]
        override fun next() = get(index++)
        override fun previous() = get(index--)
        override fun hasNext() = index < array.size
        override fun hasPrevious() = index > 0
        override fun nextIndex() = index
        override fun previousIndex() = index - 1
        override fun equals(other: Any?): Boolean {
            if (this === other) return true
            if (javaClass != other?.javaClass) return false

            other as ListIterator

            if (index != other.index) return false
            if (!array.contentEquals(other.array)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = index
            result = 31 * result + array.contentHashCode()
            return result
        }
    }

    fun copyOf() = Vector3fArray(elements.copyOf())
    fun copyOf(newSize: Int): Vector3fArray {
        val result = Vector3fArray(newSize)
        for (i in 0 until minOf(size, newSize)) result[i] = this[i]
        return result
    }

    fun fill(value: Vector3f, fromIndex: Int = 0, toIndex: Int = size) {
        require(fromIndex in 0..size && toIndex in fromIndex..size)
        for (i in fromIndex until toIndex) this[i] = value
    }
    fun filled(value: Vector3f, fromIndex: Int, toIndex: Int): Vector3fArray {
        val copy = copyOf()
        copy.fill(value, fromIndex, toIndex)
        return copy
    }

    fun reverse() {
        var left = 0
        var right = size - 1
        while (left < right) {
            val temp = this[left]
            this[left] = this[right]
            this[right] = temp
            left++
            right--
        }
    }
    fun reversed(): Vector3fArray {
        val copy = copyOf()
        copy.reverse()
        return copy
    }

    fun indexOf(element: Vector3f): Int {
        for (i in 0 until size) if (elements.isAt(i, element)) return i
        return -1
    }
    fun lastIndexOf(element: Vector3f): Int {
        for (i in size - 1 downTo 0) if (elements.isAt(i, element)) return i
        return -1
    }

    fun contains(element: Vector3f): Boolean = indexOf(element) >= 0
    fun containsAll(elements: Collection<Vector3f>, fromIndex: Int = 0, toIndex: Int = size): Boolean {
        val count = BooleanArray(elements.size)
        for (i in fromIndex until toIndex)
        {
            elements.forEachIndexed { index, ele ->
                if(this.elements.isAt(i, ele))
                    count[index] = true
            }
            if(count.all { it })
                return true
        }
        return false
    }

    inline fun forEach(action: (Vector3f) -> Unit) {
        for (i in 0 until size) action(get(i))
    }
    inline fun forEachIndexed(action: (index: Int, value: Vector3f) -> Unit) {
        for (i in 0 until size) action(i, get(i))
    }

    fun joinToString(separator: String = ", ", prefix: String = "[", postfix: String = "]"): String =
        joinToString(0, size, separator, prefix, postfix)
    fun joinToString(fromIndex: Int, toIndex: Int, separator: String = ", ", prefix: String = "[", postfix: String = "]"): String {
        val sb = StringBuilder(prefix)
        for (i in fromIndex until toIndex) {
            if (i > fromIndex) sb.append(separator)
            sb.append("(").append(elements[i*SIZE]).append(", ").append(elements[i*SIZE+1]).append(", ").append(elements[i*SIZE+2]).append(")")
        }
        sb.append(postfix)
        return sb.toString()
    }

    override fun toString(): String = joinToString()
    private fun checkIndex(index: Int) { if (index !in 0..<size) throw IndexOutOfBoundsException("Index $index out of bounds for size $size") }
}