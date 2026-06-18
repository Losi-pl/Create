package com.losi.create.world.geometry

import com.losi.create.assets.Blocks
import com.losi.create.assets.bases.Block
import com.losi.create.graphics.*
import com.losi.create.graphics.gl.glFinish
import com.losi.create.world.*
import org.eclipse.collections.impl.list.mutable.FastList
import org.eclipse.collections.impl.multimap.list.FastListMultimap
import org.eclipse.collections.impl.tuple.Tuples
import org.joml.Vector3l
import kotlin.reflect.KClass

/**Class used to create a model of a [Chunk]*/
class ChunkModeler: WorldModeler {
    companion object {
        private val noBlock = lazy { PlacedBlock(Blocks.Air) }
        private val unloadedBlock = lazy { PlacedBlock(Blocks.Stone) }
    }

    /**The primary [Chunk] used for the process*/
    private val primary: Chunk
    /**The neighboring chunks to the [primary] chunk
     * ```
     * [-1, -1] [ 0, -1] [+1, -1]
     * [-1,  0] [ 0,  0] [+1,  0]
     * [-1, +1] [ 0, +1] [+1, +1]
     * ```*/
    private val neighbors: Array<Chunk?>
    /**The center point from which the [primary] chunk is from*/
    val centerPoint: ChunkPos
    /**Origin world for this operation of the case in which, there is a call made for a [Chunk] that is not pre chased in this class*/
    val realm: RealmWorld

    /**Creates a new instance of this class can't be modified after creation
     * @param realm Source of the [World] to draw from
     * @param chunk Which specific chunk to be drawn*/
    constructor(realm: Realm, chunk: ChunkPos): this(realm.world, chunk)
    /**Creates a new instance of this class can't be modified after creation
     * @param realm Source of the [World] to draw from
     * @param chunk Which specific chunk to be drawn*/
    constructor(realm: RealmWorld, chunk: ChunkPos) {
        primary = realm.getChunk(chunk)?: throw IllegalArgumentException("The Chunk you are trying to model is not loaded")

        neighbors = Array(9) {
            if(it == 4)//This is the central(primary) chunk
                return@Array primary

            val x = (it % 3) - 1
            val z = (it / 3) - 1

            realm.getChunk(ChunkPos(chunk.x + x, chunk.z + z))
        }

        centerPoint = chunk
        this.realm = realm
    }

    /**Custom world optimized for repeated class on specific set of chunks oriented around [primary] chunk and the [centerPoint]
     *
     * Goes in order of [primary] -> [neighbors] -> [realm]*/
    val world = object: World {
        override val rangeAlonX get() = realm.rangeAlonX
        override val rangeAlonY get() = realm.rangeAlonY
        override val rangeAlonZ get() = realm.rangeAlonZ

        override fun get(x: Long, y: Long, z: Long): PlacedBlock {
            if(y !in 0..<Realm.WORLD_HEIGHT)
                return noBlock.value

            val ch = findChunk(ChunkPos(x, y, z))

            val neoX = Math.floorMod(x, Realm.CHUNK_CUBE_SIZE)
            val neoZ = Math.floorMod(z, Realm.CHUNK_CUBE_SIZE)
            return ch?.get(neoX, y.toInt(), neoZ) ?: unloadedBlock.value
        }

        private fun findChunk(pos: ChunkPos): Chunk? {
            if(pos == centerPoint)
                return primary
            if(pos.x !in centerPoint.x-1..centerPoint.x+1)
                return realm.getChunk(pos)
            if(pos.z !in centerPoint.z-1..centerPoint.z+1)
                return realm.getChunk(pos)
            val x = (pos.x - centerPoint.x) + 1
            val z = (pos.z - centerPoint.z) + 1
            return neighbors[x + (z * 3)]
        }

        override fun set(x: Long, y: Long, z: Long, block: PlacedBlock) {
            throw NotImplementedError("This is an read only view")
        }
    }

    /**Generates the model based on this Modeler
     *
     * Model is separated into segment cubes of [CHUNK_CUBE_SIZE][Realm.CHUNK_CUBE_SIZE] stacked on top of each other*/
    fun generate(): ChunkModel {
        val spanX = centerPoint.toBlockSpanX()
        val spanZ = centerPoint.toBlockSpanZ()
        val data = Block.ModelGeneration(Vector3l(), world, unloadedBlock.value)
        val models = FastList.newList<Map<KClass<*>, Mesh>>(Realm.CHUNK_CUBE_COUNT)
        for(c in 0 until Realm.CHUNK_CUBE_COUNT) {
            for (y in c * Realm.CHUNK_CUBE_SIZE until (c + 1) * Realm.CHUNK_CUBE_SIZE.toLong())
                for (x in spanX)
                    for (z in spanZ) {
                        data.position.set(x, y, z)
                        data.block = world[x, y, z]
                        data.block.block.generateModel(data)
                    }
            models.add(finish())
        }

        /**Reordered from using [BlockFacet]'s class as a key to [Shader] underlying the [Mesh]es*/
        val sortedModels = FastListMultimap.newMultimap<Shader, Mesh>()
        models.asSequence().flatMap { it.values }.forEach { model ->
            sortedModels.add(Tuples.pair(model.shader, model))
        }
        glFinish()
        return ChunkModel(sortedModels)
    }
}