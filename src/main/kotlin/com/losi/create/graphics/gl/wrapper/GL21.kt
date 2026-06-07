@file:JvmName("GL21Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.joml.*
import org.lwjgl.opengl.GL21C
import org.lwjgl.system.MemoryStack

/** `void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix4x3f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(4 * 3)
        matrix.get(buff)
        GL21C.glUniformMatrix4x3fv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix3x2f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(3 * 2)
        matrix.get(buff)
        GL21C.glUniformMatrix3x2fv(location.handle, transpose, buff)
    }
}