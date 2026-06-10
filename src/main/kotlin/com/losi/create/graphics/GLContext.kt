package com.losi.create.graphics

import com.losi.create.graphics.Window.Companion.currentContext
import com.losi.create.graphics.gl.bindErrorCather
import com.losi.create.utility.OnMainThread
import org.lwjgl.glfw.GLFW.*
import org.lwjgl.opengl.GL
import org.lwjgl.system.MemoryUtil.*

/**Made for [Window] and [GLContext] for sake of [Window.currentContext]*/
internal interface InternalGLContext

/**A way to bind a secondary thread to the same context as a [Window]*/
class GLContext: InternalGLContext, AutoCloseable {
    private val handle: Long
    private var threadBound: Boolean = false

    /**Creates a new unbound context expanding the [window]*/
    constructor(window: Window) {
        glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE)
        handle = glfwCreateWindow(1, 1, "", NULL, window.handle)
    }

    /**Binds a context to a thread
     *
     * Will fail if this context is already bound to another thread or there is another context bound to this thread*/
    fun threadBind() = synchronized (currentContext)
    {
        if(threadBound)
            throw IllegalStateException("Context is already bound to a Thread")
        if(currentContext.get() != null)
            throw IllegalStateException("Thread is already bound to a Window or Context")
        currentContext.set(this)
        glfwMakeContextCurrent(handle)
        GL.createCapabilities()
        bindErrorCather()

        threadBound = true
    }

    /**Unbinds the context from a thread, only works when called from the thread it is bound to*/
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

    /**Dissolves the resources in the GPU bound to this class*/
    override fun close() {
        release()
        if(OnMainThread.isMain())
            glfwDestroyWindow(handle)
        else
            OnMainThread.schedule { glfwDestroyWindow(handle) }
    }
}