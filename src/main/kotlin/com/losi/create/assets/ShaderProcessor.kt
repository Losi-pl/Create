package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.graphics.Shader
import com.losi.create.registry.ElementIdent

/**A processor responsible for parsing shaders in the resources into assets
 *
 * Path in assets: `shaders/`*/
internal object ShaderProcessor: AssetTypeProcessor<Shader>
{
    val shaders = HashMap<ElementIdent, Shader>()

    override fun processResources(resources: AssetTypeProcessor.Resources) {
        val shaderGroups = resources.overlayed(getResourceOrder())
            .entries.groupBy(
                { ElementIdent(it.key.first, it.key.second.substringBeforeLast('.')) },
                { Pair(it.key.second, it.value) })

        shaderGroups.forEach { (shader, parts) ->
            val vert = parts.find { it.first.endsWith(".vert") }?.let { loadResource(it.second, shader.space, it.first) }
            val frag = parts.find { it.first.endsWith(".frag") }?.let { loadResource(it.second, shader.space, it.first) }
            val xml =  parts.find { it.first.endsWith(".xml")  }?.let { loadResource(it.second, shader.space, it.first) }

            if(vert == null) { ResourceProcessingException("The Vertex shader for \"$shader\" is not defined").printStackTrace(System.err); return }
            if(frag == null) { ResourceProcessingException("The Fragment shader for \"$shader\" is not defined").printStackTrace(System.err); return }

            val shaderPr = xml?.let { Shader(vert, frag, it) } ?: Shader(vert, frag)
            shaders[shader] = shaderPr
        }
    }

    override fun clearAssets() {
        shaders.forEach { (_, shader) -> shader.release() }
        shaders.clear()
    }
    override fun getAsset(mod: ModSpace, name: String): Shader = shaders[ElementIdent(mod, name)]!!
}