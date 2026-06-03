@file: Suppress("unused")

package com.losi.create.math

import kotlin.experimental.and
import kotlin.experimental.or

open class Vector4b : Vector3b
{
    internal companion object {
        const val B4_MASK: Byte = 0b00001000
        const val B4_MASK_INV: Byte = 0b11110111.toByte()

        fun asComposite(x: Boolean, y: Boolean, z: Boolean, w: Boolean) =
            ((if (x) B1_MASK else 0) or
            (if (y) B2_MASK else 0) or
            (if (z) B3_MASK else 0) or
            (if (w) B4_MASK else 0))
    }

    constructor(x: Boolean, y: Boolean, z: Boolean, w: Boolean): super(asComposite(x, y, z, w))
    constructor(composite: Byte): super(composite)
    constructor(value: Boolean): super(value)
    constructor()

    @set:JvmName("w")
    @get:JvmName("w")
    var w : Boolean
        get() = (composite and B4_MASK) == B4_MASK
        set(it) { composite = (composite and B4_MASK_INV) or (if (it) B4_MASK else 0) }
}