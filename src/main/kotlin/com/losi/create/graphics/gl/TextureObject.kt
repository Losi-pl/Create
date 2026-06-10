package com.losi.create.graphics.gl

@JvmInline
value class TextureObject private constructor(private val collHandle: ULong) {
    val type: TextureType get() = TextureType.entries[(collHandle shr 32).toInt()]
    val handle: Int get() = collHandle.toInt()

    constructor(type: TextureType, handle: Int): this(handle.toULong() or (type.ordinal.toULong() shl 32))
}