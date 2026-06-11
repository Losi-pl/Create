@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import com.losi.create.math.*
import org.joml.*

// ===================================== Float =====================================
fun Vector3f(xy: Vector2f, z: Float) = Vector3f(xy.x, xy.y, z)
fun Vector3f(x: Float, yz: Vector2f) = Vector3f(x, yz.x, yz.y)

fun Vector4f(xy: Vector2f, zw: Vector2f) = Vector4f(xy.x, xy.y, zw.x, zw.y)
fun Vector4f(xy: Vector2f, z: Float, w: Float) = Vector4f(xy.x, xy.y, z, w)
fun Vector4f(x: Float, y: Float, zw: Vector2f) = Vector4f(x, y, zw.x, zw.y)
fun Vector4f(x: Float, yz: Vector2f, w: Float) = Vector4f(x, yz.x, yz.y, w)
fun Vector4f(xyz: Vector3f, w: Float) = Vector4f(xyz.x, xyz.y, xyz.z, w)
fun Vector4f(x: Float, yzw: Vector3f) = Vector4f(x, yzw.x, yzw.y, yzw.z)


// ===================================== Double =====================================
fun Vector3d(xy: Vector2d, z: Double) = Vector3d(xy.x, xy.y, z)
fun Vector3d(x: Double, yz: Vector2d) = Vector3d(x, yz.x, yz.y)

fun Vector4d(xy: Vector2d, zw: Vector2d) = Vector4d(xy.x, xy.y, zw.x, zw.y)
fun Vector4d(xy: Vector2d, z: Double, w: Double) = Vector4d(xy.x, xy.y, z, w)
fun Vector4d(x: Double, y: Double, zw: Vector2d) = Vector4d(x, y, zw.x, zw.y)
fun Vector4d(x: Double, yz: Vector2d, w: Double) = Vector4d(x, yz.x, yz.y, w)
fun Vector4d(xyz: Vector3d, w: Double) = Vector4d(xyz.x, xyz.y, xyz.z, w)
fun Vector4d(x: Double, yzw: Vector3d) = Vector4d(x, yzw.x, yzw.y, yzw.z)


// ===================================== Boolean =====================================
fun Vector3b(xy: Vector2b, z: Boolean) = Vector3b(xy.x, xy.y, z)
fun Vector3b(x: Boolean, yz: Vector2b) = Vector3b(x, yz.x, yz.y)

fun Vector4b(xy: Vector2b, zw: Vector2b) = Vector4b(xy.x, xy.y, zw.x, zw.y)
fun Vector4b(xy: Vector2b, z: Boolean, w: Boolean) = Vector4b(xy.x, xy.y, z, w)
fun Vector4b(x: Boolean, y: Boolean, zw: Vector2b) = Vector4b(x, y, zw.x, zw.y)
fun Vector4b(x: Boolean, yz: Vector2b, w: Boolean) = Vector4b(x, yz.x, yz.y, w)
fun Vector4b(xyz: Vector3b, w: Boolean) = Vector4b(xyz.x, xyz.y, xyz.z, w)
fun Vector4b(x: Boolean, yzw: Vector3b) = Vector4b(x, yzw.x, yzw.y, yzw.z)


// ===================================== Int =====================================
fun Vector3i(xy: Vector2i, z: Int) = Vector3i(xy.x, xy.y, z)
fun Vector3i(x: Int, yz: Vector2i) = Vector3i(x, yz.x, yz.y)

fun Vector4i(xy: Vector2i, zw: Vector2i) = Vector4i(xy.x, xy.y, zw.x, zw.y)
fun Vector4i(xy: Vector2i, z: Int, w: Int) = Vector4i(xy.x, xy.y, z, w)
fun Vector4i(x: Int, y: Int, zw: Vector2i) = Vector4i(x, y, zw.x, zw.y)
fun Vector4i(x: Int, yz: Vector2i, w: Int) = Vector4i(x, yz.x, yz.y, w)
fun Vector4i(xyz: Vector3i, w: Int) = Vector4i(xyz.x, xyz.y, xyz.z, w)
fun Vector4i(x: Int, yzw: Vector3i) = Vector4i(x, yzw.x, yzw.y, yzw.z)