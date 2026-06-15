package com.losi.create.graphics.gl

@JvmInline
value class VertexArray(val handle: UInt){
    companion object {
        val NONE = VertexArray(0u)
    }
}