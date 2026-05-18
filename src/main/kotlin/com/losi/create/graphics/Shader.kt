package com.losi.create.graphics

import com.losi.create.utility.*
import com.losi.create.utility.CShaderUniforms.*
import org.joml.*
import org.lwjgl.opengl.GL30.*
import org.lwjgl.opengl.GL40
import org.lwjgl.system.MemoryStack
import org.w3c.dom.Document
import org.w3c.dom.Element
import org.w3c.dom.Node
import java.io.InputStream
import javax.xml.parsers.DocumentBuilderFactory
import kotlin.reflect.KClass

@Suppress("unused")
class Shader {
    private var shaderProgram: Int = 0
    private var vertexShader: Int = 0
    private var fragmentShader: Int = 0
    private var uniforms: Map<String, Uniform> = emptyMap()
    private var attributes: Map<String, Attribute> = emptyMap()

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
        this.uniforms = uniforms.associateBy { it.name }
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
                val location = glGetAttribLocation(shaderProgram, name);
                attributes.add(Attribute(name, location, count.get().toUInt(), type.get()));
            }
        }
        this.attributes = attributes.associateBy { it.name }
    }

    //region Uniforms
    private inline fun setUniform(name: String, type: Int, setter: (Uniform) -> Unit) {
        val uninfo = uniforms[name].orElse { throw Exception("Unknown uniform \"$name\"") }
        if(uninfo.type != type)
            throw Exception("Uniform \"$name\" has expects a type \"${uninfo.classType?.simpleName.orElse{"GLSL: 0x%x".format(uninfo.type)}}\"")
        use()
        setter(uninfo)
    }
    fun setUniform(name: String, matrix: Matrix4f) =
        setUniform(name, GL_FLOAT_MAT4) { glUniformMatrix4f(it.location, false, matrix) }
    fun setUniform(name: String, value: Float) =
        setUniform(name, GL_FLOAT) { glUniform1f(it.location, value) }

    data class Uniform(val name: String, val location: Int, val count: UInt, val type: Int)
    {
        val classType: KClass<*>?
            get() {
                return when(type) {
                    //Basic
                    GL_BOOL -> Boolean::class
                    GL_INT -> Int::class
                    GL_UNSIGNED_INT -> UInt::class
                    GL_FLOAT -> Float::class
                    GL_DOUBLE -> Double::class

                    //TODO: Bool Vectors

                    //Int Vector
                    GL_INT_VEC2 -> Vector2i::class
                    GL_INT_VEC3 -> Vector3i::class
                    GL_INT_VEC4 -> Vector4i::class

                    //TODO: UInt Vectors

                    //Float Vectors
                    GL_FLOAT_VEC2 -> Vector2f::class
                    GL_FLOAT_VEC3 -> Vector3f::class
                    GL_FLOAT_VEC4 -> Vector4f::class

                    //Double Vectors
                    GL40.GL_DOUBLE_VEC2 -> Vector2d::class
                    GL40.GL_DOUBLE_VEC3 -> Vector3d::class
                    GL40.GL_DOUBLE_VEC4 -> Vector4d::class

                    //Float Matrix
                    GL_FLOAT_MAT4 ->   Matrix4f::class
                    GL_FLOAT_MAT4x3 -> Matrix4x3f::class
            //TODO: GL_FLOAT_MAT4x2 -> Matrix4x2f
            //TODO: GL_FLOAT_MAT3x4 -> Matrix3x4f
                    GL_FLOAT_MAT3 ->   Matrix3f::class
                    GL_FLOAT_MAT3x2 -> Matrix3f::class
            //TODO: GL_FLOAT_MAT2x4 -> Matrix2x4f::class
            //TODO: GL_FLOAT_MAT2x3 -> Matrix2x3f::class
                    GL_FLOAT_MAT2 ->   Matrix4f::class

                    //Double Matrix
                    GL40.GL_DOUBLE_MAT4 ->   Matrix4d::class
                    GL40.GL_DOUBLE_MAT4x3 -> Matrix4x3d::class
            //TODO: GL40.GL_DOUBLE_MAT4x2 -> Matrix4x2d
            //TODO: GL40.GL_DOUBLE_MAT3x4 -> Matrix3x4d
                    GL40.GL_DOUBLE_MAT3 ->   Matrix4x3d::class
                    GL40.GL_DOUBLE_MAT3x2 -> Matrix3d::class
            //TODO: GL40.GL_DOUBLE_MAT2x4 -> Matrix2x4d::class
            //TODO: GL40.GL_DOUBLE_MAT2x3 -> Matrix2x3d::class
                    GL40.GL_DOUBLE_MAT2 ->   Matrix4f::class

                    else -> null
                }
            }

    }
    //endregion

    data class Attribute(val name: String, val location: Int, val count: UInt, val type: Int)
}