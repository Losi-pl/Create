@file:Suppress("unused")
package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class ShaderType(val gl: Int, glName: String) {
    /**Processes each vertex. This is the entry point for vertex data.
     *
     * `GL_VERTEX_SHADER`*/
    Vertex(GL20.GL_VERTEX_SHADER, "GL_VERTEX_SHADER"),
    /**Processes each fragment (pixel), determining the final color.
     *
     * `GL_FRAGMENT_SHADER`*/
    Fragment(GL20.GL_FRAGMENT_SHADER, "GL_FRAGMENT_SHADER"),
    /**Runs after the vertex shader, capable of generating new geometry on the fly, such as expanding a point into a line or amplifying objects.
     *
     * `GL_GEOMETRY_SHADER`*/
    Geometry(GL32.GL_GEOMETRY_SHADER, "GL_GEOMETRY_SHADER"),
    /**The first part of the tessellation pipeline. It defines a patch's control points and sets tessellation levels.
     *
     * `GL_TESS_CONTROL_SHADER`*/
    TessControl(GL40.GL_TESS_CONTROL_SHADER, "GL_TESS_CONTROL_SHADER"),
    /**The second part of the tessellation pipeline. It takes the tessellated data and computes final vertex positions, like converting a bezier patch to triangles.
     *
     * `GL_TESS_EVALUATION_SHADER`*/
    TessEvaluation(GL40.GL_TESS_EVALUATION_SHADER, "GL_TESS_EVALUATION_SHADER"),
    /**A shader for general-purpose computing (GPGPU) that operates in parallel on data.
     *
     * `GL_COMPUTE_SHADER`*/
    Compute(GL43.GL_COMPUTE_SHADER, "GL_COMPUTE_SHADER"),
    /**A newer, advanced shader designed to eventually replace the Vertex and Tessellation stages. It defines workloads for a mesh shader.
     *
     * `GL_MESH_SHADER_EXT`*/
    Mesh(EXTMeshShader.GL_MESH_SHADER_EXT, "GL_MESH_SHADER_EXT"),
    /**An optional part of the mesh shader pipeline, used to generate work for mesh shaders.
     *
     * `GL_TASK_SHADER_EXT`*/
    Task(EXTMeshShader.GL_TASK_SHADER_EXT, "GL_TASK_SHADER_EXT"),
    ;

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}