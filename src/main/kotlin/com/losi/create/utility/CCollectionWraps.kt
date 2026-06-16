@file:Suppress("unused")
package com.losi.create.utility

import org.eclipse.collections.api.map.ImmutableMap
import org.eclipse.collections.impl.tuple.Tuples

fun <K, V> ImmutableMap<K, V>.toMap(): Map<K, V> = object: Map<K, V> {
    override val size: Int get() = this@toMap.size()
    override val keys: Set<K> get() = this@toMap.keysView().toSet()
    override val values: Collection<V> get() = this@toMap.valuesView().toSet()
    override val entries: Set<Map.Entry<K, V>> = object : Set<Map.Entry<K, V>> {
        val view = this@toMap.keyValuesView()

        override val size get() = view.size()
        override fun isEmpty() = view.isEmpty
        override fun contains(element: Map.Entry<K, V>) = view.contains(Tuples.pair(element.value, element.key))
        override fun containsAll(elements: Collection<Map.Entry<K, V>>) = view.containsAll(elements.map { Tuples.pair(it.value, it.key) })
        override fun iterator() = object : Iterator<Map.Entry<K, V>> {
            val iterator = view.iterator()
            override fun next(): Map.Entry<K, V> = iterator.next().let { java.util.Map.entry(it.one, it.two) }
            override fun hasNext() = iterator.hasNext()
        }
    }

    override fun isEmpty() = this@toMap.isEmpty

    override fun containsKey(key: K) = this@toMap.containsKey(key)

    override fun containsValue(value: V) = this@toMap.containsValue(value)

    override fun get(key: K) = this@toMap.get(key)
}

