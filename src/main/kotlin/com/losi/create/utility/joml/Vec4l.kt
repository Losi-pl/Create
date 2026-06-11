@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.utility.Quad
import org.joml.*

fun Vector4l.toQuad() = Quad(this.x, this.y, this.z, this.w)
fun Quad<Long, Long, Long, Long>.toVector() = Vector4l(this.first, this.second, this.third, this.fourth)

// ===================================== XYZW =====================================
val Vector4l.xx get() = Vector2l(this.x, this.x)
var Vector4l.xy get() = Vector2l(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4l.xz get() = Vector2l(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4l.xw get() = Vector2l(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4l.yx get() = Vector2l(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4l.yy get() = Vector2l(this.y, this.y)
var Vector4l.yz get() = Vector2l(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4l.yw get() = Vector2l(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4l.zx get() = Vector2l(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4l.zy get() = Vector2l(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4l.zz get() = Vector2l(this.z, this.z)
var Vector4l.zw get() = Vector2l(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4l.wx get() = Vector2l(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4l.wy get() = Vector2l(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4l.wz get() = Vector2l(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4l.ww get() = Vector2l(this.w, this.w)

val Vector4l.xxx get() = Vector3l(this.x, this.x, this.x)
val Vector4l.xxy get() = Vector3l(this.x, this.x, this.y)
val Vector4l.xxz get() = Vector3l(this.x, this.x, this.z)
val Vector4l.xxw get() = Vector3l(this.x, this.x, this.w)
val Vector4l.xyx get() = Vector3l(this.x, this.y, this.x)
val Vector4l.xyy get() = Vector3l(this.x, this.y, this.y)
var Vector4l.xyz get() = Vector3l(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4l.xyw get() = Vector3l(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4l.xzx get() = Vector3l(this.x, this.z, this.x)
var Vector4l.xzy get() = Vector3l(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4l.xzz get() = Vector3l(this.x, this.z, this.z)
var Vector4l.xzw get() = Vector3l(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4l.xwx get() = Vector3l(this.x, this.w, this.x)
var Vector4l.xwy get() = Vector3l(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4l.xwz get() = Vector3l(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4l.xww get() = Vector3l(this.x, this.w, this.w)
val Vector4l.yxx get() = Vector3l(this.y, this.x, this.x)
val Vector4l.yxy get() = Vector3l(this.y, this.x, this.y)
var Vector4l.yxz get() = Vector3l(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4l.yxw get() = Vector3l(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4l.yyx get() = Vector3l(this.y, this.y, this.x)
val Vector4l.yyy get() = Vector3l(this.y, this.y, this.y)
val Vector4l.yyz get() = Vector3l(this.y, this.y, this.z)
val Vector4l.yyw get() = Vector3l(this.y, this.y, this.w)
var Vector4l.yzx get() = Vector3l(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4l.yzy get() = Vector3l(this.y, this.z, this.y)
val Vector4l.yzz get() = Vector3l(this.y, this.z, this.z)
var Vector4l.yzw get() = Vector3l(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4l.ywx get() = Vector3l(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4l.ywy get() = Vector3l(this.y, this.w, this.y)
var Vector4l.ywz get() = Vector3l(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4l.yww get() = Vector3l(this.y, this.w, this.w)
val Vector4l.zxx get() = Vector3l(this.z, this.x, this.x)
var Vector4l.zxy get() = Vector3l(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4l.zxz get() = Vector3l(this.z, this.x, this.z)
var Vector4l.zxw get() = Vector3l(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4l.zyx get() = Vector3l(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4l.zyy get() = Vector3l(this.z, this.y, this.y)
val Vector4l.zyz get() = Vector3l(this.z, this.y, this.z)
var Vector4l.zyw get() = Vector3l(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4l.zzx get() = Vector3l(this.z, this.z, this.x)
val Vector4l.zzy get() = Vector3l(this.z, this.z, this.y)
val Vector4l.zzz get() = Vector3l(this.z, this.z, this.z)
val Vector4l.zzw get() = Vector3l(this.z, this.z, this.w)
var Vector4l.zwx get() = Vector3l(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4l.zwy get() = Vector3l(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4l.zwz get() = Vector3l(this.z, this.w, this.z)
val Vector4l.zww get() = Vector3l(this.z, this.w, this.w)
val Vector4l.wxx get() = Vector3l(this.w, this.x, this.x)
var Vector4l.wxy get() = Vector3l(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4l.wxz get() = Vector3l(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4l.wxw get() = Vector3l(this.w, this.x, this.w)
var Vector4l.wyx get() = Vector3l(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4l.wyy get() = Vector3l(this.w, this.y, this.y)
var Vector4l.wyz get() = Vector3l(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4l.wyw get() = Vector3l(this.w, this.y, this.w)
var Vector4l.wzx get() = Vector3l(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4l.wzy get() = Vector3l(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4l.wzz get() = Vector3l(this.w, this.z, this.z)
val Vector4l.wzw get() = Vector3l(this.w, this.z, this.w)
val Vector4l.wwx get() = Vector3l(this.w, this.w, this.x)
val Vector4l.wwy get() = Vector3l(this.w, this.w, this.y)
val Vector4l.wwz get() = Vector3l(this.w, this.w, this.z)
val Vector4l.www get() = Vector3l(this.w, this.w, this.w)

val Vector4l.xxxx get() = Vector4l(this.x, this.x, this.x, this.x)
val Vector4l.xxxy get() = Vector4l(this.x, this.x, this.x, this.y)
val Vector4l.xxxz get() = Vector4l(this.x, this.x, this.x, this.z)
val Vector4l.xxxw get() = Vector4l(this.x, this.x, this.x, this.w)
val Vector4l.xxyx get() = Vector4l(this.x, this.x, this.y, this.x)
val Vector4l.xxyy get() = Vector4l(this.x, this.x, this.y, this.y)
val Vector4l.xxyz get() = Vector4l(this.x, this.x, this.y, this.z)
val Vector4l.xxyw get() = Vector4l(this.x, this.x, this.y, this.w)
val Vector4l.xxzx get() = Vector4l(this.x, this.x, this.z, this.x)
val Vector4l.xxzy get() = Vector4l(this.x, this.x, this.z, this.y)
val Vector4l.xxzz get() = Vector4l(this.x, this.x, this.z, this.z)
val Vector4l.xxzw get() = Vector4l(this.x, this.x, this.z, this.w)
val Vector4l.xxwx get() = Vector4l(this.x, this.x, this.w, this.x)
val Vector4l.xxwy get() = Vector4l(this.x, this.x, this.w, this.y)
val Vector4l.xxwz get() = Vector4l(this.x, this.x, this.w, this.z)
val Vector4l.xxww get() = Vector4l(this.x, this.x, this.w, this.w)
val Vector4l.xyxx get() = Vector4l(this.x, this.y, this.x, this.x)
val Vector4l.xyxy get() = Vector4l(this.x, this.y, this.x, this.y)
val Vector4l.xyxz get() = Vector4l(this.x, this.y, this.x, this.z)
val Vector4l.xyxw get() = Vector4l(this.x, this.y, this.x, this.w)
val Vector4l.xyyx get() = Vector4l(this.x, this.y, this.y, this.x)
val Vector4l.xyyy get() = Vector4l(this.x, this.y, this.y, this.y)
val Vector4l.xyyz get() = Vector4l(this.x, this.y, this.y, this.z)
val Vector4l.xyyw get() = Vector4l(this.x, this.y, this.y, this.w)
val Vector4l.xyzx get() = Vector4l(this.x, this.y, this.z, this.x)
val Vector4l.xyzy get() = Vector4l(this.x, this.y, this.z, this.y)
val Vector4l.xyzz get() = Vector4l(this.x, this.y, this.z, this.z)
var Vector4l.xyzw get() = Vector4l(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4l.xywx get() = Vector4l(this.x, this.y, this.w, this.x)
val Vector4l.xywy get() = Vector4l(this.x, this.y, this.w, this.y)
var Vector4l.xywz get() = Vector4l(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4l.xyww get() = Vector4l(this.x, this.y, this.w, this.w)
val Vector4l.xzxx get() = Vector4l(this.x, this.z, this.x, this.x)
val Vector4l.xzxy get() = Vector4l(this.x, this.z, this.x, this.y)
val Vector4l.xzxz get() = Vector4l(this.x, this.z, this.x, this.z)
val Vector4l.xzxw get() = Vector4l(this.x, this.z, this.x, this.w)
val Vector4l.xzyx get() = Vector4l(this.x, this.z, this.y, this.x)
val Vector4l.xzyy get() = Vector4l(this.x, this.z, this.y, this.y)
val Vector4l.xzyz get() = Vector4l(this.x, this.z, this.y, this.z)
var Vector4l.xzyw get() = Vector4l(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4l.xzzx get() = Vector4l(this.x, this.z, this.z, this.x)
val Vector4l.xzzy get() = Vector4l(this.x, this.z, this.z, this.y)
val Vector4l.xzzz get() = Vector4l(this.x, this.z, this.z, this.z)
val Vector4l.xzzw get() = Vector4l(this.x, this.z, this.z, this.w)
val Vector4l.xzwx get() = Vector4l(this.x, this.z, this.w, this.x)
var Vector4l.xzwy get() = Vector4l(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4l.xzwz get() = Vector4l(this.x, this.z, this.w, this.z)
val Vector4l.xzww get() = Vector4l(this.x, this.z, this.w, this.w)
val Vector4l.xwxx get() = Vector4l(this.x, this.w, this.x, this.x)
val Vector4l.xwxy get() = Vector4l(this.x, this.w, this.x, this.y)
val Vector4l.xwxz get() = Vector4l(this.x, this.w, this.x, this.z)
val Vector4l.xwxw get() = Vector4l(this.x, this.w, this.x, this.w)
val Vector4l.xwyx get() = Vector4l(this.x, this.w, this.y, this.x)
val Vector4l.xwyy get() = Vector4l(this.x, this.w, this.y, this.y)
var Vector4l.xwyz get() = Vector4l(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4l.xwyw get() = Vector4l(this.x, this.w, this.y, this.w)
val Vector4l.xwzx get() = Vector4l(this.x, this.w, this.z, this.x)
var Vector4l.xwzy get() = Vector4l(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4l.xwzz get() = Vector4l(this.x, this.w, this.z, this.z)
val Vector4l.xwzw get() = Vector4l(this.x, this.w, this.z, this.w)
val Vector4l.xwwx get() = Vector4l(this.x, this.w, this.w, this.x)
val Vector4l.xwwy get() = Vector4l(this.x, this.w, this.w, this.y)
val Vector4l.xwwz get() = Vector4l(this.x, this.w, this.w, this.z)
val Vector4l.xwww get() = Vector4l(this.x, this.w, this.w, this.w)
val Vector4l.yxxx get() = Vector4l(this.y, this.x, this.x, this.x)
val Vector4l.yxxy get() = Vector4l(this.y, this.x, this.x, this.y)
val Vector4l.yxxz get() = Vector4l(this.y, this.x, this.x, this.z)
val Vector4l.yxxw get() = Vector4l(this.y, this.x, this.x, this.w)
val Vector4l.yxyx get() = Vector4l(this.y, this.x, this.y, this.x)
val Vector4l.yxyy get() = Vector4l(this.y, this.x, this.y, this.y)
val Vector4l.yxyz get() = Vector4l(this.y, this.x, this.y, this.z)
val Vector4l.yxyw get() = Vector4l(this.y, this.x, this.y, this.w)
val Vector4l.yxzx get() = Vector4l(this.y, this.x, this.z, this.x)
val Vector4l.yxzy get() = Vector4l(this.y, this.x, this.z, this.y)
val Vector4l.yxzz get() = Vector4l(this.y, this.x, this.z, this.z)
var Vector4l.yxzw get() = Vector4l(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4l.yxwx get() = Vector4l(this.y, this.x, this.w, this.x)
val Vector4l.yxwy get() = Vector4l(this.y, this.x, this.w, this.y)
var Vector4l.yxwz get() = Vector4l(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4l.yxww get() = Vector4l(this.y, this.x, this.w, this.w)
val Vector4l.yyxx get() = Vector4l(this.y, this.y, this.x, this.x)
val Vector4l.yyxy get() = Vector4l(this.y, this.y, this.x, this.y)
val Vector4l.yyxz get() = Vector4l(this.y, this.y, this.x, this.z)
val Vector4l.yyxw get() = Vector4l(this.y, this.y, this.x, this.w)
val Vector4l.yyyx get() = Vector4l(this.y, this.y, this.y, this.x)
val Vector4l.yyyy get() = Vector4l(this.y, this.y, this.y, this.y)
val Vector4l.yyyz get() = Vector4l(this.y, this.y, this.y, this.z)
val Vector4l.yyyw get() = Vector4l(this.y, this.y, this.y, this.w)
val Vector4l.yyzx get() = Vector4l(this.y, this.y, this.z, this.x)
val Vector4l.yyzy get() = Vector4l(this.y, this.y, this.z, this.y)
val Vector4l.yyzz get() = Vector4l(this.y, this.y, this.z, this.z)
val Vector4l.yyzw get() = Vector4l(this.y, this.y, this.z, this.w)
val Vector4l.yywx get() = Vector4l(this.y, this.y, this.w, this.x)
val Vector4l.yywy get() = Vector4l(this.y, this.y, this.w, this.y)
val Vector4l.yywz get() = Vector4l(this.y, this.y, this.w, this.z)
val Vector4l.yyww get() = Vector4l(this.y, this.y, this.w, this.w)
val Vector4l.yzxx get() = Vector4l(this.y, this.z, this.x, this.x)
val Vector4l.yzxy get() = Vector4l(this.y, this.z, this.x, this.y)
val Vector4l.yzxz get() = Vector4l(this.y, this.z, this.x, this.z)
var Vector4l.yzxw get() = Vector4l(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4l.yzyx get() = Vector4l(this.y, this.z, this.y, this.x)
val Vector4l.yzyy get() = Vector4l(this.y, this.z, this.y, this.y)
val Vector4l.yzyz get() = Vector4l(this.y, this.z, this.y, this.z)
val Vector4l.yzyw get() = Vector4l(this.y, this.z, this.y, this.w)
val Vector4l.yzzx get() = Vector4l(this.y, this.z, this.z, this.x)
val Vector4l.yzzy get() = Vector4l(this.y, this.z, this.z, this.y)
val Vector4l.yzzz get() = Vector4l(this.y, this.z, this.z, this.z)
val Vector4l.yzzw get() = Vector4l(this.y, this.z, this.z, this.w)
var Vector4l.yzwx get() = Vector4l(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4l.yzwy get() = Vector4l(this.y, this.z, this.w, this.y)
val Vector4l.yzwz get() = Vector4l(this.y, this.z, this.w, this.z)
val Vector4l.yzww get() = Vector4l(this.y, this.z, this.w, this.w)
val Vector4l.ywxx get() = Vector4l(this.y, this.w, this.x, this.x)
val Vector4l.ywxy get() = Vector4l(this.y, this.w, this.x, this.y)
var Vector4l.ywxz get() = Vector4l(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4l.ywxw get() = Vector4l(this.y, this.w, this.x, this.w)
val Vector4l.ywyx get() = Vector4l(this.y, this.w, this.y, this.x)
val Vector4l.ywyy get() = Vector4l(this.y, this.w, this.y, this.y)
val Vector4l.ywyz get() = Vector4l(this.y, this.w, this.y, this.z)
val Vector4l.ywyw get() = Vector4l(this.y, this.w, this.y, this.w)
var Vector4l.ywzx get() = Vector4l(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4l.ywzy get() = Vector4l(this.y, this.w, this.z, this.y)
val Vector4l.ywzz get() = Vector4l(this.y, this.w, this.z, this.z)
val Vector4l.ywzw get() = Vector4l(this.y, this.w, this.z, this.w)
val Vector4l.ywwx get() = Vector4l(this.y, this.w, this.w, this.x)
val Vector4l.ywwy get() = Vector4l(this.y, this.w, this.w, this.y)
val Vector4l.ywwz get() = Vector4l(this.y, this.w, this.w, this.z)
val Vector4l.ywww get() = Vector4l(this.y, this.w, this.w, this.w)
val Vector4l.zxxx get() = Vector4l(this.z, this.x, this.x, this.x)
val Vector4l.zxxy get() = Vector4l(this.z, this.x, this.x, this.y)
val Vector4l.zxxz get() = Vector4l(this.z, this.x, this.x, this.z)
val Vector4l.zxxw get() = Vector4l(this.z, this.x, this.x, this.w)
val Vector4l.zxyx get() = Vector4l(this.z, this.x, this.y, this.x)
val Vector4l.zxyy get() = Vector4l(this.z, this.x, this.y, this.y)
val Vector4l.zxyz get() = Vector4l(this.z, this.x, this.y, this.z)
var Vector4l.zxyw get() = Vector4l(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4l.zxzx get() = Vector4l(this.z, this.x, this.z, this.x)
val Vector4l.zxzy get() = Vector4l(this.z, this.x, this.z, this.y)
val Vector4l.zxzz get() = Vector4l(this.z, this.x, this.z, this.z)
val Vector4l.zxzw get() = Vector4l(this.z, this.x, this.z, this.w)
val Vector4l.zxwx get() = Vector4l(this.z, this.x, this.w, this.x)
var Vector4l.zxwy get() = Vector4l(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4l.zxwz get() = Vector4l(this.z, this.x, this.w, this.z)
val Vector4l.zxww get() = Vector4l(this.z, this.x, this.w, this.w)
val Vector4l.zyxx get() = Vector4l(this.z, this.y, this.x, this.x)
val Vector4l.zyxy get() = Vector4l(this.z, this.y, this.x, this.y)
val Vector4l.zyxz get() = Vector4l(this.z, this.y, this.x, this.z)
var Vector4l.zyxw get() = Vector4l(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4l.zyyx get() = Vector4l(this.z, this.y, this.y, this.x)
val Vector4l.zyyy get() = Vector4l(this.z, this.y, this.y, this.y)
val Vector4l.zyyz get() = Vector4l(this.z, this.y, this.y, this.z)
val Vector4l.zyyw get() = Vector4l(this.z, this.y, this.y, this.w)
val Vector4l.zyzx get() = Vector4l(this.z, this.y, this.z, this.x)
val Vector4l.zyzy get() = Vector4l(this.z, this.y, this.z, this.y)
val Vector4l.zyzz get() = Vector4l(this.z, this.y, this.z, this.z)
val Vector4l.zyzw get() = Vector4l(this.z, this.y, this.z, this.w)
var Vector4l.zywx get() = Vector4l(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4l.zywy get() = Vector4l(this.z, this.y, this.w, this.y)
val Vector4l.zywz get() = Vector4l(this.z, this.y, this.w, this.z)
val Vector4l.zyww get() = Vector4l(this.z, this.y, this.w, this.w)
val Vector4l.zzxx get() = Vector4l(this.z, this.z, this.x, this.x)
val Vector4l.zzxy get() = Vector4l(this.z, this.z, this.x, this.y)
val Vector4l.zzxz get() = Vector4l(this.z, this.z, this.x, this.z)
val Vector4l.zzxw get() = Vector4l(this.z, this.z, this.x, this.w)
val Vector4l.zzyx get() = Vector4l(this.z, this.z, this.y, this.x)
val Vector4l.zzyy get() = Vector4l(this.z, this.z, this.y, this.y)
val Vector4l.zzyz get() = Vector4l(this.z, this.z, this.y, this.z)
val Vector4l.zzyw get() = Vector4l(this.z, this.z, this.y, this.w)
val Vector4l.zzzx get() = Vector4l(this.z, this.z, this.z, this.x)
val Vector4l.zzzy get() = Vector4l(this.z, this.z, this.z, this.y)
val Vector4l.zzzz get() = Vector4l(this.z, this.z, this.z, this.z)
val Vector4l.zzzw get() = Vector4l(this.z, this.z, this.z, this.w)
val Vector4l.zzwx get() = Vector4l(this.z, this.z, this.w, this.x)
val Vector4l.zzwy get() = Vector4l(this.z, this.z, this.w, this.y)
val Vector4l.zzwz get() = Vector4l(this.z, this.z, this.w, this.z)
val Vector4l.zzww get() = Vector4l(this.z, this.z, this.w, this.w)
val Vector4l.zwxx get() = Vector4l(this.z, this.w, this.x, this.x)
var Vector4l.zwxy get() = Vector4l(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4l.zwxz get() = Vector4l(this.z, this.w, this.x, this.z)
val Vector4l.zwxw get() = Vector4l(this.z, this.w, this.x, this.w)
var Vector4l.zwyx get() = Vector4l(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4l.zwyy get() = Vector4l(this.z, this.w, this.y, this.y)
val Vector4l.zwyz get() = Vector4l(this.z, this.w, this.y, this.z)
val Vector4l.zwyw get() = Vector4l(this.z, this.w, this.y, this.w)
val Vector4l.zwzx get() = Vector4l(this.z, this.w, this.z, this.x)
val Vector4l.zwzy get() = Vector4l(this.z, this.w, this.z, this.y)
val Vector4l.zwzz get() = Vector4l(this.z, this.w, this.z, this.z)
val Vector4l.zwzw get() = Vector4l(this.z, this.w, this.z, this.w)
val Vector4l.zwwx get() = Vector4l(this.z, this.w, this.w, this.x)
val Vector4l.zwwy get() = Vector4l(this.z, this.w, this.w, this.y)
val Vector4l.zwwz get() = Vector4l(this.z, this.w, this.w, this.z)
val Vector4l.zwww get() = Vector4l(this.z, this.w, this.w, this.w)
val Vector4l.wxxx get() = Vector4l(this.w, this.x, this.x, this.x)
val Vector4l.wxxy get() = Vector4l(this.w, this.x, this.x, this.y)
val Vector4l.wxxz get() = Vector4l(this.w, this.x, this.x, this.z)
val Vector4l.wxxw get() = Vector4l(this.w, this.x, this.x, this.w)
val Vector4l.wxyx get() = Vector4l(this.w, this.x, this.y, this.x)
val Vector4l.wxyy get() = Vector4l(this.w, this.x, this.y, this.y)
var Vector4l.wxyz get() = Vector4l(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4l.wxyw get() = Vector4l(this.w, this.x, this.y, this.w)
val Vector4l.wxzx get() = Vector4l(this.w, this.x, this.z, this.x)
var Vector4l.wxzy get() = Vector4l(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4l.wxzz get() = Vector4l(this.w, this.x, this.z, this.z)
val Vector4l.wxzw get() = Vector4l(this.w, this.x, this.z, this.w)
val Vector4l.wxwx get() = Vector4l(this.w, this.x, this.w, this.x)
val Vector4l.wxwy get() = Vector4l(this.w, this.x, this.w, this.y)
val Vector4l.wxwz get() = Vector4l(this.w, this.x, this.w, this.z)
val Vector4l.wxww get() = Vector4l(this.w, this.x, this.w, this.w)
val Vector4l.wyxx get() = Vector4l(this.w, this.y, this.x, this.x)
val Vector4l.wyxy get() = Vector4l(this.w, this.y, this.x, this.y)
var Vector4l.wyxz get() = Vector4l(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4l.wyxw get() = Vector4l(this.w, this.y, this.x, this.w)
val Vector4l.wyyx get() = Vector4l(this.w, this.y, this.y, this.x)
val Vector4l.wyyy get() = Vector4l(this.w, this.y, this.y, this.y)
val Vector4l.wyyz get() = Vector4l(this.w, this.y, this.y, this.z)
val Vector4l.wyyw get() = Vector4l(this.w, this.y, this.y, this.w)
var Vector4l.wyzx get() = Vector4l(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4l.wyzy get() = Vector4l(this.w, this.y, this.z, this.y)
val Vector4l.wyzz get() = Vector4l(this.w, this.y, this.z, this.z)
val Vector4l.wyzw get() = Vector4l(this.w, this.y, this.z, this.w)
val Vector4l.wywx get() = Vector4l(this.w, this.y, this.w, this.x)
val Vector4l.wywy get() = Vector4l(this.w, this.y, this.w, this.y)
val Vector4l.wywz get() = Vector4l(this.w, this.y, this.w, this.z)
val Vector4l.wyww get() = Vector4l(this.w, this.y, this.w, this.w)
val Vector4l.wzxx get() = Vector4l(this.w, this.z, this.x, this.x)
var Vector4l.wzxy get() = Vector4l(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4l.wzxz get() = Vector4l(this.w, this.z, this.x, this.z)
val Vector4l.wzxw get() = Vector4l(this.w, this.z, this.x, this.w)
var Vector4l.wzyx get() = Vector4l(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4l.wzyy get() = Vector4l(this.w, this.z, this.y, this.y)
val Vector4l.wzyz get() = Vector4l(this.w, this.z, this.y, this.z)
val Vector4l.wzyw get() = Vector4l(this.w, this.z, this.y, this.w)
val Vector4l.wzzx get() = Vector4l(this.w, this.z, this.z, this.x)
val Vector4l.wzzy get() = Vector4l(this.w, this.z, this.z, this.y)
val Vector4l.wzzz get() = Vector4l(this.w, this.z, this.z, this.z)
val Vector4l.wzzw get() = Vector4l(this.w, this.z, this.z, this.w)
val Vector4l.wzwx get() = Vector4l(this.w, this.z, this.w, this.x)
val Vector4l.wzwy get() = Vector4l(this.w, this.z, this.w, this.y)
val Vector4l.wzwz get() = Vector4l(this.w, this.z, this.w, this.z)
val Vector4l.wzww get() = Vector4l(this.w, this.z, this.w, this.w)
val Vector4l.wwxx get() = Vector4l(this.w, this.w, this.x, this.x)
val Vector4l.wwxy get() = Vector4l(this.w, this.w, this.x, this.y)
val Vector4l.wwxz get() = Vector4l(this.w, this.w, this.x, this.z)
val Vector4l.wwxw get() = Vector4l(this.w, this.w, this.x, this.w)
val Vector4l.wwyx get() = Vector4l(this.w, this.w, this.y, this.x)
val Vector4l.wwyy get() = Vector4l(this.w, this.w, this.y, this.y)
val Vector4l.wwyz get() = Vector4l(this.w, this.w, this.y, this.z)
val Vector4l.wwyw get() = Vector4l(this.w, this.w, this.y, this.w)
val Vector4l.wwzx get() = Vector4l(this.w, this.w, this.z, this.x)
val Vector4l.wwzy get() = Vector4l(this.w, this.w, this.z, this.y)
val Vector4l.wwzz get() = Vector4l(this.w, this.w, this.z, this.z)
val Vector4l.wwzw get() = Vector4l(this.w, this.w, this.z, this.w)
val Vector4l.wwwx get() = Vector4l(this.w, this.w, this.w, this.x)
val Vector4l.wwwy get() = Vector4l(this.w, this.w, this.w, this.y)
val Vector4l.wwwz get() = Vector4l(this.w, this.w, this.w, this.z)
val Vector4l.wwww get() = Vector4l(this.w, this.w, this.w, this.w)

// ===================================== RGBA =====================================
var Vector4l.r: Long get() = this.x; set(it) { this.x = it }
var Vector4l.g: Long get() = this.y; set(it) { this.y = it }
var Vector4l.b: Long get() = this.z; set(it) { this.z = it }
var Vector4l.a: Long get() = this.w; set(it) { this.w = it }

val Vector4l.rr get() = Vector2l(this.x, this.x)
var Vector4l.rg get() = Vector2l(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4l.rb get() = Vector2l(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4l.ra get() = Vector2l(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4l.gr get() = Vector2l(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4l.gg get() = Vector2l(this.y, this.y)
var Vector4l.gb get() = Vector2l(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4l.ga get() = Vector2l(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4l.br get() = Vector2l(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4l.bg get() = Vector2l(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4l.bb get() = Vector2l(this.z, this.z)
var Vector4l.ba get() = Vector2l(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4l.ar get() = Vector2l(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4l.ag get() = Vector2l(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4l.ab get() = Vector2l(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4l.aa get() = Vector2l(this.w, this.w)

val Vector4l.rrr get() = Vector3l(this.x, this.x, this.x)
val Vector4l.rrg get() = Vector3l(this.x, this.x, this.y)
val Vector4l.rrb get() = Vector3l(this.x, this.x, this.z)
val Vector4l.rra get() = Vector3l(this.x, this.x, this.w)
val Vector4l.rgr get() = Vector3l(this.x, this.y, this.x)
val Vector4l.rgg get() = Vector3l(this.x, this.y, this.y)
var Vector4l.rgb get() = Vector3l(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4l.rga get() = Vector3l(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4l.rbr get() = Vector3l(this.x, this.z, this.x)
var Vector4l.rbg get() = Vector3l(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4l.rbb get() = Vector3l(this.x, this.z, this.z)
var Vector4l.rba get() = Vector3l(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4l.rar get() = Vector3l(this.x, this.w, this.x)
var Vector4l.rag get() = Vector3l(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4l.rab get() = Vector3l(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4l.raa get() = Vector3l(this.x, this.w, this.w)
val Vector4l.grr get() = Vector3l(this.y, this.x, this.x)
val Vector4l.grg get() = Vector3l(this.y, this.x, this.y)
var Vector4l.grb get() = Vector3l(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4l.gra get() = Vector3l(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4l.ggr get() = Vector3l(this.y, this.y, this.x)
val Vector4l.ggg get() = Vector3l(this.y, this.y, this.y)
val Vector4l.ggb get() = Vector3l(this.y, this.y, this.z)
val Vector4l.gga get() = Vector3l(this.y, this.y, this.w)
var Vector4l.gbr get() = Vector3l(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4l.gbg get() = Vector3l(this.y, this.z, this.y)
val Vector4l.gbb get() = Vector3l(this.y, this.z, this.z)
var Vector4l.gba get() = Vector3l(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4l.gar get() = Vector3l(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4l.gag get() = Vector3l(this.y, this.w, this.y)
var Vector4l.gab get() = Vector3l(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4l.gaa get() = Vector3l(this.y, this.w, this.w)
val Vector4l.brr get() = Vector3l(this.z, this.x, this.x)
var Vector4l.brg get() = Vector3l(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4l.brb get() = Vector3l(this.z, this.x, this.z)
var Vector4l.bra get() = Vector3l(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4l.bgr get() = Vector3l(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4l.bgg get() = Vector3l(this.z, this.y, this.y)
val Vector4l.bgb get() = Vector3l(this.z, this.y, this.z)
var Vector4l.bga get() = Vector3l(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4l.bbr get() = Vector3l(this.z, this.z, this.x)
val Vector4l.bbg get() = Vector3l(this.z, this.z, this.y)
val Vector4l.bbb get() = Vector3l(this.z, this.z, this.z)
val Vector4l.bba get() = Vector3l(this.z, this.z, this.w)
var Vector4l.bar get() = Vector3l(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4l.bag get() = Vector3l(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4l.bab get() = Vector3l(this.z, this.w, this.z)
val Vector4l.baa get() = Vector3l(this.z, this.w, this.w)
val Vector4l.arr get() = Vector3l(this.w, this.x, this.x)
var Vector4l.arg get() = Vector3l(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4l.arb get() = Vector3l(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4l.ara get() = Vector3l(this.w, this.x, this.w)
var Vector4l.agr get() = Vector3l(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4l.agg get() = Vector3l(this.w, this.y, this.y)
var Vector4l.agb get() = Vector3l(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4l.aga get() = Vector3l(this.w, this.y, this.w)
var Vector4l.abr get() = Vector3l(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4l.abg get() = Vector3l(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4l.abb get() = Vector3l(this.w, this.z, this.z)
val Vector4l.aba get() = Vector3l(this.w, this.z, this.w)
val Vector4l.aar get() = Vector3l(this.w, this.w, this.x)
val Vector4l.aag get() = Vector3l(this.w, this.w, this.y)
val Vector4l.aab get() = Vector3l(this.w, this.w, this.z)
val Vector4l.aaa get() = Vector3l(this.w, this.w, this.w)

val Vector4l.rrrr get() = Vector4l(this.x, this.x, this.x, this.x)
val Vector4l.rrrg get() = Vector4l(this.x, this.x, this.x, this.y)
val Vector4l.rrrb get() = Vector4l(this.x, this.x, this.x, this.z)
val Vector4l.rrra get() = Vector4l(this.x, this.x, this.x, this.w)
val Vector4l.rrgr get() = Vector4l(this.x, this.x, this.y, this.x)
val Vector4l.rrgg get() = Vector4l(this.x, this.x, this.y, this.y)
val Vector4l.rrgb get() = Vector4l(this.x, this.x, this.y, this.z)
val Vector4l.rrga get() = Vector4l(this.x, this.x, this.y, this.w)
val Vector4l.rrbr get() = Vector4l(this.x, this.x, this.z, this.x)
val Vector4l.rrbg get() = Vector4l(this.x, this.x, this.z, this.y)
val Vector4l.rrbb get() = Vector4l(this.x, this.x, this.z, this.z)
val Vector4l.rrba get() = Vector4l(this.x, this.x, this.z, this.w)
val Vector4l.rrar get() = Vector4l(this.x, this.x, this.w, this.x)
val Vector4l.rrag get() = Vector4l(this.x, this.x, this.w, this.y)
val Vector4l.rrab get() = Vector4l(this.x, this.x, this.w, this.z)
val Vector4l.rraa get() = Vector4l(this.x, this.x, this.w, this.w)
val Vector4l.rgrr get() = Vector4l(this.x, this.y, this.x, this.x)
val Vector4l.rgrg get() = Vector4l(this.x, this.y, this.x, this.y)
val Vector4l.rgrb get() = Vector4l(this.x, this.y, this.x, this.z)
val Vector4l.rgra get() = Vector4l(this.x, this.y, this.x, this.w)
val Vector4l.rggr get() = Vector4l(this.x, this.y, this.y, this.x)
val Vector4l.rggg get() = Vector4l(this.x, this.y, this.y, this.y)
val Vector4l.rggb get() = Vector4l(this.x, this.y, this.y, this.z)
val Vector4l.rgga get() = Vector4l(this.x, this.y, this.y, this.w)
val Vector4l.rgbr get() = Vector4l(this.x, this.y, this.z, this.x)
val Vector4l.rgbg get() = Vector4l(this.x, this.y, this.z, this.y)
val Vector4l.rgbb get() = Vector4l(this.x, this.y, this.z, this.z)
var Vector4l.rgba get() = Vector4l(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4l.rgar get() = Vector4l(this.x, this.y, this.w, this.x)
val Vector4l.rgag get() = Vector4l(this.x, this.y, this.w, this.y)
var Vector4l.rgab get() = Vector4l(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4l.rgaa get() = Vector4l(this.x, this.y, this.w, this.w)
val Vector4l.rbrr get() = Vector4l(this.x, this.z, this.x, this.x)
val Vector4l.rbrg get() = Vector4l(this.x, this.z, this.x, this.y)
val Vector4l.rbrb get() = Vector4l(this.x, this.z, this.x, this.z)
val Vector4l.rbra get() = Vector4l(this.x, this.z, this.x, this.w)
val Vector4l.rbgr get() = Vector4l(this.x, this.z, this.y, this.x)
val Vector4l.rbgg get() = Vector4l(this.x, this.z, this.y, this.y)
val Vector4l.rbgb get() = Vector4l(this.x, this.z, this.y, this.z)
var Vector4l.rbga get() = Vector4l(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4l.rbbr get() = Vector4l(this.x, this.z, this.z, this.x)
val Vector4l.rbbg get() = Vector4l(this.x, this.z, this.z, this.y)
val Vector4l.rbbb get() = Vector4l(this.x, this.z, this.z, this.z)
val Vector4l.rbba get() = Vector4l(this.x, this.z, this.z, this.w)
val Vector4l.rbar get() = Vector4l(this.x, this.z, this.w, this.x)
var Vector4l.rbag get() = Vector4l(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4l.rbab get() = Vector4l(this.x, this.z, this.w, this.z)
val Vector4l.rbaa get() = Vector4l(this.x, this.z, this.w, this.w)
val Vector4l.rarr get() = Vector4l(this.x, this.w, this.x, this.x)
val Vector4l.rarg get() = Vector4l(this.x, this.w, this.x, this.y)
val Vector4l.rarb get() = Vector4l(this.x, this.w, this.x, this.z)
val Vector4l.rara get() = Vector4l(this.x, this.w, this.x, this.w)
val Vector4l.ragr get() = Vector4l(this.x, this.w, this.y, this.x)
val Vector4l.ragg get() = Vector4l(this.x, this.w, this.y, this.y)
var Vector4l.ragb get() = Vector4l(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4l.raga get() = Vector4l(this.x, this.w, this.y, this.w)
val Vector4l.rabr get() = Vector4l(this.x, this.w, this.z, this.x)
var Vector4l.rabg get() = Vector4l(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4l.rabb get() = Vector4l(this.x, this.w, this.z, this.z)
val Vector4l.raba get() = Vector4l(this.x, this.w, this.z, this.w)
val Vector4l.raar get() = Vector4l(this.x, this.w, this.w, this.x)
val Vector4l.raag get() = Vector4l(this.x, this.w, this.w, this.y)
val Vector4l.raab get() = Vector4l(this.x, this.w, this.w, this.z)
val Vector4l.raaa get() = Vector4l(this.x, this.w, this.w, this.w)
val Vector4l.grrr get() = Vector4l(this.y, this.x, this.x, this.x)
val Vector4l.grrg get() = Vector4l(this.y, this.x, this.x, this.y)
val Vector4l.grrb get() = Vector4l(this.y, this.x, this.x, this.z)
val Vector4l.grra get() = Vector4l(this.y, this.x, this.x, this.w)
val Vector4l.grgr get() = Vector4l(this.y, this.x, this.y, this.x)
val Vector4l.grgg get() = Vector4l(this.y, this.x, this.y, this.y)
val Vector4l.grgb get() = Vector4l(this.y, this.x, this.y, this.z)
val Vector4l.grga get() = Vector4l(this.y, this.x, this.y, this.w)
val Vector4l.grbr get() = Vector4l(this.y, this.x, this.z, this.x)
val Vector4l.grbg get() = Vector4l(this.y, this.x, this.z, this.y)
val Vector4l.grbb get() = Vector4l(this.y, this.x, this.z, this.z)
var Vector4l.grba get() = Vector4l(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4l.grar get() = Vector4l(this.y, this.x, this.w, this.x)
val Vector4l.grag get() = Vector4l(this.y, this.x, this.w, this.y)
var Vector4l.grab get() = Vector4l(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4l.graa get() = Vector4l(this.y, this.x, this.w, this.w)
val Vector4l.ggrr get() = Vector4l(this.y, this.y, this.x, this.x)
val Vector4l.ggrg get() = Vector4l(this.y, this.y, this.x, this.y)
val Vector4l.ggrb get() = Vector4l(this.y, this.y, this.x, this.z)
val Vector4l.ggra get() = Vector4l(this.y, this.y, this.x, this.w)
val Vector4l.gggr get() = Vector4l(this.y, this.y, this.y, this.x)
val Vector4l.gggg get() = Vector4l(this.y, this.y, this.y, this.y)
val Vector4l.gggb get() = Vector4l(this.y, this.y, this.y, this.z)
val Vector4l.ggga get() = Vector4l(this.y, this.y, this.y, this.w)
val Vector4l.ggbr get() = Vector4l(this.y, this.y, this.z, this.x)
val Vector4l.ggbg get() = Vector4l(this.y, this.y, this.z, this.y)
val Vector4l.ggbb get() = Vector4l(this.y, this.y, this.z, this.z)
val Vector4l.ggba get() = Vector4l(this.y, this.y, this.z, this.w)
val Vector4l.ggar get() = Vector4l(this.y, this.y, this.w, this.x)
val Vector4l.ggag get() = Vector4l(this.y, this.y, this.w, this.y)
val Vector4l.ggab get() = Vector4l(this.y, this.y, this.w, this.z)
val Vector4l.ggaa get() = Vector4l(this.y, this.y, this.w, this.w)
val Vector4l.gbrr get() = Vector4l(this.y, this.z, this.x, this.x)
val Vector4l.gbrg get() = Vector4l(this.y, this.z, this.x, this.y)
val Vector4l.gbrb get() = Vector4l(this.y, this.z, this.x, this.z)
var Vector4l.gbra get() = Vector4l(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4l.gbgr get() = Vector4l(this.y, this.z, this.y, this.x)
val Vector4l.gbgg get() = Vector4l(this.y, this.z, this.y, this.y)
val Vector4l.gbgb get() = Vector4l(this.y, this.z, this.y, this.z)
val Vector4l.gbga get() = Vector4l(this.y, this.z, this.y, this.w)
val Vector4l.gbbr get() = Vector4l(this.y, this.z, this.z, this.x)
val Vector4l.gbbg get() = Vector4l(this.y, this.z, this.z, this.y)
val Vector4l.gbbb get() = Vector4l(this.y, this.z, this.z, this.z)
val Vector4l.gbba get() = Vector4l(this.y, this.z, this.z, this.w)
var Vector4l.gbar get() = Vector4l(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4l.gbag get() = Vector4l(this.y, this.z, this.w, this.y)
val Vector4l.gbab get() = Vector4l(this.y, this.z, this.w, this.z)
val Vector4l.gbaa get() = Vector4l(this.y, this.z, this.w, this.w)
val Vector4l.garr get() = Vector4l(this.y, this.w, this.x, this.x)
val Vector4l.garg get() = Vector4l(this.y, this.w, this.x, this.y)
var Vector4l.garb get() = Vector4l(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4l.gara get() = Vector4l(this.y, this.w, this.x, this.w)
val Vector4l.gagr get() = Vector4l(this.y, this.w, this.y, this.x)
val Vector4l.gagg get() = Vector4l(this.y, this.w, this.y, this.y)
val Vector4l.gagb get() = Vector4l(this.y, this.w, this.y, this.z)
val Vector4l.gaga get() = Vector4l(this.y, this.w, this.y, this.w)
var Vector4l.gabr get() = Vector4l(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4l.gabg get() = Vector4l(this.y, this.w, this.z, this.y)
val Vector4l.gabb get() = Vector4l(this.y, this.w, this.z, this.z)
val Vector4l.gaba get() = Vector4l(this.y, this.w, this.z, this.w)
val Vector4l.gaar get() = Vector4l(this.y, this.w, this.w, this.x)
val Vector4l.gaag get() = Vector4l(this.y, this.w, this.w, this.y)
val Vector4l.gaab get() = Vector4l(this.y, this.w, this.w, this.z)
val Vector4l.gaaa get() = Vector4l(this.y, this.w, this.w, this.w)
val Vector4l.brrr get() = Vector4l(this.z, this.x, this.x, this.x)
val Vector4l.brrg get() = Vector4l(this.z, this.x, this.x, this.y)
val Vector4l.brrb get() = Vector4l(this.z, this.x, this.x, this.z)
val Vector4l.brra get() = Vector4l(this.z, this.x, this.x, this.w)
val Vector4l.brgr get() = Vector4l(this.z, this.x, this.y, this.x)
val Vector4l.brgg get() = Vector4l(this.z, this.x, this.y, this.y)
val Vector4l.brgb get() = Vector4l(this.z, this.x, this.y, this.z)
var Vector4l.brga get() = Vector4l(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4l.brbr get() = Vector4l(this.z, this.x, this.z, this.x)
val Vector4l.brbg get() = Vector4l(this.z, this.x, this.z, this.y)
val Vector4l.brbb get() = Vector4l(this.z, this.x, this.z, this.z)
val Vector4l.brba get() = Vector4l(this.z, this.x, this.z, this.w)
val Vector4l.brar get() = Vector4l(this.z, this.x, this.w, this.x)
var Vector4l.brag get() = Vector4l(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4l.brab get() = Vector4l(this.z, this.x, this.w, this.z)
val Vector4l.braa get() = Vector4l(this.z, this.x, this.w, this.w)
val Vector4l.bgrr get() = Vector4l(this.z, this.y, this.x, this.x)
val Vector4l.bgrg get() = Vector4l(this.z, this.y, this.x, this.y)
val Vector4l.bgrb get() = Vector4l(this.z, this.y, this.x, this.z)
var Vector4l.bgra get() = Vector4l(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4l.bggr get() = Vector4l(this.z, this.y, this.y, this.x)
val Vector4l.bggg get() = Vector4l(this.z, this.y, this.y, this.y)
val Vector4l.bggb get() = Vector4l(this.z, this.y, this.y, this.z)
val Vector4l.bgga get() = Vector4l(this.z, this.y, this.y, this.w)
val Vector4l.bgbr get() = Vector4l(this.z, this.y, this.z, this.x)
val Vector4l.bgbg get() = Vector4l(this.z, this.y, this.z, this.y)
val Vector4l.bgbb get() = Vector4l(this.z, this.y, this.z, this.z)
val Vector4l.bgba get() = Vector4l(this.z, this.y, this.z, this.w)
var Vector4l.bgar get() = Vector4l(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4l.bgag get() = Vector4l(this.z, this.y, this.w, this.y)
val Vector4l.bgab get() = Vector4l(this.z, this.y, this.w, this.z)
val Vector4l.bgaa get() = Vector4l(this.z, this.y, this.w, this.w)
val Vector4l.bbrr get() = Vector4l(this.z, this.z, this.x, this.x)
val Vector4l.bbrg get() = Vector4l(this.z, this.z, this.x, this.y)
val Vector4l.bbrb get() = Vector4l(this.z, this.z, this.x, this.z)
val Vector4l.bbra get() = Vector4l(this.z, this.z, this.x, this.w)
val Vector4l.bbgr get() = Vector4l(this.z, this.z, this.y, this.x)
val Vector4l.bbgg get() = Vector4l(this.z, this.z, this.y, this.y)
val Vector4l.bbgb get() = Vector4l(this.z, this.z, this.y, this.z)
val Vector4l.bbga get() = Vector4l(this.z, this.z, this.y, this.w)
val Vector4l.bbbr get() = Vector4l(this.z, this.z, this.z, this.x)
val Vector4l.bbbg get() = Vector4l(this.z, this.z, this.z, this.y)
val Vector4l.bbbb get() = Vector4l(this.z, this.z, this.z, this.z)
val Vector4l.bbba get() = Vector4l(this.z, this.z, this.z, this.w)
val Vector4l.bbar get() = Vector4l(this.z, this.z, this.w, this.x)
val Vector4l.bbag get() = Vector4l(this.z, this.z, this.w, this.y)
val Vector4l.bbab get() = Vector4l(this.z, this.z, this.w, this.z)
val Vector4l.bbaa get() = Vector4l(this.z, this.z, this.w, this.w)
val Vector4l.barr get() = Vector4l(this.z, this.w, this.x, this.x)
var Vector4l.barg get() = Vector4l(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4l.barb get() = Vector4l(this.z, this.w, this.x, this.z)
val Vector4l.bara get() = Vector4l(this.z, this.w, this.x, this.w)
var Vector4l.bagr get() = Vector4l(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4l.bagg get() = Vector4l(this.z, this.w, this.y, this.y)
val Vector4l.bagb get() = Vector4l(this.z, this.w, this.y, this.z)
val Vector4l.baga get() = Vector4l(this.z, this.w, this.y, this.w)
val Vector4l.babr get() = Vector4l(this.z, this.w, this.z, this.x)
val Vector4l.babg get() = Vector4l(this.z, this.w, this.z, this.y)
val Vector4l.babb get() = Vector4l(this.z, this.w, this.z, this.z)
val Vector4l.baba get() = Vector4l(this.z, this.w, this.z, this.w)
val Vector4l.baar get() = Vector4l(this.z, this.w, this.w, this.x)
val Vector4l.baag get() = Vector4l(this.z, this.w, this.w, this.y)
val Vector4l.baab get() = Vector4l(this.z, this.w, this.w, this.z)
val Vector4l.baaa get() = Vector4l(this.z, this.w, this.w, this.w)
val Vector4l.arrr get() = Vector4l(this.w, this.x, this.x, this.x)
val Vector4l.arrg get() = Vector4l(this.w, this.x, this.x, this.y)
val Vector4l.arrb get() = Vector4l(this.w, this.x, this.x, this.z)
val Vector4l.arra get() = Vector4l(this.w, this.x, this.x, this.w)
val Vector4l.argr get() = Vector4l(this.w, this.x, this.y, this.x)
val Vector4l.argg get() = Vector4l(this.w, this.x, this.y, this.y)
var Vector4l.argb get() = Vector4l(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4l.arga get() = Vector4l(this.w, this.x, this.y, this.w)
val Vector4l.arbr get() = Vector4l(this.w, this.x, this.z, this.x)
var Vector4l.arbg get() = Vector4l(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4l.arbb get() = Vector4l(this.w, this.x, this.z, this.z)
val Vector4l.arba get() = Vector4l(this.w, this.x, this.z, this.w)
val Vector4l.arar get() = Vector4l(this.w, this.x, this.w, this.x)
val Vector4l.arag get() = Vector4l(this.w, this.x, this.w, this.y)
val Vector4l.arab get() = Vector4l(this.w, this.x, this.w, this.z)
val Vector4l.araa get() = Vector4l(this.w, this.x, this.w, this.w)
val Vector4l.agrr get() = Vector4l(this.w, this.y, this.x, this.x)
val Vector4l.agrg get() = Vector4l(this.w, this.y, this.x, this.y)
var Vector4l.agrb get() = Vector4l(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4l.agra get() = Vector4l(this.w, this.y, this.x, this.w)
val Vector4l.aggr get() = Vector4l(this.w, this.y, this.y, this.x)
val Vector4l.aggg get() = Vector4l(this.w, this.y, this.y, this.y)
val Vector4l.aggb get() = Vector4l(this.w, this.y, this.y, this.z)
val Vector4l.agga get() = Vector4l(this.w, this.y, this.y, this.w)
var Vector4l.agbr get() = Vector4l(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4l.agbg get() = Vector4l(this.w, this.y, this.z, this.y)
val Vector4l.agbb get() = Vector4l(this.w, this.y, this.z, this.z)
val Vector4l.agba get() = Vector4l(this.w, this.y, this.z, this.w)
val Vector4l.agar get() = Vector4l(this.w, this.y, this.w, this.x)
val Vector4l.agag get() = Vector4l(this.w, this.y, this.w, this.y)
val Vector4l.agab get() = Vector4l(this.w, this.y, this.w, this.z)
val Vector4l.agaa get() = Vector4l(this.w, this.y, this.w, this.w)
val Vector4l.abrr get() = Vector4l(this.w, this.z, this.x, this.x)
var Vector4l.abrg get() = Vector4l(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4l.abrb get() = Vector4l(this.w, this.z, this.x, this.z)
val Vector4l.abra get() = Vector4l(this.w, this.z, this.x, this.w)
var Vector4l.abgr get() = Vector4l(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4l.abgg get() = Vector4l(this.w, this.z, this.y, this.y)
val Vector4l.abgb get() = Vector4l(this.w, this.z, this.y, this.z)
val Vector4l.abga get() = Vector4l(this.w, this.z, this.y, this.w)
val Vector4l.abbr get() = Vector4l(this.w, this.z, this.z, this.x)
val Vector4l.abbg get() = Vector4l(this.w, this.z, this.z, this.y)
val Vector4l.abbb get() = Vector4l(this.w, this.z, this.z, this.z)
val Vector4l.abba get() = Vector4l(this.w, this.z, this.z, this.w)
val Vector4l.abar get() = Vector4l(this.w, this.z, this.w, this.x)
val Vector4l.abag get() = Vector4l(this.w, this.z, this.w, this.y)
val Vector4l.abab get() = Vector4l(this.w, this.z, this.w, this.z)
val Vector4l.abaa get() = Vector4l(this.w, this.z, this.w, this.w)
val Vector4l.aarr get() = Vector4l(this.w, this.w, this.x, this.x)
val Vector4l.aarg get() = Vector4l(this.w, this.w, this.x, this.y)
val Vector4l.aarb get() = Vector4l(this.w, this.w, this.x, this.z)
val Vector4l.aara get() = Vector4l(this.w, this.w, this.x, this.w)
val Vector4l.aagr get() = Vector4l(this.w, this.w, this.y, this.x)
val Vector4l.aagg get() = Vector4l(this.w, this.w, this.y, this.y)
val Vector4l.aagb get() = Vector4l(this.w, this.w, this.y, this.z)
val Vector4l.aaga get() = Vector4l(this.w, this.w, this.y, this.w)
val Vector4l.aabr get() = Vector4l(this.w, this.w, this.z, this.x)
val Vector4l.aabg get() = Vector4l(this.w, this.w, this.z, this.y)
val Vector4l.aabb get() = Vector4l(this.w, this.w, this.z, this.z)
val Vector4l.aaba get() = Vector4l(this.w, this.w, this.z, this.w)
val Vector4l.aaar get() = Vector4l(this.w, this.w, this.w, this.x)
val Vector4l.aaag get() = Vector4l(this.w, this.w, this.w, this.y)
val Vector4l.aaab get() = Vector4l(this.w, this.w, this.w, this.z)
val Vector4l.aaaa get() = Vector4l(this.w, this.w, this.w, this.w)