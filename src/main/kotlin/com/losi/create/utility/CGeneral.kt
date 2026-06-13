@file:Suppress("unused")
package com.losi.create.utility

import org.w3c.dom.Node
import org.w3c.dom.NodeList
import kotlin.contracts.ExperimentalContracts
import kotlin.contracts.InvocationKind
import kotlin.contracts.contract

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
/**An overload of [run] the is combined with an assertion that the value it's used on is not null*/
inline fun <T> T?.mustRun(action: T.() -> Unit) {
    assert(this != null) { "The element was supposed to be set" }
    this!!.action()
}

private val camelCaseRegex = "(?<=[a-z])(?=[A-Z])".toRegex()
/**This method is meant for splitting a single word into a sentence along all upper case characters
 *
 * `ItsAnSentence` -> `Its An Sentence`*/
fun String.splitCamelCase()= split(camelCaseRegex).joinToString(separator = " ")

/**An overload of [require][kotlin.require] that can be used in a line of code*/
@OptIn(ExperimentalContracts::class)
inline fun <T> T.require(value: Boolean, lazyMessage: () -> Any): T {
    contract {
        returns() implies value
    }

    kotlin.require(value) { lazyMessage() }
    return this
}

/**Called if the method before has a value but the value itself is not relevant
 *
 * Can be used in chaning methods with only one `?.` before the first call
 *
 * ```kotlin
 * value?.let { print(it) }.after { value = null }
 * ```*/
@OptIn(ExperimentalContracts::class)
fun <T> T?.after(action: () -> Unit): Unit? {
    contract {
        callsInPlace(action, InvocationKind.AT_MOST_ONCE)
    }
    return if(this != null) action() else null
}