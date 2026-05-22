package com.losi.create.tests;

import com.losi.create.Version;
import com.losi.create.graphics.*;
import org.joml.Vector3f;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.io.InputStream;

import static org.junit.jupiter.api.Assertions.*;

public class OpenGLTest
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
        window.setTitle(title_o);
        assertEquals(title_o, window.getTitle());

        try
        {
            var ico_o = Version.class.getModule().getResourceAsStream("Icon.ico");
            window.setIcon(ico_o);
            assertEquals(ico_o, window.getIcon());
        } catch (Exception e) { fail(e); }
    }

    @Test
    public void TestMesh()
    {
        Shader shaderProgram;
        try
        {
            InputStream vertex = null;
            try { vertex = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.vert"); }
            catch (IOException e) { fail("Failed to load vertex file"); }

            InputStream fragment = null;
            try { fragment = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.frag"); }
            catch (IOException e) { fail("Failed to load fragment file"); }

            InputStream xml = null;
            try { xml = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.xml"); }
            catch (IOException e) { fail("Failed to load xml file"); }

            try { shaderProgram = new Shader(vertex, fragment, xml); }
            catch (ShaderCompilationError ex) { fail(ex); return; }
        } catch (Exception e) { fail(e); return; }

        try
        {
            var mesh = new Mesh(shaderProgram);
            mesh.setAttribute("position", new Vector3f[]{
                    new Vector3f(-0.6f, -0.4f, 0f),
                    new Vector3f( 0.6f, -0.4f, 0f),
                    new Vector3f( 0f   , 0.6f, 0f)});
            mesh.setAttribute("color", new Vector3f[]{
                    new Vector3f(1f, 0f, 0f),
                    new Vector3f(0f, 1f, 0f),
                    new Vector3f(0f, 0f, 1f)});
            mesh.burnModel();
        } catch (Exception e) { fail(e); }
    }
}
