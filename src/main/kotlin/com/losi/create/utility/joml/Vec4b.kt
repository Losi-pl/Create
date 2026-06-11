@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.math.*
import com.losi.create.utility.Quad

fun Vector4b.toQuad() = Quad(this.x, this.y, this.z, this.w)
fun Quad<Boolean, Boolean, Boolean, Boolean>.toVector() = Vector4b(this.first, this.second, this.third, this.fourth)

// ===================================== XYZW =====================================
val Vector4b.xx get() = Vector2b(this.x, this.x)
var Vector4b.xy get() = Vector2b(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4b.xz get() = Vector2b(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4b.xw get() = Vector2b(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4b.yx get() = Vector2b(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4b.yy get() = Vector2b(this.y, this.y)
var Vector4b.yz get() = Vector2b(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4b.yw get() = Vector2b(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4b.zx get() = Vector2b(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4b.zy get() = Vector2b(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4b.zz get() = Vector2b(this.z, this.z)
var Vector4b.zw get() = Vector2b(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4b.wx get() = Vector2b(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4b.wy get() = Vector2b(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4b.wz get() = Vector2b(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4b.ww get() = Vector2b(this.w, this.w)

val Vector4b.xxx get() = Vector3b(this.x, this.x, this.x)
val Vector4b.xxy get() = Vector3b(this.x, this.x, this.y)
val Vector4b.xxz get() = Vector3b(this.x, this.x, this.z)
val Vector4b.xxw get() = Vector3b(this.x, this.x, this.w)
val Vector4b.xyx get() = Vector3b(this.x, this.y, this.x)
val Vector4b.xyy get() = Vector3b(this.x, this.y, this.y)
var Vector4b.xyz get() = Vector3b(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4b.xyw get() = Vector3b(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4b.xzx get() = Vector3b(this.x, this.z, this.x)
var Vector4b.xzy get() = Vector3b(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4b.xzz get() = Vector3b(this.x, this.z, this.z)
var Vector4b.xzw get() = Vector3b(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4b.xwx get() = Vector3b(this.x, this.w, this.x)
var Vector4b.xwy get() = Vector3b(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4b.xwz get() = Vector3b(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4b.xww get() = Vector3b(this.x, this.w, this.w)
val Vector4b.yxx get() = Vector3b(this.y, this.x, this.x)
val Vector4b.yxy get() = Vector3b(this.y, this.x, this.y)
var Vector4b.yxz get() = Vector3b(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4b.yxw get() = Vector3b(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4b.yyx get() = Vector3b(this.y, this.y, this.x)
val Vector4b.yyy get() = Vector3b(this.y, this.y, this.y)
val Vector4b.yyz get() = Vector3b(this.y, this.y, this.z)
val Vector4b.yyw get() = Vector3b(this.y, this.y, this.w)
var Vector4b.yzx get() = Vector3b(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4b.yzy get() = Vector3b(this.y, this.z, this.y)
val Vector4b.yzz get() = Vector3b(this.y, this.z, this.z)
var Vector4b.yzw get() = Vector3b(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4b.ywx get() = Vector3b(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4b.ywy get() = Vector3b(this.y, this.w, this.y)
var Vector4b.ywz get() = Vector3b(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4b.yww get() = Vector3b(this.y, this.w, this.w)
val Vector4b.zxx get() = Vector3b(this.z, this.x, this.x)
var Vector4b.zxy get() = Vector3b(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4b.zxz get() = Vector3b(this.z, this.x, this.z)
var Vector4b.zxw get() = Vector3b(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4b.zyx get() = Vector3b(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4b.zyy get() = Vector3b(this.z, this.y, this.y)
val Vector4b.zyz get() = Vector3b(this.z, this.y, this.z)
var Vector4b.zyw get() = Vector3b(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4b.zzx get() = Vector3b(this.z, this.z, this.x)
val Vector4b.zzy get() = Vector3b(this.z, this.z, this.y)
val Vector4b.zzz get() = Vector3b(this.z, this.z, this.z)
val Vector4b.zzw get() = Vector3b(this.z, this.z, this.w)
var Vector4b.zwx get() = Vector3b(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4b.zwy get() = Vector3b(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4b.zwz get() = Vector3b(this.z, this.w, this.z)
val Vector4b.zww get() = Vector3b(this.z, this.w, this.w)
val Vector4b.wxx get() = Vector3b(this.w, this.x, this.x)
var Vector4b.wxy get() = Vector3b(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4b.wxz get() = Vector3b(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4b.wxw get() = Vector3b(this.w, this.x, this.w)
var Vector4b.wyx get() = Vector3b(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4b.wyy get() = Vector3b(this.w, this.y, this.y)
var Vector4b.wyz get() = Vector3b(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4b.wyw get() = Vector3b(this.w, this.y, this.w)
var Vector4b.wzx get() = Vector3b(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4b.wzy get() = Vector3b(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4b.wzz get() = Vector3b(this.w, this.z, this.z)
val Vector4b.wzw get() = Vector3b(this.w, this.z, this.w)
val Vector4b.wwx get() = Vector3b(this.w, this.w, this.x)
val Vector4b.wwy get() = Vector3b(this.w, this.w, this.y)
val Vector4b.wwz get() = Vector3b(this.w, this.w, this.z)
val Vector4b.www get() = Vector3b(this.w, this.w, this.w)

val Vector4b.xxxx get() = Vector4b(this.x, this.x, this.x, this.x)
val Vector4b.xxxy get() = Vector4b(this.x, this.x, this.x, this.y)
val Vector4b.xxxz get() = Vector4b(this.x, this.x, this.x, this.z)
val Vector4b.xxxw get() = Vector4b(this.x, this.x, this.x, this.w)
val Vector4b.xxyx get() = Vector4b(this.x, this.x, this.y, this.x)
val Vector4b.xxyy get() = Vector4b(this.x, this.x, this.y, this.y)
val Vector4b.xxyz get() = Vector4b(this.x, this.x, this.y, this.z)
val Vector4b.xxyw get() = Vector4b(this.x, this.x, this.y, this.w)
val Vector4b.xxzx get() = Vector4b(this.x, this.x, this.z, this.x)
val Vector4b.xxzy get() = Vector4b(this.x, this.x, this.z, this.y)
val Vector4b.xxzz get() = Vector4b(this.x, this.x, this.z, this.z)
val Vector4b.xxzw get() = Vector4b(this.x, this.x, this.z, this.w)
val Vector4b.xxwx get() = Vector4b(this.x, this.x, this.w, this.x)
val Vector4b.xxwy get() = Vector4b(this.x, this.x, this.w, this.y)
val Vector4b.xxwz get() = Vector4b(this.x, this.x, this.w, this.z)
val Vector4b.xxww get() = Vector4b(this.x, this.x, this.w, this.w)
val Vector4b.xyxx get() = Vector4b(this.x, this.y, this.x, this.x)
val Vector4b.xyxy get() = Vector4b(this.x, this.y, this.x, this.y)
val Vector4b.xyxz get() = Vector4b(this.x, this.y, this.x, this.z)
val Vector4b.xyxw get() = Vector4b(this.x, this.y, this.x, this.w)
val Vector4b.xyyx get() = Vector4b(this.x, this.y, this.y, this.x)
val Vector4b.xyyy get() = Vector4b(this.x, this.y, this.y, this.y)
val Vector4b.xyyz get() = Vector4b(this.x, this.y, this.y, this.z)
val Vector4b.xyyw get() = Vector4b(this.x, this.y, this.y, this.w)
val Vector4b.xyzx get() = Vector4b(this.x, this.y, this.z, this.x)
val Vector4b.xyzy get() = Vector4b(this.x, this.y, this.z, this.y)
val Vector4b.xyzz get() = Vector4b(this.x, this.y, this.z, this.z)
var Vector4b.xyzw get() = Vector4b(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4b.xywx get() = Vector4b(this.x, this.y, this.w, this.x)
val Vector4b.xywy get() = Vector4b(this.x, this.y, this.w, this.y)
var Vector4b.xywz get() = Vector4b(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4b.xyww get() = Vector4b(this.x, this.y, this.w, this.w)
val Vector4b.xzxx get() = Vector4b(this.x, this.z, this.x, this.x)
val Vector4b.xzxy get() = Vector4b(this.x, this.z, this.x, this.y)
val Vector4b.xzxz get() = Vector4b(this.x, this.z, this.x, this.z)
val Vector4b.xzxw get() = Vector4b(this.x, this.z, this.x, this.w)
val Vector4b.xzyx get() = Vector4b(this.x, this.z, this.y, this.x)
val Vector4b.xzyy get() = Vector4b(this.x, this.z, this.y, this.y)
val Vector4b.xzyz get() = Vector4b(this.x, this.z, this.y, this.z)
var Vector4b.xzyw get() = Vector4b(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4b.xzzx get() = Vector4b(this.x, this.z, this.z, this.x)
val Vector4b.xzzy get() = Vector4b(this.x, this.z, this.z, this.y)
val Vector4b.xzzz get() = Vector4b(this.x, this.z, this.z, this.z)
val Vector4b.xzzw get() = Vector4b(this.x, this.z, this.z, this.w)
val Vector4b.xzwx get() = Vector4b(this.x, this.z, this.w, this.x)
var Vector4b.xzwy get() = Vector4b(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4b.xzwz get() = Vector4b(this.x, this.z, this.w, this.z)
val Vector4b.xzww get() = Vector4b(this.x, this.z, this.w, this.w)
val Vector4b.xwxx get() = Vector4b(this.x, this.w, this.x, this.x)
val Vector4b.xwxy get() = Vector4b(this.x, this.w, this.x, this.y)
val Vector4b.xwxz get() = Vector4b(this.x, this.w, this.x, this.z)
val Vector4b.xwxw get() = Vector4b(this.x, this.w, this.x, this.w)
val Vector4b.xwyx get() = Vector4b(this.x, this.w, this.y, this.x)
val Vector4b.xwyy get() = Vector4b(this.x, this.w, this.y, this.y)
var Vector4b.xwyz get() = Vector4b(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4b.xwyw get() = Vector4b(this.x, this.w, this.y, this.w)
val Vector4b.xwzx get() = Vector4b(this.x, this.w, this.z, this.x)
var Vector4b.xwzy get() = Vector4b(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4b.xwzz get() = Vector4b(this.x, this.w, this.z, this.z)
val Vector4b.xwzw get() = Vector4b(this.x, this.w, this.z, this.w)
val Vector4b.xwwx get() = Vector4b(this.x, this.w, this.w, this.x)
val Vector4b.xwwy get() = Vector4b(this.x, this.w, this.w, this.y)
val Vector4b.xwwz get() = Vector4b(this.x, this.w, this.w, this.z)
val Vector4b.xwww get() = Vector4b(this.x, this.w, this.w, this.w)
val Vector4b.yxxx get() = Vector4b(this.y, this.x, this.x, this.x)
val Vector4b.yxxy get() = Vector4b(this.y, this.x, this.x, this.y)
val Vector4b.yxxz get() = Vector4b(this.y, this.x, this.x, this.z)
val Vector4b.yxxw get() = Vector4b(this.y, this.x, this.x, this.w)
val Vector4b.yxyx get() = Vector4b(this.y, this.x, this.y, this.x)
val Vector4b.yxyy get() = Vector4b(this.y, this.x, this.y, this.y)
val Vector4b.yxyz get() = Vector4b(this.y, this.x, this.y, this.z)
val Vector4b.yxyw get() = Vector4b(this.y, this.x, this.y, this.w)
val Vector4b.yxzx get() = Vector4b(this.y, this.x, this.z, this.x)
val Vector4b.yxzy get() = Vector4b(this.y, this.x, this.z, this.y)
val Vector4b.yxzz get() = Vector4b(this.y, this.x, this.z, this.z)
var Vector4b.yxzw get() = Vector4b(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4b.yxwx get() = Vector4b(this.y, this.x, this.w, this.x)
val Vector4b.yxwy get() = Vector4b(this.y, this.x, this.w, this.y)
var Vector4b.yxwz get() = Vector4b(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4b.yxww get() = Vector4b(this.y, this.x, this.w, this.w)
val Vector4b.yyxx get() = Vector4b(this.y, this.y, this.x, this.x)
val Vector4b.yyxy get() = Vector4b(this.y, this.y, this.x, this.y)
val Vector4b.yyxz get() = Vector4b(this.y, this.y, this.x, this.z)
val Vector4b.yyxw get() = Vector4b(this.y, this.y, this.x, this.w)
val Vector4b.yyyx get() = Vector4b(this.y, this.y, this.y, this.x)
val Vector4b.yyyy get() = Vector4b(this.y, this.y, this.y, this.y)
val Vector4b.yyyz get() = Vector4b(this.y, this.y, this.y, this.z)
val Vector4b.yyyw get() = Vector4b(this.y, this.y, this.y, this.w)
val Vector4b.yyzx get() = Vector4b(this.y, this.y, this.z, this.x)
val Vector4b.yyzy get() = Vector4b(this.y, this.y, this.z, this.y)
val Vector4b.yyzz get() = Vector4b(this.y, this.y, this.z, this.z)
val Vector4b.yyzw get() = Vector4b(this.y, this.y, this.z, this.w)
val Vector4b.yywx get() = Vector4b(this.y, this.y, this.w, this.x)
val Vector4b.yywy get() = Vector4b(this.y, this.y, this.w, this.y)
val Vector4b.yywz get() = Vector4b(this.y, this.y, this.w, this.z)
val Vector4b.yyww get() = Vector4b(this.y, this.y, this.w, this.w)
val Vector4b.yzxx get() = Vector4b(this.y, this.z, this.x, this.x)
val Vector4b.yzxy get() = Vector4b(this.y, this.z, this.x, this.y)
val Vector4b.yzxz get() = Vector4b(this.y, this.z, this.x, this.z)
var Vector4b.yzxw get() = Vector4b(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4b.yzyx get() = Vector4b(this.y, this.z, this.y, this.x)
val Vector4b.yzyy get() = Vector4b(this.y, this.z, this.y, this.y)
val Vector4b.yzyz get() = Vector4b(this.y, this.z, this.y, this.z)
val Vector4b.yzyw get() = Vector4b(this.y, this.z, this.y, this.w)
val Vector4b.yzzx get() = Vector4b(this.y, this.z, this.z, this.x)
val Vector4b.yzzy get() = Vector4b(this.y, this.z, this.z, this.y)
val Vector4b.yzzz get() = Vector4b(this.y, this.z, this.z, this.z)
val Vector4b.yzzw get() = Vector4b(this.y, this.z, this.z, this.w)
var Vector4b.yzwx get() = Vector4b(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4b.yzwy get() = Vector4b(this.y, this.z, this.w, this.y)
val Vector4b.yzwz get() = Vector4b(this.y, this.z, this.w, this.z)
val Vector4b.yzww get() = Vector4b(this.y, this.z, this.w, this.w)
val Vector4b.ywxx get() = Vector4b(this.y, this.w, this.x, this.x)
val Vector4b.ywxy get() = Vector4b(this.y, this.w, this.x, this.y)
var Vector4b.ywxz get() = Vector4b(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4b.ywxw get() = Vector4b(this.y, this.w, this.x, this.w)
val Vector4b.ywyx get() = Vector4b(this.y, this.w, this.y, this.x)
val Vector4b.ywyy get() = Vector4b(this.y, this.w, this.y, this.y)
val Vector4b.ywyz get() = Vector4b(this.y, this.w, this.y, this.z)
val Vector4b.ywyw get() = Vector4b(this.y, this.w, this.y, this.w)
var Vector4b.ywzx get() = Vector4b(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4b.ywzy get() = Vector4b(this.y, this.w, this.z, this.y)
val Vector4b.ywzz get() = Vector4b(this.y, this.w, this.z, this.z)
val Vector4b.ywzw get() = Vector4b(this.y, this.w, this.z, this.w)
val Vector4b.ywwx get() = Vector4b(this.y, this.w, this.w, this.x)
val Vector4b.ywwy get() = Vector4b(this.y, this.w, this.w, this.y)
val Vector4b.ywwz get() = Vector4b(this.y, this.w, this.w, this.z)
val Vector4b.ywww get() = Vector4b(this.y, this.w, this.w, this.w)
val Vector4b.zxxx get() = Vector4b(this.z, this.x, this.x, this.x)
val Vector4b.zxxy get() = Vector4b(this.z, this.x, this.x, this.y)
val Vector4b.zxxz get() = Vector4b(this.z, this.x, this.x, this.z)
val Vector4b.zxxw get() = Vector4b(this.z, this.x, this.x, this.w)
val Vector4b.zxyx get() = Vector4b(this.z, this.x, this.y, this.x)
val Vector4b.zxyy get() = Vector4b(this.z, this.x, this.y, this.y)
val Vector4b.zxyz get() = Vector4b(this.z, this.x, this.y, this.z)
var Vector4b.zxyw get() = Vector4b(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4b.zxzx get() = Vector4b(this.z, this.x, this.z, this.x)
val Vector4b.zxzy get() = Vector4b(this.z, this.x, this.z, this.y)
val Vector4b.zxzz get() = Vector4b(this.z, this.x, this.z, this.z)
val Vector4b.zxzw get() = Vector4b(this.z, this.x, this.z, this.w)
val Vector4b.zxwx get() = Vector4b(this.z, this.x, this.w, this.x)
var Vector4b.zxwy get() = Vector4b(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4b.zxwz get() = Vector4b(this.z, this.x, this.w, this.z)
val Vector4b.zxww get() = Vector4b(this.z, this.x, this.w, this.w)
val Vector4b.zyxx get() = Vector4b(this.z, this.y, this.x, this.x)
val Vector4b.zyxy get() = Vector4b(this.z, this.y, this.x, this.y)
val Vector4b.zyxz get() = Vector4b(this.z, this.y, this.x, this.z)
var Vector4b.zyxw get() = Vector4b(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4b.zyyx get() = Vector4b(this.z, this.y, this.y, this.x)
val Vector4b.zyyy get() = Vector4b(this.z, this.y, this.y, this.y)
val Vector4b.zyyz get() = Vector4b(this.z, this.y, this.y, this.z)
val Vector4b.zyyw get() = Vector4b(this.z, this.y, this.y, this.w)
val Vector4b.zyzx get() = Vector4b(this.z, this.y, this.z, this.x)
val Vector4b.zyzy get() = Vector4b(this.z, this.y, this.z, this.y)
val Vector4b.zyzz get() = Vector4b(this.z, this.y, this.z, this.z)
val Vector4b.zyzw get() = Vector4b(this.z, this.y, this.z, this.w)
var Vector4b.zywx get() = Vector4b(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4b.zywy get() = Vector4b(this.z, this.y, this.w, this.y)
val Vector4b.zywz get() = Vector4b(this.z, this.y, this.w, this.z)
val Vector4b.zyww get() = Vector4b(this.z, this.y, this.w, this.w)
val Vector4b.zzxx get() = Vector4b(this.z, this.z, this.x, this.x)
val Vector4b.zzxy get() = Vector4b(this.z, this.z, this.x, this.y)
val Vector4b.zzxz get() = Vector4b(this.z, this.z, this.x, this.z)
val Vector4b.zzxw get() = Vector4b(this.z, this.z, this.x, this.w)
val Vector4b.zzyx get() = Vector4b(this.z, this.z, this.y, this.x)
val Vector4b.zzyy get() = Vector4b(this.z, this.z, this.y, this.y)
val Vector4b.zzyz get() = Vector4b(this.z, this.z, this.y, this.z)
val Vector4b.zzyw get() = Vector4b(this.z, this.z, this.y, this.w)
val Vector4b.zzzx get() = Vector4b(this.z, this.z, this.z, this.x)
val Vector4b.zzzy get() = Vector4b(this.z, this.z, this.z, this.y)
val Vector4b.zzzz get() = Vector4b(this.z, this.z, this.z, this.z)
val Vector4b.zzzw get() = Vector4b(this.z, this.z, this.z, this.w)
val Vector4b.zzwx get() = Vector4b(this.z, this.z, this.w, this.x)
val Vector4b.zzwy get() = Vector4b(this.z, this.z, this.w, this.y)
val Vector4b.zzwz get() = Vector4b(this.z, this.z, this.w, this.z)
val Vector4b.zzww get() = Vector4b(this.z, this.z, this.w, this.w)
val Vector4b.zwxx get() = Vector4b(this.z, this.w, this.x, this.x)
var Vector4b.zwxy get() = Vector4b(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4b.zwxz get() = Vector4b(this.z, this.w, this.x, this.z)
val Vector4b.zwxw get() = Vector4b(this.z, this.w, this.x, this.w)
var Vector4b.zwyx get() = Vector4b(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4b.zwyy get() = Vector4b(this.z, this.w, this.y, this.y)
val Vector4b.zwyz get() = Vector4b(this.z, this.w, this.y, this.z)
val Vector4b.zwyw get() = Vector4b(this.z, this.w, this.y, this.w)
val Vector4b.zwzx get() = Vector4b(this.z, this.w, this.z, this.x)
val Vector4b.zwzy get() = Vector4b(this.z, this.w, this.z, this.y)
val Vector4b.zwzz get() = Vector4b(this.z, this.w, this.z, this.z)
val Vector4b.zwzw get() = Vector4b(this.z, this.w, this.z, this.w)
val Vector4b.zwwx get() = Vector4b(this.z, this.w, this.w, this.x)
val Vector4b.zwwy get() = Vector4b(this.z, this.w, this.w, this.y)
val Vector4b.zwwz get() = Vector4b(this.z, this.w, this.w, this.z)
val Vector4b.zwww get() = Vector4b(this.z, this.w, this.w, this.w)
val Vector4b.wxxx get() = Vector4b(this.w, this.x, this.x, this.x)
val Vector4b.wxxy get() = Vector4b(this.w, this.x, this.x, this.y)
val Vector4b.wxxz get() = Vector4b(this.w, this.x, this.x, this.z)
val Vector4b.wxxw get() = Vector4b(this.w, this.x, this.x, this.w)
val Vector4b.wxyx get() = Vector4b(this.w, this.x, this.y, this.x)
val Vector4b.wxyy get() = Vector4b(this.w, this.x, this.y, this.y)
var Vector4b.wxyz get() = Vector4b(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4b.wxyw get() = Vector4b(this.w, this.x, this.y, this.w)
val Vector4b.wxzx get() = Vector4b(this.w, this.x, this.z, this.x)
var Vector4b.wxzy get() = Vector4b(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4b.wxzz get() = Vector4b(this.w, this.x, this.z, this.z)
val Vector4b.wxzw get() = Vector4b(this.w, this.x, this.z, this.w)
val Vector4b.wxwx get() = Vector4b(this.w, this.x, this.w, this.x)
val Vector4b.wxwy get() = Vector4b(this.w, this.x, this.w, this.y)
val Vector4b.wxwz get() = Vector4b(this.w, this.x, this.w, this.z)
val Vector4b.wxww get() = Vector4b(this.w, this.x, this.w, this.w)
val Vector4b.wyxx get() = Vector4b(this.w, this.y, this.x, this.x)
val Vector4b.wyxy get() = Vector4b(this.w, this.y, this.x, this.y)
var Vector4b.wyxz get() = Vector4b(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4b.wyxw get() = Vector4b(this.w, this.y, this.x, this.w)
val Vector4b.wyyx get() = Vector4b(this.w, this.y, this.y, this.x)
val Vector4b.wyyy get() = Vector4b(this.w, this.y, this.y, this.y)
val Vector4b.wyyz get() = Vector4b(this.w, this.y, this.y, this.z)
val Vector4b.wyyw get() = Vector4b(this.w, this.y, this.y, this.w)
var Vector4b.wyzx get() = Vector4b(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4b.wyzy get() = Vector4b(this.w, this.y, this.z, this.y)
val Vector4b.wyzz get() = Vector4b(this.w, this.y, this.z, this.z)
val Vector4b.wyzw get() = Vector4b(this.w, this.y, this.z, this.w)
val Vector4b.wywx get() = Vector4b(this.w, this.y, this.w, this.x)
val Vector4b.wywy get() = Vector4b(this.w, this.y, this.w, this.y)
val Vector4b.wywz get() = Vector4b(this.w, this.y, this.w, this.z)
val Vector4b.wyww get() = Vector4b(this.w, this.y, this.w, this.w)
val Vector4b.wzxx get() = Vector4b(this.w, this.z, this.x, this.x)
var Vector4b.wzxy get() = Vector4b(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4b.wzxz get() = Vector4b(this.w, this.z, this.x, this.z)
val Vector4b.wzxw get() = Vector4b(this.w, this.z, this.x, this.w)
var Vector4b.wzyx get() = Vector4b(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4b.wzyy get() = Vector4b(this.w, this.z, this.y, this.y)
val Vector4b.wzyz get() = Vector4b(this.w, this.z, this.y, this.z)
val Vector4b.wzyw get() = Vector4b(this.w, this.z, this.y, this.w)
val Vector4b.wzzx get() = Vector4b(this.w, this.z, this.z, this.x)
val Vector4b.wzzy get() = Vector4b(this.w, this.z, this.z, this.y)
val Vector4b.wzzz get() = Vector4b(this.w, this.z, this.z, this.z)
val Vector4b.wzzw get() = Vector4b(this.w, this.z, this.z, this.w)
val Vector4b.wzwx get() = Vector4b(this.w, this.z, this.w, this.x)
val Vector4b.wzwy get() = Vector4b(this.w, this.z, this.w, this.y)
val Vector4b.wzwz get() = Vector4b(this.w, this.z, this.w, this.z)
val Vector4b.wzww get() = Vector4b(this.w, this.z, this.w, this.w)
val Vector4b.wwxx get() = Vector4b(this.w, this.w, this.x, this.x)
val Vector4b.wwxy get() = Vector4b(this.w, this.w, this.x, this.y)
val Vector4b.wwxz get() = Vector4b(this.w, this.w, this.x, this.z)
val Vector4b.wwxw get() = Vector4b(this.w, this.w, this.x, this.w)
val Vector4b.wwyx get() = Vector4b(this.w, this.w, this.y, this.x)
val Vector4b.wwyy get() = Vector4b(this.w, this.w, this.y, this.y)
val Vector4b.wwyz get() = Vector4b(this.w, this.w, this.y, this.z)
val Vector4b.wwyw get() = Vector4b(this.w, this.w, this.y, this.w)
val Vector4b.wwzx get() = Vector4b(this.w, this.w, this.z, this.x)
val Vector4b.wwzy get() = Vector4b(this.w, this.w, this.z, this.y)
val Vector4b.wwzz get() = Vector4b(this.w, this.w, this.z, this.z)
val Vector4b.wwzw get() = Vector4b(this.w, this.w, this.z, this.w)
val Vector4b.wwwx get() = Vector4b(this.w, this.w, this.w, this.x)
val Vector4b.wwwy get() = Vector4b(this.w, this.w, this.w, this.y)
val Vector4b.wwwz get() = Vector4b(this.w, this.w, this.w, this.z)
val Vector4b.wwww get() = Vector4b(this.w, this.w, this.w, this.w)

// ===================================== RGBA =====================================
var Vector4b.r: Boolean get() = this.x; set(it) { this.x = it }
var Vector4b.g: Boolean get() = this.y; set(it) { this.y = it }
var Vector4b.b: Boolean get() = this.z; set(it) { this.z = it }
var Vector4b.a: Boolean get() = this.w; set(it) { this.w = it }

val Vector4b.rr get() = Vector2b(this.x, this.x)
var Vector4b.rg get() = Vector2b(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4b.rb get() = Vector2b(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4b.ra get() = Vector2b(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4b.gr get() = Vector2b(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4b.gg get() = Vector2b(this.y, this.y)
var Vector4b.gb get() = Vector2b(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4b.ga get() = Vector2b(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4b.br get() = Vector2b(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4b.bg get() = Vector2b(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4b.bb get() = Vector2b(this.z, this.z)
var Vector4b.ba get() = Vector2b(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4b.ar get() = Vector2b(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4b.ag get() = Vector2b(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4b.ab get() = Vector2b(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4b.aa get() = Vector2b(this.w, this.w)

val Vector4b.rrr get() = Vector3b(this.x, this.x, this.x)
val Vector4b.rrg get() = Vector3b(this.x, this.x, this.y)
val Vector4b.rrb get() = Vector3b(this.x, this.x, this.z)
val Vector4b.rra get() = Vector3b(this.x, this.x, this.w)
val Vector4b.rgr get() = Vector3b(this.x, this.y, this.x)
val Vector4b.rgg get() = Vector3b(this.x, this.y, this.y)
var Vector4b.rgb get() = Vector3b(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4b.rga get() = Vector3b(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4b.rbr get() = Vector3b(this.x, this.z, this.x)
var Vector4b.rbg get() = Vector3b(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4b.rbb get() = Vector3b(this.x, this.z, this.z)
var Vector4b.rba get() = Vector3b(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
val Vector4b.rar get() = Vector3b(this.x, this.w, this.x)
var Vector4b.rag get() = Vector3b(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4b.rab get() = Vector3b(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4b.raa get() = Vector3b(this.x, this.w, this.w)
val Vector4b.grr get() = Vector3b(this.y, this.x, this.x)
val Vector4b.grg get() = Vector3b(this.y, this.x, this.y)
var Vector4b.grb get() = Vector3b(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
var Vector4b.gra get() = Vector3b(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4b.ggr get() = Vector3b(this.y, this.y, this.x)
val Vector4b.ggg get() = Vector3b(this.y, this.y, this.y)
val Vector4b.ggb get() = Vector3b(this.y, this.y, this.z)
val Vector4b.gga get() = Vector3b(this.y, this.y, this.w)
var Vector4b.gbr get() = Vector3b(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4b.gbg get() = Vector3b(this.y, this.z, this.y)
val Vector4b.gbb get() = Vector3b(this.y, this.z, this.z)
var Vector4b.gba get() = Vector3b(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4b.gar get() = Vector3b(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4b.gag get() = Vector3b(this.y, this.w, this.y)
var Vector4b.gab get() = Vector3b(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4b.gaa get() = Vector3b(this.y, this.w, this.w)
val Vector4b.brr get() = Vector3b(this.z, this.x, this.x)
var Vector4b.brg get() = Vector3b(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4b.brb get() = Vector3b(this.z, this.x, this.z)
var Vector4b.bra get() = Vector3b(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4b.bgr get() = Vector3b(this.z, this.y, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z }
val Vector4b.bgg get() = Vector3b(this.z, this.y, this.y)
val Vector4b.bgb get() = Vector3b(this.z, this.y, this.z)
var Vector4b.bga get() = Vector3b(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4b.bbr get() = Vector3b(this.z, this.z, this.x)
val Vector4b.bbg get() = Vector3b(this.z, this.z, this.y)
val Vector4b.bbb get() = Vector3b(this.z, this.z, this.z)
val Vector4b.bba get() = Vector3b(this.z, this.z, this.w)
var Vector4b.bar get() = Vector3b(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4b.bag get() = Vector3b(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4b.bab get() = Vector3b(this.z, this.w, this.z)
val Vector4b.baa get() = Vector3b(this.z, this.w, this.w)
val Vector4b.arr get() = Vector3b(this.w, this.x, this.x)
var Vector4b.arg get() = Vector3b(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4b.arb get() = Vector3b(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4b.ara get() = Vector3b(this.w, this.x, this.w)
var Vector4b.agr get() = Vector3b(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4b.agg get() = Vector3b(this.w, this.y, this.y)
var Vector4b.agb get() = Vector3b(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4b.aga get() = Vector3b(this.w, this.y, this.w)
var Vector4b.abr get() = Vector3b(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4b.abg get() = Vector3b(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4b.abb get() = Vector3b(this.w, this.z, this.z)
val Vector4b.aba get() = Vector3b(this.w, this.z, this.w)
val Vector4b.aar get() = Vector3b(this.w, this.w, this.x)
val Vector4b.aag get() = Vector3b(this.w, this.w, this.y)
val Vector4b.aab get() = Vector3b(this.w, this.w, this.z)
val Vector4b.aaa get() = Vector3b(this.w, this.w, this.w)

val Vector4b.rrrr get() = Vector4b(this.x, this.x, this.x, this.x)
val Vector4b.rrrg get() = Vector4b(this.x, this.x, this.x, this.y)
val Vector4b.rrrb get() = Vector4b(this.x, this.x, this.x, this.z)
val Vector4b.rrra get() = Vector4b(this.x, this.x, this.x, this.w)
val Vector4b.rrgr get() = Vector4b(this.x, this.x, this.y, this.x)
val Vector4b.rrgg get() = Vector4b(this.x, this.x, this.y, this.y)
val Vector4b.rrgb get() = Vector4b(this.x, this.x, this.y, this.z)
val Vector4b.rrga get() = Vector4b(this.x, this.x, this.y, this.w)
val Vector4b.rrbr get() = Vector4b(this.x, this.x, this.z, this.x)
val Vector4b.rrbg get() = Vector4b(this.x, this.x, this.z, this.y)
val Vector4b.rrbb get() = Vector4b(this.x, this.x, this.z, this.z)
val Vector4b.rrba get() = Vector4b(this.x, this.x, this.z, this.w)
val Vector4b.rrar get() = Vector4b(this.x, this.x, this.w, this.x)
val Vector4b.rrag get() = Vector4b(this.x, this.x, this.w, this.y)
val Vector4b.rrab get() = Vector4b(this.x, this.x, this.w, this.z)
val Vector4b.rraa get() = Vector4b(this.x, this.x, this.w, this.w)
val Vector4b.rgrr get() = Vector4b(this.x, this.y, this.x, this.x)
val Vector4b.rgrg get() = Vector4b(this.x, this.y, this.x, this.y)
val Vector4b.rgrb get() = Vector4b(this.x, this.y, this.x, this.z)
val Vector4b.rgra get() = Vector4b(this.x, this.y, this.x, this.w)
val Vector4b.rggr get() = Vector4b(this.x, this.y, this.y, this.x)
val Vector4b.rggg get() = Vector4b(this.x, this.y, this.y, this.y)
val Vector4b.rggb get() = Vector4b(this.x, this.y, this.y, this.z)
val Vector4b.rgga get() = Vector4b(this.x, this.y, this.y, this.w)
val Vector4b.rgbr get() = Vector4b(this.x, this.y, this.z, this.x)
val Vector4b.rgbg get() = Vector4b(this.x, this.y, this.z, this.y)
val Vector4b.rgbb get() = Vector4b(this.x, this.y, this.z, this.z)
var Vector4b.rgba get() = Vector4b(this.x, this.y, this.z, this.w); set(v) { this.x = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
val Vector4b.rgar get() = Vector4b(this.x, this.y, this.w, this.x)
val Vector4b.rgag get() = Vector4b(this.x, this.y, this.w, this.y)
var Vector4b.rgab get() = Vector4b(this.x, this.y, this.w, this.z); set(v) { this.x = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
val Vector4b.rgaa get() = Vector4b(this.x, this.y, this.w, this.w)
val Vector4b.rbrr get() = Vector4b(this.x, this.z, this.x, this.x)
val Vector4b.rbrg get() = Vector4b(this.x, this.z, this.x, this.y)
val Vector4b.rbrb get() = Vector4b(this.x, this.z, this.x, this.z)
val Vector4b.rbra get() = Vector4b(this.x, this.z, this.x, this.w)
val Vector4b.rbgr get() = Vector4b(this.x, this.z, this.y, this.x)
val Vector4b.rbgg get() = Vector4b(this.x, this.z, this.y, this.y)
val Vector4b.rbgb get() = Vector4b(this.x, this.z, this.y, this.z)
var Vector4b.rbga get() = Vector4b(this.x, this.z, this.y, this.w); set(v) { this.x = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
val Vector4b.rbbr get() = Vector4b(this.x, this.z, this.z, this.x)
val Vector4b.rbbg get() = Vector4b(this.x, this.z, this.z, this.y)
val Vector4b.rbbb get() = Vector4b(this.x, this.z, this.z, this.z)
val Vector4b.rbba get() = Vector4b(this.x, this.z, this.z, this.w)
val Vector4b.rbar get() = Vector4b(this.x, this.z, this.w, this.x)
var Vector4b.rbag get() = Vector4b(this.x, this.z, this.w, this.y); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4b.rbab get() = Vector4b(this.x, this.z, this.w, this.z)
val Vector4b.rbaa get() = Vector4b(this.x, this.z, this.w, this.w)
val Vector4b.rarr get() = Vector4b(this.x, this.w, this.x, this.x)
val Vector4b.rarg get() = Vector4b(this.x, this.w, this.x, this.y)
val Vector4b.rarb get() = Vector4b(this.x, this.w, this.x, this.z)
val Vector4b.rara get() = Vector4b(this.x, this.w, this.x, this.w)
val Vector4b.ragr get() = Vector4b(this.x, this.w, this.y, this.x)
val Vector4b.ragg get() = Vector4b(this.x, this.w, this.y, this.y)
var Vector4b.ragb get() = Vector4b(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4b.raga get() = Vector4b(this.x, this.w, this.y, this.w)
val Vector4b.rabr get() = Vector4b(this.x, this.w, this.z, this.x)
var Vector4b.rabg get() = Vector4b(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4b.rabb get() = Vector4b(this.x, this.w, this.z, this.z)
val Vector4b.raba get() = Vector4b(this.x, this.w, this.z, this.w)
val Vector4b.raar get() = Vector4b(this.x, this.w, this.w, this.x)
val Vector4b.raag get() = Vector4b(this.x, this.w, this.w, this.y)
val Vector4b.raab get() = Vector4b(this.x, this.w, this.w, this.z)
val Vector4b.raaa get() = Vector4b(this.x, this.w, this.w, this.w)
val Vector4b.grrr get() = Vector4b(this.y, this.x, this.x, this.x)
val Vector4b.grrg get() = Vector4b(this.y, this.x, this.x, this.y)
val Vector4b.grrb get() = Vector4b(this.y, this.x, this.x, this.z)
val Vector4b.grra get() = Vector4b(this.y, this.x, this.x, this.w)
val Vector4b.grgr get() = Vector4b(this.y, this.x, this.y, this.x)
val Vector4b.grgg get() = Vector4b(this.y, this.x, this.y, this.y)
val Vector4b.grgb get() = Vector4b(this.y, this.x, this.y, this.z)
val Vector4b.grga get() = Vector4b(this.y, this.x, this.y, this.w)
val Vector4b.grbr get() = Vector4b(this.y, this.x, this.z, this.x)
val Vector4b.grbg get() = Vector4b(this.y, this.x, this.z, this.y)
val Vector4b.grbb get() = Vector4b(this.y, this.x, this.z, this.z)
var Vector4b.grba get() = Vector4b(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4b.grar get() = Vector4b(this.y, this.x, this.w, this.x)
val Vector4b.grag get() = Vector4b(this.y, this.x, this.w, this.y)
var Vector4b.grab get() = Vector4b(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4b.graa get() = Vector4b(this.y, this.x, this.w, this.w)
val Vector4b.ggrr get() = Vector4b(this.y, this.y, this.x, this.x)
val Vector4b.ggrg get() = Vector4b(this.y, this.y, this.x, this.y)
val Vector4b.ggrb get() = Vector4b(this.y, this.y, this.x, this.z)
val Vector4b.ggra get() = Vector4b(this.y, this.y, this.x, this.w)
val Vector4b.gggr get() = Vector4b(this.y, this.y, this.y, this.x)
val Vector4b.gggg get() = Vector4b(this.y, this.y, this.y, this.y)
val Vector4b.gggb get() = Vector4b(this.y, this.y, this.y, this.z)
val Vector4b.ggga get() = Vector4b(this.y, this.y, this.y, this.w)
val Vector4b.ggbr get() = Vector4b(this.y, this.y, this.z, this.x)
val Vector4b.ggbg get() = Vector4b(this.y, this.y, this.z, this.y)
val Vector4b.ggbb get() = Vector4b(this.y, this.y, this.z, this.z)
val Vector4b.ggba get() = Vector4b(this.y, this.y, this.z, this.w)
val Vector4b.ggar get() = Vector4b(this.y, this.y, this.w, this.x)
val Vector4b.ggag get() = Vector4b(this.y, this.y, this.w, this.y)
val Vector4b.ggab get() = Vector4b(this.y, this.y, this.w, this.z)
val Vector4b.ggaa get() = Vector4b(this.y, this.y, this.w, this.w)
val Vector4b.gbrr get() = Vector4b(this.y, this.z, this.x, this.x)
val Vector4b.gbrg get() = Vector4b(this.y, this.z, this.x, this.y)
val Vector4b.gbrb get() = Vector4b(this.y, this.z, this.x, this.z)
var Vector4b.gbra get() = Vector4b(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4b.gbgr get() = Vector4b(this.y, this.z, this.y, this.x)
val Vector4b.gbgg get() = Vector4b(this.y, this.z, this.y, this.y)
val Vector4b.gbgb get() = Vector4b(this.y, this.z, this.y, this.z)
val Vector4b.gbga get() = Vector4b(this.y, this.z, this.y, this.w)
val Vector4b.gbbr get() = Vector4b(this.y, this.z, this.z, this.x)
val Vector4b.gbbg get() = Vector4b(this.y, this.z, this.z, this.y)
val Vector4b.gbbb get() = Vector4b(this.y, this.z, this.z, this.z)
val Vector4b.gbba get() = Vector4b(this.y, this.z, this.z, this.w)
var Vector4b.gbar get() = Vector4b(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
val Vector4b.gbag get() = Vector4b(this.y, this.z, this.w, this.y)
val Vector4b.gbab get() = Vector4b(this.y, this.z, this.w, this.z)
val Vector4b.gbaa get() = Vector4b(this.y, this.z, this.w, this.w)
val Vector4b.garr get() = Vector4b(this.y, this.w, this.x, this.x)
val Vector4b.garg get() = Vector4b(this.y, this.w, this.x, this.y)
var Vector4b.garb get() = Vector4b(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4b.gara get() = Vector4b(this.y, this.w, this.x, this.w)
val Vector4b.gagr get() = Vector4b(this.y, this.w, this.y, this.x)
val Vector4b.gagg get() = Vector4b(this.y, this.w, this.y, this.y)
val Vector4b.gagb get() = Vector4b(this.y, this.w, this.y, this.z)
val Vector4b.gaga get() = Vector4b(this.y, this.w, this.y, this.w)
var Vector4b.gabr get() = Vector4b(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
val Vector4b.gabg get() = Vector4b(this.y, this.w, this.z, this.y)
val Vector4b.gabb get() = Vector4b(this.y, this.w, this.z, this.z)
val Vector4b.gaba get() = Vector4b(this.y, this.w, this.z, this.w)
val Vector4b.gaar get() = Vector4b(this.y, this.w, this.w, this.x)
val Vector4b.gaag get() = Vector4b(this.y, this.w, this.w, this.y)
val Vector4b.gaab get() = Vector4b(this.y, this.w, this.w, this.z)
val Vector4b.gaaa get() = Vector4b(this.y, this.w, this.w, this.w)
val Vector4b.brrr get() = Vector4b(this.z, this.x, this.x, this.x)
val Vector4b.brrg get() = Vector4b(this.z, this.x, this.x, this.y)
val Vector4b.brrb get() = Vector4b(this.z, this.x, this.x, this.z)
val Vector4b.brra get() = Vector4b(this.z, this.x, this.x, this.w)
val Vector4b.brgr get() = Vector4b(this.z, this.x, this.y, this.x)
val Vector4b.brgg get() = Vector4b(this.z, this.x, this.y, this.y)
val Vector4b.brgb get() = Vector4b(this.z, this.x, this.y, this.z)
var Vector4b.brga get() = Vector4b(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4b.brbr get() = Vector4b(this.z, this.x, this.z, this.x)
val Vector4b.brbg get() = Vector4b(this.z, this.x, this.z, this.y)
val Vector4b.brbb get() = Vector4b(this.z, this.x, this.z, this.z)
val Vector4b.brba get() = Vector4b(this.z, this.x, this.z, this.w)
val Vector4b.brar get() = Vector4b(this.z, this.x, this.w, this.x)
var Vector4b.brag get() = Vector4b(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
val Vector4b.brab get() = Vector4b(this.z, this.x, this.w, this.z)
val Vector4b.braa get() = Vector4b(this.z, this.x, this.w, this.w)
val Vector4b.bgrr get() = Vector4b(this.z, this.y, this.x, this.x)
val Vector4b.bgrg get() = Vector4b(this.z, this.y, this.x, this.y)
val Vector4b.bgrb get() = Vector4b(this.z, this.y, this.x, this.z)
var Vector4b.bgra get() = Vector4b(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
val Vector4b.bggr get() = Vector4b(this.z, this.y, this.y, this.x)
val Vector4b.bggg get() = Vector4b(this.z, this.y, this.y, this.y)
val Vector4b.bggb get() = Vector4b(this.z, this.y, this.y, this.z)
val Vector4b.bgga get() = Vector4b(this.z, this.y, this.y, this.w)
val Vector4b.bgbr get() = Vector4b(this.z, this.y, this.z, this.x)
val Vector4b.bgbg get() = Vector4b(this.z, this.y, this.z, this.y)
val Vector4b.bgbb get() = Vector4b(this.z, this.y, this.z, this.z)
val Vector4b.bgba get() = Vector4b(this.z, this.y, this.z, this.w)
var Vector4b.bgar get() = Vector4b(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
val Vector4b.bgag get() = Vector4b(this.z, this.y, this.w, this.y)
val Vector4b.bgab get() = Vector4b(this.z, this.y, this.w, this.z)
val Vector4b.bgaa get() = Vector4b(this.z, this.y, this.w, this.w)
val Vector4b.bbrr get() = Vector4b(this.z, this.z, this.x, this.x)
val Vector4b.bbrg get() = Vector4b(this.z, this.z, this.x, this.y)
val Vector4b.bbrb get() = Vector4b(this.z, this.z, this.x, this.z)
val Vector4b.bbra get() = Vector4b(this.z, this.z, this.x, this.w)
val Vector4b.bbgr get() = Vector4b(this.z, this.z, this.y, this.x)
val Vector4b.bbgg get() = Vector4b(this.z, this.z, this.y, this.y)
val Vector4b.bbgb get() = Vector4b(this.z, this.z, this.y, this.z)
val Vector4b.bbga get() = Vector4b(this.z, this.z, this.y, this.w)
val Vector4b.bbbr get() = Vector4b(this.z, this.z, this.z, this.x)
val Vector4b.bbbg get() = Vector4b(this.z, this.z, this.z, this.y)
val Vector4b.bbbb get() = Vector4b(this.z, this.z, this.z, this.z)
val Vector4b.bbba get() = Vector4b(this.z, this.z, this.z, this.w)
val Vector4b.bbar get() = Vector4b(this.z, this.z, this.w, this.x)
val Vector4b.bbag get() = Vector4b(this.z, this.z, this.w, this.y)
val Vector4b.bbab get() = Vector4b(this.z, this.z, this.w, this.z)
val Vector4b.bbaa get() = Vector4b(this.z, this.z, this.w, this.w)
val Vector4b.barr get() = Vector4b(this.z, this.w, this.x, this.x)
var Vector4b.barg get() = Vector4b(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
val Vector4b.barb get() = Vector4b(this.z, this.w, this.x, this.z)
val Vector4b.bara get() = Vector4b(this.z, this.w, this.x, this.w)
var Vector4b.bagr get() = Vector4b(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4b.bagg get() = Vector4b(this.z, this.w, this.y, this.y)
val Vector4b.bagb get() = Vector4b(this.z, this.w, this.y, this.z)
val Vector4b.baga get() = Vector4b(this.z, this.w, this.y, this.w)
val Vector4b.babr get() = Vector4b(this.z, this.w, this.z, this.x)
val Vector4b.babg get() = Vector4b(this.z, this.w, this.z, this.y)
val Vector4b.babb get() = Vector4b(this.z, this.w, this.z, this.z)
val Vector4b.baba get() = Vector4b(this.z, this.w, this.z, this.w)
val Vector4b.baar get() = Vector4b(this.z, this.w, this.w, this.x)
val Vector4b.baag get() = Vector4b(this.z, this.w, this.w, this.y)
val Vector4b.baab get() = Vector4b(this.z, this.w, this.w, this.z)
val Vector4b.baaa get() = Vector4b(this.z, this.w, this.w, this.w)
val Vector4b.arrr get() = Vector4b(this.w, this.x, this.x, this.x)
val Vector4b.arrg get() = Vector4b(this.w, this.x, this.x, this.y)
val Vector4b.arrb get() = Vector4b(this.w, this.x, this.x, this.z)
val Vector4b.arra get() = Vector4b(this.w, this.x, this.x, this.w)
val Vector4b.argr get() = Vector4b(this.w, this.x, this.y, this.x)
val Vector4b.argg get() = Vector4b(this.w, this.x, this.y, this.y)
var Vector4b.argb get() = Vector4b(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4b.arga get() = Vector4b(this.w, this.x, this.y, this.w)
val Vector4b.arbr get() = Vector4b(this.w, this.x, this.z, this.x)
var Vector4b.arbg get() = Vector4b(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4b.arbb get() = Vector4b(this.w, this.x, this.z, this.z)
val Vector4b.arba get() = Vector4b(this.w, this.x, this.z, this.w)
val Vector4b.arar get() = Vector4b(this.w, this.x, this.w, this.x)
val Vector4b.arag get() = Vector4b(this.w, this.x, this.w, this.y)
val Vector4b.arab get() = Vector4b(this.w, this.x, this.w, this.z)
val Vector4b.araa get() = Vector4b(this.w, this.x, this.w, this.w)
val Vector4b.agrr get() = Vector4b(this.w, this.y, this.x, this.x)
val Vector4b.agrg get() = Vector4b(this.w, this.y, this.x, this.y)
var Vector4b.agrb get() = Vector4b(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4b.agra get() = Vector4b(this.w, this.y, this.x, this.w)
val Vector4b.aggr get() = Vector4b(this.w, this.y, this.y, this.x)
val Vector4b.aggg get() = Vector4b(this.w, this.y, this.y, this.y)
val Vector4b.aggb get() = Vector4b(this.w, this.y, this.y, this.z)
val Vector4b.agga get() = Vector4b(this.w, this.y, this.y, this.w)
var Vector4b.agbr get() = Vector4b(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
val Vector4b.agbg get() = Vector4b(this.w, this.y, this.z, this.y)
val Vector4b.agbb get() = Vector4b(this.w, this.y, this.z, this.z)
val Vector4b.agba get() = Vector4b(this.w, this.y, this.z, this.w)
val Vector4b.agar get() = Vector4b(this.w, this.y, this.w, this.x)
val Vector4b.agag get() = Vector4b(this.w, this.y, this.w, this.y)
val Vector4b.agab get() = Vector4b(this.w, this.y, this.w, this.z)
val Vector4b.agaa get() = Vector4b(this.w, this.y, this.w, this.w)
val Vector4b.abrr get() = Vector4b(this.w, this.z, this.x, this.x)
var Vector4b.abrg get() = Vector4b(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4b.abrb get() = Vector4b(this.w, this.z, this.x, this.z)
val Vector4b.abra get() = Vector4b(this.w, this.z, this.x, this.w)
var Vector4b.abgr get() = Vector4b(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4b.abgg get() = Vector4b(this.w, this.z, this.y, this.y)
val Vector4b.abgb get() = Vector4b(this.w, this.z, this.y, this.z)
val Vector4b.abga get() = Vector4b(this.w, this.z, this.y, this.w)
val Vector4b.abbr get() = Vector4b(this.w, this.z, this.z, this.x)
val Vector4b.abbg get() = Vector4b(this.w, this.z, this.z, this.y)
val Vector4b.abbb get() = Vector4b(this.w, this.z, this.z, this.z)
val Vector4b.abba get() = Vector4b(this.w, this.z, this.z, this.w)
val Vector4b.abar get() = Vector4b(this.w, this.z, this.w, this.x)
val Vector4b.abag get() = Vector4b(this.w, this.z, this.w, this.y)
val Vector4b.abab get() = Vector4b(this.w, this.z, this.w, this.z)
val Vector4b.abaa get() = Vector4b(this.w, this.z, this.w, this.w)
val Vector4b.aarr get() = Vector4b(this.w, this.w, this.x, this.x)
val Vector4b.aarg get() = Vector4b(this.w, this.w, this.x, this.y)
val Vector4b.aarb get() = Vector4b(this.w, this.w, this.x, this.z)
val Vector4b.aara get() = Vector4b(this.w, this.w, this.x, this.w)
val Vector4b.aagr get() = Vector4b(this.w, this.w, this.y, this.x)
val Vector4b.aagg get() = Vector4b(this.w, this.w, this.y, this.y)
val Vector4b.aagb get() = Vector4b(this.w, this.w, this.y, this.z)
val Vector4b.aaga get() = Vector4b(this.w, this.w, this.y, this.w)
val Vector4b.aabr get() = Vector4b(this.w, this.w, this.z, this.x)
val Vector4b.aabg get() = Vector4b(this.w, this.w, this.z, this.y)
val Vector4b.aabb get() = Vector4b(this.w, this.w, this.z, this.z)
val Vector4b.aaba get() = Vector4b(this.w, this.w, this.z, this.w)
val Vector4b.aaar get() = Vector4b(this.w, this.w, this.w, this.x)
val Vector4b.aaag get() = Vector4b(this.w, this.w, this.w, this.y)
val Vector4b.aaab get() = Vector4b(this.w, this.w, this.w, this.z)
val Vector4b.aaaa get() = Vector4b(this.w, this.w, this.w, this.w)