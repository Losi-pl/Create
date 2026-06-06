@file:JvmName("GL20Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.*
import org.lwjgl.opengl.GL20C
import org.lwjgl.system.MemoryStack
import org.lwjgl.system.NativeType

/** `void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix4f(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLfloat const *") matrix: Matrix4f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(4 * 4)
        matrix.get(buff)
        GL20C.glUniformMatrix4fv(location, transpose, buff)
    }
}

/** `void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix3f(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLfloat const *") matrix: Matrix3f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(3 * 3)
        matrix.get(buff)
        GL20C.glUniformMatrix3fv(location, transpose, buff)
    }
}

/** `void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix2f(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLfloat const *") matrix: Matrix2f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(2 * 2)
        matrix.get(buff)
        GL20C.glUniformMatrix2fv(location, transpose, buff)
    }
}