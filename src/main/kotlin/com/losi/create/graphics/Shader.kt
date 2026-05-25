@file:Suppress("unused")
package com.losi.create.graphics

import com.losi.create.utility.CShaderUniforms.*
import javax.xml.parsers.DocumentBuilderFactory
import org.lwjgl.system.MemoryStack
import com.losi.create.utility.*
import org.lwjgl.opengl.GL30.*
import kotlin.reflect.KClass
import java.lang.ref.Cleaner
import org.w3c.dom.Document
import org.w3c.dom.Element
import java.io.InputStream
import org.w3c.dom.Node
import org.joml.*

class Shader {
    companion object{
        private fun parseProperties(xml: InputStream) : Document = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(xml)
        private val cleaner = Cleaner.create()
    }

    constructor(vertex: InputStream, fragment: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
    }
    constructor(vertex: InputStream, fragment: InputStream, xml: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
        var prop = parseProperties(xml)
        setFragmentShader(prop.getElementsByTagName("fragment").item(0))
    }

    private val handlers: Handlers = Handlers()
    private lateinit var _uniforms: Map<String, Uniform>
    private lateinit var _attributes: Map<String, Attribute>
    private val cleanable: Cleaner.Cleanable = run {
        val hand = handlers
        cleaner.register(this) {
            var onContext = try { glGetError(); true } catch (ignored: NullPointerException) { false }

            var act = {
                if (hand.vertex != 0) {
                    glDetachShader(hand.program, hand.vertex)
                    glDeleteShader(hand.vertex)
                }
                if (hand.fragment != 0) {
                    glDetachShader(hand.program, hand.fragment)
                    glDeleteShader(hand.fragment)
                }
                if (hand.program != 0)
                    glDeleteProgram(hand.program)
                hand.vertex = 0
                hand.fragment = 0
                hand.program = 0
            }
            if(onContext)
                act()
            else
                OnMainThread.schedule(act)
        }
    }

    val handler: Int get() = handlers.program
    val uniforms: Map<String, Uniform> get() = _uniforms
    val attributes: Map<String, Attribute> get() = _attributes

    fun use() { breakTest(); glUseProgram(handlers.program) }
    fun release() = glUseProgram(0)

    private fun compile(vertex: String, fragment: String) {
        handlers.vertex = glCreateShader(GL_VERTEX_SHADER)
        glShaderSource(handlers.vertex, vertex)
        glCompileShader(handlers.vertex)

        handlers.fragment = glCreateShader(GL_FRAGMENT_SHADER)
        glShaderSource(handlers.fragment, fragment)
        glCompileShader(handlers.fragment)

        run {
            val errors = mutableListOf<Pair<Int, String>>()
            if (glGetShaderi(handlers.vertex, GL_COMPILE_STATUS) != GL_TRUE) errors.add(Pair(GL_VERTEX_SHADER, glGetShaderInfoLog(handlers.vertex)))
            if (glGetShaderi(handlers.fragment,GL_COMPILE_STATUS) != GL_TRUE) errors.add(Pair(GL_FRAGMENT_SHADER, glGetShaderInfoLog(handlers.fragment)))
            if(errors.isNotEmpty())
                throw ShaderCompilationError(errors)
        }

        handlers.program = glCreateProgram()
        glAttachShader(handlers.program, handlers.vertex)
        glAttachShader(handlers.program, handlers.fragment)
        glLinkProgram(handlers.program)

        if (glGetProgrami(handlers.program, GL_LINK_STATUS) != GL_TRUE)
            throw ShaderCompilationError(glGetProgramInfoLog(handlers.program))

        glUseProgram(handlers.program)

        loadUniforms()
        loadAttributes()
    }
    private fun loadUniforms() {
        val uniforms = mutableListOf<Uniform>()
        MemoryStack.stackPush().use { stack ->
            for(i in 0 until glGetProgrami(handlers.program, GL_ACTIVE_UNIFORMS))
            {
                val count = stack.mallocInt(1)
                val type = stack.mallocInt(1)

                val name = glGetActiveUniform(handlers.program, i, count, type)
                val location = glGetUniformLocation(handlers.program, name)
                uniforms.add(Uniform(name, location, count.get(0).toUInt(), type.get(0)))
            }
        }
        this._uniforms = uniforms.associateBy { it.name }.calcify()
    }
    private fun setFragmentShader(info: Node) {
        if(info is Element)
        {
            info.getElementsByTagName("output").forEach {
                val name : String = it.getAttribute("name").orElse {
                    (it as Element).getElementsByTagName("name").first().textContent }
                val location = it.getAttribute("location").orElse {
                    (it as Element).getElementsByTagName("location").first().textContent }.toInt()
                glBindFragDataLocation(handlers.program, location, name)
            }
        }

    }
    private fun loadAttributes(){
        val attributes = mutableListOf<Attribute>()
        MemoryStack.stackPush().use { stack ->
            var offset = 0
            for(i in 0 until glGetProgrami(handlers.program, GL_ACTIVE_ATTRIBUTES))
            {
                val count = stack.mallocInt(1)
                val type = stack.mallocInt(1)
                val name = glGetActiveAttrib(handlers.program, i, count, type)
                val location = glGetAttribLocation(handlers.program, name)
                attributes.add(Attribute(name, location, count.get().toUInt(), type.get(0), offset))
                offset += baseGLTypeBytes(type.get(0))
            }
        }
        this._attributes = attributes.associateBy { it.name }.calcify()
    }

    //region Uniforms
    private inline fun setUniform(name: String, type: Int, setter: (Uniform) -> Unit) {
        breakTest()
        val uninfo = _uniforms[name].orElse { throw Exception("Unknown uniform \"$name\"") }
        if(uninfo.type != type)
            throw Exception("Uniform \"$name\" has expects a type \"${uninfo.classType?.simpleName.orElse{"GLSL: 0x%x".format(uninfo.type)}}\"")
        use()
        setter(uninfo)
    }
    fun setUniform(name: String, matrix: Matrix4f) =
        setUniform(name, GL_FLOAT_MAT4) { glUniformMatrix4f(it.location, false, matrix) }
    fun setUniform(name: String, value: Float) =
        setUniform(name, GL_FLOAT) { glUniform1f(it.location, value) }
    //TODO: setUniform(/* ALL */)

    fun destroy() = cleanable.clean()
    val destroyed: Boolean get() = handlers.program == 0
    fun breakTest() {
        if(destroyed)
            throw NullPointerException("The shader has been destroyed")
    }

    private data class Handlers(var program: Int = 0, var vertex: Int = 0, var fragment: Int = 0)

    data class Uniform(val name: String, val location: Int, val count: UInt, val type: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
    //endregion

    data class Attribute(val name: String, val location: Int, val count: UInt, val type: Int, val offset: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
}