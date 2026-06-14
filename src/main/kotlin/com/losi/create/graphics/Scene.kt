@file:Suppress("unused")
package com.losi.create.graphics

import com.losi.create.graphics.gl.*
import com.losi.create.utility.*
import org.joml.Vector2i
import java.awt.Color
import java.io.InputStream

abstract class Scene {
    /**The window this [Scene] is currently connected to*/
    private var _win: Window? = null
    /**Color of the default background after the render buffer is cleared*/
    private var background: Color = Color.black
    /**A flag specifying if the background color has been changed since last render update*/
    private var backgroundChanged = true
    /**Title of the Window, inherited from previous [Scene]*/
    protected var title: String
        set(value) { _win?.let { it.title = value } }
        get() = _win?.title?: ""
    /**Default background color*/
    protected var backgroundColor: Color
        set(value) { background = value; backgroundChanged = true }
        get() = background
    /**Targeted refresh rate for this window, inherited from previous [Scene]*/
    protected var targetFPS: UInt
        set(value) { _win?.let { it.targetFPS = value } }
        get() = _win?.targetFPS?: 0u
    /**Size of the window, inherited from previous [Scene]*/
    protected var windowSize: Vector2i
        set(value) { _win?.let { it.size = value } }
        get() = _win?.size?: Vector2i()

    /**A setter for the [Window]'s icon the icon, it can only be set and not got back*/
    @get:Deprecated("The getter of this variable does not exist", level = DeprecationLevel.ERROR)
    protected var icon: InputStream?
        set(value) { _win?.let { it.icon = value } }
        get() = unaccessible()

    /**For Keyboard events. The specific key used by the user*/
    typealias Key = KeyboardKey
    /**For Keyboard events. The codes for specific keys*/
    typealias KeyCode = KeyboardKeyCode
    /**For Keyboard events. The special states that might influence the type of reaction.
     *
     * `Shift`, `Control`, `Alt`, etc.*/
    typealias KeyMods = com.losi.create.graphics.gl.KeyMods
    /**For Keyboard events. Specific action taken by the user.
     *
     * `Press`ed, `Releas`ed, `Repeat`ed*/
    typealias KeyAction = KeyboardAction

    /**Called when the scene is first bound to the window used primarily to change the window specific settings or load Scene content if not done before*/
    open fun onSceneInit() { }
    /**Called when any keyboard related action is taken
     *
     * `Key Pressed`, `Key Released`, `Key Double Pressed`
     * @param key The specific key that this action was called for
     * @param action The type of action this method was called for
     * @param mods The extra modifications that can influence the result for this button. See [KeyMods]*/
    open fun onKeyAction(key: Key, action: KeyAction, mods: KeyMods) { }
    /**Called when any key is pressed down
     * @param key The specific key that this action was called for
     * @param mods The extra modifications that can influence the result for this button. See [KeyMods]*/
    open fun onKeyDown(key: Key, mods: KeyMods) { }
    /**Called when any key is released
     * @param key The specific key that this action was called for
     * @param mods The extra modifications that can influence the result for this button. See [KeyMods]*/
    open fun onKeyUp(key: Key, mods: KeyMods) { }
    /**Called when any key pressed down quickly again
     * @param key The specific key that this action was called for
     * @param mods The extra modifications that can influence the result for this button. See [KeyMods]*/
    open fun onKeyRepeat(key: Key, mods: KeyMods) { }

    /**Meant for per frame logic processing, any rendering done during this call will be lost
     * @param timeDelta Time since last call*/
    open fun logicUpdate(timeDelta: Float) { }
    /**Meant for frame rendering related logic
     * @param timeDelta Time since last call*/
    open fun renderUpdate(timeDelta: Float) { }

    /**Creates a thread that runs the specified block of code. This is a variation for [Scene]'s automatically connecting with the OpenGL
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

    /**The set of per frame logic put in correct order
     * @param timeDelta Time since last call*/
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
    /**Used when this scene is being connected to a [Window]*/
    internal fun bindingScene(window: Window) {
        val me = this
        _win = window
        me.onSceneInit()
    }
    /**Used when this scene is being disconnected from a [Window]*/
    internal fun unbindingScene() {
        _win = null
    }
    /**Marks this window to close after this frame*/
    fun close() {
        _win?.let { glfwWindowShouldClose(it) }
    }
}