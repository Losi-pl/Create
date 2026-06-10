package com.losi.create.graphics

import com.losi.create.graphics.gl.GLSLVar
import com.losi.create.graphics.gl.TextureObject
import java.lang.ref.Cleaner

interface Texture {
    val textureTarget: GLSLVar
    val handle: TextureObject

    companion object
    {
        internal val cleaner = Cleaner.create()
    }
}