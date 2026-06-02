@file:Suppress("unused")
package com.losi.create.registry

import com.koloboke.collect.map.hash.HashObjObjMaps
import com.losi.create.ModSpace
import com.losi.create.utility.findFirst
import java.util.HashMap

/**A registry for elements of a specified type has two types of keys an identifier and a handler
 *
 * Handlers remain unset until the game instance is loaded*/
class ElementRegister<T: GameElement>
{
    /**Unfinished map of elements in the register*/
    private var rawElementsByName: HashMap<ElementKey, T>? = HashMap<ElementKey, T>()
    /**Finished and optimized for read only map of elements*/
    private var elementsByName: Map<ElementKey, T>? = null
    /**Map of elements by the handler optimized for efficient reading from storage and space usage*/
    private var elementsById: Map<ULong, T>? = null
    /**For thread synchronization*/
    private val sync = Any()
    /**Add a new element the registry
     *
     *  - Throws an error if [element] was already registered
     *  - Throws an error if [name] is already taken*/
    fun register(element: T, space: ModSpace, name: String) = synchronized (sync)
    {
        if(rawElementsByName == null)
            throw RuntimeException("Register has been closed")
        if(element.isRegistered)
            throw IllegalArgumentException("Element was already registered")

        if(rawElementsByName?.putIfAbsent(ElementKey(space, name), element) != null)
            throw IllegalArgumentException("Element \"${space.identity}:$name\" already exists")
        element.name = name
    }
    /**Looks for an element by [name] in the register*/
    fun retrieve(name: String): T =
        elementsByName?.get(ElementKey(name)) ?: synchronized(sync)
        { rawElementsByName?.get(ElementKey(name)) ?: throw NullPointerException() }
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

    private data class ElementKey(val space: ModSpace, val identity: String)
    {
        constructor(identity: String): this(
            ModSpace.modules.findFirst { identity.startsWith(it.key) && identity[it.key.length] == ':' }?.value
                ?: throw NullPointerException("Mod with identity \"${identity.substringBefore(':')}\" was not found"),
            identity.substringAfter(':'))
    }
}
