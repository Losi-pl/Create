@file:Suppress("unused", "DuplicatedCode")
package com.losi.create.math.collections

import kotlin.experimental.and
import kotlin.streams.asStream

private typealias Vector3b = com.losi.create.math.Vector3b
@OptIn(ExperimentalStdlibApi::class)
@JvmInline @JvmExposeBoxed value class Vector3bArray private constructor(private val elements: ByteArray)
{
    companion object {
        private const val MASK: Byte = 0b00000111
        private val _empty = Vector3bArray(ByteArray(0))
        val empty: Vector3bArray get() = _empty

        private fun ByteArray.isAt(index: Int, value: Vector3b) = (this[index] and MASK) == (value.composite and MASK)
        private fun ByteArray.myContentEquals(array: ByteArray): Boolean {
            if(this === array)
                return true
            this.forEachIndexed { index, b ->
                if((b and MASK) != (array[index] and MASK))
                    return false
            }

            return false
        }

        @JvmExposeBoxed @JvmStatic fun of(count: Int): Vector3bArray = Vector3bArray(count)
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Vector3b): Vector3bArray {
            val arr = Vector3bArray(elements.size)
            elements.forEachIndexed { index, it -> arr[index] = it }
            return arr
        }
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Triple<Boolean, Boolean, Boolean>): Vector3bArray {
            val arr = Vector3bArray(elements.size)
            elements.forEachIndexed { index, (x, y, z) -> arr.elements[index] = Vector3b.asComposite(x, y, z) }
            return arr
        }
    }

    val size: Int get() = elements.size

    constructor(count: Int) : this(ByteArray(count))
    constructor(vararg elements: Vector3b): this(ByteArray(elements.size))
    { elements.forEachIndexed { index, it -> this[index] = it } }
    constructor(vararg elements: Triple<Boolean, Boolean, Boolean>): this(ByteArray(elements.size ))
    { elements.forEachIndexed { index, (x, y, z) -> this.elements[index] = Vector3b.asComposite(x, y, z) }}

    operator fun get(index: Int): Vector3b {
        checkIndex(index)
        return Vector3b(elements[index])
    }
    operator fun set(index: Int, value: Vector3b) {
        checkIndex(index)
        elements[index] = value.composite
    }

    operator fun iterator() = Iterator(elements)
    @ConsistentCopyVisibility
    data class Iterator internal constructor(private val array: ByteArray, private var cursor: Int = 0) : kotlin.collections.Iterator<Vector3b> {
        override fun next(): Vector3b = if (hasNext()) Vector3b(array[cursor++]) else throw NoSuchElementException()
        override fun hasNext(): Boolean = cursor < array.size
        override fun equals(other: Any?): Boolean {
            if (this === other) return true
            if (javaClass != other?.javaClass) return false

            other as Iterator

            if (cursor != other.cursor) return false
            if (!array.myContentEquals(other.array)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = cursor
            result = 31 * result + array.contentHashCode()
            return result
        }

        fun asStream() = this.asSequence().asStream()
    }
    fun asSequence(): Sequence<Vector3b> {
        if (isEmpty()) return emptySequence()
        return Sequence { this.iterator() }
    }

    fun isEmpty(): Boolean = size == 0
    fun isNotEmpty(): Boolean = !isEmpty()

    fun asList() = List(this)
    @JvmInline value class List internal constructor(private val array: Vector3bArray):  kotlin.collections.List<Vector3b> {
        override fun contains(element: Vector3b) = array.contains(element)
        override fun containsAll(elements: Collection<Vector3b>) = array.containsAll(elements)
        override fun get(index: Int) = array[index]
        override fun indexOf(element: Vector3b) = array.indexOf(element)
        override fun isEmpty() = array.size == 0
        override fun iterator() = array.iterator()
        override fun lastIndexOf(element: Vector3b) = array.lastIndexOf(element)
        override fun listIterator() = ListIterator(array.elements)
        override fun listIterator(index: Int) = ListIterator(array.elements, index)
        override fun toString(): String = array.toString()
        override fun subList(fromIndex: Int, toIndex: Int): SpanList {
            val from = fromIndex.coerceIn(0 until array.size)
            val to = toIndex.coerceIn(0 until array.size)
            return SpanList(array, from, to - from)
        }
        override val size: Int get() = array.elements.size
    }

    @ConsistentCopyVisibility
    data class SpanList internal constructor(private val array: Vector3bArray, private val start: Int, private val count: Int): kotlin.collections.List<Vector3b> {
        override fun contains(element: Vector3b): Boolean = indexOf(element) >= 0
        override fun containsAll(elements: Collection<Vector3b>) = array.containsAll(elements, start, start + count)
        override fun get(index: Int): Vector3b { checkIndex(index); return array[index + start] }
        override fun toString(): String = array.toString()
        override fun indexOf(element: Vector3b): Int {
            for (i in start until start + count)
                if(array.elements.isAt(i, element))
                    return i - start
            return -1
        }
        override fun isEmpty(): Boolean = count == 0
        override fun iterator(): kotlin.collections.Iterator<Vector3b> = iterator {
            for (i in start until start + count)
                yield(array[i])
        }
        override fun lastIndexOf(element: Vector3b): Int {
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
            if (!array.elements.myContentEquals(other.array.elements)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = start
            result = 31 * result + count
            result = 31 * result + array.elements.contentHashCode()
            return result
        }

        @ConsistentCopyVisibility
        data class ListIterator internal constructor(private val array: Vector3bArray, private val start: Int, private val count: Int, private var index: Int = 0):
                kotlin.collections.ListIterator<Vector3b> {
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
    data class ListIterator internal constructor(private val array: ByteArray, private var index: Int = 0): kotlin.collections.ListIterator<Vector3b> {
        fun get(index: Int) = Vector3b(array[index])
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
            if (!array.myContentEquals(other.array)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = index
            result = 31 * result + array.contentHashCode()
            return result
        }
    }

    fun copyOf() = Vector3bArray(elements.copyOf())
    fun copyOf(newSize: Int): Vector3bArray {
        val result = Vector3bArray(newSize)
        for (i in 0 until minOf(size, newSize)) result.elements[i] = this.elements[i]
        return result
    }

    fun fill(value: Vector3b, fromIndex: Int = 0, toIndex: Int = size) {
        require(fromIndex in 0..size && toIndex in fromIndex..size)
        for (i in fromIndex until toIndex) this.elements[i] = value.composite
    }
    fun filled(value: Vector3b, fromIndex: Int, toIndex: Int): Vector3bArray {
        val copy = copyOf()
        copy.fill(value, fromIndex, toIndex)
        return copy
    }

    fun reverse() {
        var left = 0
        var right = size - 1
        while (left < right) {
            val temp = get(left)
            set(left, get(right))
            set(right, temp)
            left++
            right--
        }
    }
    fun reversed(): Vector3bArray {
        val copy = copyOf()
        copy.reverse()
        return copy
    }

    fun indexOf(element: Vector3b): Int {
        for (i in 0 until size) if (elements.isAt(i, element)) return i
        return -1
    }
    fun lastIndexOf(element: Vector3b): Int {
        for (i in size - 1 downTo 0) if (elements.isAt(i, element)) return i
        return -1
    }

    fun contains(element: Vector3b): Boolean = indexOf(element) >= 0
    fun containsAll(elements: Collection<Vector3b>, fromIndex: Int = 0, toIndex: Int = size): Boolean {
        val count = BooleanArray(elements.size)
        for (i in fromIndex until toIndex)
        {
            elements.forEachIndexed { index, ele ->
                if(this.elements.isAt(index, ele))
                    count[index] = true
            }
            if(count.all { it })
                return true
        }
        return false
    }

    inline fun forEach(action: (Vector3b) -> Unit) {
        for (i in 0 until size) action(this[i])
    }
    inline fun forEachIndexed(action: (index: Int, value: Vector3b) -> Unit) {
        for (i in 0 until size) action(i, this[i])
    }

    fun joinToString(separator: String = ", ", prefix: String = "[", postfix: String = "]"): String =
        joinToString(0, size, separator, prefix, postfix)
    fun joinToString(fromIndex: Int, toIndex: Int, separator: String = ", ", prefix: String = "[", postfix: String = "]"): String {
        val sb = StringBuilder(prefix)
        for (i in fromIndex until toIndex) {
            if (i > fromIndex) sb.append(separator)
            val v2 = this[i]
            sb.append("(").append(v2.x).append(", ").append(v2.y).append(")")
        }
        sb.append(postfix)
        return sb.toString()
    }

    override fun toString(): String = joinToString()
    private fun checkIndex(index: Int) { if (index !in 0..<size) throw IndexOutOfBoundsException("Index $index out of bounds for size $size") }
}