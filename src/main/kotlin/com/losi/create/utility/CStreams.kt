@file:JvmName("CStreams")
package com.losi.create.utility

import java.io.InputStream
import java.io.OutputStream
import java.lang.ref.Cleaner

/**A shortcut to read an [InputStream] as a [String]*/
fun InputStream.readAsString(): String {
    return this.bufferedReader().readText()
}

private val streamCloser = Cleaner.create()
/**Makes a wrapper around a [InputStream] ensuring that if the reference to the stream is lost the stream will close itself*/
fun InputStream.autoClosable() = object : InputStream() {
    private val closer = streamCloser.register(this, this@autoClosable::close)

    override fun available() = this@autoClosable.available()
    override fun read() = this@autoClosable.read()
    override fun read(b: ByteArray, off: Int, len: Int) = this@autoClosable.read(b, off, len)
    override fun readAllBytes() = this@autoClosable.readAllBytes()
    override fun readNBytes(b: ByteArray, off: Int, len: Int) = this@autoClosable.readNBytes(b, off, len)
    override fun readNBytes(len: Int) = this@autoClosable.readNBytes(len)
    override fun skip(n: Long) = this@autoClosable.skip(n)
    override fun skipNBytes(n: Long) = this@autoClosable.skipNBytes(n)
    override fun transferTo(out: OutputStream?) = this@autoClosable.transferTo(out)
    override fun close() = closer.clean()
}