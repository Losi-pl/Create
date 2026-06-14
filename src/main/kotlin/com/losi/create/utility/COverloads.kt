@file:Suppress("unused")
package com.losi.create.utility

import java.nio.ByteBuffer
import org.joml.*

fun ByteBuffer.putVector2L(v: Vector2L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector2L(index: Int, v: Vector2L): ByteBuffer = v.get(index, this)
fun ByteBuffer.putVector3L(v: Vector3L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector3L(index: Int, v: Vector3L): ByteBuffer = v.get(index, this)
fun ByteBuffer.putVector4L(v: Vector4L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector4L(index: Int, v: Vector4L): ByteBuffer = v.get(index, this)