package com.losi.create.tests;

import com.losi.create.Version;
import com.losi.create.graphics.Window;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.io.IOException;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;

public class WindowTest
{
    static Window window = null;

    @BeforeAll
    public static void CreateWindow()
    {
        window = new Window();
        window.create();
        window.threadBind();
    }

    @Test
    public void SilenceWarnings()
    {
        var title_o = "Test Create";
        var title_r = window.title(title_o);
        assertEquals(title_o, title_r);

        try
        {
            var ico_o = Version.class.getModule().getResourceAsStream("Icon.ico");
            var ico_r = window.icon(ico_o);
            assertEquals(ico_o, ico_r);
        }
        catch (IOException e)
        { fail(e); }
    }

}
