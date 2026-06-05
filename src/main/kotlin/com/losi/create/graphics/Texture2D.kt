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
import java.nio.ByteOrder


class Texture2D {
    companion object {
        fun BufferedImage.convertImageType(targetType: Int): BufferedImage {
            if (this.type == targetType)
                return this
            val img = BufferedImage(this.width, this.height, targetType)
            val g2d = img.createGraphics()
            g2d.drawImage(this, 0, 0, null)
            g2d.dispose()

            return img
        }

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
        loadBuffer(image.convertImageType(BufferedImage.TYPE_BYTE_INDEXED))
    }

    @Suppress("SpellCheckingInspection", "RedundantSuppression")
    private fun loadBuffer(image: BufferedImage) { when (image.type)
    {
        BufferedImage.TYPE_INT_RGB, BufferedImage.TYPE_INT_ARGB -> {
            val pixels = (image.raster.dataBuffer as DataBufferInt).data
            val inForm = if(image.type == BufferedImage.TYPE_INT_ARGB) GLInternalFormat.RGBA8 else GLInternalFormat.RGB8
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocInt(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    inForm.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.BGRA.gl,
                    GLSLVar.UnsABGR8.gl, buffer)
            }
        }
        BufferedImage.TYPE_INT_ARGB_PRE -> {
            val pixels = (image.raster.dataBuffer as DataBufferInt).data
            MemoryStack.stackPush().use { stack ->
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

                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGBA8.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RGBA.gl,
                    GLSLVar.UnsRGBA8.gl, buffer)
            }
        }
        BufferedImage.TYPE_INT_BGR -> {
            val pixels = (image.raster.dataBuffer as DataBufferInt).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocInt(pixels.size).put(pixels).flip()

                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGB8.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RGBA.gl,
                    GLSLVar.UnsABGR8.gl, buffer)
            }
        }
        BufferedImage.TYPE_3BYTE_BGR -> {
            val pixels = (image.raster.dataBuffer as DataBufferByte).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.malloc(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGB8.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.BGR.gl,
                    GLSLVar.UByte.gl, buffer)
            }
        }
        BufferedImage.TYPE_4BYTE_ABGR -> {
            val pixels = (image.raster.dataBuffer as DataBufferByte).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.malloc(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGBA8.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RGBA.gl,
                    GLSLVar.UnsRGBA8.gl, buffer)
            }
        }
        BufferedImage.TYPE_4BYTE_ABGR_PRE -> {
            val pixels = (image.raster.dataBuffer as DataBufferInt).data
            val inForm = if(image.type == BufferedImage.TYPE_INT_ARGB) GLInternalFormat.RGBA8 else GLInternalFormat.RGB8
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocInt(pixels.size)

                pixels.forEach {
                    val a = (it shr 24) and 0xFF
                    if (a == 0) {
                        buffer.put(0)
                    } else {
                        val r = it and 0xFF
                        val g = (it shr 8) and 0xFF
                        val b = (it shr 16) and 0xFF
                        val newR = (r * 255) / a
                        val newG = (g * 255) / a
                        val newB = (b * 255) / a
                        buffer.put((newR shl 24) or (newG shl 16) or (newB shl 8) or a)
                    }
                }
                buffer.flip()

                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    inForm.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RGBA.gl,
                    GLSLVar.UnsRGBA8.gl, buffer)
            }
            TODO("Fix as it apperantly the data is not an int buffer")
        }
        BufferedImage.TYPE_USHORT_565_RGB -> {
            val pixels = (image.raster.dataBuffer as DataBufferUShort).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocShort(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGB565.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RGB.gl,
                    GLSLVar.UnsR5G6B5.gl, buffer)
            }
        }
        BufferedImage.TYPE_USHORT_555_RGB -> {
            val pixels = (image.raster.dataBuffer as DataBufferUShort).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocShort(pixels.size)
                for (packed in pixels) {
                    val r = (packed.toInt() ushr 10) and 0x1F
                    val g = (packed.toInt() ushr 5) and 0x1F
                    val b = packed.toInt() and 0x1F

                    val a = 1
                    val rgba = (r shl 11) or (g shl 6) or (b shl 1) or a
                    buffer.put(rgba.toShort())
                }
                buffer.flip()

                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.RGB5A1.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.BGR.gl,
                    GLSLVar.UnsRGB5A1.gl, buffer)
                TODO("This doesn't work")
            }
        }
        BufferedImage.TYPE_BYTE_GRAY -> {
            val pixels = (image.raster.dataBuffer as DataBufferByte).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.malloc(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.R8.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RED.gl,
                    GLSLVar.UByte.gl, buffer)
            }
        }
        BufferedImage.TYPE_USHORT_GRAY -> {
            val pixels = (image.raster.dataBuffer as DataBufferUShort).data
            MemoryStack.stackPush().use { stack ->
                val buffer = stack.mallocShort(pixels.size).put(pixels).flip()
                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    GLInternalFormat.R16.gl,
                    image.width, image.height, 0,
                    GLPixelFormat.RED.gl,
                    GLSLVar.UShort.gl, buffer)
            }
        }
        BufferedImage.TYPE_BYTE_BINARY -> {
            val width = image.width
            val height = image.height
            val raster = image.raster
            // Get the underlying packed byte data
            val packedData = (raster.dataBuffer as DataBufferByte).data

            // Calculate scanline stride (row length in bytes), accounting for padding
            val strideInBytes = (width + 7) / 8

            MemoryStack.stackPush().use { stack ->
                // Create a ByteBuffer to hold the unpacked, single-channel data
                val pixelBuffer = stack.malloc(width * height)

                for (y in 0 until height) {
                    for (x in 0 until width) {
                        // Find the byte that contains the pixel (x, y)
                        val byteIndex = y * strideInBytes + (x / 8)
                        // Get the specific byte
                        val currentByte = packedData[byteIndex].toInt()
                        // Determine the bit position within the byte
                        val bitPosition = 7 - (x % 8)

                        // Isolate the bit (0 or 1)
                        val bitValue = (currentByte shr bitPosition) and 1
                        // Convert the bit to a grayscale byte (0 or 255)
                        val grayValue = (if (bitValue == 1) 0xFF else 0x00).toByte()
                        pixelBuffer.put(grayValue)
                    }
                }
                pixelBuffer.flip()

                glTexImage2D(
                    GL_TEXTURE_2D,0,
                    GLInternalFormat.R8.gl,
                    width, height, 0,
                    GLPixelFormat.RED.gl,
                    GLSLVar.UByte.gl,
                    pixelBuffer)
            }

        }
        BufferedImage.TYPE_BYTE_INDEXED -> {
            val width = image.width
            val height = image.height

            val cm = image.colorModel
            val bytesPerPixel = if (cm.hasAlpha()) 4 else 3

            MemoryStack.stackPush().use { stack ->
                val buffer = stack.malloc(width * height * 4).order(ByteOrder.nativeOrder())

                for (y in 0 until height) {
                    for (x in 0 until width) {
                        // Use the reliable built-in method
                        val argb = image.getRGB(x, y)

                        // This line is now safe and works for all image types
                        val a = (argb shr 24) and 0xFF
                        val r = (argb shr 16) and 0xFF
                        val g = (argb shr 8) and 0xFF
                        val b = argb and 0xFF

                        if (bytesPerPixel == 4) {
                            buffer.put(r.toByte()).put(g.toByte()).put(b.toByte()).put(a.toByte())
                        } else {
                            buffer.put(r.toByte()).put(g.toByte()).put(b.toByte()).put(0xFF.toByte())
                        }
                    }
                }
                buffer.flip()

                glTexImage2D(
                    GL_TEXTURE_2D, 0,
                    (if (bytesPerPixel == 4) GLInternalFormat.RGBA4 else GLInternalFormat.RGBA4).gl,
                    width, height, 0,
                    GLPixelFormat.RGBA.gl,
                    GLSLVar.UByte.gl,
                    buffer
                )
            }
            return
            @Suppress("KotlinUnreachableCode")
            TODO("This returns grabage")
        }

        BufferedImage.TYPE_CUSTOM -> { TODO("See if there is a way to deal with this") }
    }}

    internal val handle = handles.texture

    private data class Handles (val texture: Int)
}
