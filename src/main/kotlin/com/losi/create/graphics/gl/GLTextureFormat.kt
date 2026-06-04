package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

/**OpenGL texture data format, specifies how the data is structured and formated*/
@Suppress("RedundantSuppression", "unused")
enum class GLTextureFormat(val gl: Int) {
    // --- Low Precision (Legacy / Mobile) ---
    /**Low Precision: 4-bit per channel (16-bit total). Very low quality.*/
    RGBA4(GL11.GL_RGBA4),
    /**Low Precision: 5-bit RGB, 1-bit Alpha. Binary alpha only.*/
    RGB5A1(GL11.GL_RGB5_A1),
    /**Low Precision: 5-6-5 for Read, Green and Blue, no Alpha.*/
    RGB565(GL41.GL_RGB565),

    // --- Standard 8-bit (Most Common) ---
    /**Standard: 8-bit for Read, Green, Blue and Alpha (32-bit total).*/
    RGBA8(GL11.GL_RGBA8),
    /**Standard: 8-bit sRGB (Gamma corrected). Use for standard images.*/
    @Suppress("SpellCheckingInspection")
    SRGB8_ALPHA8(GL21.GL_SRGB8_ALPHA8),
    /**Standard: 8-bit RGB for Read, Green and Blue.*/
    RGB8(GL11.GL_RGB8),

    // --- 16-bit (High Precision / HDR) ---
    /**High Precision: 16-bit integer for Read, Green, Blue and Alpha.*/
    RGBA16(GL11.GL_RGBA16),
    /**High Precision: 16-bit integer for Read, Green and Blue.*/
    RGB16(GL11.GL_RGB16),

    // --- Floating Point (HDR / Rendering Targets) ---
    /**HDR: 16-bit float for Read, Green, Blue and Alpha.*/
    RGBA16F(GL30.GL_RGBA16F),
    /**HDR: 16-bit float for Read, Green and Blue.*/
    RGB16F(GL30.GL_RGB16F),

    // --- 32-bit Float (Full HDR / Compute) ---
    /**Full HDR:  32-bit float for Read, Green, Blue and Alpha.*/
    RGBA32F(GL30.GL_RGBA32F),
    /**Full HDR:  32-bit float for Read, Green and Blue.*/
    RGB32F(GL30.GL_RGB32F),

    // --- Specialized / Extensions ---
    /**Specialized: 8-bit for Blue, Green, Read and Alpha, standard order flipped.*/
    @Suppress("SpellCheckingInspection")
    BGRA8(EXTBGRA.GL_BGRA_EXT),

    // --- Single/Double Channel Variations ---
    /**Double Channel: 8-bit Red only (Grayscale).*/
    R8(GL30.GL_R8),
    /**Double Channel: 8-bit Red + Green.*/
    RG8(GL30.GL_RG8),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}