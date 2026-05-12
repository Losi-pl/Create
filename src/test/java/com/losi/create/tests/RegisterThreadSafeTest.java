package com.losi.create.tests;

import com.losi.create.registry.ElementRegister;
import com.losi.create.registry.GameElement;
import org.junit.jupiter.api.Test;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;

public class RegisterThreadSafeTest {

    @Test
    void concurrentCommandsShouldBeThreadSafe() throws InterruptedException {
        ElementRegister<GameElement> element = new ElementRegister<>();
        int threadCount = 10;
        ExecutorService executor = Executors.newFixedThreadPool(threadCount);
        CountDownLatch latch = new CountDownLatch(threadCount);

        for (int i = 0; i < threadCount; i++) {
            int finalI = i;
            GameElement testEle = new GameElement() { };
            executor.submit(() -> {
                element.register(testEle, "abba:" + finalI);
                latch.countDown();
            });
        }
        var rezult = latch.await(30, TimeUnit.SECONDS);
        executor.shutdown();
        if(!rezult)
            fail("Timed out with: [" + element.count() + " / 10]");
        assertEquals(threadCount, element.count());
    }

}
