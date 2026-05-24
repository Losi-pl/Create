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

class Window: GContext {
    companion object
    {
        internal var currentContext = ThreadLocal<GContext>()
        private var cleaner = Cleaner.create()
        private var initialized = false
        private var ICON_SIZES = listOf(16, 32, 48, 64, 128, 256)

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

    private val sync = Any()
    private var window: Long = NULL
    @Suppress("FieldCanBeLocal") @Transient
    private lateinit var handleDestroyer: Cleaner.Cleanable
    private var threadBound = false
    private var _title: String? = null
    private var size: Vector2i? = null
    private var monitor: Monitor? = null
    private var _vSync = false
    private var _icon: InputStream? = null
    private var _targetFPS = 60
    private val logicUpdate = ExpandedConsumer<Float>()

    constructor() {
        synchronized (currentContext)
        {
            if(!initialized)
                initGL()
            initialized = true
        }
    }

    var title: String? get() { return _title; } set(it) {
        _title = it
        if(window != NULL)
            glfwSetWindowTitle(window, _title ?: "")
    }
    var vSync: Boolean get() = _vSync; set(it) { _vSync = it }
    var icon: InputStream? get() = _icon; set(it) = synchronized (sync) {
        _icon = it
        if(_icon != null && window != NULL)
            loadIcon(_icon!!)
    }
    var targetFPS: Int get() = _targetFPS; set(it) { _targetFPS = it }
    internal val handle: Long get() = window

    fun registerLogic(logic: java.util.function.Consumer<Float>)
    { logicUpdate.add(logic); }

    private fun initGL() {
        GLFWErrorCallback.createPrint(System.err).set()
        if ( !glfwInit() ) throw IllegalStateException("Unable to initialize GLFW")
    }
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

    private fun onResize(width: Int, height: Int) {
        glViewport(0, 0, width, height)
        size?.x = width; size?.y = height
    }

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
        /*// glfwSetKeyCallback(window, (wind, key, scancode, action, mods) -> {
            if ( key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE )
                glfwSetWindowShouldClose(wind, true); // We will detect this in the rendering loop
        });*/
    }
    @Suppress("unused") fun close() {
        if (window != NULL)
            glfwWindowShouldClose(window)
    }
    @Suppress("unused") fun destroy() = handleDestroyer.clean()
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
