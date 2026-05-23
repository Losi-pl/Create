package com.losi.create.tests;

import com.losi.create.registry.*;
import org.junit.jupiter.api.Test;

import java.util.concurrent.*;

import static org.junit.jupiter.api.Assertions.*;

public class RegisterTest {

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
                element.register(testEle, "Abba:" + finalI);
                latch.countDown();
            });
        }
        var result = latch.await(30, TimeUnit.SECONDS);
        executor.shutdown();
        if(!result)
            fail("Timed out with: [" + element.getCount() + " / 10]");
        assertEquals(threadCount, element.getCount());
    }

}
