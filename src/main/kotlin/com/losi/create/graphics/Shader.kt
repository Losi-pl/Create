@file:Suppress("unused")
package com.losi.create.graphics

import javax.xml.parsers.DocumentBuilderFactory
import com.losi.create.graphics.gl.*
import com.losi.create.math.*
import org.lwjgl.system.MemoryStack
import java.lang.ref.WeakReference
import com.losi.create.utility.*
import java.lang.ref.Cleaner
import org.w3c.dom.*
import java.io.InputStream
import org.joml.*

/**Shader program used to render objects*/
class Shader: GLBound {
    companion object{
        /**Turns an [InputStream] into an XML [Document]*/
        private fun parseProperties(xml: InputStream) : Document = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(xml)
        /**Used to ensure that if a Shader is Garbage Collected it is also released from OpenGL memory*/
        private val cleaner = Cleaner.create()
        /**Unbinds a current shader from the context*/
        fun release() = glUseProgram()
    }

    /**Creates a new shader from scratch using the [InputStream]'s*/
    constructor(vertex: InputStream, fragment: InputStream) {
        compile(vertex.readAsString(), fragment.readAsString())
    }
    /**Creates a new shader from scratch using the [InputStream]'s
     *
     * Accepts an extra XML with configuration*/
    constructor(vertex: InputStream, fragment: InputStream, xml: InputStream): this(vertex, fragment) {
        var prop = parseProperties(xml)
        prop.getElementsByTagName("fragment").firstOrNull()?.let {
            setFragmentShader(it)
        }
        prop.getElementsByTagName("uniform").firstOrNull()?.let {
            loadUniformSettings(it)
        }
    }

    /**The [Handlers] of this instance*/
    private val handlers: Handlers = Handlers()
    /**The list of active uniforms for this Shader*/
    private lateinit var _uniforms: Map<String, Uniform>
    /**A list of attributes used models implementing this Shader*/
    private lateinit var _attributes: Map<String, Attribute>
    private lateinit var glObjects: Map<Uniform, MutablePair<Int, Texture?>>
    /**A list of dependency's that will be released along with this Shader*/
    private val subscribers = mutableListOf<WeakReference<GLBound>>()
    private var projectionMat: Uniform? = null
    private var viewMat: Uniform? = null
    private var modelMat: Uniform? = null
    /**Sets up the even for when this object is Garbage Collected to ensure that it is also dissolved in the OpenGL memory*/
    private val cleanable: Cleaner.Cleanable = run {
        val hand = handlers
        cleaner.register(this) {
            var act = {
                if (hand.vertex != ShaderPart.NONE) {
                    glDetachShader(hand.program, hand.vertex)
                    glDeleteShader(hand.vertex)
                }
                if (hand.fragment != ShaderPart.NONE) {
                    glDetachShader(hand.program, hand.fragment)
                    glDeleteShader(hand.fragment)
                }
                if (hand.program != ShaderProgram.NONE)
                    glDeleteProgram(hand.program)
                hand.vertex = ShaderPart.NONE
                hand.fragment = ShaderPart.NONE
                hand.program = ShaderProgram.NONE
            }
            if(glTest())
                act()
            else
                OnMainThread.schedule(act)
        }
    }

    /**The OpenGL handler of this Shader Program*/
    internal val handler: ShaderProgram get() = handlers.program
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
        handlers.vertex = glCreateShader(ShaderType.Vertex)
        glShaderSource(handlers.vertex, vertex)
        glCompileShader(handlers.vertex)

        handlers.fragment = glCreateShader(ShaderType.Fragment)
        glShaderSource(handlers.fragment, fragment)
        glCompileShader(handlers.fragment)

        run {
            val errors = mutableListOf<Pair<ShaderType, String>>()
            if (!glGetShaderCompiledStatus(handlers.vertex))   errors.add(Pair(ShaderType.Vertex,   glGetShaderLog(handlers.vertex)))
            if (!glGetShaderCompiledStatus(handlers.fragment)) errors.add(Pair(ShaderType.Fragment, glGetShaderLog(handlers.fragment)))
            if(errors.isNotEmpty())
                throw ShaderCompilationError(errors)
        }

        handlers.program = glCreateProgram()
        glAttachShader(handlers.program, handlers.vertex)
        glAttachShader(handlers.program, handlers.fragment)

        if (!glLinkProgram(handlers.program))
            throw ShaderCompilationError(glGetProgramLog(handlers.program))

        glUseProgram(handlers.program)

        loadUniforms()
        loadAttributes()
    }
    /**Loads data about uniforms*/
    private fun loadUniforms() {
        val uniforms = mutableListOf<Uniform>()
        val glObj = mutableMapOf<Uniform, MutablePair<Int, Texture2D?>>()
        MemoryStack.stackPush().use { stack ->
            for(i in 0u until glGetProgramUniformCount(handlers.program))
            {
                val data = glGetUniform(handlers.program, i)
                val location = glGetUniformLocation(handlers.program, data.name)

                val uniform = Uniform(data.name, location, data.count, data.type)

                uniforms.add(uniform)
                if(uniform.type.isObject)
                {
                    glUniform1(uniform.location, glObj.size)
                    glObj[uniform] = MutablePair(glObj.size, null)
                }
            }
        }
        this._uniforms = uniforms.associateBy { it.name }.calcify()
        glObjects = glObj.toMap()
    }
    /**Reads XML data from the configuration and applies it to the shader
     *
     *  Configuration specific to Fragment Shader*/
    private fun setFragmentShader(info: Node) {
        if(info !is Element)
        return
        info.getElementsByTagName("output").forEach {
            val name : String = it.getAttribute("name").orElse {
                (it as Element).getElementsByTagName("name").firstOrNull()?.textContent?: throw NullPointerException("No name specified for fragment output")
            }
            val location = it.getAttribute("location").orElse {
                (it as Element).getElementsByTagName("location").firstOrNull()?.textContent?: throw NullPointerException("No location specified for fragment output $name") }.toUInt()
            glBindFragDataLocation(handlers.program, location, name)
        }
    }
    private fun loadUniformSettings(info: Node) {
        fun Uniform?.verify(name: String): Uniform {
            if(this == null)
                throw NullPointerException("Uniform name $name not found")
            if (this.type == GLSLVar.Matrix4f || this.type == GLSLVar.Matrix4d)
                return this
            throw IllegalArgumentException("Uniform $name is not a Matrix4x4")
        }

        if(info !is Element)
            return
        info.getElementsByTagName("projection").lastOrNull()?.let {
            val name : String = it.getAttribute("name").orElse {
                (it as Element).getElementsByTagName("name").firstOrNull()?.textContent?: throw NullPointerException("Incorrect format of specification of projection matrix") }
            projectionMat = uniforms[name].verify(name)
        }
        info.getElementsByTagName("view").lastOrNull()?.let {
            val name : String = it.getAttribute("name").orElse {
                (it as Element).getElementsByTagName("name").firstOrNull()?.textContent?: throw NullPointerException("Incorrect format of specification of view matrix") }
            viewMat = uniforms[name].verify(name)
        }
        info.getElementsByTagName("model").lastOrNull()?.let {
            val name : String = it.getAttribute("name").orElse {
                (it as Element).getElementsByTagName("name").firstOrNull()?.textContent?: throw NullPointerException("Incorrect format of specification of model matrix") }
            modelMat = uniforms[name].verify(name)
        }
    }
    /**Loads data related to the attributes from a compiles shader*/
    private fun loadAttributes(){
        val attributes = mutableListOf<Attribute>()
        MemoryStack.stackPush().use { stack ->
            var offset = 0u
            for(i in 0u until glGetProgramAttributeCount(handlers.program))
            {
                val data = glGetAttribute(handlers.program, i)
                val location = glGetAttributeLocation(handlers.program, data.name)
                attributes.add(Attribute(data.name, location, data.count, data.type, offset.toInt()))
                offset += data.type.byteCount
            }
        }
        this._attributes = attributes.associateBy { it.name }.calcify()
    }

    //region Uniforms
    /**The format fot setting an attribute
     * @param name The name of the attribute to set
     * @param type OpenGL type expected by the method to find
     * @param setter Invoked to set the uniform when the internal logic of this method set up everything else*/
    private inline fun setUniform(name: String, type: GLSLVar, setter: (Uniform) -> Unit) {
        breakTest()
        val uninfo = _uniforms[name].orElse { throw Exception("Unknown uniform \"$name\"") }
        if(uninfo.type != type)
            throw Exception("Uniform \"$name\" has expects a type \"${uninfo.type.klass?.simpleName.orElse{uninfo.type.glName}}\"")
        if(uninfo.count > 1u)
            throw Exception("Uniform \"$name\" requires ${uninfo.count} values to be passed to it while only 1 was provided")
        use()
        setter(uninfo)
        Shader.release()
    }

    //region Primitive Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Boolean) = setUniform(name, GLSLVar.Boolean) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Byte) = setUniform(name, GLSLVar.Byte) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: UByte) = setUniform(name, GLSLVar.UByte) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Short) = setUniform(name, GLSLVar.Short) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: UShort) = setUniform(name, GLSLVar.UShort) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Int) = setUniform(name, GLSLVar.Int) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: UInt) = setUniform(name, GLSLVar.UInt) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Long) = setUniform(name, GLSLVar.Long) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: ULong) = setUniform(name, GLSLVar.ULong) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Float) = setUniform(name, GLSLVar.Float) { glUniform1(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Double) = setUniform(name, GLSLVar.Double) { glUniform1(it.location, value) }
    //endregion
    //region Boolean Vector Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector2b) = setUniform(name, GLSLVar.Vector2b) { glUniform2(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector3b) = setUniform(name, GLSLVar.Vector3b) { glUniform3(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector4b) = setUniform(name, GLSLVar.Vector4b) { glUniform4(it.location, value) }
    //endregion
    //region Int Vector Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector2i) = setUniform(name, GLSLVar.Vector2i) { glUniform2(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector3i) = setUniform(name, GLSLVar.Vector3i) { glUniform3(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector4i) = setUniform(name, GLSLVar.Vector4i) {
        glUniform4(it.location, value.x, value.y, value.z, value.w) }
    //endregion
    //region Long Vector Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector2L) = setUniform(name, GLSLVar.Vector2l) { glUniform2(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector3L) = setUniform(name, GLSLVar.Vector3l) { glUniform3(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector4L) = setUniform(name, GLSLVar.Vector4l) { glUniform4(it.location, value) }
    //endregion
    //region Float Vector Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector2f) = setUniform(name, GLSLVar.Vector2f) { glUniform2(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector3f) = setUniform(name, GLSLVar.Vector3f) { glUniform3(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector4f) = setUniform(name, GLSLVar.Vector4f) { glUniform4(it.location, value) }
    //endregion
    //region Double Vector Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector2d) = setUniform(name, GLSLVar.Vector2d) { glUniform2(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector3d) = setUniform(name, GLSLVar.Vector3d) { glUniform3(it.location, value) }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Vector4d) = setUniform(name, GLSLVar.Vector4d) { glUniform4(it.location, value) }
    //endregion
    //region Float Matrix Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix4f) = setUniform(name, GLSLVar.Matrix4f) { glUniformMatrix(it.location, false, value) }
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix4x3f) = setUniform(name, GLSLVar.Matrix4x3f) { glUniformMatrix(it.location, false, value) }
    //TODO: GL_FLOAT_MAT4x2
    //TODO: GL_FLOAT_MAT3x4
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix3f) = setUniform(name, GLSLVar.Matrix3f) { glUniformMatrix(it.location, false, value) }
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix3x2f) = setUniform(name, GLSLVar.Matrix3x2f) { glUniformMatrix(it.location, false, value) }
    //TODO: GL_FLOAT_MAT2x4
    //TODO: GL_FLOAT_MAT2x3
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix2f) = setUniform(name, GLSLVar.Matrix2f) { glUniformMatrix(it.location, false, value) }
    //endregion
    //region Double Matrix Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix4d) = setUniform(name, GLSLVar.Matrix4d) { glUniformMatrix(it.location, false, value) }
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix4x3d) = setUniform(name, GLSLVar.Matrix4x3d) { glUniformMatrix(it.location, false, value) }
    //TODO: GL_FLOAT_MAT4x2
    //TODO: GL_FLOAT_MAT3x4
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix3d) = setUniform(name, GLSLVar.Matrix3d) { glUniformMatrix(it.location, false, value) }
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix3x2d) = setUniform(name, GLSLVar.Matrix3x2d) { glUniformMatrix(it.location, false, value) }
    //TODO: GL_FLOAT_MAT2x4
    //TODO: GL_FLOAT_MAT2x3
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Matrix2d) = setUniform(name, GLSLVar.Matrix2d) { glUniformMatrix(it.location, false, value) }
    //endregion

    //region Texture Uniform's
    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Texture2D?) = setUniform(name, GLSLVar.Sampler2D) { glObjects[it]!!.second = value }

    /**Used to set the [name] uniform with a [value]
     * @throws Exception If the attribute could not be found or if it's type is not matched with the [value]*/
    fun setUniform(name: String, value: Texture2DAtlas?) = setUniform(name, GLSLVar.Sampler2DArray) { glObjects[it]!!.second = value }
    //endregion

    val hasModelMatrix get() = modelMat != null
    val hasViewMatrix get() = viewMat != null
    val hasProjectionMatrix get() = projectionMat != null

    private fun setMatrix(uniform: Uniform, matrix: Matrix4f) {
        use()
        when(uniform.type) {
            GLSLVar.Matrix4f -> glUniformMatrix(uniform.location, false, matrix)
            GLSLVar.Matrix4d -> MemoryStack.stackPush().use { stack ->
                val bufferIn = stack.mallocFloat(4 * 4)
                val bufferOut = stack.mallocDouble(4 * 4)
                bufferIn.putMatrix4f(matrix).flip()
                for(i in 0.. 4*4)
                    bufferOut.put(bufferIn.get().toDouble())
                bufferOut.flip()
                org.lwjgl.opengl.ARBGPUShaderFP64.glUniformMatrix2dv(uniform.location.handle, false, bufferOut)
            }
            else -> throw Error("I do not know how you got here")
        }
        Shader.release()
    }
    fun setModelMatrix(matrix: Matrix4f) {
        requireNotNull(modelMat) { "This shader has no Model Matrix specified" }
        setMatrix(modelMat!!, matrix)
    }
    fun setViewMatrix(matrix: Matrix4f) {
        requireNotNull(viewMat) { "This shader has no View Matrix specified" }
        setMatrix(viewMat!!, matrix)
    }
    fun setProjectionMatrix(matrix: Matrix4f) {
        requireNotNull(projectionMat) { "This shader has no Projection Matrix specified" }
        setMatrix(projectionMat!!, matrix)
    }

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
    val released: Boolean get() = handlers.program == ShaderProgram.NONE
    /**A check if this Shader can be used or is it dissolved
     * @throws NullPointerException Thrown if the Shader has been dissolved*/
    private fun breakTest() {
        if(released)
            throw NullPointerException("The shader has been destroyed")
    }

    /**Used to bind proper objects to specific Uniforms*/
    fun assignObjects() {
        glObjects.forEach { (uniform, pair) ->
            if(pair.first !in 0..31)
                return@forEach

            glActiveTexture(pair.first)
            pair.second?.handle?.let { glBindTexture(it) }
                .orElse { glUnbindTexture(TextureType.usedFor(uniform.type)!!) }
        }
        glActiveTexture(0)
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
    private data class Handlers(
        var program: ShaderProgram = ShaderProgram.NONE,
        var vertex: ShaderPart = ShaderPart.NONE,
        var fragment: ShaderPart = ShaderPart.NONE)

    /**The information about the Uniform of a Shader Program
     * @property name A human friendly name
     * @property location The handler of the uniform in th shader
     * @property count Information about the values of the [type] in ths specific uniform, if bigger than `1` then this uniform is an array
     * @property type The type of data of this uniform*/
    data class Uniform(val name: String, val location: UniformLocation, val count: UInt, val type: GLSLVar)
    //endregion

    /**The information about the Attribute of a Shader Program
     * @property name A human friendly name
     * @property location The handler of the attribute in th shader
     * @property count Information about the values of the [type] in ths specific attribute, if bigger than `1` then this attribute is an array
     * @property type The type of data of this attribute*/
    data class Attribute(val name: String, val location: AttributeLocation, val count: UInt, val type: GLSLVar, val offset: Int)
}