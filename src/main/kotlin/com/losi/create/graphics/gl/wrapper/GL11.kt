@file:JvmName("GL11Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import com.losi.create.graphics.Texture2D.Companion.processForGL
import org.lwjgl.opengl.GL11C
import org.lwjgl.system.MemoryStack
import java.awt.Color
import java.awt.image.BufferedImage

/**A quick test to see if OpenGL works on the current [Thread]*/
fun glTest() = try { GL11C.glGetError(); true } catch (ignored: NullPointerException) { false }

/**`void glGenTextures(GLsizei n, GLuint * textures)`*/
fun glGenTexture(type: TextureType) = TextureObject(type, GL11C.glGenTextures())

/**`void glDeleteTextures(GLsizei n, GLuint const * textures)`*/
fun glDeleteTexture(target: TextureObject) = GL11C.glDeleteTextures(target.handle)

/**void glBindTexture(GLenum target, GLuint texture)*/
fun glBindTexture(handle: TextureObject) = GL11C.glBindTexture(handle.type.gl, handle.handle)

/**void glBindTexture(GLenum target, GLuint texture)*/
fun glUnbindTexture(target: TextureType) = GL11C.glBindTexture(target.gl, 0)

/**`void glTexParameteri(GLenum target, GLenum pname, GLint param)`*/
fun glTexParameterWrapping(texture: TextureType, direction: WrappingDirection, mode: TextureWrappingMode) =
    GL11C.glTexParameteri(texture.gl, direction.gl, mode.gl)

/**`void glTexParameteri(GLenum target, GLenum pname, GLint param)`*/
fun glTexParameter(texture: TextureType, direction: FragmentChannel, source: SampledColor) =
    GL11C.glTexParameteri(texture.gl, direction.gl, source.gl)

/**`void glTexParameteri(GLenum target, GLenum pname, GLint param)`*/
fun glTexParameter(texture: TextureType, min: MinFilterMode) = GL11C.glTexParameteri(texture.gl, GL11C.GL_TEXTURE_MIN_FILTER , min.gl)

/**`void glTexParameteri(GLenum target, GLenum pname, GLint param)`*/
fun glTexParameter(texture: TextureType, mag: MagFilterMode) = GL11C.glTexParameteri(texture.gl, GL11C.GL_TEXTURE_MAG_FILTER , mag.gl)

/**`void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, void const * pixels)`*/
fun glTexImage2D(image: BufferedImage) = MemoryStack.stackPush().use { stack ->
    val data = image.processForGL(stack)
    GL11C.glTexImage2D(TextureType.Texture2D.gl, 0,
        data.second.first.gl,
        image.width, image.height, 0,
        data.second.second.gl,
        data.second.third.gl,
        data.first)
}

/**`void glClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha)`*/
fun glClearColor(color: Color) = GL11C.glClearColor(color.red / 255f, color.green / 255f, color.blue / 255f, color.alpha / 255f)

/**`void glClear(GLbitfield mask)`*/
fun glClear(target: ClearTarget) = GL11C.glClear(target.gl)

/**`void glViewport(GLint x, GLint y, GLsizei w, GLsizei h)`*/
fun glViewport(horiz: IntRange, vertical: IntRange) = GL11C.glViewport(horiz.first, vertical.first, horiz.last, vertical.last)

/**`void glFinish(void)`*/
fun glFinish() = GL11C.glFinish()

//TODO: Investigate glFenceSync