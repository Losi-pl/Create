@file:JvmName("GL46Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL46C

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderHasSpirV(shader: ShaderPart) = GL46C.glGetShaderi(shader.handle.toInt(), ShaderParam.HasSpirV.gl) == GL46C.GL_TRUE