package com.losi.create.world

@JvmInline
value class ChunkPos(val combined: Long) {
    constructor(x: Int, z: Int): this(z.toLong() or (x.toLong() shl 32))
    constructor(x: Int, @Suppress("unused") y: Int, z: Int): this(
    if(x >= 0)
            x / Realm.CHUNK_CUBE_SIZE
        else
            (x  + 1) / Realm.CHUNK_CUBE_SIZE - 1,
    if(z >= 0)
            z / Realm.CHUNK_CUBE_SIZE
        else
            (z  + 1) / Realm.CHUNK_CUBE_SIZE - 1)

    val x: Int inline get() = (combined shr 32).toInt()
    val z: Int inline get() = combined.toInt()

    operator fun plus(pos: ChunkPos) = ChunkPos(x + pos.x, z + pos.z)
    operator fun minus(pos: ChunkPos) = ChunkPos(x - pos.x, z - pos.z)
    operator fun times(pos: ChunkPos) = ChunkPos(x * pos.x, z * pos.z)
    operator fun div(pos: ChunkPos) = ChunkPos(x / pos.x, z / pos.z)
}