@file:JvmName("CStreams")
package com.losi.create.utility

import java.io.InputStream

fun InputStream.readAsString(): String {
    return this.bufferedReader().readText()
}