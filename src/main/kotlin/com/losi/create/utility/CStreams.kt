@file:JvmName("CStreams")
package com.losi.create.utility

import java.io.InputStream

/**A shortcut to read an [InputStream] as a [String]*/
fun InputStream.readAsString(): String {
    return this.bufferedReader().readText()
}