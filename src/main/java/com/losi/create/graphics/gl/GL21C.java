package com.losi.create.graphics.gl;

import org.jetbrains.annotations.NotNull;
import org.joml.*;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.NativeType;

import static org.lwjgl.opengl.GL21C.*;

@SuppressWarnings({"RedundantSuppression", "SpellCheckingInspection", "GrazieInspectionRunner", "typo"})
public class GL21C extends GL20C {
    /** {@code void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix4x3f(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLfloat const *") @NotNull Matrix4x3f matrix) {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(4 * 3);
            matrix.get(buff);
            glUniformMatrix4x3fv(location, transpose, buff);
        }
    }
    /** {@code void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix3x2f( @NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLfloat const *") @NotNull Matrix3x2f matrix) {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(3 * 2);
            matrix.get(buff);
            glUniformMatrix3x2fv(location, transpose, buff);
        }
    }
}
