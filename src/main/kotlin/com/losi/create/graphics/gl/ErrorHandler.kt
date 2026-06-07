package com.losi.create.graphics.gl

import com.losi.create.utility.splitCamelCase
import org.lwjgl.opengl.GL11
import org.lwjgl.opengl.GL43

/**A method meant for detecting and processing OpenGL errors
 *
 * When it is called in a thread it will connect to the context
 * The caught error will be pushed to the console, it the error in question is critical it will be thrown as an Exception */
fun bindErrorCather() {
    GL11.glEnable(GL43.GL_DEBUG_OUTPUT_SYNCHRONOUS)
    glDebugMessageCallback { source, type, id, severity, message ->
        System.err.println("[OpenGL Debug] Source: ${source.name.splitCamelCase()
        } | Type: ${type.name.splitCamelCase()
        } | ID: ${id.id} | Severity: $severity | Message: $message") }
}

enum class DebugMessageSource(val gl: Int, val glName: String) {
    API           (GL43.GL_DEBUG_SOURCE_API,             "GL_DEBUG_SOURCE_API"),
    WindowSystem  (GL43.GL_DEBUG_SOURCE_WINDOW_SYSTEM,   "GL_DEBUG_SOURCE_WINDOW_SYSTEM"),
    ShaderCompiler(GL43.GL_DEBUG_SOURCE_SHADER_COMPILER, "GL_DEBUG_SOURCE_SHADER_COMPILER"),
    ThirdParty    (GL43.GL_DEBUG_SOURCE_THIRD_PARTY,     "GL_DEBUG_SOURCE_THIRD_PARTY"),
    Application   (GL43.GL_DEBUG_SOURCE_APPLICATION,     "GL_DEBUG_SOURCE_APPLICATION"),
    Other         (GL43.GL_DEBUG_SOURCE_OTHER,           "GL_DEBUG_SOURCE_OTHER"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}

enum class DebugMessageType(val gl: Int, val glName: String) {
    Error      (GL43.GL_DEBUG_TYPE_ERROR,               "GL_DEBUG_TYPE_ERROR"),
    Deprecated (GL43.GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR, "GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR"),
    Undefined  (GL43.GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR,  "GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR"),
    Portability(GL43.GL_DEBUG_TYPE_PORTABILITY,         "GL_DEBUG_TYPE_PORTABILITY"),
    Performance(GL43.GL_DEBUG_TYPE_PERFORMANCE,         "GL_DEBUG_TYPE_PERFORMANCE"),
    Marker     (GL43.GL_DEBUG_TYPE_MARKER,              "GL_DEBUG_TYPE_MARKER"),
    PushGroup  (GL43.GL_DEBUG_TYPE_PUSH_GROUP,          "GL_DEBUG_TYPE_PUSH_GROUP"),
    PopGroup   (GL43.GL_DEBUG_TYPE_POP_GROUP,           "GL_DEBUG_TYPE_POP_GROUP"),
    Other      (GL43.GL_DEBUG_TYPE_OTHER,               "GL_DEBUG_TYPE_OTHER"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}

enum class DebugMessageSeverity(val gl: Int, val glName: String) {
    Critical    (GL43.GL_DEBUG_SEVERITY_HIGH,         "GL_DEBUG_SEVERITY_HIGH"),
    Warning     (GL43.GL_DEBUG_SEVERITY_MEDIUM,       "GL_DEBUG_SEVERITY_MEDIUM"),
    Info        (GL43.GL_DEBUG_SEVERITY_LOW,          "GL_DEBUG_SEVERITY_LOW"),
    Notification(GL43.GL_DEBUG_SEVERITY_NOTIFICATION, "GL_DEBUG_SEVERITY_NOTIFICATION"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}

@JvmInline
value class DebugMessageId(val id: Int) {
    override fun toString() = String.format("0x%X", id)
}