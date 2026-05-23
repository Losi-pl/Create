@file:Suppress("unused")
package com.losi.create.utility

import org.w3c.dom.Node
import org.w3c.dom.NodeList


inline fun NodeList.forEach(action: (node: Node) -> Unit) {
    for (i in 0 until this.length) action(this.item(i))
}
fun Node.getAttribute(name: String): String? = this.attributes.getNamedItem(name).nodeValue
fun NodeList.first(): Node = this.item(0)
fun NodeList.last(): Node = this.item(this.length - 1)
fun <T> T?.orElse(default: T): T = this ?: default
inline fun <T> T?.orElse(default: () -> T): T = this ?: default()
fun Int?.orElse(els: Int) : Int {
    if(this == null)
        return els
    return this
}
inline fun <T: Comparable<T>> List<T>.assertAllEqual(negative: () -> T): T {
    if(this.isEmpty())
        return negative()
    val c: T = this[0]
    this.forEach { if(it.compareTo(c) != 0) return it }
    return negative()
}
inline fun <T: Comparable<T>> Array<T>.assertAllEqual(negative: () -> T): T {
    if(this.isEmpty())
        return negative()
    val c: T = this[0]
    this.forEach { if(it.compareTo(c) != 0) return it }
    return negative()
}
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
fun String.startsWithFrom(subString: String, fromIndex: Int): Boolean {
    if(this.length - subString.length < 0)
        return false
    subString.forEachIndexed { i, c ->
        if(this[i + fromIndex] != c)
            return false
    }
    return true
}