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


@Suppress("unused")
object Manager {
    private val assetLoaders = HashMap<ResourceSpace, (String) -> InputStream?>()
    private val typeProcessors = HashMap<KClass<*>, Pair<AssetTypeProcessor<*>, String>>()

    private abstract class ProcType {
        companion object {
            fun of(`class`: KClass<*>): ProcType = Known(`class`)
            fun of(name: String): ProcType = Unknown(name)
        }
        abstract val name: String

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

    @JvmStatic
    internal fun constructAssetLoader()
    {
        val mods = ModSpace.modules
        for (mod in mods)
        {
            assetLoaders[mod.value.resourceSpace] = { Version::class.java.module.getResourceAsStream(it) }
        }
    }

    private fun split(it: String, offset: Int): Pair<String, String> {
        val firstBreak = it.indexOf('/')
        return Pair(it.substring(0, firstBreak), it.substring(firstBreak + offset + 1, it.length))
    }
    private fun group(path: String): ProcType {
        typeProcessors.forEach { (klass, pair) ->
            if(path.startsWithFrom(pair.second, path.indexOf('/')+1))
                return ProcType.of(klass)
        }
        val firstBreak = path.indexOf('/') + 1
        val secondBreak = path.indexOf('/', firstBreak) + 1
        return ProcType.of(path.substring(firstBreak, secondBreak))
    }
    internal fun processAssets()
    {
        /* <ModSpace>/<AssetType>/<Path...>
        *  <AssetType>
        *   |
        *   +-<ModSpace>
        *   |  |
        *   |  +-<Path...>
        *   |  |
        *   |  +-<Path...>
        *   |
        *   +-<ModSpace>
        *   |  |
        *   |  +-<Path...>
        *   |  |
        *   |  +-<Path...>
        */

        val paths = listResourceFiles("assets").groupBy { group(it) }.mapValues {
            it.value.map { at -> split(at, it.key.name.length) }
        }
        .mapValues { at ->
            at.value.groupBy { ModSpace.modules[it.first]!! }.mapValues { it.value.asSequence().map { t -> t.second }.toSet() }
        }.toMap()

        AssetTypeProcessor.order.set(listOf(ModSpace.modules["create"]!!.resourceSpace))

        paths.forEach { type ->
            if(type.key is ProcType.Unknown)
                return@forEach
            val processor = typeProcessors[(type.key as ProcType.Known).type]
            AssetTypeProcessor.nameType.set(processor!!.second)
            processor.first.processResources(mapOf(ModSpace.modules["create"]!!.resourceSpace to type.value))
        }
    }

    @JvmStatic
    inline fun <reified T: Any> registerProcessor(proc: AssetTypeProcessor<T>, name: String) = registerProcessor(proc, T::class, name)
    @JvmStatic
    fun <T: Any> registerProcessor(proc: AssetTypeProcessor<T>, type: KClass<T>, name: String) = synchronized(typeProcessors)
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

    internal fun getStream(source: ResourceSpace, path: String): InputStream? = assetLoaders[source]?.invoke(path)
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