@file:Suppress("unused")
package com.losi.create.graphics.gl

import org.lwjgl.opengl.*
import org.lwjgl.opengles.ANGLETranslatedShaderSource

enum class ShaderParam(val gl: Int, glName: String) {
    /**The specific [ShaderType] of a [ShaderPart].
     *
     * `GL_SHADER_TYPE`*/
    Type(GL20.GL_SHADER_TYPE, "GL_SHADER_TYPE"),
    /**A flag specifying is this [ShaderPart] marked for deletion
     *
     * `GL_DELETE_STATUS`*/
    IsForDeletion(GL20.GL_DELETE_STATUS, "GL_DELETE_STATUS"),
    /**A flag specifying if this [ShaderPart] is compiled
     *
     * `GL_COMPILE_STATUS`*/
    IsCompiled(GL20.GL_COMPILE_STATUS, "GL_COMPILE_STATUS"),
    /**A length of the log of this [ShaderPart]. If there is no log, will return 0
     *
     * `GL_INFO_LOG_LENGTH`*/
    LogSize(GL20.GL_INFO_LOG_LENGTH, "GL_INFO_LOG_LENGTH"),
    /**The length of the concatenated [ShaderPart] source code strings, including the null terminator. If there is source, will return 0
     *
     * `GL_SHADER_SOURCE_LENGTH`*/
    CodeSize(GL20.GL_SHADER_SOURCE_LENGTH, "GL_SHADER_SOURCE_LENGTH"),
    /**A flag specifying if this [ShaderPart] contains a SPIR-V binary
     *
     * `GL_SPIR_V_BINARY`*/
    HasSpirV(GL46.GL_SPIR_V_BINARY, "GL_SPIR_V_BINARY"),
    /**The length of the translated [ShaderPart] source string (provided by ANGLE), including the null terminator. If there is no source, will return 0
     *
     * `GL_TRANSLATED_SHADER_SOURCE_LENGTH_ANGLE`*/
    CodeSizeANG(ANGLETranslatedShaderSource.GL_TRANSLATED_SHADER_SOURCE_LENGTH_ANGLE, "GL_TRANSLATED_SHADER_SOURCE_LENGTH_ANGLE"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}