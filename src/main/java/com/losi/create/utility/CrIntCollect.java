package com.losi.create.utility;

import org.eclipse.collections.api.IntIterable;
import org.eclipse.collections.api.list.primitive.*;
import org.eclipse.collections.impl.list.mutable.primitive.IntArrayList;

@SuppressWarnings("unused")
class CrIntCollect {
    static IntIterable wrapper(int[] array) { return new IntArrayList(array); }
    static void addAll(MutableIntList list, int[] array) { list.addAll(array); }
}