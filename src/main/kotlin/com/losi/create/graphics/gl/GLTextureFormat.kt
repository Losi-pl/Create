package com.losi.create.graphics.gl

import org.lwjgl.opengl.EXTBGRA
import org.lwjgl.opengl.GL40.*
import org.lwjgl.opengl.GL41

@Suppress("RedundantSuppression", "unused")
enum class GLTextureFormat(val gl: Int) {
    // --- Low Precision (Legacy / Mobile) ---
    RGBA4(GL_RGBA4),          // 4-bit per channel (16-bit total). Very low quality.
    RGB5A1(GL_RGB5_A1),       // 5-bit RGB, 1-bit Alpha. Binary alpha only.
    RGB565(GL41.GL_RGB565),   // 5-6-5 RGB, no Alpha. Common for mobile backgrounds.

    // --- Standard 8-bit (Most Common) ---
    RGBA8(GL_RGBA8),          // 8-bit per channel (32-bit total). Standard linear.
    @Suppress("SpellCheckingInspection")
    SRGB8_ALPHA8(GL_SRGB8_ALPHA8), // 8-bit sRGB (Gamma corrected). Use for standard images.
    RGB8(GL_RGB8),            // 8-bit RGB, no Alpha. Saves memory.

    // --- 16-bit (High Precision / HDR) ---
    RGBA16(GL_RGBA16),        // 16-bit integer per channel. High precision color.
    RGB16(GL_RGB16),          // 16-bit integer RGB.

    // --- Floating Point (HDR / Rendering Targets) ---
    RGBA16F(GL_RGBA16F),      // 16-bit float per channel. HDR, physics, intermediate buffers.
    RGB16F(GL_RGB16F),        // 16-bit float RGB.

    // --- 32-bit Float (Full HDR / Compute) ---
    RGBA32F(GL_RGBA32F),      // 32-bit float per channel. Full HDR, floating point math.
    RGB32F(GL_RGB32F),        // 32-bit float RGB.

    // --- Specialized / Extensions ---
    // BGRA8 is often used on Windows/Android where data is stored in BGRA order.
    @Suppress("SpellCheckingInspection")
    BGRA8(EXTBGRA.GL_BGRA_EXT),       // 8-bit BGRA. Memory order flipped (B,G,R,A).

    // --- Single/Double Channel Variations ---
    R8(GL_R8),                // 8-bit Red only (Grayscale).
    RG8(GL_RG8),              // 8-bit Red + Green.
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun fromConstant(const: Int): GLTextureFormat? =
            entries.find { it.gl == const }
    }
}