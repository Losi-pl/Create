package com.losi.create.world

import com.losi.create.registry.GameElement

open class Realm : GameElement() {
    companion object {
        const val CHUNK_CUBE_SIZE = 16
        const val CHUNK_CUBE_COUNT = 16
        const val WORLD_HEIGHT = CHUNK_CUBE_SIZE * CHUNK_CUBE_COUNT
    }

    val world = object: World {
        override val rangeAlonY: IntRange = 0..<WORLD_HEIGHT
        override val rangeAlonX: IntRange = Int.MIN_VALUE..Int.MAX_VALUE
        override val rangeAlonZ: IntRange = Int.MIN_VALUE..Int.MAX_VALUE

        override fun get(x: Int, y: Int, z: Int): PlacedBlock {
            TODO("Not yet implemented")
        }

        override fun set(x: Int, y: Int, z: Int, block: PlacedBlock) {
            TODO("Not yet implemented")
        }

    }
}