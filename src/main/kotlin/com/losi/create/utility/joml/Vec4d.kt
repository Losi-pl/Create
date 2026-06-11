@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.utility.Quad
import org.joml.*

fun Vector4d.toQuad() = Quad(this.x, this.y, this.z, this.w)
fun Quad<Double, Double, Double, Double>.toVector() = Vector4d(this.first, this.second, this.third, this.fourth)

// ===================================== XYZW =====================================
val Vector4d.xx get() = Vector2d(this.x, this.x)
var Vector4d.xy get() = Vector2d(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4d.xz get() = Vector2d(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4d.xw get() = Vector2d(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4d.yx get() = Vector2d(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4d.yy get() = Vector2d(this.y, this.y)
var Vector4d.yz get() = Vector2d(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4d.yw get() = Vector2d(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4d.zx get() = Vector2d(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4d.zy get() = Vector2d(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4d.zz get() = Vector2d(this.z, this.z)
var Vector4d.zw get() = Vector2d(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4d.wx get() = Vector2d(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4d.wy get() = Vector2d(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4d.wz get() = Vector2d(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4d.ww get() = Vector2d(this.w, this.w)

val Vector4d.xxx get() = Vector3d(this.x, this.x, this.x)
val Vector4d.xxy get() = Vector3d(this.x, this.x, this.y)
val Vector4d.xxz get() = Vector3d(this.x, this.x, this.z)
val Vector4d.xxw get() = Vector3d(this.x, this.x, this.w)
val Vector4d.xyx get() = Vector3d(this.x, this.y, this.x)
val Vector4d.xyy get() = Vector3d(this.x, this.y, this.y)
var Vector4d.xyz get() = Vector3d(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4d.xyw get() = Vector3d(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4d.xzx get() = Vector3d(this.x, this.z, this.x)
var Vector4d.xzy get() = Vector3d(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4d.xzz get() = Vector3d(this.x, this.z, this.z)
var Vector4d.xzw get() = Vector3d(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4d.xwx get() = Vector3d(this.x, this.w, this.x)
var Vector4d.xwy get() = Vector3d(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4d.xwz get() = Vector3d(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4d.xww get() = Vector3d(this.x, this.w, this.w)
val Vector4d.yxx get() = Vector3d(this.y, this.x, this.x)
val Vector4d.yxy get() = Vector3d(this.y, this.x, this.y)
var Vector4d.yxz get() = Vector3d(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4d.yxw get() = Vector3d(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4d.yyx get() = Vector3d(this.y, this.y, this.x)
val Vector4d.yyy get() = Vector3d(this.y, this.y, this.y)
val Vector4d.yyz get() = Vector3d(this.y, this.y, this.z)
val Vector4d.yyw get() = Vector3d(this.y, this.y, this.w)
var Vector4d.yzx get() = Vector3d(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4d.yzy get() = Vector3d(this.y, this.z, this.y)
val Vector4d.yzz get() = Vector3d(this.y, this.z, this.z)
var Vector4d.yzw get() = Vector3d(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4d.ywx get() = Vector3d(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4d.ywy get() = Vector3d(this.y, this.w, this.y)
var Vector4d.ywz get() = Vector3d(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4d.yww get() = Vector3d(this.y, this.w, this.w)
val Vector4d.zxx get() = Vector3d(this.z, this.x, this.x)
var Vector4d.zxy get() = Vector3d(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4d.zxz get() = Vector3d(this.z, this.x, this.z)
var Vector4d.zxw get() = Vector3d(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4d.zyx get() = Vector3d(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4d.zyy get() = Vector3d(this.z, this.y, this.y)
val Vector4d.zyz get() = Vector3d(this.z, this.y, this.z)
var Vector4d.zyw get() = Vector3d(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4d.zzx get() = Vector3d(this.z, this.z, this.x)
val Vector4d.zzy get() = Vector3d(this.z, this.z, this.y)
val Vector4d.zzz get() = Vector3d(this.z, this.z, this.z)
val Vector4d.zzw get() = Vector3d(this.z, this.z, this.w)
var Vector4d.zwx get() = Vector3d(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4d.zwy get() = Vector3d(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4d.zwz get() = Vector3d(this.z, this.w, this.z)
val Vector4d.zww get() = Vector3d(this.z, this.w, this.w)
val Vector4d.wxx get() = Vector3d(this.w, this.x, this.x)
var Vector4d.wxy get() = Vector3d(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4d.wxz get() = Vector3d(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4d.wxw get() = Vector3d(this.w, this.x, this.w)
var Vector4d.wyx get() = Vector3d(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4d.wyy get() = Vector3d(this.w, this.y, this.y)
var Vector4d.wyz get() = Vector3d(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4d.wyw get() = Vector3d(this.w, this.y, this.w)
var Vector4d.wzx get() = Vector3d(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4d.wzy get() = Vector3d(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4d.wzz get() = Vector3d(this.w, this.z, this.z)
val Vector4d.wzw get() = Vector3d(this.w, this.z, this.w)
val Vector4d.wwx get() = Vector3d(this.w, this.w, this.x)
val Vector4d.wwy get() = Vector3d(this.w, this.w, this.y)
val Vector4d.wwz get() = Vector3d(this.w, this.w, this.z)
val Vector4d.www get() = Vector3d(this.w, this.w, this.w)

val Vector4d.xxxx get() = Vector4d(this.x, this.x, this.x, this.x)
val Vector4d.xxxy get() = Vector4d(this.x, this.x, this.x, this.y)
val Vector4d.xxxz get() = Vector4d(this.x, this.x, this.x, this.z)
val Vector4d.xxxw get() = Vector4d(this.x, this.x, this.x, this.w)
val Vector4d.xxyx get() = Vector4d(this.x, this.x, this.y, this.x)
val Vector4d.xxyy get() = Vector4d(this.x, this.x, this.y, this.y)
val Vector4d.xxyz get() = Vector4d(this.x, this.x, this.y, this.z)
val Vector4d.xxyw get() = Vector4d(this.x, this.x, this.y, this.w)
val Vector4d.xxzx get() = Vector4d(this.x, this.x, this.z, this.x)
val Vector4d.xxzy get() = Vector4d(this.x, this.x, this.z, this.y)
val Vector4d.xxzz get() = Vector4d(this.x, this.x, this.z, this.z)
val Vector4d.xxzw get() = Vector4d(this.x, this.x, this.z, this.w)
val Vector4d.xxwx get() = Vector4d(this.x, this.x, this.w, this.x)
val Vector4d.xxwy get() = Vector4d(this.x, this.x, this.w, this.y)
val Vector4d.xxwz get() = Vector4d(this.x, this.x, this.w, this.z)
val Vector4d.xxww get() = Vector4d(this.x, this.x, this.w, this.w)
val Vector4d.xyxx get() = Vector4d(this.x, this.y, this.x, this.x)
val Vector4d.xyxy get() = Vector4d(this.x, this.y, this.x, this.y)
val Vector4d.xyxz get() = Vector4d(this.x, this.y, this.x, this.z)
val Vector4d.xyxw get() = Vector4d(this.x, this.y, this.x, this.w)
val Vector4d.xyyx get() = Vector4d(this.x, this.y, this.y, this.x)
val Vector4d.xyyy get() = Vector4d(this.x, this.y, this.y, this.y)
val Vector4d.xyyz get() = Vector4d(this.x, this.y, this.y, this.z)
val Vector4d.xyyw get() = Vector4d(this.x, this.y, this.y, this.w)
val Vector4d.xyzx get() = Vector4d(this.x, this.y, this.z, this.x)
val Vector4d.xyzy get() = Vector4d(this.x, this.y, this.z, this.y)
val Vector4d.xyzz get() = Vector4d(this.x, this.y, this.z, this.z)
var Vector4d.xyzw get() = Vector4d(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4d.xywx get() = Vector4d(this.x, this.y, this.w, this.x)
val Vector4d.xywy get() = Vector4d(this.x, this.y, this.w, this.y)
var Vector4d.xywz get() = Vector4d(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4d.xyww get() = Vector4d(this.x, this.y, this.w, this.w)
val Vector4d.xzxx get() = Vector4d(this.x, this.z, this.x, this.x)
val Vector4d.xzxy get() = Vector4d(this.x, this.z, this.x, this.y)
val Vector4d.xzxz get() = Vector4d(this.x, this.z, this.x, this.z)
val Vector4d.xzxw get() = Vector4d(this.x, this.z, this.x, this.w)
val Vector4d.xzyx get() = Vector4d(this.x, this.z, this.y, this.x)
val Vector4d.xzyy get() = Vector4d(this.x, this.z, this.y, this.y)
val Vector4d.xzyz get() = Vector4d(this.x, this.z, this.y, this.z)
var Vector4d.xzyw get() = Vector4d(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4d.xzzx get() = Vector4d(this.x, this.z, this.z, this.x)
val Vector4d.xzzy get() = Vector4d(this.x, this.z, this.z, this.y)
val Vector4d.xzzz get() = Vector4d(this.x, this.z, this.z, this.z)
val Vector4d.xzzw get() = Vector4d(this.x, this.z, this.z, this.w)
val Vector4d.xzwx get() = Vector4d(this.x, this.z, this.w, this.x)
var Vector4d.xzwy get() = Vector4d(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4d.xzwz get() = Vector4d(this.x, this.z, this.w, this.z)
val Vector4d.xzww get() = Vector4d(this.x, this.z, this.w, this.w)
val Vector4d.xwxx get() = Vector4d(this.x, this.w, this.x, this.x)
val Vector4d.xwxy get() = Vector4d(this.x, this.w, this.x, this.y)
val Vector4d.xwxz get() = Vector4d(this.x, this.w, this.x, this.z)
val Vector4d.xwxw get() = Vector4d(this.x, this.w, this.x, this.w)
val Vector4d.xwyx get() = Vector4d(this.x, this.w, this.y, this.x)
val Vector4d.xwyy get() = Vector4d(this.x, this.w, this.y, this.y)
var Vector4d.xwyz get() = Vector4d(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4d.xwyw get() = Vector4d(this.x, this.w, this.y, this.w)
val Vector4d.xwzx get() = Vector4d(this.x, this.w, this.z, this.x)
var Vector4d.xwzy get() = Vector4d(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4d.xwzz get() = Vector4d(this.x, this.w, this.z, this.z)
val Vector4d.xwzw get() = Vector4d(this.x, this.w, this.z, this.w)
val Vector4d.xwwx get() = Vector4d(this.x, this.w, this.w, this.x)
val Vector4d.xwwy get() = Vector4d(this.x, this.w, this.w, this.y)
val Vector4d.xwwz get() = Vector4d(this.x, this.w, this.w, this.z)
val Vector4d.xwww get() = Vector4d(this.x, this.w, this.w, this.w)
val Vector4d.yxxx get() = Vector4d(this.y, this.x, this.x, this.x)
val Vector4d.yxxy get() = Vector4d(this.y, this.x, this.x, this.y)
val Vector4d.yxxz get() = Vector4d(this.y, this.x, this.x, this.z)
val Vector4d.yxxw get() = Vector4d(this.y, this.x, this.x, this.w)
val Vector4d.yxyx get() = Vector4d(this.y, this.x, this.y, this.x)
val Vector4d.yxyy get() = Vector4d(this.y, this.x, this.y, this.y)
val Vector4d.yxyz get() = Vector4d(this.y, this.x, this.y, this.z)
val Vector4d.yxyw get() = Vector4d(this.y, this.x, this.y, this.w)
val Vector4d.yxzx get() = Vector4d(this.y, this.x, this.z, this.x)
val Vector4d.yxzy get() = Vector4d(this.y, this.x, this.z, this.y)
val Vector4d.yxzz get() = Vector4d(this.y, this.x, this.z, this.z)
var Vector4d.yxzw get() = Vector4d(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4d.yxwx get() = Vector4d(this.y, this.x, this.w, this.x)
val Vector4d.yxwy get() = Vector4d(this.y, this.x, this.w, this.y)
var Vector4d.yxwz get() = Vector4d(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4d.yxww get() = Vector4d(this.y, this.x, this.w, this.w)
val Vector4d.yyxx get() = Vector4d(this.y, this.y, this.x, this.x)
val Vector4d.yyxy get() = Vector4d(this.y, this.y, this.x, this.y)
val Vector4d.yyxz get() = Vector4d(this.y, this.y, this.x, this.z)
val Vector4d.yyxw get() = Vector4d(this.y, this.y, this.x, this.w)
val Vector4d.yyyx get() = Vector4d(this.y, this.y, this.y, this.x)
val Vector4d.yyyy get() = Vector4d(this.y, this.y, this.y, this.y)
val Vector4d.yyyz get() = Vector4d(this.y, this.y, this.y, this.z)
val Vector4d.yyyw get() = Vector4d(this.y, this.y, this.y, this.w)
val Vector4d.yyzx get() = Vector4d(this.y, this.y, this.z, this.x)
val Vector4d.yyzy get() = Vector4d(this.y, this.y, this.z, this.y)
val Vector4d.yyzz get() = Vector4d(this.y, this.y, this.z, this.z)
val Vector4d.yyzw get() = Vector4d(this.y, this.y, this.z, this.w)
val Vector4d.yywx get() = Vector4d(this.y, this.y, this.w, this.x)
val Vector4d.yywy get() = Vector4d(this.y, this.y, this.w, this.y)
val Vector4d.yywz get() = Vector4d(this.y, this.y, this.w, this.z)
val Vector4d.yyww get() = Vector4d(this.y, this.y, this.w, this.w)
val Vector4d.yzxx get() = Vector4d(this.y, this.z, this.x, this.x)
val Vector4d.yzxy get() = Vector4d(this.y, this.z, this.x, this.y)
val Vector4d.yzxz get() = Vector4d(this.y, this.z, this.x, this.z)
var Vector4d.yzxw get() = Vector4d(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4d.yzyx get() = Vector4d(this.y, this.z, this.y, this.x)
val Vector4d.yzyy get() = Vector4d(this.y, this.z, this.y, this.y)
val Vector4d.yzyz get() = Vector4d(this.y, this.z, this.y, this.z)
val Vector4d.yzyw get() = Vector4d(this.y, this.z, this.y, this.w)
val Vector4d.yzzx get() = Vector4d(this.y, this.z, this.z, this.x)
val Vector4d.yzzy get() = Vector4d(this.y, this.z, this.z, this.y)
val Vector4d.yzzz get() = Vector4d(this.y, this.z, this.z, this.z)
val Vector4d.yzzw get() = Vector4d(this.y, this.z, this.z, this.w)
var Vector4d.yzwx get() = Vector4d(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4d.yzwy get() = Vector4d(this.y, this.z, this.w, this.y)
val Vector4d.yzwz get() = Vector4d(this.y, this.z, this.w, this.z)
val Vector4d.yzww get() = Vector4d(this.y, this.z, this.w, this.w)
val Vector4d.ywxx get() = Vector4d(this.y, this.w, this.x, this.x)
val Vector4d.ywxy get() = Vector4d(this.y, this.w, this.x, this.y)
var Vector4d.ywxz get() = Vector4d(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4d.ywxw get() = Vector4d(this.y, this.w, this.x, this.w)
val Vector4d.ywyx get() = Vector4d(this.y, this.w, this.y, this.x)
val Vector4d.ywyy get() = Vector4d(this.y, this.w, this.y, this.y)
val Vector4d.ywyz get() = Vector4d(this.y, this.w, this.y, this.z)
val Vector4d.ywyw get() = Vector4d(this.y, this.w, this.y, this.w)
var Vector4d.ywzx get() = Vector4d(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4d.ywzy get() = Vector4d(this.y, this.w, this.z, this.y)
val Vector4d.ywzz get() = Vector4d(this.y, this.w, this.z, this.z)
val Vector4d.ywzw get() = Vector4d(this.y, this.w, this.z, this.w)
val Vector4d.ywwx get() = Vector4d(this.y, this.w, this.w, this.x)
val Vector4d.ywwy get() = Vector4d(this.y, this.w, this.w, this.y)
val Vector4d.ywwz get() = Vector4d(this.y, this.w, this.w, this.z)
val Vector4d.ywww get() = Vector4d(this.y, this.w, this.w, this.w)
val Vector4d.zxxx get() = Vector4d(this.z, this.x, this.x, this.x)
val Vector4d.zxxy get() = Vector4d(this.z, this.x, this.x, this.y)
val Vector4d.zxxz get() = Vector4d(this.z, this.x, this.x, this.z)
val Vector4d.zxxw get() = Vector4d(this.z, this.x, this.x, this.w)
val Vector4d.zxyx get() = Vector4d(this.z, this.x, this.y, this.x)
val Vector4d.zxyy get() = Vector4d(this.z, this.x, this.y, this.y)
val Vector4d.zxyz get() = Vector4d(this.z, this.x, this.y, this.z)
var Vector4d.zxyw get() = Vector4d(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4d.zxzx get() = Vector4d(this.z, this.x, this.z, this.x)
val Vector4d.zxzy get() = Vector4d(this.z, this.x, this.z, this.y)
val Vector4d.zxzz get() = Vector4d(this.z, this.x, this.z, this.z)
val Vector4d.zxzw get() = Vector4d(this.z, this.x, this.z, this.w)
val Vector4d.zxwx get() = Vector4d(this.z, this.x, this.w, this.x)
var Vector4d.zxwy get() = Vector4d(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4d.zxwz get() = Vector4d(this.z, this.x, this.w, this.z)
val Vector4d.zxww get() = Vector4d(this.z, this.x, this.w, this.w)
val Vector4d.zyxx get() = Vector4d(this.z, this.y, this.x, this.x)
val Vector4d.zyxy get() = Vector4d(this.z, this.y, this.x, this.y)
val Vector4d.zyxz get() = Vector4d(this.z, this.y, this.x, this.z)
var Vector4d.zyxw get() = Vector4d(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4d.zyyx get() = Vector4d(this.z, this.y, this.y, this.x)
val Vector4d.zyyy get() = Vector4d(this.z, this.y, this.y, this.y)
val Vector4d.zyyz get() = Vector4d(this.z, this.y, this.y, this.z)
val Vector4d.zyyw get() = Vector4d(this.z, this.y, this.y, this.w)
val Vector4d.zyzx get() = Vector4d(this.z, this.y, this.z, this.x)
val Vector4d.zyzy get() = Vector4d(this.z, this.y, this.z, this.y)
val Vector4d.zyzz get() = Vector4d(this.z, this.y, this.z, this.z)
val Vector4d.zyzw get() = Vector4d(this.z, this.y, this.z, this.w)
var Vector4d.zywx get() = Vector4d(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4d.zywy get() = Vector4d(this.z, this.y, this.w, this.y)
val Vector4d.zywz get() = Vector4d(this.z, this.y, this.w, this.z)
val Vector4d.zyww get() = Vector4d(this.z, this.y, this.w, this.w)
val Vector4d.zzxx get() = Vector4d(this.z, this.z, this.x, this.x)
val Vector4d.zzxy get() = Vector4d(this.z, this.z, this.x, this.y)
val Vector4d.zzxz get() = Vector4d(this.z, this.z, this.x, this.z)
val Vector4d.zzxw get() = Vector4d(this.z, this.z, this.x, this.w)
val Vector4d.zzyx get() = Vector4d(this.z, this.z, this.y, this.x)
val Vector4d.zzyy get() = Vector4d(this.z, this.z, this.y, this.y)
val Vector4d.zzyz get() = Vector4d(this.z, this.z, this.y, this.z)
val Vector4d.zzyw get() = Vector4d(this.z, this.z, this.y, this.w)
val Vector4d.zzzx get() = Vector4d(this.z, this.z, this.z, this.x)
val Vector4d.zzzy get() = Vector4d(this.z, this.z, this.z, this.y)
val Vector4d.zzzz get() = Vector4d(this.z, this.z, this.z, this.z)
val Vector4d.zzzw get() = Vector4d(this.z, this.z, this.z, this.w)
val Vector4d.zzwx get() = Vector4d(this.z, this.z, this.w, this.x)
val Vector4d.zzwy get() = Vector4d(this.z, this.z, this.w, this.y)
val Vector4d.zzwz get() = Vector4d(this.z, this.z, this.w, this.z)
val Vector4d.zzww get() = Vector4d(this.z, this.z, this.w, this.w)
val Vector4d.zwxx get() = Vector4d(this.z, this.w, this.x, this.x)
var Vector4d.zwxy get() = Vector4d(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4d.zwxz get() = Vector4d(this.z, this.w, this.x, this.z)
val Vector4d.zwxw get() = Vector4d(this.z, this.w, this.x, this.w)
var Vector4d.zwyx get() = Vector4d(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4d.zwyy get() = Vector4d(this.z, this.w, this.y, this.y)
val Vector4d.zwyz get() = Vector4d(this.z, this.w, this.y, this.z)
val Vector4d.zwyw get() = Vector4d(this.z, this.w, this.y, this.w)
val Vector4d.zwzx get() = Vector4d(this.z, this.w, this.z, this.x)
val Vector4d.zwzy get() = Vector4d(this.z, this.w, this.z, this.y)
val Vector4d.zwzz get() = Vector4d(this.z, this.w, this.z, this.z)
val Vector4d.zwzw get() = Vector4d(this.z, this.w, this.z, this.w)
val Vector4d.zwwx get() = Vector4d(this.z, this.w, this.w, this.x)
val Vector4d.zwwy get() = Vector4d(this.z, this.w, this.w, this.y)
val Vector4d.zwwz get() = Vector4d(this.z, this.w, this.w, this.z)
val Vector4d.zwww get() = Vector4d(this.z, this.w, this.w, this.w)
val Vector4d.wxxx get() = Vector4d(this.w, this.x, this.x, this.x)
val Vector4d.wxxy get() = Vector4d(this.w, this.x, this.x, this.y)
val Vector4d.wxxz get() = Vector4d(this.w, this.x, this.x, this.z)
val Vector4d.wxxw get() = Vector4d(this.w, this.x, this.x, this.w)
val Vector4d.wxyx get() = Vector4d(this.w, this.x, this.y, this.x)
val Vector4d.wxyy get() = Vector4d(this.w, this.x, this.y, this.y)
var Vector4d.wxyz get() = Vector4d(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4d.wxyw get() = Vector4d(this.w, this.x, this.y, this.w)
val Vector4d.wxzx get() = Vector4d(this.w, this.x, this.z, this.x)
var Vector4d.wxzy get() = Vector4d(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4d.wxzz get() = Vector4d(this.w, this.x, this.z, this.z)
val Vector4d.wxzw get() = Vector4d(this.w, this.x, this.z, this.w)
val Vector4d.wxwx get() = Vector4d(this.w, this.x, this.w, this.x)
val Vector4d.wxwy get() = Vector4d(this.w, this.x, this.w, this.y)
val Vector4d.wxwz get() = Vector4d(this.w, this.x, this.w, this.z)
val Vector4d.wxww get() = Vector4d(this.w, this.x, this.w, this.w)
val Vector4d.wyxx get() = Vector4d(this.w, this.y, this.x, this.x)
val Vector4d.wyxy get() = Vector4d(this.w, this.y, this.x, this.y)
var Vector4d.wyxz get() = Vector4d(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4d.wyxw get() = Vector4d(this.w, this.y, this.x, this.w)
val Vector4d.wyyx get() = Vector4d(this.w, this.y, this.y, this.x)
val Vector4d.wyyy get() = Vector4d(this.w, this.y, this.y, this.y)
val Vector4d.wyyz get() = Vector4d(this.w, this.y, this.y, this.z)
val Vector4d.wyyw get() = Vector4d(this.w, this.y, this.y, this.w)
var Vector4d.wyzx get() = Vector4d(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4d.wyzy get() = Vector4d(this.w, this.y, this.z, this.y)
val Vector4d.wyzz get() = Vector4d(this.w, this.y, this.z, this.z)
val Vector4d.wyzw get() = Vector4d(this.w, this.y, this.z, this.w)
val Vector4d.wywx get() = Vector4d(this.w, this.y, this.w, this.x)
val Vector4d.wywy get() = Vector4d(this.w, this.y, this.w, this.y)
val Vector4d.wywz get() = Vector4d(this.w, this.y, this.w, this.z)
val Vector4d.wyww get() = Vector4d(this.w, this.y, this.w, this.w)
val Vector4d.wzxx get() = Vector4d(this.w, this.z, this.x, this.x)
var Vector4d.wzxy get() = Vector4d(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4d.wzxz get() = Vector4d(this.w, this.z, this.x, this.z)
val Vector4d.wzxw get() = Vector4d(this.w, this.z, this.x, this.w)
var Vector4d.wzyx get() = Vector4d(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4d.wzyy get() = Vector4d(this.w, this.z, this.y, this.y)
val Vector4d.wzyz get() = Vector4d(this.w, this.z, this.y, this.z)
val Vector4d.wzyw get() = Vector4d(this.w, this.z, this.y, this.w)
val Vector4d.wzzx get() = Vector4d(this.w, this.z, this.z, this.x)
val Vector4d.wzzy get() = Vector4d(this.w, this.z, this.z, this.y)
val Vector4d.wzzz get() = Vector4d(this.w, this.z, this.z, this.z)
val Vector4d.wzzw get() = Vector4d(this.w, this.z, this.z, this.w)
val Vector4d.wzwx get() = Vector4d(this.w, this.z, this.w, this.x)
val Vector4d.wzwy get() = Vector4d(this.w, this.z, this.w, this.y)
val Vector4d.wzwz get() = Vector4d(this.w, this.z, this.w, this.z)
val Vector4d.wzww get() = Vector4d(this.w, this.z, this.w, this.w)
val Vector4d.wwxx get() = Vector4d(this.w, this.w, this.x, this.x)
val Vector4d.wwxy get() = Vector4d(this.w, this.w, this.x, this.y)
val Vector4d.wwxz get() = Vector4d(this.w, this.w, this.x, this.z)
val Vector4d.wwxw get() = Vector4d(this.w, this.w, this.x, this.w)
val Vector4d.wwyx get() = Vector4d(this.w, this.w, this.y, this.x)
val Vector4d.wwyy get() = Vector4d(this.w, this.w, this.y, this.y)
val Vector4d.wwyz get() = Vector4d(this.w, this.w, this.y, this.z)
val Vector4d.wwyw get() = Vector4d(this.w, this.w, this.y, this.w)
val Vector4d.wwzx get() = Vector4d(this.w, this.w, this.z, this.x)
val Vector4d.wwzy get() = Vector4d(this.w, this.w, this.z, this.y)
val Vector4d.wwzz get() = Vector4d(this.w, this.w, this.z, this.z)
val Vector4d.wwzw get() = Vector4d(this.w, this.w, this.z, this.w)
val Vector4d.wwwx get() = Vector4d(this.w, this.w, this.w, this.x)
val Vector4d.wwwy get() = Vector4d(this.w, this.w, this.w, this.y)
val Vector4d.wwwz get() = Vector4d(this.w, this.w, this.w, this.z)
val Vector4d.wwww get() = Vector4d(this.w, this.w, this.w, this.w)

// ===================================== RGBA =====================================
var Vector4d.r: Double get() = this.x; set(it) { this.x = it }
var Vector4d.g: Double get() = this.y; set(it) { this.y = it }
var Vector4d.b: Double get() = this.z; set(it) { this.z = it }
var Vector4d.a: Double get() = this.w; set(it) { this.w = it }

val Vector4d.rr get() = Vector2d(this.x, this.x)
var Vector4d.rg get() = Vector2d(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4d.rb get() = Vector2d(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4d.ra get() = Vector2d(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4d.gr get() = Vector2d(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4d.gg get() = Vector2d(this.y, this.y)
var Vector4d.gb get() = Vector2d(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4d.ga get() = Vector2d(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4d.br get() = Vector2d(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4d.bg get() = Vector2d(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4d.bb get() = Vector2d(this.z, this.z)
var Vector4d.ba get() = Vector2d(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4d.ar get() = Vector2d(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4d.ag get() = Vector2d(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4d.ab get() = Vector2d(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4d.aa get() = Vector2d(this.w, this.w)

val Vector4d.rrr get() = Vector3d(this.x, this.x, this.x)
val Vector4d.rrg get() = Vector3d(this.x, this.x, this.y)
val Vector4d.rrb get() = Vector3d(this.x, this.x, this.z)
val Vector4d.rra get() = Vector3d(this.x, this.x, this.w)
val Vector4d.rgr get() = Vector3d(this.x, this.y, this.x)
val Vector4d.rgg get() = Vector3d(this.x, this.y, this.y)
var Vector4d.rgb get() = Vector3d(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4d.rga get() = Vector3d(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4d.rbr get() = Vector3d(this.x, this.z, this.x)
var Vector4d.rbg get() = Vector3d(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4d.rbb get() = Vector3d(this.x, this.z, this.z)
var Vector4d.rba get() = Vector3d(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4d.rar get() = Vector3d(this.x, this.w, this.x)
var Vector4d.rag get() = Vector3d(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4d.rab get() = Vector3d(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4d.raa get() = Vector3d(this.x, this.w, this.w)
val Vector4d.grr get() = Vector3d(this.y, this.x, this.x)
val Vector4d.grg get() = Vector3d(this.y, this.x, this.y)
var Vector4d.grb get() = Vector3d(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4d.gra get() = Vector3d(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4d.ggr get() = Vector3d(this.y, this.y, this.x)
val Vector4d.ggg get() = Vector3d(this.y, this.y, this.y)
val Vector4d.ggb get() = Vector3d(this.y, this.y, this.z)
val Vector4d.gga get() = Vector3d(this.y, this.y, this.w)
var Vector4d.gbr get() = Vector3d(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4d.gbg get() = Vector3d(this.y, this.z, this.y)
val Vector4d.gbb get() = Vector3d(this.y, this.z, this.z)
var Vector4d.gba get() = Vector3d(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4d.gar get() = Vector3d(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4d.gag get() = Vector3d(this.y, this.w, this.y)
var Vector4d.gab get() = Vector3d(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4d.gaa get() = Vector3d(this.y, this.w, this.w)
val Vector4d.brr get() = Vector3d(this.z, this.x, this.x)
var Vector4d.brg get() = Vector3d(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4d.brb get() = Vector3d(this.z, this.x, this.z)
var Vector4d.bra get() = Vector3d(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4d.bgr get() = Vector3d(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4d.bgg get() = Vector3d(this.z, this.y, this.y)
val Vector4d.bgb get() = Vector3d(this.z, this.y, this.z)
var Vector4d.bga get() = Vector3d(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4d.bbr get() = Vector3d(this.z, this.z, this.x)
val Vector4d.bbg get() = Vector3d(this.z, this.z, this.y)
val Vector4d.bbb get() = Vector3d(this.z, this.z, this.z)
val Vector4d.bba get() = Vector3d(this.z, this.z, this.w)
var Vector4d.bar get() = Vector3d(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4d.bag get() = Vector3d(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4d.bab get() = Vector3d(this.z, this.w, this.z)
val Vector4d.baa get() = Vector3d(this.z, this.w, this.w)
val Vector4d.arr get() = Vector3d(this.w, this.x, this.x)
var Vector4d.arg get() = Vector3d(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4d.arb get() = Vector3d(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4d.ara get() = Vector3d(this.w, this.x, this.w)
var Vector4d.agr get() = Vector3d(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4d.agg get() = Vector3d(this.w, this.y, this.y)
var Vector4d.agb get() = Vector3d(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4d.aga get() = Vector3d(this.w, this.y, this.w)
var Vector4d.abr get() = Vector3d(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4d.abg get() = Vector3d(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4d.abb get() = Vector3d(this.w, this.z, this.z)
val Vector4d.aba get() = Vector3d(this.w, this.z, this.w)
val Vector4d.aar get() = Vector3d(this.w, this.w, this.x)
val Vector4d.aag get() = Vector3d(this.w, this.w, this.y)
val Vector4d.aab get() = Vector3d(this.w, this.w, this.z)
val Vector4d.aaa get() = Vector3d(this.w, this.w, this.w)

val Vector4d.rrrr get() = Vector4d(this.x, this.x, this.x, this.x)
val Vector4d.rrrg get() = Vector4d(this.x, this.x, this.x, this.y)
val Vector4d.rrrb get() = Vector4d(this.x, this.x, this.x, this.z)
val Vector4d.rrra get() = Vector4d(this.x, this.x, this.x, this.w)
val Vector4d.rrgr get() = Vector4d(this.x, this.x, this.y, this.x)
val Vector4d.rrgg get() = Vector4d(this.x, this.x, this.y, this.y)
val Vector4d.rrgb get() = Vector4d(this.x, this.x, this.y, this.z)
val Vector4d.rrga get() = Vector4d(this.x, this.x, this.y, this.w)
val Vector4d.rrbr get() = Vector4d(this.x, this.x, this.z, this.x)
val Vector4d.rrbg get() = Vector4d(this.x, this.x, this.z, this.y)
val Vector4d.rrbb get() = Vector4d(this.x, this.x, this.z, this.z)
val Vector4d.rrba get() = Vector4d(this.x, this.x, this.z, this.w)
val Vector4d.rrar get() = Vector4d(this.x, this.x, this.w, this.x)
val Vector4d.rrag get() = Vector4d(this.x, this.x, this.w, this.y)
val Vector4d.rrab get() = Vector4d(this.x, this.x, this.w, this.z)
val Vector4d.rraa get() = Vector4d(this.x, this.x, this.w, this.w)
val Vector4d.rgrr get() = Vector4d(this.x, this.y, this.x, this.x)
val Vector4d.rgrg get() = Vector4d(this.x, this.y, this.x, this.y)
val Vector4d.rgrb get() = Vector4d(this.x, this.y, this.x, this.z)
val Vector4d.rgra get() = Vector4d(this.x, this.y, this.x, this.w)
val Vector4d.rggr get() = Vector4d(this.x, this.y, this.y, this.x)
val Vector4d.rggg get() = Vector4d(this.x, this.y, this.y, this.y)
val Vector4d.rggb get() = Vector4d(this.x, this.y, this.y, this.z)
val Vector4d.rgga get() = Vector4d(this.x, this.y, this.y, this.w)
val Vector4d.rgbr get() = Vector4d(this.x, this.y, this.z, this.x)
val Vector4d.rgbg get() = Vector4d(this.x, this.y, this.z, this.y)
val Vector4d.rgbb get() = Vector4d(this.x, this.y, this.z, this.z)
var Vector4d.rgba get() = Vector4d(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4d.rgar get() = Vector4d(this.x, this.y, this.w, this.x)
val Vector4d.rgag get() = Vector4d(this.x, this.y, this.w, this.y)
var Vector4d.rgab get() = Vector4d(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4d.rgaa get() = Vector4d(this.x, this.y, this.w, this.w)
val Vector4d.rbrr get() = Vector4d(this.x, this.z, this.x, this.x)
val Vector4d.rbrg get() = Vector4d(this.x, this.z, this.x, this.y)
val Vector4d.rbrb get() = Vector4d(this.x, this.z, this.x, this.z)
val Vector4d.rbra get() = Vector4d(this.x, this.z, this.x, this.w)
val Vector4d.rbgr get() = Vector4d(this.x, this.z, this.y, this.x)
val Vector4d.rbgg get() = Vector4d(this.x, this.z, this.y, this.y)
val Vector4d.rbgb get() = Vector4d(this.x, this.z, this.y, this.z)
var Vector4d.rbga get() = Vector4d(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4d.rbbr get() = Vector4d(this.x, this.z, this.z, this.x)
val Vector4d.rbbg get() = Vector4d(this.x, this.z, this.z, this.y)
val Vector4d.rbbb get() = Vector4d(this.x, this.z, this.z, this.z)
val Vector4d.rbba get() = Vector4d(this.x, this.z, this.z, this.w)
val Vector4d.rbar get() = Vector4d(this.x, this.z, this.w, this.x)
var Vector4d.rbag get() = Vector4d(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4d.rbab get() = Vector4d(this.x, this.z, this.w, this.z)
val Vector4d.rbaa get() = Vector4d(this.x, this.z, this.w, this.w)
val Vector4d.rarr get() = Vector4d(this.x, this.w, this.x, this.x)
val Vector4d.rarg get() = Vector4d(this.x, this.w, this.x, this.y)
val Vector4d.rarb get() = Vector4d(this.x, this.w, this.x, this.z)
val Vector4d.rara get() = Vector4d(this.x, this.w, this.x, this.w)
val Vector4d.ragr get() = Vector4d(this.x, this.w, this.y, this.x)
val Vector4d.ragg get() = Vector4d(this.x, this.w, this.y, this.y)
var Vector4d.ragb get() = Vector4d(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4d.raga get() = Vector4d(this.x, this.w, this.y, this.w)
val Vector4d.rabr get() = Vector4d(this.x, this.w, this.z, this.x)
var Vector4d.rabg get() = Vector4d(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4d.rabb get() = Vector4d(this.x, this.w, this.z, this.z)
val Vector4d.raba get() = Vector4d(this.x, this.w, this.z, this.w)
val Vector4d.raar get() = Vector4d(this.x, this.w, this.w, this.x)
val Vector4d.raag get() = Vector4d(this.x, this.w, this.w, this.y)
val Vector4d.raab get() = Vector4d(this.x, this.w, this.w, this.z)
val Vector4d.raaa get() = Vector4d(this.x, this.w, this.w, this.w)
val Vector4d.grrr get() = Vector4d(this.y, this.x, this.x, this.x)
val Vector4d.grrg get() = Vector4d(this.y, this.x, this.x, this.y)
val Vector4d.grrb get() = Vector4d(this.y, this.x, this.x, this.z)
val Vector4d.grra get() = Vector4d(this.y, this.x, this.x, this.w)
val Vector4d.grgr get() = Vector4d(this.y, this.x, this.y, this.x)
val Vector4d.grgg get() = Vector4d(this.y, this.x, this.y, this.y)
val Vector4d.grgb get() = Vector4d(this.y, this.x, this.y, this.z)
val Vector4d.grga get() = Vector4d(this.y, this.x, this.y, this.w)
val Vector4d.grbr get() = Vector4d(this.y, this.x, this.z, this.x)
val Vector4d.grbg get() = Vector4d(this.y, this.x, this.z, this.y)
val Vector4d.grbb get() = Vector4d(this.y, this.x, this.z, this.z)
var Vector4d.grba get() = Vector4d(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4d.grar get() = Vector4d(this.y, this.x, this.w, this.x)
val Vector4d.grag get() = Vector4d(this.y, this.x, this.w, this.y)
var Vector4d.grab get() = Vector4d(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4d.graa get() = Vector4d(this.y, this.x, this.w, this.w)
val Vector4d.ggrr get() = Vector4d(this.y, this.y, this.x, this.x)
val Vector4d.ggrg get() = Vector4d(this.y, this.y, this.x, this.y)
val Vector4d.ggrb get() = Vector4d(this.y, this.y, this.x, this.z)
val Vector4d.ggra get() = Vector4d(this.y, this.y, this.x, this.w)
val Vector4d.gggr get() = Vector4d(this.y, this.y, this.y, this.x)
val Vector4d.gggg get() = Vector4d(this.y, this.y, this.y, this.y)
val Vector4d.gggb get() = Vector4d(this.y, this.y, this.y, this.z)
val Vector4d.ggga get() = Vector4d(this.y, this.y, this.y, this.w)
val Vector4d.ggbr get() = Vector4d(this.y, this.y, this.z, this.x)
val Vector4d.ggbg get() = Vector4d(this.y, this.y, this.z, this.y)
val Vector4d.ggbb get() = Vector4d(this.y, this.y, this.z, this.z)
val Vector4d.ggba get() = Vector4d(this.y, this.y, this.z, this.w)
val Vector4d.ggar get() = Vector4d(this.y, this.y, this.w, this.x)
val Vector4d.ggag get() = Vector4d(this.y, this.y, this.w, this.y)
val Vector4d.ggab get() = Vector4d(this.y, this.y, this.w, this.z)
val Vector4d.ggaa get() = Vector4d(this.y, this.y, this.w, this.w)
val Vector4d.gbrr get() = Vector4d(this.y, this.z, this.x, this.x)
val Vector4d.gbrg get() = Vector4d(this.y, this.z, this.x, this.y)
val Vector4d.gbrb get() = Vector4d(this.y, this.z, this.x, this.z)
var Vector4d.gbra get() = Vector4d(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4d.gbgr get() = Vector4d(this.y, this.z, this.y, this.x)
val Vector4d.gbgg get() = Vector4d(this.y, this.z, this.y, this.y)
val Vector4d.gbgb get() = Vector4d(this.y, this.z, this.y, this.z)
val Vector4d.gbga get() = Vector4d(this.y, this.z, this.y, this.w)
val Vector4d.gbbr get() = Vector4d(this.y, this.z, this.z, this.x)
val Vector4d.gbbg get() = Vector4d(this.y, this.z, this.z, this.y)
val Vector4d.gbbb get() = Vector4d(this.y, this.z, this.z, this.z)
val Vector4d.gbba get() = Vector4d(this.y, this.z, this.z, this.w)
var Vector4d.gbar get() = Vector4d(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4d.gbag get() = Vector4d(this.y, this.z, this.w, this.y)
val Vector4d.gbab get() = Vector4d(this.y, this.z, this.w, this.z)
val Vector4d.gbaa get() = Vector4d(this.y, this.z, this.w, this.w)
val Vector4d.garr get() = Vector4d(this.y, this.w, this.x, this.x)
val Vector4d.garg get() = Vector4d(this.y, this.w, this.x, this.y)
var Vector4d.garb get() = Vector4d(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4d.gara get() = Vector4d(this.y, this.w, this.x, this.w)
val Vector4d.gagr get() = Vector4d(this.y, this.w, this.y, this.x)
val Vector4d.gagg get() = Vector4d(this.y, this.w, this.y, this.y)
val Vector4d.gagb get() = Vector4d(this.y, this.w, this.y, this.z)
val Vector4d.gaga get() = Vector4d(this.y, this.w, this.y, this.w)
var Vector4d.gabr get() = Vector4d(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4d.gabg get() = Vector4d(this.y, this.w, this.z, this.y)
val Vector4d.gabb get() = Vector4d(this.y, this.w, this.z, this.z)
val Vector4d.gaba get() = Vector4d(this.y, this.w, this.z, this.w)
val Vector4d.gaar get() = Vector4d(this.y, this.w, this.w, this.x)
val Vector4d.gaag get() = Vector4d(this.y, this.w, this.w, this.y)
val Vector4d.gaab get() = Vector4d(this.y, this.w, this.w, this.z)
val Vector4d.gaaa get() = Vector4d(this.y, this.w, this.w, this.w)
val Vector4d.brrr get() = Vector4d(this.z, this.x, this.x, this.x)
val Vector4d.brrg get() = Vector4d(this.z, this.x, this.x, this.y)
val Vector4d.brrb get() = Vector4d(this.z, this.x, this.x, this.z)
val Vector4d.brra get() = Vector4d(this.z, this.x, this.x, this.w)
val Vector4d.brgr get() = Vector4d(this.z, this.x, this.y, this.x)
val Vector4d.brgg get() = Vector4d(this.z, this.x, this.y, this.y)
val Vector4d.brgb get() = Vector4d(this.z, this.x, this.y, this.z)
var Vector4d.brga get() = Vector4d(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4d.brbr get() = Vector4d(this.z, this.x, this.z, this.x)
val Vector4d.brbg get() = Vector4d(this.z, this.x, this.z, this.y)
val Vector4d.brbb get() = Vector4d(this.z, this.x, this.z, this.z)
val Vector4d.brba get() = Vector4d(this.z, this.x, this.z, this.w)
val Vector4d.brar get() = Vector4d(this.z, this.x, this.w, this.x)
var Vector4d.brag get() = Vector4d(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4d.brab get() = Vector4d(this.z, this.x, this.w, this.z)
val Vector4d.braa get() = Vector4d(this.z, this.x, this.w, this.w)
val Vector4d.bgrr get() = Vector4d(this.z, this.y, this.x, this.x)
val Vector4d.bgrg get() = Vector4d(this.z, this.y, this.x, this.y)
val Vector4d.bgrb get() = Vector4d(this.z, this.y, this.x, this.z)
var Vector4d.bgra get() = Vector4d(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4d.bggr get() = Vector4d(this.z, this.y, this.y, this.x)
val Vector4d.bggg get() = Vector4d(this.z, this.y, this.y, this.y)
val Vector4d.bggb get() = Vector4d(this.z, this.y, this.y, this.z)
val Vector4d.bgga get() = Vector4d(this.z, this.y, this.y, this.w)
val Vector4d.bgbr get() = Vector4d(this.z, this.y, this.z, this.x)
val Vector4d.bgbg get() = Vector4d(this.z, this.y, this.z, this.y)
val Vector4d.bgbb get() = Vector4d(this.z, this.y, this.z, this.z)
val Vector4d.bgba get() = Vector4d(this.z, this.y, this.z, this.w)
var Vector4d.bgar get() = Vector4d(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4d.bgag get() = Vector4d(this.z, this.y, this.w, this.y)
val Vector4d.bgab get() = Vector4d(this.z, this.y, this.w, this.z)
val Vector4d.bgaa get() = Vector4d(this.z, this.y, this.w, this.w)
val Vector4d.bbrr get() = Vector4d(this.z, this.z, this.x, this.x)
val Vector4d.bbrg get() = Vector4d(this.z, this.z, this.x, this.y)
val Vector4d.bbrb get() = Vector4d(this.z, this.z, this.x, this.z)
val Vector4d.bbra get() = Vector4d(this.z, this.z, this.x, this.w)
val Vector4d.bbgr get() = Vector4d(this.z, this.z, this.y, this.x)
val Vector4d.bbgg get() = Vector4d(this.z, this.z, this.y, this.y)
val Vector4d.bbgb get() = Vector4d(this.z, this.z, this.y, this.z)
val Vector4d.bbga get() = Vector4d(this.z, this.z, this.y, this.w)
val Vector4d.bbbr get() = Vector4d(this.z, this.z, this.z, this.x)
val Vector4d.bbbg get() = Vector4d(this.z, this.z, this.z, this.y)
val Vector4d.bbbb get() = Vector4d(this.z, this.z, this.z, this.z)
val Vector4d.bbba get() = Vector4d(this.z, this.z, this.z, this.w)
val Vector4d.bbar get() = Vector4d(this.z, this.z, this.w, this.x)
val Vector4d.bbag get() = Vector4d(this.z, this.z, this.w, this.y)
val Vector4d.bbab get() = Vector4d(this.z, this.z, this.w, this.z)
val Vector4d.bbaa get() = Vector4d(this.z, this.z, this.w, this.w)
val Vector4d.barr get() = Vector4d(this.z, this.w, this.x, this.x)
var Vector4d.barg get() = Vector4d(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4d.barb get() = Vector4d(this.z, this.w, this.x, this.z)
val Vector4d.bara get() = Vector4d(this.z, this.w, this.x, this.w)
var Vector4d.bagr get() = Vector4d(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4d.bagg get() = Vector4d(this.z, this.w, this.y, this.y)
val Vector4d.bagb get() = Vector4d(this.z, this.w, this.y, this.z)
val Vector4d.baga get() = Vector4d(this.z, this.w, this.y, this.w)
val Vector4d.babr get() = Vector4d(this.z, this.w, this.z, this.x)
val Vector4d.babg get() = Vector4d(this.z, this.w, this.z, this.y)
val Vector4d.babb get() = Vector4d(this.z, this.w, this.z, this.z)
val Vector4d.baba get() = Vector4d(this.z, this.w, this.z, this.w)
val Vector4d.baar get() = Vector4d(this.z, this.w, this.w, this.x)
val Vector4d.baag get() = Vector4d(this.z, this.w, this.w, this.y)
val Vector4d.baab get() = Vector4d(this.z, this.w, this.w, this.z)
val Vector4d.baaa get() = Vector4d(this.z, this.w, this.w, this.w)
val Vector4d.arrr get() = Vector4d(this.w, this.x, this.x, this.x)
val Vector4d.arrg get() = Vector4d(this.w, this.x, this.x, this.y)
val Vector4d.arrb get() = Vector4d(this.w, this.x, this.x, this.z)
val Vector4d.arra get() = Vector4d(this.w, this.x, this.x, this.w)
val Vector4d.argr get() = Vector4d(this.w, this.x, this.y, this.x)
val Vector4d.argg get() = Vector4d(this.w, this.x, this.y, this.y)
var Vector4d.argb get() = Vector4d(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4d.arga get() = Vector4d(this.w, this.x, this.y, this.w)
val Vector4d.arbr get() = Vector4d(this.w, this.x, this.z, this.x)
var Vector4d.arbg get() = Vector4d(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4d.arbb get() = Vector4d(this.w, this.x, this.z, this.z)
val Vector4d.arba get() = Vector4d(this.w, this.x, this.z, this.w)
val Vector4d.arar get() = Vector4d(this.w, this.x, this.w, this.x)
val Vector4d.arag get() = Vector4d(this.w, this.x, this.w, this.y)
val Vector4d.arab get() = Vector4d(this.w, this.x, this.w, this.z)
val Vector4d.araa get() = Vector4d(this.w, this.x, this.w, this.w)
val Vector4d.agrr get() = Vector4d(this.w, this.y, this.x, this.x)
val Vector4d.agrg get() = Vector4d(this.w, this.y, this.x, this.y)
var Vector4d.agrb get() = Vector4d(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4d.agra get() = Vector4d(this.w, this.y, this.x, this.w)
val Vector4d.aggr get() = Vector4d(this.w, this.y, this.y, this.x)
val Vector4d.aggg get() = Vector4d(this.w, this.y, this.y, this.y)
val Vector4d.aggb get() = Vector4d(this.w, this.y, this.y, this.z)
val Vector4d.agga get() = Vector4d(this.w, this.y, this.y, this.w)
var Vector4d.agbr get() = Vector4d(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4d.agbg get() = Vector4d(this.w, this.y, this.z, this.y)
val Vector4d.agbb get() = Vector4d(this.w, this.y, this.z, this.z)
val Vector4d.agba get() = Vector4d(this.w, this.y, this.z, this.w)
val Vector4d.agar get() = Vector4d(this.w, this.y, this.w, this.x)
val Vector4d.agag get() = Vector4d(this.w, this.y, this.w, this.y)
val Vector4d.agab get() = Vector4d(this.w, this.y, this.w, this.z)
val Vector4d.agaa get() = Vector4d(this.w, this.y, this.w, this.w)
val Vector4d.abrr get() = Vector4d(this.w, this.z, this.x, this.x)
var Vector4d.abrg get() = Vector4d(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4d.abrb get() = Vector4d(this.w, this.z, this.x, this.z)
val Vector4d.abra get() = Vector4d(this.w, this.z, this.x, this.w)
var Vector4d.abgr get() = Vector4d(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4d.abgg get() = Vector4d(this.w, this.z, this.y, this.y)
val Vector4d.abgb get() = Vector4d(this.w, this.z, this.y, this.z)
val Vector4d.abga get() = Vector4d(this.w, this.z, this.y, this.w)
val Vector4d.abbr get() = Vector4d(this.w, this.z, this.z, this.x)
val Vector4d.abbg get() = Vector4d(this.w, this.z, this.z, this.y)
val Vector4d.abbb get() = Vector4d(this.w, this.z, this.z, this.z)
val Vector4d.abba get() = Vector4d(this.w, this.z, this.z, this.w)
val Vector4d.abar get() = Vector4d(this.w, this.z, this.w, this.x)
val Vector4d.abag get() = Vector4d(this.w, this.z, this.w, this.y)
val Vector4d.abab get() = Vector4d(this.w, this.z, this.w, this.z)
val Vector4d.abaa get() = Vector4d(this.w, this.z, this.w, this.w)
val Vector4d.aarr get() = Vector4d(this.w, this.w, this.x, this.x)
val Vector4d.aarg get() = Vector4d(this.w, this.w, this.x, this.y)
val Vector4d.aarb get() = Vector4d(this.w, this.w, this.x, this.z)
val Vector4d.aara get() = Vector4d(this.w, this.w, this.x, this.w)
val Vector4d.aagr get() = Vector4d(this.w, this.w, this.y, this.x)
val Vector4d.aagg get() = Vector4d(this.w, this.w, this.y, this.y)
val Vector4d.aagb get() = Vector4d(this.w, this.w, this.y, this.z)
val Vector4d.aaga get() = Vector4d(this.w, this.w, this.y, this.w)
val Vector4d.aabr get() = Vector4d(this.w, this.w, this.z, this.x)
val Vector4d.aabg get() = Vector4d(this.w, this.w, this.z, this.y)
val Vector4d.aabb get() = Vector4d(this.w, this.w, this.z, this.z)
val Vector4d.aaba get() = Vector4d(this.w, this.w, this.z, this.w)
val Vector4d.aaar get() = Vector4d(this.w, this.w, this.w, this.x)
val Vector4d.aaag get() = Vector4d(this.w, this.w, this.w, this.y)
val Vector4d.aaab get() = Vector4d(this.w, this.w, this.w, this.z)
val Vector4d.aaaa get() = Vector4d(this.w, this.w, this.w, this.w)