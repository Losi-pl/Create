package com.losi.create.world.geometry

import com.losi.create.math.collections.*

/**Used to fill the model data used in [BlockFacet.draw()][com.losi.create.graphics.BlockFacet.draw]*/
fun interface FillModelData {
    @OptIn(ExperimentalUnsignedTypes::class)
    fun fill(positions: Vector3fArray, uvs: Vector2fArray, elements: UIntArray)
}