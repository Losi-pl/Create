package com.losi.create.world

import com.losi.create.registry.ElementRegister

object Realms {
    val manifest = ElementRegister<Realm>()

    val Earth = object: Realm() { }
}