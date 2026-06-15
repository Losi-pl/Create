@file:JvmName("GL15Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL15C
import java.nio.*

/**`void glGenBuffers(GLsizei n, GLuint * buffers)`*/
fun glGenBuffer(type: BufferType) = BufferObject(GL15C.glGenBuffers(), type)

/**`void glDeleteBuffers(GLsizei n, GLuint const * buffers)`*/
fun glDeleteBuffer(buffer: BufferObject) = GL15C.glDeleteBuffers(buffer.handle.toInt())

/**`void glBindBuffer(GLenum target, GLuint buffer)`*/
fun glBindBuffer(buffer: BufferObject) = GL15C.glBindBuffer(buffer.type.gl, buffer.handle.toInt())

/**`void glBufferData(GLenum target, GLsizeiptr size, void const * data, GLenum usage)`*/
fun glBufferData(target: BufferType, data: ByteBuffer, usage: BufferUsage) = GL15C.glBufferData(target.gl, data, usage.gl)
/**`void glBufferData(GLenum target, GLsizeiptr size, void const * data, GLenum usage)`*/
fun glBufferData(target: BufferType, data: ShortBuffer, usage: BufferUsage) = GL15C.glBufferData(target.gl, data, usage.gl)
/**`void glBufferData(GLenum target, GLsizeiptr size, void const * data, GLenum usage)`*/
fun glBufferData(target: BufferType, data: IntBuffer, usage: BufferUsage) = GL15C.glBufferData(target.gl, data, usage.gl)
/**`void glBufferData(GLenum target, GLsizeiptr size, void const * data, GLenum usage)`*/
fun glBufferData(target: BufferType, data: LongBuffer, usage: BufferUsage) = GL15C.glBufferData(target.gl, data, usage.gl)

