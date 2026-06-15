package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class BufferUsage(val gl: Int, val glName: String) {
    /**Data is specified once and used at most a few times as the source of drawing and image specification commands. This is the most common choice for streaming data.
     *
     * `GL_STREAM_DRAW`*/
    StreamDraw(GL15.GL_STREAM_DRAW, "GL_STREAM_DRAW"),
    /**Data is specified once and used many times as the source of drawing or image specification commands.
     *
     * `GL_STATIC_DRAW`*/
    StaticDraw(GL15.GL_STATIC_DRAW, "GL_STATIC_DRAW"),
    /**Data is specified many times and used many times as the source of drawing and image specification commands.
     *
     * `GL_DYNAMIC_DRAW`*/
    DynamicDraw(GL15.GL_DYNAMIC_DRAW, "GL_DYNAMIC_DRAW"),
    /**Data is copied once from an OpenGL buffer and is used at most a few times by the application as data values.
     *
     * `GL_STREAM_READ`*/
    StreamRead(GL15.GL_STREAM_READ, "GL_STREAM_READ"),
    /**Data is copied once from an OpenGL buffer and is used many times by the application as data values.
     *
     * `GL_STATIC_READ`*/
    StaticRead(GL15.GL_STATIC_READ, "GL_STATIC_READ"),
    /**Data is copied many times from an OpenGL buffer and is used many times by the application as data values.
     *
     * `GL_DYNAMIC_READ`*/
    DynamicRead(GL15.GL_DYNAMIC_READ, "GL_DYNAMIC_READ"),
    /**Data is copied once from an OpenGL buffer and is used at most a few times as the source for drawing or image specification commands.
     *
     * `GL_STREAM_COPY`*/
    StreamCopy(GL15.GL_STREAM_COPY, "GL_STREAM_COPY"),
    /**Data is copied once from an OpenGL buffer and is used many times as the source for drawing or image specification commands.
     *
     * `GL_STATIC_COPY`*/
    StaticCopy(GL15.GL_STATIC_COPY, "GL_STATIC_COPY"),
    /**Data is copied many times from an OpenGL buffer and is used many times as the source for drawing or image specification commands.
     *
     * `GL_DYNAMIC_COPY`*/
    DynamicCopy(GL15.GL_DYNAMIC_COPY, "GL_DYNAMIC_COPY"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}