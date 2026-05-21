@file:Suppress("unused")
package com.losi.create.graphics

import org.joml.Vector2i
import org.lwjgl.glfw.GLFWVidMode
import org.lwjgl.system.MemoryStack
import org.lwjgl.glfw.GLFW.*
import java.util.Collections

class Monitor
{
    companion object
    {
        var monitors: MutableList<Monitor>? = null
        var readOnlyMonitors: List<Monitor>? = null

        @JvmStatic val list: List<Monitor> get(){
            if(readOnlyMonitors == null)
                load()
            return readOnlyMonitors!!
        }
        @JvmStatic private fun load() {
            monitors = Collections.synchronizedList(mutableListOf())
            readOnlyMonitors = Collections.unmodifiableList(monitors!!)
            val monitors = glfwGetMonitors()!!
            for (i in 0 until monitors.limit())
                Monitor.monitors?.add(Monitor(monitors.get(i)))
        }
    }

    private val _handler: Long
    private var vidmode: GLFWVidMode? = null
    private var _name: String? = null

    private constructor( handler: Long) { _handler = handler; }

    val width: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.width()
    }
    val height: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.height()
    }
    val framerate: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.refreshRate()
    }

    val position: Vector2i get() {
        MemoryStack.stackPush().use { stack ->
            val x = stack.mallocInt(1); val y = stack.mallocInt(1)
            glfwGetMonitorPos(handler, x, y)
            return Vector2i(x[0], y[0])
        }
    }
    val workArea: WorkArea get() {
        MemoryStack.stackPush().use { stack->
            val x = stack.mallocInt(1); val width = stack.mallocInt(1)
            val y = stack.mallocInt(1); val height = stack.mallocInt(1)
            glfwGetMonitorWorkarea(handler, x, y, width, height)
            return WorkArea(x[0], y[0], width[0], height[0])
        }
    }

    val name: String get() {
        if(_name == null)
            _name = glfwGetMonitorName(handler)
        return _name!!
    }

    val handler: Long get() { return _handler; }

    private fun loadMonitor() { vidmode = glfwGetVideoMode(handler); }

    data class WorkArea(val x: Int, val y: Int, val width: Int, val height: Int) {
        val position: Vector2i get() = Vector2i(x, y)
        val size: Vector2i get() = Vector2i(width, height)
    }
}
