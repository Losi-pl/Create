@file:Suppress("SpellCheckingInspection")

package com.losi.create.utility.joml

import org.joml.Matrix4f

fun Matrix4f.setOrtho(width: Int, height: Int): Matrix4f {
    var ratio = width / height.toFloat()
    if(ratio < 1f)
    {
        ratio = height / width.toFloat()
        return this.setOrtho(-1f, 1f, -ratio, ratio, -1f, 1f)
    }
    return this.setOrtho(-ratio, ratio, -1f, 1f, -1f, 1f)
}