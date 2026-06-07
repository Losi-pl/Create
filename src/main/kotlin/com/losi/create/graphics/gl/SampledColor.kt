package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class SampledColor(val gl: Int, val glName: String) {
    Zero (GL11C.GL_ZERO,  "GL_ZERO"),
    One  (GL11C.GL_ONE,   "GL_ONE"),
    Red  (GL11C.GL_RED,   "GL_RED"),
    Green(GL11C.GL_GREEN, "GL_GREEN"),
    Blue (GL11C.GL_BLUE,  "GL_BLUE"),
    Alpha(GL11C.GL_ALPHA, "GL_ALPHA"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}