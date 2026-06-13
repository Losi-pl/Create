package com.losi.create.assets

import com.koloboke.collect.set.hash.HashLongSets
import com.losi.create.ModSpace
import com.losi.create.registry.ElementIdent
import com.losi.create.utility.autoClosable
import com.losi.create.utility.forceAdd
import com.losi.create.utility.longHashCode

/**The interface used to create a mechanism used in [AssetManager] to process objects of type [T]*/
interface AssetTypeProcessor<T>
{
    /**Format of resources to be processed in [processResources]*/
    typealias Resources = Map<ResourceSpace, Map<ModSpace, List<String>>>
    companion object
    {
        /**Path in assets which this specific type occupies like `shaders/`
         *
         * It is set whenever this block is called*/
        internal val nameType = ThreadLocal<String>()
        /**Contains a list of all recognized sources of resources in order of their importance
         *
         * They are put in order of least to most important ones*/
        internal val order = ThreadLocal<List<ResourceSpace>>()

        /**The standard file identity contains its file extension, this method is meant purely for removing that extension*/
        fun Pair<ModSpace, String>.cutExtent() = ElementIdent(this.first, this.second.substringBeforeLast('.'))

        /**An extension of [AssetTypeProcessor.getAsset]
         *
         * This variation is made for a quick call on resource without a need to pass on the specific [ModSpace]
         * and that type being inferred from the structure of the query in format `mod:resource`*/
        internal fun <T> AssetTypeProcessor<T>.getAsset(name: String): T? {
            val modIdent = name.substringBefore(':')
            val actualName = name.substringAfter(':')
            val mod = ModSpace.modules[modIdent] ?: ModSpace("", modIdent, ResourceSpace(), true)
            return getAsset(mod, actualName)
        }
    }

    /**Will return a list of all [ResourceSpace] recognized by the game in the order of their importance from least to most important ones*/
    fun getResourceOrder() = order.get()!!
    /**This is used to get an [java.io.InputStream] to a specified resource
     * @param space The specific [ResourceSpace] you want to load from
     * @param mod The specific [ModSpace] the resource is attached to
     * @param name The path to or name of the resource you want to load within the type*/
    fun loadResource(space: ResourceSpace, mod: ModSpace, name: String) =
        AssetManager.getStream(space, "assets/${mod.identity}/${nameType.get()}$name")?.autoClosable()
    /**This is used to get an [java.io.InputStream] to a specified resource
     * @param space The specific [ResourceSpace] you want to load from
     * @param path A combination of the [ModSpace] and the path within this type to the searched resource*/
    fun loadResource(space: ResourceSpace, path: Pair<ModSpace, String>) = loadResource(space, path.first, path.second)
    /**Created a map with the paths to specific resources serving as keys and leading to [ResourceSpace]'s containing the most relevant version of that resource
     * @param order the order of relevance of the resources, can  be obtained from [getResourceOrder]*/
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

    /**This method is used for loading resources and processing them into assets
     * @param resources The resources to be processed*/
    fun processResources(resources: Resources)
    /**If the resources are meant to be reloaded this method is called to clear the current versions of them*/
    fun clearAssets()
    /**This method is used to obtain a specific resource of this type
     * @param mod The [ModSpace] the resource is attached to
     * @param name The path or name of the resource being looked for*/
    fun getAsset(mod: ModSpace, name : String): T?
}