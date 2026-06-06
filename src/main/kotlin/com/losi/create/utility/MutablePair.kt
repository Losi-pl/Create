package com.losi.create.utility

import java.io.Serializable

data class MutablePair<out A, out B>(
    var first: @UnsafeVariance A,
    var second: @UnsafeVariance B) : Serializable {

    /**
     * Returns string representation of the [MutablePair] including its [first] and [second] values.
     */
    override fun toString() = "($first, $second)"
}

