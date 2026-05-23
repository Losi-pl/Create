package com.losi.create

import com.losi.create.assets.ResourceSpace

class ModSpace {
    companion object
    {
        val modules: Map<String, ModSpace> = mapOf("create" to ModSpace(Version.version, "create"))

    }

    val version: String
    val identity: String
    val resourceSpace: ResourceSpace

    internal constructor(version: String, identity: String) {
        this.version = version
        this.identity = identity
        resourceSpace = ResourceSpace()
    }
}