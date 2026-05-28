package com.losi.create.assets

import com.losi.create.ModSpace
import com.losi.create.Version
import com.losi.create.utility.startsWithFrom
import java.io.File
import java.io.InputStream
import java.util.*
import java.util.jar.JarFile
import java.net.URL
import java.util.jar.JarEntry
import kotlin.reflect.KClass
import kotlin.reflect.full.isSubclassOf
import kotlin.reflect.full.isSuperclassOf

import com.losi.create.graphics.Shader
/**The main manager of the game assets like Shaders, Textures, Languages, etc...
 *
 * ---
 *
 * Currently supported:
 * - [Shader] -> `shader/`*/
@Suppress("unused")
object AssetManager {
    private val assetLoaders = HashMap<ResourceSpace, (String) -> InputStream?>()
    private val typeProcessors = HashMap<KClass<*>, Pair<AssetTypeProcessor<*>, String>>()
    private var assetsLoaded = false

    /**A data class made for organization of resources during processing in [processAssets]
     *
     * It can store known and unknown types of data, whether the type is known or not is determined by the presence of the path to it in [typeProcessors]*/
    private abstract class ProcType {
        companion object {
            fun of(`class`: KClass<*>): ProcType = Known(`class`)
            fun of(name: String): ProcType = Unknown(name)
        }
        abstract val name: String
        /**The type of data is known and the [KClass] is used as key for it in [typeProcessors]*/
        data class Known(val type: KClass<*>): ProcType() {
            override fun toString() = "Known: ${type.simpleName}"
            override val name: String get() = typeProcessors[type]!!.second
            override fun hashCode() = type.hashCode()
            override fun equals(other: Any?): Boolean {
                if (this === other) return true
                if (javaClass != other?.javaClass) return false
                return type == (other as Known).type
            }
        }
        /**The type of data is not known so for the future if something is done about it this will store the path to the unknow data*/
        data class Unknown(val type: String): ProcType() {
            override fun toString() = "Unknown: ${type.substring(0, type.length - 1)}"
            override val name: String get() = type
            override fun hashCode() = type.hashCode()
            override fun equals(other: Any?): Boolean {
                if (this === other) return true
                if (javaClass != other?.javaClass) return false
                return type == (other as Unknown).type
            }
        }
    }

    /**It's meant for loading all path from where the assets can be loaded
     *
     * At the moment the only available path is the module of the game itself, but in the future it is planet do add things like mods or resource packs*/
    @JvmStatic
    internal fun constructAssetLoader() {
        val mods = ModSpace.modules
        for (mod in mods)
        {
            assetLoaders[mod.value.resourceSpace] = { Version::class.java.module.getResourceAsStream(it) }
        }
    }

    /**Splits a path into identifier of [ModSpace] and the in type path to the resource itself
     * @param it A full path in the resources, `<ModSpace>/<DataType>/<Path...>`
     * @param offset A length of `<DataType>` acquired from [ProcType]
     * @return A combination of `<ModSpace>` / `<Path...>` in that order, both in String*/
    private fun split(it: String, offset: Int): Pair<String, String> {
        val firstBreak = it.indexOf('/')
        return Pair(it.substring(0, firstBreak), it.substring(firstBreak + offset + 1, it.length))
    }

    /**Figures out the [ProcType] from a path to a resource
     * @param path A path to a resource `<ModSpace>/<DataType>/<Path...>`
     * @return If a type is recognized it will return [ProcType.Known] containing the type of data that works as the key for it.
     * If the type is not known it will return [ProcType.Unknown] containing the path to that type*/
    private fun group(path: String): ProcType {
        typeProcessors.forEach { (klass, pair) ->
            if(path.startsWithFrom(pair.second, path.indexOf('/')+1))
                return ProcType.of(klass)
        }
        val firstBreak = path.indexOf('/') + 1
        val secondBreak = path.indexOf('/', firstBreak) + 1
        return ProcType.of(path.substring(firstBreak, secondBreak))
    }

    /**This method an entry point to processing all resources into assets.
     * First it maps paths to all files and all resources then groups the by: `ResourceSpace` -> `AssetType` -> `ModSpace`
     *
     * The `AssetType`'s are stored in [typeProcessors] where they store a [KClass] serving both as the type of data itself and the key to access it, and a combination
     * of a specific processor for a type [AssetTypeProcessor] combined with its type specific path in resources
     *
     * When all paths have been mapped and groups they are passed to type specific processors
     * ```
     *  <ModSpace>/<AssetType>/<Path...>
     *
     *  <ModSpace>
     *   |
     *   +-<AssetType>
     *   |  |
     *   |  +-<Path...>
     *   |  |
     *   |  +-<Path...>
     *   |
     *   +-<AssetType>
     *   |  |
     *   |  +-<Path...>
     *   |  |
     *   |  +-<Path...>
     * ```
     * */
    internal fun processAssets() {

        val paths = listResourceFiles("assets").asSequence().map { Pair(group(it), it) }
            .groupBy(keySelector = { it.first },  valueTransform = { it.second  })
            .mapValues {
                ti -> ti.value.asSequence()
                    .map { split(it, ti.key.name.length) }
                    .groupBy(keySelector = {it.first}, valueTransform = {it.second})
                .mapKeys { ModSpace.modules[it.key] ?: ModSpace("", it.key, ResourceSpace(), true) }
            }


        AssetTypeProcessor.order.set(listOf(ModSpace.modules["create"]!!.resourceSpace))

        paths.forEach { type ->
            if(type.key is ProcType.Unknown)
                return@forEach
            val processor = typeProcessors[(type.key as ProcType.Known).type]
            AssetTypeProcessor.nameType.set(processor!!.second)
            processor.first.processResources(mapOf(ModSpace.modules["create"]!!.resourceSpace to type.value))
        }

        assetsLoaded = true
    }

    /**This method is used to register a new type of data to be detected and processed
     * @param proc An object implementing the [AssetTypeProcessor] interface containing mechanisms to process and store objects of type [T]
     * @param name A path in resources where this specific type of resources is going to be stored. It must be unique and not repeat or overlap with any other type meaning it cant be a sub path to another type but must be unique
     * @param T A type of objects this specific mechanism is meant for processing it, the type must be unique not deriving from or being derived from any other registered type*/
    inline fun <reified T: Any> registerProcessor(proc: AssetTypeProcessor<T>, name: String) = registerProcessor(proc, T::class, name)
    /**This method is used to register a new type of data to be detected and processed
     * @param proc An object implementing the [AssetTypeProcessor] interface containing mechanisms to process and store objects of type [T]
     * @param name A path in resources where this specific type of resources is going to be stored. It must be unique and not repeat or overlap with any other type meaning it cant be a sub path to another type but must be unique
     * @param T A type of objects this specific mechanism is meant for processing it, the type must be unique not deriving from or being derived from any other registered type
     * @param type The class object which is required for mother to find the correct data type*/
    @JvmStatic fun <T: Any> registerProcessor(proc: AssetTypeProcessor<T>, type: KClass<T>, name: String) = synchronized(typeProcessors)
    {
        val procName = name.replace('\\', '/').let { if(it.last() == '/') it else "$it/" }
        val procBarLast = lazy { procName.substringBeforeLast('/') }

        if(typeProcessors.containsKey(type))
            throw IllegalArgumentException("Processor for type $type has already been registered")
        typeProcessors.forEach { (cls, pir) ->
            if(cls.isSubclassOf(type) || cls.isSuperclassOf(type))
                throw IllegalArgumentException("Processor for type $type is blocked by presence of $cls")
            if(pir.second == procName)
                throw IllegalArgumentException("The type name \"${procBarLast.value}\" is already taken")
            if(pir.second.startsWith(procName) || procName.startsWith(pir.second))
                throw IllegalArgumentException("The type path \"${procBarLast.value}\" is already taken locked by ${pir.second.substringBeforeLast('/')}")
        }
        typeProcessors[type] = Pair(proc, procName)
    }
    /**This method is used to register a new type of data to be detected and processed
     * @param proc An object implementing the [AssetTypeProcessor] interface containing mechanisms to process and store objects of type [T]
     * @param name A path in resources where this specific type of resources is going to be stored. It must be unique and not repeat or overlap with any other type meaning it cant be a sub path to another type but must be unique
     * @param T A type of objects this specific mechanism is meant for processing it, the type must be unique not deriving from or being derived from any other registered type
     * @param type The class object which is required for mother to find the correct data type*/
    @JvmStatic fun <T: Any> registerProcessor(proc: AssetTypeProcessor<T>, type: Class<T>, name: String) = registerProcessor(proc, type.kotlin, name)
    /**Used to take out data that has already been loaded and processed earlier
     * @param name A compact name of the asset in format of `<ModSpace>:<Path...>`
     * @param T The type of resource that is being looked for*/
    inline fun <reified T: Any> get(name: String): T? = get(T::class, name) as? T
    /**Used to take out data that has already been loaded and processed earlier
     * @param name A compact name of the asset in format of `<ModSpace>:<Path...>`
     * @param T The type of resource that is being looked for*/
    @JvmStatic fun <T: Any> get(klass: KClass<T>, name: String): Any? {
        if(!assetsLoaded) throw RuntimeException("The assets have not yet been loaded")
        val proc = typeProcessors[klass] ?: return null
        return proc.first.getAsset(name)
    }
    /**Used to take out data that has already been loaded and processed earlier
     * @param name A compact name of the asset in format of `<ModSpace>:<Path...>`
     * @param T The type of resource that is being looked for*/
    @JvmStatic fun <T: Any> get(klass: Class<T>, name: String): Any? {
        if(!assetsLoaded) throw RuntimeException("The assets have not yet been loaded")
        val proc = typeProcessors[klass.kotlin] ?: return null
        return proc.first.getAsset(name)
    }
    /**Used to take out data that has already been loaded and processed earlier
     * @param mod The specific modification you are tying from
     * @param name Name or path to the specific object with the [ModSpace]
     * @param T The type of resource that is being looked for*/
    inline fun <reified T: Any> get(mod: ModSpace, name: String): T? = get(T::class, mod, name) as? T
    /**Used to take out data that has already been loaded and processed earlier
     * @param mod The specific modification you are tying from
     * @param name Name or path to the specific object with the [ModSpace]
     * @param T The type of resource that is being looked for*/
    @JvmStatic fun <T: Any> get(klass: KClass<T>, mod: ModSpace, name: String): Any? {
        if(!assetsLoaded) throw RuntimeException("The assets have not yet been loaded")
        val proc = typeProcessors[klass] ?: return null
        return proc.first.getAsset(mod, name)
    }
    /**Used to take out data that has already been loaded and processed earlier
     * @param mod The specific modification you are tying from
     * @param name Name or path to the specific object with the [ModSpace]
     * @param T The type of resource that is being looked for*/
    @JvmStatic fun <T: Any> get(klass: Class<T>, mod: ModSpace, name: String): Any? {
        if(!assetsLoaded) throw RuntimeException("The assets have not yet been loaded")
        val proc = typeProcessors[klass.kotlin] ?: return null
        return proc.first.getAsset(mod, name)
    }

    /**This method is for getting an [InputStream] to a resource
     * @param source The specific source of resources being queried
     * @param path A path within the [ResourceSpace] to be loaded from*/
    internal fun getStream(source: ResourceSpace, path: String): InputStream? = assetLoaders[source]?.invoke(path)
    /**This is a method I got from the internet, it is meant for getting a manifest of all files available in the [ClassLoader]
     * @param resourceFolder A sub path to be manifested
     * @author DeepSeek*/
    internal fun listResourceFiles(resourceFolder: String): MutableList<String> {
        val fileNames = ArrayList<String>()
        val classLoader = Thread.currentThread().contextClassLoader
        val folderUrl: URL? = classLoader.getResource(resourceFolder)

        requireNotNull(folderUrl) { "Resource folder not found: $resourceFolder" }

        if (folderUrl.protocol.equals("file")) {
            val folder = File(folderUrl.toURI())
            val files = folder.listFiles()
            if (files != null) {
                for (file in files) {
                    if (file.isFile()) {
                        fileNames.add(file.getName())
                    }
                }
            }
        }
        else if (folderUrl.protocol.equals("jar")) {
            val jarPath = folderUrl.path.substring(5, folderUrl.path.indexOf("!"))
            JarFile(jarPath).use { jarFile ->
                val entries = jarFile.entries()
                while (entries.hasMoreElements()) {
                    val entry: JarEntry = entries.nextElement()
                    val entryName: String = entry.getName()
                    if (entryName.startsWith("$resourceFolder/") && !entry.isDirectory) {
                        val relativePath = entryName.substring(resourceFolder.length + 1)
                        fileNames.add(relativePath)
                    }
                }
            }
        }
        else { throw UnsupportedOperationException("Unsupported protocol: " + folderUrl.protocol) }

        return fileNames
    }
}