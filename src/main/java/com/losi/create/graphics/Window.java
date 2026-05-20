package com.losi.create.graphics;

import com.losi.create.internal.GLErrorHandler;
import com.losi.create.utility.ExpandedConsumer;
import org.jetbrains.annotations.NotNull;
import org.joml.Matrix4f;
import org.joml.Vector2i;
import org.joml.Vector3f;
import org.lwjgl.glfw.*;
import org.lwjgl.opengl.GL;
import org.lwjgl.system.MemoryUtil;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferInt;
import java.io.IOException;
import java.io.InputStream;
import java.lang.ref.Cleaner;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.util.ArrayList;
import java.util.function.Consumer;

import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.opengl.GL30.*;
import static org.lwjgl.system.MemoryUtil.*;

//TODO: Kotlinify
@SuppressWarnings("unused")
public class Window {
    private static final ThreadLocal<Window> currentWindow = new ThreadLocal<>();
    private static final Cleaner cleaner = Cleaner.create();
    private static boolean initialized = false;

    private static final int[] ICON_SIZES = {16, 32, 48, 64, 128, 256};

    private transient final Object sync = new Object();
    private transient volatile long window = NULL;
    @SuppressWarnings({"FieldCanBeLocal"})
    private transient Cleaner.Cleanable handleDestroyer;
    private volatile boolean thread_bound = false;
    private volatile String title = null;
    private volatile Vector2i size = null;
    private volatile Monitor monitor = null;
    private volatile boolean vSync = false;
    private volatile InputStream icon;
    private volatile int targetFPS = 60;
    private volatile ExpandedConsumer<Float> logicUpdate;

    public Window()
    {
        synchronized (currentWindow)
        {
            if(!initialized)
                initGL();
            initialized = true;
        }
    }

    public String title() { return title; }
    public String title(String _new) {
        title = _new;
        if(window != NULL)
            glfwSetWindowTitle(window, title == null ? "" : title);
        return title;
    }

    public boolean vSync() { return vSync; }
    public boolean vSync(boolean _new) { vSync = _new; return vSync; }

    public InputStream icon() { return icon; }
    public InputStream icon(InputStream _new) {
        synchronized (sync)
        {
            icon = _new;
            if(icon != null && window != NULL)
                loadIcon(icon);
            return icon;
        }
    }

    public int targetFPS() { return targetFPS; }
    public int targetFPS(int _new) { targetFPS = _new; return targetFPS; }

    public void registerLogic(Consumer<Float> logic)
    { logicUpdate.add(logic); }

    private void initGL() {
        GLFWErrorCallback.createPrint(System.err).set();
        if ( !glfwInit() ) throw new IllegalStateException("Unable to initialize GLFW");
    }
    private void loadIcon(@NotNull InputStream icon) {
        BufferedImage image;
        try { image = ImageIO.read(icon); }
        catch (IOException e) { throw  new RuntimeException("Unable to parse icon", e); }
        if(image == null)
            throw  new RuntimeException("Unable to parse icon");

        var buffers = new ArrayList<ByteBuffer>();
        try
        {
            for (var SIZE : ICON_SIZES) {
                if(SIZE > image.getWidth() || SIZE > image.getHeight())
                    continue;

                var pixBuff = (DataBufferInt)scaleImage(image, SIZE).getRaster().getDataBuffer();

                ByteBuffer buffer = memAlloc(SIZE * SIZE * 4);
                buffers.add(buffer);
                buffer.order(ByteOrder.nativeOrder());

                for (var pixel: pixBuff.getData())
                {
                    buffer.put((byte) (pixel & 0xFF));          //R
                    buffer.put((byte) ((pixel >> 8) & 0xFF));   //G
                    buffer.put((byte) ((pixel >> 16) & 0xFF));  //B
                    buffer.put((byte) ((pixel >> 24) & 0xFF));  //A
                }
                buffer.flip();
            }
            var images = new ArrayList<GLFWImage>();
            try
            {
                for (var buffer: buffers)
                {
                    var img = GLFWImage.malloc();
                    images.add(img);
                    var size = (int)Math.sqrt((double) buffer.capacity() / 4);
                    img.set(size, size, buffer);
                }

                try (var set = GLFWImage.malloc(buffers.size()))
                {
                    for (int i = 0; i < buffers.size(); i++)
                        set.put(i, images.get(i));
                    glfwSetWindowIcon(window, set);
                }
            }
            finally {
                images.forEach(GLFWImage::free);
            }
        }
        finally {
            buffers.forEach(MemoryUtil::memFree);
        }
    }
    private static @NotNull BufferedImage scaleImage(@NotNull BufferedImage image, int size) {
        if(image.getWidth() == size && image.getHeight() == size)
            return image;
        var scaled = image.getScaledInstance(size, size, Image.SCALE_SMOOTH);
        var result = new BufferedImage(size, size, BufferedImage.TYPE_INT_ARGB);
        var graphic = result.createGraphics();
        graphic.drawImage(scaled, 0, 0, null);
        graphic.dispose();
        return result;
    }
    private void onResize(int width, int height) {
        glViewport(0, 0, width, height);
        size.x = width; size.y = height;
    }

    @SuppressWarnings({"BusyWait", "ConstantConditions"})
    public void run() {
        glfwShowWindow(window);
        glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

        var timer = new Timer();
        long targetTime = 1000L / targetFPS;

        Shader shaderProgram;
        {
            InputStream vertex;
            try { vertex = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.vert"); }
            catch (IOException e) { vertex = null; }

            InputStream fragment;
            try { fragment = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.frag"); }
            catch (IOException e) { fragment = null; }

            InputStream xml;
            try { xml = Window.class.getModule().getResourceAsStream("assets/create/shaders/basic.xml"); }
            catch (IOException e) { xml = null; }

            try { shaderProgram = new Shader(vertex, fragment, xml); }
            catch (ShaderCompilationError ex) { IO.println(ex.getMessage()); return; }
        }

        shaderProgram.setUniform("model", new Matrix4f());
        shaderProgram.setUniform("view", new Matrix4f());
        float ratio = 640f / 480f;
        shaderProgram.setUniform("projection", new Matrix4f().setOrtho(-ratio, ratio, -1f, 1f, -1f, 1f));

        Mesh mesh = new Mesh(shaderProgram);
        mesh.setAttribute("position", new Vector3f[]{
                new Vector3f(-0.6f, -0.4f, 0f),
                new Vector3f( 0.6f, -0.4f, 0f),
                new Vector3f( 0f   , 0.6f, 0f)});
        mesh.setAttribute("color", new Vector3f[]{
                new Vector3f(1f, 0f, 0f),
                new Vector3f(0f, 1f, 0f),
                new Vector3f(0f, 0f, 1f)});
        mesh.burnModel();

        glfwSwapInterval(vSync ? 1 : 0);
        while ( !glfwWindowShouldClose(window) ) {
            var startTime = timer.getLongTime();
            float delta = timer.getDelta();

            logicUpdate.accept(delta);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

            mesh.draw();

            glfwSwapBuffers(window);
            glfwPollEvents();

            var endTime = timer.getLongTime();
            var timeOut = startTime + targetTime - endTime;
            if(timeOut > 0)
                try { Thread.sleep(timeOut); }
                catch (InterruptedException ignored) { }
        }
    }
    public void create() {
        synchronized (sync)
        {
            if(window != NULL)
                return;

            glfwDefaultWindowHints();
            glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE);
            glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE);

            if(monitor == null)
                monitor = Monitor.list().getFirst();
            if(size == null)
                size = new Vector2i(monitor.width() * 2 / 3, monitor.height() * 2 / 3);

            window = glfwCreateWindow(size.x, size.y, title == null ? "" : title, NULL, NULL);
            if ( window == NULL )
                throw new RuntimeException("Failed to create the GLFW window");
        }

        final var handler = window;
        handleDestroyer = cleaner.register(this, () -> glfwDestroyWindow(handler));
        logicUpdate = new ExpandedConsumer<>();
        var pos = monitor.position();
        var work = monitor.workArea();
        glfwSetWindowPos(window,
                pos.x() + (work.getWidth() - size.x) / 2,
                pos.y() + (work.getHeight() - size.y) / 2);
        work.release();

        if(icon != null)
            loadIcon(icon);

        glfwSetFramebufferSizeCallback(window ,(window, width, height) -> this.onResize(width, height));
        /*// glfwSetKeyCallback(window, (wind, key, scancode, action, mods) -> {
            if ( key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE )
                glfwSetWindowShouldClose(wind, true); // We will detect this in the rendering loop
        });*/
    }
    public void close() {
        if (window != NULL)
            glfwWindowShouldClose(window);
    }
    public void threadBind() {
        synchronized (currentWindow)
        {
            if(thread_bound)
                throw new IllegalStateException("Window is already bound to a Thread");
            if(currentWindow.get() != null)
                throw new IllegalStateException("Thread is already bound to a Window");
            if(window == NULL)
                throw new IllegalStateException("The window was not yet created");
            currentWindow.set(this);
            glfwMakeContextCurrent(window);
            GL.createCapabilities();
            glViewport(0, 0, size.x, size.y);
            GLErrorHandler.bindErrorCather();

            thread_bound = true;
        }
    }



    static class Timer{
        double lastLoopTime;
        float timeCount;

        public double getTime() { return glfwGetTime(); }
        public long getLongTime(){ return (long)getTime() * 1000; }
        public void init() { lastLoopTime = getTime(); }
        public float getDelta() {
            double time = getTime();
            float delta = (float) (time - lastLoopTime);
            lastLoopTime = time;
            timeCount += delta;
            return delta;
        }

    }
}
