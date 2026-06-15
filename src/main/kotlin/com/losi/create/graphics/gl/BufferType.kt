package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class BufferType(val gl: Int, val glName: String) {
    /**Stores vertex attribute data (positions, colors, normals, texture coordinates).
     *
     * `GL_ARRAY_BUFFER`*/
    Array(GL15C.GL_ARRAY_BUFFER, "GL_ARRAY_BUFFER"),
    /**Stores index data, which tells OpenGL which vertices to connect to form triangles.
     *
     * `GL_ELEMENT_ARRAY_BUFFER`*/
    ElementArray(GL15C.GL_ELEMENT_ARRAY_BUFFER, "GL_ELEMENT_ARRAY_BUFFER"),
    /**Stores uniform data that can be shared across many shader programs.
     *
     * `GL_UNIFORM_BUFFER`*/
    Uniform(GL31C.GL_UNIFORM_BUFFER, "GL_UNIFORM_BUFFER"),
    /**Efficient texture streaming (e.g., video playback, procedural textures)
     *
     * `GL_PIXEL_UNPACK_BUFFER`*/
    PixelUnpack(GL21C.GL_PIXEL_UNPACK_BUFFER, "GL_PIXEL_UNPACK_BUFFER"),
    /**Asynchronous framebuffer capture (e.g., screenshots, recording)
     *
     * `GL_PIXEL_PACK_BUFFER`*/
    PixelPack(GL21C.GL_PIXEL_PACK_BUFFER, "GL_PIXEL_PACK_BUFFER"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}