package com.losi.create.utility;

import org.jetbrains.annotations.NotNull;
import org.joml.Matrix4f;
import org.lwjgl.system.MemoryStack;
import org.lwjgl.system.NativeType;

import static org.lwjgl.opengl.GL20.*;

@SuppressWarnings({"RedundantSuppression", "SpellCheckingInspection", "GrazieInspectionRunner", "typo"})
public class GL20C {
    /** {@code void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)} */
    public static void glUniformMatrix4f(
            @NativeType("GLint") int location,
            @NativeType("GLboolean")boolean transpose,
            @NativeType("GLfloat const *") @NotNull Matrix4f matrix)
    {
        try (var stack = MemoryStack.stackPush())
        {
            var buff = stack.mallocFloat(4 * 4);
            matrix.get(buff);
            glUniformMatrix4fv(location, transpose, buff);
        }
    }
}
