@file:Suppress("unused")
package com.losi.create.utility

import org.joml.*
import org.lwjgl.opengl.GL21.*
import org.lwjgl.opengl.GL40
import org.w3c.dom.Node
import org.w3c.dom.NodeList
import kotlin.reflect.KClass


inline fun NodeList.forEach(action: (node: Node) -> Unit) {
    for (i in 0 until this.length) action(this.item(i))
}
fun Node.getAttribute(name: String): String? = this.attributes.getNamedItem(name).nodeValue
fun NodeList.first(): Node = this.item(0)
fun NodeList.last(): Node = this.item(this.length - 1)
fun <T> T?.orElse(default: T): T = this ?: default
inline fun <T> T?.orElse(default: () -> T): T = this ?: default()
fun  Int?.orElse(els: Int) : Int {
    if(this == null)
        return els
    return this
}
fun translateGLTypes(type: Int): KClass<*>? = when(type) {
    //Basic
    GL_BOOL -> Boolean::class
    GL_INT -> Int::class
    GL_UNSIGNED_INT -> UInt::class
    GL_FLOAT -> Float::class
    GL_DOUBLE -> Double::class

    //TODO: GL_BOOL_VEC2 -> Vector2b::class
    //TODO: GL_BOOL_VEC3 -> Vector3b::class
    //TODO: GL_BOOL_VEC4 -> Vector4b::class

    //Int Vector
    GL_INT_VEC2 -> Vector2i::class
    GL_INT_VEC3 -> Vector3i::class
    GL_INT_VEC4 -> Vector4i::class

    //TODO: GL40.GL_UNSIGNED_INT_VEC2 -> Vector2ui::class
    //TODO: GL40.GL_UNSIGNED_INT_VEC3 -> Vector3ui::class
    //TODO: GL40.GL_UNSIGNED_INT_VEC4 -> Vector4ui::class

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