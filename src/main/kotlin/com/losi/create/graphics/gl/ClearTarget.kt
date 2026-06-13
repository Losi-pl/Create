package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

sealed interface ClearTarget {
    private enum class EnumTarget(override val gl: Int, override val glName: String): ClearTarget {
        Color(GL11.GL_COLOR_BUFFER_BIT, "GL_COLOR_BUFFER_BIT"),
        Depth(GL11.GL_DEPTH_BUFFER_BIT, "GL_DEPTH_BUFFER_BIT"),
        Stencil(GL11.GL_STENCIL_BUFFER_BIT, "GL_STENCIL_BUFFER_BIT"),
        Accum(GL11.GL_ACCUM_BUFFER_BIT, "GL_ACCUM_BUFFER_BIT"),
    }
    private data class Composite(override val gl: Int): ClearTarget {
        override val glName: String get() {
            val builder = StringBuilder()
            EnumTarget.entries.forEach {
                if(gl and it.gl != it.gl)
                    return@forEach
                if(builder.isNotEmpty())
                    builder.append(" | ")
                builder.append(it.glName)
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

    @Suppress("unused")
    companion object {
        /**Clears the color buffers currently enabled for writing
         *
         * `GL_COLOR_BUFFER_BIT`*/
        val Color: ClearTarget get() = EnumTarget.Color
        /**Clears the depth buffer (Z-buffer)
         *
         * `GL_DEPTH_BUFFER_BIT`*/
        val Depth: ClearTarget get() = EnumTarget.Depth
        /**Clears the stencil buffer
         *
         * `GL_STENCIL_BUFFER_BIT`*/
        val Stencil: ClearTarget get() = EnumTarget.Stencil
        /**Clears the accumulation buffer. This is a legacy feature from older OpenGL versions (removed in Core Profile)
         *
         * `GL_ACCUM_BUFFER_BIT`*/
        val Accum: ClearTarget get() = EnumTarget.Accum

        /**Returns an immutable [EnumEntries][kotlin.enums.EnumEntries] list containing the constants of this enum type, in the order they're declared.*/
        val entries: List<ClearTarget> get() = EnumTarget.entries

        val ALL: ClearTarget get() = lazy_all.value
        private val lazy_all = lazy {
            var gl = 0
            EnumTarget.entries.forEach {
                gl = gl or it.gl
            }
            Composite(gl)
        }
    }

    infix fun and(other: ClearTarget): ClearTarget {
        return Composite(gl or other.gl)
    }

    infix fun contains(lesser: ClearTarget): Boolean {
        return this.gl and lesser.gl == lesser.gl
    }

    val gl: Int
    val glName: String
    val name: String
}