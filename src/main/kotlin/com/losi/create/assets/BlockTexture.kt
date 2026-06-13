package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.assets.AssetTypeProcessor.Companion.cutExtent
import com.losi.create.graphics.*
import com.losi.create.graphics.gl.*
import com.losi.create.registry.ElementIdent
import com.losi.create.utility.orElse
import java.io.InputStream
import kotlin.collections.forEach

/**A handler to a specific texture in the [atlas] used for efficient texture handling*/
@JvmInline
value class BlockTexture private constructor(val index: UInt){
    companion object {
        /**The standard pixel size of a block texture, all textures that don't meat this standard will cause an error*/
        const val TEXTURE_SIZE = 16u
        /**The atlas with all loaded block textures*/
        val atlas: Texture2DAtlas get() = BlockAtlasProcessor.atlas?: throw NullPointerException("Atlas with textures of blocks was not yet created")
        /**Default texture returned if their specific texture couldn't be located*/
        val NOT_FOUND = BlockTexture(0u)
    }

    /**A processor responsible for parsing block textures and combining them into a single array that can later be used in shaders
     *
     * Returns instances of [BlockTexture] that serve as markers to that specific texture in the atlas
     *
     * Path in assets: `textures/blocks/`*/
    internal object BlockAtlasProcessor: AssetTypeProcessor<BlockTexture> {
        var atlas: Texture2DAtlas? = null
        var atlasMap: Map<ElementIdent, BlockTexture>? = null

        override fun processResources(resources: AssetTypeProcessor.Resources) {
            val overlayed = resources.overlayed(getResourceOrder())
            val estCount = overlayed.size
            val map = atlasMap?.toMutableMap()?.apply { keys.forEach { this[it] = NOT_FOUND } } ?: mutableMapOf()
            var count = 0u
            atlas = Texture2DAtlas.create {
                //OpenGL formating
                internalFormat(InternalFormat.RGBA8)
                wrapping(WrappingDirection.Horizontal, TextureWrappingMode.ClampToEdge)
                wrapping(WrappingDirection.Vertical, TextureWrappingMode.ClampToEdge)
                mag(MagFilterMode.Nearest)
                min(MinFilterMode.Nearest)

                //Atlas dimensions
                imageSize(TEXTURE_SIZE, TEXTURE_SIZE)
                imageCount(estCount + 1)

                //Texture loading
                NULL_TEXTURE_STREAM.use { set(0u, it) }
                overlayed.forEach { (identity, resource) ->
                    val file = loadResource(resource, identity)
                    assert(file != null) { "Resource ${identity.first.identity}:${identity.second} not found" }

                    file?.use { set(++count, it) }
                    map[identity.cutExtent()] = BlockTexture(count)
                }
            }
            atlasMap = map.toMap()
        }

        override fun clearAssets() {
            atlas?.release()
            atlas = null
        }

        override fun getAsset(mod: ModSpace, name: String) = atlasMap.orElse {
            throw NullPointerException("Texture atlas was not yet created") }[ElementIdent(mod, name)]?: NOT_FOUND

        /**A stream with the texture used in [NOT_FOUND]*/
        private val NULL_TEXTURE_STREAM: InputStream get() =
            """<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16">
              |    <rect x="0" y="0" width="16" height="16" fill="black"/>
              |    <rect x="0" y="8" width="8" height="8" fill="magenta"/>
              |    <rect x="8" y="0" width="8" height="8" fill="magenta"/>
              |</svg>""".trimMargin().byteInputStream()
    }
}