package com.losi.create.world

interface World {
    val rangeAlonX: IntRange
    val rangeAlonY: IntRange
    val rangeAlonZ: IntRange

    operator fun get(x: Int, y: Int, z: Int): PlacedBlock
    operator fun set(x: Int, y: Int, z: Int, block: PlacedBlock)
}