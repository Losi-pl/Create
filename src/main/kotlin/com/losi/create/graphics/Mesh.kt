package com.losi.create.graphics

import com.losi.create.math.*
import com.losi.create.utility.*
import org.joml.*
import org.lwjgl.opengl.GL40.*
import org.lwjgl.system.MemoryStack
import java.lang.ref.Cleaner

@Suppress("unused")
class Mesh: GLBound {
    companion object {
        val cleaner = Cleaner.create()!!
        val identity = Matrix4f()

        private inline fun <reified T> List<*>.assert(): List<T> {
            require(first() is T) { "${first()!!::class} is not an instance of ${T::class}" }
            @Suppress("UNCHECKED_CAST")
            return this as List<T>
        }
        private inline fun <reified T> Array<*>.assert(): Array<T> {
            require(first() is T) { "${first()!!::class} is not an instance of ${T::class}" }
            @Suppress("UNCHECKED_CAST")
            return this as Array<T>
        }

        private inline fun <reified T, reified NotK> Any?.aSequence(setArray: (NotK) -> Sequence<T>): Sequence<T> = this.let { when (it) {
            is NotK -> setArray(it)
            is List<*> ->  it.assert<T>().asSequence()
            is Array<*> -> it.assert<T>().asSequence()
            else -> sequenceOf()
        }}
        private inline fun <reified T> Any?.aSequence(): Sequence<T> = this.let { when (it) {
            is List<*> ->  it.assert<T>().asSequence()
            is Array<*> -> it.assert<T>().asSequence()
            else -> sequenceOf()
        }}

        private inline fun <T> Sequence<T>.putIntoBuffer(data: Triple<Shader.Attribute, Int, Int>, action: (Int, T) -> Unit) {
            val (attr, verSize, attrSize) = data

            if(attr.count == 1u)
                this.forEachIndexed { index, it -> action(attr.offset + (index * verSize), it) }
            else
                this.chunkedReuse(attr.count.toInt()).withIndex().forEach { ti->
                    ti.value.forEachIndexed { index, it -> action(attr.offset + (attrSize + index) + (ti.index * verSize), it) }
                }
        }

        private fun garbageCollect(data: GLBinds)
        {
            glBindVertexArray(0)
            glBindBuffer(GL_ARRAY_BUFFER, 0)
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0)

            glDeleteBuffers(data.vbo)
            glDeleteVertexArrays(data.vao)
        }
    }

    private var glBinds: GLBinds? = null
    private val shader: Shader
    private val variables: MutableMap<Shader.Attribute, Any?>
    private var cleaner: Cleaner.Cleanable? = null

    constructor(shader: Shader) {
        shader.dependencySubscription(this)
        this.shader = shader
        this.variables =  shader.attributes.values
            .associateWithTo(mutableMapOf()) { null }
    }

    fun flushBuffers() = synchronized(variables) { variables.keys.forEach { variables[it] = null }}
    fun draw(): Unit = synchronized(variables) {
        if(shader.released)
            throw NullPointerException("The Shader used by this Mesh was destroyed")
        val bin = glBinds ?: throw NullPointerException("This Mesh does not have a burned model to draw")
        glBindVertexArray(bin.vao)
        shader.use()
        glDrawArrays(GL_TRIANGLES, 0, bin.vertexCount)
    }

    private fun findAttr(name: String) = shader.attributes[name].orElse { throw IllegalArgumentException("Attribute \"$name\" not found") }
    private fun <T> setAttribute(name: String, type: Int, values: T) {
        synchronized(variables)
        {
            if(shader.released)
                throw NullPointerException("The Shader used by this Mesh was destroyed")

            val attr = findAttr(name)
            if(attr.type != type)
                throw IllegalArgumentException("Attribute \"${attr.name}\" is not of type ${translateGLTypes(type)} and requires ${attr.classType}")
            else
                variables[attr] = values
        }
    }

    override fun release() = synchronized(variables) {
        cleaner?.clean()
        cleaner = null
        glBinds = null
    }

    //region Primitives
    @JvmName("setAttributeIntList")
    fun setAttribute(name: String, value: List<Int>) = setAttribute(name, GL_INT, value)
    fun setAttribute(name: String, value: Array<Int>) = setAttribute(name, GL_INT, value)
    fun setAttribute(name: String, value: IntArray) = setAttribute(name, GL_INT, value)

    @OptIn(ExperimentalUnsignedTypes::class)
    fun setAttribute(name: String, value: UIntArray) = setAttribute(name, GL_UNSIGNED_INT, value)
    @JvmName("setAttributeUIntList")
    fun setAttribute(name: String, value: List<UInt>) = setAttribute(name, GL_UNSIGNED_INT, value)
    fun setAttribute(name: String, value: Array<UInt>) = setAttribute(name, GL_UNSIGNED_INT, value)

    @JvmName("setAttributeFloatList")
    fun setAttribute(name: String, value: List<Float>) = setAttribute(name, GL_FLOAT, value)
    fun setAttribute(name: String, value: Array<Float>) = setAttribute(name, GL_FLOAT, value)
    fun setAttribute(name: String, value: FloatArray) = setAttribute(name, GL_FLOAT, value)

    @JvmName("setAttributeDoubleList")
    fun setAttribute(name: String, value: List<Double>) = setAttribute(name, GL_DOUBLE, value)
    fun setAttribute(name: String, value: Array<Double>) = setAttribute(name, GL_DOUBLE, value)
    fun setAttribute(name: String, value: DoubleArray) = setAttribute(name, GL_DOUBLE, value)
    //endregion

    //region Int Vector
    @JvmName("setAttributeVector2iList")
    fun setAttribute(name: String, value: List<Vector2i>) = setAttribute(name, GL_INT_VEC2, value)
    fun setAttribute(name: String, value: Array<Vector2i>) = setAttribute(name, GL_INT_VEC2, value)
    fun setAttribute(name: String, value: Vector2iArray) = setAttribute(name, GL_INT_VEC2, value)

    @JvmName("setAttributeVector3iList")
    fun setAttribute(name: String, value: List<Vector3i>) = setAttribute(name, GL_INT_VEC3, value)
    fun setAttribute(name: String, value: Array<Vector3i>) = setAttribute(name, GL_INT_VEC3, value)

    @JvmName("setAttributeVector4iList")
    fun setAttribute(name: String, value: List<Vector4i>) = setAttribute(name, GL_INT_VEC4, value)
    fun setAttribute(name: String, value: Array<Vector4i>) = setAttribute(name, GL_INT_VEC4, value)
    //endregion
    //TODO: GL40.GL_UNSIGNED_INT_VEC2 -> Vector2ui::class
    //TODO: GL40.GL_UNSIGNED_INT_VEC3 -> Vector3ui::class
    //TODO: GL40.GL_UNSIGNED_INT_VEC4 -> Vector4ui::class
    //region Float Vectors
    @JvmName("setAttributeVector2fList")
    fun setAttribute(name: String, value: List<Vector2f>) = setAttribute(name, GL_FLOAT_VEC2, value)
    fun setAttribute(name: String, value: Array<Vector2f>) = setAttribute(name, GL_FLOAT_VEC2, value)

    @JvmName("setAttributeVector3fList")
    fun setAttribute(name: String, value: List<Vector3f>) = setAttribute(name, GL_FLOAT_VEC3, value)
    fun setAttribute(name: String, value: Array<Vector3f>) = setAttribute(name, GL_FLOAT_VEC3, value)

    @JvmName("setAttributeVector4fList")
    fun setAttribute(name: String, value: List<Vector4f>) = setAttribute(name, GL_FLOAT_VEC4, value)
    fun setAttribute(name: String, value: Array<Vector4f>) = setAttribute(name, GL_FLOAT_VEC4, value)
    //endregion
    //region Double Vectors
    @JvmName("setAttributeVector2dList")
    fun setAttribute(name: String, value: List<Vector2d>) = setAttribute(name, GL_DOUBLE_VEC2, value)
    fun setAttribute(name: String, value: Array<Vector2d>) = setAttribute(name, GL_DOUBLE_VEC2, value)

    @JvmName("setAttributeVector3dList")
    fun setAttribute(name: String, value: List<Vector3d>) = setAttribute(name, GL_DOUBLE_VEC3, value)
    fun setAttribute(name: String, value: Array<Vector3d>) = setAttribute(name, GL_DOUBLE_VEC3, value)

    @JvmName("setAttributeVector4dList")
    fun setAttribute(name: String, value: List<Vector4d>) = setAttribute(name, GL_DOUBLE_VEC4, value)
    fun setAttribute(name: String, value: Array<Vector4d>) = setAttribute(name, GL_DOUBLE_VEC4, value)
    //endregion

    //region Float Matrix
    @JvmName("setAttributeMatrix4fList")
    fun setAttribute(name: String, value: List<Matrix4f>) = setAttribute(name, GL_FLOAT_MAT4, value)
    fun setAttribute(name: String, value: Array<Matrix4f>) = setAttribute(name, GL_FLOAT_MAT4, value)

    @JvmName("setAttributeMatrix4x3fList")
    fun setAttribute(name: String, value: List<Matrix4x3f>) = setAttribute(name, GL_FLOAT_MAT4x3, value)
    fun setAttribute(name: String, value: Array<Matrix4x3f>) = setAttribute(name, GL_FLOAT_MAT4x3, value)
    //TODO: GL_FLOAT_MAT4x2 -> Matrix4x2f
    //TODO: GL_FLOAT_MAT3x4 -> Matrix3x4f
    @JvmName("setAttributeMatrix3fList")
    fun setAttribute(name: String, value: List<Matrix3f>) = setAttribute(name, GL_FLOAT_MAT3, value)
    fun setAttribute(name: String, value: Array<Matrix3f>) = setAttribute(name, GL_FLOAT_MAT3, value)

    @JvmName("setAttributeMatrix3x2fList")
    fun setAttribute(name: String, value: List<Matrix3x2f>) = setAttribute(name, GL_FLOAT_MAT3x2, value)
    fun setAttribute(name: String, value: Array<Matrix3x2f>) = setAttribute(name, GL_FLOAT_MAT3x2, value)
    //TODO: GL_FLOAT_MAT2x4 -> Matrix2x4f::class
    //TODO: GL_FLOAT_MAT2x3 -> Matrix2x3f::class
    @JvmName("setAttributeMatrix2fList")
    fun setAttribute(name: String, value: List<Matrix2f>) = setAttribute(name, GL_FLOAT_MAT2, value)
    fun setAttribute(name: String, value: Array<Matrix2f>) = setAttribute(name, GL_FLOAT_MAT2, value)
    //endregion
    //region Double Matrix
    @JvmName("setAttributeMatrix4dList")
    fun setAttribute(name: String, value: List<Matrix4d>) = setAttribute(name, GL_DOUBLE_MAT4, value)
    fun setAttribute(name: String, value: Array<Matrix4d>) = setAttribute(name, GL_DOUBLE_MAT4, value)

    @JvmName("setAttributeMatrix4x3dList")
    fun setAttribute(name: String, value: List<Matrix4x3d>) = setAttribute(name, GL_DOUBLE_MAT4x3, value)
    fun setAttribute(name: String, value: Array<Matrix4x3d>) = setAttribute(name, GL_DOUBLE_MAT4x3, value)
    //TODO: GL40.GL_DOUBLE_MAT4x2 -> Matrix4x2d
    //TODO: GL40.GL_DOUBLE_MAT3x4 -> Matrix3x4d
    @JvmName("setAttributeMatrix3dList")
    fun setAttribute(name: String, value: List<Matrix3d>) = setAttribute(name, GL_DOUBLE_MAT3, value)
    fun setAttribute(name: String, value: Array<Matrix3d>) = setAttribute(name, GL_DOUBLE_MAT3, value)

    @JvmName("setAttributeMatrix3x2dList")
    fun setAttribute(name: String, value: List<Matrix3x2d>) = setAttribute(name, GL_DOUBLE_MAT3x2, value)
    fun setAttribute(name: String, value: Array<Matrix3x2d>) = setAttribute(name, GL_DOUBLE_MAT3x2, value)
    //TODO: GL40.GL_DOUBLE_MAT2x4 -> Matrix2x4d::class
    //TODO: GL40.GL_DOUBLE_MAT2x3 -> Matrix2x3d::class
    @JvmName("setAttributeMatrix2dList")
    fun setAttribute(name: String, value: List<Matrix2d>) = setAttribute(name, GL_DOUBLE_MAT2, value)
    fun setAttribute(name: String, value: Array<Matrix2d>) = setAttribute(name, GL_DOUBLE_MAT2, value)
    //endregion

    fun burnModel() = synchronized(variables) {
        if(glBinds != null)
            throw IllegalArgumentException("The previous model still persists")
        if(shader.released)
            throw NullPointerException("The shader used by this mesh was destroyed")

        val vertexCount = variables.asSequence().map {
            @OptIn(ExperimentalUnsignedTypes::class)
            val c = it.value.let { at-> when (at) {
                is List<*> -> at.size
                is Array<*> -> at.size
                is IntArray -> at.size
                is UIntArray -> at.size
                is FloatArray -> at.size
                is DoubleArray -> at.size
                else -> -1
            }}
            if(c % it.key.count.toInt() == 0) c / it.key.count.toInt() else -1
        }.assertAllEqual { -1 }
        if(vertexCount == -1) throw RuntimeException("Not all vertexes have data specified")

        val vertFormat = shader.attributes.values.associateWithTo(mutableMapOf()) { baseGLTypeBytes(it.type) }
        val vertexSize = vertFormat.values.sum()

        @OptIn(ExperimentalUnsignedTypes::class)
        MemoryStack.stackPush().use { stack ->
            val fullBuffer = stack.malloc(vertexSize * vertexCount)
            variables.forEach { (attribute, list) ->
                val attrSize = baseGLTypeBytes(attribute.type)
                val dataSet = Triple(attribute, vertexSize, attrSize)
                when(attribute.type)
                {
                    GL_INT ->          list.aSequence<Int, IntArray>       { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putInt(i, v) }
                    GL_UNSIGNED_INT -> list.aSequence<UInt, UIntArray>     { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putInt(i, v.toInt()) }
                    GL_FLOAT ->        list.aSequence<Float, FloatArray>   { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putFloat(i, v) }
                    GL_DOUBLE ->       list.aSequence<Double, DoubleArray> { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putDouble(i, v) }

                    GL_INT_VEC2 -> list.aSequence<Vector2i, Vector2iArray> { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2i(i, v) }
                    GL_INT_VEC3 -> list.aSequence<Vector3i/*TODO:Vector3iArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3i(i, v) }
                    GL_INT_VEC4 -> list.aSequence<Vector4i/*TODO:Vector4iArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4i(i, v) }

                    //TODO: GL_UNSIGNED_INT_VEC2 -> Vector2ui
                    //TODO: GL_UNSIGNED_INT_VEC3 -> Vector3ui
                    //TODO: GL_UNSIGNED_INT_VEC4 -> Vector4ui

                    GL_FLOAT_VEC2 -> list.aSequence<Vector2f/*TODO:Vector2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2f(i, v) }
                    GL_FLOAT_VEC3 -> list.aSequence<Vector3f/*TODO:Vector3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3f(i, v) }
                    GL_FLOAT_VEC4 -> list.aSequence<Vector4f/*TODO:Vector4fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4f(i, v) }

                    GL_DOUBLE_VEC2 -> list.aSequence<Vector2d/*TODO:Vector2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2d(i, v) }
                    GL_DOUBLE_VEC3 -> list.aSequence<Vector3d/*TODO:Vector3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3d(i, v) }
                    GL_DOUBLE_VEC4 -> list.aSequence<Vector4d/*TODO:Vector4dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4d(i, v) }

                    GL_FLOAT_MAT4 ->   list.aSequence<Matrix4f    /*TODO:Matrix4fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4f(i, v) }
                    GL_FLOAT_MAT4x3 -> list.aSequence<Matrix4x3f/*TODO:Matrix4x3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4x3f(i, v) }
                    //TODO: GL_FLOAT_MAT4x2 -> Matrix4x2f
                    //TODO: GL_FLOAT_MAT3x4 -> Matrix3x4f
                    GL_FLOAT_MAT3 ->   list.aSequence<Matrix3f    /*TODO:Matrix3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3f(i, v) }
                    GL_FLOAT_MAT3x2 -> list.aSequence<Matrix3x2f/*TODO:Matrix3x2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3x2f(i, v) }
                    //TODO: GL_FLOAT_MAT2x4 -> Matrix2x4f
                    //TODO: GL_FLOAT_MAT2x3 -> Matrix2x3f
                    GL_FLOAT_MAT2 ->   list.aSequence<Matrix2f    /*TODO:Matrix2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix2f(i, v) }

                    GL_DOUBLE_MAT4 ->   list.aSequence<Matrix4d    /*TODO:Matrix4dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4d(i, v) }
                    GL_DOUBLE_MAT4x3 -> list.aSequence<Matrix4x3d/*TODO:Matrix4x3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4x3d(i, v) }
                    //TODO: GL_DOUBLE_MAT4x2 -> Matrix4x2d
                    //TODO: GL_DOUBLE_MAT3x4 -> Matrix3x4d
                    GL_DOUBLE_MAT3 ->   list.aSequence<Matrix3d    /*TODO:Matrix3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3d(i, v) }
                    GL_DOUBLE_MAT3x2 -> list.aSequence<Matrix3x2d/*TODO:Matrix3x2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3x2d(i, v) }
                    //TODO: GL_DOUBLE_MAT2x4 -> Matrix2x4d
                    //TODO: GL_DOUBLE_MAT2x3 -> Matrix2x3d
                    GL_DOUBLE_MAT2 ->   list.aSequence<Matrix2d    /*TODO:Matrix2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix2d(i, v) }
                }
            }

            glBinds = GLBinds(0, 0, 0)
            glBinds?.let {
                cleaner = Mesh.cleaner.register(this) {
                    val onContext = try { glGetError(); true } catch (ignored: NullPointerException) { false }
                    if(onContext)
                        garbageCollect(it)
                    else
                        OnMainThread.schedule { garbageCollect(it)}
                }
                it.vertexCount = vertexCount
                it.vbo = glGenBuffers()
                glBindBuffer(GL_ARRAY_BUFFER, it.vbo)
                glBufferData(GL_ARRAY_BUFFER, fullBuffer, GL_STATIC_DRAW)
            }
        }

        glBinds?.let { bind ->
            bind.vao = glGenVertexArrays()
            glBindVertexArray(bind.vao)
            shader.use()
            shader.attributes.values.forEach {
                glEnableVertexAttribArray(it.location)
                glVertexAttribPointer(it.location, baseGLPrimitivesCount(it.type),
                    baseGLPrimitiveTypes(it.type), false,
                    vertexSize, it.offset.toLong())
            }
        }
    }
    val isBurned: Boolean get() = synchronized(variables) { glBinds != null }

    private data class GLBinds(var vao: Int, var vbo: Int, var vertexCount: Int)
}