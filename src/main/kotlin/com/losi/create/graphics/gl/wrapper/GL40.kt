@file:JvmName("GL40Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.*
import org.lwjgl.opengl.GL40C
import org.lwjgl.system.MemoryStack
import org.lwjgl.system.NativeType

/** `void glUniformMatrix4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix4d(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLdouble const *") matrix: Matrix4d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(4 * 4)
        matrix.get(buff)
        GL40C.glUniformMatrix4dv(location, transpose, buff)
    }
}

/** `void glUniformMatrix3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix3d(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLdouble const *") matrix: Matrix3d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(3 * 3)
        matrix.get(buff)
        GL40C.glUniformMatrix3dv(location, transpose, buff)
    }
}

/** `void glUniformMatrix2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix2d(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLdouble const *") matrix: Matrix2d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(2 * 2)
        matrix.get(buff)
        GL40C.glUniformMatrix2dv(location, transpose, buff)
    }
}

/** `void glUniformMatrix4x3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix4x3d(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLdouble const *") matrix: Matrix4x3d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(4 * 3)
        matrix.get(buff)
        GL40C.glUniformMatrix4x3dv(location, transpose, buff)
    }
}

/** `void glUniformMatrix3x2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)`  */
fun glUniformMatrix3x2d(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLdouble const *") matrix: Matrix3x2d) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocDouble(3 * 2)
        matrix.get(buff)
        GL40C.glUniformMatrix3x2dv(location, transpose, buff)
    }
}