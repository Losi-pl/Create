package com.losi.create

import com.losi.create.assets.ResourceSpace

/**The identity of a modification in the game
 * @property version Specifies what is the current version of the modification
 * @property identity A code name of this modification used primarily for easier developer side logic
 * @property resourceSpace The resources contained in the `jar` of this modification
 * @property abstract If it's `true` that means that this [ModSpace] doesn't actually exists and was only created as the point of reference for all elements that associate to it*/
@ConsistentCopyVisibility
data class ModSpace internal constructor(val version: String, val identity: String, val resourceSpace: ResourceSpace = ResourceSpace(), val abstract: Boolean = false) {
    companion object
    {
        /**A list of all modification currently detected by the game*/
        val modules: Map<String, ModSpace> = mapOf("create" to ModSpace(Version.version, "create"))
    }

    override fun toString(): String = "Mod: $identity"
}