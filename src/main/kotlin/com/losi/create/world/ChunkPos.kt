package com.losi.create.world

@JvmInline
value class ChunkPos(val combined: Long) {
    /**Direct constructor for this `value class`*/
    constructor(x: Int, z: Int): this(z.toLong() or (x.toLong() shl 32))
    /**Takes in the in [World] coordinates and calculates in chich chunk they are present*/
    @Suppress("unused") constructor(x: Int, @Suppress("unused") y: Int, z: Int): this(
    if(x >= 0)
            x / Realm.CHUNK_CUBE_SIZE
        else
            (x  + 1) / Realm.CHUNK_CUBE_SIZE - 1,
    if(z >= 0)
            z / Realm.CHUNK_CUBE_SIZE
        else
            (z  + 1) / Realm.CHUNK_CUBE_SIZE - 1)
    /**Takes in the in [World] coordinates and calculates in chich chunk they are present*/
    constructor(x: Long, @Suppress("unused") y: Long, z: Long): this(
    (if(x >= 0)
            x / Realm.CHUNK_CUBE_SIZE
        else
            (x  + 1) / Realm.CHUNK_CUBE_SIZE - 1).toInt(),
    (if(z >= 0)
            z / Realm.CHUNK_CUBE_SIZE
        else
            (z  + 1) / Realm.CHUNK_CUBE_SIZE - 1).toInt(),)

    val x: Int inline get() = (combined shr 32).toInt()
    val z: Int inline get() = combined.toInt()

    operator fun plus(pos: ChunkPos) = ChunkPos(x + pos.x, z + pos.z)
    operator fun minus(pos: ChunkPos) = ChunkPos(x - pos.x, z - pos.z)
    operator fun times(pos: ChunkPos) = ChunkPos(x * pos.x, z * pos.z)
    operator fun div(pos: ChunkPos) = ChunkPos(x / pos.x, z / pos.z)

    /**The span of blocks on `X` axis contained in this chunk*/
    fun toBlockSpanX() = (combined shr 32) *         Realm.CHUNK_CUBE_SIZE..<((combined shr 32) + 1) *         Realm.CHUNK_CUBE_SIZE
    /**The span of blocks on `Z` axis contained in this chunk*/
    fun toBlockSpanZ() = (combined and 0xFFFFFFFF) * Realm.CHUNK_CUBE_SIZE..<((combined and 0xFFFFFFFF) + 1) * Realm.CHUNK_CUBE_SIZE
}