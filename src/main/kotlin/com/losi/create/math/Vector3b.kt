@file: Suppress("unused")

package com.losi.create.math

import kotlin.experimental.and
import kotlin.experimental.or

open class Vector3b : Vector2b
{
    companion object {
        internal const val B3_MASK: Byte = 0b00000100
        internal const val B3_MASK_INV: Byte = 0b11111011.toByte()

        fun asComposite(x: Boolean, y: Boolean, z: Boolean) = ((if (x) B1_MASK else 0) or (if (y) B2_MASK else 0) or (if (z) B3_MASK else 0))
    }

    constructor(x: Boolean, y: Boolean, z: Boolean): super(asComposite(x,y, z))
    constructor(composite: Byte): super(composite)
    constructor(value: Boolean): super(value)
    constructor()

    @set:JvmName("z")
    @get:JvmName("z")
    var z : Boolean
        get() = (composite and B3_MASK) == B3_MASK
        set(it) { composite = (composite and B3_MASK_INV) or (if (it) B3_MASK else 0) }
}