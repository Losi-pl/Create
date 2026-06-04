@file:Suppress("unused")
package com.losi.create.utility

import com.koloboke.collect.set.LongSet
import java.nio.ByteBuffer
import org.joml.*

/**Adds the [value] to the set
 *
 * Apparently the interface has two methods that in the Kotlin interpretation come down to the same thing but
 * instead of just choosing one over the other, Kotlin will refuse to choose one and return a compilation error.*/
internal fun LongSet.forceAdd(value: Long) = COverloads.add(this, value)

fun ByteBuffer.putVector2L(v: Vector2L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector2L(index: Int, v: Vector2L): ByteBuffer = v.get(index, this)
fun ByteBuffer.putVector3L(v: Vector3L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector3L(index: Int, v: Vector3L): ByteBuffer = v.get(index, this)
fun ByteBuffer.putVector4L(v: Vector4L): ByteBuffer = v.get(this)
fun ByteBuffer.putVector4L(index: Int, v: Vector4L): ByteBuffer = v.get(index, this)