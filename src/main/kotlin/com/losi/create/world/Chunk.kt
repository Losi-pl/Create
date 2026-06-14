package com.losi.create.world

interface Chunk {
    companion object {
        const val CHUNK_BLOCK_COUNT = Realm.CHUNK_CUBE_SIZE * Realm.CHUNK_CUBE_SIZE * Realm.WORLD_HEIGHT
    }

    operator fun get(x: Int, y: Int, z: Int): PlacedBlock
    operator fun set(x: Int, y: Int, z: Int, block: PlacedBlock)

    @Suppress("LocalVariableName")
    fun pointToIndex(x: Int, y: Int, z: Int): Int {
        val _z = Realm.CHUNK_CUBE_SIZE * z
        val _y = Realm.CHUNK_CUBE_SIZE * Realm.CHUNK_CUBE_SIZE * y
        return x + _y + _z
    }
}