@file:Suppress("unused")
package com.losi.create.utility

import org.w3c.dom.Node
import org.w3c.dom.NodeList

/**Performs the given action on each element.
 *
 * The operation is terminal.*/
inline fun NodeList.forEach(action: (node: Node) -> Unit) {
    for (i in 0 until this.length) action(this.item(i))
}
/**A quick way to retrieve an attribute value from a [Node]
 * @throws org.w3c.dom.DOMException DOMSTRING_SIZE_ERR: Raised when it would return more characters than fit in a DOMString variable on the implementation platform.*/
fun Node.getAttribute(name: String): String? = this.attributes.getNamedItem(name).nodeValue
/**Returns a first item of a [NodeList]
 * @throws NullPointerException If the list is empty*/
fun NodeList.first(): Node = this.item(0)
/**Returns a last item of a [NodeList]
 * @throws NullPointerException If the list is empty*/
fun NodeList.last(): Node = this.item(this.length - 1)
/**If [this] value is `null` will return the [default]*/
fun <T> T?.orElse(default: T): T = this ?: default
/**If [this] value is `null` will return the lazy defined [default]*/
inline fun <T> T?.orElse(default: () -> T): T = this ?: default()
/**If [this] value is `null` will return the [default]*/
fun Int?.orElse(default: Int) : Int {
    if(this == null)
        return default
    return this
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
        for(j in i until size)
            buffer.removeLast()
        yield(buffer)
    }
}
/**A variation of [startsWith] but starting [fromIndex] instead of the start of the string*/
fun String.startsWithFrom(subString: String, fromIndex: Int): Boolean {
    if(this.length - subString.length < 0)
        return false
    subString.forEachIndexed { i, c ->
        if(this[i + fromIndex] != c)
            return false
    }
    return true
}
/**Creates an 64-bit hash intend of the standard 32-bit
 *
 * It is created from hashes of values in the [Pair]*/
fun <T, R> Pair<T, R>.longHashCode() =
    ((this.first.hashCode().toLong() and 0xFFFFFFFFL) or (this.second.hashCode().toLong() shl 32))
/**Finds and returns the first element in the [Map] that meats the [condition], if nothing is found will return `null`*/
inline fun <K, F> Map<K, F>.findFirst(condition: (Map.Entry<K, F>) -> Boolean): Map.Entry<K, F>? {
    this.forEach {
        if(condition(it))
            return it
    }
    return null
}
fun Boolean.toInt() = if(this) 1 else 0