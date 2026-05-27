package com.losi.create.utility;

import com.koloboke.collect.set.LongSet;
import org.jetbrains.annotations.NotNull;

class COverloads {
    /**A last resort way to solve the problem in Kotlin where the {@link LongSet} has two methods:
     * <p>{@link LongSet#add(long)}, {@link LongSet#add(Long)}</p>
     * Which in kotlin come down to the same thing as it does not make a distinction between primitive
     * types and their object variants. Due to that whe compiler will refuze to chose one and will throw an error no matter what*/
    public static boolean add(@NotNull LongSet set, long value) { return set.add(value); }
}
