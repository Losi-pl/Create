package com.losi.create.tests;

import com.losi.create.internal.InternalGameElement;
import com.losi.create.registry.GameElement;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.fail;

public class GameElementTest {


    @Test
    public void gettingAtInternals()
    {
        try
        {
            var test_elem = new GameElement() { };
            @SuppressWarnings("unused")
            var rez = InternalGameElement.of(test_elem);
        }
        catch (Exception e)
        {
            fail(e.getMessage());
        }
    }
}
