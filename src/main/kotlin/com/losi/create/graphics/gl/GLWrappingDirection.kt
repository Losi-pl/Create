package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

/**The direction of wrapping in a texture*/
enum class GLWrappingDirection(val gl: Int) {
    /**`GL_TEXTURE_WRAP_S`*/
    Horizontal(GL11.GL_TEXTURE_WRAP_S),
    /**`GL_TEXTURE_WRAP_T`*/
    Vertical(GL11.GL_TEXTURE_WRAP_T),
    /**`GL_TEXTURE_WRAP_R`*/
    Depth(GL12.GL_TEXTURE_WRAP_R)
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = GLWrappingDirection.entries.find { it.gl == gl }
    }
}