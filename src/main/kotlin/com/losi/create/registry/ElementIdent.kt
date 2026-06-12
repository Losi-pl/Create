package com.losi.create.registry

import com.losi.create.ModSpace
import com.losi.create.utility.findFirst

data class ElementIdent(val space: ModSpace, val identity: String)
{
    constructor(identity: String): this(
        ModSpace.modules.findFirst { identity.startsWith(it.key) && identity[it.key.length] == ':' }?.value
            ?: throw NullPointerException("Mod with identity \"${identity.substringBefore(':')}\" was not found"),
        identity.substringAfter(':'))
}