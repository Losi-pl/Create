package com.losi.create.graphics;

import org.joml.Vector2i;
import org.lwjgl.glfw.*;
import org.lwjgl.opengl.*;

import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.system.MemoryUtil.*;

public class Window {
    private static final ThreadLocal<Window> currentWindow = new ThreadLocal<>();
    private static boolean initialized = false;

    private transient long window = NULL;
    private boolean thread_bound = false;
    private String title = null;
    private Vector2i size = null;
    private Monitor monitor = null;
    private boolean vSync = false;

    public Window()
    {
        synchronized (currentWindow)
        {
            if(!initialized)
                initGL();
            initialized = true;
        }
    }

    private void initGL() {
        GLFWErrorCallback.createPrint(System.err).set();
        if ( !glfwInit() ) throw new IllegalStateException("Unable to initialize GLFW");
    }

    /*
    }*/

    public void run() {
        glfwShowWindow(window);
        glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

        glfwSwapInterval(vSync ? 1 : 0);
        while ( !glfwWindowShouldClose(window) ) {
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glfwSwapBuffers(window);
            glfwPollEvents();
        }
    }

    public void create()
    {
        glfwDefaultWindowHints();
        glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE);
        glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE);

        if(monitor == null)
            monitor = Monitor.list().getFirst();
        if(size == null)
            size = new Vector2i(monitor.width() * 2 / 3, monitor.height() * 2 / 3);

        window = glfwCreateWindow(size.x, size.y, title == null ? "null" : title, NULL, NULL);
        if ( window == NULL )
            throw new RuntimeException("Failed to create the GLFW window");
        else
        {
            var pos = monitor.position();
            var work = monitor.workArea();

            int windowX = pos.x() + (work.width() - size.x) / 2;
            int windowY = pos.y() + (work.height() - size.y) / 2;

            glfwSetWindowPos(window, windowX, windowY);
        }

        /*// Setup a key callback. It will be called every time a key is pressed, repeated or released.
        glfwSetKeyCallback(window, (wind, key, scancode, action, mods) -> {
            if ( key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE )
                glfwSetWindowShouldClose(wind, true); // We will detect this in the rendering loop
        });*/
    }

    public void threadBind()
    {
        synchronized (currentWindow)
        {
            if(thread_bound)
                throw new IllegalStateException("Window is already bound to a Thread");
            if(currentWindow.get() != null)
                throw new IllegalStateException("Thread is already bound to a Window");
            currentWindow.set(this);
            glfwMakeContextCurrent(window);
            GL.createCapabilities();
            thread_bound = true;
        }
    }
}
