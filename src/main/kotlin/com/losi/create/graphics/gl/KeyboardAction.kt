package com.losi.create.graphics.gl

import org.lwjgl.glfw.GLFW

enum class KeyboardAction(val gl: Int, val glName: String) {
    /**`GLFW_PRESS`*/
    Press(GLFW.GLFW_PRESS, "GLFW_PRESS"),
    /**`GLFW_RELEASE`*/
    Release(GLFW.GLFW_RELEASE, "GLFW_RELEASE"),
    /**`GLFW_REPEAT`*/
    Repeat(GLFW.GLFW_REPEAT, "GLFW_REPEAT");
    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}