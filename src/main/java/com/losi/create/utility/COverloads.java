package com.losi.create.utility;

import com.koloboke.collect.set.LongSet;
import org.jetbrains.annotations.NotNull;

class COverloads {
    public static void add(@NotNull LongSet set, long value) { set.add(value); }
}
