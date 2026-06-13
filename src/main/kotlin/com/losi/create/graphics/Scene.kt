@file:Suppress("unused")
package com.losi.create.graphics

import com.losi.create.graphics.gl.*
import com.losi.create.utility.OnMainThread
import com.losi.create.utility.unaccessible
import org.joml.Vector2i
import java.awt.Color
import java.io.InputStream

abstract class Scene {
    private var _win: Window? = null
    private var background: Color = Color.black
    private var backgroundChanged = false

    protected var title: String
        set(value) { _win?.let { it.title = value } }
        get() = _win?.title?: ""

    protected var backgroundColor: Color
        set(value) { background = value; backgroundChanged = true }
        get() = background

    protected var targetFPS: UInt
        set(value) { _win?.let { it.targetFPS = value } }
        get() = _win?.targetFPS?: 0u

    protected var windowSize: Vector2i
        set(value) { _win?.let { it.size = value } }
        get() = _win?.size?: Vector2i()

    /**A setter for the [Window]'s icon the icon, it can only be set and not got back*/
    @get:Deprecated("The getter of this variable does not exist", level = DeprecationLevel.ERROR)
    protected var icon: InputStream?
        set(value) { _win?.let { it.icon = value } }
        get() = unaccessible()

    typealias Key = KeyboardKey
    typealias KeyCode = KeyboardKeyCode
    typealias KeyMods = com.losi.create.graphics.gl.KeyMods
    typealias KeyAction = KeyboardAction

    open fun onSceneInit() { }
    open fun onKeyAction(key: Key, action: KeyAction, mods: KeyMods) { }
    open fun onKeyDown(key: Key, mods: KeyMods) { }
    open fun onKeyUp(key: Key, mods: KeyMods) { }
    open fun onKeyRepeat(key: Key, mods: KeyMods) { }

    open fun logicUpdate(timeDelta: Float) { }
    open fun renderUpdate(timeDelta: Float) { }

    /**Creates a thread that runs the specified block of code. This is a variation
     * @param start if `true`, the thread is immediately started.
     * @param isDaemon if `true`, the thread is created as a daemon thread. The Java Virtual Machine exits when the only threads running are all daemon threads.
     * @param contextClassLoader - the class loader to use for loading classes and resources in this thread.
     * @param name the name of the thread.
     * @param priority the priority of the thread.*/
    protected fun thread(start: Boolean = true,
                         isDaemon: Boolean = false,
                         contextClassLoader: ClassLoader? = null,
                         name: String? = null,
                         priority: Int = -1,
                         block: () -> Unit): Thread {
        check(_win != null) { "The scene is not currently utilized" }

        val context = OnMainThread.query { GLContext(_win!!) }
        val thread = object : Thread() {
            override fun run() {
                context.threadBind()
                context.use { block() }
            }
        }

        if (isDaemon)
            thread.isDaemon = true
        if (priority > 0)
            thread.priority = priority
        if (name != null)
            thread.name = name
        if (contextClassLoader != null)
            thread.contextClassLoader = contextClassLoader
        if (start)
            thread.start()
        return thread
    }

    internal fun update(timeDelta: Float) {
        val me = this

        me.logicUpdate(timeDelta)

        if(backgroundChanged)
        {
            glClearColor(background)
            backgroundChanged = false
        }

        glClear(ClearTarget.Color and ClearTarget.Depth)
        me.renderUpdate(timeDelta)
    }
    internal fun bindingScene(window: Window) {
        val me = this
        _win = window
        me.onSceneInit()
    }
    internal fun unbindingScene() {
        _win = null
    }
}