@file:JvmName("GL21Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.Matrix3x2f
import org.joml.Matrix4x3f
import org.lwjgl.opengl.GL21C
import org.lwjgl.system.MemoryStack
import org.lwjgl.system.NativeType

/** `void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix4x3f(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLfloat const *") matrix: Matrix4x3f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(4 * 3)
        matrix.get(buff)
        GL21C.glUniformMatrix4x3fv(location, transpose, buff)
    }
}

/** `void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix3x2f(
    @NativeType("GLint") location: Int,
    @NativeType("GLboolean") transpose: Boolean,
    @NativeType("GLfloat const *") matrix: Matrix3x2f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(3 * 2)
        matrix.get(buff)
        GL21C.glUniformMatrix3x2fv(location, transpose, buff)
    }
}