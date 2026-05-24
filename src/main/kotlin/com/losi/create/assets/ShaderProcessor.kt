package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.graphics.Shader

internal object ShaderProcessor: AssetTypeProcessor<Shader>
{
    val shaders = HashMap<Pair<ModSpace, String>, Shader>()

    override fun processResources(resources: AssetTypeProcessor.Resources) {
        val combined = resources.overlayed(getResourceOrder())
        val shaderGroups = combined.keys.groupBy({ Pair(it.first, it.second.substringBeforeLast('.')) }, { it.second })
    }

    override fun clearAssets() = TODO("Not yet implemented")
    override fun getAsset(mod: ModSpace, name: String): Shader = TODO("Not yet implemented")
}