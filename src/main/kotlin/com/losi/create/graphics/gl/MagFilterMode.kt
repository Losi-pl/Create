package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class MagFilterMode(val gl: Int, val glName: String) {
    /**Selects the texel closest to the texture coordinate. Produces a blocky, pixelated look.
     *
     * `GL_NEAREST`*/
    Nearest(GL11.GL_NEAREST, "GL_NEAREST"),
    /**Performs bilinear interpolation of the four nearest texels. Results in a smoother, blurred appearance.
     *
     * `GL_LINEAR`*/
    Linear(GL11.GL_LINEAR, "GL_LINEAR"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}