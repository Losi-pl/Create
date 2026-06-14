@file:JvmName("GLFWCr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import com.losi.create.graphics.*
import org.lwjgl.glfw.GLFW

/**`void glfwSetWindowShouldClose(GLFWwindow * window, int value)`*/
fun glfwWindowShouldClose(window: Window) = GLFW.glfwSetWindowShouldClose(window.handle, true)