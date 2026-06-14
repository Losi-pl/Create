package com.losi.create.world

import com.losi.create.graphics.Scene
import com.losi.create.registry.ElementRegister
import java.awt.Color

class GameSession: Scene() {

    override fun onSceneInit() {
        title = "Create"
        backgroundColor = Color.ORANGE

        //Placeholder
        ElementRegister.loadElementUuids(sequenceOf())
    }
}