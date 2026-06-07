@file:JvmName("GL13Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL13C

/**`void glActiveTexture(GLenum texture)`*/
fun glActiveTexture(texture: ActiveTexture) = GL13C.glActiveTexture(texture.gl)

/**`void glActiveTexture(GLenum texture)`*/
fun glActiveTexture(texture: Int) {
    if(texture !in 0..31)
        throw IndexOutOfBoundsException()
    GL13C.glActiveTexture(GL13C.GL_TEXTURE0 + texture)
}