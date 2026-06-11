@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import org.joml.*

fun Vector2f.toPair() = Pair(this.x, this.y)
fun Pair<Float, Float>.toVector() = Vector2f(this.first, this.second)

// ===================================== XY =====================================
val Vector2f.xx get() = Vector2f(this.x, this.x)
var Vector2f.xy get() = Vector2f(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2f.yx get() = Vector2f(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2f.yy get() = Vector2f(this.y, this.y)

val Vector2f.xxx get() = Vector3f(this.x, this.x, this.x)
val Vector2f.xxy get() = Vector3f(this.x, this.x, this.y)
val Vector2f.xyx get() = Vector3f(this.x, this.y, this.x)
val Vector2f.xyy get() = Vector3f(this.x, this.y, this.y)
val Vector2f.yxx get() = Vector3f(this.y, this.x, this.x)
val Vector2f.yxy get() = Vector3f(this.y, this.x, this.y)
val Vector2f.yyx get() = Vector3f(this.y, this.y, this.x)
val Vector2f.yyy get() = Vector3f(this.y, this.y, this.y)

val Vector2f.xxxx get() = Vector4f(this.x, this.x, this.x, this.x)
val Vector2f.xxxy get() = Vector4f(this.x, this.x, this.x, this.y)
val Vector2f.xxyx get() = Vector4f(this.x, this.x, this.y, this.x)
val Vector2f.xxyy get() = Vector4f(this.x, this.x, this.y, this.y)
val Vector2f.xyxx get() = Vector4f(this.x, this.y, this.x, this.x)
val Vector2f.xyxy get() = Vector4f(this.x, this.y, this.x, this.y)
val Vector2f.xyyx get() = Vector4f(this.x, this.y, this.y, this.x)
val Vector2f.xyyy get() = Vector4f(this.x, this.y, this.y, this.y)
val Vector2f.yxxx get() = Vector4f(this.y, this.x, this.x, this.x)
val Vector2f.yxxy get() = Vector4f(this.y, this.x, this.x, this.y)
val Vector2f.yxyx get() = Vector4f(this.y, this.x, this.y, this.x)
val Vector2f.yxyy get() = Vector4f(this.y, this.x, this.y, this.y)
val Vector2f.yyxx get() = Vector4f(this.y, this.y, this.x, this.x)
val Vector2f.yyxy get() = Vector4f(this.y, this.y, this.x, this.y)
val Vector2f.yyyx get() = Vector4f(this.y, this.y, this.y, this.x)
val Vector2f.yyyy get() = Vector4f(this.y, this.y, this.y, this.y)

// ===================================== RG =====================================
var Vector2f.r: Float get() = this.x; set(it) { this.x = it }
var Vector2f.g: Float get() = this.y; set(it) { this.y = it }

val Vector2f.rr get() = Vector2f(this.x, this.x)
var Vector2f.rg get() = Vector2f(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2f.gr get() = Vector2f(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2f.gg get() = Vector2f(this.y, this.y)

val Vector2f.rrr get() = Vector3f(this.x, this.x, this.x)
val Vector2f.rrg get() = Vector3f(this.x, this.x, this.y)
val Vector2f.rgr get() = Vector3f(this.x, this.y, this.x)
val Vector2f.rgg get() = Vector3f(this.x, this.y, this.y)
val Vector2f.grr get() = Vector3f(this.y, this.x, this.x)
val Vector2f.grg get() = Vector3f(this.y, this.x, this.y)
val Vector2f.ggr get() = Vector3f(this.y, this.y, this.x)
val Vector2f.ggg get() = Vector3f(this.y, this.y, this.y)

val Vector2f.rrrr get() = Vector4f(this.x, this.x, this.x, this.x)
val Vector2f.rrrg get() = Vector4f(this.x, this.x, this.x, this.y)
val Vector2f.rrgr get() = Vector4f(this.x, this.x, this.y, this.x)
val Vector2f.rrgg get() = Vector4f(this.x, this.x, this.y, this.y)
val Vector2f.rgrr get() = Vector4f(this.x, this.y, this.x, this.x)
val Vector2f.rgrg get() = Vector4f(this.x, this.y, this.x, this.y)
val Vector2f.rggr get() = Vector4f(this.x, this.y, this.y, this.x)
val Vector2f.rggg get() = Vector4f(this.x, this.y, this.y, this.y)
val Vector2f.grrr get() = Vector4f(this.y, this.x, this.x, this.x)
val Vector2f.grrg get() = Vector4f(this.y, this.x, this.x, this.y)
val Vector2f.grgr get() = Vector4f(this.y, this.x, this.y, this.x)
val Vector2f.grgg get() = Vector4f(this.y, this.x, this.y, this.y)
val Vector2f.ggrr get() = Vector4f(this.y, this.y, this.x, this.x)
val Vector2f.ggrg get() = Vector4f(this.y, this.y, this.x, this.y)
val Vector2f.gggr get() = Vector4f(this.y, this.y, this.y, this.x)
val Vector2f.gggg get() = Vector4f(this.y, this.y, this.y, this.y)