@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import org.joml.*

fun Vector2i.toPair() = Pair(this.x, this.y)
fun Pair<Int, Int>.toVector() = Vector2i(this.first, this.second)

// ===================================== XY =====================================
val Vector2i.xx get() = Vector2i(this.x, this.x)
var Vector2i.xy get() = Vector2i(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2i.yx get() = Vector2i(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2i.yy get() = Vector2i(this.y, this.y)

val Vector2i.xxx get() = Vector3i(this.x, this.x, this.x)
val Vector2i.xxy get() = Vector3i(this.x, this.x, this.y)
val Vector2i.xyx get() = Vector3i(this.x, this.y, this.x)
val Vector2i.xyy get() = Vector3i(this.x, this.y, this.y)
val Vector2i.yxx get() = Vector3i(this.y, this.x, this.x)
val Vector2i.yxy get() = Vector3i(this.y, this.x, this.y)
val Vector2i.yyx get() = Vector3i(this.y, this.y, this.x)
val Vector2i.yyy get() = Vector3i(this.y, this.y, this.y)

val Vector2i.xxxx get() = Vector4i(this.x, this.x, this.x, this.x)
val Vector2i.xxxy get() = Vector4i(this.x, this.x, this.x, this.y)
val Vector2i.xxyx get() = Vector4i(this.x, this.x, this.y, this.x)
val Vector2i.xxyy get() = Vector4i(this.x, this.x, this.y, this.y)
val Vector2i.xyxx get() = Vector4i(this.x, this.y, this.x, this.x)
val Vector2i.xyxy get() = Vector4i(this.x, this.y, this.x, this.y)
val Vector2i.xyyx get() = Vector4i(this.x, this.y, this.y, this.x)
val Vector2i.xyyy get() = Vector4i(this.x, this.y, this.y, this.y)
val Vector2i.yxxx get() = Vector4i(this.y, this.x, this.x, this.x)
val Vector2i.yxxy get() = Vector4i(this.y, this.x, this.x, this.y)
val Vector2i.yxyx get() = Vector4i(this.y, this.x, this.y, this.x)
val Vector2i.yxyy get() = Vector4i(this.y, this.x, this.y, this.y)
val Vector2i.yyxx get() = Vector4i(this.y, this.y, this.x, this.x)
val Vector2i.yyxy get() = Vector4i(this.y, this.y, this.x, this.y)
val Vector2i.yyyx get() = Vector4i(this.y, this.y, this.y, this.x)
val Vector2i.yyyy get() = Vector4i(this.y, this.y, this.y, this.y)

// ===================================== RG =====================================
var Vector2i.r: Int get() = this.x; set(it) { this.x = it }
var Vector2i.g: Int get() = this.y; set(it) { this.y = it }

val Vector2i.rr get() = Vector2i(this.x, this.x)
var Vector2i.rg get() = Vector2i(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2i.gr get() = Vector2i(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2i.gg get() = Vector2i(this.y, this.y)

val Vector2i.rrr get() = Vector3i(this.x, this.x, this.x)
val Vector2i.rrg get() = Vector3i(this.x, this.x, this.y)
val Vector2i.rgr get() = Vector3i(this.x, this.y, this.x)
val Vector2i.rgg get() = Vector3i(this.x, this.y, this.y)
val Vector2i.grr get() = Vector3i(this.y, this.x, this.x)
val Vector2i.grg get() = Vector3i(this.y, this.x, this.y)
val Vector2i.ggr get() = Vector3i(this.y, this.y, this.x)
val Vector2i.ggg get() = Vector3i(this.y, this.y, this.y)

val Vector2i.rrrr get() = Vector4i(this.x, this.x, this.x, this.x)
val Vector2i.rrrg get() = Vector4i(this.x, this.x, this.x, this.y)
val Vector2i.rrgr get() = Vector4i(this.x, this.x, this.y, this.x)
val Vector2i.rrgg get() = Vector4i(this.x, this.x, this.y, this.y)
val Vector2i.rgrr get() = Vector4i(this.x, this.y, this.x, this.x)
val Vector2i.rgrg get() = Vector4i(this.x, this.y, this.x, this.y)
val Vector2i.rggr get() = Vector4i(this.x, this.y, this.y, this.x)
val Vector2i.rggg get() = Vector4i(this.x, this.y, this.y, this.y)
val Vector2i.grrr get() = Vector4i(this.y, this.x, this.x, this.x)
val Vector2i.grrg get() = Vector4i(this.y, this.x, this.x, this.y)
val Vector2i.grgr get() = Vector4i(this.y, this.x, this.y, this.x)
val Vector2i.grgg get() = Vector4i(this.y, this.x, this.y, this.y)
val Vector2i.ggrr get() = Vector4i(this.y, this.y, this.x, this.x)
val Vector2i.ggrg get() = Vector4i(this.y, this.y, this.x, this.y)
val Vector2i.gggr get() = Vector4i(this.y, this.y, this.y, this.x)
val Vector2i.gggg get() = Vector4i(this.y, this.y, this.y, this.y)