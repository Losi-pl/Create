@file:Suppress("unused", "DuplicatedCode")
package com.losi.create.math.collections

import com.google.common.collect.Streams
private typealias Vector2f = org.joml.Vector2f
@OptIn(ExperimentalStdlibApi::class)
@JvmInline @JvmExposeBoxed value class Vector2fArray private constructor(private val elements: FloatArray)
{
    companion object {


        private val _empty = Vector2fArray(FloatArray(0))
        private const val SIZE = 2
        val empty: Vector2fArray get() = _empty

        private fun FloatArray.isAt(ind: Int, vec: Vector2f) = this[ind * SIZE] == vec.x && this[ind * SIZE + 1] == vec.y

        @JvmExposeBoxed @JvmStatic fun of(count: Int): Vector2fArray = Vector2fArray(count)
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Vector2f): Vector2fArray {
            val arr = Vector2fArray(elements.size)
            elements.forEachIndexed { index, it -> arr[index] = it }
            return arr
        }
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Pair<Float, Float>): Vector2fArray {
            val arr = Vector2fArray(elements.size)
            elements.forEachIndexed { index, pair -> arr[index] = pair }
            return arr
        }

        /**Wraps around an existing array changes [array] will reflect in created [Vector2fArray] and vice versa*/
        fun around(array: FloatArray) = Vector2fArray(array)
    }

    val size: Int get() = elements.size / SIZE

    constructor(count: Int) : this(FloatArray(count * SIZE))
    constructor(vararg elements: Vector2f): this(FloatArray(elements.size * SIZE))
    { elements.forEachIndexed { index, it -> this[index] = it } }
    constructor(vararg elements: Pair<Float, Float>): this(FloatArray(elements.size * SIZE))
    { elements.forEachIndexed { index, pair -> this[index] = pair }}

    operator fun get(index: Int): Vector2f {
        checkIndex(index)
        return Vector2f(elements, index * SIZE)
    }
    operator fun set(index: Int, value: Vector2f) {
        checkIndex(index)
        value.get(elements, index * SIZE)
    }
    operator fun set(index: Int, value: Pair<Float, Float>) {
        checkIndex(index)
        elements[index * 2] = value.first
        elements[index * 2 + 1] = value.second
    }

    operator fun iterator(): Iterator = Iterator(this)
    @ConsistentCopyVisibility
    data class Iterator internal constructor(private val array: Vector2fArray, private var cursor: Int = 0):
        kotlin.collections.Iterator<Vector2f> {
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

    fun contentEquals(array: Vector2fArray) = this.elements.contentEquals(array.elements)
    fun contentHashCode() = this.elements.contentHashCode()

    fun asSequence(): Sequence<Vector2f> {
        if (isEmpty()) return emptySequence()
        return Sequence { this.iterator() }
    }
    fun asSequence(reuseObjects: Boolean = true): Sequence<Vector2f> {
        if (!reuseObjects)
            return asSequence()
        return object : Sequence<Vector2f> {
            override fun iterator() = object : kotlin.collections.Iterator<Vector2f> {
                val obj = Vector2f()
                val array = this@Vector2fArray
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
    @JvmInline value class List internal constructor(private val array: Vector2fArray):  kotlin.collections.List<Vector2f> {
        override fun contains(element: Vector2f) = array.contains(element)
        override fun containsAll(elements: Collection<Vector2f>) = array.containsAll(elements)
        override fun get(index: Int) = array[index]
        override fun indexOf(element: Vector2f) = array.indexOf(element)
        override fun isEmpty() = array.isEmpty()
        override fun iterator() = array.iterator()
        override fun lastIndexOf(element: Vector2f) = array.lastIndexOf(element)
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
    data class SpanList internal constructor(private val array: Vector2fArray, private val start: Int, private val count: Int): kotlin.collections.List<Vector2f> {
        override fun contains(element: Vector2f): Boolean = indexOf(element) >= 0
        override fun containsAll(elements: Collection<Vector2f>) = array.containsAll(elements, start, start + count)
        override fun get(index: Int): Vector2f { checkIndex(index); return array[index + start] }
        override fun toString(): String = array.toString()
        override fun indexOf(element: Vector2f): Int {
            for (i in start until start + count)
                if(array.elements.isAt(i, element))
                    return i - start
            return -1
        }
        override fun isEmpty(): Boolean = count == 0
        override fun iterator(): kotlin.collections.Iterator<Vector2f> = iterator {
            for (i in start until start + count)
                yield(array[i])
        }
        override fun lastIndexOf(element: Vector2f): Int {
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
        data class ListIterator internal constructor(private val array: Vector2fArray, private val start: Int, private val count: Int, private var index: Int = 0):
                kotlin.collections.ListIterator<Vector2f> {
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
    data class ListIterator internal constructor(private val array: Vector2fArray, private var index: Int = 0): kotlin.collections.ListIterator<Vector2f> {
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

    fun copyOf() = Vector2fArray(elements.copyOf())
    fun copyOf(newSize: Int): Vector2fArray {
        val result = Vector2fArray(newSize)
        for (i in 0 until minOf(size, newSize)) result[i] = this[i]
        return result
    }

    fun fill(value: Vector2f, fromIndex: Int = 0, toIndex: Int = size) {
        require(fromIndex in 0..size && toIndex in fromIndex..size)
        for (i in fromIndex until toIndex) this[i] = value
    }
    fun filled(value: Vector2f, fromIndex: Int, toIndex: Int): Vector2fArray {
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
    fun reversed(): Vector2fArray {
        val copy = copyOf()
        copy.reverse()
        return copy
    }

    fun indexOf(element: Vector2f): Int {
        for (i in 0 until size) if (elements.isAt(i, element)) return i
        return -1
    }
    fun lastIndexOf(element: Vector2f): Int {
        for (i in size - 1 downTo 0) if (elements.isAt(i, element)) return i
        return -1
    }

    fun contains(element: Vector2f): Boolean = indexOf(element) >= 0
    fun containsAll(elements: Collection<Vector2f>, fromIndex: Int = 0, toIndex: Int = size): Boolean {
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

    inline fun forEach(action: (Vector2f) -> Unit) {
        for (i in 0 until size) action(get(i))
    }
    inline fun forEachIndexed(action: (index: Int, value: Vector2f) -> Unit) {
        for (i in 0 until size) action(i, get(i))
    }

    fun joinToString(separator: String = ", ", prefix: String = "[", postfix: String = "]"): String =
        joinToString(0, size, separator, prefix, postfix)
    fun joinToString(fromIndex: Int, toIndex: Int, separator: String = ", ", prefix: String = "[", postfix: String = "]"): String {
        val sb = StringBuilder(prefix)
        for (i in fromIndex until toIndex) {
            if (i > fromIndex) sb.append(separator)
            sb.append("(").append(elements[i*2]).append(", ").append(elements[i*2+1]).append(")")
        }
        sb.append(postfix)
        return sb.toString()
    }

    override fun toString(): String = joinToString()
    private fun checkIndex(index: Int) { if (index !in 0..<size) throw IndexOutOfBoundsException("Index $index out of bounds for size $size") }
}