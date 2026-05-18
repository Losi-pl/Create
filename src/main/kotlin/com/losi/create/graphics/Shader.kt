package com.losi.create.graphics

import com.losi.create.utility.CShaderUniforms.*
import javax.xml.parsers.DocumentBuilderFactory
import org.lwjgl.system.MemoryStack
import com.losi.create.utility.*
import org.lwjgl.opengl.GL30.*
import kotlin.reflect.KClass
import org.w3c.dom.Document
import org.w3c.dom.Element
import java.io.InputStream
import org.w3c.dom.Node
import org.joml.*

@Suppress("unused")
class Shader {
    private var shaderProgram: Int = 0
    private var vertexShader: Int = 0
    private var fragmentShader: Int = 0
    private var _uniforms: Map<String, Uniform> = emptyMap()
    private var _attributes: Map<String, Attribute> = emptyMap()

    companion object{
        private fun parseProperties(xml: InputStream) : Document = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(xml)
    }

    constructor(vertex: InputStream, fragment: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
    }
    constructor(vertex: InputStream, fragment: InputStream, xml: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
        var prop = parseProperties(xml)
        setFragmentShader(prop.getElementsByTagName("fragment").item(0))
    }

    val handler: Int get() = shaderProgram
    val uniforms: Map<String, Uniform> get() = _uniforms
    val attributes: Map<String, Attribute> get() = _attributes

    fun use() = glUseProgram(shaderProgram)
    fun release() = glUseProgram(0)

    private fun compile(vertex: String, fragment: String) {
        vertexShader = glCreateShader(GL_VERTEX_SHADER)
        glShaderSource(vertexShader, vertex)
        glCompileShader(vertexShader)

        fragmentShader = glCreateShader(GL_FRAGMENT_SHADER)
        glShaderSource(fragmentShader, fragment)
        glCompileShader(fragmentShader)

        run {
            val errors = mutableListOf<Pair<Int, String>>()
            if (glGetShaderi(vertexShader, GL_COMPILE_STATUS) != GL_TRUE) errors.add(Pair(GL_VERTEX_SHADER, glGetShaderInfoLog(vertexShader)))
            if (glGetShaderi(fragmentShader,GL_COMPILE_STATUS) != GL_TRUE) errors.add(Pair(GL_FRAGMENT_SHADER, glGetShaderInfoLog(fragmentShader)))
            if(errors.isNotEmpty())
                throw ShaderCompilationError(errors)
        }

        shaderProgram = glCreateProgram()
        glAttachShader(shaderProgram, vertexShader)
        glAttachShader(shaderProgram, fragmentShader)
        glLinkProgram(shaderProgram)

        if (glGetProgrami(shaderProgram, GL_LINK_STATUS) != GL_TRUE)
            throw ShaderCompilationError(glGetProgramInfoLog(shaderProgram))

        glUseProgram(shaderProgram)

        loadUniforms()
        loadAttributes()
    }
    private fun loadUniforms() {
        val uniforms = mutableListOf<Uniform>()
        MemoryStack.stackPush().use { stack ->
            for(i in 0 until glGetProgrami(shaderProgram, GL_ACTIVE_UNIFORMS))
            {
                val count = stack.mallocInt(1)
                val type = stack.mallocInt(1)

                val name = glGetActiveUniform(shaderProgram, i, count, type)
                val location = glGetUniformLocation(shaderProgram, name)
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
                glBindFragDataLocation(shaderProgram, location, name)
            }
        }

    }
    private fun loadAttributes(){
        val attributes = mutableListOf<Attribute>()
        MemoryStack.stackPush().use { stack ->
            for(i in 0 until glGetProgrami(shaderProgram, GL_ACTIVE_ATTRIBUTES))
            {
                val count = stack.mallocInt(1)
                val type = stack.mallocInt(1)
                val name = glGetActiveAttrib(shaderProgram, i, count, type)
                val location = glGetAttribLocation(shaderProgram, name)
                attributes.add(Attribute(name, location, count.get().toUInt(), type.get()))
            }
        }
        this._attributes = attributes.associateBy { it.name }.calcify()
    }

    //region Uniforms
    private inline fun setUniform(name: String, type: Int, setter: (Uniform) -> Unit) {
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

    data class Uniform(val name: String, val location: Int, val count: UInt, val type: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
    //endregion

    data class Attribute(val name: String, val location: Int, val count: UInt, val type: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
}