package com.losi.create.internal;

import org.lwjgl.system.MemoryUtil;

import static org.lwjgl.opengl.GL43.*;

public class GLErrorHandler
{
    public static void bindErrorCather() {
        glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);
        glDebugMessageCallback((source, type, id, severity, length, message, _) -> {
            String messageStr = org.lwjgl.opengl.GLDebugMessageCallback.getMessage(length, message);

            System.err.printf("[OpenGL Debug] Source: 0x%X | Type: 0x%X | ID: %d | Severity: %s | Message: %s%n",
                    source, type, id, switch (severity) {
                        case GL_DEBUG_SEVERITY_HIGH       -> "CRITICAL";
                        case GL_DEBUG_SEVERITY_MEDIUM     -> "WARNING";
                        case GL_DEBUG_SEVERITY_LOW        -> "INFO";
                        case GL_DEBUG_SEVERITY_NOTIFICATION -> "NOTIFICATION";
                        default -> String.format("0x%X", severity);
                    }, messageStr);

            // Optional: Break execution on high-severity errors
            if (severity == GL_DEBUG_SEVERITY_HIGH) {
                throw new RuntimeException("Critical OpenGL Error encountered!");
            }
        }, MemoryUtil.NULL);
    }
}
