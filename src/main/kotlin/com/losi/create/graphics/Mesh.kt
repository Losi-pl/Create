package com.losi.create.graphics

import com.losi.create.graphics.gl.GLBound
import com.losi.create.graphics.gl.GLSLVar
import com.losi.create.math.collections.*
import com.losi.create.utility.*
import org.joml.*
import org.lwjgl.opengl.GL40.*
import org.lwjgl.system.MemoryStack
import java.lang.ref.Cleaner

/**Creates a model that can be drawn with OpenGL
 *
 * The Mesh is automatically bound to a [Shader] and will automatically dissolve if the Shader is dissolved*/
@Suppress("unused")
class Mesh: GLBound {
    companion object {
        /**The [Cleaner] used to ensure that the resources bound the this [Mesh] are released if the object is Garbage Collected*/
        val cleaner = Cleaner.create()!!

        /**Used to force the type in the [List] to be recognized as [T]*/
        private inline fun <reified T> List<*>.assert(): List<T> {
            require(first() is T) { "${first()!!::class} is not an instance of ${T::class}" }
            @Suppress("UNCHECKED_CAST")
            return this as List<T>
        }
        /**Used to force the type in the [Array] to be recognized as [T]*/
        private inline fun <reified T> Array<*>.assert(): Array<T> {
            require(first() is T) { "${first()!!::class} is not an instance of ${T::class}" }
            @Suppress("UNCHECKED_CAST")
            return this as Array<T>
        }

        /**Used to convert an unknown collection of objects into a sequence with added mechanism for unrecognized containers
         *
         * In build types: [List], [Array]
         * @param T The type of objects the looked sequences should contain
         * @param NotK The extra type which the method should be able to recognize with its caster provided in [setArray]
         * @param setArray Used to manage an extra type the caster should be able to recognize, it that type is picked up it is passed to this lambda to be specifically cast into a [Sequence]*/
        private inline fun <reified T, reified NotK> Any?.aSequence(setArray: (NotK) -> Sequence<T>): Sequence<T> = this.let { when (it) {
            is NotK -> setArray(it)
            is List<*> ->  it.assert<T>().asSequence()
            is Array<*> -> it.assert<T>().asSequence()
            else -> sequenceOf()
        }}
        /**Will convert a known type into a sequence
         *
         * Known types: [List], [Array]*/
        private inline fun <reified T> Any?.aSequence(): Sequence<T> = this.let { when (it) {
            is List<*> ->  it.assert<T>().asSequence()
            is Array<*> -> it.assert<T>().asSequence()
            else -> sequenceOf()
        }}

        /**Used to put objects into a [ByteBuffer][java.nio.ByteBuffer]
         *
         * Works by being provided a sequence of object and [data] informing the method how to properly scatter them in that boffer
         * @param data A set of values: [Attribute][Shader.Attribute] with the data of the attribute being set, an [Int] specifying how much the data has to be stadered, and [Int] specifying the secondary stadder if there has to be more than one object in a group together
         * @param action the method to handle the process of putting the specific object into the buffer, with its specific position and content being already calculated by the method*/
        private inline fun <T> Sequence<T>.putIntoBuffer(data: Triple<Shader.Attribute, Int, Int>, action: (Int, T) -> Unit) {
            val (attr, verSize, attrSize) = data

            if(attr.count == 1u)
                this.forEachIndexed { index, it -> action(attr.offset + (index * verSize), it) }
            else
                this.chunkedReuse(attr.count.toInt()).withIndex().forEach { ti->
                    ti.value.forEachIndexed { index, it -> action(attr.offset + (attrSize + index) + (ti.index * verSize), it) }
                }
        }

        /**The method used to dissolve the OpenGL data of the model releasing that space in GPU
         * @param data The bindings to specific objects in OpenGL*/
        private fun garbageCollect(data: GLBinds) {
            glDeleteBuffers(data.vbo)
            glDeleteVertexArrays(data.vao)
        }
    }

    /**The [GLBinds] related to this specific Mesh*/
    private var glBinds: GLBinds? = null
    /**The [Shader] this Mesh is being dependent on and is based of*/
    private val shader: Shader
    /**A map of all attributes specified in the [Shader] of this Mesh and the content of those attributes*/
    private val variables: MutableMap<Shader.Attribute, Any?>
    private var cleaner: Cleaner.Cleanable? = null

    /**Creates a Mesh connected to a [Shader]*/
    constructor(shader: Shader) {
        shader.dependencySubscription(this)
        this.shader = shader
        this.variables =  shader.attributes.values
            .associateWithTo(mutableMapOf()) { null }
    }

    /**Releases all data roted in computer ram connected to the structure of the model freeing up the memory and only leaving the necessary parts to use the Mesh or possibly rebuild a new one*/
    fun flushBuffers() = synchronized(variables) { variables.keys.forEach { variables[it] = null }}
    /**As the name states this method is used to draw the Mesh on screen the only extra part in it is a check is the Shader of this model is still viable and if the model itself was burned*/
    fun draw(): Unit = synchronized(variables) {
        if(shader.released)
            throw NullPointerException("The Shader used by this Mesh was destroyed")
        val bin = glBinds ?: throw NullPointerException("This Mesh does not have a burned model to draw")
        glBindVertexArray(bin.vao)
        shader.use()
        shader.assignObjects()
        glDrawArrays(GL_TRIANGLES, 0, bin.vertexCount)
    }

    /**Finds the attribute by its name
     * @throws IllegalArgumentException If the attribute by that name can't be found*/
    private fun findAttr(name: String) = shader.attributes[name].orElse { throw IllegalArgumentException("Attribute \"$name\" not found") }
    /**A map for setting an attribute of [T] type defined in the application
     * @param name Name of the set attribute
     * @param type The expected type within OpenGL format
     * @param values Data to be set to that attribute*/
    private fun <T> setAttribute(name: String, type: GLSLVar, values: T) {
        synchronized(variables)
        {
            if(shader.released)
                throw NullPointerException("The Shader used by this Mesh was destroyed")

            val attr = findAttr(name)
            if(attr.type != type)
                throw IllegalArgumentException("Attribute \"${attr.name}\" is not of type ${type.klass} and requires ${attr.type.klass}")
            else
                variables[attr] = values
        }
    }

    /**Releases data of the model from OpenGL memory after calling this, the current instance of
     * the mesh can no longer be drawn but a new model can still be burned as long as all
     * attributes are set making it usable again*/
    override fun release() = synchronized(variables) {
        cleaner?.clean()
        cleaner = null
        glBinds = null
    }

    //region Primitives
    @JvmName("setAttributeByteList")
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: List<Byte>) = setAttribute(name, GLSLVar.Byte, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Byte>) = setAttribute(name, GLSLVar.Byte, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: ByteArray) = setAttribute(name, GLSLVar.Byte, value)

    @JvmName("setAttributeUByteList")
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: List<UByte>) = setAttribute(name, GLSLVar.UByte, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<UByte>) = setAttribute(name, GLSLVar.UByte, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @OptIn(ExperimentalUnsignedTypes::class)
    fun setAttribute(name: String, value: UByteArray) = setAttribute(name, GLSLVar.UByte, value)

    @JvmName("setAttributeShortList")
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: List<Short>) = setAttribute(name, GLSLVar.Short, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Short>) = setAttribute(name, GLSLVar.Short, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: ShortArray) = setAttribute(name, GLSLVar.Short, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @OptIn(ExperimentalUnsignedTypes::class)
    fun setAttribute(name: String, value: UShortArray) = setAttribute(name, GLSLVar.UShort, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeUShortList")
    fun setAttribute(name: String, value: List<UShort>) = setAttribute(name, GLSLVar.UShort, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<UShort>) = setAttribute(name, GLSLVar.UShort, value)

    @JvmName("setAttributeIntList")
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: List<Int>) = setAttribute(name, GLSLVar.Int, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Int>) = setAttribute(name, GLSLVar.Int, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: IntArray) = setAttribute(name, GLSLVar.Int, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @OptIn(ExperimentalUnsignedTypes::class)
    fun setAttribute(name: String, value: UIntArray) = setAttribute(name, GLSLVar.UInt, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeUIntList")
    fun setAttribute(name: String, value: List<UInt>) = setAttribute(name, GLSLVar.UInt, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<UInt>) = setAttribute(name, GLSLVar.UInt, value)

    @JvmName("setAttributeLongList")
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: List<Long>) = setAttribute(name, GLSLVar.Long, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Long>) = setAttribute(name, GLSLVar.Long, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: LongArray) = setAttribute(name, GLSLVar.Long, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @OptIn(ExperimentalUnsignedTypes::class)
    fun setAttribute(name: String, value: ULongArray) = setAttribute(name, GLSLVar.ULong, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeULongList")
    fun setAttribute(name: String, value: List<ULong>) = setAttribute(name, GLSLVar.ULong, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<ULong>) = setAttribute(name, GLSLVar.ULong, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeFloatList")
    fun setAttribute(name: String, value: List<Float>) = setAttribute(name, GLSLVar.Float, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Float>) = setAttribute(name, GLSLVar.Float, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: FloatArray) = setAttribute(name, GLSLVar.Float, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeDoubleList")
    fun setAttribute(name: String, value: List<Double>) = setAttribute(name, GLSLVar.Double, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Double>) = setAttribute(name, GLSLVar.Double, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: DoubleArray) = setAttribute(name, GLSLVar.Double, value)
    //endregion

    //region Int Vector
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector2iList")
    fun setAttribute(name: String, value: List<Vector2i>) = setAttribute(name, GLSLVar.Vector2i, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector2i>) = setAttribute(name, GLSLVar.Vector2i, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Vector2iArray) = setAttribute(name, GLSLVar.Vector2i, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector3iList")
    fun setAttribute(name: String, value: List<Vector3i>) = setAttribute(name, GLSLVar.Vector3i, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector3i>) = setAttribute(name, GLSLVar.Vector3i, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector4iList")
    fun setAttribute(name: String, value: List<Vector4i>) = setAttribute(name, GLSLVar.Vector4i, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector4i>) = setAttribute(name, GLSLVar.Vector4i, value)
    //endregion
    //region Long Vector
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector2lList")
    fun setAttribute(name: String, value: List<Vector2L>) = setAttribute(name, GLSLVar.Vector2l, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector2L>) = setAttribute(name, GLSLVar.Vector2l, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector3lList")
    fun setAttribute(name: String, value: List<Vector3L>) = setAttribute(name, GLSLVar.Vector3l, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector3L>) = setAttribute(name, GLSLVar.Vector3l, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector4lList")
    fun setAttribute(name: String, value: List<Vector4L>) = setAttribute(name, GLSLVar.Vector4l, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector4L>) = setAttribute(name, GLSLVar.Vector4l, value)
    //endregion
    //region Float Vectors
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector2fList")
    fun setAttribute(name: String, value: List<Vector2f>) = setAttribute(name, GLSLVar.Vector2f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector2f>) = setAttribute(name, GLSLVar.Vector2f, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector3fList")
    fun setAttribute(name: String, value: List<Vector3f>) = setAttribute(name, GLSLVar.Vector3f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector3f>) = setAttribute(name, GLSLVar.Vector3f, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector4fList")
    fun setAttribute(name: String, value: List<Vector4f>) = setAttribute(name, GLSLVar.Vector4f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector4f>) = setAttribute(name, GLSLVar.Vector4f, value)
    //endregion
    //region Double Vectors
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector2dList")
    fun setAttribute(name: String, value: List<Vector2d>) = setAttribute(name, GLSLVar.Vector2d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector2d>) = setAttribute(name, GLSLVar.Vector2d, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector3dList")
    fun setAttribute(name: String, value: List<Vector3d>) = setAttribute(name, GLSLVar.Vector3d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector3d>) = setAttribute(name, GLSLVar.Vector3d, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeVector4dList")
    fun setAttribute(name: String, value: List<Vector4d>) = setAttribute(name, GLSLVar.Vector4d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Vector4d>) = setAttribute(name, GLSLVar.Vector4d, value)
    //endregion

    //region Float Matrix
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix4fList")
    fun setAttribute(name: String, value: List<Matrix4f>) = setAttribute(name, GLSLVar.Matrix4f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix4f>) = setAttribute(name, GLSLVar.Matrix4f, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix4x3fList")
    fun setAttribute(name: String, value: List<Matrix4x3f>) = setAttribute(name, GLSLVar.Matrix4x3f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix4x3f>) = setAttribute(name, GLSLVar.Matrix4x3f, value)
    //TODO: GL_FLOAT_MAT4x2 -> Matrix4x2f
    //TODO: GL_FLOAT_MAT3x4 -> Matrix3x4f
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix3fList")
    fun setAttribute(name: String, value: List<Matrix3f>) = setAttribute(name, GLSLVar.Matrix3f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix3f>) = setAttribute(name, GLSLVar.Matrix3f, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix3x2fList")
    fun setAttribute(name: String, value: List<Matrix3x2f>) = setAttribute(name, GLSLVar.Matrix3x2f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix3x2f>) = setAttribute(name, GLSLVar.Matrix3x2f, value)
    //TODO: GL_FLOAT_MAT2x4 -> Matrix2x4f::class
    //TODO: GL_FLOAT_MAT2x3 -> Matrix2x3f::class
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix2fList")
    fun setAttribute(name: String, value: List<Matrix2f>) = setAttribute(name, GLSLVar.Matrix2f, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix2f>) = setAttribute(name, GLSLVar.Matrix2f, value)
    //endregion
    //region Double Matrix
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix4dList")
    fun setAttribute(name: String, value: List<Matrix4d>) = setAttribute(name, GLSLVar.Matrix4d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix4d>) = setAttribute(name, GLSLVar.Matrix4d, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix4x3dList")
    fun setAttribute(name: String, value: List<Matrix4x3d>) = setAttribute(name, GLSLVar.Matrix4x3d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix4x3d>) = setAttribute(name, GLSLVar.Matrix4x3d, value)
    //TODO: GL40.GL_DOUBLE_MAT4x2 -> Matrix4x2d
    //TODO: GL40.GL_DOUBLE_MAT3x4 -> Matrix3x4d
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix3dList")
    fun setAttribute(name: String, value: List<Matrix3d>) = setAttribute(name, GLSLVar.Matrix3d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix3d>) = setAttribute(name, GLSLVar.Matrix3d, value)

    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix3x2dList")
    fun setAttribute(name: String, value: List<Matrix3x2d>) = setAttribute(name, GLSLVar.Matrix3x2d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix3x2d>) = setAttribute(name, GLSLVar.Matrix3x2d, value)
    //TODO: GL40.GL_DOUBLE_MAT2x4 -> Matrix2x4d::class
    //TODO: GL40.GL_DOUBLE_MAT2x3 -> Matrix2x3d::class
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    @JvmName("setAttributeMatrix2dList")
    fun setAttribute(name: String, value: List<Matrix2d>) = setAttribute(name, GLSLVar.Matrix2d, value)
    /**Set's an attribute by [name] with [value]'s. If an attribute takes in multiple values per vertex they should be listed consecutively*/
    fun setAttribute(name: String, value: Array<Matrix2d>) = setAttribute(name, GLSLVar.Matrix2d, value)
    //endregion

    /**Used to finish the model
     *
     * Using all data set with [setAttribute] a model will be calculated and passed on to OpenGL. If attributes are modified again after that the model in OpenGL has to be dissolved and the model has to be reburned*/
    fun burnModel() = synchronized(variables) {
        if(glBinds != null)
            throw IllegalArgumentException("The previous model still persists")
        if(shader.released)
            throw NullPointerException("The shader used by this mesh was destroyed")

        /**TODO: Add an recognition of all other types of Arrays*/
        val vertexCount = variables.asSequence().map {
            @OptIn(ExperimentalUnsignedTypes::class)
            val c = it.value.let { at-> when (at) {
                is List<*> -> at.size
                is Array<*> -> at.size
                is ByteArray -> at.size
                is UByteArray -> at.size
                is ShortArray -> at.size
                is UShortArray -> at.size
                is IntArray -> at.size
                is UIntArray -> at.size
                is LongArray -> at.size
                is ULongArray -> at.size
                is FloatArray -> at.size
                is DoubleArray -> at.size
                is Vector2iArray -> at.size
                else -> -1
            }}
            if(c % it.key.count.toInt() == 0) c / it.key.count.toInt() else -1
        }.assertAllEqual { -1 }
        if(vertexCount == -1) throw RuntimeException("Not all vertexes have data specified")

        val vertFormat = shader.attributes.values.associateWithTo(mutableMapOf()) { it.type.byteCount }
        val vertexSize = vertFormat.values.sum().toInt()

        @OptIn(ExperimentalUnsignedTypes::class)
        MemoryStack.stackPush().use { stack ->
            val fullBuffer = stack.malloc(vertexSize * vertexCount)
            variables.forEach { (attribute, list) ->
                val dataSet = Triple(attribute, vertexSize, attribute.type.byteCount.toInt())
                when(attribute.type)
                {
                    GLSLVar.Byte ->   list.aSequence<Byte, ByteArray>     { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.put(i, v) }
                    GLSLVar.UByte ->  list.aSequence<UByte, UByteArray>   { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.put(i, v.toByte()) }
                    GLSLVar.Short ->  list.aSequence<Short, ShortArray>   { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putShort(i, v) }
                    GLSLVar.UShort -> list.aSequence<UShort, UShortArray> { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putShort(i, v.toShort()) }
                    GLSLVar.Int ->    list.aSequence<Int, IntArray>       { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putInt(i, v) }
                    GLSLVar.UInt ->   list.aSequence<UInt, UIntArray>     { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putInt(i, v.toInt()) }
                    GLSLVar.Float ->  list.aSequence<Float, FloatArray>   { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putFloat(i, v) }
                    GLSLVar.Double -> list.aSequence<Double, DoubleArray> { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putDouble(i, v) }

                    GLSLVar.Vector2i -> list.aSequence<Vector2i, Vector2iArray> { it.asSequence() }.putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2i(i, v) }
                    GLSLVar.Vector3i -> list.aSequence<Vector3i/*TODO:Vector3iArray*/>().           putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3i(i, v) }
                    GLSLVar.Vector4i -> list.aSequence<Vector4i/*TODO:Vector4iArray*/>().           putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4i(i, v) }

                    GLSLVar.Vector2l -> list.aSequence<Vector2L/*TODO:Vector2LArray*/>().           putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2L(i, v) }
                    GLSLVar.Vector3l -> list.aSequence<Vector3L/*TODO:Vector3LArray*/>().           putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3L(i, v) }
                    GLSLVar.Vector4l -> list.aSequence<Vector4L/*TODO:Vector4LArray*/>().           putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4L(i, v) }

                    GLSLVar.Vector2f -> list.aSequence<Vector2f/*TODO:Vector2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2f(i, v) }
                    GLSLVar.Vector3f -> list.aSequence<Vector3f/*TODO:Vector3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3f(i, v) }
                    GLSLVar.Vector4f -> list.aSequence<Vector4f/*TODO:Vector4fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4f(i, v) }

                    GLSLVar.Vector2d -> list.aSequence<Vector2d/*TODO:Vector2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector2d(i, v) }
                    GLSLVar.Vector3d -> list.aSequence<Vector3d/*TODO:Vector3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector3d(i, v) }
                    GLSLVar.Vector4d -> list.aSequence<Vector4d/*TODO:Vector4dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putVector4d(i, v) }

                    GLSLVar.Matrix4f ->   list.aSequence<Matrix4f    /*TODO:Matrix4fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4f(i, v) }
                    GLSLVar.Matrix4x3f -> list.aSequence<Matrix4x3f/*TODO:Matrix4x3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4x3f(i, v) }
                    //TODO: GL_FLOAT_MAT4x2 -> Matrix4x2f
                    //TODO: GL_FLOAT_MAT3x4 -> Matrix3x4f
                    GLSLVar.Matrix3f ->   list.aSequence<Matrix3f    /*TODO:Matrix3fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3f(i, v) }
                    GLSLVar.Matrix3x2f -> list.aSequence<Matrix3x2f/*TODO:Matrix3x2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3x2f(i, v) }
                    //TODO: GL_FLOAT_MAT2x4 -> Matrix2x4f
                    //TODO: GL_FLOAT_MAT2x3 -> Matrix2x3f
                    GLSLVar.Matrix2f ->   list.aSequence<Matrix2f    /*TODO:Matrix2fArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix2f(i, v) }

                    GLSLVar.Matrix4d ->   list.aSequence<Matrix4d    /*TODO:Matrix4dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4d(i, v) }
                    GLSLVar.Matrix4x3d -> list.aSequence<Matrix4x3d/*TODO:Matrix4x3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix4x3d(i, v) }
                    //TODO: GL_DOUBLE_MAT4x2 -> Matrix4x2d
                    //TODO: GL_DOUBLE_MAT3x4 -> Matrix3x4d
                    GLSLVar.Matrix3d ->   list.aSequence<Matrix3d    /*TODO:Matrix3dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3d(i, v) }
                    GLSLVar.Matrix3x2d -> list.aSequence<Matrix3x2d/*TODO:Matrix3x2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix3x2d(i, v) }
                    //TODO: GL_DOUBLE_MAT2x4 -> Matrix2x4d
                    //TODO: GL_DOUBLE_MAT2x3 -> Matrix2x3d
                    GLSLVar.Matrix2d ->   list.aSequence<Matrix2d    /*TODO:Matrix2dArray*/>().putIntoBuffer(dataSet) { i, v-> fullBuffer.putMatrix2d(i, v) }
                    else -> { }
                }
            }

            glBinds = GLBinds(0, 0, 0).apply  {
                cleaner = Mesh.cleaner.register(this) {
                    val onContext = try { glGetError(); true } catch (ignored: NullPointerException) { false }
                    if(onContext)
                        garbageCollect(this)
                    else
                        OnMainThread.schedule { garbageCollect(this)}
                }
                this.vertexCount = vertexCount
                vbo = glGenBuffers()
                glBindBuffer(GL_ARRAY_BUFFER, vbo)
                glBufferData(GL_ARRAY_BUFFER, fullBuffer, GL_STATIC_DRAW)
            }
        }

        glBinds?.let { bind ->
            bind.vao = glGenVertexArrays()
            glBindVertexArray(bind.vao)
            shader.use()
            shader.attributes.values.forEach {
                glEnableVertexAttribArray(it.location.handle)
                glVertexAttribPointer(it.location.handle, it.type.primitivesCount.toInt(),
                    it.type.primitive.gl, false,
                    vertexSize, it.offset.toLong())
            }
        }
    }
    /**A check if there is a burned model connected to this Mesh*/
    val isBurned: Boolean get() = synchronized(variables) { glBinds != null }

    /**OpenGL handlers connected to this model
     * @property vao Vertex Array, stores bindings to of variables to proper attributes in the model
     * @property vbo Array Buffer, stores the actual data of the model
     * @property vertexCount Stores the count of vertexes in the burned model*/
    private data class GLBinds(var vao: Int, var vbo: Int, var vertexCount: Int)
}