@file:Suppress("unused")

package com.losi.create.registry

import com.losi.create.ModSpace

/**The base for the modular structure of the games mechanics*/
abstract class GameElement
{
    /**The mod this element is attached to*/
    private var _space: ModSpace? = null
    /**Human friendly name of the element*/
    private var _name: String? = null
    /**System efficient id of the element*/
    private var _uuid: ULong? = null

    /**Identifier of this element
     *
     * Set during registration of the element*/
    var name: String get() = this._name?: throw RuntimeException("Element has not been registered")
        internal set(it) { _name = it }
    /**Unique identifier of this element
     *
     * Set during world creation will remain the same for this specific world*/
    var uuid: ULong get() = this._uuid ?: throw RuntimeException("Game session was not yet started")
        internal set(it) { this._uuid = it }
    /**The Mod this element is registered to
     *
     * Set during registration of the element*/
    var space: ModSpace get() = _space?: throw RuntimeException("Element has not been registered")
        internal set(it) { _space = it }
    /**A flag, telling if the element was registered*/
    @get:JvmName("isRegistered")
    val isRegistered: Boolean get() = _name != null
    /**A flag telling if this element already has created Uuid.
     *
     * Uuid's are only assigned when the game session starts*/
    @get:JvmName("hasUuid")
    val hasUuid: Boolean get() = _uuid != null

    /**This event is called when this element is registered*/
    open fun onRegister() { }
}