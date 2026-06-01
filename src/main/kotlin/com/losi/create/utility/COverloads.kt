package com.losi.create.utility

import com.koloboke.collect.set.LongSet

/**Adds the [value] to the set
 *
 * Apparently the interface has two methods that in the Kotlin interpretation come down to the same thing but
 * instead of just choosing one over the other, Kotlin will refuse to choose one and return a compilation error.*/
internal fun LongSet.forceAdd(value: Long) = COverloads.add(this, value)