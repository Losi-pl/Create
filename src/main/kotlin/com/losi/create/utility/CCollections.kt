@file:JvmName("CCollections")
@file:Suppress("unused")
package com.losi.create.utility

import java.util.Collections

/**Creates a reflection of a list that is read only but still reflects changes to the original*/
fun <T> List<T>.calcify(): List<T> = Collections.unmodifiableList(this)

/**Creates a reflection of a map that is read only but still reflects changes to the original*/
fun <T> Map<String, T>.calcify(): Map<String, T> = Collections.unmodifiableMap(this)

/**A variation of [chunked] but reusing the same list to avoid extra allocation*/
fun <T> Sequence<T>.chunkedReuse(size: Int) = sequence {
    require(size > 0) { "Chunk size must be positive" }
    val buffer = mutableListOf<T>()
    var i = 0
    for (item in this@chunkedReuse) {
        if (i == size) { yield(buffer); i = 0 }

        if(buffer.size == i)
            buffer.add(item)
        else buffer[i] = item
        ++i
    }
    if (i > 0) {
        (i until size).forEach { _ -> buffer.removeLast() }
        yield(buffer)
    }
}

/**Finds and returns the first element in the [Map] that meats the [condition], if nothing is found will return `null`*/
inline fun <K, F> Map<K, F>.findFirst(condition: (Map.Entry<K, F>) -> Boolean): Map.Entry<K, F>? {
    this.forEach {
        if(condition(it))
            return it
    }
    return null
}

/**Goes through the list ensuring that all elements in it are equal to each other
 * @return First value of the list, if not all values are equal returns [negative]*/
inline fun <T: Comparable<T>> List<T>.assertAllEqual(negative: () -> T): T {
    if(this.isEmpty())
        return negative()
    val c: T = this[0]
    this.forEach { if(it.compareTo(c) != 0) return it }
    return negative()
}

/**Goes through the array ensuring that all elements in it are equal to each other
 * @return First value of the array, if not all values are equal returns [negative]*/
inline fun <T: Comparable<T>> Array<T>.assertAllEqual(negative: () -> T): T {
    if(this.isEmpty())
        return negative()
    val c: T = this[0]
    this.forEach { if(it.compareTo(c) != 0) return it }
    return negative()
}

/**Goes through the sequence ensuring that all elements in it are equal to each other
 * @return First value of the sequence, if not all values are equal returns [negative]*/
inline fun <T: Comparable<T>> Sequence<T>.assertAllEqual(negative: () -> T): T {
    val it = this.iterator()
    if(!it.hasNext())
        return negative()
    val c: T = it.next()
    while (it.hasNext())
        if(c.compareTo(it.next()) != 0)
            return negative()
    return c
}

/**Performs the given [action] on each element.
 *
 * The operation is _terminal_.*/
inline fun <T> java.util.Enumeration<T>.forEach(action: (T) -> Unit) {
    while (this.hasMoreElements())
        action(this.nextElement())
}

/**Turns a [Sequence] into a [MutableMap]*/
fun <K, V> Sequence<Pair<K, V>>.toMutableMap(): MutableMap<K, V> {
    val map = mutableMapOf<K, V>()
    this.forEach { (key, value) -> map[key] = value }
    return map
}

fun <T> Sequence<T>.exclude(coll: Collection<T>) = sequence {
    this@exclude.forEach {
        if(!coll.contains(it))
            yield(it)
    }
}