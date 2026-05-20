package com.losi.create.utility

import java.util.concurrent.ConcurrentLinkedQueue
import java.util.concurrent.CountDownLatch

object OnMainThread
{
    private val queue: ConcurrentLinkedQueue<() -> Unit> = ConcurrentLinkedQueue()

    internal fun callAction(@Suppress("unused") ignore: Float)
    {
        while (true) {
            val item = queue.poll() ?: break
            item()
        }
    }

    @JvmStatic
    fun schedule(action: () -> Unit) = queue.offer(action)

    @JvmStatic @Suppress("unused")
    inline fun <T> schedule(crossinline action: () -> T): T
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

