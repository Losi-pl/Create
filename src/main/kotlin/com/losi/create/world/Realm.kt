package com.losi.create.world

import com.losi.create.assets.Blocks
import com.losi.create.registry.GameElement

open class Realm : GameElement() {
    companion object {
        const val CHUNK_CUBE_SIZE = 16
        const val CHUNK_CUBE_COUNT = 16
        const val WORLD_HEIGHT = CHUNK_CUBE_SIZE * CHUNK_CUBE_COUNT
    }

    open fun generateChunk(pos: ChunkPos): Chunk { return Chunk256(PlacedBlock(Blocks.Stone)); }

    val world = RealmWorld(this)
}