package com.losi.create

import com.losi.create.assets.ResourceSpace

@ConsistentCopyVisibility
data class ModSpace internal constructor(val version: String, val identity: String, val resourceSpace: ResourceSpace = ResourceSpace(), val abstract: Boolean = false) {
    companion object
    {
        val modules: Map<String, ModSpace> = mapOf("create" to ModSpace(Version.version, "create"))
    }

    override fun toString(): String = "Mod: $identity"
}