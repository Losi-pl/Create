@file:Suppress("unused")
package com.losi.create.assets

import com.losi.create.assets.bases.Block
import com.losi.create.graphics.BlockFacet
import com.losi.create.registry.ElementRegister
import com.losi.create.world.geometry.*
import java.awt.Color

object Blocks {
    /**The manifest of all blocks registered the game*/
    val manifest = ElementRegister<Block>()

    val Air = object: Block() {
        // Air has no model
        context(modeler: WorldModeler) override fun generateModel(data: ModelGeneration) { }
        override fun imVisibleFrom(data: AmIVisible) = true
    }
    val Stone = object: Block() {
        lateinit var texture: BlockFacet
        override fun onRegister() { texture = SingleTextureFaced(BlockTexture.find("create:stone")) }
        override fun getSideFaced(data: SideFacedQuery) = texture
    }
    val Dirt = object: Block() {
        lateinit var texture: BlockFacet
        override fun onRegister() { texture = SingleTextureFaced(BlockTexture.find("create:dirt")) }
        override fun getSideFaced(data: SideFacedQuery) = texture
    }
    val GrassBlock = object: Block() {
        lateinit var topTexture: BlockFacet
        lateinit var bottomTexture: BlockFacet
        lateinit var sideTexture: BlockFacet
        override fun onRegister() {
            topTexture = ColoredTextureFaced(BlockTexture.find("create:grass-block-top"), Color.decode("#308d27"))
            bottomTexture = SingleTextureFaced(BlockTexture.find("create:dirt"))
            sideTexture = DoubleColoredTexturesFaced(BlockTexture.find("create:dirt"), Color.WHITE,
                                                     BlockTexture.find("create:grass-block-side"), Color.decode("#308d27"))
        }

        override fun getSideFaced(data: SideFacedQuery): BlockFacet {
            if(data.side == BlockDirection.Top)
                return topTexture
            if(data.side == BlockDirection.Bottom)
                return bottomTexture
            return sideTexture
        }
    }
    val Bedrock = object: Block() {
        lateinit var texture: BlockFacet
        override fun onRegister() { texture = SingleTextureFaced(BlockTexture.find("create:bedrock")) }
        override fun getSideFaced(data: SideFacedQuery) = texture
    }
}