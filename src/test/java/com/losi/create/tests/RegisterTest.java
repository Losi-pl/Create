package com.losi.create.tests;

import com.losi.create.ModSpace;
import com.losi.create.registry.*;
import kotlin.jvm.JvmClassMappingKt;
import org.junit.jupiter.api.Test;

import java.util.concurrent.*;

import static org.junit.jupiter.api.Assertions.*;

public class RegisterTest {

    static class TestElement extends GameElement { }

    @Test
    void concurrentCommandsShouldBeThreadSafe() throws InterruptedException {
        ElementRegister<TestElement> element = new ElementRegister<>(JvmClassMappingKt.getKotlinClass(TestElement.class));
        int threadCount = 10;
        ExecutorService executor = Executors.newFixedThreadPool(threadCount);
        CountDownLatch latch = new CountDownLatch(threadCount);

        for (int i = 0; i < threadCount; i++) {
            int finalI = i;
            TestElement testEle = new TestElement();
            executor.submit(() -> {
                element.register(testEle, ModSpace.Companion.getModules().get("create") ,"Abba:" + finalI);
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
