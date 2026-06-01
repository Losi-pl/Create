@file:Suppress("unused")
package com.losi.create.graphics

import org.joml.Vector2i
import org.lwjgl.glfw.GLFWVidMode
import org.lwjgl.system.MemoryStack
import org.lwjgl.glfw.GLFW.*
import java.util.Collections

/**A handler for computer monitors*/
class Monitor
{
    companion object
    {
        /**List of all currently recognized by OpenGL monitors*/
        var monitors: MutableList<Monitor>? = null
        /**The read only reflection of [monitors]*/
        var readOnlyMonitors: List<Monitor>? = null

        /**Returns a list of all monitors currently detected by OpenGL*/
        @JvmStatic val list: List<Monitor> get(){
            if(readOnlyMonitors == null)
                load()
            return readOnlyMonitors!!
        }
        /**Creates a list of all monitors currently recognized by OpenGL*/
        @JvmStatic private fun load() {
            monitors = Collections.synchronizedList(mutableListOf())
            readOnlyMonitors = Collections.unmodifiableList(monitors!!)
            val monitors = glfwGetMonitors()!!
            for (i in 0 until monitors.limit())
                Monitor.monitors?.add(Monitor(monitors.get(i)))
        }
    }

    /**The handler to the specific monitor in the OpenGL*/
    private val _handler: Long
    /**The OpenGL struct with data of the monitors view*/
    private var vidmode: GLFWVidMode? = null
    /**The human friendly name of the monitor*/
    private var _name: String? = null

    /**Creates this wrapper for the OpenGL methods for the monitor*/
    private constructor( handler: Long) { _handler = handler; }

    /**The pixel width of the monitor*/
    val width: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.width()
    }
    /**The pixel height of the monitor*/
    val height: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.height()
    }
    /**Refresh rate of the monitor (FPS)*/
    val framerate: Int get() {
        if(vidmode == null)
            loadMonitor()
        return vidmode!!.refreshRate()
    }
    /**The virtual coordinates of the monitor*/
    val position: Vector2i get() {
        MemoryStack.stackPush().use { stack ->
            val x = stack.mallocInt(1); val y = stack.mallocInt(1)
            glfwGetMonitorPos(handler, x, y)
            return Vector2i(x[0], y[0])
        }
    }
    /**The usable space of the monitor and its position within the virtual coordinate system*/
    val workArea: WorkArea get() {
        MemoryStack.stackPush().use { stack->
            val x = stack.mallocInt(1); val width = stack.mallocInt(1)
            val y = stack.mallocInt(1); val height = stack.mallocInt(1)
            glfwGetMonitorWorkarea(handler, x, y, width, height)
            return WorkArea(x[0], y[0], width[0], height[0])
        }
    }
    /**The name of the monitor*/
    val name: String get() {
        if(_name == null)
            _name = glfwGetMonitorName(handler)
        return _name!!
    }
    /**OpenGL handler of the monitor*/
    val handler: Long get() { return _handler; }

    /**Loads [GLFWVidMode] struct with data of this monitor*/
    private fun loadMonitor() { vidmode = glfwGetVideoMode(handler); }

    /**Usable space of a monitor, not blocked by system UI*/
    data class WorkArea(val x: Int, val y: Int, val width: Int, val height: Int) {
        val position: Vector2i get() = Vector2i(x, y)
        val size: Vector2i get() = Vector2i(width, height)
    }
}
