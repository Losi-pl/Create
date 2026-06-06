@file:Suppress("RedundantSuppression", "unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

/** OpenGL internal texture format, used as the `internalFormat` parameter in `glTexImage2D`, `glTexStorage2D`, etc. */
enum class GLInternalFormat(val gl: Int, val glName: String) {
    // --- Low Precision (Legacy / Mobile) ---
    /**4‑bit per channel RGBA (16 bits total). Very low quality, legacy/mobile.
     *
     * `GL_RGBA4`*/
    RGBA4(GL11.GL_RGBA4, "GL_RGBA4"),
    /** 5‑bit RGB, 1‑bit alpha (binary alpha only). Low precision.
     *
     * `GL_RGB5_A1`*/
    RGB5A1(GL11.GL_RGB5_A1, "GL_RGB5_A1"),
    /** 5‑bit red, 6‑bit green, 5‑bit blue (16 bits total). Common low‑precision RGB.
     *
     * `GL_RGB565`*/
    RGB565(GL41.GL_RGB565, "GL_RGB565"),

    // --- Standard 8-bit (Most Common) ---
    /** 8 bits per channel RGBA (32 bits total). The most common standard format.
     *
     * `GL_RGBA8`*/
    RGBA8(GL11.GL_RGBA8, "GL_RGBA8"),
    /** 8‑bit sRGB (gamma corrected) with 8‑bit alpha. Use for standard images.
     *
     * `GL_SRGB8_ALPHA8`*/
    SRGB8_ALPHA8(GL21.GL_SRGB8_ALPHA8, "GL_SRGB8_ALPHA8"),
    /** 8 bits per channel RGB (24 bits total). No alpha.
     *
     * `GL_RGB8`*/
    RGB8(GL11.GL_RGB8, "GL_RGB8"),

    // --- 16-bit (High Precision / HDR) ---
    /** 16‑bit integer per channel RGBA (64 bits total). High precision integer storage.
     *
     * `GL_RGBA16`*/
    RGBA16(GL11.GL_RGBA16, "GL_RGBA16"),
    /** 16‑bit integer per channel RGB (48 bits total). High precision integer storage.
     *
     * `GL_RGB16`*/
    RGB16(GL11.GL_RGB16, "GL_RGB16"),

    // --- Floating Point (HDR / Rendering Targets) ---
    /** 16‑bit float per channel RGBA (64 bits total). Half‑float HDR rendering.
     *
     * `GL_RGBA16F`*/
    RGBA16F(GL30.GL_RGBA16F, "GL_RGBA16F"),
    /** 16‑bit float per channel RGB (48 bits total). Half‑float HDR rendering.
     *
     * `GL_RGB16F`*/
    RGB16F(GL30.GL_RGB16F, "GL_RGB16F"),

    // --- 32-bit Float (Full HDR / Compute) ---
    /** 32‑bit float per channel RGBA (128 bits total). Full HDR / compute.
     *
     * `GL_RGBA32F`*/
    RGBA32F(GL30.GL_RGBA32F, "GL_RGBA32F"),
    /** 32‑bit float per channel RGB (96 bits total). Full HDR / compute.
     *
     * `GL_RGB32F`*/
    RGB32F(GL30.GL_RGB32F, "GL_RGB32F"),

    // --- Specialized / Extensions ---
    /** 8‑bit BGRA (blue, green, red, alpha) order. Extension for direct framebuffer readback
     *
     * `GL_BGRA_EXT`*/
    BGRA8(EXTBGRA.GL_BGRA_EXT, "GL_BGRA_EXT"),

    // --- Single/Double Channel Variations ---
    /** Single channel: 8‑bit red (grayscale). Also used for luminance.
     *
     * `GL_R8`*/
    R8(GL30.GL_R8, "GL_R8"),
    /** Two channels: 8‑bit red, 8‑bit green.
     *
     * `GL_RG8`*/
    RG8(GL30.GL_RG8, "GL_RG8"),

    // ---- Normalized integer (core) ----
    /** Single channel, 8‑bit signed normalized red.
     *
     * `GL_R8_SNORM`*/
    R8_SNORM(GL31.GL_R8_SNORM, "GL_R8_SNORM"),
    /** Single channel, 16‑bit unsigned normalized red.
     *
     * `GL_R16`*/
    R16(GL30.GL_R16, "GL_R16"),
    /** Single channel, 16‑bit signed normalized red.
     *
     * `GL_R16_SNORM`*/
    R16_SNORM(GL43.GL_R16_SNORM, "GL_R16_SNORM"),
    /** Single channel, 32‑bit float red.
     *
     * `GL_R32F`*/
    R32F(GL30.GL_R32F, "GL_R32F"),
    /** Two channels, 8‑bit signed normalized RG.
     *
     * `GL_RG8_SNORM`*/
    RG8_SNORM(GL31.GL_RG8_SNORM, "GL_RG8_SNORM"),
    /** Two channels, 16‑bit unsigned normalized RG.
     *
     * `GL_RG16`*/
    RG16(GL30.GL_RG16, "GL_RG16"),
    /** Two channels, 16‑bit signed normalized RG.
     *
     * `GL_RG16_SNORM`*/
    RG16_SNORM(GL31.GL_RG16_SNORM, "GL_RG16_SNORM"),
    /** Two channels, 32‑bit float RG.
     *
     * `GL_RG32F`*/
    RG32F(GL30.GL_RG32F, "GL_RG32F"),
    /** Three channels, 8‑bit signed normalized RGB.
     *
     * `GL_RGB8_SNORM`*/
    RGB8_SNORM(GL31.GL_RGB8_SNORM, "GL_RGB8_SNORM"),
    /** Three channels, 16‑bit signed normalized RGB.
     *
     * `GL_RGB16_SNORM`*/
    RGB16_SNORM(GL31.GL_RGB16_SNORM, "GL_RGB16_SNORM"),
    /** Four channels, 8‑bit signed normalized RGBA.
     *
     * `GL_RGBA8_SNORM`*/
    RGBA8_SNORM(GL31.GL_RGBA8_SNORM, "GL_RGBA8_SNORM"),
    /** Four channels, 16‑bit signed normalized RGBA.
     *
     * `GL_RGBA16_SNORM`*/
    RGBA16_SNORM(GL31.GL_RGBA16_SNORM, "GL_RGBA16_SNORM"),

    // ---- Pure integer (signed/unsigned) ----
    /** Single channel, 8‑bit signed integer.
     *
     * `GL_R8I`*/
    R8I(GL30.GL_R8I, "GL_R8I"),
    /** Single channel, 8‑bit unsigned integer.
     *
     * `GL_R8UI`*/
    R8UI(GL30.GL_R8UI, "GL_R8UI"),
    /** Single channel, 16‑bit signed integer.
     *
     * `GL_R16I`*/
    R16I(GL30.GL_R16I, "GL_R16I"),
    /** Single channel, 16‑bit unsigned integer.
     *
     * `GL_R16UI`*/
    R16UI(GL30.GL_R16UI, "GL_R16UI"),
    /** Single channel, 32‑bit signed integer.
     *
     * `GL_R32I`*/
    R32I(GL30.GL_R32I, "GL_R32I"),
    /** Single channel, 32‑bit unsigned integer.
     *
     * `GL_R32UI`*/
    R32UI(GL30.GL_R32UI, "GL_R32UI"),
    /** Two channels, 8‑bit signed integer RG.
     *
     * `GL_RG8I`*/
    RG8I(GL30.GL_RG8I, "GL_RG8I"),
    /** Two channels, 8‑bit unsigned integer RG.
     *
     * `GL_RG8UI`*/
    RG8UI(GL30.GL_RG8UI, "GL_RG8UI"),
    /** Two channels, 16‑bit signed integer RG.
     *
     * `GL_RG16I`*/
    RG16I(GL30.GL_RG16I, "GL_RG16I"),
    /** Two channels, 16‑bit unsigned integer RG.
     *
     * `GL_RG16UI`*/
    RG16UI(GL30.GL_RG16UI, "GL_RG16UI"),
    /** Two channels, 32‑bit signed integer RG.
     *
     * `GL_RG32I`*/
    RG32I(GL30.GL_RG32I, "GL_RG32I"),
    /** Two channels, 32‑bit unsigned integer RG.
     *
     * `GL_RG32UI`*/
    RG32UI(GL30.GL_RG32UI, "GL_RG32UI"),
    /** Three channels, 8‑bit signed integer RGB.
     *
     * `GL_RGB8I`*/
    RGB8I(GL30.GL_RGB8I, "GL_RGB8I"),
    /** Three channels, 8‑bit unsigned integer RGB.
     *
     * `GL_RGB8UI`*/
    RGB8UI(GL30.GL_RGB8UI, "GL_RGB8UI"),
    /** Three channels, 16‑bit signed integer RGB.
     *
     * `GL_RGB16I`*/
    RGB16I(GL30.GL_RGB16I, "GL_RGB16I"),
    /** Three channels, 16‑bit unsigned integer RGB.
     *
     * `GL_RGB16UI`*/
    RGB16UI(GL30.GL_RGB16UI, "GL_RGB16UI"),
    /** Three channels, 32‑bit signed integer RGB.
     *
     * `GL_RGB32I`*/
    RGB32I(GL30.GL_RGB32I, "GL_RGB32I"),
    /** Three channels, 32‑bit unsigned integer RGB.
     *
     * `GL_RGB32UI`*/
    RGB32UI(GL30.GL_RGB32UI, "GL_RGB32UI"),
    /** Four channels, 8‑bit signed integer RGBA.
     *
     * `GL_RGBA8I`*/
    RGBA8I(GL30.GL_RGBA8I, "GL_RGBA8I"),
    /** Four channels, 8‑bit unsigned integer RGBA.
     *
     * `GL_RGBA8UI`*/
    RGBA8UI(GL30.GL_RGBA8UI, "GL_RGBA8UI"),
    /** Four channels, 16‑bit signed integer RGBA.
     *
     * `GL_RGBA16I`*/
    RGBA16I(GL30.GL_RGBA16I, "GL_RGBA16I"),
    /** Four channels, 16‑bit unsigned integer RGBA.
     *
     * `GL_RGBA16UI`*/
    RGBA16UI(GL30.GL_RGBA16UI, "GL_RGBA16UI"),
    /** Four channels, 32‑bit signed integer RGBA.
     *
     * `GL_RGBA32I`*/
    RGBA32I(GL30.GL_RGBA32I, "GL_RGBA32I"),
    /** Four channels, 32‑bit unsigned integer RGBA.
     *
     * `GL_RGBA32UI`*/
    RGBA32UI(GL30.GL_RGBA32UI, "GL_RGBA32UI"),

    // ---- Packed / mixed precision ----
    /** 32‑bit packed: 9‑bit mantissa for RGB, shared 5‑bit exponent (HDR).
     *
     * `GL_RGB9_E5`*/
    RGB9_E5(GL30.GL_RGB9_E5, "GL_RGB9_E5"),
    /** 32‑bit packed: 10‑bit RGB, 2‑bit alpha (unsigned normalized).
     *
     * `GL_RGB10_A2`*/
    RGB10_A2(GL30.GL_RGB10_A2, "GL_RGB10_A2"),
    /** 32‑bit packed: 10‑bit unsigned integer RGB, 2‑bit unsigned integer alpha.
     *
     * `GL_RGB10_A2UI`*/
    RGB10_A2UI(GL33.GL_RGB10_A2UI, "GL_RGB10_A2UI"),
    /** 32‑bit packed: 10‑bit red, 11‑bit green, 11‑bit blue floating‑point (HDR).
     *
     * `GL_R11F_G11F_B10F`*/
    R11F_G11F_B10F(GL30.GL_R11F_G11F_B10F, "GL_R11F_G11F_B10F"),

    // ---- sRGB (gamma correct) ----
    /** 8‑bit sRGB (gamma corrected) without alpha.
     *
     * `GL_SRGB8`*/
    SRGB8(GL30.GL_SRGB8, "GL_SRGB8"),

    // ---- Depth & Stencil ----
    /** 16‑bit depth component.
     *
     * `GL_DEPTH_COMPONENT16`*/
    DEPTH_COMPONENT16(GL14.GL_DEPTH_COMPONENT16, "GL_DEPTH_COMPONENT16"),
    /** 24‑bit depth component.
     *
     * `GL_DEPTH_COMPONENT24`*/
    DEPTH_COMPONENT24(GL14.GL_DEPTH_COMPONENT24, "GL_DEPTH_COMPONENT24"),
    /** 32‑bit depth component (unsigned integer).
     *
     * `GL_DEPTH_COMPONENT32`*/
    DEPTH_COMPONENT32(GL14.GL_DEPTH_COMPONENT32, "GL_DEPTH_COMPONENT32"),
    /** 32‑bit floating‑point depth component.
     *
     * `GL_DEPTH_COMPONENT32F`*/
    DEPTH_COMPONENT32F(GL30.GL_DEPTH_COMPONENT32F, "GL_DEPTH_COMPONENT32F"),
    /** 24‑bit depth + 8‑bit stencil (combined).
     *
     * `GL_DEPTH24_STENCIL8`*/
    DEPTH24_STENCIL8(GL30.GL_DEPTH24_STENCIL8, "GL_DEPTH24_STENCIL8"),
    /** 32‑bit floating‑point depth + 8‑bit stencil.
     *
     * `GL_DEPTH32F_STENCIL8`*/
    DEPTH32F_STENCIL8(GL32.GL_DEPTH32F_STENCIL8, "GL_DEPTH32F_STENCIL8"),
    /** 1‑bit stencil index.
     *
     * `GL_STENCIL_INDEX1`*/
    STENCIL_INDEX1(GL30.GL_STENCIL_INDEX1, "GL_STENCIL_INDEX1"),
    /** 4‑bit stencil index.
     *
     * `GL_STENCIL_INDEX4`*/
    STENCIL_INDEX4(GL30.GL_STENCIL_INDEX4, "GL_STENCIL_INDEX4"),
    /** 8‑bit stencil index.
     *
     * `GL_STENCIL_INDEX8`*/
    STENCIL_INDEX8(GL30.GL_STENCIL_INDEX8, "GL_STENCIL_INDEX8"),
    /** 16‑bit stencil index.
     *
     * `GL_STENCIL_INDEX16`*/
    STENCIL_INDEX16(GL30.GL_STENCIL_INDEX16, "GL_STENCIL_INDEX16"),

    // ---- Compressed S3TC (DXT) ----
    /** DXT1 compression: RGB (4:1 compression).
     *
     * `GL_COMPRESSED_RGB_S3TC_DXT1_EXT`*/
    COMPRESSED_RGB_S3TC_DXT1_EXT(EXTTextureCompressionS3TC.GL_COMPRESSED_RGB_S3TC_DXT1_EXT, "GL_COMPRESSED_RGB_S3TC_DXT1_EXT"),
    /** DXT1 compression: RGBA with 1‑bit alpha (4:1 compression).
     *
     * `GL_COMPRESSED_RGBA_S3TC_DXT1_EXT`*/
    COMPRESSED_RGBA_S3TC_DXT1_EXT(EXTTextureCompressionS3TC.GL_COMPRESSED_RGBA_S3TC_DXT1_EXT, "GL_COMPRESSED_RGBA_S3TC_DXT1_EXT"),
    /** DXT3 compression: RGBA with explicit alpha (4:1 compression).
     *
     * `GL_COMPRESSED_RGBA_S3TC_DXT3_EXT`*/
    COMPRESSED_RGBA_S3TC_DXT3_EXT(EXTTextureCompressionS3TC.GL_COMPRESSED_RGBA_S3TC_DXT3_EXT, "GL_COMPRESSED_RGBA_S3TC_DXT3_EXT"),
    /** DXT5 compression: RGBA with interpolated alpha (4:1 compression).
     *
     * `GL_COMPRESSED_RGBA_S3TC_DXT5_EXT`*/
    COMPRESSED_RGBA_S3TC_DXT5_EXT(EXTTextureCompressionS3TC.GL_COMPRESSED_RGBA_S3TC_DXT5_EXT, "GL_COMPRESSED_RGBA_S3TC_DXT5_EXT"),
    /** DXT1 sRGB compressed RGB.
     *
     * `GL_COMPRESSED_SRGB_S3TC_DXT1_EXT`*/
    COMPRESSED_SRGB_S3TC_DXT1_EXT(EXTTextureSRGB.GL_COMPRESSED_SRGB_S3TC_DXT1_EXT, "GL_COMPRESSED_SRGB_S3TC_DXT1_EXT"),
    /** DXT1 sRGB compressed RGBA (1‑bit alpha).
     *
     * `GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT`*/
    COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT(EXTTextureSRGB.GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT, "GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT"),
    /** DXT3 sRGB compressed RGBA (explicit alpha).
     *
     * `GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT`*/
    COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT(EXTTextureSRGB.GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT, "GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT"),
    /** DXT5 sRGB compressed RGBA (interpolated alpha).
     *
     * `GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT`*/
    COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT(EXTTextureSRGB.GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT, "GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT"),

    // ---- Compressed RGTC ----
    /** Single‑channel unsigned red (BC4). Good for normal maps.
     *
     * `GL_COMPRESSED_RED_RGTC1`*/
    COMPRESSED_RED_RGTC1(GL30.GL_COMPRESSED_RED_RGTC1, "GL_COMPRESSED_RED_RGTC1"),
    /** Single‑channel signed red (BC4).
     *
     * `GL_COMPRESSED_SIGNED_RED_RGTC1`*/
    COMPRESSED_SIGNED_RED_RGTC1(GL30.GL_COMPRESSED_SIGNED_RED_RGTC1, "GL_COMPRESSED_SIGNED_RED_RGTC1"),
    /** Two‑channel unsigned RG (BC5). Good for tangent space normals.
     *
     * `GL_COMPRESSED_RG_RGTC2`*/
    COMPRESSED_RG_RGTC2(GL30.GL_COMPRESSED_RG_RGTC2, "GL_COMPRESSED_RG_RGTC2"),
    /** Two‑channel signed RG (BC5).
     *
     * `GL_COMPRESSED_SIGNED_RG_RGTC2`*/
    COMPRESSED_SIGNED_RG_RGTC2(GL30.GL_COMPRESSED_SIGNED_RG_RGTC2, "GL_COMPRESSED_SIGNED_RG_RGTC2"),

    // ---- Compressed BPTC (BC6H/BC7) ----
    /** BC7 block compression (8 bits per pixel). High quality RGBA.
     *
     * `GL_COMPRESSED_RGBA_BPTC_UNORM_ARB`*/
    COMPRESSED_RGBA_BPTC_UNORM_ARB(ARBTextureCompressionBPTC.GL_COMPRESSED_RGBA_BPTC_UNORM_ARB, "GL_COMPRESSED_RGBA_BPTC_UNORM_ARB"),
    /** BC7 sRGB compression.
     *
     * `GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB`*/
    COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB(ARBTextureCompressionBPTC.GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB, "GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB"),
    /** BC6H signed floating‑point (HDR RGB).
     *
     * `GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB`*/
    COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB(ARBTextureCompressionBPTC.GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB, "GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB"),
    /** BC6H unsigned floating‑point (HDR RGB).
     *
     * `GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB`*/
    COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB(ARBTextureCompressionBPTC.GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB, "GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB"),

    // ---- Compressed ETC2 / EAC ----
    /** ETC2 RGB8 compression.
     *
     * `GL_COMPRESSED_RGB8_ETC2`*/
    COMPRESSED_RGB8_ETC2(GL43.GL_COMPRESSED_RGB8_ETC2, "GL_COMPRESSED_RGB8_ETC2"),
    /** ETC2 RGBA8 (EAC) compression.
     *
     * `GL_COMPRESSED_RGBA8_ETC2_EAC`*/
    COMPRESSED_RGBA8_ETC2_EAC(GL43.GL_COMPRESSED_RGBA8_ETC2_EAC, "GL_COMPRESSED_RGBA8_ETC2_EAC"),
    /** ETC2 RGB8 with punchthrough alpha (1‑bit alpha).
     *
     * `GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2`*/
    COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2(GL43.GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2, "GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2"),
    /** ETC2 sRGB8 compression.
     *
     * `GL_COMPRESSED_SRGB8_ETC2`*/
    COMPRESSED_SRGB8_ETC2(GL43.GL_COMPRESSED_SRGB8_ETC2, "GL_COMPRESSED_SRGB8_ETC2"),
    /** ETC2 sRGB8 alpha8 (EAC) compression.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC`*/
    COMPRESSED_SRGB8_ALPHA8_ETC2_EAC(GL43.GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC, "GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC"),
    /** ETC2 sRGB8 punchthrough alpha1.
     *
     * `GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2`*/
    COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2(GL43.GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2, "GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2"),

    // ---- Compressed ASTC (KHR) ----

    // RGBA ASTC (LDR)
    /** ASTC LDR: 4x4 block (8 bits/pixel).
     *
     * `GL_COMPRESSED_RGBA_ASTC_4x4_KHR`*/
    COMPRESSED_RGBA_ASTC_4X4_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_4x4_KHR, "GL_COMPRESSED_RGBA_ASTC_4x4_KHR"),
    /** ASTC LDR: 5x4 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_5x4_KHR`*/
    COMPRESSED_RGBA_ASTC_5X4_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_5x4_KHR, "GL_COMPRESSED_RGBA_ASTC_5x4_KHR"),
    /** ASTC LDR: 5x5 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_5x5_KHR`*/
    COMPRESSED_RGBA_ASTC_5X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_5x5_KHR, "GL_COMPRESSED_RGBA_ASTC_5x5_KHR"),
    /** ASTC LDR: 6x5 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_6x5_KHR`*/
    COMPRESSED_RGBA_ASTC_6X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_6x5_KHR, "GL_COMPRESSED_RGBA_ASTC_6x5_KHR"),
    /** ASTC LDR: 6x6 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_6x6_KHR`*/
    COMPRESSED_RGBA_ASTC_6X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_6x6_KHR, "GL_COMPRESSED_RGBA_ASTC_6x6_KHR"),
    /** ASTC LDR: 8x5 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_8x5_KHR`*/
    COMPRESSED_RGBA_ASTC_8X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_8x5_KHR, "GL_COMPRESSED_RGBA_ASTC_8x5_KHR"),
    /** ASTC LDR: 8x6 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_8x6_KHR`*/
    COMPRESSED_RGBA_ASTC_8X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_8x6_KHR, "GL_COMPRESSED_RGBA_ASTC_8x6_KHR"),
    /** ASTC LDR: 8x8 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_8x8_KHR`*/
    COMPRESSED_RGBA_ASTC_8X8_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_8x8_KHR, "GL_COMPRESSED_RGBA_ASTC_8x8_KHR"),
    /** ASTC LDR: 10x5 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_10x5_KHR`*/
    COMPRESSED_RGBA_ASTC_10X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_10x5_KHR, "GL_COMPRESSED_RGBA_ASTC_10x5_KHR"),
    /** ASTC LDR: 10x6 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_10x6_KHR`*/
    COMPRESSED_RGBA_ASTC_10X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_10x6_KHR, "GL_COMPRESSED_RGBA_ASTC_10x6_KHR"),
    /** ASTC LDR: 10x8 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_10x8_KHR`*/
    COMPRESSED_RGBA_ASTC_10X8_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_10x8_KHR, "GL_COMPRESSED_RGBA_ASTC_10x8_KHR"),
    /** ASTC LDR: 10x10 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_10x10_KHR`*/
    COMPRESSED_RGBA_ASTC_10X10_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_10x10_KHR, "GL_COMPRESSED_RGBA_ASTC_10x10_KHR"),
    /** ASTC LDR: 12x10 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_12x10_KHR`*/
    COMPRESSED_RGBA_ASTC_12X10_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_12x10_KHR, "GL_COMPRESSED_RGBA_ASTC_12x10_KHR"),
    /** ASTC LDR: 12x12 block.
     *
     * `GL_COMPRESSED_RGBA_ASTC_12x12_KHR`*/
    COMPRESSED_RGBA_ASTC_12X12_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_RGBA_ASTC_12x12_KHR, "GL_COMPRESSED_RGBA_ASTC_12x12_KHR"),

    // sRGB ASTC
    /** ASTC sRGB: 4x4 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_4x4_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_4X4_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_4x4_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_4x4_KHR"),
    /** ASTC sRGB: 5x4 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x4_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_5X4_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x4_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x4_KHR"),
    /** ASTC sRGB: 5x5 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x5_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_5X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x5_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x5_KHR"),
    /** ASTC sRGB: 6x5 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x5_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_6X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x5_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x5_KHR"),
    /** ASTC sRGB: 6x6 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_6X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR"),
    /** ASTC sRGB: 8x5 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x5_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_8X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x5_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x5_KHR"),
    /** ASTC sRGB: 8x6 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x6_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_8X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x6_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x6_KHR"),
    /** ASTC sRGB: 8x8 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x8_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_8X8_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x8_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x8_KHR"),
    /** ASTC sRGB: 10x5 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x5_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_10X5_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x5_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x5_KHR"),
    /** ASTC sRGB: 10x6 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x6_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_10X6_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x6_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x6_KHR"),
    /** ASTC sRGB: 10x8 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x8_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_10X8_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x8_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x8_KHR"),
    /** ASTC sRGB: 10x10 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x10_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_10X10_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x10_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x10_KHR"),
    /** ASTC sRGB: 12x10 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x10_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_12X10_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x10_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x10_KHR"),
    /** ASTC sRGB: 12x12 block.
     *
     * `GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x12_KHR`*/
    COMPRESSED_SRGB8_ALPHA8_ASTC_12X12_KHR(KHRTextureCompressionASTCLDR.GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x12_KHR, "GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x12_KHR"),

    // ---- Legacy / deprecated (optional) ----
    /** Legacy luminance (single channel, deprecated).
     *
     * `GL_LUMINANCE`*/
    LUMINANCE(GL11.GL_LUMINANCE, "GL_LUMINANCE"),
    /** Legacy luminance+alpha (deprecated).
     *
     * `GL_LUMINANCE_ALPHA`*/
    LUMINANCE_ALPHA(GL11.GL_LUMINANCE_ALPHA, "GL_LUMINANCE_ALPHA"),
    /** Legacy intensity (deprecated).
     *
     * `GL_INTENSITY`*/
    INTENSITY(GL11.GL_INTENSITY, "GL_INTENSITY"),
    /** Legacy alpha only (deprecated).
     *
     * `GL_ALPHA`*/
    ALPHA(GL11.GL_ALPHA, "GL_ALPHA"),

    // ---- Generic compressed tokens (placeholder) ----
    /** Generic compressed RGB (driver selects actual scheme).
     *
     * `GL_COMPRESSED_RGB`*/
    COMPRESSED_RGB(GL13.GL_COMPRESSED_RGB, "GL_COMPRESSED_RGB"),
    /** Generic compressed RGBA (driver selects actual scheme).
     *
     * `GL_COMPRESSED_RGBA`*/
    COMPRESSED_RGBA(GL13.GL_COMPRESSED_RGBA, "GL_COMPRESSED_RGBA"),
    /** Generic compressed sRGB (driver selects actual scheme).
     *
     * `GL_COMPRESSED_SRGB`*/
    COMPRESSED_SRGB(GL21.GL_COMPRESSED_SRGB, "GL_COMPRESSED_SRGB"),
    /** Generic compressed sRGB alpha (driver selects actual scheme).
     *
     * `GL_COMPRESSED_SRGB_ALPHA`*/
    COMPRESSED_SRGB_ALPHA(GL21.GL_COMPRESSED_SRGB_ALPHA, "GL_COMPRESSED_SRGB_ALPHA"),
    /** Generic compressed single‑channel red (driver selects actual scheme).
     *
     * `GL_COMPRESSED_RED`*/
    COMPRESSED_RED(GL30.GL_COMPRESSED_RED, "GL_COMPRESSED_RED"),
    /** Generic compressed two‑channel RG (driver selects actual scheme).
     *
     * `GL_COMPRESSED_RG`*/
    COMPRESSED_RG(GL30.GL_COMPRESSED_RG, "GL_COMPRESSED_RG"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}