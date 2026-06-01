package com.losi.create.graphics

import com.losi.create.internal.GLErrorHandler
import com.losi.create.utility.ExpandedConsumer
import org.joml.*
import org.lwjgl.glfw.*
import org.lwjgl.opengl.GL
import org.lwjgl.system.MemoryUtil
import java.awt.*
import java.awt.image.*
import java.io.*
import java.lang.ref.Cleaner
import java.nio.*
import java.util.ArrayList
import org.lwjgl.glfw.GLFW.*
import org.lwjgl.opengl.GL30.*
import org.lwjgl.system.MemoryUtil.*

/**An OpenGL Window and a Context, contains mechanisms for interaction with the user*/
class Window: InternalGLContext {
    companion object {
        /**A lock for better synchronization between OpenGL context's and [Thread]'s*/
        internal var currentContext = ThreadLocal<InternalGLContext>()
        /**For ensuring that there are no free floating unused windows*/
        private var cleaner = Cleaner.create()
        /**A flag, is OpenGL was initialized already*/
        private var initialized = false
        /**List of recommend sized of an icon*/
        private var ICON_SIZES = listOf(16, 32, 48, 64, 128, 256)
        /**Takes in an [BufferedImage] and returns a new version of it resized to [size]x[size]
         * @param image An initial image to be resized
         * @param size Expected size on an image
         * @return If the input image is already of an expected size the original will be returned, otherwise a new instance will be created*/
        private fun scaleImage(image: BufferedImage, size: Int): BufferedImage {
            if(image.width == size && image.height == size)
                return image
            val scaled = image.getScaledInstance(size, size, Image.SCALE_SMOOTH)
            val result = BufferedImage(size, size, BufferedImage.TYPE_INT_ARGB)
            val graphic = result.createGraphics()
            graphic.drawImage(scaled, 0, 0, null)
            graphic.dispose()
            return result
        }
    }

    /**For some flimsy thread synchronization*/
    private val sync = Any()
    /**Handler for the OpenGL instance of the window*/
    private var window: Long = NULL
    /**Clearer operation of the window to ensure that the OpenGL instance will not float untethered to anything*/
    @Suppress("FieldCanBeLocal") @Transient
    private lateinit var handleDestroyer: Cleaner.Cleanable
    /**A flag, is this window already connected to a [Thread]*/
    private var threadBound = false
    /**Title of this window*/
    private var _title: String? = null
    /**Size of this window*/
    private var size: Vector2i? = null
    /**The monitor on which the window was set to be present*/
    private var monitor: Monitor? = null
    /**A flag, is the V-Sync mechanism enabled*/
    private var _vSync = false
    /**The stream to the icon of this window*/
    private var _icon: InputStream? = null
    /**Targeted frame rate for this window*/
    private var _targetFPS = 60
    /**A collective lambda that it called every frame*/
    private val logicUpdate = ExpandedConsumer<Float>()

    /**The standard constructor for the Window
     * Will ensure that OpenGL is initiated before finishing construction*/
    constructor() {
        synchronized (currentContext)
        {
            if(!initialized)
                initGL()
            initialized = true
        }
    }
    /**Title of this window*/
    var title: String? get() { return _title; } set(it) {
        _title = it
        if(window != NULL)
            glfwSetWindowTitle(window, _title ?: "")
    }
    /**A flag, is V-Sync mechanism enabled*/
    var vSync: Boolean get() = _vSync; set(it) { _vSync = it }
    /**An [InputStream] to the icon currently set in this window*/
    var icon: InputStream? get() = _icon; set(it) = synchronized (sync) {
        _icon = it
        if(_icon != null && window != NULL)
            loadIcon(_icon!!)
    }
    /**The targeted frame rate of the window*/
    var targetFPS: Int get() = _targetFPS; set(it) { _targetFPS = it }
    /**The handler of this window*/
    internal val handle: Long get() = window

    /**Registers a lambda to be executed every frame as a part of logic update
     * @param logic Logic lambda*/
    fun registerLogic(logic: java.util.function.Consumer<Float>)
    { logicUpdate.add(logic); }

    /**Initializes the GLFW library*/
    private fun initGL() {
        GLFWErrorCallback.createPrint(System.err).set()
        if ( !glfwInit() ) throw IllegalStateException("Unable to initialize GLFW")
    }
    /**Loads and processes an [InputStream] into an GLFW icon and binds it to the window.
     * Automatically creates multiple sizes of the icon using [ICON_SIZES] and up to the size of the icon in [InputStream]
     * @param icon Contains the data of the icon to be processed*/
    private fun loadIcon(icon: InputStream) {
        val image = javax.imageio.ImageIO.read(icon) ?: throw RuntimeException("Unable to parse icon")
        val buffers = ArrayList<ByteBuffer>()
        try
        {
            @Suppress("LocalVariableName")
            ICON_SIZES.forEach { SIZE->
                if(SIZE > image.width || SIZE > image.height)
                    return@forEach

                val pixBuff = scaleImage(image, SIZE).raster.dataBuffer as DataBufferInt
                val buffer = memAlloc(SIZE * SIZE * 4)
                buffers.add(buffer)
                buffer.order(ByteOrder.nativeOrder())

                pixBuff.data.forEach {
                    buffer.put((it and 0xFF).toByte())         //R
                    buffer.put(((it shr 8) and 0xFF).toByte()) //G
                    buffer.put(((it shr 16) and 0xFF).toByte())//B
                    buffer.put(((it shr 24) and 0xFF).toByte())//A
                }
                buffer.flip()
            }
            val images = ArrayList<GLFWImage>()
            try
            {
                buffers.forEach { buffer->
                    val img = GLFWImage.malloc()
                    images.add(img)
                    val size = Math.sqrt(buffer.capacity().toDouble() / 4).toInt()
                    img.set(size, size, buffer)
                }

                GLFWImage.malloc(buffers.size).use { set->
                    for (i in buffers.indices)
                        set.put(i, images[i])
                    glfwSetWindowIcon(window, set)
                }
            }
            finally { images.forEach { it.free() }}
        }
        finally { buffers.forEach { MemoryUtil.memFree(it) }}
    }
    /**Used as an event for when the window was resized*/
    private fun onResize(width: Int, height: Int) {
        glViewport(0, 0, width, height)
        size?.x = width; size?.y = height
    }
    /**Starts up the window logic
     *
     * Run's in the current thread*/
    fun run() {
        glfwShowWindow(window)
        glClearColor(0.0f, 0.0f, 0.0f, 0.0f)

        val timer = Timer()
        val targetTime = 1000L / targetFPS

        val shaderProgram = run {
            val vertex = Window::class.java.module.getResourceAsStream("assets/create/shaders/basic.vert") ?: throw IOException("Unable to open shaders")
            val fragment = Window::class.java.module.getResourceAsStream("assets/create/shaders/basic.frag") ?: throw IOException("Unable to open shaders")
            val xml = Window::class.java.module.getResourceAsStream("assets/create/shaders/basic.xml") ?: throw IOException("Unable to open shaders")

            Shader(vertex, fragment, xml)
        }

        shaderProgram.setUniform("model", Matrix4f())
        shaderProgram.setUniform("view", Matrix4f())

        val ratio = 640f / 480f
        shaderProgram.setUniform("projection", Matrix4f().setOrtho(-ratio, ratio, -1f, 1f, -1f, 1f))

        val mesh = Mesh(shaderProgram)
        mesh.setAttribute("position", arrayOf(
                Vector3f(-0.6f, -0.4f, 0f),
                Vector3f( 0.6f, -0.4f, 0f),
                Vector3f( 0f  ,  0.6f, 0f)))
        mesh.setAttribute("color", arrayOf(
                Vector3f(1f, 0f, 0f),
                Vector3f(0f, 1f, 0f),
                Vector3f(0f, 0f, 1f)))
        mesh.burnModel()
        mesh.flushBuffers()

        @Suppress("unused")
        glfwSetKeyCallback(window) { wind, key, scancode, action, mods ->
            if(key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE)
                glfwSetWindowShouldClose(wind, true)
            if(key == GLFW_KEY_D && action == GLFW_RELEASE)
                shaderProgram.release()
        }

        glfwSwapInterval(if(vSync) 1 else 0)
        timer.init()
        while (!glfwWindowShouldClose(window)) {
            val startTime = timer.longTime
            val delta = timer.delta

            logicUpdate.accept(delta)

            glClear(GL_COLOR_BUFFER_BIT or GL_DEPTH_BUFFER_BIT)

            mesh.draw()

            glfwSwapBuffers(window)
            glfwPollEvents()

            val endTime = timer.longTime
            val timeOut = startTime + targetTime - endTime
            if(timeOut > 0)
                Thread.sleep(timeOut)
        }
    }

    /**Creates a new OpenGL window instance and it's related logic*/
    fun create() {
        @Suppress("USELESS_ELVIS")
        synchronized (sync) {
            if(window != NULL)
                return

            glfwDefaultWindowHints()
            glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE)
            glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE)

            monitor = monitor ?: Monitor.list.first()
            size = size ?: Vector2i(monitor!!.width * 2 / 3, monitor!!.height * 2 / 3)

            window = glfwCreateWindow(size!!.x, size!!.y, _title ?: "", NULL, NULL)
            if ( window == NULL )
                throw RuntimeException("Failed to create the GLFW window")
        }

        val handler = window
        handleDestroyer = cleaner.register(this) { glfwDestroyWindow(handler) }
        val pos = monitor?.position ?: Vector2i(0, 0)
        val work = monitor!!.workArea
        glfwSetWindowPos(window, pos.x() + (work.width - size!!.x) / 2, pos.y() + (work.height - size!!.y) / 2)

        icon?.let { loadIcon(it) }

        glfwSetFramebufferSizeCallback(window) {_, width, height ->
            this.onResize(width, height)
        }

    }
    /**Sets the flag stating that whe window should close itself, will go into effect during next logic update*/
    @Suppress("unused") fun close() {
        if (window != NULL)
            glfwWindowShouldClose(window)
    }
    /**Dissolves the OpenGL window making this instance unusable*/
    @Suppress("unused") fun destroy() = handleDestroyer.clean()
    /**Binds the required OpenGL logic to this [Thread]*/
    fun threadBind() = synchronized (currentContext)
    {
        if(threadBound)
            throw IllegalStateException("Window is already bound to a Thread")
        if(currentContext.get() != null)
            throw IllegalStateException("Thread is already bound to a Window or Context")
        if(window == NULL)
            throw IllegalStateException("The window was not yet created")
        currentContext.set(this)
        glfwMakeContextCurrent(window)
        GL.createCapabilities()
        glViewport(0, 0, size!!.x, size!!.y)
        GLErrorHandler.bindErrorCather()

        threadBound = true
    }

    /**Mechanism for limiting frames per second*/
    private data class Timer (private var lastLoopTime: Double = .0, private var timeCount: Float = 0f) {
        val time: Double get() = glfwGetTime()
        val longTime get() = time.toLong() * 1000
        fun init() { lastLoopTime = time }
        val delta: Float get() {
            val time = time
            val delta = (time - lastLoopTime).toFloat()
            lastLoopTime = time
            timeCount += delta
            return delta
        }
    }
}
