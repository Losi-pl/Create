package com.losi.create.utility;

import org.eclipse.collections.api.IntIterable;
import org.eclipse.collections.api.list.primitive.*;
import org.eclipse.collections.impl.list.mutable.primitive.FloatArrayList;
import org.eclipse.collections.impl.list.mutable.primitive.IntArrayList;

import java.nio.FloatBuffer;
import java.nio.IntBuffer;

@SuppressWarnings("unused")
class CrIntCollect {
    static IntIterable wrapper(int[] array) { return new IntArrayList(array); }
    static void addAll(MutableIntList list, int[] array) { list.addAll(array); }
    static void addAll(MutableFloatList list, float[] array) { list.addAll(array); }
    static void addAll(MutableIntList list, IntBuffer buffer) {
        if(list instanceof IntArrayList)
            ((IntArrayList) list).ensureCapacity(list.size() + buffer.remaining());
        while (buffer.hasRemaining())
            list.add(buffer.get());
    }
    static void addAll(MutableFloatList list, FloatBuffer buffer) {
        if(list instanceof FloatArrayList)
            ((FloatArrayList) list).ensureCapacity(list.size() + buffer.remaining());
        while (buffer.hasRemaining())
            list.add(buffer.get());
    }
}