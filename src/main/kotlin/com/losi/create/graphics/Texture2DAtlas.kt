package com.losi.create.graphics

import com.losi.create.graphics.gl.*
import com.losi.create.utility.orElse
import org.joml.*
import java.awt.image.BufferedImage
import java.io.InputStream
import javax.imageio.ImageIO
import kotlin.require

class Texture2DAtlas : Texture, GLBound {
    private val handlers: Handlers

    override val textureTarget: GLSLVar get() = GLSLVar.Sampler2DArray
    override val handle: TextureObject get() = handlers.texture

    private constructor(`object`: TextureObject) { handlers = Handlers(`object`) }

    override fun release() = TODO("Not yet implemented")

    private data class Handlers(var texture: TextureObject)

    companion object {
        private val texture = TextureType.Texture2DArray

        fun create(creation: Constructor.() -> Unit) : Texture2DAtlas {
            val con = Constructor()
            con.creation()
            return Texture2DAtlas(con.textureObject)
        }
    }

    @Suppress("unused")
    class Constructor {
        internal val textureObject = glGenTexture(TextureType.Texture2DArray).apply { glBindTexture(this) }
        internal constructor()

        private var size = AtlasSize()
        private var defined = false
        private var intFormat = InternalFormat.SRGB8_ALPHA8

        /**Specifies what will happen if the UV of the texture goes beyond its bounds*/
        fun wrapping(direction: WrappingDirection, mode: TextureWrappingMode) {
            require(direction != WrappingDirection.Depth) { "Depth is not a valid value of this method" }
            glTexParameterWrapping(texture, direction, mode)
        }

        fun min(min: MinFilterMode) = glTexParameter(texture, min)
        fun mag(mag: MagFilterMode) = glTexParameter(texture, mag)

        fun imageSize(width: Int, height: Int) { size.width = width.toUInt(); size.height = height.toUInt() }
        fun imageSize(width: UInt, height: UInt) { size.width = width; size.height = height }

        fun imageCount(count: Int) { size.count = count.toUInt() }
        fun imageCount(count: UInt) { size.count = count }

        fun internalFormat(format: InternalFormat) { intFormat = format }

        private fun construct() {
            if(defined)
                return
            check(size.width != null)  { "Size of image's must be specified before any data can be inputted" }
            check(size.height != null) { "Size of image's must be specified before any data can be inputted" }
            check(size.count != null)  { "Count images must be specified before any data can be inputted" }

            glTexStorage3D(texture, 1u, intFormat, size.width!!, size.height!!, size.count!!)
            defined = true
        }

        fun set(index: UInt, stream: InputStream) {
            construct()

            require(index in 0u..size.count!! - 1u) { "You are trying to set an texture outside of bounds" }

            val image: BufferedImage? = ImageIO.read(stream)

            requireNotNull(image) { "Texture could not be loaded from the stream" }
            require(image.width.toUInt() == size.width!! && image.height.toUInt() == size.height!!) {
                "Size of the image you are trying to load is not ${size.width}x${size.height}" }

            glTexSubImage2DArray(index, image)
        }

        @JvmInline
        private value class AtlasSize(private val vec: Vector3i = Vector3i()) {
            var width: UInt?
                get() = if(vec.x == 0) return null else vec.x.toUInt() - 1u
                set(it) = it?.let { vec.x = (it + 1u).toInt() }.orElse { vec.x = 0 }
            var height: UInt?
                get() = if(vec.y == 0) return null else vec.y.toUInt() - 1u
                set(it) = it?.let { vec.y = (it + 1u).toInt() }.orElse { vec.y = 0 }
            var count: UInt?
                get() = if(vec.z == 0) return null else vec.z.toUInt() - 1u
                set(it) = it?.let { vec.z = (it + 1u).toInt() }.orElse { vec.z = 0 }
        }
    }
}