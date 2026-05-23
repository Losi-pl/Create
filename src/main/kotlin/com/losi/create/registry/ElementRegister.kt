@file:Suppress("unused")
package com.losi.create.registry

import com.koloboke.collect.map.hash.HashObjObjMaps
import java.util.HashMap

//TODO: Make better asynchronous
class ElementRegister<T: GameElement>
{
    private var rawElementsByName: HashMap<String, T>? = HashMap<String, T>()
    private var elementsByName: Map<String, T>? = null

    private var elementsById: Map<ULong, T>? = null
    private val sync = Any()

    fun register(element: T, name: String) = synchronized (sync)
    {
        if(rawElementsByName == null)
            throw RuntimeException("Register has been closed")

        element.name = name
        if(rawElementsByName?.putIfAbsent(name, element) != null)
            throw IllegalArgumentException("Element \"$name\" already exists")
    }

    fun retrieve(name: String): T = synchronized (sync)
    { (elementsByName ?: rawElementsByName ?: throw IllegalArgumentException("Element \"$name\" does not exist"))[name] as T }

    fun completed() = elementsByName != null

    internal fun complete(): Unit = synchronized (sync)
    {
        if(completed())
            return

        elementsByName = HashObjObjMaps.newImmutableMap(rawElementsByName!!)
        rawElementsByName = null
    }


    val count: Int get() = synchronized(sync)
    { (elementsByName ?: rawElementsByName ?: throw RuntimeException("Register is null")).size }
}
