package com.losi.create.graphics.gl

@JvmInline
value class ShaderProgram(val handle: UInt) {
    companion object {
        @JvmStatic
        val NONE = ShaderProgram(0u)
    }
}