package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class MinFilterMode(val gl: Int, val glName: String) {
    /**Sample the closest texel from the base level (no mipmapping) – blocky, sharp.
     *
     * `GL_NEAREST`*/
    Nearest(GL11.GL_NEAREST, "GL_NEAREST"),
    /**Bilinear filter on the base level (no mipmapping) – smooth but aliasing at distance.
     *
     * `GL_LINEAR`*/
    Linear(GL11.GL_LINEAR, "GL_LINEAR"),
    /**Pick the mipmap level closest to the required LOD, then take 1 texel from it.
     *
     * `GL_NEAREST_MIPMAP_NEAREST`*/
    NearestMipNearest(GL11.GL_NEAREST_MIPMAP_NEAREST, "GL_NEAREST_MIPMAP_NEAREST"),
    /**Pick the nearest mipmap level, then bilinearly filter within that level.
     *
     * `GL_LINEAR_MIPMAP_NEAREST`*/
    LinearMipNearest(GL11.GL_LINEAR_MIPMAP_NEAREST, "GL_LINEAR_MIPMAP_NEAREST"),
    /**Linearly interpolate between the two nearest mipmap levels, but take 1 texel from each.
     *
     * `GL_NEAREST_MIPMAP_LINEAR`*/
    NearestMipLinear(GL11.GL_NEAREST_MIPMAP_LINEAR, "GL_NEAREST_MIPMAP_LINEAR"),
    /**Bilinear filter within each of the two nearest mipmap levels, then linearly interpolate between them – smoothest transition.
     *
     * `GL_LINEAR_MIPMAP_LINEAR`*/
    LinearMipLinear(GL11.GL_LINEAR_MIPMAP_LINEAR, "GL_LINEAR_MIPMAP_LINEAR"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}