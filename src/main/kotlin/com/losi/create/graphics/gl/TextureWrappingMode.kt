package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

/**The mode of texture wrapping then exiting the border*/
enum class TextureWrappingMode(val gl: Int, val glName: String) {
    /**Repeats the texture.
     *
     * `GL_REPEAT`*/
    Repeat(GL11.GL_REPEAT,                  "GL_REPEAT"),
    /**Repeats the texture, but mirrored with odd coordinates.
     *
     * `GL_MIRRORED_REPEAT`*/
    MirroredRepeat(GL14.GL_MIRRORED_REPEAT, "GL_MIRRORED_REPEAT"),
    /**Clamps the coordinates between `0.0` and `1.0`.
     *
     * `GL_CLAMP_TO_EDGE`*/
    ClampToEdge(GL12.GL_CLAMP_TO_EDGE,      "GL_CLAMP_TO_EDGE"),
    /**Will give the coordinates outside of `0.0` and `1.0` a specified border color.
     *
     * `GL_CLAMP_TO_BORDER`*/
    ClampToBorder(GL13.GL_CLAMP_TO_BORDER,  "GL_CLAMP_TO_BORDER"),
    /**Will mirror the texture coordinate once when outside `0.0` and `1.0`, then clamp the mirrored coordinate to the nearest edge.
     *
     * `GL_MIRROR_CLAMP_TO_EDGE`*/
    ClampToEdgeMirror(ARBTextureMirrorClampToEdge.GL_MIRROR_CLAMP_TO_EDGE, "GL_MIRROR_CLAMP_TO_EDGE"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}