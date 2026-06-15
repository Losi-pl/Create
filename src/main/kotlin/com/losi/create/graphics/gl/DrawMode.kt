package com.losi.create.graphics.gl

import org.lwjgl.opengl.*

enum class DrawMode(val gl: Int, val glName: String) {
    /**Draws a single point for each vertex in the sequence.
     *
     * `GL_POINTS`*/
    Points(GL11.GL_POINTS, "GL_POINTS"),
    /**Draws line segments connecting pairs of vertices. If there's an odd number of vertices, the last one is ignored.
     *
     * `GL_LINES`*/
    Lines(GL11.GL_LINES, "GL_LINES"),
    /**Draws a continuous line from the first vertex to the last, connecting each vertex to the next.
     *
     * `GL_LINE_STRIP`*/
    LineStrip(GL11.GL_LINE_STRIP, "GL_LINE_STRIP"),
    /**Draws a continuous line with no end, connecting each vertex to the next.
     *
     * `GL_LINE_LOOP`*/
    LineLoop(GL11.GL_LINE_LOOP, "GL_LINE_LOOP"),
    /**Each vertex represents a line segment, with an additional "adjacent" vertex on each side. Used with geometry shaders for effects like thick lines
     *
     * `GL_LINES_ADJACENCY`*/
    LinesAdjacency(GL32.GL_LINES_ADJACENCY, "GL_LINES_ADJACENCY"),
    /**Builds a continuous line strip where each vertex also has two adjacent vertices. Used with geometry shaders for advanced line rendering.
     *
     * `GL_LINE_STRIP_ADJACENCY`*/
    LineStripAdjacency(GL32.GL_LINE_STRIP_ADJACENCY, "GL_LINE_STRIP_ADJACENCY"),
    /**Draws a triangle for each set of three vertices. If the vertex count is not a multiple of 3, the remaining vertices are ignored.
     *
     * `GL_TRIANGLES`*/
    Triangles(GL11.GL_TRIANGLES, "GL_TRIANGLES"),
    /**Creates a strip of connected triangles, where each new vertex forms a new triangle with the two previous vertices. This is highly efficient for building grid-based surfaces.
     *
     * `GL_TRIANGLE_STRIP`*/
    TriangleStrip(GL11.GL_TRIANGLE_STRIP, "GL_TRIANGLE_STRIP"),
    /**Creates a fan of triangles, all sharing a common starting vertex. Each subsequent set of two vertices forms a new triangle with the first vertex, ideal for drawing circles or sectors.
     *
     * `GL_TRIANGLE_FAN`*/
    TriangleFan(GL11.GL_TRIANGLE_FAN, "GL_TRIANGLE_FAN"),
    /**Each triangle is represented by 6 vertices (the 3 triangle vertices, plus an adjacent vertex for each edge). Used with geometry shaders for tessellation and shadows.
     *
     * `GL_TRIANGLES_ADJACENCY`*/
    TrianglesAdjacency(GL32.GL_TRIANGLES_ADJACENCY, "GL_TRIANGLES_ADJACENCY"),
    /**Builds a strip of triangles where each vertex has information about its adjacent vertices. Used with geometry shaders for advanced geometry processing.
     *
     * `GL_TRIANGLE_STRIP_ADJACENCY`*/
    TriangleStripAdjacency(GL32.GL_TRIANGLE_STRIP_ADJACENCY, "GL_TRIANGLE_STRIP_ADJACENCY"),
    /**A special mode for use with Tessellation Shaders. Each group of vertices forms a "patch" that is then subdivided into smaller primitives by the tessellation engine.
     *
     * `GL_PATCHES`*/
    Patches(GL40.GL_PATCHES, "GL_PATCHES"),
    /**Draws a quadrilateral for each set of four vertices. Deprecated in modern OpenGL (3.0+).
     *
     * `GL_QUADS`*/
    Quads(GL40.GL_QUADS, "GL_QUADS"),
    /**Creates a strip of connected quadrilaterals. Deprecated in modern OpenGL (3.0+).
     *
     * `GL_QUAD_STRIP`*/
    QuadStrip(GL40.GL_QUAD_STRIP, "GL_QUAD_STRIP"),
    /**Draws a single filled polygon using all given vertices. Deprecated in modern OpenGL (3.0+).
     *
     * `GL_POLYGON`*/
    Polygon(GL40.GL_POLYGON, "GL_POLYGON"),
    ;
    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(gl: Int) = entries.find { it.gl == gl }
    }
}