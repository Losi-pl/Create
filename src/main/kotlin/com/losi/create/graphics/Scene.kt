@file:Suppress("unused")
package com.losi.create.graphics

import com.losi.create.graphics.gl.*
import org.joml.Vector2i
import java.awt.Color

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

    open fun onSceneInit() { }
    open fun onKeyAction(key: KeyboardKey, action: KeyboardAction, mods: KeyMods) { }
    open fun onKeyDown(key: KeyboardKey, mods: KeyMods) { }
    open fun onKeyUp(key: KeyboardKey, mods: KeyMods) { }
    open fun onKeyRepeat(key: KeyboardKey, mods: KeyMods) { }

    open fun logicUpdate(timeDelta: Float) { }
    open fun renderUpdate(timeDelta: Float) { }

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