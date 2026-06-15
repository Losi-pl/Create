@file:Suppress("unused")
package com.losi.create.registry

import com.losi.create.ModSpace
import com.losi.create.assets.Blocks
import com.losi.create.utility.*
import com.losi.create.world.Realms
import org.eclipse.collections.api.factory.primitive.LongObjectMaps
import org.eclipse.collections.api.map.ImmutableMap
import org.eclipse.collections.api.map.primitive.ImmutableLongObjectMap
import org.eclipse.collections.impl.map.mutable.UnifiedMap
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
         *  - [Blocks][Blocks] -> [Block][com.losi.create.assets.bases.Block]
         *  - [Realms][Realms] -> [Realm][com.losi.create.world.Realm]*/
        val loadingGameElements = LoadingProcess(name = "Registering Game Elements") {
            loadFromObject(Blocks, Blocks.manifest)
            loadFromObject(Realms, Realms.manifest)
        }

        /**When all elements are loaded, the registers are closed. This preventing them from being further modified and makes them faster to read*/
        val completeElementRegisters = LoadingProcess(name = "Completing Game Element Registers") {
            all.forEach { (klass, register) ->
                register.complete()
            }
        }

        /**Loads Uuid's for [all] elements in the game
         *
         * It is meant for when the game gets to a stage where saves can be made and is meant for more compact data storage*/
        internal fun loadElementUuids(uuids: Sequence<Pair<String, Sequence<Pair<String, ULong>>>>) {
            val toAssign = all.toMutableMap()
            uuids.forEach { (target, uuids) ->
                val current = toAssign.findFirst { it.key.java.typeName == target }?.apply { toAssign.remove(this.key) }.orElse { return@forEach }.value
                current.assignUuids(uuids)
            }
            toAssign.forEach { (_, register) -> register.assignUuids(sequenceOf()) }
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
    private var rawElementsByName: UnifiedMap<ElementIdent, T>? = UnifiedMap.newMap()
    /**Finished and optimized for read only map of elements*/
    private var elementsByName: ImmutableMap<ElementIdent, T>? = null
    /**Map of elements by the handler optimized for efficient reading from storage and space usage*/
    private var elementsByUuid: ImmutableLongObjectMap<T>? = null
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
        elementsByName?.let { it[ElementIdent(name)] ?: throw IllegalArgumentException("Element $name not found") } ?: synchronized(sync)
        { rawElementsByName?.get(ElementIdent(name)) ?: throw IllegalArgumentException("Element $name not found") }
    /**Looks for an element by [uuid] in the register*/
    fun retrieve(uuid: ULong): T =
        elementsByUuid.orElse { throw IllegalStateException("Game session has not yes been started") }.get(uuid.toLong())?:
            throw IllegalArgumentException("Element with uuid:$uuid not found")

    /**A flag, is the register completed*/
    val isCompleted: Boolean get() = elementsByName != null
    /**Finalizes the registry, making it impossible to register new elements in it*/
    private fun complete(): Unit = synchronized (sync)
    {
        if(isCompleted)
            return

        elementsByName = rawElementsByName!!.toImmutable()
        rawElementsByName = null
    }
    /**The count of elements registered in this register*/
    val count: Int get() = elementsByName?.count() ?: synchronized(sync)
        { rawElementsByName?.size ?: elementsByName?.size() ?: throw Error("Register is compromised") }
    /**Loads uuid's of all elements from the [Sequence] and loads them to the registry */
    private fun assignUuids(ids: Sequence<Pair<String, ULong>>): Set<Pair<String, ULong>> {
        val used = org.eclipse.collections.api.factory.primitive.LongSets.mutable.empty()
        fun findFree(): ULong { var l = ULong.MAX_VALUE
            while(true) { if(used.add((++l).toLong())) return l }
        }

        val unknown = mutableSetOf<Pair<String, ULong>>()
        val elements = elementsByName?: rawElementsByName!!

        val byUuid = LongObjectMaps.mutable.of<T>().apply {
            ids.forEach { id ->
                used.add(id.second.toLong())
                elements[ElementIdent(id.first)]?.apply { uuid = id.second; put(uuid.toLong(), this) }
                    .orElse { unknown.add(id) }
            }
            elements.valuesView().asSequence().filter { !it.hasUuid }.forEach {
                val next = findFree()
                it.uuid = next
                put(next.toLong(), it)
            }
        }
        elementsByUuid = LongObjectMaps.immutable.ofAll(byUuid)
        return unknown
    }
}