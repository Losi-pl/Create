package com.losi.create.world

import com.losi.create.utility.MutablePair

class Chunk256: Chunk {
    private var MutablePair<PlacedBlock, UInt>.block: PlacedBlock inline get() = this.first; inline set(v) { this.first = v }
    private var MutablePair<PlacedBlock, UInt>.count: UInt inline get() = this.second; inline set(v) { this.second = v }

    private val knownBlocks = mutableListOf<MutablePair<PlacedBlock, UInt>>()
    private val whereWhatBlock = ByteArray(Realm.CHUNK_CUBE_SIZE * Realm.CHUNK_CUBE_SIZE * Realm.WORLD_HEIGHT) { 0 }

    constructor(block: PlacedBlock)
    {
        knownBlocks.add(MutablePair(block, Chunk.CHUNK_BLOCK_COUNT.toUInt()))
    }

    override fun get(x: Int, y: Int, z: Int) = knownBlocks[whereWhatBlock[pointToIndex(x, y, z)].toInt()].block

    override fun set(x: Int, y: Int, z: Int, block: PlacedBlock) {
        val ind = pointToIndex(x, y, z)
        if(knownBlocks[whereWhatBlock[ind].toInt()].block == block)
            return

        decrement(ind)
        val where = increment(block)
        whereWhatBlock[ind] = where.toByte()
    }

    fun decrement(ind: Int) {
        val placedBlock = knownBlocks[ind]
        placedBlock.count--
    }

    fun increment(block: PlacedBlock): Int {
        val oldInd = knownBlocks.indexOfFirst { it.block == block }
        if (oldInd > -1) {
            knownBlocks[oldInd].count++
            return oldInd
        }
        else {
            var newInd = knownBlocks.indexOfFirst { it.count == 0u }
            val newBlock = if(newInd > -1) knownBlocks[newInd].apply { this.block = block } else
                MutablePair(block, 0u).apply { newInd = knownBlocks.size; knownBlocks.add(this) }
            newBlock.count++
            return newInd
        }
    }
}