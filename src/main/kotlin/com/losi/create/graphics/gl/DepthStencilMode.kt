package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class DepthStencilMode(val gl: Int, val glName:String) {
    /**The texture sample returns the depth component. The stencil part is discarded
     *
     * `GL_DEPTH_COMPONENT`*/
    Depth(GL11.GL_DEPTH_COMPONENT, "GL_DEPTH_COMPONENT"),
    /**The texture sample returns the stencil component. The depth part is discarded.
     *
     * `GL_STENCIL_INDEX`*/
    Stencil(GL11.GL_STENCIL_INDEX, "GL_STENCIL_INDEX"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}