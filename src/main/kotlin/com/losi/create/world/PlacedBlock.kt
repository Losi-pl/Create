package com.losi.create.world

import com.losi.create.assets.bases.Block

@JvmRecord
data class PlacedBlock (val block: Block, val variant: UByte = 0u) {
    init {
        require(block.isRegistered) { "Block is not registered" }
    }

    override fun equals(other: Any?): Boolean {
        if(this === other) return true
        if(other !is PlacedBlock) return false
        return block === other.block && variant == other.variant
    }

    override fun hashCode(): Int {
        var result = block.hashCode()
        result = 31 * result + variant.hashCode()
        return result
    }
}
