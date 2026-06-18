package com.losi.create.assets.bases

import com.losi.create.assets.*
import com.losi.create.registry.GameElement
import com.losi.create.world.World
import com.losi.create.world.geometry.*
import org.joml.*

/**A base for all blocks within the game*/
abstract class Block: GameElement() {

    /**Data set for [generateModel] if you need to preserve some of those data remember that unless they can't be modified they will be reused later
     * @param position The precise location of the targeted block
     * @param world The world in which this block is present*/
    data class ModelGeneration(var position: Vector3l, var world: World)
    /**Used to generate a model of the current block
     * @param data The data used to generate the model
     * @param modeler Mechanism used to generate the model passed onto [BlockFacet][com.losi.create.graphics.BlockFacet]*/
    context(modeler: WorldModeler)
    open fun generateModel(data: ModelGeneration) {
        val texture = SingleTextureFaced(BlockTexture.find("create:dirt"))

        val model = BasicCubeModel(data.position)

        model.direction = BlockDirection.North
        texture.draw(model)

        model.direction = BlockDirection.East
        texture.draw(model)

        model.direction = BlockDirection.South
        texture.draw(model)

        model.direction = BlockDirection.West
        texture.draw(model)

        model.direction = BlockDirection.Top
        texture.draw(model)

        model.direction = BlockDirection.Bottom
        texture.draw(model)
    }
}