@file:Suppress("unused")

package com.losi.create.utility

import java.util.function.Consumer

/**A collection of lambdas to be called collectively at once*/
class ExpandedConsumer<T> : Consumer<T>
{
    /**For asynchronous support*/
    private var sync = Any()
    /**All registered lambdas*/
    private var consumers: List<Consumer<T>> = mutableListOf<Consumer<T>>().calcify()

    /**Used to add a new lambda to the collection*/
    @com.google.errorprone.annotations.CanIgnoreReturnValue
    fun add(runnable: Consumer<T>): Consumer<T> {
        synchronized(sync) {
            val temp = ArrayList(consumers)
            temp.add(runnable)
            consumers = temp.calcify()
        }
        return runnable
    }
    /**Removes a specific lambda from the collection
     * @return `true` if lambda was successfully removed*/
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