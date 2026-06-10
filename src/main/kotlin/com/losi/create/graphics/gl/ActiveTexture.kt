package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class ActiveTexture {
    Texture0 ("GL_TEXTURE0"),
    Texture1 ("GL_TEXTURE1"),
    Texture2 ("GL_TEXTURE2"),
    Texture3 ("GL_TEXTURE3"),
    Texture4 ("GL_TEXTURE4"),
    Texture5 ("GL_TEXTURE5"),
    Texture6 ("GL_TEXTURE6"),
    Texture7 ("GL_TEXTURE7"),
    Texture8 ("GL_TEXTURE8"),
    Texture9 ("GL_TEXTURE9"),
    Texture10("GL_TEXTURE10"),
    Texture11("GL_TEXTURE11"),
    Texture12("GL_TEXTURE12"),
    Texture13("GL_TEXTURE13"),
    Texture14("GL_TEXTURE14"),
    Texture15("GL_TEXTURE15"),
    Texture16("GL_TEXTURE16"),
    Texture17("GL_TEXTURE17"),
    Texture18("GL_TEXTURE18"),
    Texture19("GL_TEXTURE19"),
    Texture20("GL_TEXTURE20"),
    Texture21("GL_TEXTURE21"),
    Texture22("GL_TEXTURE22"),
    Texture23("GL_TEXTURE23"),
    Texture24("GL_TEXTURE24"),
    Texture25("GL_TEXTURE25"),
    Texture26("GL_TEXTURE26"),
    Texture27("GL_TEXTURE27"),
    Texture28("GL_TEXTURE28"),
    Texture29("GL_TEXTURE29"),
    Texture30("GL_TEXTURE30"),
    Texture31("GL_TEXTURE31"),
    ;

    val glName: String

    constructor(name: String) {
        this.glName = name
    }

    val gl = GL13.GL_TEXTURE0 + ordinal

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.*/
        fun of(gl: Int): ActiveTexture? {
            return if(gl in GL13.GL_TEXTURE0..GL13.GL_TEXTURE31)
                entries[gl - GL13.GL_TEXTURE0]
            else
                null
        }

        operator fun get(index: Int): ActiveTexture {
            return if(index in 0..31)
                entries[index]
            else
                throw IndexOutOfBoundsException()
        }
    }
}