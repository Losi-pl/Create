package com.losi.create.utility

import java.util.concurrent.ConcurrentLinkedQueue
import java.util.concurrent.CountDownLatch

/**Operations performed on the main thread of the application*/
object OnMainThread
{
    /**The queue of methods to me performed in the main thread*/
    private val queue: ConcurrentLinkedQueue<Runnable> = ConcurrentLinkedQueue()
    /**Main [Thread] of the application*/
    internal var mainThread: Thread? = null

    /**In this method lambdas from the [queue] are actually called
     * Loops through the queue until it's empty*/
    internal fun callAction(@Suppress("unused") ignore: Float) {
        while (true) {
            val item = queue.poll() ?: break
            item.run()
        }
    }
    /**Test if the caller of the method in calling from the main thread*/
    @JvmStatic
    fun isMain(): Boolean = mainThread == Thread.currentThread()
    /**Registers a lambda to be executed in the main thread
     *
     * Will not wait for the lambda to be finished*/
    @JvmStatic
    fun schedule(action: Runnable) = queue.offer(action)
    /**Registers a lambda to be executed in the main thread
     *
     * Will wait until the lambda is done before continuing*/
    @JvmStatic @Suppress("unused")
    inline fun <T> query(crossinline action: () -> T): T
    {
        val latch = CountDownLatch(1)
        var result: T? = null
        schedule {
            result = action()
            latch.countDown()
        }
        latch.await()
        return result!!
    }
}

