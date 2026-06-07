package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL20.*

/**The mode of texture wrapping then exiting the border*/
enum class TextureWrappingMode(val gl: Int) {
    /**Repeats the texture.*/
    Repeat(GL_REPEAT),
    /**Repeats the texture, but mirrored with odd coordinates.*/
    MirroredRepeat(GL_MIRRORED_REPEAT),
    /**Clamps the coordinates between `0.0` and `1.0`.*/
    ClampToEdge(GL_CLAMP_TO_EDGE),
    /**Will give the coordinates outside of `0.0` and `1.0` a specified border color.*/
    ClampToBorder(GL_CLAMP_TO_BORDER)
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}