package com.losi.create.graphics.gl

@JvmInline @Suppress("unused")
value class KeyboardKey(val scanCode: Int) {
    val keyCode: KeyboardKeyCode? get() = KeyboardKeyCode.entries.find { it.scanCode == scanCode }?.let { if(it == KeyboardKeyCode.UNKNOWN) null else it }
    val isAll: Boolean get() = scanCode == 0

    constructor(key: KeyboardKeyCode): this(key.scanCode)

    infix fun isKey(key: KeyboardKeyCode): Boolean = scanCode == key.scanCode
}