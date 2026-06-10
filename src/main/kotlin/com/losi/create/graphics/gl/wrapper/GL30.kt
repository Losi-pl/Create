@file:JvmName("GL30Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL30C

/**`void glBindFragDataLocation(GLuint program, GLuint colorNumber, GLchar const * name)`*///TODO: Look into colorNumber
fun glBindFragDataLocation(program: ShaderProgram, location: UInt, name: String) = GL30C.glBindFragDataLocation(program.handle.toInt(), location.toInt(), name)

/**`void glUniform1ui(GLint location, GLuint v0)`*/
fun glUniform1(location: UniformLocation, value: UByte) = GL30C.glUniform1ui(location.handle, value.toInt())
/**`void glUniform1ui(GLint location, GLuint v0)`*/
fun glUniform1(location: UniformLocation, value: UShort) = GL30C.glUniform1ui(location.handle, value.toInt())
/**`void glUniform1ui(GLint location, GLuint v0)`*/
fun glUniform1(location: UniformLocation, value: UInt) = GL30C.glUniform1ui(location.handle, value.toInt())

/**`void glTexParameteri(GLenum target, GLenum pname, GLint param)`*/
fun glTexParameterComparison(texture: TextureType, func: ComparisonFunction?) {
    GL30C.glTexParameteri(texture.gl, GL30C.GL_TEXTURE_COMPARE_MODE , if(func != null) GL30C.GL_COMPARE_REF_TO_TEXTURE else GL30C.GL_NONE)
    func?.let { GL30C.glTexParameteri(texture.gl, GL30C.GL_TEXTURE_COMPARE_FUNC, it.gl) }
}