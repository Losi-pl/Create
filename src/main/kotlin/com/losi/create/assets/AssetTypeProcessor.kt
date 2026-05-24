package com.losi.create.assets

import com.koloboke.collect.set.hash.HashLongSets
import com.losi.create.ModSpace
import com.losi.create.utility.forceAdd
import com.losi.create.utility.longHashCode

interface AssetTypeProcessor<T>
{
    typealias Resources = Map<ResourceSpace, Map<ModSpace, List<String>>>
    companion object
    {
        internal val nameType = ThreadLocal<String>()
        internal val order = ThreadLocal<List<ResourceSpace>>()
    }

    fun getResourceOrder() = order.get()!!
    fun loadResource(space: ResourceSpace, mod: ModSpace, name: String) =
        Manager.getStream(space, "assets/${mod.identity}/${nameType.get()}$name")
    fun loadResource(space: ResourceSpace, path: Pair<ModSpace, String>) =
        Manager.getStream(space, "assets/${path.first.identity}/${nameType.get()}${path.second}")
    fun Resources.overlayed(order: List<ResourceSpace>) = this.let { resources-> object : Map<Pair<ModSpace, String>, ResourceSpace> {
        val lazySize = lazy {
            val hash = HashLongSets.newMutableSet()
            for(i in order.size-1 downTo 0)
            {
                val map = resources[order[i]]
                map?.forEach { (space, strings) ->
                    strings.forEach { hash.forceAdd(Pair(space, it).longHashCode()) }
                }
            }
            return@lazy hash.size
        }
        val lazyIsEmpty = lazy {
            if(lazySize.isInitialized())
                return@lazy lazySize.value == 0

            if(order.isEmpty())
                return@lazy true
            order.forEach { space ->
                resources[space]?.forEach { (_, strings) ->
                    if(!strings.isEmpty())
                        return@lazy false
                }
            }
            return@lazy true
        }

        override val size: Int get() = lazySize.value
        override val keys: Set<Pair<ModSpace, String>> get() = object : Set<Pair<ModSpace, String>>{
            val values = entries

            override val size: Int get() = values.size
            override fun isEmpty(): Boolean = values.isEmpty()
            override fun contains(element: Pair<ModSpace, String>): Boolean {
                order.reversed().forEach { space ->
                    if (resources[space]?.get(element.first)?.contains(element.second) ?: false)
                        return true
                }
                return false
            }
            override fun containsAll(elements: Collection<Pair<ModSpace, String>>): Boolean
              = elements.all { contains(it) }
            override fun iterator(): Iterator<Pair<ModSpace, String>> = object : Iterator<Pair<ModSpace, String>> {
                val iter = values.iterator()
                override fun next(): Pair<ModSpace, String> = iter.next().key
                override fun hasNext(): Boolean  = iter.hasNext()
            }
        }
        override val values: Collection<ResourceSpace> get() = throw NotImplementedError("For the time being it is not needed")
        override val entries get(): Set<Map.Entry<Pair<ModSpace, String>, ResourceSpace>>
        = object : Set<Map.Entry<Pair<ModSpace, String>, ResourceSpace>> {
            override val size get() = lazySize.value
            override fun isEmpty() = lazyIsEmpty.value
            override fun contains(element: Map.Entry<Pair<ModSpace, String>, ResourceSpace>): Boolean {
                order.reversed().forEach { space ->
                    if (resources[space]?.get(element.key.first)?.contains(element.key.second) ?: false)
                        return space == element.value
                }
                return false
            }
            override fun iterator(): Iterator<Map.Entry<Pair<ModSpace, String>, ResourceSpace>> = iterator {
                val hash = HashLongSets.newMutableSet()
                for(i in order.size-1 downTo 0)
                {
                    val map = resources[order[i]]
                    map?.forEach { (space, strings) ->
                        strings.forEach {
                            val pair = Pair(space, it)
                            if(hash.forceAdd(pair.longHashCode()))
                                yield(java.util.Map.entry(pair, order[i]))
                        }
                    }
                }
            }
            override fun containsAll(elements: Collection<Map.Entry<Pair<ModSpace, String>, ResourceSpace>>): Boolean
              = elements.all { contains(it) }
        }

        override fun isEmpty() = lazyIsEmpty.value
        override fun containsKey(key: Pair<ModSpace, String>): Boolean = get(key) != null
        override fun containsValue(value: ResourceSpace): Boolean = throw NotImplementedError("For the time being it is not needed")
        override fun get(key: Pair<ModSpace, String>): ResourceSpace? {
            for(i in order.size-1 downTo 0)
            {
                val map = resources[order[i]]
                if(map?.get(key.first)?.any { it == key.second }?: false)
                    return order[i]
            }
            return null
        }
    }}

    fun processResources(resources: Resources)
    fun clearAssets()
    fun getAsset(mod: ModSpace, name : String): T?
    fun getAsset(name: String): T? {
        val modIdent = name.substringBefore(':')
        val actualName = name.substringAfter(':')
        val mod = ModSpace.modules[modIdent] ?: ModSpace("", modIdent, ResourceSpace(), true)
        return getAsset(mod, actualName)
    }
}