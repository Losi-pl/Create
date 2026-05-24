package com.losi.create.utility

import java.util.concurrent.ConcurrentLinkedQueue
import java.util.concurrent.CountDownLatch

object OnMainThread
{
    private val queue: ConcurrentLinkedQueue<Runnable> = ConcurrentLinkedQueue()
    internal var mainThread: Thread? = null

    internal fun callAction(ignore: Float)
    {
        while (true) {
            val item = queue.poll() ?: break
            item.run()
        }
    }

    @JvmStatic
    fun schedule(action: Runnable) = queue.offer(action)

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

