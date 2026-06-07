package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class FragmentChannel(val gl: Int, val glName: String) {
    Red  (GL33C.GL_TEXTURE_SWIZZLE_R, "GL_TEXTURE_SWIZZLE_R"),
    Green(GL33C.GL_TEXTURE_SWIZZLE_G, "GL_TEXTURE_SWIZZLE_G"),
    Blue (GL33C.GL_TEXTURE_SWIZZLE_B, "GL_TEXTURE_SWIZZLE_B"),
    Alpha(GL33C.GL_TEXTURE_SWIZZLE_A, "GL_TEXTURE_SWIZZLE_A"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}