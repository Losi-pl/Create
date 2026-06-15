@file:JvmName("GL20Cr")
@file:Suppress("PackageDirectoryMismatch","unused", "SpellCheckingInspection")

package com.losi.create.graphics.gl

import com.losi.create.math.*
import org.joml.*
import org.lwjgl.opengl.GL20C
import org.lwjgl.system.MemoryStack

/** `void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix4f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(4 * 4)
        matrix.get(buff)
        GL20C.glUniformMatrix4fv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix3f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(3 * 3)
        matrix.get(buff)
        GL20C.glUniformMatrix3fv(location.handle, transpose, buff)
    }
}

/** `void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat const * value)`  */
fun glUniformMatrix(location: UniformLocation, transpose: Boolean, matrix: Matrix2f) {
    MemoryStack.stackPush().use { stack ->
        val buff = stack.mallocFloat(2 * 2)
        matrix.get(buff)
        GL20C.glUniformMatrix2fv(location.handle, transpose, buff)
    }
}

//region ===================================================== Shader Programs =====================================================

/**`void glUseProgram(GLuint program)`*/
fun glUseProgram(program: ShaderProgram = ShaderProgram.NONE) = GL20C.glUseProgram(program.handle.toInt())

/**`GLuint glCreateProgram(void)`*/
fun glCreateProgram() = ShaderProgram(GL20C.glCreateProgram().toUInt())

/**`void glDeleteProgram(GLuint program)`*/
fun glDeleteProgram(program: ShaderProgram) = GL20C.glDeleteProgram(program.handle.toInt())

/**`void glLinkProgram(GLuint program)`
 *
 * `void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glLinkProgram(program: ShaderProgram): Boolean {
    GL20C.glLinkProgram(program.handle.toInt())
    return GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.LinkStatus.gl) == GL20C.GL_TRUE
}

/**`void glValidateProgram(GLuint program)`
 *
 * `void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glValidateProgram(program: ShaderProgram): Boolean {
    GL20C.glValidateProgram(program.handle.toInt())
    return GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.IsValid.gl) == GL20C.GL_TRUE
}

/**`void glGetProgramInfoLog(GLuint program, GLsizei maxLength, GLsizei* length, GLchar* infoLog)`*/
fun glGetProgramLog(program: ShaderProgram) = GL20C.glGetProgramInfoLog(program.handle.toInt())

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramParam(program: ShaderProgram, param: ProgramParam) = GL20C.glGetProgrami(program.handle.toInt(), param.gl)

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramIsForDeletion(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.IsForDeletion.gl) == GL20C.GL_TRUE

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramLogSize(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.LosSize.gl).toUInt()

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramPartCount(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.PartCount.gl).toUInt()

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramAttributeCount(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.AttributeCount.gl).toUInt()

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramAttributeMaxNameSize(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.MaxAttributeNameSize.gl).toUInt()

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramUniformCount(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.UniformCount.gl).toUInt()

/**`void glGetProgramiv(GLuint program, GLenum pname, GLint * params)`*/
fun glGetProgramUniformMaxNameSize(program: ShaderProgram) = GL20C.glGetProgrami(program.handle.toInt(), ProgramParam.MaxUniformNameSize.gl).toUInt()

data class ValEntryData(val name: String, val count: UInt, val type: GLSLVar)
/**`void glGetActiveUniform(GLuint program, GLuint index, GLsizei maxLength, GLsizei * length, GLint * size, GLenum * type, GLchar * name)`*/
fun glGetUniform(program: ShaderProgram, index: UInt): ValEntryData {
    MemoryStack.stackPush().use { stack ->
        val count = stack.mallocInt(1)
        val type = stack.mallocInt(1)
        val name = GL20C.glGetActiveUniform(program.handle.toInt(), index.toInt(), count, type)
        return ValEntryData(name, count.get(0).toUInt(), GLSLVar.of(type.get(0))?:
            throw Exception("glGetActiveUniform() has encountered an unknown type: ${type.get(0).toUInt()}"))
    }
}

/**`void glGetActiveAttrib(GLuint program, GLuint index, GLsizei maxLength, GLsizei * length, GLint * size, GLenum * type, GLchar * name)`*/
fun glGetAttribute(program: ShaderProgram, index: UInt): ValEntryData {
    MemoryStack.stackPush().use { stack ->
        val count = stack.mallocInt(1)
        val type = stack.mallocInt(1)
        val name = GL20C.glGetActiveAttrib(program.handle.toInt(), index.toInt(), count, type)
        return ValEntryData(name, count.get(0).toUInt(), GLSLVar.of(type.get(0))?:
        throw Exception("glGetActiveAttrib() has encountered an unknown type: ${type.get(0).toUInt()}"))
    }
}

/**`GLint glGetUniformLocation(GLuint program, GLchar const * name)`*/
fun glGetUniformLocation(program: ShaderProgram, name: String) = UniformLocation(GL20C.glGetUniformLocation(program.handle.toInt(), name))

/**`GLint glGetAttribLocation(GLuint program, GLchar const * name)`*/
fun glGetAttributeLocation(program: ShaderProgram, name: String) = AttributeLocation(GL20C.glGetAttribLocation(program.handle.toInt(), name))
//endregion


//region ===================================================== Shaders =====================================================

/**`GLuint glCreateShader(GLenum type)`*/
fun glCreateShader(type: ShaderType) = ShaderPart(GL20C.glCreateShader(type.gl).toUInt())

/**`void glDeleteShader(GLuint shader)`*/
fun glDeleteShader(shader: ShaderPart) = GL20C.glDeleteShader(shader.handle.toInt())

/**`void glShaderSource(GLuint shader, GLsizei count, GLchar const* const* strings, GLint const* length)`*/
fun glShaderSource(shader: ShaderPart, code: String) = GL20C.glShaderSource(shader.handle.toInt(), code)

/**`void glCompileShader(GLuint shader`*/
fun glCompileShader(shader: ShaderPart) = GL20C.glCompileShader(shader.handle.toInt())

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint* params)`*/
fun glGetShaderParam(shader: ShaderPart, param: ShaderParam) = GL20C.glGetShaderi(shader.handle.toInt(), param.gl)

/**`void glGetShaderInfoLog(GLuint shader, GLsizei maxLength, GLsizei * length, GLchar * infoLog)`*/
fun glGetShaderLog(shader: ShaderPart) = GL20C.glGetShaderInfoLog(shader.handle.toInt())

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderType(shader: ShaderPart) = ShaderType.of(GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.Type.gl))?: throw Exception("glGetShaderType() encountered an unknown shader type: ${shader.handle}")

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderCompiledStatus(shader: ShaderPart) = GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.IsCompiled.gl) == GL20C.GL_TRUE

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderForDeleate(shader: ShaderPart) = GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.IsForDeletion.gl) == GL20C.GL_TRUE

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderLogSize(shader: ShaderPart) = GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.LogSize.gl).toUInt()

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderCodeSize(shader: ShaderPart) = GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.CodeSize.gl).toUInt()

/**`void glGetShaderiv(GLuint shader, GLenum pname, GLint * params)`*/
fun glGetShaderCodeSizeANG(shader: ShaderPart) = GL20C.glGetShaderi(shader.handle.toInt(), ShaderParam.CodeSize.gl).toUInt()

/**`void glAttachShader(GLuint program, GLuint shader)`*/
fun glAttachShader(program: ShaderProgram, shader: ShaderPart) = GL20C.glAttachShader(program.handle.toInt(), shader.handle.toInt())

/**`void glDetachShader(GLuint program, GLuint shader)`*/
fun glDetachShader(program: ShaderProgram, shader: ShaderPart) = GL20C.glDetachShader(program.handle.toInt(), shader.handle.toInt())
//endregion


//region ===================================================== Uniforms =====================================================

/**`void glUniform1i(GLint location, GLint v0)`*/
fun glUniform1(location: UniformLocation, value: Byte) = GL20C.glUniform1i(location.handle, value.toInt())

/**`void glUniform1i(GLint location, GLint v0)`*/
fun glUniform1(location: UniformLocation, value: Short) = GL20C.glUniform1i(location.handle, value.toInt())

/**`void glUniform1i(GLint location, GLint v0)`*/
fun glUniform1(location: UniformLocation, value: Int) = GL20C.glUniform1i(location.handle, value)

/**`void glUniform1f(GLint location, GLfloat v0)`*/
fun glUniform1(location: UniformLocation, value: Float) = GL20C.glUniform1f(location.handle, value)

/**`void glUniform1i(GLint location, GLint v0)`*/
fun glUniform1(location: UniformLocation, value: Boolean) = GL20C.glUniform1i(location.handle, if(value) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform2i(GLint location, GLint v0, GLint v1)`*/
fun glUniform2(location: UniformLocation, value: Vector2b) = GL20C.glUniform2i(location.handle,
    if(value.x) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.y) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform2i(GLint location, GLint v0, GLint v1)`*/
fun glUniform2(location: UniformLocation, v1: Boolean, v2: Boolean) = GL20C.glUniform2i(location.handle,
    if(v1) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v2) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform2i(GLint location, GLint v0, GLint v1)`*/
fun glUniform2(location: UniformLocation, value: Vector2i) = GL20C.glUniform2i(location.handle, value.x, value.y)

/**`void glUniform2i(GLint location, GLint v0, GLint v1)`*/
fun glUniform2(location: UniformLocation, v1: Int, v2: Int) = GL20C.glUniform2i(location.handle, v1, v2)

/**`void glUniform2f(GLint location, GLfloat v0, GLfloat v1)`*/
fun glUniform2(location: UniformLocation, value: Vector2f) = GL20C.glUniform2f(location.handle, value.x, value.y)

/**`void glUniform2f(GLint location, GLfloat v0, GLfloat v1)`*/
fun glUniform2(location: UniformLocation, v1: Float, v2: Float) = GL20C.glUniform2f(location.handle, v1, v2)

/**`void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2)`*/
fun glUniform3(location: UniformLocation, value: Vector3b) = GL20C.glUniform3i(location.handle,
    if(value.x) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.y) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.z) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2)`*/
fun glUniform3(location: UniformLocation, v1: Boolean, v2: Boolean, v3: Boolean) = GL20C.glUniform3i(location.handle,
    if(v1) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v2) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v3) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2)`*/
fun glUniform3(location: UniformLocation, value: Vector3i) = GL20C.glUniform3i(location.handle, value.x, value.y, value.z)

/**`void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2)`*/
fun glUniform3(location: UniformLocation, v1: Int, v2: Int, v3: Int) = GL20C.glUniform3i(location.handle, v1, v2, v3)

/**`void glUniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2)`*/
fun glUniform3(location: UniformLocation, value: Vector3f) = GL20C.glUniform3f(location.handle, value.x, value.y, value.z)

/**`void glUniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2)`*/
fun glUniform3(location: UniformLocation, v1: Float, v2: Float, v3: Float) = GL20C.glUniform3f(location.handle, v1, v2, v3)

/**`void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3)`*/
fun glUniform4(location: UniformLocation, value: Vector4b) = GL20C.glUniform4i(location.handle,
    if(value.x) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.y) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.z) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(value.w) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3)`*/
fun glUniform4(location: UniformLocation, v1: Boolean, v2: Boolean, v3: Boolean, v4: Boolean) = GL20C.glUniform4i(location.handle,
    if(v1) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v2) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v3) GL20C.GL_TRUE else GL20C.GL_FALSE,
    if(v4) GL20C.GL_TRUE else GL20C.GL_FALSE)

/**`void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3)`*/
fun glUniform4(location: UniformLocation, value: Vector4i) = GL20C.glUniform4i(location.handle, value.x, value.y, value.z, value.w)

/**`void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3)`*/
fun glUniform4(location: UniformLocation, v1: Int, v2: Int, v3: Int, v4: Int) = GL20C.glUniform4i(location.handle, v1, v2, v3, v4)

/**`void glUniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3)`*/
fun glUniform4(location: UniformLocation, value: Vector4f) = GL20C.glUniform4f(location.handle, value.x, value.y, value.z, value.w)

/**`void glUniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3)`*/
fun glUniform4(location: UniformLocation, v1: Float, v2: Float, v3: Float, v4: Float) = GL20C.glUniform4f(location.handle, v1, v2, v3, v4)

//endregion


fun glVertexAttribPointer(location: AttributeLocation, type: GLSLVar, stride: Int, offsetr: Long) {
    GL20C.glEnableVertexAttribArray(location.handle)
    GL20C.glVertexAttribPointer(location.handle, type.primitivesCount.toInt(), type.primitive.gl, false, stride, offsetr)
}