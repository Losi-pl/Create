@file:JvmName("GLLongCr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.*
import org.lwjgl.opengl.ARBGPUShaderInt64

/**`void glUniform1i64ARB(GLint location, GLint64 x)`*/
fun glUniform1(location: UniformLocation, value: Long) = ARBGPUShaderInt64.glUniform1i64ARB(location.handle, value)

/**`void glUniform1ui64ARB(GLint location, GLuint64 x)`*/
fun glUniform1(location: UniformLocation, value: ULong) = ARBGPUShaderInt64.glUniform1ui64ARB(location.handle, value.toLong())

/**`void glUniform2i64ARB(GLint location, GLint64 x, GLint64 y)`*/
fun glUniform2(location: UniformLocation, value: Vector2L) = ARBGPUShaderInt64.glUniform2i64ARB(location.handle, value.x, value.y)

/**`void glUniform2i64ARB(GLint location, GLint64 x, GLint64 y)`*/
fun glUniform2(location: UniformLocation, v1: Long, v2: Long) = ARBGPUShaderInt64.glUniform2i64ARB(location.handle, v1, v2)

/**`void glUniform3i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z)`*/
fun glUniform3(location: UniformLocation, value: Vector3L) = ARBGPUShaderInt64.glUniform3i64ARB(location.handle, value.x, value.y, value.z)

/**`void glUniform3i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z)`*/
fun glUniform3(location: UniformLocation, v1: Long, v2: Long, v3: Long) = ARBGPUShaderInt64.glUniform3i64ARB(location.handle, v1, v2, v3)

/**`void glUniform3i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z)`*/
fun glUniform4(location: UniformLocation, value: Vector4L) = ARBGPUShaderInt64.glUniform4i64ARB(location.handle, value.x, value.y, value.z, value.w)

/**`void glUniform3i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z)`*/
fun glUniform4(location: UniformLocation, v1: Long, v2: Long, v3: Long, v4: Long) = ARBGPUShaderInt64.glUniform4i64ARB(location.handle, v1, v2, v3, v4)