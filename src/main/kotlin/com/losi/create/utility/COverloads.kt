package com.losi.create.utility

import com.koloboke.collect.set.LongSet

internal fun LongSet.forceAdd(value: Long) = COverloads.add(this, value)