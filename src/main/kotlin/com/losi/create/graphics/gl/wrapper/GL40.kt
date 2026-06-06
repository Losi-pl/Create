package com.losi.create.graphics.gl;

import org.jetbrains.annotations.NotNull;
import org.joml.*;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.NativeType;
import static org.lwjgl.opengl.GL40C.*;

@SuppressWarnings({"RedundantSuppression", "SpellCheckingInspection", "GrazieInspectionRunner", "typo"})
public class GL40Cr extends GL21Cr {
    /** {@code void glUniformMatrix4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)} */
    public static void glUniformMatrix4d(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLdouble const *") @NotNull Matrix4d matrix) {
        try (var stack = MemoryStack.stackPush())
            {
                var buff = stack.mallocDouble(4 * 4);
                matrix.get(buff);
                glUniformMatrix4dv(location, transpose, buff);
            }
        }
    /** {@code void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)} */
    public static void glUniformMatrix3d(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLdouble const *") @NotNull Matrix3d matrix) {
        try (var stack = MemoryStack.stackPush())
            {
                var buff = stack.mallocDouble(3 * 3);
                matrix.get(buff);
                glUniformMatrix3dv(location, transpose, buff);
            }
        }
    /** {@code void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)} */
    public static void glUniformMatrix2d(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLdouble const *") @NotNull Matrix2d matrix) {
        try (var stack = MemoryStack.stackPush())
            {
                var buff = stack.mallocDouble(2 * 2);
                matrix.get(buff);
                glUniformMatrix2dv(location, transpose, buff);
            }
        }

    /** {@code void glUniformMatrix4x3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)} */
    public static void glUniformMatrix4x3d(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLdouble const *") @NotNull Matrix4x3d matrix) {
        try (var stack = MemoryStack.stackPush())
            {
                var buff = stack.mallocDouble(4 * 3);
                matrix.get(buff);
                glUniformMatrix4x3dv(location, transpose, buff);
            }
        }
    /** {@code void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLdouble const * value)} */
    public static void glUniformMatrix3x2d(@NativeType("GLint") int location, @NativeType("GLboolean")boolean transpose, @NativeType("GLdouble const *") @NotNull Matrix3x2d matrix) {
        try (var stack = MemoryStack.stackPush())
            {
                var buff = stack.mallocDouble(3 * 2);
                matrix.get(buff);
                glUniformMatrix3x2dv(location, transpose, buff);
            }
        }
}
