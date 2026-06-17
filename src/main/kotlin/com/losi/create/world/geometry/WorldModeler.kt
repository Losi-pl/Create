package com.losi.create.world.geometry

import com.losi.create.graphics.BlockFacet
import com.losi.create.graphics.Mesh
import org.eclipse.collections.api.factory.Maps
import org.eclipse.collections.api.map.MutableMap
import kotlin.reflect.KClass
import kotlin.reflect.full.companionObjectInstance

/**Used to generate a model of the world*/
open class WorldModeler {
    private val lists = Maps.mutable.empty<KClass<*>, MutableMap<String, Any>>()

    @PublishedApi @JvmSynthetic /**Internal meant for use in [BlockFacet] and [FacetModeler][BlockFacet.FacetModeler]*/
    internal fun <Owner : Any> getObject(owner: KClass<Owner>, name: String): Any? {
        val facetType = lists[owner]?: Maps.mutable.empty<String, Any>().apply { lists[owner] = this }
        return facetType[name]
    }

    @PublishedApi @JvmSynthetic /**Internal meant for use in [BlockFacet] and [FacetModeler][BlockFacet.FacetModeler]*/
    internal fun <Owner : Any> addObject(owner: KClass<Owner>, name: String, obj: Any) {
        val facetType = lists[owner]?: Maps.mutable.empty<String, Any>().apply { lists[owner] = this }
        facetType[name] = obj
    }

    /**Finalized the current data into [Mesh]es and clears the buffers to allow foc start of a new section if there are any*/
    protected fun finish(): Map<KClass<*>, Mesh> {
        val rez = lists.keys.associate { type ->
            val modeler = type.companionObjectInstance as? BlockFacet.FacetModeler
                ?: throw Error("Modeler not implemented correctly for $type")
            context(this) {
                type to modeler.finish()
            }
        }
        lists.clear()
        return rez
    }
}