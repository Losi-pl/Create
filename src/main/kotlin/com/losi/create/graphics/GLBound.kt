package com.losi.create.graphics

/**Used by classes that are serve as wrappers for resources in the OpenGL*/
interface GLBound {
    /**Dissolves the OpenGL resources bound to object implementing this interface*/
    fun release()
}