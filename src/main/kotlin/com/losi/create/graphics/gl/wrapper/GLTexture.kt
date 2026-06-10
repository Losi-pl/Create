@file:JvmName("GLTexturesCr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import com.losi.create.graphics.Texture2D.Companion.processForGL
import com.losi.create.utility.require
import org.lwjgl.opengl.ARBTextureStorage
import org.lwjgl.opengl.GL31C
import org.lwjgl.system.MemoryStack
import java.awt.image.BufferedImage

/**`void glTexStorage3D(GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth)`*/
fun glTexStorage3D(target: TextureType, mipmap: UInt, internalFormat: InternalFormat, width: UInt, height: UInt, depth: UInt)
 = ARBTextureStorage.glTexStorage3D(
    target.gl,
    mipmap.require(mipmap > 0u) { "Mipmap must be grater than 0" }.toInt(),
    internalFormat.gl,
    width.toInt(),
    height.toInt(),
    depth.toInt())

/**`void glTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void const * pixels)`*/
fun glTexSubImage2DArray(index: UInt, image: BufferedImage) = MemoryStack.stackPush().use { stack ->
     val data = image.processForGL(stack)

    GL31C.glTexSubImage3D(
        TextureType.Texture2DArray.gl, 0,
        0, 0, index.toInt(),
        image.width, image.height, 1,
        data.second.second.gl,
        data.second.third.gl,
        data.first)
}