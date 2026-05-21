package com.losi.create.registry

abstract class GameElement
{
    private var _name: String? = null
    private var _uuid: ULong? = null

    var name: String get() = this._name?: throw RuntimeException("Element has not been registered")
        internal set(it) { _name = it }

    val uuid: ULong? get() = this._uuid
    @Suppress("unused")
    fun registered(): Boolean = uuid != null
}
