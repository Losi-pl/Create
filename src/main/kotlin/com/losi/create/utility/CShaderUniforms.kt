@file:Suppress("SpellCheckingInspection")

package com.losi.create.utility

import org.jetbrains.annotations.NotNull
import org.joml.Matrix4f
import org.lwjgl.opengl.GL20.*
import org.lwjgl.system.MemoryStack
import org.lwjgl.system.NativeType

class CShaderUniforms
{
    companion object
    {
        @JvmStatic
        fun glUniformMatrix4fv(@NativeType("GLint")location: Int,
                               @NativeType("GLboolean")transpose: Boolean,
                               @NativeType("GLfloat const *") @NotNull matrix: Matrix4f)
        {
            MemoryStack.stackPush().use {
                val buff = it.mallocFloat(4 * 4)
                buff.put(matrix.m00()).put(matrix.m01()).put(matrix.m02()).put(matrix.m03())
                    .put(matrix.m10()).put(matrix.m11()).put(matrix.m12()).put(matrix.m13())
                    .put(matrix.m20()).put(matrix.m21()).put(matrix.m22()).put(matrix.m23())
                    .put(matrix.m30()).put(matrix.m31()).put(matrix.m32()).put(matrix.m33())
                buff.flip()
                glUniformMatrix4fv(location, transpose, buff)
            }
        }
    }

}

