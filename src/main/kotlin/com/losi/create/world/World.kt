package com.losi.create.world

import com.losi.create.assets.bases.Block

interface World {
    val rangeAlonX: IntRange
    val rangeAlonY: IntRange
    val rangeAlonZ: IntRange

    operator fun get(x: Int, y: Int, z: Int): Block
    operator fun set(x: Int, y: Int, z: Int, block: Block)
}