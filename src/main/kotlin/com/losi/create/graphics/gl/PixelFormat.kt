@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class PixelFormat(val gl: Int, val glName: String) {
    RGB(GL11.GL_RGB, "GL_RGB"),
    BGR(GL12.GL_BGR, "GL_BGR"),
    RGBA(GL11.GL_RGBA, "GL_RGBA"),
    BGRA(GL12.GL_BGRA, "GL_BGRA"),

    // For completeness
    RED(GL11.GL_RED, "GL_RED"),
    GREEN(GL11.GL_GREEN, "GL_GREEN"),
    BLUE(GL11.GL_BLUE, "GL_BLUE"),
    ALPHA(GL11.GL_ALPHA, "GL_ALPHA"),
    LUMINANCE(GL11.GL_LUMINANCE, "GL_LUMINANCE"),
    LUMINANCE_ALPHA(GL11.GL_LUMINANCE_ALPHA, "GL_LUMINANCE_ALPHA")
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}