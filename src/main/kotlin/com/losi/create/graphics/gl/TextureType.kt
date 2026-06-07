@file:Suppress("unused")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class TextureType(val gl: Int, val glName: String) {
    /**A one-dimensional texture.
     *
     * `GL_TEXTURE_1D`*/
    Texture1D(GL11.GL_TEXTURE_1D, "GL_TEXTURE_1D"),
    /**A standard two-dimensional texture.
     *
     * `GL_TEXTURE_2D`*/
    Texture2D(GL11.GL_TEXTURE_2D, "GL_TEXTURE_2D"),
    /**A three-dimensional volume texture.
     *
     * `GL_TEXTURE_3D`*/
    Texture3D(GL12.GL_TEXTURE_3D, "GL_TEXTURE_3D"),
    /**An array of [Texture1D]'s
     *
     * `GL_TEXTURE_1D_ARRAY`*/
    Texture1DArray(GL30.GL_TEXTURE_1D_ARRAY, "GL_TEXTURE_1D_ARRAY"),
    /**An array of [Texture2D]'s.
     *
     * `GL_TEXTURE_2D_ARRAY`*/
    Texture2DArray(GL30.GL_TEXTURE_2D_ARRAY, "GL_TEXTURE_2D_ARRAY"),
    /**A non-power-of-two 2D texture without mipmaps.
     *
     * `GL_TEXTURE_RECTANGLE`*/
    TextureRectangle(GL31.GL_TEXTURE_RECTANGLE, "GL_TEXTURE_RECTANGLE"),
    /**A cube map texture with six square faces.
     *
     * `GL_TEXTURE_CUBE_MAP`*/
    TextureCube(GL13.GL_TEXTURE_CUBE_MAP, "GL_TEXTURE_CUBE_MAP"),
    /**An array of [TextureCube]'s
     *
     * `GL_TEXTURE_CUBE_MAP_ARRAY`*/
    TextureCubeMap(GL40.GL_TEXTURE_CUBE_MAP_ARRAY, "GL_TEXTURE_CUBE_MAP_ARRAY"),
    /**A texture that uses a buffer object as its data store.
     *
     * `GL_TEXTURE_BUFFER`*/
    TextureBuffer(GL31.GL_TEXTURE_BUFFER, "GL_TEXTURE_BUFFER"),
    /**A 2D multisampled texture for anti-aliasing.
     *
     * `GL_TEXTURE_2D_MULTISAMPLE`*/
    Texture2DMul(GL32.GL_TEXTURE_2D_MULTISAMPLE, "GL_TEXTURE_2D_MULTISAMPLE"),
    /**An array of [Texture2DMul]'s
     *
     * `GL_TEXTURE_2D_MULTISAMPLE_ARRAY`*/
    Texture2DMalArray(GL32.GL_TEXTURE_2D_MULTISAMPLE_ARRAY, "GL_TEXTURE_2D_MULTISAMPLE_ARRAY"),

    /**Query support for a [Texture1D].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_1D`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture1D(GL11.GL_PROXY_TEXTURE_1D, "GL_PROXY_TEXTURE_1D"),
    /**Query support for a [Texture2D].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_2D`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture2D(GL11.GL_PROXY_TEXTURE_2D, "GL_PROXY_TEXTURE_2D"),
    /**Query support for a [Texture3D].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_3D`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture3D(GL12.GL_PROXY_TEXTURE_3D, "GL_PROXY_TEXTURE_3D"),
    /**Query support for a [Texture1DArray].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_1D_ARRAY`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture1DArray(GL30.GL_PROXY_TEXTURE_1D_ARRAY, "GL_PROXY_TEXTURE_1D_ARRAY"),
    /**Query support for a [Texture2DArray].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_2D_ARRAY`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture2DArray(GL30.GL_PROXY_TEXTURE_2D_ARRAY, "GL_PROXY_TEXTURE_2D_ARRAY"),
    /**Query support for a [TextureRectangle].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_RECTANGLE`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTextureRectangle(GL31.GL_PROXY_TEXTURE_RECTANGLE, "GL_PROXY_TEXTURE_RECTANGLE"),
    /**Query support for a [TextureCube].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_CUBE_MAP`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTextureCube(GL13.GL_PROXY_TEXTURE_CUBE_MAP, "GL_PROXY_TEXTURE_CUBE_MAP"),
    /**Query support for a [TextureCubeMap].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_CUBE_MAP_ARRAY`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTextureCubeMap(GL40.GL_PROXY_TEXTURE_CUBE_MAP_ARRAY, "GL_PROXY_TEXTURE_CUBE_MAP_ARRAY"),
    /**Query support for a [Texture2DMul].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_2D_MULTISAMPLE`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture2DMul(GL32.GL_PROXY_TEXTURE_2D_MULTISAMPLE, "GL_PROXY_TEXTURE_2D_MULTISAMPLE"),
    /**Query support for a [Texture2DMalArray].
     *
     * Not recommended past `OpenGL ES 3.0`. Currently used
     * `glGetInternalformativ()`.
     *
     * `GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY`*/ @Deprecated("\nNot recommended past OpenGL ES 3.0\nUse glGetInternalformativ() instead.")
    ProxyTexture2DMalArray(GL32.GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY, "GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }

        private val typeForSampler = lazy { mapOf(
            // ===== STANDARD =====
            GLSLVar.Sampler1D               to Texture1D,
            GLSLVar.Sampler2D               to Texture2D,
            GLSLVar.Sampler3D               to Texture3D,
            GLSLVar.Sampler1DArray          to Texture1DArray,
            GLSLVar.Sampler2DArray          to Texture2DArray,
            GLSLVar.Sampler2DMul            to Texture2DMul,
            GLSLVar.SamplerCube             to TextureCube,
            GLSLVar.SamplerCubeArray        to TextureCubeMap,
            GLSLVar.SamplerBuffer           to TextureBuffer,
            GLSLVar.Sampler2DRect           to TextureRectangle,

            // ===== SHADOW SAMPLERS =====
            GLSLVar.Sampler1DShadow             to Texture1D,
            GLSLVar.Sampler2DShadow             to Texture2D,
            GLSLVar.SamplerCubeShadow           to TextureCube,
            GLSLVar.Sampler1DArrayShadow        to Texture1DArray,
            GLSLVar.Sampler2DArrayShadow        to Texture2DArray,
            GLSLVar.SamplerCubeMapArrayShadow   to TextureCubeMap,
            GLSLVar.Sampler2DRectShadow         to TextureRectangle,

            // ===== INTEGER SAMPLERS =====
            GLSLVar.IntSampler1D            to Texture1D,
            GLSLVar.IntSampler2D            to Texture2D,
            GLSLVar.IntSampler3D            to Texture3D,
            GLSLVar.IntSampler2DMul         to Texture2DMul,
            GLSLVar.IntSamplerCube          to TextureCube,
            GLSLVar.IntSampler1DArray       to Texture1DArray,
            GLSLVar.IntSampler2DArray       to Texture2DArray,
            GLSLVar.IntSamplerCubeMapArray  to TextureCubeMap,
            GLSLVar.IntSampler2DMulArray    to Texture2DMalArray,
            GLSLVar.IntSamplerBuffer        to TextureBuffer,
            GLSLVar.IntSampler2DRect        to TextureRectangle,

            // ===== UNSIGNED INTEGER SAMPLERS =====
            GLSLVar.UIntSampler1D           to Texture1D,
            GLSLVar.UIntSampler2D           to Texture2D,
            GLSLVar.UIntSampler3D           to Texture3D,
            GLSLVar.UIntSampler2DMul        to Texture2DMul,
            GLSLVar.UIntSamplerCube         to TextureCube,
            GLSLVar.UIntSampler1DArray      to Texture1DArray,
            GLSLVar.UIntSampler2DArray      to Texture2DArray,
            GLSLVar.UIntSamplerCubeMapArray to TextureCubeMap,
            GLSLVar.UIntSampler2DMulArray   to Texture2DMalArray,
            GLSLVar.UIntSamplerBuffer       to TextureBuffer,
            GLSLVar.UIntSampler2DRect       to TextureRectangle
        )}

        fun usedFor(type: GLSLVar) = typeForSampler.value[type]
    }
}