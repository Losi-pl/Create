@file:JvmName("GL43Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import org.lwjgl.opengl.GL43C
import org.lwjgl.system.MemoryUtil

fun glDebugMessageCallback(callback: (DebugMessageSource, DebugMessageType, DebugMessageId, DebugMessageSeverity, message: String) -> Unit) {
    GL43C.glDebugMessageCallback({ source, type, id, severity, length, message, _ ->
        callback(DebugMessageSource.of(source)?: throw Exception("Unknown Debug Message Source: $source"),
                 DebugMessageType.of(type)?: throw Exception("Unknown Debug Message Type: $type"),
                 DebugMessageId(id),
                 DebugMessageSeverity.of(severity)?: throw Exception("Unknown Debug Message Severity: $severity"),
       org.lwjgl.opengl.GLDebugMessageCallback.getMessage(length, message))
    }, MemoryUtil.NULL)
}