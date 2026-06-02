package com.losi.create.graphics.gl;

import org.jetbrains.annotations.NotNull;
import org.joml.*;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.NativeType;

import static org.lwjgl.opengl.GL20C.*;

@SuppressWarnings({"RedundantSuppression", "SpellCheckingInspection", "GrazieInspectionRunner", "typo"})
public class GL20C {
    /** {@code void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix4f(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLfloat const *") @NotNull Matrix4f matrix) {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(4 * 4);
            matrix.get(buff);
            glUniformMatrix4fv(location, transpose, buff);
        }
    }
    /** {@code void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix3f(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLfloat const *") @NotNull Matrix3f matrix) {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(3 * 3);
            matrix.get(buff);
            glUniformMatrix3fv(location, transpose, buff);
        }
    }
    /** {@code void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix2f(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLfloat const *") @NotNull Matrix2f matrix) {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(2 * 2);
            matrix.get(buff);
            glUniformMatrix2fv(location, transpose, buff);
        }
    }
}
