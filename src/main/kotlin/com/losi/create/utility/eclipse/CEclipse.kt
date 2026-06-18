@file:Suppress("unused", "PackageDirectoryMismatch")

package com.losi.create.utility

fun IntArray.toEcIterable(): org.eclipse.collections.api.IntIterable = CrIntCollect.wrapper(this)
fun org.eclipse.collections.api.list.primitive.MutableIntList.addAll(array: IntArray) = CrIntCollect.addAll(this, array)
fun org.eclipse.collections.api.list.primitive.MutableFloatList.addAll(array: FloatArray) = CrIntCollect.addAll(this, array)