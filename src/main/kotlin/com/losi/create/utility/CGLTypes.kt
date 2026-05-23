@file:Suppress("SpellCheckingInspection", "GrazieInspectionRunner")
@file:JvmName("CGLTypes")

package com.losi.create.utility

import org.joml.*
import org.lwjgl.opengl.GL40.*
import kotlin.reflect.KClass

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

    //TODO: GL_UNSIGNED_INT_VEC2 -> Vector2ui::class
    //TODO: GL_UNSIGNED_INT_VEC3 -> Vector3ui::class
    //TODO: GL_UNSIGNED_INT_VEC4 -> Vector4ui::class

    //Float Vectors
    GL_FLOAT_VEC2 -> Vector2f::class
    GL_FLOAT_VEC3 -> Vector3f::class
    GL_FLOAT_VEC4 -> Vector4f::class

    //Double Vectors
    GL_DOUBLE_VEC2 -> Vector2d::class
    GL_DOUBLE_VEC3 -> Vector3d::class
    GL_DOUBLE_VEC4 -> Vector4d::class

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
    GL_DOUBLE_MAT4 ->   Matrix4d::class
    GL_DOUBLE_MAT4x3 -> Matrix4x3d::class
    //TODO: GL_DOUBLE_MAT4x2 -> Matrix4x2d
    //TODO: GL_DOUBLE_MAT3x4 -> Matrix3x4d
    GL_DOUBLE_MAT3 ->   Matrix4x3d::class
    GL_DOUBLE_MAT3x2 -> Matrix3d::class
    //TODO: GL_DOUBLE_MAT2x4 -> Matrix2x4d::class
    //TODO: GL_DOUBLE_MAT2x3 -> Matrix2x3d::class
    GL_DOUBLE_MAT2 ->   Matrix4f::class

    //TODO: Samplers, Images, etc
    else -> null
}
fun baseGLPrimitiveTypes(type: Int): Int = when(type) {
    GL_BOOL -> GL_BOOL
    GL_INT -> GL_INT
    GL_UNSIGNED_INT -> GL_UNSIGNED_INT
    GL_FLOAT -> GL_FLOAT
    GL_DOUBLE -> GL_DOUBLE

    GL_BOOL_VEC2 -> GL_BOOL
    GL_BOOL_VEC3 -> GL_BOOL
    GL_BOOL_VEC4 -> GL_BOOL

    //Int Vector
    GL_INT_VEC2 -> GL_INT
    GL_INT_VEC3 -> GL_INT
    GL_INT_VEC4 -> GL_INT

    GL_UNSIGNED_INT_VEC2 -> GL_UNSIGNED_INT
    GL_UNSIGNED_INT_VEC3 -> GL_UNSIGNED_INT
    GL_UNSIGNED_INT_VEC4 -> GL_UNSIGNED_INT

    //Float Vectors
    GL_FLOAT_VEC2 -> GL_FLOAT
    GL_FLOAT_VEC3 -> GL_FLOAT
    GL_FLOAT_VEC4 -> GL_FLOAT

    //Double Vectors
    GL_DOUBLE_VEC2 -> GL_DOUBLE
    GL_DOUBLE_VEC3 -> GL_DOUBLE
    GL_DOUBLE_VEC4 -> GL_DOUBLE

    //Float Matrix
    GL_FLOAT_MAT4 ->   GL_FLOAT
    GL_FLOAT_MAT4x3 -> GL_FLOAT
    GL_FLOAT_MAT4x2 -> GL_FLOAT
    GL_FLOAT_MAT3x4 -> GL_FLOAT
    GL_FLOAT_MAT3 ->   GL_FLOAT
    GL_FLOAT_MAT3x2 -> GL_FLOAT
    GL_FLOAT_MAT2x4 -> GL_FLOAT
    GL_FLOAT_MAT2x3 -> GL_FLOAT
    GL_FLOAT_MAT2 ->   GL_FLOAT

    //Double Matrix
    GL_DOUBLE_MAT4 ->   GL_DOUBLE
    GL_DOUBLE_MAT4x3 -> GL_DOUBLE
    GL_DOUBLE_MAT4x2 -> GL_DOUBLE
    GL_DOUBLE_MAT3x4 -> GL_DOUBLE
    GL_DOUBLE_MAT3 ->   GL_DOUBLE
    GL_DOUBLE_MAT3x2 -> GL_DOUBLE
    GL_DOUBLE_MAT2x4 -> GL_DOUBLE
    GL_DOUBLE_MAT2x3 -> GL_DOUBLE
    GL_DOUBLE_MAT2 ->   GL_DOUBLE

    else -> GL_INT
}
fun baseGLPrimitivesCount(type: Int): Int = when(type) {
    GL_BOOL -> 1
    GL_INT -> 1
    GL_UNSIGNED_INT -> 1
    GL_FLOAT -> 1
    GL_DOUBLE -> 1

    GL_BOOL_VEC2 -> 2
    GL_BOOL_VEC3 -> 3
    GL_BOOL_VEC4 -> 4

    //Int Vector
    GL_INT_VEC2 -> 2
    GL_INT_VEC3 -> 3
    GL_INT_VEC4 -> 4

    GL_UNSIGNED_INT_VEC2 -> 2
    GL_UNSIGNED_INT_VEC3 -> 3
    GL_UNSIGNED_INT_VEC4 -> 4

    //Float Vectors
    GL_FLOAT_VEC2 -> 2
    GL_FLOAT_VEC3 -> 3
    GL_FLOAT_VEC4 -> 4

    //Double Vectors
    GL_DOUBLE_VEC2 -> 2
    GL_DOUBLE_VEC3 -> 3
    GL_DOUBLE_VEC4 -> 4

    //Float Matrix
    GL_FLOAT_MAT4 ->   4*4
    GL_FLOAT_MAT4x3 -> 4*3
    GL_FLOAT_MAT4x2 -> 4*2
    GL_FLOAT_MAT3x4 -> 3*4
    GL_FLOAT_MAT3 ->   3*3
    GL_FLOAT_MAT3x2 -> 3*2
    GL_FLOAT_MAT2x4 -> 2*4
    GL_FLOAT_MAT2x3 -> 2*3
    GL_FLOAT_MAT2 ->   2*2

    //Double Matrix
    GL_DOUBLE_MAT4 ->   4*4
    GL_DOUBLE_MAT4x3 -> 4*3
    GL_DOUBLE_MAT4x2 -> 4*2
    GL_DOUBLE_MAT3x4 -> 3*4
    GL_DOUBLE_MAT3 ->   3*3
    GL_DOUBLE_MAT3x2 -> 3*2
    GL_DOUBLE_MAT2x4 -> 2*4
    GL_DOUBLE_MAT2x3 -> 2*3
    GL_DOUBLE_MAT2 ->   2*2

    else -> GL_INT
}
fun baseGLTypeBytes(type: Int): Int =
    when(baseGLPrimitiveTypes(type)){
        GL_BOOL -> 1
        GL_INT -> 4
        GL_UNSIGNED_INT -> 4
        GL_FLOAT -> 4
        GL_DOUBLE -> 8
        else -> 0
    } * baseGLPrimitivesCount(type)