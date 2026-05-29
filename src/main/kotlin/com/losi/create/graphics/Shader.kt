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
import java.lang.ref.WeakReference

/**Shader program used to render objects*/
class Shader: GLBound {
    companion object{
        /**Turns an [InputStream] into an XML [Document]*/
        private fun parseProperties(xml: InputStream) : Document = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(xml)
        /**Used to ensure that if a Shader is Garbage Collected it is also released from OpenGL memory*/
        private val cleaner = Cleaner.create()
        /**Unbinds a current shader from the context*/
        fun release() = glUseProgram(0)
    }

    /**Creates a new shader from scratch using the [InputStream]'s*/
    constructor(vertex: InputStream, fragment: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
    }
    /**Creates a new shader from scratch using the [InputStream]'s
     *
     * Accepts an extra XML with configuration*/
    constructor(vertex: InputStream, fragment: InputStream, xml: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
        var prop = parseProperties(xml)
        setFragmentShader(prop.getElementsByTagName("fragment").item(0))
    }

    /**The [Handlers] of this instance*/
    private val handlers: Handlers = Handlers()
    /**The list of active uniforms for this Shader*/
    private lateinit var _uniforms: Map<String, Uniform>
    /**A list of attributes used models implementing this Shader*/
    private lateinit var _attributes: Map<String, Attribute>
    /**A list of dependency's that will be released along with this Shader*/
    private val subscribers = mutableListOf<WeakReference<GLBound>>()
    /**Sets up the even for when this object is Garbage Collected to ensure that it is also dissolved in the OpenGL memory*/
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

    /**The OpenGL handler of this Shader Program*/
    val handler: Int get() = handlers.program
    /**The list of active uniforms for this Shader*/
    val uniforms: Map<String, Uniform> get() = _uniforms
    /**A list of attributes used models implementing this Shader*/
    val attributes: Map<String, Attribute> get() = _attributes

    /**Specifies that this is the Shader current used in this Thread
     *
     * Meant for OpenGL*/
    fun use() { breakTest(); glUseProgram(handlers.program) }

    /**Compiles the Shader from [InputStream]'s*/
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
    /**Loads data about uniforms*/
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
    /**Reads XML data from the configuration and applies it to the shader
     *
     *  Configuration specific to Fragment Shader*/
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
    /**Loads data related to the attributes from a compiles shader*/
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
    /**The format fot setting an attribute
     * @param name The name of the attribute to set
     * @param type OpenGL type expected by the method to find
     * @param setter Invoked to set the uniform when the internal logic of this method set up everything else*/
    private inline fun setUniform(name: String, type: Int, setter: (Uniform) -> Unit) {
        breakTest()
        val uninfo = _uniforms[name].orElse { throw Exception("Unknown uniform \"$name\"") }
        if(uninfo.type != type)
            throw Exception("Uniform \"$name\" has expects a type \"${uninfo.classType?.simpleName.orElse{"GLSL: 0x%x".format(uninfo.type)}}\"")
        use()
        setter(uninfo)
    }
    /**Used to set the [name] uniform with a [matrix]
     * @throws Exception If the attribute could not be found or if its type is not matched to the value*/
    fun setUniform(name: String, matrix: Matrix4f) =
        setUniform(name, GL_FLOAT_MAT4) { glUniformMatrix4f(it.location, false, matrix) }
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if its type is not matched to the [value]*/
    fun setUniform(name: String, value: Float) =
        setUniform(name, GL_FLOAT) { glUniform1f(it.location, value) }
    //TODO: setUniform(/* ALL */)

    /**Used to dissolve the Shader allowing the OpenGl data to be freed
     *
     * After calling the Shader will become unusable, and all dependency's will be released as well*/
    override fun release() {
        var exc: MutableList<Exception>? = null
        synchronized(subscribers) {
            subscribers.forEach {
                try { it.get()?.release() }
                catch (ex: Exception) {
                    if(exc == null)
                        exc = ArrayList()
                    exc.add(ex)
                }
            }
        }
        cleanable.clean()
        if(exc != null)
        {
            val run = RuntimeException("During shader release, the following Exceptions gave been caught.")
            exc.forEach { run.addSuppressed(it) }
            throw run
        }
    }
    /**Is this Shader dissolved flag*/
    val released: Boolean get() = handlers.program == 0
    /**A check if this Shader can be used or is it dissolved
     * @throws NullPointerException Thrown if the Shader has been dissolved*/
    fun breakTest() {
        if(released)
            throw NullPointerException("The shader has been destroyed")
    }

    /**
     * All subscribers will be called to release when this object is called to.
     * If this object is collected by the garbage collector, the call will not happen.
     *
     * @param dependant Contains the reference to the subscriber in format of [GLBound] interface, when this Shader is released, all the subscribers wil release as well
     */
    fun dependencySubscription(dependant: GLBound) = synchronized(subscribers) {
        val ref = WeakReference(dependant)
        if(subscribers.contains(ref))
            return@synchronized
        else
            subscribers.add(ref)
    }

    /**The handlers to all relevant OpenGL objects
     * @property program The Shader Program
     * @property vertex The Vertex Shader
     * @property fragment Thr Fragment Shader*/
    private data class Handlers(var program: Int = 0, var vertex: Int = 0, var fragment: Int = 0)

    /**The information about the Uniform of a Shader Program
     * @property name A human friendly name
     * @property location The handler of the uniform in th shader
     * @property count Information about the values of the [type] in ths specific uniform, if bigger than `1` then this uniform is an array
     * @property type The type of data of this uniform
     * @property classType The [type] of this uniform but in Kotlin format*/
    data class Uniform(val name: String, val location: Int, val count: UInt, val type: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
    //endregion

    /**The information about the Attribute of a Shader Program
     * @property name A human friendly name
     * @property location The handler of the attribute in th shader
     * @property count Information about the values of the [type] in ths specific attribute, if bigger than `1` then this attribute is an array
     * @property type The type of data of this attribute
     * @property classType The [type] of this attribute but in Kotlin format*/
    data class Attribute(val name: String, val location: Int, val count: UInt, val type: Int, val offset: Int) {
        val classType: KClass<*>? get() = translateGLTypes(type)
    }
}