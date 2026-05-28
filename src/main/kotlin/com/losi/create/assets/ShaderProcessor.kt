package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.graphics.Shader

/**A processor responsible for parsing shaders in the resources into assets
 *
 * Path in assets: `shaders/`*/
internal object ShaderProcessor: AssetTypeProcessor<Shader>
{
    val shaders = HashMap<Pair<ModSpace, String>, Shader>()

    override fun processResources(resources: AssetTypeProcessor.Resources) {
        val shaderGroups = resources.overlayed(getResourceOrder())
            .entries.groupBy(
                { Pair(it.key.first, it.key.second.substringBeforeLast('.')) },
                { Pair(it.key.second, it.value) })

        shaderGroups.forEach { (shader, parts) ->
            val vert = parts.find { it.first.endsWith(".vert") }?.let { loadResource(it.second, shader.first, it.first) }
            val frag = parts.find { it.first.endsWith(".frag") }?.let { loadResource(it.second, shader.first, it.first) }
            val xml =  parts.find { it.first.endsWith(".xml")  }?.let { loadResource(it.second, shader.first, it.first) }

            if(vert == null) { ResourceProcessingException("The Vertex shader for \"${genName(shader)}\" is not defined").printStackTrace(System.err); return }
            if(frag == null) { ResourceProcessingException("The Fragment shader for \"${genName(shader)}\" is not defined").printStackTrace(System.err); return }

            val shaderPr = xml?.let { Shader(vert, frag, it) } ?: Shader(vert, frag)
            shaders[shader] = shaderPr
        }
    }

    /**Converts a pair containing the [ModSpace] and the path to resource into a human friendly format*/
    fun genName(shader: Pair<ModSpace, String>): String = "${shader.first.identity}:${shader.second}"

    override fun clearAssets() = TODO("Not yet implemented")
    override fun getAsset(mod: ModSpace, name: String): Shader = shaders[Pair(mod, name)]!!
}