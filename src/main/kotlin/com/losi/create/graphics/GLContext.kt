package com.losi.create.graphics

import com.losi.create.graphics.Window.Companion.currentContext
import com.losi.create.internal.GLErrorHandler
import org.lwjgl.glfw.GLFW.*
import org.lwjgl.opengl.GL
import org.lwjgl.system.MemoryUtil.*

internal interface GContext

class GLContext: GContext, AutoCloseable {
    private val handle: Long
    private var threadBound: Boolean = false

    constructor(window: Window) {
        glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE)
        handle = glfwCreateWindow(1, 1, "", NULL, window.handle)
    }

    fun threadBind() = synchronized (currentContext)
    {
        if(threadBound)
            throw IllegalStateException("Context is already bound to a Thread")
        if(currentContext.get() != null)
            throw IllegalStateException("Thread is already bound to a Window or Context")
        currentContext.set(this)
        glfwMakeContextCurrent(handle)
        GL.createCapabilities()
        GLErrorHandler.bindErrorCather()

        threadBound = true
    }

    fun release() = synchronized (currentContext) {
        if(!threadBound)
            return@synchronized
        if(currentContext.get() == null)
            return@synchronized
        if(currentContext.get() != this)
            throw IllegalStateException("This is not context bound to this thread")

        glfwMakeContextCurrent(NULL)
        currentContext.set(null)
        threadBound = false
    }

    override fun close() {
        release()
        glfwDestroyWindow(handle)
    }
}