package com.losi.create.graphics

import com.losi.create.world.geometry.*
import org.lwjgl.system.MemoryStack
import kotlin.reflect.KClass
import kotlin.reflect.full.isSubclassOf

abstract class BlockFacet {
    @PublishedApi @JvmSynthetic
    internal val type: KClass<*> = this::class

    /**Allows to get objects related to this model type in the [modeler]
     * @param modeler The source and container of these buffers
     * @param T Type of object to be taken from the [modeler] is there was another object but of a different type it will be overeaten with new one
     * @param name The name of the targeted object
     * @param factory If there is no object under the [name] or object is of mismatched type a new instance will be created and placed in that location*/
    context(modeler: WorldModeler) @Suppress("unused")
    protected inline fun<reified T: Any> getObject(name: String, factory: () -> T): T {
        val t = modeler.getObject(type, name)?: factory().apply { modeler.addObject(type, name, this) }
        return t as? T ?: factory().apply { modeler.addObject(type, name, this) }
    }

    protected fun MemoryStack.mallocPositions(count: UInt) = FillModelData.PositionStorage(count.toInt(), this)
    protected fun MemoryStack.mallocUVs(count: UInt) = FillModelData.UVsStorage(count.toInt(), this)
    protected fun MemoryStack.mallocTriangles(count: UInt) = FillModelData.ElementIndexes(count.toInt() * 3, this)

    /**For use of this [BlockFacet] during model generation
     * @param vertexCount The amount of vertexes that are to be added
     * @param elementCount The amount of triangles to be added to the model, they have to be specified from index `0`*/
    context(modeler: WorldModeler)
    abstract fun draw(vertexCount: UInt, elementCount: UInt, specifier: FillModelData)

    /**For use of this [BlockFacet] during model generation
     * @param data Has all needed data to add the model*/
    context(modeler: WorldModeler)
    fun draw(data: AutoModelFill) = draw(data.vertexCount, data.elementCount, data)

    /**This is meant to be applied to a companion object of an [BlockFacet] it stores the method used to generate a final model for this type of faced*/
    abstract class FacetModeler {
        /**The type of the faced that this companion object is created for*/
        val facetType = run {
            val me = this::class
            if(!me.isCompanion)
                throw Error("Facet Modeler must be a companion object to a BlockFacet")
            val him = me.java.enclosingClass.kotlin
            if(!him.isSubclassOf(BlockFacet::class))
                throw Error("Facet Modeler must be a companion object to a BlockFacet")

            him
        }


        context(modeler: WorldModeler)
        abstract fun finish(): Mesh
        /**Allows to get objects related to this model type in the [modeler]
         * @param modeler The source and container of these buffers
         * @param T Type of object to be taken from the [modeler] is there was another object but of a different type it will be overeaten with new one
         * @param name The name of the targeted object
         * @param factory If there is no object under the [name] or object is of mismatched type a new instance will be created and placed in that location*/
        context(modeler: WorldModeler)
        protected inline fun<reified T: Any> getObject(name: String, factory: () -> T): T {
            val t = modeler.getObject(facetType, name)?: factory().apply { modeler.addObject(facetType, name, this) }
            return t as? T ?: factory().apply { modeler.addObject(facetType, name, this) }
        }
    }
}