@file:JvmName("CStreams")
package com.losi.create.utility

import org.joml.Matrix4f
import java.io.InputStream

fun InputStream.readAsString(): String {
    return this.bufferedReader().readText()
}