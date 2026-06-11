@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.math.*

fun Vector2b.toPair() = Pair(this.x, this.y)
fun Pair<Boolean, Boolean>.toVector() = Vector2b(this.first, this.second)

// ===================================== XY =====================================
val Vector2b.xx get() = Vector2b(this.x, this.x)
var Vector2b.xy get() = Vector2b(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2b.yx get() = Vector2b(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2b.yy get() = Vector2b(this.y, this.y)

val Vector2b.xxx get() = Vector3b(this.x, this.x, this.x)
val Vector2b.xxy get() = Vector3b(this.x, this.x, this.y)
val Vector2b.xyx get() = Vector3b(this.x, this.y, this.x)
val Vector2b.xyy get() = Vector3b(this.x, this.y, this.y)
val Vector2b.yxx get() = Vector3b(this.y, this.x, this.x)
val Vector2b.yxy get() = Vector3b(this.y, this.x, this.y)
val Vector2b.yyx get() = Vector3b(this.y, this.y, this.x)
val Vector2b.yyy get() = Vector3b(this.y, this.y, this.y)

val Vector2b.xxxx get() = Vector4b(this.x, this.x, this.x, this.x)
val Vector2b.xxxy get() = Vector4b(this.x, this.x, this.x, this.y)
val Vector2b.xxyx get() = Vector4b(this.x, this.x, this.y, this.x)
val Vector2b.xxyy get() = Vector4b(this.x, this.x, this.y, this.y)
val Vector2b.xyxx get() = Vector4b(this.x, this.y, this.x, this.x)
val Vector2b.xyxy get() = Vector4b(this.x, this.y, this.x, this.y)
val Vector2b.xyyx get() = Vector4b(this.x, this.y, this.y, this.x)
val Vector2b.xyyy get() = Vector4b(this.x, this.y, this.y, this.y)
val Vector2b.yxxx get() = Vector4b(this.y, this.x, this.x, this.x)
val Vector2b.yxxy get() = Vector4b(this.y, this.x, this.x, this.y)
val Vector2b.yxyx get() = Vector4b(this.y, this.x, this.y, this.x)
val Vector2b.yxyy get() = Vector4b(this.y, this.x, this.y, this.y)
val Vector2b.yyxx get() = Vector4b(this.y, this.y, this.x, this.x)
val Vector2b.yyxy get() = Vector4b(this.y, this.y, this.x, this.y)
val Vector2b.yyyx get() = Vector4b(this.y, this.y, this.y, this.x)
val Vector2b.yyyy get() = Vector4b(this.y, this.y, this.y, this.y)

// ===================================== RG =====================================
var Vector2b.r: Boolean get() = this.x; set(it) { this.x = it }
var Vector2b.g: Boolean get() = this.y; set(it) { this.y = it }

val Vector2b.rr get() = Vector2b(this.x, this.x)
var Vector2b.rg get() = Vector2b(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector2b.gr get() = Vector2b(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector2b.gg get() = Vector2b(this.y, this.y)

val Vector2b.rrr get() = Vector3b(this.x, this.x, this.x)
val Vector2b.rrg get() = Vector3b(this.x, this.x, this.y)
val Vector2b.rgr get() = Vector3b(this.x, this.y, this.x)
val Vector2b.rgg get() = Vector3b(this.x, this.y, this.y)
val Vector2b.grr get() = Vector3b(this.y, this.x, this.x)
val Vector2b.grg get() = Vector3b(this.y, this.x, this.y)
val Vector2b.ggr get() = Vector3b(this.y, this.y, this.x)
val Vector2b.ggg get() = Vector3b(this.y, this.y, this.y)

val Vector2b.rrrr get() = Vector4b(this.x, this.x, this.x, this.x)
val Vector2b.rrrg get() = Vector4b(this.x, this.x, this.x, this.y)
val Vector2b.rrgr get() = Vector4b(this.x, this.x, this.y, this.x)
val Vector2b.rrgg get() = Vector4b(this.x, this.x, this.y, this.y)
val Vector2b.rgrr get() = Vector4b(this.x, this.y, this.x, this.x)
val Vector2b.rgrg get() = Vector4b(this.x, this.y, this.x, this.y)
val Vector2b.rggr get() = Vector4b(this.x, this.y, this.y, this.x)
val Vector2b.rggg get() = Vector4b(this.x, this.y, this.y, this.y)
val Vector2b.grrr get() = Vector4b(this.y, this.x, this.x, this.x)
val Vector2b.grrg get() = Vector4b(this.y, this.x, this.x, this.y)
val Vector2b.grgr get() = Vector4b(this.y, this.x, this.y, this.x)
val Vector2b.grgg get() = Vector4b(this.y, this.x, this.y, this.y)
val Vector2b.ggrr get() = Vector4b(this.y, this.y, this.x, this.x)
val Vector2b.ggrg get() = Vector4b(this.y, this.y, this.x, this.y)
val Vector2b.gggr get() = Vector4b(this.y, this.y, this.y, this.x)
val Vector2b.gggg get() = Vector4b(this.y, this.y, this.y, this.y)