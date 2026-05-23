package com.losi.create.assets

import com.losi.create.ModSpace

interface AssetTypeProcessor<T>
{
    typealias Resources = Map<ResourceSpace, Map<ModSpace, Set<String>>>
    companion object
    {
        internal val nameType = ThreadLocal<String>()
    }

    fun loadResource(space: ResourceSpace, mod: ModSpace, name: String) =
        Manager.getStream(space, "assets/${mod.identity}/${nameType.get()}$name")

    fun processResources(resources: Resources)
    fun clearAssets()
    fun getAsset(name : String) : T?
}