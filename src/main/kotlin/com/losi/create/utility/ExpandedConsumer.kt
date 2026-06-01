package com.losi.create.utility

import java.util.function.Consumer

/**A collection of lambdas to be called collectively at once*/
class ExpandedConsumer<T> : Consumer<T>
{
    /**For asynchronous support*/
    private var sync = Any()
    /**All registered lambdas*/
    private var consumers: List<Consumer<T>> = mutableListOf<Consumer<T>>().calcify()

    /**Used to add a new lambda to the collection
     *
     * TODO: Make the method return it's lambda*/
    fun add(runnable: Consumer<T>) {
        synchronized(sync) {
            val temp = ArrayList(consumers)
            temp.add(runnable)
            consumers = temp.calcify()
        }
    }
    /**Removes a specific lambda from the collection
     * @return `true` if lambda was successfully removed*/
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