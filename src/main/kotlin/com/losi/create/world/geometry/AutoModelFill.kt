package com.losi.create.world.geometry

/**An automatic set of required parameters used in [BlockFacet.draw()][com.losi.create.graphics.BlockFacet.draw]*/
interface AutoModelFill: FillModelData {
    /**The amount of vertexes to add*/
    val vertexCount: UInt
    /**Amount of triangles to add*/
    val elementCount: UInt
}