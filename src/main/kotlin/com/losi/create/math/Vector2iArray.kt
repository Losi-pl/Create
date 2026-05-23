@file:Suppress("unused")
package com.losi.create.math

import org.joml.Vector2i
import kotlin.streams.asStream

@OptIn(ExperimentalStdlibApi::class)
@JvmInline @JvmExposeBoxed value class Vector2iArray private constructor(private val elements: IntArray)
{
    companion object {
        private val _empty = Vector2iArray(IntArray(0))
        val empty: Vector2iArray get() = _empty
        private fun IntArray.isAt(ind: Int, vec: Vector2i): Boolean =
            this[ind*2] == vec.x && this[ind*2+1] == vec.y

        @JvmExposeBoxed @JvmStatic fun of(count: Int): Vector2iArray = Vector2iArray(count)
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Vector2i): Vector2iArray {
            val arr = Vector2iArray(elements.size)
            elements.forEachIndexed { index, it -> arr[index] = it }
            return arr
        }
        @JvmExposeBoxed @JvmStatic fun of(vararg elements: Pair<Int, Int>): Vector2iArray {
            val arr = Vector2iArray(elements.size)
            elements.forEachIndexed { index, (x, y) -> arr[index] = Vector2i(x, y) }
            return arr
        }
    }

    val size: Int get() = elements.size / 2

    constructor(count: Int) : this(IntArray(count * 2))
    constructor(vararg elements: Vector2i): this(IntArray(elements.size * 2))
    { elements.forEachIndexed { index, it -> this[index] = it } }
    constructor(vararg elements: Pair<Int, Int>): this(IntArray(elements.size * 2))
    { elements.forEachIndexed { index, (x, y) -> this.elements[index*2]=x; this.elements[index*2+1]=y }}

    operator fun get(index: Int): Vector2i {
        checkIndex(index)
        return Vector2i(elements[index * 2], elements[index * 2 + 1])
    }
    operator fun set(index: Int, value: Vector2i) {
        checkIndex(index)
        elements[index * 2] = value.x
        elements[index * 2 + 1] = value.y
    }

    operator fun iterator(): Vector2iIterator = Vector2iIterator(elements)
    @ConsistentCopyVisibility
    data class Vector2iIterator internal constructor(private val array: IntArray, private var cursor: Int = 0) : Iterator<Vector2i> {
        override fun next(): Vector2i = if (hasNext()) Vector2i(array[cursor++], array[cursor++]) else throw NoSuchElementException()
        override fun hasNext(): Boolean = cursor < array.size
        override fun equals(other: Any?): Boolean {
            if (this === other) return true
            if (javaClass != other?.javaClass) return false

            other as Vector2iIterator

            if (cursor != other.cursor) return false
            if (!array.contentEquals(other.array)) return false

            return true
        }
        override fun hashCode(): Int {
            var result = cursor
            result = 31 * result + array.contentHashCode()
            return result
        }

        fun asStream() = this.asSequence().asStream()
    }
    fun asSequence(): Sequence<Vector2i> {
        if (isEmpty()) return emptySequence()
        return Sequence { this.iterator() }
    }

    fun isEmpty(): Boolean = size == 0
    fun isNotEmpty(): Boolean = !isEmpty()

    fun asList() = List(this)
    @JvmInline value class List internal constructor(private val array: Vector2iArray):  kotlin.collections.List<Vector2i> {
        override fun contains(element: Vector2i): Boolean = array.contains(element)
        override fun containsAll(elements: Collection<Vector2i>) = array.containsAll(elements)
        override fun get(index: Int): Vector2i = array[index]
        override fun indexOf(element: Vector2i): Int = array.indexOf(element)
        override fun isEmpty(): Boolean = array.size == 0
        override fun iterator(): Iterator<Vector2i> = array.iterator()
        override fun lastIndexOf(element: Vector2i): Int = array.lastIndexOf(element)
        override fun listIterator(): kotlin.collections.ListIterator<Vector2i> = ListIterator(array.elements)
        override fun listIterator(index: Int): kotlin.collections.ListIterator<Vector2i> = ListIterator(array.elements, index * 2)
        override fun toString(): String = array.toString()
        override fun subList(fromIndex: Int, toIndex: Int): SpanList {
            val from = fromIndex.coerceIn(0 until array.size)
            val to = toIndex.coerceIn(0 until array.size)
            return SpanList(array, from, to - from)
        }
        override val size: Int get() = array.elements.size / 2
    }

    @ConsistentCopyVisibility
    data class SpanList internal constructor(private val array: Vector2iArray, private val start: Int, private val count: Int): kotlin.collections.List<Vector2i> {
        override fun contains(element: Vector2i): Boolean = indexOf(element) >= 0
        override fun containsAll(elements: Collection<Vector2i>) = array.containsAll(elements, start, start + count)
        override fun get(index: Int): Vector2i { checkIndex(index); return array[index + start] }
        override fun toString(): String = array.toString()
        override fun indexOf(element: Vector2i): Int {
            for (i in start until start + count)
                if(array.elements.isAt(i, element))
                    return i - start
            return -1
        }
        override fun isEmpty(): Boolean = count == 0
        override fun iterator(): Iterator<Vector2i> = iterator {
            for (i in start until start + count)
                yield(array[i])
        }
        override fun lastIndexOf(element: Vector2i): Int {
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
            return SpanList(array, start + from, to - (start + from))
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
        data class ListIterator internal constructor(private val array: Vector2iArray, private val start: Int, private val count: Int, private var index: Int = 0):
                kotlin.collections.ListIterator<Vector2i> {
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
    data class ListIterator internal constructor(private val array: IntArray, private var index: Int = 0): kotlin.collections.ListIterator<Vector2i> {
        fun get(index: Int) = Vector2i(array[index * 2], array[index * 2 + 1])
        override fun next() = get(index++)
        override fun previous() = get(index--)
        override fun hasNext() = index < array.size / 2
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

    fun copyOf() = Vector2iArray(elements.copyOf())
    fun copyOf(newSize: Int): Vector2iArray {
        val result = Vector2iArray(newSize)
        for (i in 0 until minOf(size, newSize)) result[i] = this[i]
        return result
    }

    fun fill(value: Vector2i, fromIndex: Int = 0, toIndex: Int = size) {
        require(fromIndex in 0..size && toIndex in fromIndex..size)
        for (i in fromIndex until toIndex) this[i] = value
    }
    fun filled(value: Vector2i, fromIndex: Int, toIndex: Int): Vector2iArray {
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
    fun reversed(): Vector2iArray {
        val copy = copyOf()
        copy.reverse()
        return copy
    }

    fun indexOf(element: Vector2i): Int {
        for (i in 0 until size) if (elements.isAt(i, element)) return i
        return -1
    }
    fun lastIndexOf(element: Vector2i): Int {
        for (i in size - 1 downTo 0) if (elements.isAt(i, element)) return i
        return -1
    }

    fun contains(element: Vector2i): Boolean = indexOf(element) >= 0
    fun containsAll(elements: Collection<Vector2i>, fromIndex: Int = 0, toIndex: Int = size): Boolean {
        var count = elements.size
        for (i in fromIndex until toIndex)
        {
            val x = this.elements[i*2]; val y = this.elements[i*2+1]
            for(ele in elements)
                if(x == ele.x && y == ele.y)
                    count--
            if(count == 0)
                return true
        }
        return false
    }

    inline fun forEach(action: (Vector2i) -> Unit) {
        for (i in 0 until size) action(get(i))
    }
    inline fun forEachIndexed(action: (index: Int, value: Vector2i) -> Unit) {
        for (i in 0 until size) action(i, get(i))
    }

    fun joinToString(separator: String = ", ", prefix: String = "[", postfix: String = "]"): String =
        joinToString(0, size, separator, prefix, postfix)
    fun joinToString(fromIndex: Int, toIndex: Int, separator: String = ", ", prefix: String = "[", postfix: String = "]"): String {
        val sb = StringBuilder(prefix)
        for (i in fromIndex until toIndex) {
            if (i > 0) sb.append(separator)
            sb.append("(").append(elements[i*2]).append(", ").append(elements[i*2+1]).append(")")
        }
        sb.append(postfix)
        return sb.toString()
    }

    override fun toString(): String = joinToString()
    private fun checkIndex(index: Int) { if (index !in 0..<size) throw IndexOutOfBoundsException("Index $index out of bounds for size $size") }
}