package com.losi.create.tests.math;

import com.losi.create.math.Vector2iArray;
import org.joml.Vector2i;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

public class CustomArrayTests {
    Vector2iArray array;

    @BeforeEach
    public void setUp()
    {
        array = Vector2iArray.of(10);
        array.set(0, new Vector2i(1, 2));
        array.set(1, new Vector2i(3, 4));
        array.set(2, new Vector2i(5, 6));
        array.set(3, new Vector2i(7, 8));
        array.set(4, new Vector2i(9, 10));
        array.set(5, new Vector2i(11, 12));
        array.set(6, new Vector2i(13, 14));
        array.set(7, new Vector2i(15, 16));
        array.set(8, new Vector2i(17, 18));
        array.set(9, new Vector2i(19, 20));
    }

    @Test
    public void PutInTakeOut()
    {
        var arr = Vector2iArray.of(4);
        arr.set(0, new Vector2i(0, 0));
        arr.set(1, new Vector2i(0, 1));
        arr.set(2, new Vector2i(1, 0));
        arr.set(3, new Vector2i(1, 1));

        assertEquals(new Vector2i(0, 0), arr.get(0));
        assertEquals(new Vector2i(0, 1), arr.get(1));
        assertEquals(new Vector2i(1, 0), arr.get(2));
        assertEquals(new Vector2i(1, 1), arr.get(3));

        var list = (List<Vector2i>)arr.asList();
        assertEquals(new Vector2i(0, 0), list.get(0));
        assertEquals(new Vector2i(0, 1), list.get(1));
        assertEquals(new Vector2i(1, 0), list.get(2));
        assertEquals(new Vector2i(1, 1), list.get(3));
    }

    @Test
    public void EnumerationAndToList()
    {
        var content = array.iterator().asStream();
        var enumeration = content.toList();
        for (int i = 0; i < enumeration.size(); i++)
            assertEquals(array.get(i), enumeration.get(i));

        var list = (List<Vector2i>)array.asList();
        for (int i = 0; i < enumeration.size(); i++)
            assertEquals(array.get(i), list.get(i));
    }

    @Test
    public void Modifiers()
    {
        var reversed = array.reversed();
        for (int i = 0; i < array.getSize(); i++)
            assertEquals(array.get(i), reversed.get(array.getSize() - i - 1));

        var filled = array.filled(new Vector2i(12, 12), 3, 6);
        for (int i = 0; i < array.getSize(); i++)
            if(3 <= i && i < 6)
                assertEquals(new Vector2i(12, 12), filled.get(i));
            else
                assertEquals(array.get(i), filled.get(i));
    }

    @Test
    public void Spans()
    {
        var subSpan = array.asList().subList(2, 7);

        for(int i = 0; i < 5; i++)
            assertEquals(array.get(2 + i), subSpan.get(i));

        assertEquals(2, subSpan.indexOf(new Vector2i(9, 10)));
    }
}
