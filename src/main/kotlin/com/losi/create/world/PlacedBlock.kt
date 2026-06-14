package com.losi.create.world

import com.losi.create.assets.bases.Block

data class PlacedBlock (val block: Block, val variant: UByte) {
    init {
        require(block.isRegistered) { "Block is not registered" }
    }
}
