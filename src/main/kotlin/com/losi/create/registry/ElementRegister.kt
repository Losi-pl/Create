@file:Suppress("unused")
package com.losi.create.registry

import com.koloboke.collect.map.hash.HashObjObjMaps
import java.util.HashMap

//TODO: Make more asynchronous
class ElementRegister<T: GameElement>
{
    private var elements_by_name_raw: HashMap<String, T>? = HashMap<String, T>()
    private var elements_by_name: Map<String, T>? = null

    private var elements_by_id: Map<ULong, T>? = null
    private val sync = Any()

    fun register(element: T, name: String) = synchronized (sync)
    {
        if(elements_by_name_raw == null)
            throw RuntimeException("Register has been closed")

        element.name = name
        if(elements_by_name_raw?.putIfAbsent(name, element) != null)
            throw IllegalArgumentException("Element \"$name\" already exists")
    }

    fun retrieve(name: String): T = synchronized (sync)
    { (elements_by_name ?: elements_by_name_raw ?: throw IllegalArgumentException("Element \"$name\" does not exist"))[name] as T }

    fun completed() = elements_by_name != null

    internal fun complete(): Unit = synchronized (sync)
    {
        if(completed())
            return

        elements_by_name = HashObjObjMaps.newImmutableMap(elements_by_name_raw!!)
        elements_by_name_raw = null
    }


    val count: Int get() = synchronized(sync)
    { (elements_by_name ?: elements_by_name_raw ?: throw RuntimeException("Register is null")).size }
}
