@file:Suppress("unused")
package com.losi.create.world

import com.koloboke.collect.map.hash.HashLongObjMaps
import com.losi.create.assets.Blocks

/**The variant of [World] used by the [Realm]'s*/
class RealmWorld: World {
    companion object {
        /**Used when a block is called on from an unloaded chunk*/
        private val unloadedBlock = lazy { PlacedBlock(Blocks.Stone) }
        /**Used when a block is called on from beyond height limit [0<=[..]<WORLD_HEIGHT]*/
        @Suppress("GrazieInspection", "RedundantSuppression") private val emptyBlock = lazy { PlacedBlock(Blocks.Air) }

        val rangeAlonY = 0L..<Realm.WORLD_HEIGHT
        val rangeAlonX = Int.MIN_VALUE.toLong() * Realm.CHUNK_CUBE_SIZE.toLong() ..<Int.MAX_VALUE.toLong() * Realm.CHUNK_CUBE_SIZE.toLong()
        val rangeAlonZ = Int.MIN_VALUE.toLong() * Realm.CHUNK_CUBE_SIZE.toLong()..<Int.MAX_VALUE.toLong() * Realm.CHUNK_CUBE_SIZE.toLong()
    }

    val realm: Realm

    internal constructor(realm: Realm) { this.realm = realm; }

    private val chunks = HashLongObjMaps.newMutableMap<Chunk>()

    fun getChunk(pos: ChunkPos) = chunks[pos.combined]
    fun isChungLoaded(pos: ChunkPos): Boolean = chunks.containsKey(pos.combined)
    fun loadChunk(pos: ChunkPos) = chunks[pos.combined]?: realm.generateChunk(pos).apply { chunks[pos.combined] = this }
    fun unloadChunk(pos: ChunkPos) = chunks.remove(pos.combined) != null

    override val rangeAlonX get() = RealmWorld.rangeAlonX
    override val rangeAlonY get() = RealmWorld.rangeAlonY
    override val rangeAlonZ get() = RealmWorld.rangeAlonZ

    override fun get(x: Int, y: Int, z: Int): PlacedBlock = get(x.toLong(), y.toLong(), z.toLong())
    override fun set(x: Int, y: Int, z: Int, block: PlacedBlock) = set(x.toLong(), y.toLong(), z.toLong(), block)

    override fun get(x: Long, y: Long, z: Long): PlacedBlock {
        if(y !in 0..<Realm.WORLD_HEIGHT)
            return emptyBlock.value

        val ch = getChunk(ChunkPos(x, y, z)) ?: return unloadedBlock.value
        val inX = Math.floorMod(x, Realm.CHUNK_CUBE_SIZE)
        val inZ = Math.floorMod(z, Realm.CHUNK_CUBE_SIZE)
        return ch[inX, y.toInt(), inZ]
    }

    override fun set(x: Long, y: Long, z: Long, block: PlacedBlock) {
        if(y !in 0..<Realm.WORLD_HEIGHT)
            return
        getChunk(ChunkPos(x, y, z))?.let { ch ->
            val inX = Math.floorMod(x, Realm.CHUNK_CUBE_SIZE)
            val inZ = Math.floorMod(z, Realm.CHUNK_CUBE_SIZE)
            ch[inX, y.toInt(), inZ] = block
        }
    }
}