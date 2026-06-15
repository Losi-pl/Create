@file:Suppress("unused")
package com.losi.create.utility

import org.eclipse.collections.api.map.ImmutableMap
import org.eclipse.collections.impl.tuple.Tuples

fun <V, K> ImmutableMap<V, K>.toMap(): Map<V, K> = object: Map<V, K> {
    override val size: Int get() = this@toMap.size()
    override val keys: Set<V> get() = this@toMap.keysView().toSet()
    override val values: Collection<K> get() = this@toMap.valuesView().toSet()
    override val entries: Set<Map.Entry<V, K>> = object : Set<Map.Entry<V, K>> {
        val view = this@toMap.keyValuesView()

        override val size get() = view.size()
        override fun isEmpty() = view.isEmpty
        override fun contains(element: Map.Entry<V, K>) = view.contains(Tuples.pair(element.value, element.key))
        override fun containsAll(elements: Collection<Map.Entry<V, K>>) = view.containsAll(elements.map { Tuples.pair(it.value, it.key) })
        override fun iterator() = object : Iterator<Map.Entry<V, K>> {
            val iterator = view.iterator()
            override fun next(): Map.Entry<V, K> = iterator.next().let { java.util.Map.entry(it.one, it.two) }
            override fun hasNext() = iterator.hasNext()
        }
    }

    override fun isEmpty() = this@toMap.isEmpty

    override fun containsKey(key: V) = this@toMap.containsKey(key)

    override fun containsValue(value: K) = this@toMap.containsValue(value)

    override fun get(key: V) = this@toMap.get(key)
}

