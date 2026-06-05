package com.losi.create.utility

import java.io.Serializable

data class MutablePair<A, B>(var first: A, var second: B) : Serializable
{
    override fun toString() = "($first, $second)"
}