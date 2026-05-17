@file:JvmName("CShaderUniforms")
@file:Suppress("SpellCheckingInspection")

package com.losi.create.utility

import org.joml.Matrix4f
import org.lwjgl.opengl.GL20.*
import org.lwjgl.system.MemoryStack
import org.lwjgl.system.NativeType

fun glUniformMatrix4fv(@NativeType("GLint")location: Int,
                       @NativeType("GLboolean")transpose: Boolean,
                       @NativeType("GLfloat const *") matrix: Matrix4f)
{
    MemoryStack.stackPush().use {
        val buff = it.mallocFloat(4 * 4)
        matrix.get(buff)
        glUniformMatrix4fv(location, transpose, buff)
    }
}