@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import org.joml.*

fun Vector2d.toPair() = Pair(this.x, this.y)
fun Pair<Double, Double>.toVector() = Vector2d(this.first, this.second)

// ===================================== XY =====================================
val Vector2d.xx get() = Vector2d(this.x, this.x)
var Vector2d.xy get() = Vector2d(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2d.yx get() = Vector2d(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2d.yy get() = Vector2d(this.y, this.y)

val Vector2d.xxx get() = Vector3d(this.x, this.x, this.x)
val Vector2d.xxy get() = Vector3d(this.x, this.x, this.y)
val Vector2d.xyx get() = Vector3d(this.x, this.y, this.x)
val Vector2d.xyy get() = Vector3d(this.x, this.y, this.y)
val Vector2d.yxx get() = Vector3d(this.y, this.x, this.x)
val Vector2d.yxy get() = Vector3d(this.y, this.x, this.y)
val Vector2d.yyx get() = Vector3d(this.y, this.y, this.x)
val Vector2d.yyy get() = Vector3d(this.y, this.y, this.y)

val Vector2d.xxxx get() = Vector4d(this.x, this.x, this.x, this.x)
val Vector2d.xxxy get() = Vector4d(this.x, this.x, this.x, this.y)
val Vector2d.xxyx get() = Vector4d(this.x, this.x, this.y, this.x)
val Vector2d.xxyy get() = Vector4d(this.x, this.x, this.y, this.y)
val Vector2d.xyxx get() = Vector4d(this.x, this.y, this.x, this.x)
val Vector2d.xyxy get() = Vector4d(this.x, this.y, this.x, this.y)
val Vector2d.xyyx get() = Vector4d(this.x, this.y, this.y, this.x)
val Vector2d.xyyy get() = Vector4d(this.x, this.y, this.y, this.y)
val Vector2d.yxxx get() = Vector4d(this.y, this.x, this.x, this.x)
val Vector2d.yxxy get() = Vector4d(this.y, this.x, this.x, this.y)
val Vector2d.yxyx get() = Vector4d(this.y, this.x, this.y, this.x)
val Vector2d.yxyy get() = Vector4d(this.y, this.x, this.y, this.y)
val Vector2d.yyxx get() = Vector4d(this.y, this.y, this.x, this.x)
val Vector2d.yyxy get() = Vector4d(this.y, this.y, this.x, this.y)
val Vector2d.yyyx get() = Vector4d(this.y, this.y, this.y, this.x)
val Vector2d.yyyy get() = Vector4d(this.y, this.y, this.y, this.y)

// ===================================== RG =====================================
var Vector2d.r: Double get() = this.x; set(it) { this.x = it }
var Vector2d.g: Double get() = this.y; set(it) { this.y = it }

val Vector2d.rr get() = Vector2d(this.x, this.x)
var Vector2d.rg get() = Vector2d(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2d.gr get() = Vector2d(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2d.gg get() = Vector2d(this.y, this.y)

val Vector2d.rrr get() = Vector3d(this.x, this.x, this.x)
val Vector2d.rrg get() = Vector3d(this.x, this.x, this.y)
val Vector2d.rgr get() = Vector3d(this.x, this.y, this.x)
val Vector2d.rgg get() = Vector3d(this.x, this.y, this.y)
val Vector2d.grr get() = Vector3d(this.y, this.x, this.x)
val Vector2d.grg get() = Vector3d(this.y, this.x, this.y)
val Vector2d.ggr get() = Vector3d(this.y, this.y, this.x)
val Vector2d.ggg get() = Vector3d(this.y, this.y, this.y)

val Vector2d.rrrr get() = Vector4d(this.x, this.x, this.x, this.x)
val Vector2d.rrrg get() = Vector4d(this.x, this.x, this.x, this.y)
val Vector2d.rrgr get() = Vector4d(this.x, this.x, this.y, this.x)
val Vector2d.rrgg get() = Vector4d(this.x, this.x, this.y, this.y)
val Vector2d.rgrr get() = Vector4d(this.x, this.y, this.x, this.x)
val Vector2d.rgrg get() = Vector4d(this.x, this.y, this.x, this.y)
val Vector2d.rggr get() = Vector4d(this.x, this.y, this.y, this.x)
val Vector2d.rggg get() = Vector4d(this.x, this.y, this.y, this.y)
val Vector2d.grrr get() = Vector4d(this.y, this.x, this.x, this.x)
val Vector2d.grrg get() = Vector4d(this.y, this.x, this.x, this.y)
val Vector2d.grgr get() = Vector4d(this.y, this.x, this.y, this.x)
val Vector2d.grgg get() = Vector4d(this.y, this.x, this.y, this.y)
val Vector2d.ggrr get() = Vector4d(this.y, this.y, this.x, this.x)
val Vector2d.ggrg get() = Vector4d(this.y, this.y, this.x, this.y)
val Vector2d.gggr get() = Vector4d(this.y, this.y, this.y, this.x)
val Vector2d.gggg get() = Vector4d(this.y, this.y, this.y, this.y)