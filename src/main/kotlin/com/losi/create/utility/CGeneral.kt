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

}