@file:Suppress("unused")
package com.losi.create.registry

import com.koloboke.collect.map.hash.HashObjObjMaps
import java.util.HashMap

/**A registry for elements of a specified type has two types of keys an identifier and a handler
 *
 * Handlers remain unset until the game instance is loaded*/
class ElementRegister<T: GameElement>
{
    /**Unfinished map of elements in the register*/
    private var rawElementsByName: HashMap<String, T>? = HashMap<String, T>()
    /**Finished and optimized for read only map of elements*/
    private var elementsByName: Map<String, T>? = null
    /**Map of elements by the handler optimized for efficient reading from storage and space usage*/
    private var elementsById: Map<ULong, T>? = null
    /**For thread synchronization*/
    private val sync = Any()
    /**Add a new element the registry
     *
     *  - Throws an error if [element] was already registered
     *  - Throws an error if [name] is already taken*/
    fun register(element: T, name: String) = synchronized (sync)
    {
        if(rawElementsByName == null)
            throw RuntimeException("Register has been closed")
        if(element.isRegistered)
            throw IllegalArgumentException("Element was already registered")

        if(rawElementsByName?.putIfAbsent(name, element) != null)
            throw IllegalArgumentException("Element \"$name\" already exists")
        element.name = name
    }
    /**Looks for an element by [name] in the register*/
    fun retrieve(name: String): T =
        elementsByName?.get(name) ?: synchronized(sync)
        { rawElementsByName?.get(name) ?: throw NullPointerException() }
    /**A flag, is the register completed*/
    fun completed() = elementsByName != null
    /**Finalizes the registry, making it impossible to register new elements in it*/
    internal fun complete(): Unit = synchronized (sync)
    {
        if(completed())
            return

        elementsByName = HashObjObjMaps.newImmutableMap(rawElementsByName!!)
        rawElementsByName = null
    }
    /**The count of elements registered in this register*/
    val count: Int get() {
        return if(elementsByName != null)
            elementsByName!!.size
        else synchronized(sync)
        { rawElementsByName!!.size }
    }
}
