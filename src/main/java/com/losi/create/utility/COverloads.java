package com.losi.create.utility;

import com.koloboke.collect.set.LongSet;
import org.jetbrains.annotations.NotNull;

class COverloads {
    public static boolean add(@NotNull LongSet set, long value) { return set.add(value); }
}
