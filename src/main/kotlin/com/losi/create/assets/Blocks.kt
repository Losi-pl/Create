@file:Suppress("unused")
package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.assets.bases.Block
import com.losi.create.registry.ElementRegister
import com.losi.create.utility.splitCamelCase
import kotlin.reflect.KMutableProperty
import kotlin.reflect.full.declaredMemberProperties
import kotlin.reflect.full.isSubtypeOf
import kotlin.reflect.full.starProjectedType

object Blocks {
    /**The manifest of all blocks registered the game*/
    val manifest = ElementRegister<Block>()

    val Air = object: Block() { }
    val Stone = object: Block() { }
    val Dirt = object: Block() { }
    val GrassBlock = object: Block() { }
    val Bedrock = object: Block() { }

    /**Ads all elements in this object to the [manifest]*/
    internal fun registerElements() {

        val list = Blocks::class.declaredMemberProperties.asSequence().filter {
            if(it is KMutableProperty<*>)
                return@filter false

            if(it.returnType == Block::class.starProjectedType)
                return@filter true

            return@filter it.returnType.isSubtypeOf(Block::class.starProjectedType)
        }.map {
            it.name to (it.get(Blocks) as? Block?: return@map null)
        }.filterNotNull()

        val space = ModSpace.modules["create"]!!
        val regex = "[^\\p{L}0-9-_]+".toRegex()

        list.forEach {
            manifest.register(it.second, space, it.first.splitCamelCase().lowercase().replace(regex, "-"))
        }
    }
}