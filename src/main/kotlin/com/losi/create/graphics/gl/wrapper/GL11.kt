@file:JvmName("GL11Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL11C

/**A quick test to see if OpenGL works on the current [Thread]*/
fun glTest() = try { GL11C.glGetError(); true } catch (ignored: NullPointerException) { false }

/**void glBindTexture(GLenum target, GLuint texture)*/
fun glBindTexture(handle: TextureObject) = GL11C.glBindTexture(handle.type.gl, handle.handle)