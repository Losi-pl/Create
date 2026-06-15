package com.losi.create.graphics.gl

@JvmInline
value class BufferObject private constructor(private val fullHandle: ULong) {
    constructor(handle: Int, type: BufferType): this(handle.toULong() or (type.ordinal.toULong() shl 32))

    val handle: UInt get() = fullHandle.toUInt()
    val type: BufferType get() = BufferType.entries[(fullHandle shr 32).toInt()]
}