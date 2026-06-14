@file:Suppress("unused")
package com.losi.create.registry

import com.koloboke.collect.map.hash.HashObjObjMaps
import com.losi.create.ModSpace
import com.losi.create.assets.Blocks
import com.losi.create.utility.splitCamelCase
import com.losi.create.world.Realms
import java.util.HashMap
import kotlin.reflect.*
import kotlin.reflect.full.*
import kotlin.sequences.forEach

/**A registry for elements of a specified type has two types of keys an identifier and a handler
 *
 * Handlers remain unset until the game instance is loaded*/
class ElementRegister<T: GameElement>
{
    companion object {
        /**This is the process of registering all game elements into the registers to be used
         *
         * Registered:
         *  - [Block][Blocks]*/
        val loadingGameElements = LoadingProcess(name = "Registering Game Elements") {
            loadFromObject(Blocks, Blocks.manifest)
        }

        /**When all elements are loaded, the registers are closed. This preventing them from being further modified and makes them faster to read*/
        val completeElementRegisters = LoadingProcess(name = "Completing Game Element Registers") {
            all.forEach { (klass, register) ->
                register.complete()
            }
        }


        /**Al created registers regardles of where*/
        private val all = mutableMapOf<KClass<*>, ElementRegister<*>>()

        /**Works as a constructor*/
        inline operator fun <reified T: GameElement> invoke() = ElementRegister(T::class)

        @PublishedApi @JvmSynthetic /**Internal processing logic of [loadFromObject]*/
        internal fun <S: Any, T: GameElement> loadFromObjectTernal(source: S, objKlass: KClass<S>, tarKlass: KClass<T>) =
            objKlass.declaredMemberProperties.asSequence().filter {
                if(it is KMutableProperty<*>)
                    return@filter false

                if(it.returnType == tarKlass.starProjectedType)
                    return@filter true

                return@filter it.returnType.isSubtypeOf(tarKlass.starProjectedType)
            }.map {
                it.name to (it.get(source)?: return@map null)
            }.filterNotNull()

        /**Used to conveniently load all elements of [Target] type to a [ElementRegister]*/
        inline fun <reified Target: GameElement, reified Source: Any> loadFromObject(source: Source, target: ElementRegister<Target>) {
            val list = loadFromObjectTernal(source, Source::class, Target::class)
            val space = ModSpace.modules["create"]!!
            val regexS1 = "[^\\p{L}0-9-_]+".toRegex()
            val regexS2 = "\\p{Mn}+".toRegex()

            list.forEach {
                val name = java.text.Normalizer.normalize(
                    it.first.splitCamelCase().lowercase().replace(regexS1, "-"),
                    java.text.Normalizer.Form.NFD).replace(regexS2, "")
                target.register(it.second as Target, space, name)
            }
        }
    }

    @PublishedApi
    internal constructor(klass: KClass<T>) {
        require(klass != GameElement::class) { "You can't create a register of Game Elements directly." }
        require(!all.containsKey(klass)) { "Register of this element already exists" }
        require(all.keys.find { it.isSuperclassOf(klass) || klass.isSuperclassOf(it) } == null) { "Register for an element from the related tree already exists" }

        all[klass] = this
    }

    /**Unfinished map of elements in the register*/
    private var rawElementsByName: HashMap<ElementIdent, T>? = HashMap<ElementIdent, T>()
    /**Finished and optimized for read only map of elements*/
    private var elementsByName: Map<ElementIdent, T>? = null
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
        check(rawElementsByName != null) { "Register has been closed" }
        require(!element.isRegistered) { "Element was already registered" }
        require("[^a-zA-Z0-9-_]+".toRegex().find(name) == null) { "Name is using illegal characters, Allowed characters [A-Z] [a-z] [0-9] '_' '-'" }

        rawElementsByName?.putIfAbsent(ElementIdent(space, name), element)?.run {
            throw IllegalArgumentException("Element \"${space.identity}:$name\" already exists")
        }

        element.name = name
        element.space = space

        element.onRegister()
    }
    /**Looks for an element by [name] in the register*/
    fun retrieve(name: String): T =
        elementsByName?.let { return it[ElementIdent(name)] ?: throw IllegalArgumentException("Element $name not found") } ?: synchronized(sync)
        { rawElementsByName?.get(ElementIdent(name)) ?: throw IllegalArgumentException("Element $name not found") }
    /**A flag, is the register completed*/
    val isCompleted: Boolean get() = elementsByName != null
    /**Finalizes the registry, making it impossible to register new elements in it*/
    private fun complete(): Unit = synchronized (sync)
    {
        if(isCompleted)
            return

        elementsByName = HashObjObjMaps.newImmutableMap(rawElementsByName!!)
        rawElementsByName = null
    }
    /**The count of elements registered in this register*/
    val count: Int get() = elementsByName?.count() ?: synchronized(sync)
        { rawElementsByName?.count() ?: elementsByName?.count() ?: throw Error("Register is compromised") }
}
