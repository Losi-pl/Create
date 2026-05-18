@file:JvmName("CLists")
package com.losi.create.utility

import java.util.Collections

fun <T> List<T>.calcify(): List<T> = Collections.unmodifiableList(this)
fun <T> Map<String, T>.calcify(): Map<String, T> = Collections.unmodifiableMap(this)