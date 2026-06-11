@file:Suppress("unused", "SpellCheckingInspection")
package com.losi.create.utility.joml

import org.joml.*

fun Vector3f(xy: Vector2f, z: Float) = Vector3f(xy.x, xy.y, z)
fun Vector3f(x: Float, yz: Vector2f) = Vector3f(x, yz.x, yz.y)

fun Vector4f(xy: Vector2f, zw: Vector2f) = Vector4f(xy.x, xy.y, zw.x, zw.y)
fun Vector4f(xy: Vector2f, z: Float, w: Float) = Vector4f(xy.x, xy.y, z, w)
fun Vector4f(x: Float, y: Float, zw: Vector2f) = Vector4f(x, y, zw.x, zw.y)
fun Vector4f(x:Float, yz: Vector2f, w: Float) = Vector4f(x, yz.x, yz.y, w)
fun Vector4f(xyz: Vector3f, w: Float) = Vector4f(xyz.x, xyz.y, xyz.z, w)
fun Vector4f(x: Float, yzw: Vector3f) = Vector4f(x, yzw.x, yzw.y, yzw.z)