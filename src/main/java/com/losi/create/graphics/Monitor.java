package com.losi.create.graphics;

import org.jetbrains.annotations.Contract;
import org.jetbrains.annotations.NotNull;
import org.joml.Vector2i;
import org.lwjgl.glfw.GLFW;
import org.lwjgl.glfw.GLFWVidMode;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import static org.lwjgl.glfw.GLFW.*;

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
        org.lwjgl.glfw.GLFW.glfwGetMonitorPos(handler, monitorX, monitorY);
        return new Vector2i(monitorX[0], monitorY[0]);
    }

    public record WorkArea(int x, int y, int width, int height) {
        @Contract(value = " -> new", pure = true)
        public @NotNull Vector2i position() { return new Vector2i(x, y); }
        @Contract(value = " -> new", pure = true)
        public @NotNull Vector2i size() { return new Vector2i(width, height); }
    }
    public WorkArea workArea()
    {
        int[] posX = new int[1], posY = new int[1];
        int[] workWidth = new int[1], workHeight = new int[1];
        GLFW.glfwGetMonitorWorkarea(handler, posX, posY, workWidth, workHeight);
        return new WorkArea(posX[0], posY[0], workWidth[0], workHeight[0]);
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
