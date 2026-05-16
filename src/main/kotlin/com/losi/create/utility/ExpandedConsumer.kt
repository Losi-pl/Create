package com.losi.create.utility

import java.util.function.Consumer

class ExpandedConsumer<T> : Consumer<T>
{
    private var sync = Any()
    private var consumers: List<Consumer<T>> = mutableListOf<Consumer<T>>().calcify()

    fun add(runnable: Consumer<T>) {
        synchronized(sync) {
            val temp = ArrayList(consumers)
            temp.add(runnable)
            consumers = temp.calcify()
        }
    }

    @Suppress("UNUSED")
    fun remove(runnable: Consumer<T>): Boolean {
        synchronized(sync) {
            val temp = ArrayList(consumers)
            val rez = temp.remove(runnable)
            consumers = temp.calcify()
            return rez
        }
    }

    override fun accept(value: T) {
        synchronized(sync) {
            for (runnable in consumers)
                runnable.accept(value)
        }
    }
}