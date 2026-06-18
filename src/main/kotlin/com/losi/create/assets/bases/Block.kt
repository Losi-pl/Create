package com.losi.create.assets.bases

import com.losi.create.assets.*
import com.losi.create.graphics.BlockFacet
import com.losi.create.registry.GameElement
import com.losi.create.world.PlacedBlock
import com.losi.create.world.World
import com.losi.create.world.geometry.*
import org.joml.*

/**A base for all blocks within the game*/
abstract class Block: GameElement() {

    /**Data set for [generateModel] if you need to preserve some of those data remember that unless they can't be modified they will be reused later
     * @param position The precise location of the targeted block
     * @param world The world in which this block is present*/
    data class ModelGeneration(var position: Vector3l, var world: World, var block: PlacedBlock)
    /**Used to generate a model of the current block
     * @param data The data used to generate the model
     * @param modeler Mechanism used to generate the model passed onto [BlockFacet][com.losi.create.graphics.BlockFacet]*/
    context(modeler: WorldModeler)
    open fun generateModel(data: ModelGeneration) {
        val model = BasicCubeModel(data.position)
        val visible = lazy { AmIVisible(Vector3l(), data.world, data.block, BlockDirection.Top) }
        val texture = lazy { SideFacedQuery(visible.value.position, data.world, data.block, BlockDirection.Top) }

        fun drawSide(side: BlockDirection) {
            val neighbor = data.world[data.position.x + side.offset.x,
                                      data.position.y + side.offset.y,
                                      data.position.z + side.offset.z]

            if(neighbor != Blocks.Air) {
                data.position.add(side.offset, visible.value.position)
                visible.value.side = side.inverse
                visible.value.block = neighbor
                if(neighbor.block.imVisibleFrom(visible.value)) {
                    texture.value.position.set(data.position)
                    texture.value.side = side
                    model.direction = side
                    getSideFaced(texture.value).draw(model)
                }
            }
        }

        drawSide(BlockDirection.North)
        drawSide(BlockDirection.East)
        drawSide(BlockDirection.South)
        drawSide(BlockDirection.West)
        drawSide(BlockDirection.Top)
        drawSide(BlockDirection.Bottom)
    }

    /**Data for [imVisibleFrom], Note that all mutable classes in this object along with this object itself are meant to be reused repeatedly
     * @param position The coordinates of the queried block
     * @param world The world in which this block is present
     * @param block Object containing of this block for reference
     * @param side The side of the blocks that is queried for the answer*/
    data class AmIVisible(var position: Vector3l, var world: World, var block: PlacedBlock, var side: BlockDirection)
    /**Allows to determine if this blocks hides the face of its neighbor from the specified [AmIVisible.side]*/
    open fun imVisibleFrom(data: AmIVisible): Boolean { return true }

    /**Data for [getSideFaced], Note that all mutable classes in this object along with this object itself are meant to be reused repeatedly
     * @param position The coordinates of the queried block
     * @param world The world in which this block is present
     * @param block Object containing of this block for reference
     * @param side The side of the blocks that is queried for the answer*/
    data class SideFacedQuery(var position: Vector3l, var world: World, var block: PlacedBlock, var side: BlockDirection)
    /**Returns the texture of this [SideFacedQuery.side] of this block*/
    open fun getSideFaced(data: SideFacedQuery): BlockFacet { return noTexture.value }

    companion object {
        private val noTexture = lazy { SingleTextureFaced(BlockTexture.NOT_FOUND) }
    }
}