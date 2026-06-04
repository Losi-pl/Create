@file:Suppress("unused", "RemoveRedundantQualifierName")
package com.losi.create.graphics.gl

import org.lwjgl.opengl.*
import org.lwjgl.opengles.OESVertexType1010102
import kotlin.reflect.KClass

enum class GLSLVar {
    // Primitives
    Boolean(GL20.GL_BOOL,                                  kotlin.Boolean::class,             name = "GL_BOOL"),
    Byte   (GL11.GL_BYTE,                                  kotlin.Byte::class,   attr = true, name = "GL_BYTE"),
    UByte  (GL11.GL_UNSIGNED_BYTE,                         kotlin.UByte::class,  attr = true, name = "GL_UNSIGNED_BYTE"),
    Short  (GL11.GL_SHORT,                                 kotlin.Short::class,  attr = true, name = "GL_SHORT"),
    UShort (GL11.GL_UNSIGNED_SHORT,                        kotlin.UShort::class, attr = true, name = "GL_UNSIGNED_SHORT"),
    Int    (GL11.GL_INT,                                   kotlin.Int::class,    attr = true, name = "GL_INT"),
    UInt   (GL11.GL_UNSIGNED_INT,                          kotlin.UInt::class,   attr = true, name = "GL_UNSIGNED_INT"),
    Long   (ARBGPUShaderInt64.GL_INT64_ARB,           kotlin.Long::class,   attr = true, name = "GL_INT64_ARB"),
    ULong  (ARBGPUShaderInt64.GL_UNSIGNED_INT64_ARB,  kotlin.ULong::class,  attr = true, name = "GL_UNSIGNED_INT64_ARB"),
    Float  (GL11.GL_FLOAT,                                 kotlin.Float::class,  attr = true, name = "GL_FLOAT"),
    Double (GL11.GL_DOUBLE,                                kotlin.Double::class, attr = true, name = "GL_DOUBLE"),

    // Atomic
    AtomicUInt(GL42.GL_UNSIGNED_INT_ATOMIC_COUNTER , kotlin.UInt::class, UInt, name = "GL_UNSIGNED_INT_ATOMIC_COUNTER"),

    // Bool Vectors
    Vector2b(GL20.GL_BOOL_VEC2, com.losi.create.math.Vector2b::class, Boolean, 2u, name = "GL_BOOL_VEC2"),
    Vector3b(GL20.GL_BOOL_VEC3, com.losi.create.math.Vector3b::class, Boolean, 3u, name = "GL_BOOL_VEC3"),
    Vector4b(GL20.GL_BOOL_VEC4, com.losi.create.math.Vector4b::class, Boolean, 4u, name = "GL_BOOL_VEC4"),

    // Int Vectors
    Vector2i(GL20.GL_INT_VEC2, org.joml.Vector2i::class, Int, 2u, true, name = "GL_INT_VEC2"),
    Vector3i(GL20.GL_INT_VEC3, org.joml.Vector3i::class, Int, 3u, true, name = "GL_INT_VEC3"),
    Vector4i(GL20.GL_INT_VEC4, org.joml.Vector4i::class, Int, 4u, true, name = "GL_INT_VEC4"),

    // UInt Vectors
    Vector2ui(GL30.GL_UNSIGNED_INT_VEC2, null, UInt, 2u, true, name = "GL_UNSIGNED_INT_VEC2"),
    Vector3ui(GL30.GL_UNSIGNED_INT_VEC3, null, UInt, 3u, true, name = "GL_UNSIGNED_INT_VEC3"),
    Vector4ui(GL30.GL_UNSIGNED_INT_VEC4, null, UInt, 4u, true, name = "GL_UNSIGNED_INT_VEC4"),

    // Long Vectors
    Vector2l(ARBGPUShaderInt64.GL_INT64_VEC2_ARB, org.joml.Vector2L::class, Long, 2u, true, name = "GL_INT64_VEC2_ARB"),
    Vector3l(ARBGPUShaderInt64.GL_INT64_VEC3_ARB, org.joml.Vector3L::class, Long, 3u, true, name = "GL_INT64_VEC3_ARB"),
    Vector4l(ARBGPUShaderInt64.GL_INT64_VEC4_ARB, org.joml.Vector4L::class, Long, 4u, true, name = "GL_INT64_VEC4_ARB"),

    // ULong Vectors
    Vector2ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC2_ARB, null, ULong, 2u, true, name = "GL_UNSIGNED_INT64_VEC2_ARB"),
    Vector3ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC3_ARB, null, ULong, 3u, true, name = "GL_UNSIGNED_INT64_VEC3_ARB"),
    Vector4ul(ARBGPUShaderInt64.GL_UNSIGNED_INT64_VEC4_ARB, null, ULong, 4u, true, name = "GL_UNSIGNED_INT64_VEC4_ARB"),

    // Float Vectors
    Vector2f(GL20.GL_FLOAT_VEC2, org.joml.Vector2f::class, Float, 2u, true, name = "GL_FLOAT_VEC2"),
    Vector3f(GL20.GL_FLOAT_VEC3, org.joml.Vector3f::class, Float, 3u, true, name = "GL_FLOAT_VEC3"),
    Vector4f(GL20.GL_FLOAT_VEC4, org.joml.Vector4f::class, Float, 4u, true, name = "GL_FLOAT_VEC4"),

    //Double Vectors
    Vector2d(GL40.GL_DOUBLE_VEC2, org.joml.Vector2d::class, Double, 2u, true, name = "GL_DOUBLE_VEC2"),
    Vector3d(GL40.GL_DOUBLE_VEC3, org.joml.Vector3d::class, Double, 3u, true, name = "GL_DOUBLE_VEC3"),
    Vector4d(GL40.GL_DOUBLE_VEC4, org.joml.Vector4d::class, Double, 4u, true, name = "GL_DOUBLE_VEC4"),

    // Float Matrix
    Matrix4f  (GL20.GL_FLOAT_MAT4,   org.joml.Matrix4f::class,   Float, 4u * 4u, true, "GL_FLOAT_MAT4"),
    Matrix4x3f(GL21.GL_FLOAT_MAT4x3, org.joml.Matrix4x3f::class, Float, 4u * 3u, true, "GL_FLOAT_MAT4x3"),
    Matrix4x2f(GL21.GL_FLOAT_MAT4x2, null,                       Float, 4u * 2u, true, "GL_FLOAT_MAT4x2"),
    Matrix3x4f(GL21.GL_FLOAT_MAT3x4, null,                       Float, 3u * 4u, true, "GL_FLOAT_MAT3x4"),
    Matrix3f  (GL20.GL_FLOAT_MAT3,   org.joml.Matrix3f::class,   Float, 3u * 3u, true, "GL_FLOAT_MAT3"),
    Matrix3x2f(GL21.GL_FLOAT_MAT3x2, org.joml.Matrix3x2f::class, Float, 3u * 2u, true, "GL_FLOAT_MAT3x2"),
    Matrix2x4f(GL21.GL_FLOAT_MAT2x4, null,                       Float, 2u * 4u, true, "GL_FLOAT_MAT2x4"),
    Matrix2x3f(GL21.GL_FLOAT_MAT2x3, null,                       Float, 2u * 3u, true, "GL_FLOAT_MAT2x3"),
    Matrix2f  (GL20.GL_FLOAT_MAT2,   org.joml.Matrix2f::class,   Float, 2u * 2u, true, "GL_FLOAT_MAT2"),

    // Double Matrix
    Matrix4d  (GL40.GL_DOUBLE_MAT4,   org.joml.Matrix4d::class,   Double, 4u * 4u, true, "GL_DOUBLE_MAT4"),
    Matrix4x3d(GL41.GL_DOUBLE_MAT4x3, org.joml.Matrix4x3d::class, Double, 4u * 3u, true, "GL_DOUBLE_MAT4x3"),
    Matrix4x2d(GL41.GL_DOUBLE_MAT4x2, null,                       Double, 4u * 2u, true, "GL_DOUBLE_MAT4x2"),
    Matrix3x4d(GL41.GL_DOUBLE_MAT3x4, null,                       Double, 3u * 4u, true, "GL_DOUBLE_MAT3x4"),
    Matrix3d  (GL40.GL_DOUBLE_MAT3,   org.joml.Matrix3d::class,   Double, 3u * 3u, true, "GL_DOUBLE_MAT3"),
    Matrix3x2d(GL41.GL_DOUBLE_MAT3x2, org.joml.Matrix3x2d::class, Double, 3u * 2u, true, "GL_DOUBLE_MAT3x2"),
    Matrix2x4d(GL41.GL_DOUBLE_MAT2x4, null,                       Double, 2u * 4u, true, "GL_DOUBLE_MAT2x4"),
    Matrix2x3d(GL41.GL_DOUBLE_MAT2x3, null,                       Double, 2u * 3u, true, "GL_DOUBLE_MAT2x3"),
    Matrix2d  (GL40.GL_DOUBLE_MAT2,   org.joml.Matrix2d::class,   Double, 2u * 2u, true, "GL_DOUBLE_MAT2"),

    // Regular Samplers
    Sampler1D                (GL20.GL_SAMPLER_1D,                    null, Int, name = "GL_SAMPLER_1D"),
    Sampler2D                (GL20.GL_SAMPLER_2D,                    null, Int, name = "GL_SAMPLER_2D"),
    Sampler3D                (GL20.GL_SAMPLER_3D,                    null, Int, name = "GL_SAMPLER_3D"),
    Sampler2DMul             (GL32.GL_SAMPLER_2D_MULTISAMPLE,        null, Int, name = "GL_SAMPLER_2D_MULTISAMPLE"),
    SamplerCube              (GL20.GL_SAMPLER_CUBE,                  null, Int, name = "GL_SAMPLER_CUBE"),
    SamplerBuffer            (GL31.GL_SAMPLER_BUFFER,                null, Int, name = "GL_SAMPLER_BUFFER"),
    Sampler2DRect            (GL31.GL_SAMPLER_2D_RECT,               null, Int, name = "GL_SAMPLER_2D_RECT"),
    Sampler1DShadow          (GL20.GL_SAMPLER_1D_SHADOW,             null, Int, name = "GL_SAMPLER_1D_SHADOW"),
    Sampler2DShadow          (GL20.GL_SAMPLER_2D_SHADOW,             null, Int, name = "GL_SAMPLER_2D_SHADOW"),
    SamplerCubeShadow        (GL30.GL_SAMPLER_CUBE_SHADOW,           null, Int, name = "GL_SAMPLER_CUBE_SHADOW"),
    Sampler1DArray           (GL30.GL_SAMPLER_1D_ARRAY,              null, Int, name = "GL_SAMPLER_1D_ARRAY"),
    Sampler2DArray           (GL30.GL_SAMPLER_2D_ARRAY,              null, Int, name = "GL_SAMPLER_2D_ARRAY"),
    Sampler2DMulArray        (GL32.GL_SAMPLER_2D_MULTISAMPLE_ARRAY,  null, Int, name = "GL_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    SamplerCubeArray         (GL40.GL_SAMPLER_CUBE_MAP_ARRAY,        null, Int, name = "GL_SAMPLER_CUBE_MAP_ARRAY"),
    Sampler1DArrayShadow     (GL30.GL_SAMPLER_1D_ARRAY_SHADOW,       null, Int, name = "GL_SAMPLER_1D_ARRAY_SHADOW"),
    Sampler2DArrayShadow     (GL30.GL_SAMPLER_2D_ARRAY_SHADOW,       null, Int, name = "GL_SAMPLER_2D_ARRAY_SHADOW"),
    SamplerCubeMapArrayShadow(GL40.GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW, null, Int, name = "GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW"),
    Sampler2DRectShadow      (GL31.GL_SAMPLER_2D_RECT_SHADOW,        null, Int, name = "GL_SAMPLER_2D_RECT_SHADOW"),

    // Signed Integer Samplers
    IntSampler1D          (GL30.GL_INT_SAMPLER_1D,                   null, Int, name = "GL_INT_SAMPLER_1D"),
    IntSampler2D          (GL30.GL_INT_SAMPLER_2D,                   null, Int, name = "GL_INT_SAMPLER_2D"),
    IntSampler3D          (GL30.GL_INT_SAMPLER_3D,                   null, Int, name = "GL_INT_SAMPLER_3D"),
    IntSampler2DMul       (GL32.GL_INT_SAMPLER_2D_MULTISAMPLE,       null, Int, name = "GL_INT_SAMPLER_2D_MULTISAMPLE"),
    IntSamplerCube        (GL30.GL_INT_SAMPLER_CUBE,                 null, Int, name = "GL_INT_SAMPLER_CUBE"),
    IntSampler1DArray     (GL30.GL_INT_SAMPLER_1D_ARRAY,             null, Int, name = "GL_INT_SAMPLER_1D_ARRAY"),
    IntSampler2DArray     (GL30.GL_INT_SAMPLER_2D_ARRAY,             null, Int, name = "GL_INT_SAMPLER_2D_ARRAY"),
    IntSamplerCubeMapArray(GL40.GL_INT_SAMPLER_CUBE_MAP_ARRAY,       null, Int, name = "GL_INT_SAMPLER_CUBE_MAP_ARRAY"),
    IntSampler2DMulArray  (GL32.GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY, null, Int, name = "GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    IntSamplerBuffer      (GL31.GL_INT_SAMPLER_BUFFER,               null, Int, name = "GL_INT_SAMPLER_BUFFER"),
    IntSampler2DRect      (GL31.GL_INT_SAMPLER_2D_RECT,              null, Int, name = "GL_INT_SAMPLER_2D_RECT"),

    // Unsigned Integer Samplers
    UIntSampler1D          (GL30.GL_UNSIGNED_INT_SAMPLER_1D,                   null, Int, name = "GL_UNSIGNED_INT_SAMPLER_1D"),
    UIntSampler2D          (GL30.GL_UNSIGNED_INT_SAMPLER_2D,                   null, Int, name = "GL_UNSIGNED_INT_SAMPLER_2D"),
    UIntSampler3D          (GL30.GL_UNSIGNED_INT_SAMPLER_3D,                   null, Int, name = "GL_UNSIGNED_INT_SAMPLER_3D"),
    UIntSampler2DMul       (GL32.GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE,       null, Int, name = "GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE"),
    UIntSamplerCube        (GL30.GL_UNSIGNED_INT_SAMPLER_CUBE,                 null, Int, name = "GL_UNSIGNED_INT_SAMPLER_CUBE"),
    UIntSampler1DArray     (GL30.GL_UNSIGNED_INT_SAMPLER_1D_ARRAY,             null, Int, name = "GL_UNSIGNED_INT_SAMPLER_1D_ARRAY"),
    UIntSampler2DShadow    (GL30.GL_UNSIGNED_INT_SAMPLER_2D_ARRAY,             null, Int, name = "GL_UNSIGNED_INT_SAMPLER_2D_ARRAY"),
    UIntSamplerCubeMapArray(GL40.GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY,       null, Int, name = "GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY"),
    UIntSampler2DMulArray  (GL32.GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY, null, Int, name = "GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY"),
    UIntSamplerBuffer      (GL31.GL_UNSIGNED_INT_SAMPLER_BUFFER,               null, Int, name = "GL_UNSIGNED_INT_SAMPLER_BUFFER"),
    UIntSampler2DRect      (GL31.GL_UNSIGNED_INT_SAMPLER_2D_RECT,              null, Int, name = "GL_UNSIGNED_INT_SAMPLER_2D_RECT"),

    // Regular Images
    Image1D                (GL42.GL_IMAGE_1D,                    null, Int, name = "GL_IMAGE_1D"),
    Image2D                (GL42.GL_IMAGE_2D,                    null, Int, name = "GL_IMAGE_2D"),
    Image3D                (GL42.GL_IMAGE_3D,                    null, Int, name = "GL_IMAGE_3D"),
    Image2DMul             (GL42.GL_IMAGE_2D_MULTISAMPLE,        null, Int, name = "GL_IMAGE_2D_MULTISAMPLE"),
    ImageCube              (GL42.GL_IMAGE_CUBE,                  null, Int, name = "GL_IMAGE_CUBE"),
    ImageBuffer            (GL42.GL_IMAGE_BUFFER,                null, Int, name = "GL_IMAGE_BUFFER"),
    Image2DRect            (GL42.GL_IMAGE_2D_RECT,               null, Int, name = "GL_IMAGE_2D_RECT"),
    Image1DArray           (GL42.GL_IMAGE_1D_ARRAY,              null, Int, name = "GL_IMAGE_1D_ARRAY"),
    Image2DArray           (GL42.GL_IMAGE_2D_ARRAY,              null, Int, name = "GL_IMAGE_2D_ARRAY"),
    Image2DMulArray        (GL42.GL_IMAGE_2D_MULTISAMPLE_ARRAY,  null, Int, name = "GL_IMAGE_2D_MULTISAMPLE_ARRAY"),
    ImageCubeArray         (GL42.GL_IMAGE_CUBE_MAP_ARRAY,        null, Int, name = "GL_IMAGE_CUBE_MAP_ARRAY"),

    // Signed Integer Images
    IntImage1D          (GL42.GL_INT_IMAGE_1D,                   null, Int, name = "GL_INT_IMAGE_1D"),
    IntImage2D          (GL43.GL_INT_IMAGE_2D,                   null, Int, name = "GL_INT_IMAGE_2D"),
    IntImage3D          (GL42.GL_INT_IMAGE_3D,                   null, Int, name = "GL_INT_IMAGE_3D"),
    IntImage2DMul       (GL42.GL_INT_IMAGE_2D_MULTISAMPLE,       null, Int, name = "GL_INT_IMAGE_2D_MULTISAMPLE"),
    IntImageCube        (GL42.GL_INT_IMAGE_CUBE,                 null, Int, name = "GL_INT_IMAGE_CUBE"),
    IntImage1DArray     (GL42.GL_INT_IMAGE_1D_ARRAY,             null, Int, name = "GL_INT_IMAGE_1D_ARRAY"),
    IntImage2DArray     (GL42.GL_INT_IMAGE_2D_ARRAY,             null, Int, name = "GL_INT_IMAGE_2D_ARRAY"),
    IntImageCubeMapArray(GL42.GL_INT_IMAGE_CUBE_MAP_ARRAY,       null, Int, name = "GL_INT_IMAGE_CUBE_MAP_ARRAY"),
    IntImage2DMulArray  (GL42.GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY, null, Int, name = "GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY"),
    IntImageBuffer      (GL42.GL_INT_IMAGE_BUFFER,               null, Int, name = "GL_INT_IMAGE_BUFFER"),
    IntImage2DRect      (GL42.GL_INT_IMAGE_2D_RECT,              null, Int, name = "GL_INT_IMAGE_2D_RECT"),

    // Unsigned Integer Images
    UIntImage1D          (GL42.GL_UNSIGNED_INT_IMAGE_1D,                   null, Int, name = "GL_UNSIGNED_INT_IMAGE_1D"),
    UIntImage2D          (GL42.GL_UNSIGNED_INT_IMAGE_2D,                   null, Int, name = "GL_UNSIGNED_INT_IMAGE_2D"),
    UIntImage3D          (GL42.GL_UNSIGNED_INT_IMAGE_3D,                   null, Int, name = "GL_UNSIGNED_INT_IMAGE_3D"),
    UIntImage2DMul       (GL42.GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE,       null, Int, name = "GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE"),
    UIntImageCube        (GL42.GL_UNSIGNED_INT_IMAGE_CUBE,                 null, Int, name = "GL_UNSIGNED_INT_IMAGE_CUBE"),
    UIntImage1DArray     (GL42.GL_UNSIGNED_INT_IMAGE_1D_ARRAY,             null, Int, name = "GL_UNSIGNED_INT_IMAGE_1D_ARRAY"),
    UIntImage2DShadow    (GL42.GL_UNSIGNED_INT_IMAGE_2D_ARRAY,             null, Int, name = "GL_UNSIGNED_INT_IMAGE_2D_ARRAY"),
    UIntImageCubeMapArray(GL42.GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY,       null, Int, name = "GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY"),
    UIntImage2DMulArray  (GL42.GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY, null, Int, name = "GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY"),
    UIntImageBuffer      (GL42.GL_UNSIGNED_INT_IMAGE_BUFFER,               null, Int, name = "GL_UNSIGNED_INT_IMAGE_BUFFER"),
    UIntImage2DRect      (GL42.GL_UNSIGNED_INT_IMAGE_2D_RECT,              null, Int, name = "GL_UNSIGNED_INT_IMAGE_2D_RECT"),

    //Legacy
    Bytes2(GL11.GL_2_BYTES, null, Byte, 2u, true, "GL_2_BYTES"),
    Bytes3(GL11.GL_3_BYTES, null, Byte, 3u, true, "GL_3_BYTES"),
    Bytes4(GL11.GL_4_BYTES, null, Byte, 4u, true, "GL_4_BYTES"),

    //Special
    UnsignedRGB332     (GL12.GL_UNSIGNED_BYTE_3_3_2,                              null, UByte,  1u, true, "GL_UNSIGNED_BYTE_3_3_2"),
    UnsignedBGR233Rev  (GL12.GL_UNSIGNED_BYTE_2_3_3_REV,                          null, UByte,  1u, true, "GL_UNSIGNED_BYTE_2_3_3_REV"),
    UnsignedRGB565     (GL12.GL_UNSIGNED_SHORT_5_6_5,                             null, UShort, 1u, true, "GL_UNSIGNED_SHORT_5_6_5"),
    UnsignedRGBA4444   (GL12.GL_UNSIGNED_SHORT_4_4_4_4,                           null, UShort, 1u, true, "GL_UNSIGNED_SHORT_4_4_4_4"),
    UnsignedRGBA5551   (GL12.GL_UNSIGNED_SHORT_5_5_5_1,                           null, UShort, 1u, true, "GL_UNSIGNED_SHORT_5_5_5_1"),
    UnsignedRGBA1555Rev(GL12.GL_UNSIGNED_SHORT_1_5_5_5_REV,                       null, UShort, 1u, true, "GL_UNSIGNED_SHORT_1_5_5_5_REV"),
    UnsignedRGBA8888   (GL12.GL_UNSIGNED_INT_8_8_8_8,                             null, UInt,   1u, true, "GL_UNSIGNED_INT_8_8_8_8"),
    UnsignedRGB10A2    (GL12.GL_UNSIGNED_INT_2_10_10_10_REV,                      null, UInt,   1u, true, "GL_UNSIGNED_INT_2_10_10_10_REV"),
    UnsignedR10G11B11F (GL30.GL_UNSIGNED_INT_10F_11F_11F_REV,                     null, UInt,   1u, true, "GL_UNSIGNED_INT_10F_11F_11F_REV"),
    UnsignedRGB999Rev  (GL30.GL_UNSIGNED_INT_5_9_9_9_REV,                         null, UInt,   1u, true, "GL_UNSIGNED_INT_5_9_9_9_REV"),
    SignedRGB10A2      (GL33.GL_INT_2_10_10_10_REV,                               null, Int,    1u, true, "GL_INT_2_10_10_10_REV"),
    UnsignedRGB10A2OES (OESVertexType1010102.GL_UNSIGNED_INT_10_10_10_2_OES, null, UInt,   1u, true, "GL_UNSIGNED_INT_10_10_10_2_OES"),
    SignedRGB10A2OES   (OESVertexType1010102.GL_INT_10_10_10_2_OES,          null, Int,    1u, true, "GL_INT_10_10_10_2_OES"),
    ;

    val gl: Int
    val klass: KClass<*>?
    val primitive: GLSLVar
    val primitivesCount: UInt
    val attributeUsable: Boolean
    val glName: String
    constructor(gl: Int, klass: KClass<*>?, primitive: GLSLVar? = null, primitivesCount: UInt = 1u, attr: Boolean = false, name: String) {
        this.gl = gl
        this.klass = klass
        this.primitive = primitive ?: this
        this.primitivesCount = primitivesCount
        attributeUsable = attr
        glName = name
    }
    val byteCount: UInt get() = when(primitive) {
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

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = GLSLVar.entries.find { it.gl == gl }
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(klass: KClass<*>) = GLSLVar.entries.find { it.klass == klass }
    }
}