@file:JvmName("GLDoubleCr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.*
import org.lwjgl.opengl.ARBGPUShaderFP64
import org.lwjgl.system.MemoryStack
import kotlin.use

/**`void glUniform1d(GLint location, GLdouble x)`*/
fun glUniform1(location: UniformLocation, value: Double) = ARBGPUShaderFP64.glUniform1d(location.handle, value)

/**`void glUniform2d(GLint location, GLdouble x, GLdouble y)`*/
fun glUniform2(location: UniformLocation, value: Vector2d) = ARBGPUShaderFP64.glUniform2d(location.handle, value.x, value.y)

/**`void glUniform2d(GLint location, GLdouble x, GLdouble y)`*/
fun glUniform2(location: UniformLocation, v1: Double, v2: Double) = ARBGPUShaderFP64.glUniform2d(location.handle, v1, v2)

/**`void glUniform3d(GLint location, GLdouble x, GLdouble y, GLdouble z)`*/
fun glUniform3(location: UniformLocation, value: Vector3d) = ARBGPUShaderFP64.glUniform3d(location.handle, value.x, value.y, value.z)

/**`void glUniform3d(GLint location, GLdouble x, GLdouble y, GLdouble z)`*/
fun glUniform3(location: UniformLocation, v1: Double, v2: Double, v3: Double) = ARBGPUShaderFP64.glUniform3d(location.handle, v1, v2, v3)

/**`void glUniform4d(GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w)`*/
fun glUniform4(location: UniformLocation, value: Vector4d) = ARBGPUShaderFP64.glUniform4d(location.handle, value.x, value.y, value.z, value.w)

/**`void glUniform4d(GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w)`*/
fun glUniform4(location: UniformLocation, v1: Double, v2: Double, v3: Double, v4: Double) = ARBGPUShaderFP64.glUniform4d(location.handle, v1, v2, v3, v4)

/** `void glUniformMatrix4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix4d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(4 * 4)
        matrix.get(buff)
        ARBGPUShaderFP64.glUniformMatrix4dv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix3d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(3 * 3)
        matrix.get(buff)
        ARBGPUShaderFP64.glUniformMatrix3dv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix2d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(2 * 2)
        matrix.get(buff)
        ARBGPUShaderFP64.glUniformMatrix2dv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix4x3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix4x3d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(4 * 3)
        matrix.get(buff)
        ARBGPUShaderFP64.glUniformMatrix4x3dv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix3x2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix3x2d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(3 * 2)
        matrix.get(buff)
        ARBGPUShaderFP64.glUniformMatrix3x2dv(location.handle, transpose, buff)
    }
}