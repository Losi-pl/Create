package com.losi.create.graphics;

import org.jetbrains.annotations.Contract;
import org.jetbrains.annotations.NotNull;
import org.joml.Vector2i;
import org.lwjgl.glfw.GLFW;
import org.lwjgl.glfw.GLFWVidMode;
import org.lwjgl.system.MemoryStack;

import java.nio.IntBuffer;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import static org.lwjgl.glfw.GLFW.*;

@SuppressWarnings("unused")
public class Monitor
{
    private static List<Monitor> monitors = null;
    private static List<Monitor> readOnlyMonitors = null;

    private final long handler;
    GLFWVidMode vidmode;
    String name;

    private Monitor(long handler)
    {
        this.handler = handler;
    }

    public static List<Monitor> list() {
        if(readOnlyMonitors == null)
            load();
        return readOnlyMonitors;
    }

    public int width() {
        if(vidmode == null)
            load_monitor();
        return vidmode.width();
    }

    public int height() {
        if(vidmode == null)
            load_monitor();
        return vidmode.height();
    }

    public int framerate() {
        if(vidmode == null)
            load_monitor();
        return vidmode.refreshRate();
    }

    public Vector2i position() {
        int[] monitorX = new int[1], monitorY = new int[1];
        glfwGetMonitorPos(handler, monitorX, monitorY);
        return new Vector2i(monitorX[0], monitorY[0]);
    }

    public static class WorkArea {
        private int x, y, width, height;

        public int getX() { return x; }
        public int getY() { return y; }

        public int getWidth() { return width; }
        public int getHeight() { return height; }

        @Contract(value = " -> new", pure = true)
        public @NotNull Vector2i position() { return new Vector2i(x, y); }
        @Contract(value = " -> new", pure = true)
        public @NotNull Vector2i size() { return new Vector2i(width, height); }
        static final ArrayDeque<WorkArea> pool = new ArrayDeque<>();
        public static @NotNull WorkArea pull() {
            synchronized (pool) { return pool.isEmpty() ? new WorkArea() : pool.pop(); }
        }
        public static @NotNull WorkArea pull(int x, int y, int width, int height) {
            var obj = pull();
            obj.x = x; obj.y = y; obj.width = width; obj.height = height;
            return obj;
        }
        public void release() {
            synchronized (pool) {pool.addLast(this); }
        }
    }
    public WorkArea workArea()
    {
        try (var stack = MemoryStack.stackPush())
        {
            IntBuffer posX = stack.mallocInt(1), posY = stack.mallocInt(1);
            IntBuffer workWidth = stack.mallocInt(1), workHeight = stack.mallocInt(1);
            GLFW.glfwGetMonitorWorkarea(handler, posX, posY, workWidth, workHeight);
            return WorkArea.pull(posX.get(), posY.get(), workWidth.get(), workHeight.get());
        }

    }

    public String name() {
        if(name == null)
            name = glfwGetMonitorName(handler);
        return name;
    }

    public long handler()
    { return handler; }

    private void load_monitor()
    { vidmode = glfwGetVideoMode(handler); }

    private static void load()
    {
        monitors = Collections.synchronizedList(new ArrayList<>());
        readOnlyMonitors = Collections.unmodifiableList(monitors);
        var monitors = glfwGetMonitors();
        assert monitors != null;
        for (int i = 0; i < monitors.limit(); i++)
            Monitor.monitors.add(new Monitor(monitors.get(i)));
    }
}
