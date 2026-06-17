@file:Suppress("unused")
package com.losi.create.assets

import com.losi.create.assets.bases.Block
import com.losi.create.registry.ElementRegister
import com.losi.create.world.geometry.WorldModeler

object Blocks {
    /**The manifest of all blocks registered the game*/
    val manifest = ElementRegister<Block>()

    val Air = object: Block() {
        // Air has no model
        context(modeler: WorldModeler) override fun generateModel(data: ModelGeneration) { }
    }
    val Stone = object: Block() { }
    val Dirt = object: Block() { }
    val GrassBlock = object: Block() { }
    val Bedrock = object: Block() { }
}