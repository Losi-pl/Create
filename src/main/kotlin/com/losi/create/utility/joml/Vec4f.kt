@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.utility.Quad
import org.joml.*

fun Vector4f.toQuad() = Quad(this.x, this.y, this.z, this.w)
fun Quad<Float, Float, Float, Float>.toVector() = Vector4f(this.first, this.second, this.third, this.fourth)

// ===================================== XYZW =====================================
val Vector4f.xx get() = Vector2f(this.x, this.x)
var Vector4f.xy get() = Vector2f(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4f.xz get() = Vector2f(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4f.xw get() = Vector2f(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4f.yx get() = Vector2f(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4f.yy get() = Vector2f(this.y, this.y)
var Vector4f.yz get() = Vector2f(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4f.yw get() = Vector2f(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4f.zx get() = Vector2f(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4f.zy get() = Vector2f(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4f.zz get() = Vector2f(this.z, this.z)
var Vector4f.zw get() = Vector2f(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4f.wx get() = Vector2f(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4f.wy get() = Vector2f(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4f.wz get() = Vector2f(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4f.ww get() = Vector2f(this.w, this.w)

val Vector4f.xxx get() = Vector3f(this.x, this.x, this.x)
val Vector4f.xxy get() = Vector3f(this.x, this.x, this.y)
val Vector4f.xxz get() = Vector3f(this.x, this.x, this.z)
val Vector4f.xxw get() = Vector3f(this.x, this.x, this.w)
val Vector4f.xyx get() = Vector3f(this.x, this.y, this.x)
val Vector4f.xyy get() = Vector3f(this.x, this.y, this.y)
var Vector4f.xyz get() = Vector3f(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4f.xyw get() = Vector3f(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4f.xzx get() = Vector3f(this.x, this.z, this.x)
var Vector4f.xzy get() = Vector3f(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4f.xzz get() = Vector3f(this.x, this.z, this.z)
var Vector4f.xzw get() = Vector3f(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
var Vector4f.xwy get() = Vector3f(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4f.xwz get() = Vector3f(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4f.xww get() = Vector3f(this.x, this.w, this.w)
val Vector4f.yxx get() = Vector3f(this.y, this.x, this.x)
val Vector4f.yxy get() = Vector3f(this.y, this.x, this.y)
var Vector4f.yxz get() = Vector3f(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
val Vector4f.xwx get() = Vector3f(this.x, this.w, this.x)
var Vector4f.yxw get() = Vector3f(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4f.yyx get() = Vector3f(this.y, this.y, this.x)
val Vector4f.yyy get() = Vector3f(this.y, this.y, this.y)
val Vector4f.yyz get() = Vector3f(this.y, this.y, this.z)
val Vector4f.yyw get() = Vector3f(this.y, this.y, this.w)
var Vector4f.yzx get() = Vector3f(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4f.yzy get() = Vector3f(this.y, this.z, this.y)
val Vector4f.yzz get() = Vector3f(this.y, this.z, this.z)
var Vector4f.yzw get() = Vector3f(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4f.ywx get() = Vector3f(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4f.ywy get() = Vector3f(this.y, this.w, this.y)
var Vector4f.ywz get() = Vector3f(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4f.yww get() = Vector3f(this.y, this.w, this.w)
val Vector4f.zxx get() = Vector3f(this.z, this.x, this.x)
var Vector4f.zxy get() = Vector3f(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4f.zxz get() = Vector3f(this.z, this.x, this.z)
var Vector4f.zxw get() = Vector3f(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4f.zyx get() = Vector3f(this.z, this.y, this.x); set(v) { z = v.x; y = v.y; x = v.z }
val Vector4f.zyy get() = Vector3f(this.z, this.y, this.y)
val Vector4f.zyz get() = Vector3f(this.z, this.y, this.z)
var Vector4f.zyw get() = Vector3f(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4f.zzx get() = Vector3f(this.z, this.z, this.x)
val Vector4f.zzy get() = Vector3f(this.z, this.z, this.y)
val Vector4f.zzz get() = Vector3f(this.z, this.z, this.z)
val Vector4f.zzw get() = Vector3f(this.z, this.z, this.w)
var Vector4f.zwx get() = Vector3f(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4f.zwy get() = Vector3f(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4f.zwz get() = Vector3f(this.z, this.w, this.z)
val Vector4f.zww get() = Vector3f(this.z, this.w, this.w)
val Vector4f.wxx get() = Vector3f(this.w, this.x, this.x)
var Vector4f.wxy get() = Vector3f(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4f.wxz get() = Vector3f(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4f.wxw get() = Vector3f(this.w, this.x, this.w)
var Vector4f.wyx get() = Vector3f(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4f.wyy get() = Vector3f(this.w, this.y, this.y)
var Vector4f.wyz get() = Vector3f(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4f.wyw get() = Vector3f(this.w, this.y, this.w)
var Vector4f.wzx get() = Vector3f(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4f.wzy get() = Vector3f(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4f.wzz get() = Vector3f(this.w, this.z, this.z)
val Vector4f.wzw get() = Vector3f(this.w, this.z, this.w)
val Vector4f.wwx get() = Vector3f(this.w, this.w, this.x)
val Vector4f.wwy get() = Vector3f(this.w, this.w, this.y)
val Vector4f.wwz get() = Vector3f(this.w, this.w, this.z)
val Vector4f.www get() = Vector3f(this.w, this.w, this.w)

val Vector4f.wwww get() = Vector4f(this.w, this.w, this.w, this.w)
val Vector4f.wwwx get() = Vector4f(this.w, this.w, this.w, this.x)
val Vector4f.wwwy get() = Vector4f(this.w, this.w, this.w, this.y)
val Vector4f.wwwz get() = Vector4f(this.w, this.w, this.w, this.z)
val Vector4f.wwxw get() = Vector4f(this.w, this.w, this.x, this.w)
val Vector4f.wwxx get() = Vector4f(this.w, this.w, this.x, this.x)
val Vector4f.wwxy get() = Vector4f(this.w, this.w, this.x, this.y)
val Vector4f.wwxz get() = Vector4f(this.w, this.w, this.x, this.z)
val Vector4f.wwyw get() = Vector4f(this.w, this.w, this.y, this.w)
val Vector4f.wwyx get() = Vector4f(this.w, this.w, this.y, this.x)
val Vector4f.wwyy get() = Vector4f(this.w, this.w, this.y, this.y)
val Vector4f.wwyz get() = Vector4f(this.w, this.w, this.y, this.z)
val Vector4f.wwzw get() = Vector4f(this.w, this.w, this.z, this.w)
val Vector4f.wwzx get() = Vector4f(this.w, this.w, this.z, this.x)
val Vector4f.wwzy get() = Vector4f(this.w, this.w, this.z, this.y)
val Vector4f.wwzz get() = Vector4f(this.w, this.w, this.z, this.z)
val Vector4f.wxww get() = Vector4f(this.w, this.x, this.w, this.w)
val Vector4f.wxwx get() = Vector4f(this.w, this.x, this.w, this.x)
val Vector4f.wxwy get() = Vector4f(this.w, this.x, this.w, this.y)
val Vector4f.wxwz get() = Vector4f(this.w, this.x, this.w, this.z)
var Vector4f.wxxw get() = Vector4f(this.w, this.x, this.x, this.w); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.w = v.w }
val Vector4f.wxxx get() = Vector4f(this.w, this.x, this.x, this.x)
var Vector4f.wxxy get() = Vector4f(this.w, this.x, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.y = v.w }
var Vector4f.wxxz get() = Vector4f(this.w, this.x, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.z = v.w }
var Vector4f.wxyw get() = Vector4f(this.w, this.x, this.y, this.w); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
var Vector4f.wxyx get() = Vector4f(this.w, this.x, this.y, this.x); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.x = v.w }
var Vector4f.wxyy get() = Vector4f(this.w, this.x, this.y, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.y = v.w }
var Vector4f.wxyz get() = Vector4f(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
var Vector4f.wxzw get() = Vector4f(this.w, this.x, this.z, this.w); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
var Vector4f.wxzx get() = Vector4f(this.w, this.x, this.z, this.x); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.x = v.w }
var Vector4f.wxzy get() = Vector4f(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
var Vector4f.wxzz get() = Vector4f(this.w, this.x, this.z, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.z = v.w }
val Vector4f.wyww get() = Vector4f(this.w, this.y, this.w, this.w)
val Vector4f.wywx get() = Vector4f(this.w, this.y, this.w, this.x)
val Vector4f.wywy get() = Vector4f(this.w, this.y, this.w, this.y)
val Vector4f.wywz get() = Vector4f(this.w, this.y, this.w, this.z)
var Vector4f.wyxw get() = Vector4f(this.w, this.y, this.x, this.w); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
var Vector4f.wyxx get() = Vector4f(this.w, this.y, this.x, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.x = v.w }
var Vector4f.wyxy get() = Vector4f(this.w, this.y, this.x, this.y); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.y = v.w }
var Vector4f.wyxz get() = Vector4f(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4f.wyyw get() = Vector4f(this.w, this.y, this.y, this.w)
var Vector4f.wyyx get() = Vector4f(this.w, this.y, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.y = v.z; this.x = v.w }
val Vector4f.wyyy get() = Vector4f(this.w, this.y, this.y, this.y)
val Vector4f.wyyz get() = Vector4f(this.w, this.y, this.y, this.z)
var Vector4f.wyzw get() = Vector4f(this.w, this.y, this.z, this.w); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
var Vector4f.wyzx get() = Vector4f(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
var Vector4f.wyzy get() = Vector4f(this.w, this.y, this.z, this.y); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.y = v.w }
var Vector4f.wyzz get() = Vector4f(this.w, this.y, this.z, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.z = v.w }
val Vector4f.wzww get() = Vector4f(this.w, this.z, this.w, this.w)
val Vector4f.wzwx get() = Vector4f(this.w, this.z, this.w, this.x)
val Vector4f.wzwy get() = Vector4f(this.w, this.z, this.w, this.y)
val Vector4f.wzwz get() = Vector4f(this.w, this.z, this.w, this.z)
var Vector4f.wzxw get() = Vector4f(this.w, this.z, this.x, this.w); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
var Vector4f.wzxx get() = Vector4f(this.w, this.z, this.x, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.x = v.w }
var Vector4f.wzxy get() = Vector4f(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
var Vector4f.wzxz get() = Vector4f(this.w, this.z, this.x, this.z); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.z = v.w }
var Vector4f.wzyw get() = Vector4f(this.w, this.z, this.y, this.w); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
var Vector4f.wzyx get() = Vector4f(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
var Vector4f.wzyy get() = Vector4f(this.w, this.z, this.y, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.y = v.w }
var Vector4f.wzyz get() = Vector4f(this.w, this.z, this.y, this.z); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.z = v.w }
val Vector4f.wzzw get() = Vector4f(this.w, this.z, this.z, this.w)
val Vector4f.wzzx get() = Vector4f(this.w, this.z, this.z, this.x)
val Vector4f.wzzy get() = Vector4f(this.w, this.z, this.z, this.y)
val Vector4f.wzzz get() = Vector4f(this.w, this.z, this.z, this.z)
val Vector4f.xwww get() = Vector4f(this.x, this.w, this.w, this.w)
val Vector4f.xwwx get() = Vector4f(this.x, this.w, this.w, this.x)
val Vector4f.xwwy get() = Vector4f(this.x, this.w, this.w, this.y)
val Vector4f.xwwz get() = Vector4f(this.x, this.w, this.w, this.z)
val Vector4f.xwxw get() = Vector4f(this.x, this.w, this.x, this.w)
val Vector4f.xwxx get() = Vector4f(this.x, this.w, this.x, this.x)
val Vector4f.xwxy get() = Vector4f(this.x, this.w, this.x, this.y)
val Vector4f.xwxz get() = Vector4f(this.x, this.w, this.x, this.z)
var Vector4f.xwyw get() = Vector4f(this.x, this.w, this.y, this.w); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.w = v.w }
val Vector4f.xwyx get() = Vector4f(this.x, this.w, this.y, this.x)
val Vector4f.xwyy get() = Vector4f(this.x, this.w, this.y, this.y)
var Vector4f.xwyz get() = Vector4f(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
var Vector4f.xwzw get() = Vector4f(this.x, this.w, this.z, this.w); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.w = v.w }
val Vector4f.xwzx get() = Vector4f(this.x, this.w, this.z, this.x)
var Vector4f.xwzy get() = Vector4f(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4f.xwzz get() = Vector4f(this.x, this.w, this.z, this.z)
val Vector4f.xxww get() = Vector4f(this.x, this.x, this.w, this.w)
val Vector4f.xxwx get() = Vector4f(this.x, this.x, this.w, this.x)
val Vector4f.xxwy get() = Vector4f(this.x, this.x, this.w, this.y)
val Vector4f.xxwz get() = Vector4f(this.x, this.x, this.w, this.z)
val Vector4f.xxxw get() = Vector4f(this.x, this.x, this.x, this.w)
val Vector4f.xxxx get() = Vector4f(this.x, this.x, this.x, this.x)
val Vector4f.xxxy get() = Vector4f(this.x, this.x, this.x, this.y)
val Vector4f.xxxz get() = Vector4f(this.x, this.x, this.x, this.z)
val Vector4f.xxyw get() = Vector4f(this.x, this.x, this.y, this.w)
val Vector4f.xxyx get() = Vector4f(this.x, this.x, this.y, this.x)
val Vector4f.xxyy get() = Vector4f(this.x, this.x, this.y, this.y)
val Vector4f.xxyz get() = Vector4f(this.x, this.x, this.y, this.z)
val Vector4f.xxzw get() = Vector4f(this.x, this.x, this.z, this.w)
val Vector4f.xxzx get() = Vector4f(this.x, this.x, this.z, this.x)
val Vector4f.xxzy get() = Vector4f(this.x, this.x, this.z, this.y)
val Vector4f.xxzz get() = Vector4f(this.x, this.x, this.z, this.z)
val Vector4f.xyww get() = Vector4f(this.x, this.y, this.w, this.w)
val Vector4f.xywx get() = Vector4f(this.x, this.y, this.w, this.x)
val Vector4f.xywy get() = Vector4f(this.x, this.y, this.w, this.y)
var Vector4f.xywz get() = Vector4f(this.x, this.y, this.w, this.z); set(v) { x = v.x; y = v.y; w = v.z; z = v.w }
val Vector4f.xyxw get() = Vector4f(this.x, this.y, this.x, this.w)
val Vector4f.xyxx get() = Vector4f(this.x, this.y, this.x, this.x)
val Vector4f.xyxy get() = Vector4f(this.x, this.y, this.x, this.y)
val Vector4f.xyxz get() = Vector4f(this.x, this.y, this.x, this.z)
val Vector4f.xyyw get() = Vector4f(this.x, this.y, this.y, this.w)
val Vector4f.xyyx get() = Vector4f(this.x, this.y, this.y, this.x)
val Vector4f.xyyy get() = Vector4f(this.x, this.y, this.y, this.y)
val Vector4f.xyyz get() = Vector4f(this.x, this.y, this.y, this.z)
var Vector4f.xyzw get() = Vector4f(this.x, this.y, this.z, this.w); set(v) { x = v.x; y = v.y; z = v.z; w = v.w }
val Vector4f.xyzx get() = Vector4f(this.x, this.y, this.z, this.x)
val Vector4f.xyzy get() = Vector4f(this.x, this.y, this.z, this.y)
val Vector4f.xyzz get() = Vector4f(this.x, this.y, this.z, this.z)
var Vector4f.xzww get() = Vector4f(this.x, this.z, this.w, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.w = v.w }
val Vector4f.xzwx get() = Vector4f(this.x, this.z, this.w, this.x)
val Vector4f.xzwy get() = Vector4f(this.x, this.z, this.w, this.y)
val Vector4f.xzwz get() = Vector4f(this.x, this.z, this.w, this.z)
val Vector4f.xzxw get() = Vector4f(this.x, this.z, this.x, this.w)
val Vector4f.xzxx get() = Vector4f(this.x, this.z, this.x, this.x)
val Vector4f.xzxy get() = Vector4f(this.x, this.z, this.x, this.y)
val Vector4f.xzxz get() = Vector4f(this.x, this.z, this.x, this.z)
val Vector4f.xzyw get() = Vector4f(this.x, this.z, this.y, this.w)
val Vector4f.xzyx get() = Vector4f(this.x, this.z, this.y, this.x)
val Vector4f.xzyy get() = Vector4f(this.x, this.z, this.y, this.y)
var Vector4f.xzyz get() = Vector4f(this.x, this.z, this.y, this.z); set(v) { x = v.x; z = v.y; y = v.z; z = v.w }
val Vector4f.xzzw get() = Vector4f(this.x, this.z, this.z, this.w)
val Vector4f.xzzx get() = Vector4f(this.x, this.z, this.z, this.x)
val Vector4f.xzzy get() = Vector4f(this.x, this.z, this.z, this.y)
val Vector4f.xzzz get() = Vector4f(this.x, this.z, this.z, this.z)
val Vector4f.ywww get() = Vector4f(this.y, this.w, this.w, this.w)
val Vector4f.ywwx get() = Vector4f(this.y, this.w, this.w, this.x)
val Vector4f.ywwy get() = Vector4f(this.y, this.w, this.w, this.y)
val Vector4f.ywwz get() = Vector4f(this.y, this.w, this.w, this.z)
val Vector4f.ywxw get() = Vector4f(this.y, this.w, this.x, this.w)
val Vector4f.ywxx get() = Vector4f(this.y, this.w, this.x, this.x)
var Vector4f.ywxy get() = Vector4f(this.y, this.w, this.x, this.y); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
var Vector4f.ywxz get() = Vector4f(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4f.ywyw get() = Vector4f(this.y, this.w, this.y, this.w)
var Vector4f.ywyx get() = Vector4f(this.y, this.w, this.y, this.x); set(v) { this.y = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4f.ywyy get() = Vector4f(this.y, this.w, this.y, this.y)
val Vector4f.ywyz get() = Vector4f(this.y, this.w, this.y, this.z)
var Vector4f.ywzw get() = Vector4f(this.y, this.w, this.z, this.w); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.w = v.w }
var Vector4f.ywzx get() = Vector4f(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
var Vector4f.ywzy get() = Vector4f(this.y, this.w, this.z, this.y); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4f.ywzz get() = Vector4f(this.y, this.w, this.z, this.z)
val Vector4f.yxww get() = Vector4f(this.y, this.x, this.w, this.w)
val Vector4f.yxwx get() = Vector4f(this.y, this.x, this.w, this.x)
var Vector4f.yxwy get() = Vector4f(this.y, this.x, this.w, this.y); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
var Vector4f.yxwz get() = Vector4f(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4f.yxxw get() = Vector4f(this.y, this.x, this.x, this.w)
val Vector4f.yxxx get() = Vector4f(this.y, this.x, this.x, this.x)
val Vector4f.yxxy get() = Vector4f(this.y, this.x, this.x, this.y)
val Vector4f.yxxz get() = Vector4f(this.y, this.x, this.x, this.z)
var Vector4f.yxyw get() = Vector4f(this.y, this.x, this.y, this.w); set(v) { this.y = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4f.yxyx get() = Vector4f(this.y, this.x, this.y, this.x)
val Vector4f.yxyy get() = Vector4f(this.y, this.x, this.y, this.y)
var Vector4f.yxyz get() = Vector4f(this.y, this.x, this.y, this.z); set(v) { this.y = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
var Vector4f.yxzw get() = Vector4f(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4f.yxzx get() = Vector4f(this.y, this.x, this.z, this.x)
var Vector4f.yxzy get() = Vector4f(this.y, this.x, this.z, this.y); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4f.yxzz get() = Vector4f(this.y, this.x, this.z, this.z)
val Vector4f.yyww get() = Vector4f(this.y, this.y, this.w, this.w)
val Vector4f.yywx get() = Vector4f(this.y, this.y, this.w, this.x)
val Vector4f.yywy get() = Vector4f(this.y, this.y, this.w, this.y)
val Vector4f.yywz get() = Vector4f(this.y, this.y, this.w, this.z)
val Vector4f.yyxw get() = Vector4f(this.y, this.y, this.x, this.w)
val Vector4f.yyxx get() = Vector4f(this.y, this.y, this.x, this.x)
val Vector4f.yyxy get() = Vector4f(this.y, this.y, this.x, this.y)
val Vector4f.yyxz get() = Vector4f(this.y, this.y, this.x, this.z)
val Vector4f.yyyw get() = Vector4f(this.y, this.y, this.y, this.w)
val Vector4f.yyyx get() = Vector4f(this.y, this.y, this.y, this.x)
val Vector4f.yyyy get() = Vector4f(this.y, this.y, this.y, this.y)
val Vector4f.yyyz get() = Vector4f(this.y, this.y, this.y, this.z)
val Vector4f.yyzw get() = Vector4f(this.y, this.y, this.z, this.w)
val Vector4f.yyzx get() = Vector4f(this.y, this.y, this.z, this.x)
val Vector4f.yyzy get() = Vector4f(this.y, this.y, this.z, this.y)
val Vector4f.yyzz get() = Vector4f(this.y, this.y, this.z, this.z)
var Vector4f.yzww get() = Vector4f(this.y, this.z, this.w, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.w = v.w }
var Vector4f.yzwx get() = Vector4f(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
var Vector4f.yzwy get() = Vector4f(this.y, this.z, this.w, this.y); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4f.yzwz get() = Vector4f(this.y, this.z, this.w, this.z)
var Vector4f.yzxw get() = Vector4f(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4f.yzxx get() = Vector4f(this.y, this.z, this.x, this.x)
var Vector4f.yzxy get() = Vector4f(this.y, this.z, this.x, this.y); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4f.yzxz get() = Vector4f(this.y, this.z, this.x, this.z)
val Vector4f.yzyw get() = Vector4f(this.y, this.z, this.y, this.w)
var Vector4f.yzyx get() = Vector4f(this.y, this.z, this.y, this.x); set(v) { this.y = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4f.yzyy get() = Vector4f(this.y, this.z, this.y, this.y)
val Vector4f.yzyz get() = Vector4f(this.y, this.z, this.y, this.z)
val Vector4f.yzzw get() = Vector4f(this.y, this.z, this.z, this.w)
val Vector4f.yzzx get() = Vector4f(this.y, this.z, this.z, this.x)
val Vector4f.yzzy get() = Vector4f(this.y, this.z, this.z, this.y)
val Vector4f.yzzz get() = Vector4f(this.y, this.z, this.z, this.z)
val Vector4f.zwww get() = Vector4f(this.z, this.w, this.w, this.w)
val Vector4f.zwwx get() = Vector4f(this.z, this.w, this.w, this.x)
val Vector4f.zwwy get() = Vector4f(this.z, this.w, this.w, this.y)
val Vector4f.zwwz get() = Vector4f(this.z, this.w, this.w, this.z)
var Vector4f.zwxw get() = Vector4f(this.z, this.w, this.x, this.w); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.w = v.w }
var Vector4f.zwxx get() = Vector4f(this.z, this.w, this.x, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.x = v.w }
var Vector4f.zwxy get() = Vector4f(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
var Vector4f.zwxz get() = Vector4f(this.z, this.w, this.x, this.z); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
var Vector4f.zwyw get() = Vector4f(this.z, this.w, this.y, this.w); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.w = v.w }
var Vector4f.zwyx get() = Vector4f(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
var Vector4f.zwyy get() = Vector4f(this.z, this.w, this.y, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.y = v.w }
var Vector4f.zwyz get() = Vector4f(this.z, this.w, this.y, this.z); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4f.zwzw get() = Vector4f(this.z, this.w, this.z, this.w)
val Vector4f.zwzx get() = Vector4f(this.z, this.w, this.z, this.x)
val Vector4f.zwzy get() = Vector4f(this.z, this.w, this.z, this.y)
val Vector4f.zwzz get() = Vector4f(this.z, this.w, this.z, this.z)
val Vector4f.zxww get() = Vector4f(this.z, this.x, this.w, this.w)
val Vector4f.zxwx get() = Vector4f(this.z, this.x, this.w, this.x)
var Vector4f.zxwy get() = Vector4f(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
var Vector4f.zxwz get() = Vector4f(this.z, this.x, this.w, this.z); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4f.zxxw get() = Vector4f(this.z, this.x, this.x, this.w)
val Vector4f.zxxx get() = Vector4f(this.z, this.x, this.x, this.x)
val Vector4f.zxxy get() = Vector4f(this.z, this.x, this.x, this.y)
val Vector4f.zxxz get() = Vector4f(this.z, this.x, this.x, this.z)
var Vector4f.zxyw get() = Vector4f(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4f.zxyx get() = Vector4f(this.z, this.x, this.y, this.x)
var Vector4f.zxyy get() = Vector4f(this.z, this.x, this.y, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.y = v.w }
var Vector4f.zxyz get() = Vector4f(this.z, this.x, this.y, this.z); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4f.zxzw get() = Vector4f(this.z, this.x, this.z, this.w)
val Vector4f.zxzx get() = Vector4f(this.z, this.x, this.z, this.x)
val Vector4f.zxzy get() = Vector4f(this.z, this.x, this.z, this.y)
val Vector4f.zxzz get() = Vector4f(this.z, this.x, this.z, this.z)
val Vector4f.zyww get() = Vector4f(this.z, this.y, this.w, this.w)
var Vector4f.zywx get() = Vector4f(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
var Vector4f.zywy get() = Vector4f(this.z, this.y, this.w, this.y); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.y = v.w }
var Vector4f.zywz get() = Vector4f(this.z, this.y, this.w, this.z); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
var Vector4f.zyxw get() = Vector4f(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
var Vector4f.zyxx get() = Vector4f(this.z, this.y, this.x, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.x = v.w }
val Vector4f.zyxy get() = Vector4f(this.z, this.y, this.x, this.y)
val Vector4f.zyxz get() = Vector4f(this.z, this.y, this.x, this.z)
val Vector4f.zyyw get() = Vector4f(this.z, this.y, this.y, this.w)
val Vector4f.zyyx get() = Vector4f(this.z, this.y, this.y, this.x)
val Vector4f.zyyy get() = Vector4f(this.z, this.y, this.y, this.y)
val Vector4f.zyyz get() = Vector4f(this.z, this.y, this.y, this.z)
val Vector4f.zyzw get() = Vector4f(this.z, this.y, this.z, this.w)
val Vector4f.zyzx get() = Vector4f(this.z, this.y, this.z, this.x)
val Vector4f.zyzy get() = Vector4f(this.z, this.y, this.z, this.y)
val Vector4f.zyzz get() = Vector4f(this.z, this.y, this.z, this.z)
val Vector4f.zzww get() = Vector4f(this.z, this.z, this.w, this.w)
val Vector4f.zzwx get() = Vector4f(this.z, this.z, this.w, this.x)
val Vector4f.zzwy get() = Vector4f(this.z, this.z, this.w, this.y)
val Vector4f.zzwz get() = Vector4f(this.z, this.z, this.w, this.z)
val Vector4f.zzxw get() = Vector4f(this.z, this.z, this.x, this.w)
val Vector4f.zzxx get() = Vector4f(this.z, this.z, this.x, this.x)
val Vector4f.zzxy get() = Vector4f(this.z, this.z, this.x, this.y)
val Vector4f.zzxz get() = Vector4f(this.z, this.z, this.x, this.z)
val Vector4f.zzyw get() = Vector4f(this.z, this.z, this.y, this.w)
val Vector4f.zzyx get() = Vector4f(this.z, this.z, this.y, this.x)
val Vector4f.zzyy get() = Vector4f(this.z, this.z, this.y, this.y)
val Vector4f.zzyz get() = Vector4f(this.z, this.z, this.y, this.z)
val Vector4f.zzzw get() = Vector4f(this.z, this.z, this.z, this.w)
val Vector4f.zzzx get() = Vector4f(this.z, this.z, this.z, this.x)
val Vector4f.zzzy get() = Vector4f(this.z, this.z, this.z, this.y)
val Vector4f.zzzz get() = Vector4f(this.z, this.z, this.z, this.z)

// ===================================== RGBA =====================================
var Vector4f.r: Float get() = this.x; set(it) { this.x = it }
var Vector4f.g: Float get() = this.y; set(it) { this.y = it }
var Vector4f.b: Float get() = this.z; set(it) { this.z = it }
var Vector4f.a: Float get() = this.w; set(it) { this.w = it }

val Vector4f.rr get() = Vector2f(this.x, this.x)
var Vector4f.rg get() = Vector2f(this.x, this.y); set(v) { this.x = v.x; this.y = v.y }
var Vector4f.rb get() = Vector2f(this.x, this.z); set(v) { this.x = v.x; this.z = v.y }
var Vector4f.ra get() = Vector2f(this.x, this.w); set(v) { this.x = v.x; this.w = v.y }
var Vector4f.gr get() = Vector2f(this.y, this.x); set(v) { this.y = v.x; this.x = v.y }
val Vector4f.gg get() = Vector2f(this.y, this.y)
var Vector4f.gb get() = Vector2f(this.y, this.z); set(v) { this.y = v.x; this.z = v.y }
var Vector4f.ga get() = Vector2f(this.y, this.w); set(v) { this.y = v.x; this.w = v.y }
var Vector4f.br get() = Vector2f(this.z, this.x); set(v) { this.z = v.x; this.x = v.y }
var Vector4f.bg get() = Vector2f(this.z, this.y); set(v) { this.z = v.x; this.y = v.y }
val Vector4f.bb get() = Vector2f(this.z, this.z)
var Vector4f.ba get() = Vector2f(this.z, this.w); set(v) { this.z = v.x; this.w = v.y }
var Vector4f.ar get() = Vector2f(this.w, this.x); set(v) { this.w = v.x; this.x = v.y }
var Vector4f.ag get() = Vector2f(this.w, this.y); set(v) { this.w = v.x; this.y = v.y }
var Vector4f.ab get() = Vector2f(this.w, this.z); set(v) { this.w = v.x; this.z = v.y }
val Vector4f.aa get() = Vector2f(this.w, this.w)

val Vector4f.rrr get() = Vector3f(this.x, this.x, this.x)
val Vector4f.rrg get() = Vector3f(this.x, this.x, this.y)
val Vector4f.rrb get() = Vector3f(this.x, this.x, this.z)
val Vector4f.rra get() = Vector3f(this.x, this.x, this.w)
val Vector4f.rgr get() = Vector3f(this.x, this.y, this.x)
val Vector4f.rgg get() = Vector3f(this.x, this.y, this.y)
var Vector4f.rgb get() = Vector3f(this.x, this.y, this.z); set(v) { this.x = v.x; this.y = v.y; this.z = v.z }
var Vector4f.rga get() = Vector3f(this.x, this.y, this.w); set(v) { this.x = v.x; this.y = v.y; this.w = v.z }
val Vector4f.rbr get() = Vector3f(this.x, this.z, this.x)
var Vector4f.rbg get() = Vector3f(this.x, this.z, this.y); set(v) { this.x = v.x; this.z = v.y; this.y = v.z }
val Vector4f.rbb get() = Vector3f(this.x, this.z, this.z)
var Vector4f.rba get() = Vector3f(this.x, this.z, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z }
var Vector4f.rag get() = Vector3f(this.x, this.w, this.y); set(v) { this.x = v.x; this.w = v.y; this.y = v.z }
var Vector4f.rab get() = Vector3f(this.x, this.w, this.z); set(v) { this.x = v.x; this.w = v.y; this.z = v.z }
val Vector4f.raa get() = Vector3f(this.x, this.w, this.w)
val Vector4f.grr get() = Vector3f(this.y, this.x, this.x)
val Vector4f.grg get() = Vector3f(this.y, this.x, this.y)
var Vector4f.grb get() = Vector3f(this.y, this.x, this.z); set(v) { this.y = v.x; this.x = v.y; this.z = v.z }
val Vector4f.rar get() = Vector3f(this.x, this.w, this.x)
var Vector4f.gra get() = Vector3f(this.y, this.x, this.w); set(v) { this.y = v.x; this.x = v.y; this.w = v.z }
val Vector4f.ggr get() = Vector3f(this.y, this.y, this.x)
val Vector4f.ggg get() = Vector3f(this.y, this.y, this.y)
val Vector4f.ggb get() = Vector3f(this.y, this.y, this.z)
val Vector4f.gga get() = Vector3f(this.y, this.y, this.w)
var Vector4f.gbr get() = Vector3f(this.y, this.z, this.x); set(v) { this.y = v.x; this.z = v.y; this.x = v.z }
val Vector4f.gbg get() = Vector3f(this.y, this.z, this.y)
val Vector4f.gbb get() = Vector3f(this.y, this.z, this.z)
var Vector4f.gba get() = Vector3f(this.y, this.z, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z }
var Vector4f.gar get() = Vector3f(this.y, this.w, this.x); set(v) { this.y = v.x; this.w = v.y; this.x = v.z }
val Vector4f.gag get() = Vector3f(this.y, this.w, this.y)
var Vector4f.gab get() = Vector3f(this.y, this.w, this.z); set(v) { this.y = v.x; this.w = v.y; this.z = v.z }
val Vector4f.gaa get() = Vector3f(this.y, this.w, this.w)
val Vector4f.brr get() = Vector3f(this.z, this.x, this.x)
var Vector4f.brg get() = Vector3f(this.z, this.x, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z }
val Vector4f.brb get() = Vector3f(this.z, this.x, this.z)
var Vector4f.bra get() = Vector3f(this.z, this.x, this.w); set(v) { this.z = v.x; this.x = v.y; this.w = v.z }
var Vector4f.bgr get() = Vector3f(this.z, this.y, this.x); set(v) { z = v.x; y = v.y; x = v.z }
val Vector4f.bgg get() = Vector3f(this.z, this.y, this.y)
val Vector4f.bgb get() = Vector3f(this.z, this.y, this.z)
var Vector4f.bga get() = Vector3f(this.z, this.y, this.w); set(v) { this.z = v.x; this.y = v.y; this.w = v.z }
val Vector4f.bbr get() = Vector3f(this.z, this.z, this.x)
val Vector4f.bbg get() = Vector3f(this.z, this.z, this.y)
val Vector4f.bbb get() = Vector3f(this.z, this.z, this.z)
val Vector4f.bba get() = Vector3f(this.z, this.z, this.w)
var Vector4f.bar get() = Vector3f(this.z, this.w, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z }
var Vector4f.bag get() = Vector3f(this.z, this.w, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z }
val Vector4f.bab get() = Vector3f(this.z, this.w, this.z)
val Vector4f.baa get() = Vector3f(this.z, this.w, this.w)
val Vector4f.arr get() = Vector3f(this.w, this.x, this.x)
var Vector4f.arg get() = Vector3f(this.w, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z }
var Vector4f.arb get() = Vector3f(this.w, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z }
val Vector4f.ara get() = Vector3f(this.w, this.x, this.w)
var Vector4f.agr get() = Vector3f(this.w, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z }
val Vector4f.agg get() = Vector3f(this.w, this.y, this.y)
var Vector4f.agb get() = Vector3f(this.w, this.y, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z }
val Vector4f.aga get() = Vector3f(this.w, this.y, this.w)
var Vector4f.abr get() = Vector3f(this.w, this.z, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z }
var Vector4f.abg get() = Vector3f(this.w, this.z, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z }
val Vector4f.abb get() = Vector3f(this.w, this.z, this.z)
val Vector4f.aba get() = Vector3f(this.w, this.z, this.w)
val Vector4f.aar get() = Vector3f(this.w, this.w, this.x)
val Vector4f.aag get() = Vector3f(this.w, this.w, this.y)
val Vector4f.aab get() = Vector3f(this.w, this.w, this.z)
val Vector4f.aaa get() = Vector3f(this.w, this.w, this.w)

val Vector4f.aaaa get() = Vector4f(this.w, this.w, this.w, this.w)
val Vector4f.aaar get() = Vector4f(this.w, this.w, this.w, this.x)
val Vector4f.aaag get() = Vector4f(this.w, this.w, this.w, this.y)
val Vector4f.aaab get() = Vector4f(this.w, this.w, this.w, this.z)
val Vector4f.aara get() = Vector4f(this.w, this.w, this.x, this.w)
val Vector4f.aarr get() = Vector4f(this.w, this.w, this.x, this.x)
val Vector4f.aarg get() = Vector4f(this.w, this.w, this.x, this.y)
val Vector4f.aarb get() = Vector4f(this.w, this.w, this.x, this.z)
val Vector4f.aaga get() = Vector4f(this.w, this.w, this.y, this.w)
val Vector4f.aagr get() = Vector4f(this.w, this.w, this.y, this.x)
val Vector4f.aagg get() = Vector4f(this.w, this.w, this.y, this.y)
val Vector4f.aagb get() = Vector4f(this.w, this.w, this.y, this.z)
val Vector4f.aaba get() = Vector4f(this.w, this.w, this.z, this.w)
val Vector4f.aabr get() = Vector4f(this.w, this.w, this.z, this.x)
val Vector4f.aabg get() = Vector4f(this.w, this.w, this.z, this.y)
val Vector4f.aabb get() = Vector4f(this.w, this.w, this.z, this.z)
val Vector4f.araa get() = Vector4f(this.w, this.x, this.w, this.w)
val Vector4f.arar get() = Vector4f(this.w, this.x, this.w, this.x)
val Vector4f.arag get() = Vector4f(this.w, this.x, this.w, this.y)
val Vector4f.arab get() = Vector4f(this.w, this.x, this.w, this.z)
var Vector4f.arra get() = Vector4f(this.w, this.x, this.x, this.w); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.w = v.w }
val Vector4f.arrr get() = Vector4f(this.w, this.x, this.x, this.x)
var Vector4f.arrg get() = Vector4f(this.w, this.x, this.x, this.y); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.y = v.w }
var Vector4f.arrb get() = Vector4f(this.w, this.x, this.x, this.z); set(v) { this.w = v.x; this.x = v.y; this.x = v.z; this.z = v.w }
var Vector4f.arga get() = Vector4f(this.w, this.x, this.y, this.w); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
var Vector4f.argr get() = Vector4f(this.w, this.x, this.y, this.x); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.x = v.w }
var Vector4f.argg get() = Vector4f(this.w, this.x, this.y, this.y); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.y = v.w }
var Vector4f.argb get() = Vector4f(this.w, this.x, this.y, this.z); set(v) { this.w = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
var Vector4f.arba get() = Vector4f(this.w, this.x, this.z, this.w); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
var Vector4f.arbr get() = Vector4f(this.w, this.x, this.z, this.x); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.x = v.w }
var Vector4f.arbg get() = Vector4f(this.w, this.x, this.z, this.y); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
var Vector4f.arbb get() = Vector4f(this.w, this.x, this.z, this.z); set(v) { this.w = v.x; this.x = v.y; this.z = v.z; this.z = v.w }
val Vector4f.agaa get() = Vector4f(this.w, this.y, this.w, this.w)
val Vector4f.agar get() = Vector4f(this.w, this.y, this.w, this.x)
val Vector4f.agag get() = Vector4f(this.w, this.y, this.w, this.y)
val Vector4f.agab get() = Vector4f(this.w, this.y, this.w, this.z)
var Vector4f.agra get() = Vector4f(this.w, this.y, this.x, this.w); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
var Vector4f.agrr get() = Vector4f(this.w, this.y, this.x, this.x); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.x = v.w }
var Vector4f.agrg get() = Vector4f(this.w, this.y, this.x, this.y); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.y = v.w }
var Vector4f.agrb get() = Vector4f(this.w, this.y, this.x, this.z); set(v) { this.w = v.x; this.y = v.y; this.x = v.z; this.z = v.w }
val Vector4f.agga get() = Vector4f(this.w, this.y, this.y, this.w)
var Vector4f.aggr get() = Vector4f(this.w, this.y, this.y, this.x); set(v) { this.w = v.x; this.y = v.y; this.y = v.z; this.x = v.w }
val Vector4f.aggg get() = Vector4f(this.w, this.y, this.y, this.y)
val Vector4f.aggb get() = Vector4f(this.w, this.y, this.y, this.z)
var Vector4f.agba get() = Vector4f(this.w, this.y, this.z, this.w); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.w = v.w }
var Vector4f.agbr get() = Vector4f(this.w, this.y, this.z, this.x); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.x = v.w }
var Vector4f.agbg get() = Vector4f(this.w, this.y, this.z, this.y); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.y = v.w }
var Vector4f.agbb get() = Vector4f(this.w, this.y, this.z, this.z); set(v) { this.w = v.x; this.y = v.y; this.z = v.z; this.z = v.w }
val Vector4f.abaa get() = Vector4f(this.w, this.z, this.w, this.w)
val Vector4f.abar get() = Vector4f(this.w, this.z, this.w, this.x)
val Vector4f.abag get() = Vector4f(this.w, this.z, this.w, this.y)
val Vector4f.abab get() = Vector4f(this.w, this.z, this.w, this.z)
var Vector4f.abra get() = Vector4f(this.w, this.z, this.x, this.w); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
var Vector4f.abrr get() = Vector4f(this.w, this.z, this.x, this.x); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.x = v.w }
var Vector4f.abrg get() = Vector4f(this.w, this.z, this.x, this.y); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
var Vector4f.abrb get() = Vector4f(this.w, this.z, this.x, this.z); set(v) { this.w = v.x; this.z = v.y; this.x = v.z; this.z = v.w }
var Vector4f.abga get() = Vector4f(this.w, this.z, this.y, this.w); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.w = v.w }
var Vector4f.abgr get() = Vector4f(this.w, this.z, this.y, this.x); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
var Vector4f.abgg get() = Vector4f(this.w, this.z, this.y, this.y); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.y = v.w }
var Vector4f.abgb get() = Vector4f(this.w, this.z, this.y, this.z); set(v) { this.w = v.x; this.z = v.y; this.y = v.z; this.z = v.w }
val Vector4f.abba get() = Vector4f(this.w, this.z, this.z, this.w)
val Vector4f.abbr get() = Vector4f(this.w, this.z, this.z, this.x)
val Vector4f.abbg get() = Vector4f(this.w, this.z, this.z, this.y)
val Vector4f.abbb get() = Vector4f(this.w, this.z, this.z, this.z)
val Vector4f.raaa get() = Vector4f(this.x, this.w, this.w, this.w)
val Vector4f.raar get() = Vector4f(this.x, this.w, this.w, this.x)
val Vector4f.raag get() = Vector4f(this.x, this.w, this.w, this.y)
val Vector4f.raab get() = Vector4f(this.x, this.w, this.w, this.z)
val Vector4f.rara get() = Vector4f(this.x, this.w, this.x, this.w)
val Vector4f.rarr get() = Vector4f(this.x, this.w, this.x, this.x)
val Vector4f.rarg get() = Vector4f(this.x, this.w, this.x, this.y)
val Vector4f.rarb get() = Vector4f(this.x, this.w, this.x, this.z)
var Vector4f.raga get() = Vector4f(this.x, this.w, this.y, this.w); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.w = v.w }
val Vector4f.ragr get() = Vector4f(this.x, this.w, this.y, this.x)
val Vector4f.ragg get() = Vector4f(this.x, this.w, this.y, this.y)
var Vector4f.ragb get() = Vector4f(this.x, this.w, this.y, this.z); set(v) { this.x = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
var Vector4f.raba get() = Vector4f(this.x, this.w, this.z, this.w); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.w = v.w }
val Vector4f.rabr get() = Vector4f(this.x, this.w, this.z, this.x)
var Vector4f.rabg get() = Vector4f(this.x, this.w, this.z, this.y); set(v) { this.x = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4f.rabb get() = Vector4f(this.x, this.w, this.z, this.z)
val Vector4f.rraa get() = Vector4f(this.x, this.x, this.w, this.w)
val Vector4f.rrar get() = Vector4f(this.x, this.x, this.w, this.x)
val Vector4f.rrag get() = Vector4f(this.x, this.x, this.w, this.y)
val Vector4f.rrab get() = Vector4f(this.x, this.x, this.w, this.z)
val Vector4f.rrra get() = Vector4f(this.x, this.x, this.x, this.w)
val Vector4f.rrrr get() = Vector4f(this.x, this.x, this.x, this.x)
val Vector4f.rrrg get() = Vector4f(this.x, this.x, this.x, this.y)
val Vector4f.rrrb get() = Vector4f(this.x, this.x, this.x, this.z)
val Vector4f.rrga get() = Vector4f(this.x, this.x, this.y, this.w)
val Vector4f.rrgr get() = Vector4f(this.x, this.x, this.y, this.x)
val Vector4f.rrgg get() = Vector4f(this.x, this.x, this.y, this.y)
val Vector4f.rrgb get() = Vector4f(this.x, this.x, this.y, this.z)
val Vector4f.rrba get() = Vector4f(this.x, this.x, this.z, this.w)
val Vector4f.rrbr get() = Vector4f(this.x, this.x, this.z, this.x)
val Vector4f.rrbg get() = Vector4f(this.x, this.x, this.z, this.y)
val Vector4f.rrbb get() = Vector4f(this.x, this.x, this.z, this.z)
val Vector4f.rgaa get() = Vector4f(this.x, this.y, this.w, this.w)
val Vector4f.rgar get() = Vector4f(this.x, this.y, this.w, this.x)
val Vector4f.rgag get() = Vector4f(this.x, this.y, this.w, this.y)
var Vector4f.rgab get() = Vector4f(this.x, this.y, this.w, this.z); set(v) { x = v.x; y = v.y; w = v.z; z = v.w }
val Vector4f.rgra get() = Vector4f(this.x, this.y, this.x, this.w)
val Vector4f.rgrr get() = Vector4f(this.x, this.y, this.x, this.x)
val Vector4f.rgrg get() = Vector4f(this.x, this.y, this.x, this.y)
val Vector4f.rgrb get() = Vector4f(this.x, this.y, this.x, this.z)
val Vector4f.rgga get() = Vector4f(this.x, this.y, this.y, this.w)
val Vector4f.rggr get() = Vector4f(this.x, this.y, this.y, this.x)
val Vector4f.rggg get() = Vector4f(this.x, this.y, this.y, this.y)
val Vector4f.rggb get() = Vector4f(this.x, this.y, this.y, this.z)
var Vector4f.rgba get() = Vector4f(this.x, this.y, this.z, this.w); set(v) { x = v.x; y = v.y; z = v.z; w = v.w }
val Vector4f.rgbr get() = Vector4f(this.x, this.y, this.z, this.x)
val Vector4f.rgbg get() = Vector4f(this.x, this.y, this.z, this.y)
val Vector4f.rgbb get() = Vector4f(this.x, this.y, this.z, this.z)
var Vector4f.rbaa get() = Vector4f(this.x, this.z, this.w, this.w); set(v) { this.x = v.x; this.z = v.y; this.w = v.z; this.w = v.w }
val Vector4f.rbar get() = Vector4f(this.x, this.z, this.w, this.x)
val Vector4f.rbag get() = Vector4f(this.x, this.z, this.w, this.y)
val Vector4f.rbab get() = Vector4f(this.x, this.z, this.w, this.z)
val Vector4f.rbra get() = Vector4f(this.x, this.z, this.x, this.w)
val Vector4f.rbrr get() = Vector4f(this.x, this.z, this.x, this.x)
val Vector4f.rbrg get() = Vector4f(this.x, this.z, this.x, this.y)
val Vector4f.rbrb get() = Vector4f(this.x, this.z, this.x, this.z)
val Vector4f.rbga get() = Vector4f(this.x, this.z, this.y, this.w)
val Vector4f.rbgr get() = Vector4f(this.x, this.z, this.y, this.x)
val Vector4f.rbgg get() = Vector4f(this.x, this.z, this.y, this.y)
var Vector4f.rbgb get() = Vector4f(this.x, this.z, this.y, this.z); set(v) { x = v.x; z = v.y; y = v.z; z = v.w }
val Vector4f.rbba get() = Vector4f(this.x, this.z, this.z, this.w)
val Vector4f.rbbr get() = Vector4f(this.x, this.z, this.z, this.x)
val Vector4f.rbbg get() = Vector4f(this.x, this.z, this.z, this.y)
val Vector4f.rbbb get() = Vector4f(this.x, this.z, this.z, this.z)
val Vector4f.gaaa get() = Vector4f(this.y, this.w, this.w, this.w)
val Vector4f.gaar get() = Vector4f(this.y, this.w, this.w, this.x)
val Vector4f.gaag get() = Vector4f(this.y, this.w, this.w, this.y)
val Vector4f.gaab get() = Vector4f(this.y, this.w, this.w, this.z)
val Vector4f.gara get() = Vector4f(this.y, this.w, this.x, this.w)
val Vector4f.garr get() = Vector4f(this.y, this.w, this.x, this.x)
var Vector4f.garg get() = Vector4f(this.y, this.w, this.x, this.y); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
var Vector4f.garb get() = Vector4f(this.y, this.w, this.x, this.z); set(v) { this.y = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
val Vector4f.gaga get() = Vector4f(this.y, this.w, this.y, this.w)
var Vector4f.gagr get() = Vector4f(this.y, this.w, this.y, this.x); set(v) { this.y = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
val Vector4f.gagg get() = Vector4f(this.y, this.w, this.y, this.y)
val Vector4f.gagb get() = Vector4f(this.y, this.w, this.y, this.z)
var Vector4f.gaba get() = Vector4f(this.y, this.w, this.z, this.w); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.w = v.w }
var Vector4f.gabr get() = Vector4f(this.y, this.w, this.z, this.x); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.x = v.w }
var Vector4f.gabg get() = Vector4f(this.y, this.w, this.z, this.y); set(v) { this.y = v.x; this.w = v.y; this.z = v.z; this.y = v.w }
val Vector4f.gabb get() = Vector4f(this.y, this.w, this.z, this.z)
val Vector4f.graa get() = Vector4f(this.y, this.x, this.w, this.w)
val Vector4f.grar get() = Vector4f(this.y, this.x, this.w, this.x)
var Vector4f.grag get() = Vector4f(this.y, this.x, this.w, this.y); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
var Vector4f.grab get() = Vector4f(this.y, this.x, this.w, this.z); set(v) { this.y = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4f.grra get() = Vector4f(this.y, this.x, this.x, this.w)
val Vector4f.grrr get() = Vector4f(this.y, this.x, this.x, this.x)
val Vector4f.grrg get() = Vector4f(this.y, this.x, this.x, this.y)
val Vector4f.grrb get() = Vector4f(this.y, this.x, this.x, this.z)
var Vector4f.grga get() = Vector4f(this.y, this.x, this.y, this.w); set(v) { this.y = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4f.grgr get() = Vector4f(this.y, this.x, this.y, this.x)
val Vector4f.grgg get() = Vector4f(this.y, this.x, this.y, this.y)
var Vector4f.grgb get() = Vector4f(this.y, this.x, this.y, this.z); set(v) { this.y = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
var Vector4f.grba get() = Vector4f(this.y, this.x, this.z, this.w); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.w = v.w }
val Vector4f.grbr get() = Vector4f(this.y, this.x, this.z, this.x)
var Vector4f.grbg get() = Vector4f(this.y, this.x, this.z, this.y); set(v) { this.y = v.x; this.x = v.y; this.z = v.z; this.y = v.w }
val Vector4f.grbb get() = Vector4f(this.y, this.x, this.z, this.z)
val Vector4f.ggaa get() = Vector4f(this.y, this.y, this.w, this.w)
val Vector4f.ggar get() = Vector4f(this.y, this.y, this.w, this.x)
val Vector4f.ggag get() = Vector4f(this.y, this.y, this.w, this.y)
val Vector4f.ggab get() = Vector4f(this.y, this.y, this.w, this.z)
val Vector4f.ggra get() = Vector4f(this.y, this.y, this.x, this.w)
val Vector4f.ggrr get() = Vector4f(this.y, this.y, this.x, this.x)
val Vector4f.ggrg get() = Vector4f(this.y, this.y, this.x, this.y)
val Vector4f.ggrb get() = Vector4f(this.y, this.y, this.x, this.z)
val Vector4f.ggga get() = Vector4f(this.y, this.y, this.y, this.w)
val Vector4f.gggr get() = Vector4f(this.y, this.y, this.y, this.x)
val Vector4f.gggg get() = Vector4f(this.y, this.y, this.y, this.y)
val Vector4f.gggb get() = Vector4f(this.y, this.y, this.y, this.z)
val Vector4f.ggba get() = Vector4f(this.y, this.y, this.z, this.w)
val Vector4f.ggbr get() = Vector4f(this.y, this.y, this.z, this.x)
val Vector4f.ggbg get() = Vector4f(this.y, this.y, this.z, this.y)
val Vector4f.ggbb get() = Vector4f(this.y, this.y, this.z, this.z)
var Vector4f.gbaa get() = Vector4f(this.y, this.z, this.w, this.w); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.w = v.w }
var Vector4f.gbar get() = Vector4f(this.y, this.z, this.w, this.x); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.x = v.w }
var Vector4f.gbag get() = Vector4f(this.y, this.z, this.w, this.y); set(v) { this.y = v.x; this.z = v.y; this.w = v.z; this.y = v.w }
val Vector4f.gbab get() = Vector4f(this.y, this.z, this.w, this.z)
var Vector4f.gbra get() = Vector4f(this.y, this.z, this.x, this.w); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.w = v.w }
val Vector4f.gbrr get() = Vector4f(this.y, this.z, this.x, this.x)
var Vector4f.gbrg get() = Vector4f(this.y, this.z, this.x, this.y); set(v) { this.y = v.x; this.z = v.y; this.x = v.z; this.y = v.w }
val Vector4f.gbrb get() = Vector4f(this.y, this.z, this.x, this.z)
val Vector4f.gbga get() = Vector4f(this.y, this.z, this.y, this.w)
var Vector4f.gbgr get() = Vector4f(this.y, this.z, this.y, this.x); set(v) { this.y = v.x; this.z = v.y; this.y = v.z; this.x = v.w }
val Vector4f.gbgg get() = Vector4f(this.y, this.z, this.y, this.y)
val Vector4f.gbgb get() = Vector4f(this.y, this.z, this.y, this.z)
val Vector4f.gbba get() = Vector4f(this.y, this.z, this.z, this.w)
val Vector4f.gbbr get() = Vector4f(this.y, this.z, this.z, this.x)
val Vector4f.gbbg get() = Vector4f(this.y, this.z, this.z, this.y)
val Vector4f.gbbb get() = Vector4f(this.y, this.z, this.z, this.z)
val Vector4f.baaa get() = Vector4f(this.z, this.w, this.w, this.w)
val Vector4f.baar get() = Vector4f(this.z, this.w, this.w, this.x)
val Vector4f.baag get() = Vector4f(this.z, this.w, this.w, this.y)
val Vector4f.baab get() = Vector4f(this.z, this.w, this.w, this.z)
var Vector4f.bara get() = Vector4f(this.z, this.w, this.x, this.w); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.w = v.w }
var Vector4f.barr get() = Vector4f(this.z, this.w, this.x, this.x); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.x = v.w }
var Vector4f.barg get() = Vector4f(this.z, this.w, this.x, this.y); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.y = v.w }
var Vector4f.barb get() = Vector4f(this.z, this.w, this.x, this.z); set(v) { this.z = v.x; this.w = v.y; this.x = v.z; this.z = v.w }
var Vector4f.baga get() = Vector4f(this.z, this.w, this.y, this.w); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.w = v.w }
var Vector4f.bagr get() = Vector4f(this.z, this.w, this.y, this.x); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.x = v.w }
var Vector4f.bagg get() = Vector4f(this.z, this.w, this.y, this.y); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.y = v.w }
var Vector4f.bagb get() = Vector4f(this.z, this.w, this.y, this.z); set(v) { this.z = v.x; this.w = v.y; this.y = v.z; this.z = v.w }
val Vector4f.baba get() = Vector4f(this.z, this.w, this.z, this.w)
val Vector4f.babr get() = Vector4f(this.z, this.w, this.z, this.x)
val Vector4f.babg get() = Vector4f(this.z, this.w, this.z, this.y)
val Vector4f.babb get() = Vector4f(this.z, this.w, this.z, this.z)
val Vector4f.braa get() = Vector4f(this.z, this.x, this.w, this.w)
val Vector4f.brar get() = Vector4f(this.z, this.x, this.w, this.x)
var Vector4f.brag get() = Vector4f(this.z, this.x, this.w, this.y); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.y = v.w }
var Vector4f.brab get() = Vector4f(this.z, this.x, this.w, this.z); set(v) { this.z = v.x; this.x = v.y; this.w = v.z; this.z = v.w }
val Vector4f.brra get() = Vector4f(this.z, this.x, this.x, this.w)
val Vector4f.brrr get() = Vector4f(this.z, this.x, this.x, this.x)
val Vector4f.brrg get() = Vector4f(this.z, this.x, this.x, this.y)
val Vector4f.brrb get() = Vector4f(this.z, this.x, this.x, this.z)
var Vector4f.brga get() = Vector4f(this.z, this.x, this.y, this.w); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.w = v.w }
val Vector4f.brgr get() = Vector4f(this.z, this.x, this.y, this.x)
var Vector4f.brgg get() = Vector4f(this.z, this.x, this.y, this.y); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.y = v.w }
var Vector4f.brgb get() = Vector4f(this.z, this.x, this.y, this.z); set(v) { this.z = v.x; this.x = v.y; this.y = v.z; this.z = v.w }
val Vector4f.brba get() = Vector4f(this.z, this.x, this.z, this.w)
val Vector4f.brbr get() = Vector4f(this.z, this.x, this.z, this.x)
val Vector4f.brbg get() = Vector4f(this.z, this.x, this.z, this.y)
val Vector4f.brbb get() = Vector4f(this.z, this.x, this.z, this.z)
val Vector4f.bgaa get() = Vector4f(this.z, this.y, this.w, this.w)
var Vector4f.bgar get() = Vector4f(this.z, this.y, this.w, this.x); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.x = v.w }
var Vector4f.bgag get() = Vector4f(this.z, this.y, this.w, this.y); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.y = v.w }
var Vector4f.bgab get() = Vector4f(this.z, this.y, this.w, this.z); set(v) { this.z = v.x; this.y = v.y; this.w = v.z; this.z = v.w }
var Vector4f.bgra get() = Vector4f(this.z, this.y, this.x, this.w); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.w = v.w }
var Vector4f.bgrr get() = Vector4f(this.z, this.y, this.x, this.x); set(v) { this.z = v.x; this.y = v.y; this.x = v.z; this.x = v.w }
val Vector4f.bgrg get() = Vector4f(this.z, this.y, this.x, this.y)
val Vector4f.bgrb get() = Vector4f(this.z, this.y, this.x, this.z)
val Vector4f.bgga get() = Vector4f(this.z, this.y, this.y, this.w)
val Vector4f.bggr get() = Vector4f(this.z, this.y, this.y, this.x)
val Vector4f.bggg get() = Vector4f(this.z, this.y, this.y, this.y)
val Vector4f.bggb get() = Vector4f(this.z, this.y, this.y, this.z)
val Vector4f.bgba get() = Vector4f(this.z, this.y, this.z, this.w)
val Vector4f.bgbr get() = Vector4f(this.z, this.y, this.z, this.x)
val Vector4f.bgbg get() = Vector4f(this.z, this.y, this.z, this.y)
val Vector4f.bgbb get() = Vector4f(this.z, this.y, this.z, this.z)
val Vector4f.bbaa get() = Vector4f(this.z, this.z, this.w, this.w)
val Vector4f.bbar get() = Vector4f(this.z, this.z, this.w, this.x)
val Vector4f.bbag get() = Vector4f(this.z, this.z, this.w, this.y)
val Vector4f.bbab get() = Vector4f(this.z, this.z, this.w, this.z)
val Vector4f.bbra get() = Vector4f(this.z, this.z, this.x, this.w)
val Vector4f.bbrr get() = Vector4f(this.z, this.z, this.x, this.x)
val Vector4f.bbrg get() = Vector4f(this.z, this.z, this.x, this.y)
val Vector4f.bbrb get() = Vector4f(this.z, this.z, this.x, this.z)
val Vector4f.bbga get() = Vector4f(this.z, this.z, this.y, this.w)
val Vector4f.bbgr get() = Vector4f(this.z, this.z, this.y, this.x)
val Vector4f.bbgg get() = Vector4f(this.z, this.z, this.y, this.y)
val Vector4f.bbgb get() = Vector4f(this.z, this.z, this.y, this.z)
val Vector4f.bbba get() = Vector4f(this.z, this.z, this.z, this.w)
val Vector4f.bbbr get() = Vector4f(this.z, this.z, this.z, this.x)
val Vector4f.bbbg get() = Vector4f(this.z, this.z, this.z, this.y)
val Vector4f.bbbb get() = Vector4f(this.z, this.z, this.z, this.z)