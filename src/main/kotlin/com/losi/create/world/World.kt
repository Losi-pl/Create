package com.losi.create.world

interface World {
    val rangeAlonX: LongRange
    val rangeAlonY: LongRange
    val rangeAlonZ: LongRange

    operator fun get(x: Int, y: Int, z: Int) = get(x.toLong(), y.toLong(), z.toLong())
    operator fun set(x: Int, y: Int, z: Int, block: PlacedBlock) = set(x.toLong(), y.toLong(), z.toLong(), block)

    operator fun get(x: Long, y: Long, z: Long): PlacedBlock

    operator fun set(x: Long, y: Long, z: Long, block: PlacedBlock)
}