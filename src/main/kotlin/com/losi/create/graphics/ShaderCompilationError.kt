package com.losi.create.graphics

import com.losi.create.graphics.gl.ShaderType

/**An Exception used when the compilation or linking of a Shader failed*/
class ShaderCompilationError : RuntimeException {
    var shaderError: Error

    /**When only one part of a Shader failed to compile properly*/
    @Suppress("unused")
    constructor(shaderType: ShaderType, content: String) {
        shaderError = Error.ShaderParts(listOf(Pair(shaderType, content)))
    }
    /**A list of multiple failers to properly compile shader part's*/
    constructor(compilationErrors: List<Pair<ShaderType, String>>) {
        shaderError = Error.ShaderParts(ArrayList(compilationErrors))
    }
    /**An error with liking of the shader parts into a single program*/
    constructor(linkingError: String) {
        shaderError = Error.ShaderLinking(linkingError)
    }

    override val message: String get() {
        when (shaderError)
        {
            is Error.ShaderParts -> {
                var message = "Errors encountered during compilations of shaders:"
                (shaderError as Error.ShaderParts).errors.forEach {
                    message += '\n' + when(it.first) {
                        ShaderType.Vertex -> "Vertex"
                        ShaderType.Fragment -> "Fragment"
                        ShaderType.Geometry -> "Geometry"
                        ShaderType.TessControl -> "Tessellation Control"
                        ShaderType.TessEvaluation -> "Tessellation Evaluation"
                        ShaderType.Compute -> "Compute"
                        ShaderType.Mesh -> "Mesh"
                        ShaderType.Task -> "Task"
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

    /**Information about the specific error of this exception*/
    sealed class Error
    {
        /**Errors in compilation of Shaders Part's*/
        data class ShaderParts(var errors: List<Pair<ShaderType, String>>) : Error()
        /**Error in linking of Shader Part's*/
        data class ShaderLinking(var message: String) : Error()
    }
}