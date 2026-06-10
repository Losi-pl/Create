package com.losi.create.graphics.gl

@JvmInline
value class ShaderPart(val handle: UInt) {
    companion object {
        @JvmStatic
        val NONE = ShaderPart(0u)
    }
}