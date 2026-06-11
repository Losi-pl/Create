@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import org.joml.*

fun Vector2l.toPair() = Pair(this.x, this.y)
fun Pair<Long, Long>.toVector() = Vector2l(this.first, this.second)

// ===================================== XY =====================================
val Vector2l.xx get() = Vector2l(this.x, this.x)
var Vector2l.xy get() = Vector2l(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2l.yx get() = Vector2l(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2l.yy get() = Vector2l(this.y, this.y)

val Vector2l.xxx get() = Vector3l(this.x, this.x, this.x)
val Vector2l.xxy get() = Vector3l(this.x, this.x, this.y)
val Vector2l.xyx get() = Vector3l(this.x, this.y, this.x)
val Vector2l.xyy get() = Vector3l(this.x, this.y, this.y)
val Vector2l.yxx get() = Vector3l(this.y, this.x, this.x)
val Vector2l.yxy get() = Vector3l(this.y, this.x, this.y)
val Vector2l.yyx get() = Vector3l(this.y, this.y, this.x)
val Vector2l.yyy get() = Vector3l(this.y, this.y, this.y)

val Vector2l.xxxx get() = Vector4l(this.x, this.x, this.x, this.x)
val Vector2l.xxxy get() = Vector4l(this.x, this.x, this.x, this.y)
val Vector2l.xxyx get() = Vector4l(this.x, this.x, this.y, this.x)
val Vector2l.xxyy get() = Vector4l(this.x, this.x, this.y, this.y)
val Vector2l.xyxx get() = Vector4l(this.x, this.y, this.x, this.x)
val Vector2l.xyxy get() = Vector4l(this.x, this.y, this.x, this.y)
val Vector2l.xyyx get() = Vector4l(this.x, this.y, this.y, this.x)
val Vector2l.xyyy get() = Vector4l(this.x, this.y, this.y, this.y)
val Vector2l.yxxx get() = Vector4l(this.y, this.x, this.x, this.x)
val Vector2l.yxxy get() = Vector4l(this.y, this.x, this.x, this.y)
val Vector2l.yxyx get() = Vector4l(this.y, this.x, this.y, this.x)
val Vector2l.yxyy get() = Vector4l(this.y, this.x, this.y, this.y)
val Vector2l.yyxx get() = Vector4l(this.y, this.y, this.x, this.x)
val Vector2l.yyxy get() = Vector4l(this.y, this.y, this.x, this.y)
val Vector2l.yyyx get() = Vector4l(this.y, this.y, this.y, this.x)
val Vector2l.yyyy get() = Vector4l(this.y, this.y, this.y, this.y)

// ===================================== RG =====================================
var Vector2l.r: Long get() = this.x; set(it) { this.x = it }
var Vector2l.g: Long get() = this.y; set(it) { this.y = it }

val Vector2l.rr get() = Vector2l(this.x, this.x)
var Vector2l.rg get() = Vector2l(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2l.gr get() = Vector2l(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2l.gg get() = Vector2l(this.y, this.y)

val Vector2l.rrr get() = Vector3l(this.x, this.x, this.x)
val Vector2l.rrg get() = Vector3l(this.x, this.x, this.y)
val Vector2l.rgr get() = Vector3l(this.x, this.y, this.x)
val Vector2l.rgg get() = Vector3l(this.x, this.y, this.y)
val Vector2l.grr get() = Vector3l(this.y, this.x, this.x)
val Vector2l.grg get() = Vector3l(this.y, this.x, this.y)
val Vector2l.ggr get() = Vector3l(this.y, this.y, this.x)
val Vector2l.ggg get() = Vector3l(this.y, this.y, this.y)

val Vector2l.rrrr get() = Vector4l(this.x, this.x, this.x, this.x)
val Vector2l.rrrg get() = Vector4l(this.x, this.x, this.x, this.y)
val Vector2l.rrgr get() = Vector4l(this.x, this.x, this.y, this.x)
val Vector2l.rrgg get() = Vector4l(this.x, this.x, this.y, this.y)
val Vector2l.rgrr get() = Vector4l(this.x, this.y, this.x, this.x)
val Vector2l.rgrg get() = Vector4l(this.x, this.y, this.x, this.y)
val Vector2l.rggr get() = Vector4l(this.x, this.y, this.y, this.x)
val Vector2l.rggg get() = Vector4l(this.x, this.y, this.y, this.y)
val Vector2l.grrr get() = Vector4l(this.y, this.x, this.x, this.x)
val Vector2l.grrg get() = Vector4l(this.y, this.x, this.x, this.y)
val Vector2l.grgr get() = Vector4l(this.y, this.x, this.y, this.x)
val Vector2l.grgg get() = Vector4l(this.y, this.x, this.y, this.y)
val Vector2l.ggrr get() = Vector4l(this.y, this.y, this.x, this.x)
val Vector2l.ggrg get() = Vector4l(this.y, this.y, this.x, this.y)
val Vector2l.gggr get() = Vector4l(this.y, this.y, this.y, this.x)
val Vector2l.gggg get() = Vector4l(this.y, this.y, this.y, this.y)