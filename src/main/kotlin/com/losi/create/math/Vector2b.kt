@file: Suppress("unused")

package com.losi.create.math

import kotlin.experimental.and
import kotlin.experimental.or

open class Vector2b
{
    companion object {
        internal const val B1_MASK: Byte = 0b00000001
        internal const val B1_MASK_INV: Byte = 0b11111110.toByte()

        internal const val B2_MASK: Byte = 0b00000010
        internal const val B2_MASK_INV: Byte = 0b11111101.toByte()

        fun asComposite(x: Boolean, y: Boolean) = ((if (x) B1_MASK else 0) or (if (y) B2_MASK else 0))
    }

    @set:JvmName("composite")
    @get:JvmName("composite")
    var composite: Byte = 0
    constructor()
    constructor(x: Boolean, y: Boolean) { composite = asComposite(x, y) }
    constructor(composite: Byte) { this.composite = composite; }
    constructor(value: Boolean) { composite = if(value) Byte.MAX_VALUE else 0 }

    @set:JvmName("x")
    @get:JvmName("x")
    var x : Boolean
        get() = (composite and B1_MASK) == B1_MASK
        set(it) { composite = (composite and B1_MASK_INV) or (if (it) B1_MASK else 0) }

    @set:JvmName("y")
    @get:JvmName("y")
    var y : Boolean
        get() = (composite and B2_MASK) == B2_MASK
        set(it) { composite = (composite and B2_MASK_INV) or (if (it) B2_MASK else 0) }
}