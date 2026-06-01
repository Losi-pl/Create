@file:Suppress("unused")

package com.losi.create.registry

/**The base for the modular structure of the games mechanics*/
abstract class GameElement
{
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

    /**A flag, telling if the element was registered*/
    @get:JvmName("isRegistered")
    val isRegistered: Boolean get() = _name != null
}