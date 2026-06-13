@file:Suppress("unused")
package com.losi.create.graphics.gl

import org.lwjgl.glfw.GLFW

sealed interface KeyMods {
    private enum class EnumTarget(override val gl: Int, override val glfwName: String): KeyMods {
        Shift(GLFW.GLFW_MOD_SHIFT, "GLFW_MOD_SHIFT"),
        Control(GLFW.GLFW_MOD_CONTROL, "GLFW_MOD_CONTROL"),
        Alt(GLFW.GLFW_MOD_ALT, "GLFW_MOD_ALT"),
        Super(GLFW.GLFW_MOD_SUPER, "GLFW_MOD_SUPER"),
        CapsLock(GLFW.GLFW_MOD_CAPS_LOCK, "GLFW_MOD_CAPS_LOCK"),
        NumPad(GLFW.GLFW_MOD_NUM_LOCK, "GLFW_MOD_NUM_LOCK"),
    }
    @Suppress("DuplicatedCode")
    private data class Composite(override val gl: Int): KeyMods {
        override val glfwName: String get() {
            val builder = StringBuilder()
            EnumTarget.entries.forEach {
                if(gl and it.gl != it.gl)
                    return@forEach
                if(builder.isNotEmpty())
                    builder.append(" | ")
                builder.append(it.glfwName)
            }
            return builder.toString()
        }
        override fun toString(): String {
            val builder = StringBuilder()
            EnumTarget.entries.forEach {
                if(gl and it.gl != it.gl)
                    return@forEach
                if(builder.isNotEmpty())
                    builder.append(" and ")
                builder.append(it.name)
            }
            return builder.toString()
        }
        override val name: String get() = toString()
    }

    companion object {
        fun of(glfw: Int): KeyMods = Composite(glfw)

        /**`GLFW_MOD_SHIFT`*/
        val Shift: KeyMods get() = EnumTarget.Shift
        /**`GLFW_MOD_CONTROL`*/
        val Control: KeyMods get() = EnumTarget.Control
        /**`GLFW_MOD_ALT`*/
        val Alt: KeyMods get() = EnumTarget.Alt
        /**`GLFW_MOD_SUPER`*/
        val Super: KeyMods get() = EnumTarget.Super

        val ALL: KeyMods get() = lazy_all.value
        private val lazy_all = lazy {
            var glfw = 0
            EnumTarget.entries.forEach {
                glfw = glfw or it.gl
            }
            Composite(glfw)
        }
    }

    infix fun and(other: KeyMods): KeyMods {
        return Composite(gl or other.gl)
    }

    infix fun contains(lesser: KeyMods): Boolean {
        return this.gl and lesser.gl == lesser.gl
    }

    val gl: Int
    val glfwName: String
    val name: String

    val isShift: Boolean get() = this contains Shift
    val isControl: Boolean get() = this contains Control
    val isAlt: Boolean get() = this contains Alt
    val isSuper: Boolean get() = this contains Super
}