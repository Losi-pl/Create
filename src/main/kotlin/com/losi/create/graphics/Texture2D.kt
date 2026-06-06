package com.losi.create.graphics

import com.losi.create.graphics.gl.GLInternalFormat
import com.losi.create.graphics.gl.GLPixelFormat
import com.losi.create.graphics.gl.GLSLVar
import com.losi.create.graphics.gl.GLTextureWrappingMode
import com.losi.create.graphics.gl.GLTextureWrappingMode.Repeat
import com.losi.create.graphics.gl.GLWrappingDirection.Horizontal
import com.losi.create.graphics.gl.GLWrappingDirection.Vertical
import org.lwjgl.opengl.GL20.*
import org.lwjgl.system.MemoryStack
import java.awt.image.BufferedImage
import java.awt.image.DataBufferByte
import java.awt.image.DataBufferInt
import java.awt.image.DataBufferUShort
import java.io.InputStream
import java.lang.foreign.MemorySegment
import java.nio.ByteBuffer


class Texture2D {
    typealias ProcessedImage = Triple<ByteBuffer, Triple<GLInternalFormat, GLPixelFormat, GLSLVar>, Pair<Int, Int>>
    companion object {
        @Suppress("unused")
        fun BufferedImage.convertImageType(targetType: Int): BufferedImage {
            if (this.type == targetType)
                return this
            val img = BufferedImage(this.width, this.height, targetType)
            val g2d = img.createGraphics()
            g2d.drawImage(this, 0, 0, null)
            g2d.dispose()

            return img
        }

        @Suppress("SpellCheckingInspection", "RedundantSuppression")
        fun BufferedImage.processForGL(stack: MemoryStack): ProcessedImage { when (this.type)
        {
            BufferedImage.TYPE_INT_RGB, BufferedImage.TYPE_INT_ARGB -> {
                val pixels = (this.raster.dataBuffer as DataBufferInt).data
                val inForm = if(this.type == BufferedImage.TYPE_INT_ARGB) GLInternalFormat.RGBA8 else GLInternalFormat.RGB8
                val buffer = stack.mallocInt(pixels.size).put(pixels).flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                       Triple(inForm, GLPixelFormat.BGRA, GLSLVar.UnsABGR8),
                       Pair(width, height))
            }
            BufferedImage.TYPE_INT_ARGB_PRE -> {
                val pixels = (this.raster.dataBuffer as DataBufferInt).data
                val buffer = stack.mallocInt(pixels.size)

                pixels.forEach {
                    val a = (it shr 24) and 0xFF
                    if (a == 0) {
                        buffer.put(0)
                    } else {
                        val r = ((it shr 16) and 0xFF).toUByte()
                        val g = ((it shr 8) and 0xFF).toUByte()
                        val b = (it and 0xFF).toUByte()
                        val newR = ((r.toInt() * 255f) / a).toInt()
                        val newG = ((g.toInt() * 255f) / a).toInt()
                        val newB = ((b.toInt() * 255f) / a).toInt()
                        val rez = (newR shl 24) or (newG shl 16) or (newB shl 8) or a
                        buffer.put(rez)
                    }
                }
                buffer.flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                       Triple(GLInternalFormat.RGBA8, GLPixelFormat.RGBA, GLSLVar.UnsRGBA8),
                       Pair(width, height))
            }
            BufferedImage.TYPE_INT_BGR -> {
                val pixels = (this.raster.dataBuffer as DataBufferInt).data
                val buffer = stack.mallocInt(pixels.size).put(pixels).flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                    Triple(GLInternalFormat.RGB8, GLPixelFormat.RGBA, GLSLVar.UnsABGR8),
                    Pair(width, height))
            }
            BufferedImage.TYPE_3BYTE_BGR -> {
                val pixels = (this.raster.dataBuffer as DataBufferByte).data
                val buffer = stack.malloc(pixels.size).put(pixels).flip()

                return Triple(buffer,
                    Triple(GLInternalFormat.RGB8, GLPixelFormat.BGR, GLSLVar.UByte),
                    Pair(width, height))
            }
            BufferedImage.TYPE_4BYTE_ABGR -> {
                val pixels = (this.raster.dataBuffer as DataBufferByte).data
                val buffer = stack.malloc(pixels.size).put(pixels).flip()

                return Triple(buffer,
                    Triple(GLInternalFormat.RGBA8, GLPixelFormat.RGBA, GLSLVar.UnsRGBA8),
                    Pair(width, height))
            }
            BufferedImage.TYPE_4BYTE_ABGR_PRE -> {
                val pixels = (this.raster.dataBuffer as DataBufferByte).data
                val buffer = stack.malloc(pixels.size)

                for (ind in pixels.indices step 4)
                {
                    val a = pixels[ind]
                    if (a == 0.toByte()) {
                        buffer.put(0).put(0).put(0).put(0)
                    } else {
                        val b = pixels[ind + 1]
                        val g = pixels[ind + 2]
                        val r = pixels[ind + 3]
                        val newR = (r * 255f) / a
                        val newG = (g * 255f) / a
                        val newB = (b * 255f) / a
                        buffer.put(newR.toInt().toByte())
                            .put(newG.toInt().toByte())
                            .put(newB.toInt().toByte())
                            .put(a)
                    }
                }
                buffer.flip()

                return Triple(buffer,
                    Triple(GLInternalFormat.RGBA8, GLPixelFormat.RGBA, GLSLVar.UByte),
                    Pair(width, height))
            }
            BufferedImage.TYPE_USHORT_565_RGB -> {
                val pixels = (this.raster.dataBuffer as DataBufferUShort).data
                val buffer = stack.mallocShort(pixels.size).put(pixels).flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                    Triple(GLInternalFormat.RGB565, GLPixelFormat.RGB, GLSLVar.UnsR5G6B5),
                    Pair(width, height))
            }
            BufferedImage.TYPE_USHORT_555_RGB -> {
                val pixels = (this.raster.dataBuffer as DataBufferUShort).data
                val buffer = stack.mallocShort(pixels.size)
                for (packed in pixels) {
                    buffer.put(((packed.toInt() shl 1) or 1).toShort())
                }
                buffer.flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                    Triple(GLInternalFormat.RGB5A1, GLPixelFormat.RGBA, GLSLVar.UnsRGB5A1),
                    Pair(width, height))
            }
            BufferedImage.TYPE_BYTE_GRAY -> {
                val pixels = (this.raster.dataBuffer as DataBufferByte).data
                val buffer = stack.malloc(pixels.size).put(pixels).flip()

                return Triple(buffer,
                    Triple(GLInternalFormat.R8, GLPixelFormat.RED, GLSLVar.UByte),
                    Pair(width, height))

            }
            BufferedImage.TYPE_USHORT_GRAY -> {
                val pixels = (this.raster.dataBuffer as DataBufferUShort).data
                val buffer = stack.mallocShort(pixels.size).put(pixels).flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                    Triple(GLInternalFormat.R16, GLPixelFormat.RED, GLSLVar.UShort),
                    Pair(width, height))
            }
            BufferedImage.TYPE_BYTE_BINARY -> {
                val packedData = (this.raster.dataBuffer as DataBufferByte).data

                val strideInBytes = (width + 7) / 8
                val buffer = stack.malloc(width * height)

                for (y in 0 until height) {
                    for (x in 0 until width) {
                        val byteIndex = y * strideInBytes + (x / 8)
                        val currentByte = packedData[byteIndex].toInt()
                        val bitPosition = 7 - (x % 8)

                        val bitValue = (currentByte shr bitPosition) and 1
                        val grayValue = (if (bitValue == 1) 0xFF else 0x00).toByte()
                        buffer.put(grayValue)
                    }
                }
                buffer.flip()

                return Triple(buffer,
                    Triple(GLInternalFormat.R8, GLPixelFormat.RED, GLSLVar.UByte),
                    Pair(width, height))
            }
            else /* BufferedImage.TYPE_BYTE_INDEXED, BufferedImage.TYPE_CUSTOM*/ -> {
                val buffer = stack.mallocInt(width * height)

                for (y in 0 until height) {
                    for (x in 0 until width) {
                        val argb = this.getRGB(x, y)
                        buffer.put(argb)
                    }
                }
                buffer.flip()

                return Triple(MemorySegment.ofBuffer(buffer).asByteBuffer(),
                    Triple(GLInternalFormat.SRGB8_ALPHA8, GLPixelFormat.BGRA, GLSLVar.UnsABGR8),
                    Pair(width, height))
            }
        }}

        fun ProcessedImage.loadToGL() =
            glTexImage2D(
                GL_TEXTURE_2D,0,
                second.first.gl,
                third.first, third.second, 0,
                second.second.gl,
                second.third.gl,
                first)
    }

    private val handles = Handles(glGenTextures())

    constructor(stream: InputStream, wrappingMode: GLTextureWrappingMode = Repeat):
            this (stream, wrappingMode, wrappingMode)

    constructor(stream: InputStream,
                verticalWrapping: GLTextureWrappingMode,
                horizontalWrapping: GLTextureWrappingMode) {
        glBindTexture(GL_TEXTURE_2D, handle)
        glTexParameteri(GL_TEXTURE_2D, Horizontal.gl, horizontalWrapping.gl)
        glTexParameteri(GL_TEXTURE_2D, Vertical.gl, verticalWrapping.gl)

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST)
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST)

        val image = javax.imageio.ImageIO.read(stream) ?: throw RuntimeException("Unable to parse icon")
        MemoryStack.stackPush().use { stack -> image.processForGL(stack).loadToGL() }
    }

    internal val handle = handles.texture

    private data class Handles (val texture: Int)
}
