@file:Suppress("unused", "RemoveRedundantQualifierName", "SpellCheckingInspection")
package com.losi.create.graphics.gl

import com.losi.create.graphics.*
import org.lwjgl.opengl.*
import org.lwjgl.opengles.OESVertexType1010102
import kotlin.reflect.KClass

enum class GLSLVar {
    // Primitives
    /**`GL_BOOL` -> [Boolean][kotlin.Boolean]*/
    Boolean(GL20.GL_BOOL,                                  kotlin.Boolean::class,             name = "GL_BOOL"),
    /**`GL_BYTE` -> [Byte][kotlin.Byte]*/
    Byte   (GL11.GL_BYTE,                                  kotlin.Byte::class,   attr = true, name = "GL_BYTE"),
    /**`GL_UNSIGNED_BYTE` -> [UByte][kotlin.UByte]*/
    UByte  (GL11.GL_UNSIGNED_BYTE,                         kotlin.UByte::class,  attr = true, name = "GL_UNSIGNED_BYTE"),
    /**`GL_SHORT` -> [Short][kotlin.Short]*/
    Short  (GL11.GL_SHORT,                                 kotlin.Short::class,  attr = true, name = "GL_SHORT"),
    /**`GL_UNSIGNED_SHORT` -> [UShort][kotlin.UShort]*/
    UShort (GL11.GL_UNSIGNED_SHORT,                        kotlin.UShort::class, attr = true, name = "GL_UNSIGNED_SHORT"),
    /**`GL_INT` -> [Int][kotlin.Int]*/
    Int    (GL11.GL_INT,                                   kotlin.Int::class,    attr = true, name = "GL_INT"),
    /**`GL_UNSIGNED_INT` -> [UInt][kotlin.UInt]*/
    UInt   (GL11.GL_UNSIGNED_INT,                          kotlin.UInt::class,   attr = true, name = "GL_UNSIGNED_INT"),
    /**`GL_INT64_ARB` -> [Long][kotlin.Long]*/
    Long   (ARBGPUShaderInt64.GL_INT64_ARB,           kotlin.Long::class,   attr = true, name = "GL_INT64_ARB"),
    /**`GL_UNSIGNED_INT64_ARB` -> [ULong][kotlin.ULong]*/
    ULong  (ARBGPUShaderInt64.GL_UNSIGNED_INT64_ARB,  kotlin.ULong::class,  attr = true, name = "GL_UNSIGNED_INT64_ARB"),
    /**`GL_FLOAT` -> [Float][kotlin.Float]*/
    Float  (GL11.GL_FLOAT,                                 kotlin.Float::class,  attr = true, name = "GL_FLOAT"),
    /**`GL_DOUBLE` -> [Double][kotlin.Double]*/
    Double (GL11.GL_DOUBLE,                                kotlin.Double::class, attr = true, name = "GL_DOUBLE"),

    // Atomic
    /**`GL_UNSIGNED_INT_ATOMIC_COUNTER` -> None*/
    AtomicUInt(GL42.GL_UNSIGNED_INT_ATOMIC_COUNTER , null, UInt, name = "GL_UNSIGNED_INT_ATOMIC_COUNTER"),

    // Bool Vectors
    /**`GL_BOOL_VEC2` -> [Vector2b][com.losi.create.math.Vector4b]*/
    Vector2b(GL20.GL_BOOL_VEC2, com.losi.create.math.Vector2b::class, Boolean, 2u, name = "GL_BOOL_VEC2"),
    /**`GL_BOOL_VEC3` -> [Vector3b][com.losi.create.math.Vector4b]*/
    Vector3b(GL20.GL_BOOL_VEC3, com.losi.create.math.Vector3b::class, Boolean, 3u, name = "GL_BOOL_VEC3"),
    /**`GL_BOOL_VEC4` -> [Vector4b][com.losi.create.math.Vector4b]*/
    Vector4b(GL20.GL_BOOL_VEC4, com.losi.create.math.Vector4b::class, Boolean, 4u, name = "GL_BOOL_VEC4"),

    // Int Vectors
    /**`GL_INT_VEC2` -> [Vector2i][org.joml.Vector2i]*/
    Vector2i(GL20.GL_INT_VEC2, org.joml.Vector2i::class, Int, 2u, true, name = "GL_INT_VEC2"),
    /**`GL_INT_VEC3` -> [Vector3i][org.joml.Vector3i]*/
    Vector3i(GL20.GL_INT_VEC3, org.joml.Vector3i::class, Int, 3u, true, name = "GL_INT_VEC3"),
    /**`GL_INT_VEC4` -> [Vector4i][org.joml.Vector4i]*/
    Vector4i(GL20.GL_INT_VEC4, org.joml.Vector4i::class, Int, 4u, true, name = "GL_INT_VEC4"),

    // UInt Vectors
    /**`GL_UNSIGNED_INT_VEC2` -> None*/
    Vector2ui(GL30.GL_UNSIGNED_INT_VEC2, null, UInt, 2u, true, name = "GL_UNSIGNED_INT_VEC2"),
    /**`GL_UNSIGNED_INT_VEC3` -> None*/
    Vector3ui(GL30.GL_UNSIGNED_INT_VEC3, null, UInt, 3u, true, name = "GL_UNSIGNED_INT_VEC3"),
    /**`GL_UNSIGNED_INT_VEC4` -> None*/
    Vector4ui(GL30.GL_UNSIGNED_INT_VEC4, null, UInt, 4u, true, name = "GL_UNSIGNED_INT_VEC4"),

    // Long Vectors
    /**`GL_INT64_VEC2_ARB` -> [Vector2L][org.joml.Vector2L]*/
    Vector2l(ARBGPUShaderInt64.GL_INT64_VEC2_ARB, org.joml.Vector2L::class, Long, 2u, true, name = "GL_INT64_VEC2_ARB"),
    /**`GL_INT64_VEC3_ARB` -> [Vector3L][org.joml.Vector3L]*/
    Vector3l(ARBGPUShaderInt64.GL_INT64_VEC3_ARB, org.joml.Vector3L::class, Long, 3u, true, name = "GL_INT64_VEC3_ARB"),
    /**`GL_INT64_VEC4_ARB` -> [Vector4L][org.joml.Vector4L]*/
    Vector4l(ARBGPUShaderInt64.GL_INT64_VEC4_ARB, org.joml.Vector4L::class, Long, 4u, true, name = "GL_INT64_VEC4_ARB"),

    // ULong Vectors
    /**`GL_UNSIGNED_INT64_VEC2_ARB` -> None*/
    Vector2ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC2_ARB, null, ULong, 2u, true, name = "GL_UNSIGNED_INT64_VEC2_ARB"),
    /**`GL_UNSIGNED_INT64_VEC3_ARB` -> None*/
    Vector3ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC3_ARB, null, ULong, 3u, true, name = "GL_UNSIGNED_INT64_VEC3_ARB"),
    /**`GL_UNSIGNED_INT64_VEC4_ARB` -> None*/
    Vector4ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC4_ARB, null, ULong, 4u, true, name = "GL_UNSIGNED_INT64_VEC4_ARB"),

    // Float Vectors
    /**`GL_FLOAT_VEC2` -> [Vector2f][org.joml.Vector2f]*/
    Vector2f(GL20.GL_FLOAT_VEC2, org.joml.Vector2f::class, Float, 2u, true, name = "GL_FLOAT_VEC2"),
    /**`GL_FLOAT_VEC3` -> [Vector3f][org.joml.Vector3f]*/
    Vector3f(GL20.GL_FLOAT_VEC3, org.joml.Vector3f::class, Float, 3u, true, name = "GL_FLOAT_VEC3"),
    /**`GL_FLOAT_VEC4` -> [Vector4f][org.joml.Vector4f]*/
    Vector4f(GL20.GL_FLOAT_VEC4, org.joml.Vector4f::class, Float, 4u, true, name = "GL_FLOAT_VEC4"),

    //Double Vectors
    /**`GL_DOUBLE_VEC2` -> [Vector2d][org.joml.Vector2d]*/
    Vector2d(ARBGPUShaderFP64.GL_DOUBLE_VEC2, org.joml.Vector2d::class, Double, 2u, true, name = "GL_DOUBLE_VEC2"),
    /**`GL_DOUBLE_VEC3` -> [Vector3d][org.joml.Vector3d]*/
    Vector3d(ARBGPUShaderFP64.GL_DOUBLE_VEC3, org.joml.Vector3d::class, Double, 3u, true, name = "GL_DOUBLE_VEC3"),
    /**`GL_DOUBLE_VEC4` -> [Vector4d][org.joml.Vector4d]*/
    Vector4d(ARBGPUShaderFP64.GL_DOUBLE_VEC4, org.joml.Vector4d::class, Double, 4u, true, name = "GL_DOUBLE_VEC4"),

    // Float Matrix
    /**`GL_FLOAT_MAT4` -> [Matrix4f][org.joml.Matrix4f]*/
    Matrix4f  (GL20.GL_FLOAT_MAT4,   org.joml.Matrix4f::class,   Float, 4u * 4u, true, "GL_FLOAT_MAT4"),
    /**`GL_FLOAT_MAT4x3` -> [Matrix4x3f][org.joml.Matrix4x3f]*/
    Matrix4x3f(GL21.GL_FLOAT_MAT4x3, org.joml.Matrix4x3f::class, Float, 4u * 3u, true, "GL_FLOAT_MAT4x3"),
    /**`GL_FLOAT_MAT4x2` -> None*/
    Matrix4x2f(GL21.GL_FLOAT_MAT4x2, null,                       Float, 4u * 2u, true, "GL_FLOAT_MAT4x2"),
    /**`GL_FLOAT_MAT3x4` -> None*/
    Matrix3x4f(GL21.GL_FLOAT_MAT3x4, null,                       Float, 3u * 4u, true, "GL_FLOAT_MAT3x4"),
    /**`GL_FLOAT_MAT3` -> [Matrix3f][org.joml.Matrix3f]*/
    Matrix3f  (GL20.GL_FLOAT_MAT3,   org.joml.Matrix3f::class,   Float, 3u * 3u, true, "GL_FLOAT_MAT3"),
    /**`GL_FLOAT_MAT3x2` -> [Matrix3x2f][org.joml.Matrix3x2f]*/
    Matrix3x2f(GL21.GL_FLOAT_MAT3x2, org.joml.Matrix3x2f::class, Float, 3u * 2u, true, "GL_FLOAT_MAT3x2"),
    /**`GL_FLOAT_MAT2x4` -> None*/
    Matrix2x4f(GL21.GL_FLOAT_MAT2x4, null,                       Float, 2u * 4u, true, "GL_FLOAT_MAT2x4"),
    /**`GL_FLOAT_MAT2x3` -> None*/
    Matrix2x3f(GL21.GL_FLOAT_MAT2x3, null,                       Float, 2u * 3u, true, "GL_FLOAT_MAT2x3"),
    /**`GL_FLOAT_MAT2` -> [Matrix2f][org.joml.Matrix2f]*/
    Matrix2f  (GL20.GL_FLOAT_MAT2,   org.joml.Matrix2f::class,   Float, 2u * 2u, true, "GL_FLOAT_MAT2"),

    // Double Matrix
    /**`GL_DOUBLE_MAT4` -> [Matrix4d][org.joml.Matrix4d]*/
    Matrix4d  (ARBGPUShaderFP64.GL_DOUBLE_MAT4,   org.joml.Matrix4d::class,   Double, 4u * 4u, true, "GL_DOUBLE_MAT4"),
    /**`GL_DOUBLE_MAT4x3` -> [Matrix4x3d][org.joml.Matrix4x3d]*/
    Matrix4x3d(ARBGPUShaderFP64.GL_DOUBLE_MAT4x3, org.joml.Matrix4x3d::class, Double, 4u * 3u, true, "GL_DOUBLE_MAT4x3"),
    /**`GL_DOUBLE_MAT4x2` -> None*/
    Matrix4x2d(ARBGPUShaderFP64.GL_DOUBLE_MAT4x2, null,                       Double, 4u * 2u, true, "GL_DOUBLE_MAT4x2"),
    /**`GL_DOUBLE_MAT3x4` -> None*/
    Matrix3x4d(ARBGPUShaderFP64.GL_DOUBLE_MAT3x4, null,                       Double, 3u * 4u, true, "GL_DOUBLE_MAT3x4"),
    /**`GL_DOUBLE_MAT3` -> [Matrix3d][org.joml.Matrix3d]*/
    Matrix3d  (ARBGPUShaderFP64.GL_DOUBLE_MAT3,   org.joml.Matrix3d::class,   Double, 3u * 3u, true, "GL_DOUBLE_MAT3"),
    /**`GL_DOUBLE_MAT3x2` -> [Matrix3x2d][org.joml.Matrix3x2d]*/
    Matrix3x2d(ARBGPUShaderFP64.GL_DOUBLE_MAT3x2, org.joml.Matrix3x2d::class, Double, 3u * 2u, true, "GL_DOUBLE_MAT3x2"),
    /**`GL_DOUBLE_MAT2x4` -> None*/
    Matrix2x4d(ARBGPUShaderFP64.GL_DOUBLE_MAT2x4, null,                       Double, 2u * 4u, true, "GL_DOUBLE_MAT2x4"),
    /**`GL_DOUBLE_MAT2x3` -> None*/
    Matrix2x3d(ARBGPUShaderFP64.GL_DOUBLE_MAT2x3, null,                       Double, 2u * 3u, true, "GL_DOUBLE_MAT2x3"),
    /**`GL_DOUBLE_MAT2` -> [Matrix2d][org.joml.Matrix2d]*/
    Matrix2d  (ARBGPUShaderFP64.GL_DOUBLE_MAT2,   org.joml.Matrix2d::class,   Double, 2u * 2u, true, "GL_DOUBLE_MAT2"),

    // Regular Samplers
    /**`GL_SAMPLER_1D` -> None*/
    Sampler1D                (GL20.GL_SAMPLER_1D,                    null,             Int, obj = true, name = "GL_SAMPLER_1D"),
    /**`GL_SAMPLER_2D` -> [Texture2D]*/
    Sampler2D                (GL20.GL_SAMPLER_2D,                    Texture2D::class, Int, obj = true, name = "GL_SAMPLER_2D"),
    /**`GL_SAMPLER_3D` -> None*/
    Sampler3D                (GL20.GL_SAMPLER_3D,                    null,             Int, obj = true, name = "GL_SAMPLER_3D"),
    /**`GL_SAMPLER_2D_MULTISAMPLE` -> None*/
    Sampler2DMul             (GL32.GL_SAMPLER_2D_MULTISAMPLE,        null,             Int, obj = true, name = "GL_SAMPLER_2D_MULTISAMPLE"),
    /**`GL_SAMPLER_CUBE` -> None*/
    SamplerCube              (GL20.GL_SAMPLER_CUBE,                  null,             Int, obj = true, name = "GL_SAMPLER_CUBE"),
    /**`GL_SAMPLER_BUFFER` -> None*/
    SamplerBuffer            (GL31.GL_SAMPLER_BUFFER,                null,             Int, obj = true, name = "GL_SAMPLER_BUFFER"),
    /**`GL_SAMPLER_2D_RECT` -> None*/
    Sampler2DRect            (GL31.GL_SAMPLER_2D_RECT,               null,             Int, obj = true, name = "GL_SAMPLER_2D_RECT"),
    /**`GL_SAMPLER_1D_SHADOW` -> None*/
    Sampler1DShadow          (GL20.GL_SAMPLER_1D_SHADOW,             null,             Int, obj = true, name = "GL_SAMPLER_1D_SHADOW"),
    /**`GL_SAMPLER_2D_SHADOW` -> None*/
    Sampler2DShadow          (GL20.GL_SAMPLER_2D_SHADOW,             null,             Int, obj = true, name = "GL_SAMPLER_2D_SHADOW"),
    /**`GL_SAMPLER_CUBE_SHADOW` -> None*/
    SamplerCubeShadow        (GL30.GL_SAMPLER_CUBE_SHADOW,           null,             Int, obj = true, name = "GL_SAMPLER_CUBE_SHADOW"),
    /**`GL_SAMPLER_1D_ARRAY` -> None*/
    Sampler1DArray           (GL30.GL_SAMPLER_1D_ARRAY,              null,             Int, obj = true, name = "GL_SAMPLER_1D_ARRAY"),
    /**`GL_SAMPLER_2D_ARRAY` -> None*/
    Sampler2DArray           (GL30.GL_SAMPLER_2D_ARRAY,              null,             Int, obj = true, name = "GL_SAMPLER_2D_ARRAY"),
    /**`GL_SAMPLER_2D_MULTISAMPLE_ARRAY` -> None*/
    Sampler2DMulArray        (GL32.GL_SAMPLER_2D_MULTISAMPLE_ARRAY,  null,             Int, obj = true, name = "GL_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    /**`GL_SAMPLER_CUBE_MAP_ARRAY` -> None*/
    SamplerCubeArray         (GL40.GL_SAMPLER_CUBE_MAP_ARRAY,        null,             Int, obj = true, name = "GL_SAMPLER_CUBE_MAP_ARRAY"),
    /**`GL_SAMPLER_1D_ARRAY_SHADOW` -> None*/
    Sampler1DArrayShadow     (GL30.GL_SAMPLER_1D_ARRAY_SHADOW,       null,             Int, obj = true, name = "GL_SAMPLER_1D_ARRAY_SHADOW"),
    /**`GL_SAMPLER_2D_ARRAY_SHADOW` -> None*/
    Sampler2DArrayShadow     (GL30.GL_SAMPLER_2D_ARRAY_SHADOW,       null,             Int, obj = true, name = "GL_SAMPLER_2D_ARRAY_SHADOW"),
    /**`GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW` -> None*/
    SamplerCubeMapArrayShadow(GL40.GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW, null,             Int, obj = true, name = "GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW"),
    /**`GL_SAMPLER_2D_RECT_SHADOW` -> None*/
    Sampler2DRectShadow      (GL31.GL_SAMPLER_2D_RECT_SHADOW,        null,             Int, obj = true, name = "GL_SAMPLER_2D_RECT_SHADOW"),

    // Signed Integer Samplers
    /**`GL_INT_SAMPLER_1D` -> None*/
    IntSampler1D          (GL30.GL_INT_SAMPLER_1D,                   null, Int, obj = true, name = "GL_INT_SAMPLER_1D"),
    /**`GL_INT_SAMPLER_2D` -> None*/
    IntSampler2D          (GL30.GL_INT_SAMPLER_2D,                   null, Int, obj = true, name = "GL_INT_SAMPLER_2D"),
    /**`GL_INT_SAMPLER_3D` -> None*/
    IntSampler3D          (GL30.GL_INT_SAMPLER_3D,                   null, Int, obj = true, name = "GL_INT_SAMPLER_3D"),
    /**`GL_INT_SAMPLER_2D_MULTISAMPLE` -> None*/
    IntSampler2DMul       (GL32.GL_INT_SAMPLER_2D_MULTISAMPLE,       null, Int, obj = true, name = "GL_INT_SAMPLER_2D_MULTISAMPLE"),
    /**`GL_INT_SAMPLER_CUBE` -> None*/
    IntSamplerCube        (GL30.GL_INT_SAMPLER_CUBE,                 null, Int, obj = true, name = "GL_INT_SAMPLER_CUBE"),
    /**`GL_INT_SAMPLER_1D_ARRAY` -> None*/
    IntSampler1DArray     (GL30.GL_INT_SAMPLER_1D_ARRAY,             null, Int, obj = true, name = "GL_INT_SAMPLER_1D_ARRAY"),
    /**`GL_INT_SAMPLER_2D_ARRAY` -> None*/
    IntSampler2DArray     (GL30.GL_INT_SAMPLER_2D_ARRAY,             null, Int, obj = true, name = "GL_INT_SAMPLER_2D_ARRAY"),
    /**`GL_INT_SAMPLER_CUBE_MAP_ARRAY` -> None*/
    IntSamplerCubeMapArray(GL40.GL_INT_SAMPLER_CUBE_MAP_ARRAY,       null, Int, obj = true, name = "GL_INT_SAMPLER_CUBE_MAP_ARRAY"),
    /**`GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY` -> None*/
    IntSampler2DMulArray  (GL32.GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY, null, Int, obj = true, name = "GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    /**`GL_INT_SAMPLER_BUFFER` -> None*/
    IntSamplerBuffer      (GL31.GL_INT_SAMPLER_BUFFER,               null, Int, obj = true, name = "GL_INT_SAMPLER_BUFFER"),
    /**`GL_INT_SAMPLER_2D_RECT` -> None*/
    IntSampler2DRect      (GL31.GL_INT_SAMPLER_2D_RECT,              null, Int, obj = true, name = "GL_INT_SAMPLER_2D_RECT"),

    // Unsigned Integer Samplers
    /**`GL_UNSIGNED_INT_SAMPLER_1D` -> None*/
    UIntSampler1D          (GL30.GL_UNSIGNED_INT_SAMPLER_1D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_1D"),
    /**`GL_UNSIGNED_INT_SAMPLER_2D` -> None*/
    UIntSampler2D          (GL30.GL_UNSIGNED_INT_SAMPLER_2D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_2D"),
    /**`GL_UNSIGNED_INT_SAMPLER_3D` -> None*/
    UIntSampler3D          (GL30.GL_UNSIGNED_INT_SAMPLER_3D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_3D"),
    /**`GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE` -> None*/
    UIntSampler2DMul       (GL32.GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE,       null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE"),
    /**`GL_UNSIGNED_INT_SAMPLER_CUBE` -> None*/
    UIntSamplerCube        (GL30.GL_UNSIGNED_INT_SAMPLER_CUBE,                 null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_CUBE"),
    /**`GL_UNSIGNED_INT_SAMPLER_1D_ARRAY` -> None*/
    UIntSampler1DArray     (GL30.GL_UNSIGNED_INT_SAMPLER_1D_ARRAY,             null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_1D_ARRAY"),
    /**`GL_UNSIGNED_INT_SAMPLER_2D_ARRAY` -> None*/
    UIntSampler2DArray    (GL30.GL_UNSIGNED_INT_SAMPLER_2D_ARRAY,             null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_2D_ARRAY"),
    /**`GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY` -> None*/
    UIntSamplerCubeMapArray(GL40.GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY,       null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY"),
    /**`GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY` -> None*/
    UIntSampler2DMulArray  (GL32.GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY, null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    /**`GL_UNSIGNED_INT_SAMPLER_BUFFER` -> None*/
    UIntSamplerBuffer      (GL31.GL_UNSIGNED_INT_SAMPLER_BUFFER,               null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_BUFFER"),
    /**`GL_UNSIGNED_INT_SAMPLER_2D_RECT` -> None*/
    UIntSampler2DRect      (GL31.GL_UNSIGNED_INT_SAMPLER_2D_RECT,              null, Int, obj = true, name = "GL_UNSIGNED_INT_SAMPLER_2D_RECT"),

    // Regular Images
    /**`GL_IMAGE_1D` -> None*/
    Image1D                (GL42.GL_IMAGE_1D,                    null, Int, obj = true, name = "GL_IMAGE_1D"),
    /**`GL_IMAGE_2D` -> None*/
    Image2D                (GL42.GL_IMAGE_2D,                    null, Int, obj = true, name = "GL_IMAGE_2D"),
    /**`GL_IMAGE_3D` -> None*/
    Image3D                (GL42.GL_IMAGE_3D,                    null, Int, obj = true, name = "GL_IMAGE_3D"),
    /**`GL_IMAGE_2D_MULTISAMPLE` -> None*/
    Image2DMul             (GL42.GL_IMAGE_2D_MULTISAMPLE,        null, Int, obj = true, name = "GL_IMAGE_2D_MULTISAMPLE"),
    /**`GL_IMAGE_CUBE` -> None*/
    ImageCube              (GL42.GL_IMAGE_CUBE,                  null, Int, obj = true, name = "GL_IMAGE_CUBE"),
    /**`GL_IMAGE_BUFFER` -> None*/
    ImageBuffer            (GL42.GL_IMAGE_BUFFER,                null, Int, obj = true, name = "GL_IMAGE_BUFFER"),
    /**`GL_IMAGE_2D_RECT` -> None*/
    Image2DRect            (GL42.GL_IMAGE_2D_RECT,               null, Int, obj = true, name = "GL_IMAGE_2D_RECT"),
    /**`GL_IMAGE_1D_ARRAY` -> None*/
    Image1DArray           (GL42.GL_IMAGE_1D_ARRAY,              null, Int, obj = true, name = "GL_IMAGE_1D_ARRAY"),
    /**`GL_IMAGE_2D_ARRAY` -> None*/
    Image2DArray           (GL42.GL_IMAGE_2D_ARRAY,              null, Int, obj = true, name = "GL_IMAGE_2D_ARRAY"),
    /**`GL_IMAGE_2D_MULTISAMPLE_ARRAY` -> None*/
    Image2DMulArray        (GL42.GL_IMAGE_2D_MULTISAMPLE_ARRAY,  null, Int, obj = true, name = "GL_IMAGE_2D_MULTISAMPLE_ARRAY"),
    /**`GL_IMAGE_CUBE_MAP_ARRAY` -> None*/
    ImageCubeArray         (GL42.GL_IMAGE_CUBE_MAP_ARRAY,        null, Int, obj = true, name = "GL_IMAGE_CUBE_MAP_ARRAY"),

    // Signed Integer Images
    /**`GL_INT_IMAGE_1D` -> None*/
    IntImage1D          (GL42.GL_INT_IMAGE_1D,                   null, Int, obj = true, name = "GL_INT_IMAGE_1D"),
    /**`GL_INT_IMAGE_2D` -> None*/
    IntImage2D          (GL43.GL_INT_IMAGE_2D,                   null, Int, obj = true, name = "GL_INT_IMAGE_2D"),
    /**`GL_INT_IMAGE_3D` -> None*/
    IntImage3D          (GL42.GL_INT_IMAGE_3D,                   null, Int, obj = true, name = "GL_INT_IMAGE_3D"),
    /**`GL_INT_IMAGE_2D_MULTISAMPLE` -> None*/
    IntImage2DMul       (GL42.GL_INT_IMAGE_2D_MULTISAMPLE,       null, Int, obj = true, name = "GL_INT_IMAGE_2D_MULTISAMPLE"),
    /**`GL_INT_IMAGE_CUBE` -> None*/
    IntImageCube        (GL42.GL_INT_IMAGE_CUBE,                 null, Int, obj = true, name = "GL_INT_IMAGE_CUBE"),
    /**`GL_INT_IMAGE_1D_ARRAY` -> None*/
    IntImage1DArray     (GL42.GL_INT_IMAGE_1D_ARRAY,             null, Int, obj = true, name = "GL_INT_IMAGE_1D_ARRAY"),
    /**`GL_INT_IMAGE_2D_ARRAY` -> None*/
    IntImage2DArray     (GL42.GL_INT_IMAGE_2D_ARRAY,             null, Int, obj = true, name = "GL_INT_IMAGE_2D_ARRAY"),
    /**`GL_INT_IMAGE_CUBE_MAP_ARRAY` -> None*/
    IntImageCubeMapArray(GL42.GL_INT_IMAGE_CUBE_MAP_ARRAY,       null, Int, obj = true, name = "GL_INT_IMAGE_CUBE_MAP_ARRAY"),
    /**`GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY` -> None*/
    IntImage2DMulArray  (GL42.GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY, null, Int, obj = true, name = "GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY"),
    /**`GL_INT_IMAGE_BUFFER` -> None*/
    IntImageBuffer      (GL42.GL_INT_IMAGE_BUFFER,               null, Int, obj = true, name = "GL_INT_IMAGE_BUFFER"),
    /**`GL_INT_IMAGE_2D_RECT` -> None*/
    IntImage2DRect      (GL42.GL_INT_IMAGE_2D_RECT,              null, Int, obj = true, name = "GL_INT_IMAGE_2D_RECT"),

    // Unsigned Integer Images
    /**`GL_UNSIGNED_INT_IMAGE_1D` -> None*/
    UIntImage1D          (GL42.GL_UNSIGNED_INT_IMAGE_1D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_1D"),
    /**`GL_UNSIGNED_INT_IMAGE_2D` -> None*/
    UIntImage2D          (GL42.GL_UNSIGNED_INT_IMAGE_2D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_2D"),
    /**`GL_UNSIGNED_INT_IMAGE_3D` -> None*/
    UIntImage3D          (GL42.GL_UNSIGNED_INT_IMAGE_3D,                   null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_3D"),
    /**`GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE` -> None*/
    UIntImage2DMul       (GL42.GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE,       null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE"),
    /**`GL_UNSIGNED_INT_IMAGE_CUBE` -> None*/
    UIntImageCube        (GL42.GL_UNSIGNED_INT_IMAGE_CUBE,                 null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_CUBE"),
    /**`GL_UNSIGNED_INT_IMAGE_1D_ARRAY` -> None*/
    UIntImage1DArray     (GL42.GL_UNSIGNED_INT_IMAGE_1D_ARRAY,             null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_1D_ARRAY"),
    /**`GL_UNSIGNED_INT_IMAGE_2D_ARRAY` -> None*/
    UIntImage2DShadow    (GL42.GL_UNSIGNED_INT_IMAGE_2D_ARRAY,             null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_2D_ARRAY"),
    /**`GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY` -> None*/
    UIntImageCubeMapArray(GL42.GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY,       null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY"),
    /**`GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY` -> None*/
    UIntImage2DMulArray  (GL42.GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY, null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY"),
    /**`GL_UNSIGNED_INT_IMAGE_BUFFER` -> None*/
    UIntImageBuffer      (GL42.GL_UNSIGNED_INT_IMAGE_BUFFER,               null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_BUFFER"),
    /**`GL_UNSIGNED_INT_IMAGE_2D_RECT` -> None*/
    UIntImage2DRect      (GL42.GL_UNSIGNED_INT_IMAGE_2D_RECT,              null, Int, obj = true, name = "GL_UNSIGNED_INT_IMAGE_2D_RECT"),

    //Legacy
    /**`GL_2_BYTES` -> None*/
    Bytes2(GL11.GL_2_BYTES, null, Byte, 2u, true, "GL_2_BYTES"),
    /**`GL_3_BYTES` -> None*/
    Bytes3(GL11.GL_3_BYTES, null, Byte, 3u, true, "GL_3_BYTES"),
    /**`GL_4_BYTES` -> None*/
    Bytes4(GL11.GL_4_BYTES, null, Byte, 4u, true, "GL_4_BYTES"),

    //Special
    /**`GL_UNSIGNED_BYTE_3_3_2` -> None*/
    UnsRGB332     (GL12.GL_UNSIGNED_BYTE_3_3_2,                              null, UByte,  1u, true, "GL_UNSIGNED_BYTE_3_3_2"),
    /**`GL_UNSIGNED_BYTE_2_3_3_REV` -> None*/
    UnsBGR233     (GL12.GL_UNSIGNED_BYTE_2_3_3_REV,                          null, UByte,  1u, true, "GL_UNSIGNED_BYTE_2_3_3_REV"),
    /**`GL_UNSIGNED_SHORT_5_6_5` -> None*/
    UnsR5G6B5     (GL12.GL_UNSIGNED_SHORT_5_6_5,                             null, UShort, 1u, true, "GL_UNSIGNED_SHORT_5_6_5"),
    /**`GL_UNSIGNED_SHORT_5_6_5_REV` -> None*/
    UnsB5G6R5     (GL12.GL_UNSIGNED_SHORT_5_6_5_REV,                         null, UShort, 1u, true, "GL_UNSIGNED_SHORT_5_6_5_REV"),
    /**`GL_UNSIGNED_SHORT_4_4_4_4` -> None*/
    UnsRGBA4      (GL12.GL_UNSIGNED_SHORT_4_4_4_4,                           null, UShort, 1u, true, "GL_UNSIGNED_SHORT_4_4_4_4"),
    /**`GL_UNSIGNED_SHORT_4_4_4_4_REV` -> None*/
    UnsABGR4      (GL12.GL_UNSIGNED_SHORT_4_4_4_4_REV,                       null, UShort, 1u, true, "GL_UNSIGNED_SHORT_4_4_4_4_REV"),
    /**`GL_UNSIGNED_SHORT_5_5_5_1` -> None*/
    UnsRGB5A1     (GL12.GL_UNSIGNED_SHORT_5_5_5_1,                           null, UShort, 1u, true, "GL_UNSIGNED_SHORT_5_5_5_1"),
    /**`GL_UNSIGNED_SHORT_1_5_5_5_REV` -> None*/
    UnsA1BGR5     (GL12.GL_UNSIGNED_SHORT_1_5_5_5_REV,                       null, UShort, 1u, true, "GL_UNSIGNED_SHORT_1_5_5_5_REV"),
    /**`GL_UNSIGNED_INT_8_8_8_8` -> None*/
    UnsRGBA8      (GL12.GL_UNSIGNED_INT_8_8_8_8,                             null, UInt,   1u, true, "GL_UNSIGNED_INT_8_8_8_8"),
    /**`GL_UNSIGNED_INT_8_8_8_8_REV` -> None*/
    UnsABGR8      (GL12.GL_UNSIGNED_INT_8_8_8_8_REV,                         null, UInt,   1u, true, "GL_UNSIGNED_INT_8_8_8_8_REV"),
    /**`GL_UNSIGNED_INT_10_10_10_2` -> None*/
    UnsRGB10A2    (GL12.GL_UNSIGNED_INT_10_10_10_2,                          null, UInt,   1u, true, "GL_UNSIGNED_INT_10_10_10_2"),
    /**`GL_UNSIGNED_INT_2_10_10_10_REV` -> None*/
    UnsA2BGR10    (GL12.GL_UNSIGNED_INT_2_10_10_10_REV,                      null, UInt,   1u, true, "GL_UNSIGNED_INT_2_10_10_10_REV"),
    /**`GL_UNSIGNED_INT_10F_11F_11F_REV` -> None*/
    UnsB10G11R11F (GL30.GL_UNSIGNED_INT_10F_11F_11F_REV,                     null, UInt,   1u, true, "GL_UNSIGNED_INT_10F_11F_11F_REV"),
    /**`GL_UNSIGNED_INT_5_9_9_9_REV` -> None*/
    UnsA5BGR9     (GL30.GL_UNSIGNED_INT_5_9_9_9_REV,                         null, UInt,   1u, true, "GL_UNSIGNED_INT_5_9_9_9_REV"),
    /**`GL_INT_2_10_10_10_REV` -> None*/
    SigA2BGR10    (GL33.GL_INT_2_10_10_10_REV,                               null, Int,    1u, true, "GL_INT_2_10_10_10_REV"),
    /**`GL_UNSIGNED_INT_10_10_10_2_OES` -> None*/
    UnsRGB10A2OES (OESVertexType1010102.GL_UNSIGNED_INT_10_10_10_2_OES, null, UInt,   1u, true, "GL_UNSIGNED_INT_10_10_10_2_OES"),
    /**`GL_INT_10_10_10_2_OES` -> None*/
    SigRGB10A2OES (OESVertexType1010102.GL_INT_10_10_10_2_OES,          null, Int,    1u, true, "GL_INT_10_10_10_2_OES"),
    ;

    val gl: Int
    val klass: KClass<*>?
    val primitive: GLSLVar
    val primitivesCount: UInt
    val isAttribute: Boolean
    val glName: String
    val isObject: Boolean
    private var bCou = 0u
    constructor(gl: Int, klass: KClass<*>?, primitive: GLSLVar? = null, primitivesCount: UInt = 1u, attr: Boolean = false, name: String, obj: Boolean = false) {
        this.gl = gl
        this.klass = klass
        this.primitive = primitive ?: this
        this.primitivesCount = primitivesCount
        isAttribute = attr
        glName = name
        isObject = obj
    }
    val byteCount: UInt get() {
        if(bCou > 0u)
            return bCou
        bCou = when(primitive) {
            Boolean -> 1u
            Byte -> 1u
            UByte -> 1u
            Short -> 2u
            UShort -> 2u
            Int -> 4u
            UInt -> 4u
            Long -> 8u
            ULong -> 8u
            Float -> 4u
            Double -> 8u
            else -> 1u
        } * primitivesCount
        return bCou
    }
    val isPrimitive : Boolean get() = this === primitive

    companion object {
        private val classSupp = lazy { entries.filter { it.klass != null }.map { it.klass!! }}

        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(klass: KClass<*>) = entries.find { it.klass == klass }

        val supportedClasses = classSupp.value
    }
}