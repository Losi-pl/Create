package com.losi.create.graphics

import org.lwjgl.opengl.GL43.*

class ShaderCompilationError : RuntimeException {
    var shaderError: Error

    @Suppress("unused")
    constructor(shaderType: Int, content: String) {
        shaderError = Error.ShaderParts(listOf(Pair(shaderType, content)))
    }
    constructor(compilationErrors: List<Pair<Int, String>>) {
        shaderError = Error.ShaderParts(ArrayList(compilationErrors))
    }
    constructor(linkingError: String)
    {
        shaderError = Error.ShaderLinking(linkingError)
    }

    override val message: String get() {
        when (shaderError)
        {
            is Error.ShaderParts -> {
                var message = "Errors encountered during compilations of shaders:"
                (shaderError as Error.ShaderParts).errors.forEach {
                    message += '\n' + when(it.first) {
                        GL_VERTEX_SHADER -> "Vertex"
                        GL_TESS_CONTROL_SHADER -> "Tessellation Control"
                        GL_TESS_EVALUATION_SHADER -> "Tessellation Evaluation"
                        GL_GEOMETRY_SHADER -> "Geometry"
                        GL_FRAGMENT_SHADER -> "Fragment"
                        GL_COMPUTE_SHADER -> "Compute"
                        else -> "Unknown [0x%X] Type".format(it.first)
                    } + " Shader: \n    " + it.second.replace("\n", "\n    ")
                }
                return message
            }
            is Error.ShaderLinking -> {
                return "Errors encountered during linking of shaders:\n    " +
                (shaderError as Error.ShaderLinking).message.replace("\n", "\n    ")
            }
        }
    }

    sealed class Error
    {
        data class ShaderParts(var errors: List<Pair<Int, String>>) : Error()
        data class ShaderLinking(var message: String) : Error()
    }
}