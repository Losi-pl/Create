@file:Suppress("unused", "PackageDirectoryMismatch")

package com.losi.create.utility

import java.nio.FloatBuffer
import java.nio.IntBuffer

fun IntArray.toEcIterable(): org.eclipse.collections.api.IntIterable = CrIntCollect.wrapper(this)
fun org.eclipse.collections.api.list.primitive.MutableIntList.addAll(array: IntArray) = CrIntCollect.addAll(this, array)
fun org.eclipse.collections.api.list.primitive.MutableIntList.addAll(buffer: IntBuffer) = CrIntCollect.addAll(this, buffer)
fun org.eclipse.collections.api.list.primitive.MutableFloatList.addAll(array: FloatArray) = CrIntCollect.addAll(this, array)
fun org.eclipse.collections.api.list.primitive.MutableFloatList.addAll(buffer: FloatBuffer) = CrIntCollect.addAll(this, buffer)