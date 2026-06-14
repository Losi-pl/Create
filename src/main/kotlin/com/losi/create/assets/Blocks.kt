@file:Suppress("unused")
package com.losi.create.assets

import com.losi.create.assets.bases.Block
import com.losi.create.registry.ElementRegister

object Blocks {
    /**The manifest of all blocks registered the game*/
    val manifest = ElementRegister<Block>()

    val Air = object: Block() { }
    val Stone = object: Block() { }
    val Dirt = object: Block() { }
    val GrassBlock = object: Block() { }
    val Bedrock = object: Block() { }
}