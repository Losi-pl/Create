@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.utility.Quad
import org.joml.*

fun Vector4i.toQuad() = Quad(this.x, this.y, this.z, this.w)
fun Quad<Int, Int, Int, Int>.toVector() = Vector4i(this.first, this.second, this.third, this.fourth)

// ===================================== XYZW =====================================
val Vector4i.xx get() = Vector2i(this.x, this.x)
var Vector4i.xy get() = Vector2i(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4i.xz get() = Vector2i(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4i.xw get() = Vector2i(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4i.yx get() = Vector2i(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4i.yy get() = Vector2i(this.y, this.y)
var Vector4i.yz get() = Vector2i(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4i.yw get() = Vector2i(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4i.zx get() = Vector2i(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4i.zy get() = Vector2i(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4i.zz get() = Vector2i(this.z, this.z)
var Vector4i.zw get() = Vector2i(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4i.wx get() = Vector2i(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4i.wy get() = Vector2i(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4i.wz get() = Vector2i(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4i.ww get() = Vector2i(this.w, this.w)

val Vector4i.xxx get() = Vector3i(this.x, this.x, this.x)
val Vector4i.xxy get() = Vector3i(this.x, this.x, this.y)
val Vector4i.xxz get() = Vector3i(this.x, this.x, this.z)
val Vector4i.xxw get() = Vector3i(this.x, this.x, this.w)
val Vector4i.xyx get() = Vector3i(this.x, this.y, this.x)
val Vector4i.xyy get() = Vector3i(this.x, this.y, this.y)
var Vector4i.xyz get() = Vector3i(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4i.xyw get() = Vector3i(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4i.xzx get() = Vector3i(this.x, this.z, this.x)
var Vector4i.xzy get() = Vector3i(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4i.xzz get() = Vector3i(this.x, this.z, this.z)
var Vector4i.xzw get() = Vector3i(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4i.xwx get() = Vector3i(this.x, this.w, this.x)
var Vector4i.xwy get() = Vector3i(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4i.xwz get() = Vector3i(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4i.xww get() = Vector3i(this.x, this.w, this.w)
val Vector4i.yxx get() = Vector3i(this.y, this.x, this.x)
val Vector4i.yxy get() = Vector3i(this.y, this.x, this.y)
var Vector4i.yxz get() = Vector3i(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4i.yxw get() = Vector3i(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4i.yyx get() = Vector3i(this.y, this.y, this.x)
val Vector4i.yyy get() = Vector3i(this.y, this.y, this.y)
val Vector4i.yyz get() = Vector3i(this.y, this.y, this.z)
val Vector4i.yyw get() = Vector3i(this.y, this.y, this.w)
var Vector4i.yzx get() = Vector3i(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4i.yzy get() = Vector3i(this.y, this.z, this.y)
val Vector4i.yzz get() = Vector3i(this.y, this.z, this.z)
var Vector4i.yzw get() = Vector3i(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4i.ywx get() = Vector3i(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4i.ywy get() = Vector3i(this.y, this.w, this.y)
var Vector4i.ywz get() = Vector3i(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4i.yww get() = Vector3i(this.y, this.w, this.w)
val Vector4i.zxx get() = Vector3i(this.z, this.x, this.x)
var Vector4i.zxy get() = Vector3i(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4i.zxz get() = Vector3i(this.z, this.x, this.z)
var Vector4i.zxw get() = Vector3i(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4i.zyx get() = Vector3i(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4i.zyy get() = Vector3i(this.z, this.y, this.y)
val Vector4i.zyz get() = Vector3i(this.z, this.y, this.z)
var Vector4i.zyw get() = Vector3i(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4i.zzx get() = Vector3i(this.z, this.z, this.x)
val Vector4i.zzy get() = Vector3i(this.z, this.z, this.y)
val Vector4i.zzz get() = Vector3i(this.z, this.z, this.z)
val Vector4i.zzw get() = Vector3i(this.z, this.z, this.w)
var Vector4i.zwx get() = Vector3i(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4i.zwy get() = Vector3i(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4i.zwz get() = Vector3i(this.z, this.w, this.z)
val Vector4i.zww get() = Vector3i(this.z, this.w, this.w)
val Vector4i.wxx get() = Vector3i(this.w, this.x, this.x)
var Vector4i.wxy get() = Vector3i(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4i.wxz get() = Vector3i(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4i.wxw get() = Vector3i(this.w, this.x, this.w)
var Vector4i.wyx get() = Vector3i(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4i.wyy get() = Vector3i(this.w, this.y, this.y)
var Vector4i.wyz get() = Vector3i(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4i.wyw get() = Vector3i(this.w, this.y, this.w)
var Vector4i.wzx get() = Vector3i(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4i.wzy get() = Vector3i(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4i.wzz get() = Vector3i(this.w, this.z, this.z)
val Vector4i.wzw get() = Vector3i(this.w, this.z, this.w)
val Vector4i.wwx get() = Vector3i(this.w, this.w, this.x)
val Vector4i.wwy get() = Vector3i(this.w, this.w, this.y)
val Vector4i.wwz get() = Vector3i(this.w, this.w, this.z)
val Vector4i.www get() = Vector3i(this.w, this.w, this.w)

val Vector4i.xxxx get() = Vector4i(this.x, this.x, this.x, this.x)
val Vector4i.xxxy get() = Vector4i(this.x, this.x, this.x, this.y)
val Vector4i.xxxz get() = Vector4i(this.x, this.x, this.x, this.z)
val Vector4i.xxxw get() = Vector4i(this.x, this.x, this.x, this.w)
val Vector4i.xxyx get() = Vector4i(this.x, this.x, this.y, this.x)
val Vector4i.xxyy get() = Vector4i(this.x, this.x, this.y, this.y)
val Vector4i.xxyz get() = Vector4i(this.x, this.x, this.y, this.z)
val Vector4i.xxyw get() = Vector4i(this.x, this.x, this.y, this.w)
val Vector4i.xxzx get() = Vector4i(this.x, this.x, this.z, this.x)
val Vector4i.xxzy get() = Vector4i(this.x, this.x, this.z, this.y)
val Vector4i.xxzz get() = Vector4i(this.x, this.x, this.z, this.z)
val Vector4i.xxzw get() = Vector4i(this.x, this.x, this.z, this.w)
val Vector4i.xxwx get() = Vector4i(this.x, this.x, this.w, this.x)
val Vector4i.xxwy get() = Vector4i(this.x, this.x, this.w, this.y)
val Vector4i.xxwz get() = Vector4i(this.x, this.x, this.w, this.z)
val Vector4i.xxww get() = Vector4i(this.x, this.x, this.w, this.w)
val Vector4i.xyxx get() = Vector4i(this.x, this.y, this.x, this.x)
val Vector4i.xyxy get() = Vector4i(this.x, this.y, this.x, this.y)
val Vector4i.xyxz get() = Vector4i(this.x, this.y, this.x, this.z)
val Vector4i.xyxw get() = Vector4i(this.x, this.y, this.x, this.w)
val Vector4i.xyyx get() = Vector4i(this.x, this.y, this.y, this.x)
val Vector4i.xyyy get() = Vector4i(this.x, this.y, this.y, this.y)
val Vector4i.xyyz get() = Vector4i(this.x, this.y, this.y, this.z)
val Vector4i.xyyw get() = Vector4i(this.x, this.y, this.y, this.w)
val Vector4i.xyzx get() = Vector4i(this.x, this.y, this.z, this.x)
val Vector4i.xyzy get() = Vector4i(this.x, this.y, this.z, this.y)
val Vector4i.xyzz get() = Vector4i(this.x, this.y, this.z, this.z)
var Vector4i.xyzw get() = Vector4i(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4i.xywx get() = Vector4i(this.x, this.y, this.w, this.x)
val Vector4i.xywy get() = Vector4i(this.x, this.y, this.w, this.y)
var Vector4i.xywz get() = Vector4i(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4i.xyww get() = Vector4i(this.x, this.y, this.w, this.w)
val Vector4i.xzxx get() = Vector4i(this.x, this.z, this.x, this.x)
val Vector4i.xzxy get() = Vector4i(this.x, this.z, this.x, this.y)
val Vector4i.xzxz get() = Vector4i(this.x, this.z, this.x, this.z)
val Vector4i.xzxw get() = Vector4i(this.x, this.z, this.x, this.w)
val Vector4i.xzyx get() = Vector4i(this.x, this.z, this.y, this.x)
val Vector4i.xzyy get() = Vector4i(this.x, this.z, this.y, this.y)
val Vector4i.xzyz get() = Vector4i(this.x, this.z, this.y, this.z)
var Vector4i.xzyw get() = Vector4i(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4i.xzzx get() = Vector4i(this.x, this.z, this.z, this.x)
val Vector4i.xzzy get() = Vector4i(this.x, this.z, this.z, this.y)
val Vector4i.xzzz get() = Vector4i(this.x, this.z, this.z, this.z)
val Vector4i.xzzw get() = Vector4i(this.x, this.z, this.z, this.w)
val Vector4i.xzwx get() = Vector4i(this.x, this.z, this.w, this.x)
var Vector4i.xzwy get() = Vector4i(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4i.xzwz get() = Vector4i(this.x, this.z, this.w, this.z)
val Vector4i.xzww get() = Vector4i(this.x, this.z, this.w, this.w)
val Vector4i.xwxx get() = Vector4i(this.x, this.w, this.x, this.x)
val Vector4i.xwxy get() = Vector4i(this.x, this.w, this.x, this.y)
val Vector4i.xwxz get() = Vector4i(this.x, this.w, this.x, this.z)
val Vector4i.xwxw get() = Vector4i(this.x, this.w, this.x, this.w)
val Vector4i.xwyx get() = Vector4i(this.x, this.w, this.y, this.x)
val Vector4i.xwyy get() = Vector4i(this.x, this.w, this.y, this.y)
var Vector4i.xwyz get() = Vector4i(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4i.xwyw get() = Vector4i(this.x, this.w, this.y, this.w)
val Vector4i.xwzx get() = Vector4i(this.x, this.w, this.z, this.x)
var Vector4i.xwzy get() = Vector4i(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4i.xwzz get() = Vector4i(this.x, this.w, this.z, this.z)
val Vector4i.xwzw get() = Vector4i(this.x, this.w, this.z, this.w)
val Vector4i.xwwx get() = Vector4i(this.x, this.w, this.w, this.x)
val Vector4i.xwwy get() = Vector4i(this.x, this.w, this.w, this.y)
val Vector4i.xwwz get() = Vector4i(this.x, this.w, this.w, this.z)
val Vector4i.xwww get() = Vector4i(this.x, this.w, this.w, this.w)
val Vector4i.yxxx get() = Vector4i(this.y, this.x, this.x, this.x)
val Vector4i.yxxy get() = Vector4i(this.y, this.x, this.x, this.y)
val Vector4i.yxxz get() = Vector4i(this.y, this.x, this.x, this.z)
val Vector4i.yxxw get() = Vector4i(this.y, this.x, this.x, this.w)
val Vector4i.yxyx get() = Vector4i(this.y, this.x, this.y, this.x)
val Vector4i.yxyy get() = Vector4i(this.y, this.x, this.y, this.y)
val Vector4i.yxyz get() = Vector4i(this.y, this.x, this.y, this.z)
val Vector4i.yxyw get() = Vector4i(this.y, this.x, this.y, this.w)
val Vector4i.yxzx get() = Vector4i(this.y, this.x, this.z, this.x)
val Vector4i.yxzy get() = Vector4i(this.y, this.x, this.z, this.y)
val Vector4i.yxzz get() = Vector4i(this.y, this.x, this.z, this.z)
var Vector4i.yxzw get() = Vector4i(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4i.yxwx get() = Vector4i(this.y, this.x, this.w, this.x)
val Vector4i.yxwy get() = Vector4i(this.y, this.x, this.w, this.y)
var Vector4i.yxwz get() = Vector4i(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4i.yxww get() = Vector4i(this.y, this.x, this.w, this.w)
val Vector4i.yyxx get() = Vector4i(this.y, this.y, this.x, this.x)
val Vector4i.yyxy get() = Vector4i(this.y, this.y, this.x, this.y)
val Vector4i.yyxz get() = Vector4i(this.y, this.y, this.x, this.z)
val Vector4i.yyxw get() = Vector4i(this.y, this.y, this.x, this.w)
val Vector4i.yyyx get() = Vector4i(this.y, this.y, this.y, this.x)
val Vector4i.yyyy get() = Vector4i(this.y, this.y, this.y, this.y)
val Vector4i.yyyz get() = Vector4i(this.y, this.y, this.y, this.z)
val Vector4i.yyyw get() = Vector4i(this.y, this.y, this.y, this.w)
val Vector4i.yyzx get() = Vector4i(this.y, this.y, this.z, this.x)
val Vector4i.yyzy get() = Vector4i(this.y, this.y, this.z, this.y)
val Vector4i.yyzz get() = Vector4i(this.y, this.y, this.z, this.z)
val Vector4i.yyzw get() = Vector4i(this.y, this.y, this.z, this.w)
val Vector4i.yywx get() = Vector4i(this.y, this.y, this.w, this.x)
val Vector4i.yywy get() = Vector4i(this.y, this.y, this.w, this.y)
val Vector4i.yywz get() = Vector4i(this.y, this.y, this.w, this.z)
val Vector4i.yyww get() = Vector4i(this.y, this.y, this.w, this.w)
val Vector4i.yzxx get() = Vector4i(this.y, this.z, this.x, this.x)
val Vector4i.yzxy get() = Vector4i(this.y, this.z, this.x, this.y)
val Vector4i.yzxz get() = Vector4i(this.y, this.z, this.x, this.z)
var Vector4i.yzxw get() = Vector4i(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4i.yzyx get() = Vector4i(this.y, this.z, this.y, this.x)
val Vector4i.yzyy get() = Vector4i(this.y, this.z, this.y, this.y)
val Vector4i.yzyz get() = Vector4i(this.y, this.z, this.y, this.z)
val Vector4i.yzyw get() = Vector4i(this.y, this.z, this.y, this.w)
val Vector4i.yzzx get() = Vector4i(this.y, this.z, this.z, this.x)
val Vector4i.yzzy get() = Vector4i(this.y, this.z, this.z, this.y)
val Vector4i.yzzz get() = Vector4i(this.y, this.z, this.z, this.z)
val Vector4i.yzzw get() = Vector4i(this.y, this.z, this.z, this.w)
var Vector4i.yzwx get() = Vector4i(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4i.yzwy get() = Vector4i(this.y, this.z, this.w, this.y)
val Vector4i.yzwz get() = Vector4i(this.y, this.z, this.w, this.z)
val Vector4i.yzww get() = Vector4i(this.y, this.z, this.w, this.w)
val Vector4i.ywxx get() = Vector4i(this.y, this.w, this.x, this.x)
val Vector4i.ywxy get() = Vector4i(this.y, this.w, this.x, this.y)
var Vector4i.ywxz get() = Vector4i(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4i.ywxw get() = Vector4i(this.y, this.w, this.x, this.w)
val Vector4i.ywyx get() = Vector4i(this.y, this.w, this.y, this.x)
val Vector4i.ywyy get() = Vector4i(this.y, this.w, this.y, this.y)
val Vector4i.ywyz get() = Vector4i(this.y, this.w, this.y, this.z)
val Vector4i.ywyw get() = Vector4i(this.y, this.w, this.y, this.w)
var Vector4i.ywzx get() = Vector4i(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4i.ywzy get() = Vector4i(this.y, this.w, this.z, this.y)
val Vector4i.ywzz get() = Vector4i(this.y, this.w, this.z, this.z)
val Vector4i.ywzw get() = Vector4i(this.y, this.w, this.z, this.w)
val Vector4i.ywwx get() = Vector4i(this.y, this.w, this.w, this.x)
val Vector4i.ywwy get() = Vector4i(this.y, this.w, this.w, this.y)
val Vector4i.ywwz get() = Vector4i(this.y, this.w, this.w, this.z)
val Vector4i.ywww get() = Vector4i(this.y, this.w, this.w, this.w)
val Vector4i.zxxx get() = Vector4i(this.z, this.x, this.x, this.x)
val Vector4i.zxxy get() = Vector4i(this.z, this.x, this.x, this.y)
val Vector4i.zxxz get() = Vector4i(this.z, this.x, this.x, this.z)
val Vector4i.zxxw get() = Vector4i(this.z, this.x, this.x, this.w)
val Vector4i.zxyx get() = Vector4i(this.z, this.x, this.y, this.x)
val Vector4i.zxyy get() = Vector4i(this.z, this.x, this.y, this.y)
val Vector4i.zxyz get() = Vector4i(this.z, this.x, this.y, this.z)
var Vector4i.zxyw get() = Vector4i(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4i.zxzx get() = Vector4i(this.z, this.x, this.z, this.x)
val Vector4i.zxzy get() = Vector4i(this.z, this.x, this.z, this.y)
val Vector4i.zxzz get() = Vector4i(this.z, this.x, this.z, this.z)
val Vector4i.zxzw get() = Vector4i(this.z, this.x, this.z, this.w)
val Vector4i.zxwx get() = Vector4i(this.z, this.x, this.w, this.x)
var Vector4i.zxwy get() = Vector4i(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4i.zxwz get() = Vector4i(this.z, this.x, this.w, this.z)
val Vector4i.zxww get() = Vector4i(this.z, this.x, this.w, this.w)
val Vector4i.zyxx get() = Vector4i(this.z, this.y, this.x, this.x)
val Vector4i.zyxy get() = Vector4i(this.z, this.y, this.x, this.y)
val Vector4i.zyxz get() = Vector4i(this.z, this.y, this.x, this.z)
var Vector4i.zyxw get() = Vector4i(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4i.zyyx get() = Vector4i(this.z, this.y, this.y, this.x)
val Vector4i.zyyy get() = Vector4i(this.z, this.y, this.y, this.y)
val Vector4i.zyyz get() = Vector4i(this.z, this.y, this.y, this.z)
val Vector4i.zyyw get() = Vector4i(this.z, this.y, this.y, this.w)
val Vector4i.zyzx get() = Vector4i(this.z, this.y, this.z, this.x)
val Vector4i.zyzy get() = Vector4i(this.z, this.y, this.z, this.y)
val Vector4i.zyzz get() = Vector4i(this.z, this.y, this.z, this.z)
val Vector4i.zyzw get() = Vector4i(this.z, this.y, this.z, this.w)
var Vector4i.zywx get() = Vector4i(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4i.zywy get() = Vector4i(this.z, this.y, this.w, this.y)
val Vector4i.zywz get() = Vector4i(this.z, this.y, this.w, this.z)
val Vector4i.zyww get() = Vector4i(this.z, this.y, this.w, this.w)
val Vector4i.zzxx get() = Vector4i(this.z, this.z, this.x, this.x)
val Vector4i.zzxy get() = Vector4i(this.z, this.z, this.x, this.y)
val Vector4i.zzxz get() = Vector4i(this.z, this.z, this.x, this.z)
val Vector4i.zzxw get() = Vector4i(this.z, this.z, this.x, this.w)
val Vector4i.zzyx get() = Vector4i(this.z, this.z, this.y, this.x)
val Vector4i.zzyy get() = Vector4i(this.z, this.z, this.y, this.y)
val Vector4i.zzyz get() = Vector4i(this.z, this.z, this.y, this.z)
val Vector4i.zzyw get() = Vector4i(this.z, this.z, this.y, this.w)
val Vector4i.zzzx get() = Vector4i(this.z, this.z, this.z, this.x)
val Vector4i.zzzy get() = Vector4i(this.z, this.z, this.z, this.y)
val Vector4i.zzzz get() = Vector4i(this.z, this.z, this.z, this.z)
val Vector4i.zzzw get() = Vector4i(this.z, this.z, this.z, this.w)
val Vector4i.zzwx get() = Vector4i(this.z, this.z, this.w, this.x)
val Vector4i.zzwy get() = Vector4i(this.z, this.z, this.w, this.y)
val Vector4i.zzwz get() = Vector4i(this.z, this.z, this.w, this.z)
val Vector4i.zzww get() = Vector4i(this.z, this.z, this.w, this.w)
val Vector4i.zwxx get() = Vector4i(this.z, this.w, this.x, this.x)
var Vector4i.zwxy get() = Vector4i(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4i.zwxz get() = Vector4i(this.z, this.w, this.x, this.z)
val Vector4i.zwxw get() = Vector4i(this.z, this.w, this.x, this.w)
var Vector4i.zwyx get() = Vector4i(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4i.zwyy get() = Vector4i(this.z, this.w, this.y, this.y)
val Vector4i.zwyz get() = Vector4i(this.z, this.w, this.y, this.z)
val Vector4i.zwyw get() = Vector4i(this.z, this.w, this.y, this.w)
val Vector4i.zwzx get() = Vector4i(this.z, this.w, this.z, this.x)
val Vector4i.zwzy get() = Vector4i(this.z, this.w, this.z, this.y)
val Vector4i.zwzz get() = Vector4i(this.z, this.w, this.z, this.z)
val Vector4i.zwzw get() = Vector4i(this.z, this.w, this.z, this.w)
val Vector4i.zwwx get() = Vector4i(this.z, this.w, this.w, this.x)
val Vector4i.zwwy get() = Vector4i(this.z, this.w, this.w, this.y)
val Vector4i.zwwz get() = Vector4i(this.z, this.w, this.w, this.z)
val Vector4i.zwww get() = Vector4i(this.z, this.w, this.w, this.w)
val Vector4i.wxxx get() = Vector4i(this.w, this.x, this.x, this.x)
val Vector4i.wxxy get() = Vector4i(this.w, this.x, this.x, this.y)
val Vector4i.wxxz get() = Vector4i(this.w, this.x, this.x, this.z)
val Vector4i.wxxw get() = Vector4i(this.w, this.x, this.x, this.w)
val Vector4i.wxyx get() = Vector4i(this.w, this.x, this.y, this.x)
val Vector4i.wxyy get() = Vector4i(this.w, this.x, this.y, this.y)
var Vector4i.wxyz get() = Vector4i(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4i.wxyw get() = Vector4i(this.w, this.x, this.y, this.w)
val Vector4i.wxzx get() = Vector4i(this.w, this.x, this.z, this.x)
var Vector4i.wxzy get() = Vector4i(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4i.wxzz get() = Vector4i(this.w, this.x, this.z, this.z)
val Vector4i.wxzw get() = Vector4i(this.w, this.x, this.z, this.w)
val Vector4i.wxwx get() = Vector4i(this.w, this.x, this.w, this.x)
val Vector4i.wxwy get() = Vector4i(this.w, this.x, this.w, this.y)
val Vector4i.wxwz get() = Vector4i(this.w, this.x, this.w, this.z)
val Vector4i.wxww get() = Vector4i(this.w, this.x, this.w, this.w)
val Vector4i.wyxx get() = Vector4i(this.w, this.y, this.x, this.x)
val Vector4i.wyxy get() = Vector4i(this.w, this.y, this.x, this.y)
var Vector4i.wyxz get() = Vector4i(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4i.wyxw get() = Vector4i(this.w, this.y, this.x, this.w)
val Vector4i.wyyx get() = Vector4i(this.w, this.y, this.y, this.x)
val Vector4i.wyyy get() = Vector4i(this.w, this.y, this.y, this.y)
val Vector4i.wyyz get() = Vector4i(this.w, this.y, this.y, this.z)
val Vector4i.wyyw get() = Vector4i(this.w, this.y, this.y, this.w)
var Vector4i.wyzx get() = Vector4i(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4i.wyzy get() = Vector4i(this.w, this.y, this.z, this.y)
val Vector4i.wyzz get() = Vector4i(this.w, this.y, this.z, this.z)
val Vector4i.wyzw get() = Vector4i(this.w, this.y, this.z, this.w)
val Vector4i.wywx get() = Vector4i(this.w, this.y, this.w, this.x)
val Vector4i.wywy get() = Vector4i(this.w, this.y, this.w, this.y)
val Vector4i.wywz get() = Vector4i(this.w, this.y, this.w, this.z)
val Vector4i.wyww get() = Vector4i(this.w, this.y, this.w, this.w)
val Vector4i.wzxx get() = Vector4i(this.w, this.z, this.x, this.x)
var Vector4i.wzxy get() = Vector4i(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4i.wzxz get() = Vector4i(this.w, this.z, this.x, this.z)
val Vector4i.wzxw get() = Vector4i(this.w, this.z, this.x, this.w)
var Vector4i.wzyx get() = Vector4i(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4i.wzyy get() = Vector4i(this.w, this.z, this.y, this.y)
val Vector4i.wzyz get() = Vector4i(this.w, this.z, this.y, this.z)
val Vector4i.wzyw get() = Vector4i(this.w, this.z, this.y, this.w)
val Vector4i.wzzx get() = Vector4i(this.w, this.z, this.z, this.x)
val Vector4i.wzzy get() = Vector4i(this.w, this.z, this.z, this.y)
val Vector4i.wzzz get() = Vector4i(this.w, this.z, this.z, this.z)
val Vector4i.wzzw get() = Vector4i(this.w, this.z, this.z, this.w)
val Vector4i.wzwx get() = Vector4i(this.w, this.z, this.w, this.x)
val Vector4i.wzwy get() = Vector4i(this.w, this.z, this.w, this.y)
val Vector4i.wzwz get() = Vector4i(this.w, this.z, this.w, this.z)
val Vector4i.wzww get() = Vector4i(this.w, this.z, this.w, this.w)
val Vector4i.wwxx get() = Vector4i(this.w, this.w, this.x, this.x)
val Vector4i.wwxy get() = Vector4i(this.w, this.w, this.x, this.y)
val Vector4i.wwxz get() = Vector4i(this.w, this.w, this.x, this.z)
val Vector4i.wwxw get() = Vector4i(this.w, this.w, this.x, this.w)
val Vector4i.wwyx get() = Vector4i(this.w, this.w, this.y, this.x)
val Vector4i.wwyy get() = Vector4i(this.w, this.w, this.y, this.y)
val Vector4i.wwyz get() = Vector4i(this.w, this.w, this.y, this.z)
val Vector4i.wwyw get() = Vector4i(this.w, this.w, this.y, this.w)
val Vector4i.wwzx get() = Vector4i(this.w, this.w, this.z, this.x)
val Vector4i.wwzy get() = Vector4i(this.w, this.w, this.z, this.y)
val Vector4i.wwzz get() = Vector4i(this.w, this.w, this.z, this.z)
val Vector4i.wwzw get() = Vector4i(this.w, this.w, this.z, this.w)
val Vector4i.wwwx get() = Vector4i(this.w, this.w, this.w, this.x)
val Vector4i.wwwy get() = Vector4i(this.w, this.w, this.w, this.y)
val Vector4i.wwwz get() = Vector4i(this.w, this.w, this.w, this.z)
val Vector4i.wwww get() = Vector4i(this.w, this.w, this.w, this.w)

// ===================================== RGBA =====================================
var Vector4i.r: Int get() = this.x; set(it) { this.x = it }
var Vector4i.g: Int get() = this.y; set(it) { this.y = it }
var Vector4i.b: Int get() = this.z; set(it) { this.z = it }
var Vector4i.a: Int get() = this.w; set(it) { this.w = it }

val Vector4i.rr get() = Vector2i(this.x, this.x)
var Vector4i.rg get() = Vector2i(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4i.rb get() = Vector2i(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4i.ra get() = Vector2i(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4i.gr get() = Vector2i(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4i.gg get() = Vector2i(this.y, this.y)
var Vector4i.gb get() = Vector2i(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4i.ga get() = Vector2i(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4i.br get() = Vector2i(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4i.bg get() = Vector2i(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4i.bb get() = Vector2i(this.z, this.z)
var Vector4i.ba get() = Vector2i(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4i.ar get() = Vector2i(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4i.ag get() = Vector2i(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4i.ab get() = Vector2i(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4i.aa get() = Vector2i(this.w, this.w)

val Vector4i.rrr get() = Vector3i(this.x, this.x, this.x)
val Vector4i.rrg get() = Vector3i(this.x, this.x, this.y)
val Vector4i.rrb get() = Vector3i(this.x, this.x, this.z)
val Vector4i.rra get() = Vector3i(this.x, this.x, this.w)
val Vector4i.rgr get() = Vector3i(this.x, this.y, this.x)
val Vector4i.rgg get() = Vector3i(this.x, this.y, this.y)
var Vector4i.rgb get() = Vector3i(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4i.rga get() = Vector3i(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4i.rbr get() = Vector3i(this.x, this.z, this.x)
var Vector4i.rbg get() = Vector3i(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4i.rbb get() = Vector3i(this.x, this.z, this.z)
var Vector4i.rba get() = Vector3i(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4i.rar get() = Vector3i(this.x, this.w, this.x)
var Vector4i.rag get() = Vector3i(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4i.rab get() = Vector3i(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4i.raa get() = Vector3i(this.x, this.w, this.w)
val Vector4i.grr get() = Vector3i(this.y, this.x, this.x)
val Vector4i.grg get() = Vector3i(this.y, this.x, this.y)
var Vector4i.grb get() = Vector3i(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4i.gra get() = Vector3i(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4i.ggr get() = Vector3i(this.y, this.y, this.x)
val Vector4i.ggg get() = Vector3i(this.y, this.y, this.y)
val Vector4i.ggb get() = Vector3i(this.y, this.y, this.z)
val Vector4i.gga get() = Vector3i(this.y, this.y, this.w)
var Vector4i.gbr get() = Vector3i(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4i.gbg get() = Vector3i(this.y, this.z, this.y)
val Vector4i.gbb get() = Vector3i(this.y, this.z, this.z)
var Vector4i.gba get() = Vector3i(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4i.gar get() = Vector3i(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4i.gag get() = Vector3i(this.y, this.w, this.y)
var Vector4i.gab get() = Vector3i(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4i.gaa get() = Vector3i(this.y, this.w, this.w)
val Vector4i.brr get() = Vector3i(this.z, this.x, this.x)
var Vector4i.brg get() = Vector3i(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4i.brb get() = Vector3i(this.z, this.x, this.z)
var Vector4i.bra get() = Vector3i(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4i.bgr get() = Vector3i(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4i.bgg get() = Vector3i(this.z, this.y, this.y)
val Vector4i.bgb get() = Vector3i(this.z, this.y, this.z)
var Vector4i.bga get() = Vector3i(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4i.bbr get() = Vector3i(this.z, this.z, this.x)
val Vector4i.bbg get() = Vector3i(this.z, this.z, this.y)
val Vector4i.bbb get() = Vector3i(this.z, this.z, this.z)
val Vector4i.bba get() = Vector3i(this.z, this.z, this.w)
var Vector4i.bar get() = Vector3i(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4i.bag get() = Vector3i(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4i.bab get() = Vector3i(this.z, this.w, this.z)
val Vector4i.baa get() = Vector3i(this.z, this.w, this.w)
val Vector4i.arr get() = Vector3i(this.w, this.x, this.x)
var Vector4i.arg get() = Vector3i(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4i.arb get() = Vector3i(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4i.ara get() = Vector3i(this.w, this.x, this.w)
var Vector4i.agr get() = Vector3i(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4i.agg get() = Vector3i(this.w, this.y, this.y)
var Vector4i.agb get() = Vector3i(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4i.aga get() = Vector3i(this.w, this.y, this.w)
var Vector4i.abr get() = Vector3i(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4i.abg get() = Vector3i(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4i.abb get() = Vector3i(this.w, this.z, this.z)
val Vector4i.aba get() = Vector3i(this.w, this.z, this.w)
val Vector4i.aar get() = Vector3i(this.w, this.w, this.x)
val Vector4i.aag get() = Vector3i(this.w, this.w, this.y)
val Vector4i.aab get() = Vector3i(this.w, this.w, this.z)
val Vector4i.aaa get() = Vector3i(this.w, this.w, this.w)

val Vector4i.rrrr get() = Vector4i(this.x, this.x, this.x, this.x)
val Vector4i.rrrg get() = Vector4i(this.x, this.x, this.x, this.y)
val Vector4i.rrrb get() = Vector4i(this.x, this.x, this.x, this.z)
val Vector4i.rrra get() = Vector4i(this.x, this.x, this.x, this.w)
val Vector4i.rrgr get() = Vector4i(this.x, this.x, this.y, this.x)
val Vector4i.rrgg get() = Vector4i(this.x, this.x, this.y, this.y)
val Vector4i.rrgb get() = Vector4i(this.x, this.x, this.y, this.z)
val Vector4i.rrga get() = Vector4i(this.x, this.x, this.y, this.w)
val Vector4i.rrbr get() = Vector4i(this.x, this.x, this.z, this.x)
val Vector4i.rrbg get() = Vector4i(this.x, this.x, this.z, this.y)
val Vector4i.rrbb get() = Vector4i(this.x, this.x, this.z, this.z)
val Vector4i.rrba get() = Vector4i(this.x, this.x, this.z, this.w)
val Vector4i.rrar get() = Vector4i(this.x, this.x, this.w, this.x)
val Vector4i.rrag get() = Vector4i(this.x, this.x, this.w, this.y)
val Vector4i.rrab get() = Vector4i(this.x, this.x, this.w, this.z)
val Vector4i.rraa get() = Vector4i(this.x, this.x, this.w, this.w)
val Vector4i.rgrr get() = Vector4i(this.x, this.y, this.x, this.x)
val Vector4i.rgrg get() = Vector4i(this.x, this.y, this.x, this.y)
val Vector4i.rgrb get() = Vector4i(this.x, this.y, this.x, this.z)
val Vector4i.rgra get() = Vector4i(this.x, this.y, this.x, this.w)
val Vector4i.rggr get() = Vector4i(this.x, this.y, this.y, this.x)
val Vector4i.rggg get() = Vector4i(this.x, this.y, this.y, this.y)
val Vector4i.rggb get() = Vector4i(this.x, this.y, this.y, this.z)
val Vector4i.rgga get() = Vector4i(this.x, this.y, this.y, this.w)
val Vector4i.rgbr get() = Vector4i(this.x, this.y, this.z, this.x)
val Vector4i.rgbg get() = Vector4i(this.x, this.y, this.z, this.y)
val Vector4i.rgbb get() = Vector4i(this.x, this.y, this.z, this.z)
var Vector4i.rgba get() = Vector4i(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4i.rgar get() = Vector4i(this.x, this.y, this.w, this.x)
val Vector4i.rgag get() = Vector4i(this.x, this.y, this.w, this.y)
var Vector4i.rgab get() = Vector4i(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4i.rgaa get() = Vector4i(this.x, this.y, this.w, this.w)
val Vector4i.rbrr get() = Vector4i(this.x, this.z, this.x, this.x)
val Vector4i.rbrg get() = Vector4i(this.x, this.z, this.x, this.y)
val Vector4i.rbrb get() = Vector4i(this.x, this.z, this.x, this.z)
val Vector4i.rbra get() = Vector4i(this.x, this.z, this.x, this.w)
val Vector4i.rbgr get() = Vector4i(this.x, this.z, this.y, this.x)
val Vector4i.rbgg get() = Vector4i(this.x, this.z, this.y, this.y)
val Vector4i.rbgb get() = Vector4i(this.x, this.z, this.y, this.z)
var Vector4i.rbga get() = Vector4i(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4i.rbbr get() = Vector4i(this.x, this.z, this.z, this.x)
val Vector4i.rbbg get() = Vector4i(this.x, this.z, this.z, this.y)
val Vector4i.rbbb get() = Vector4i(this.x, this.z, this.z, this.z)
val Vector4i.rbba get() = Vector4i(this.x, this.z, this.z, this.w)
val Vector4i.rbar get() = Vector4i(this.x, this.z, this.w, this.x)
var Vector4i.rbag get() = Vector4i(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4i.rbab get() = Vector4i(this.x, this.z, this.w, this.z)
val Vector4i.rbaa get() = Vector4i(this.x, this.z, this.w, this.w)
val Vector4i.rarr get() = Vector4i(this.x, this.w, this.x, this.x)
val Vector4i.rarg get() = Vector4i(this.x, this.w, this.x, this.y)
val Vector4i.rarb get() = Vector4i(this.x, this.w, this.x, this.z)
val Vector4i.rara get() = Vector4i(this.x, this.w, this.x, this.w)
val Vector4i.ragr get() = Vector4i(this.x, this.w, this.y, this.x)
val Vector4i.ragg get() = Vector4i(this.x, this.w, this.y, this.y)
var Vector4i.ragb get() = Vector4i(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4i.raga get() = Vector4i(this.x, this.w, this.y, this.w)
val Vector4i.rabr get() = Vector4i(this.x, this.w, this.z, this.x)
var Vector4i.rabg get() = Vector4i(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4i.rabb get() = Vector4i(this.x, this.w, this.z, this.z)
val Vector4i.raba get() = Vector4i(this.x, this.w, this.z, this.w)
val Vector4i.raar get() = Vector4i(this.x, this.w, this.w, this.x)
val Vector4i.raag get() = Vector4i(this.x, this.w, this.w, this.y)
val Vector4i.raab get() = Vector4i(this.x, this.w, this.w, this.z)
val Vector4i.raaa get() = Vector4i(this.x, this.w, this.w, this.w)
val Vector4i.grrr get() = Vector4i(this.y, this.x, this.x, this.x)
val Vector4i.grrg get() = Vector4i(this.y, this.x, this.x, this.y)
val Vector4i.grrb get() = Vector4i(this.y, this.x, this.x, this.z)
val Vector4i.grra get() = Vector4i(this.y, this.x, this.x, this.w)
val Vector4i.grgr get() = Vector4i(this.y, this.x, this.y, this.x)
val Vector4i.grgg get() = Vector4i(this.y, this.x, this.y, this.y)
val Vector4i.grgb get() = Vector4i(this.y, this.x, this.y, this.z)
val Vector4i.grga get() = Vector4i(this.y, this.x, this.y, this.w)
val Vector4i.grbr get() = Vector4i(this.y, this.x, this.z, this.x)
val Vector4i.grbg get() = Vector4i(this.y, this.x, this.z, this.y)
val Vector4i.grbb get() = Vector4i(this.y, this.x, this.z, this.z)
var Vector4i.grba get() = Vector4i(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4i.grar get() = Vector4i(this.y, this.x, this.w, this.x)
val Vector4i.grag get() = Vector4i(this.y, this.x, this.w, this.y)
var Vector4i.grab get() = Vector4i(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4i.graa get() = Vector4i(this.y, this.x, this.w, this.w)
val Vector4i.ggrr get() = Vector4i(this.y, this.y, this.x, this.x)
val Vector4i.ggrg get() = Vector4i(this.y, this.y, this.x, this.y)
val Vector4i.ggrb get() = Vector4i(this.y, this.y, this.x, this.z)
val Vector4i.ggra get() = Vector4i(this.y, this.y, this.x, this.w)
val Vector4i.gggr get() = Vector4i(this.y, this.y, this.y, this.x)
val Vector4i.gggg get() = Vector4i(this.y, this.y, this.y, this.y)
val Vector4i.gggb get() = Vector4i(this.y, this.y, this.y, this.z)
val Vector4i.ggga get() = Vector4i(this.y, this.y, this.y, this.w)
val Vector4i.ggbr get() = Vector4i(this.y, this.y, this.z, this.x)
val Vector4i.ggbg get() = Vector4i(this.y, this.y, this.z, this.y)
val Vector4i.ggbb get() = Vector4i(this.y, this.y, this.z, this.z)
val Vector4i.ggba get() = Vector4i(this.y, this.y, this.z, this.w)
val Vector4i.ggar get() = Vector4i(this.y, this.y, this.w, this.x)
val Vector4i.ggag get() = Vector4i(this.y, this.y, this.w, this.y)
val Vector4i.ggab get() = Vector4i(this.y, this.y, this.w, this.z)
val Vector4i.ggaa get() = Vector4i(this.y, this.y, this.w, this.w)
val Vector4i.gbrr get() = Vector4i(this.y, this.z, this.x, this.x)
val Vector4i.gbrg get() = Vector4i(this.y, this.z, this.x, this.y)
val Vector4i.gbrb get() = Vector4i(this.y, this.z, this.x, this.z)
var Vector4i.gbra get() = Vector4i(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4i.gbgr get() = Vector4i(this.y, this.z, this.y, this.x)
val Vector4i.gbgg get() = Vector4i(this.y, this.z, this.y, this.y)
val Vector4i.gbgb get() = Vector4i(this.y, this.z, this.y, this.z)
val Vector4i.gbga get() = Vector4i(this.y, this.z, this.y, this.w)
val Vector4i.gbbr get() = Vector4i(this.y, this.z, this.z, this.x)
val Vector4i.gbbg get() = Vector4i(this.y, this.z, this.z, this.y)
val Vector4i.gbbb get() = Vector4i(this.y, this.z, this.z, this.z)
val Vector4i.gbba get() = Vector4i(this.y, this.z, this.z, this.w)
var Vector4i.gbar get() = Vector4i(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4i.gbag get() = Vector4i(this.y, this.z, this.w, this.y)
val Vector4i.gbab get() = Vector4i(this.y, this.z, this.w, this.z)
val Vector4i.gbaa get() = Vector4i(this.y, this.z, this.w, this.w)
val Vector4i.garr get() = Vector4i(this.y, this.w, this.x, this.x)
val Vector4i.garg get() = Vector4i(this.y, this.w, this.x, this.y)
var Vector4i.garb get() = Vector4i(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4i.gara get() = Vector4i(this.y, this.w, this.x, this.w)
val Vector4i.gagr get() = Vector4i(this.y, this.w, this.y, this.x)
val Vector4i.gagg get() = Vector4i(this.y, this.w, this.y, this.y)
val Vector4i.gagb get() = Vector4i(this.y, this.w, this.y, this.z)
val Vector4i.gaga get() = Vector4i(this.y, this.w, this.y, this.w)
var Vector4i.gabr get() = Vector4i(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4i.gabg get() = Vector4i(this.y, this.w, this.z, this.y)
val Vector4i.gabb get() = Vector4i(this.y, this.w, this.z, this.z)
val Vector4i.gaba get() = Vector4i(this.y, this.w, this.z, this.w)
val Vector4i.gaar get() = Vector4i(this.y, this.w, this.w, this.x)
val Vector4i.gaag get() = Vector4i(this.y, this.w, this.w, this.y)
val Vector4i.gaab get() = Vector4i(this.y, this.w, this.w, this.z)
val Vector4i.gaaa get() = Vector4i(this.y, this.w, this.w, this.w)
val Vector4i.brrr get() = Vector4i(this.z, this.x, this.x, this.x)
val Vector4i.brrg get() = Vector4i(this.z, this.x, this.x, this.y)
val Vector4i.brrb get() = Vector4i(this.z, this.x, this.x, this.z)
val Vector4i.brra get() = Vector4i(this.z, this.x, this.x, this.w)
val Vector4i.brgr get() = Vector4i(this.z, this.x, this.y, this.x)
val Vector4i.brgg get() = Vector4i(this.z, this.x, this.y, this.y)
val Vector4i.brgb get() = Vector4i(this.z, this.x, this.y, this.z)
var Vector4i.brga get() = Vector4i(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4i.brbr get() = Vector4i(this.z, this.x, this.z, this.x)
val Vector4i.brbg get() = Vector4i(this.z, this.x, this.z, this.y)
val Vector4i.brbb get() = Vector4i(this.z, this.x, this.z, this.z)
val Vector4i.brba get() = Vector4i(this.z, this.x, this.z, this.w)
val Vector4i.brar get() = Vector4i(this.z, this.x, this.w, this.x)
var Vector4i.brag get() = Vector4i(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4i.brab get() = Vector4i(this.z, this.x, this.w, this.z)
val Vector4i.braa get() = Vector4i(this.z, this.x, this.w, this.w)
val Vector4i.bgrr get() = Vector4i(this.z, this.y, this.x, this.x)
val Vector4i.bgrg get() = Vector4i(this.z, this.y, this.x, this.y)
val Vector4i.bgrb get() = Vector4i(this.z, this.y, this.x, this.z)
var Vector4i.bgra get() = Vector4i(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4i.bggr get() = Vector4i(this.z, this.y, this.y, this.x)
val Vector4i.bggg get() = Vector4i(this.z, this.y, this.y, this.y)
val Vector4i.bggb get() = Vector4i(this.z, this.y, this.y, this.z)
val Vector4i.bgga get() = Vector4i(this.z, this.y, this.y, this.w)
val Vector4i.bgbr get() = Vector4i(this.z, this.y, this.z, this.x)
val Vector4i.bgbg get() = Vector4i(this.z, this.y, this.z, this.y)
val Vector4i.bgbb get() = Vector4i(this.z, this.y, this.z, this.z)
val Vector4i.bgba get() = Vector4i(this.z, this.y, this.z, this.w)
var Vector4i.bgar get() = Vector4i(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4i.bgag get() = Vector4i(this.z, this.y, this.w, this.y)
val Vector4i.bgab get() = Vector4i(this.z, this.y, this.w, this.z)
val Vector4i.bgaa get() = Vector4i(this.z, this.y, this.w, this.w)
val Vector4i.bbrr get() = Vector4i(this.z, this.z, this.x, this.x)
val Vector4i.bbrg get() = Vector4i(this.z, this.z, this.x, this.y)
val Vector4i.bbrb get() = Vector4i(this.z, this.z, this.x, this.z)
val Vector4i.bbra get() = Vector4i(this.z, this.z, this.x, this.w)
val Vector4i.bbgr get() = Vector4i(this.z, this.z, this.y, this.x)
val Vector4i.bbgg get() = Vector4i(this.z, this.z, this.y, this.y)
val Vector4i.bbgb get() = Vector4i(this.z, this.z, this.y, this.z)
val Vector4i.bbga get() = Vector4i(this.z, this.z, this.y, this.w)
val Vector4i.bbbr get() = Vector4i(this.z, this.z, this.z, this.x)
val Vector4i.bbbg get() = Vector4i(this.z, this.z, this.z, this.y)
val Vector4i.bbbb get() = Vector4i(this.z, this.z, this.z, this.z)
val Vector4i.bbba get() = Vector4i(this.z, this.z, this.z, this.w)
val Vector4i.bbar get() = Vector4i(this.z, this.z, this.w, this.x)
val Vector4i.bbag get() = Vector4i(this.z, this.z, this.w, this.y)
val Vector4i.bbab get() = Vector4i(this.z, this.z, this.w, this.z)
val Vector4i.bbaa get() = Vector4i(this.z, this.z, this.w, this.w)
val Vector4i.barr get() = Vector4i(this.z, this.w, this.x, this.x)
var Vector4i.barg get() = Vector4i(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4i.barb get() = Vector4i(this.z, this.w, this.x, this.z)
val Vector4i.bara get() = Vector4i(this.z, this.w, this.x, this.w)
var Vector4i.bagr get() = Vector4i(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4i.bagg get() = Vector4i(this.z, this.w, this.y, this.y)
val Vector4i.bagb get() = Vector4i(this.z, this.w, this.y, this.z)
val Vector4i.baga get() = Vector4i(this.z, this.w, this.y, this.w)
val Vector4i.babr get() = Vector4i(this.z, this.w, this.z, this.x)
val Vector4i.babg get() = Vector4i(this.z, this.w, this.z, this.y)
val Vector4i.babb get() = Vector4i(this.z, this.w, this.z, this.z)
val Vector4i.baba get() = Vector4i(this.z, this.w, this.z, this.w)
val Vector4i.baar get() = Vector4i(this.z, this.w, this.w, this.x)
val Vector4i.baag get() = Vector4i(this.z, this.w, this.w, this.y)
val Vector4i.baab get() = Vector4i(this.z, this.w, this.w, this.z)
val Vector4i.baaa get() = Vector4i(this.z, this.w, this.w, this.w)
val Vector4i.arrr get() = Vector4i(this.w, this.x, this.x, this.x)
val Vector4i.arrg get() = Vector4i(this.w, this.x, this.x, this.y)
val Vector4i.arrb get() = Vector4i(this.w, this.x, this.x, this.z)
val Vector4i.arra get() = Vector4i(this.w, this.x, this.x, this.w)
val Vector4i.argr get() = Vector4i(this.w, this.x, this.y, this.x)
val Vector4i.argg get() = Vector4i(this.w, this.x, this.y, this.y)
var Vector4i.argb get() = Vector4i(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4i.arga get() = Vector4i(this.w, this.x, this.y, this.w)
val Vector4i.arbr get() = Vector4i(this.w, this.x, this.z, this.x)
var Vector4i.arbg get() = Vector4i(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4i.arbb get() = Vector4i(this.w, this.x, this.z, this.z)
val Vector4i.arba get() = Vector4i(this.w, this.x, this.z, this.w)
val Vector4i.arar get() = Vector4i(this.w, this.x, this.w, this.x)
val Vector4i.arag get() = Vector4i(this.w, this.x, this.w, this.y)
val Vector4i.arab get() = Vector4i(this.w, this.x, this.w, this.z)
val Vector4i.araa get() = Vector4i(this.w, this.x, this.w, this.w)
val Vector4i.agrr get() = Vector4i(this.w, this.y, this.x, this.x)
val Vector4i.agrg get() = Vector4i(this.w, this.y, this.x, this.y)
var Vector4i.agrb get() = Vector4i(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4i.agra get() = Vector4i(this.w, this.y, this.x, this.w)
val Vector4i.aggr get() = Vector4i(this.w, this.y, this.y, this.x)
val Vector4i.aggg get() = Vector4i(this.w, this.y, this.y, this.y)
val Vector4i.aggb get() = Vector4i(this.w, this.y, this.y, this.z)
val Vector4i.agga get() = Vector4i(this.w, this.y, this.y, this.w)
var Vector4i.agbr get() = Vector4i(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4i.agbg get() = Vector4i(this.w, this.y, this.z, this.y)
val Vector4i.agbb get() = Vector4i(this.w, this.y, this.z, this.z)
val Vector4i.agba get() = Vector4i(this.w, this.y, this.z, this.w)
val Vector4i.agar get() = Vector4i(this.w, this.y, this.w, this.x)
val Vector4i.agag get() = Vector4i(this.w, this.y, this.w, this.y)
val Vector4i.agab get() = Vector4i(this.w, this.y, this.w, this.z)
val Vector4i.agaa get() = Vector4i(this.w, this.y, this.w, this.w)
val Vector4i.abrr get() = Vector4i(this.w, this.z, this.x, this.x)
var Vector4i.abrg get() = Vector4i(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4i.abrb get() = Vector4i(this.w, this.z, this.x, this.z)
val Vector4i.abra get() = Vector4i(this.w, this.z, this.x, this.w)
var Vector4i.abgr get() = Vector4i(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4i.abgg get() = Vector4i(this.w, this.z, this.y, this.y)
val Vector4i.abgb get() = Vector4i(this.w, this.z, this.y, this.z)
val Vector4i.abga get() = Vector4i(this.w, this.z, this.y, this.w)
val Vector4i.abbr get() = Vector4i(this.w, this.z, this.z, this.x)
val Vector4i.abbg get() = Vector4i(this.w, this.z, this.z, this.y)
val Vector4i.abbb get() = Vector4i(this.w, this.z, this.z, this.z)
val Vector4i.abba get() = Vector4i(this.w, this.z, this.z, this.w)
val Vector4i.abar get() = Vector4i(this.w, this.z, this.w, this.x)
val Vector4i.abag get() = Vector4i(this.w, this.z, this.w, this.y)
val Vector4i.abab get() = Vector4i(this.w, this.z, this.w, this.z)
val Vector4i.abaa get() = Vector4i(this.w, this.z, this.w, this.w)
val Vector4i.aarr get() = Vector4i(this.w, this.w, this.x, this.x)
val Vector4i.aarg get() = Vector4i(this.w, this.w, this.x, this.y)
val Vector4i.aarb get() = Vector4i(this.w, this.w, this.x, this.z)
val Vector4i.aara get() = Vector4i(this.w, this.w, this.x, this.w)
val Vector4i.aagr get() = Vector4i(this.w, this.w, this.y, this.x)
val Vector4i.aagg get() = Vector4i(this.w, this.w, this.y, this.y)
val Vector4i.aagb get() = Vector4i(this.w, this.w, this.y, this.z)
val Vector4i.aaga get() = Vector4i(this.w, this.w, this.y, this.w)
val Vector4i.aabr get() = Vector4i(this.w, this.w, this.z, this.x)
val Vector4i.aabg get() = Vector4i(this.w, this.w, this.z, this.y)
val Vector4i.aabb get() = Vector4i(this.w, this.w, this.z, this.z)
val Vector4i.aaba get() = Vector4i(this.w, this.w, this.z, this.w)
val Vector4i.aaar get() = Vector4i(this.w, this.w, this.w, this.x)
val Vector4i.aaag get() = Vector4i(this.w, this.w, this.w, this.y)
val Vector4i.aaab get() = Vector4i(this.w, this.w, this.w, this.z)
val Vector4i.aaaa get() = Vector4i(this.w, this.w, this.w, this.w)