package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class ComparisonFunction(val gl: Int, val glName: String) {
    /**Will pass the test when the incoming value is less than or equal to the reference value.
     *
     * `GL_LEQUAL`*/
    Lequal(GL11.GL_LEQUAL, "GL_LEQUAL"),
    /**Will pass the test when the incoming value is greater than or equal to the reference value.
     *
     * `GL_GEQUAL`*/
    Gequal(GL11.GL_GEQUAL, "GL_GEQUAL"),
    /**Will pass the test when the incoming value is strictly less than the reference value.
     *
     * `GL_LESS`*/
    Less(GL11.GL_LESS, "GL_LESS"),
    /**Will pass the test when the incoming value is strictly greater than the reference value.
     *
     * `GL_GREATER`*/
    Greater(GL11.GL_GREATER, "GL_GREATER"),
    /**Will pass the test when the incoming value is equal to the reference value.
     *
     * `GL_EQUAL`*/
    Equal(GL11.GL_EQUAL, "GL_EQUAL"),
    /**Will pass the test when the incoming value is not equal to the reference value.
     *
     * `GL_NOTEQUAL`*/
    NotEqual(GL11.GL_NOTEQUAL, "GL_NOTEQUAL"),
    /**Will always pass the test, regardless of the incoming value.
     *
     * `GL_ALWAYS`*/
    Always(GL11.GL_ALWAYS, "GL_ALWAYS"),
    /**Will never pass the test, regardless of the incoming value.
     *
     * `GL_NEVER`*/
    Never(GL11.GL_NEVER, "GL_NEVER"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}