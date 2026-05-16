package com.losi.create.internal;

import org.lwjgl.system.MemoryUtil;

import static org.lwjgl.opengl.GL43.*;

public class GLErrorHandler
{
    public static void bindErrorCather() {
        glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);
        glDebugMessageCallback((source, type, id, severity, length, message, _) -> {
            String messageStr = org.lwjgl.opengl.GLDebugMessageCallback.getMessage(length, message);

            System.err.printf("[OpenGL Debug] Source: %s | Type: %s | ID: %d | Severity: %s | Message: %s%n",
                    switch (source) {
                        case GL_DEBUG_SOURCE_API             -> "API";
                        case GL_DEBUG_SOURCE_WINDOW_SYSTEM   -> "OS DRIVER / GLFW";
                        case GL_DEBUG_SOURCE_SHADER_COMPILER -> "SHADER COMPILER ";
                        case GL_DEBUG_SOURCE_THIRD_PARTY     -> "THIRD PARTY";
                        case GL_DEBUG_SOURCE_APPLICATION     -> "APPLICATION";
                        case GL_DEBUG_SOURCE_OTHER           -> "OTHER";
                        default -> String.format("0x%X", source);
                    },
                    switch (type) {
                        case GL_DEBUG_TYPE_ERROR               -> "ERROR";
                        case GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR -> "DEPRECATED BEHAVIOR";
                        case GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR  -> "UNDEFINED BEHAVIOR";
                        case GL_DEBUG_TYPE_PORTABILITY         -> "PORTABILITY WARNING";
                        case GL_DEBUG_TYPE_PERFORMANCE         -> "PERFORMANCE HINT";
                        case GL_DEBUG_TYPE_MARKER              -> "MARKER";
                        case GL_DEBUG_TYPE_PUSH_GROUP          -> "PUSH GROUP";
                        case GL_DEBUG_TYPE_POP_GROUP           -> "POP GROUP";
                        case GL_DEBUG_TYPE_OTHER               -> "OTHER";
                        default -> String.format("0x%X", type);
                    }, id, switch (severity) {
                        case GL_DEBUG_SEVERITY_HIGH         -> "CRITICAL";
                        case GL_DEBUG_SEVERITY_MEDIUM       -> "WARNING";
                        case GL_DEBUG_SEVERITY_LOW          -> "INFO";
                        case GL_DEBUG_SEVERITY_NOTIFICATION -> "NOTIFICATION";
                        default -> String.format("0x%X", severity);
                    }, messageStr);

            // Optional: Break execution on high-severity errors
            if (severity == GL_DEBUG_SEVERITY_HIGH) {
                throw new RuntimeException("Critical OpenGL Error encountered!\n" +  messageStr);
            }
        }, MemoryUtil.NULL);
    }
}
