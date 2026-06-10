@file:Suppress("unused")
package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class ProgramParam(val gl: Int, val glName: String) {
    /**A flag specifying if this [ShaderProgram] is marked for deletion
     *
     * `GL_DELETE_STATUS`*/
    IsForDeletion(GL20.GL_DELETE_STATUS, "GL_DELETE_STATUS"),

    /**A flag specifying if linking of the [ShaderProgram] was successful
     *
     * `GL_LINK_STATUS`*/
    LinkStatus(GL20.GL_LINK_STATUS, "GL_LINK_STATUS"),

    /**A flag specifying if last validation of [ShaderProgram] was successful
     *
     * `GL_VALIDATE_STATUS`*/
    IsValid(GL20.GL_VALIDATE_STATUS, "GL_VALIDATE_STATUS"),

    /**Length of a log of this [ShaderProgram], if there is no log, will return 0
     *
     * `GL_INFO_LOG_LENGTH`*/
    LosSize(GL20.GL_INFO_LOG_LENGTH, "GL_INFO_LOG_LENGTH"),

    /**A count of [ShaderPart]'s attached to this [ShaderProgram]
     *
     * `GL_ATTACHED_SHADERS`*/
    PartCount(GL20.GL_ATTACHED_SHADERS, "GL_ATTACHED_SHADERS"),

    /**A count of Attributes in this [ShaderProgram]
     *
     * `GL_ACTIVE_ATTRIBUTES`*/
    AttributeCount(GL20.GL_ACTIVE_ATTRIBUTES, "GL_ACTIVE_ATTRIBUTES"),

    /**Length of the longest Attribute name present. If there are none, will return 0
     *
     * `GL_ACTIVE_ATTRIBUTE_MAX_LENGTH`*/
    MaxAttributeNameSize(GL20.GL_ACTIVE_ATTRIBUTE_MAX_LENGTH, "GL_ACTIVE_ATTRIBUTE_MAX_LENGTH"),

    /**A count of Uniforms in this [ShaderProgram]
     *
     * `GL_ACTIVE_UNIFORMS`*/
    UniformCount(GL20.GL_ACTIVE_UNIFORMS, "GL_ACTIVE_UNIFORMS"),

    /**Length of the longest Uniform name present. If there are none, will return 0
     *
     * `GL_ACTIVE_UNIFORM_MAX_LENGTH`*/
    MaxUniformNameSize(GL20.GL_ACTIVE_UNIFORM_MAX_LENGTH, "GL_ACTIVE_UNIFORM_MAX_LENGTH"),

    /**The max count of vertices the [Geometry Shader][ShaderType.Geometry] can output
     *
     * `GL_GEOMETRY_VERTICES_OUT`*/
    MaxVerticesCount(GL32.GL_GEOMETRY_VERTICES_OUT, "GL_GEOMETRY_VERTICES_OUT"),

    /**The type of input data accepted by the [Geometry Shader][ShaderType.Geometry]
     *
     * `GL_GEOMETRY_INPUT_TYPE`*///TODO: Enum for type's of Geometry input
    GeometryInputType(GL32.GL_GEOMETRY_INPUT_TYPE, "GL_GEOMETRY_INPUT_TYPE"),

    /**The type of output data from [Geometry Shader][ShaderType.Geometry]
     *
     * `GL_GEOMETRY_OUTPUT_TYPE`*///TODO: Enum for type's of Geometry output
    GeometryOutputType(GL32.GL_GEOMETRY_OUTPUT_TYPE, "GL_GEOMETRY_OUTPUT_TYPE"),

    /**Count of invocations per primitive, the [Geometry Shader][ShaderType.Geometry] will execute.
     *
     * `GL_GEOMETRY_SHADER_INVOCATIONS`*/
    GeometryInvocationCount(GL40.GL_GEOMETRY_SHADER_INVOCATIONS, "GL_GEOMETRY_SHADER_INVOCATIONS"),

    /**Count of vertices in the [Tessellation Control Shader][ShaderType.TessControl]'s output patch.
     *
     * `GL_TESS_CONTROL_OUTPUT_VERTICES`*/
    TessControlOutCount(GL40.GL_TESS_CONTROL_OUTPUT_VERTICES, "GL_TESS_CONTROL_OUTPUT_VERTICES"),

    /**Specifies the primitive generation mode used by Tessellation
     *
     * `GL_TESS_GEN_MODE`*///TODO: Enum for specifying the primitive generation mode's
    TessGenMode(GL40.GL_TESS_GEN_MODE, "GL_TESS_GEN_MODE"),

    /**Specifies the spacing for edge subdivision in Tessellation
     *
     * `GL_TESS_GEN_SPACING`*///TODO: Enum for Tessellation spacing modes
    TessGenSpacing(GL40.GL_TESS_GEN_SPACING, "GL_TESS_GEN_SPACING"),

    /**Specifies the vertex winding order for Tessellation
     *
     * `GL_TESS_GEN_VERTEX_ORDER`*///TODO: Enum for winding orders (GL_CW / GL_CCW)
    TessGenOrder(GL40.GL_TESS_GEN_VERTEX_ORDER, "GL_TESS_GEN_VERTEX_ORDER"),

    /**A flag specifying if Tessellation the primitive generator should emit points
     *
     * `GL_TESS_GEN_POINT_MODE`*/
    TessGenPointMode(GL40.GL_TESS_GEN_POINT_MODE, "GL_TESS_GEN_POINT_MODE"),

    /**An array of three [Int] values returning the local work group size as defined in the compute shader's layout qualifier
     *
     * `GL_COMPUTE_WORK_GROUP_SIZE`*/
    ComputeWorkSize(GL43.GL_COMPUTE_WORK_GROUP_SIZE, "GL_COMPUTE_WORK_GROUP_SIZE"),

    /**Buffer mode for transform feedback
     *
     * `GL_TRANSFORM_FEEDBACK_BUFFER_MODE`*///TODO: Enum transform feedback buffer mode
    TransformBuffMode(GL30.GL_TRANSFORM_FEEDBACK_BUFFER_MODE, "GL_TRANSFORM_FEEDBACK_BUFFER_MODE"),

    /**Count of varying variables to be captured in transform feedback mode.
     *
     * `GL_TRANSFORM_FEEDBACK_VARYINGS`*/
    TransformVaryingsCount(GL30.GL_TRANSFORM_FEEDBACK_VARYINGS, "GL_TRANSFORM_FEEDBACK_VARYINGS"),

    /**Length of the longest Varying Variable name present. If there are none, will return 0
     *
     * `GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH`*/
    MaxTransformVaryingNameSize(GL30.GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH, "GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH"),

    /**The byte size of this [ShaderProgram]'s binary representation. If the linking failed will return 0
     *
     * `GL_PROGRAM_BINARY_LENGTH`*/
    ByteSize(GL41.GL_PROGRAM_BINARY_LENGTH, "GL_PROGRAM_BINARY_LENGTH"),

    /**A flag specifying if [ShaderProgram] allows binary retrieval
     *
     * `GL_PROGRAM_BINARY_RETRIEVABLE_HINT`*/
    AllowBinRetrieve(GL41.GL_PROGRAM_BINARY_RETRIEVABLE_HINT, "GL_PROGRAM_BINARY_RETRIEVABLE_HINT"),

    /**A flag specifying if [ShaderProgram] can be bound into a program pipeline
     *
     * `GL_PROGRAM_SEPARABLE`*/
    AllowInPipeline(GL41.GL_PROGRAM_SEPARABLE, "GL_PROGRAM_SEPARABLE"),

    /**Count of Uniform Blocks in this [ShaderProgram]
     *
     * `GL_ACTIVE_UNIFORM_BLOCKS`*/
    UniformBlocksCount(GL31.GL_ACTIVE_UNIFORM_BLOCKS, "GL_ACTIVE_UNIFORM_BLOCKS"),

    /**Length of the longest Uniform Block name present. If there are none, will return 0
     *
     * `GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH`*/
    MaxUniformBlockNameSize(GL31.GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH, "GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH"),

    /**Count of [Atomic Counters][GLSLVar.AtomicUInt] in this [ShaderProgram]
     *
     * `GL_ACTIVE_ATOMIC_COUNTER_BUFFERS`*/
    AtomicCount(GL42.GL_ACTIVE_ATOMIC_COUNTER_BUFFERS, "GL_ACTIVE_ATOMIC_COUNTER_BUFFERS"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}