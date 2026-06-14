package com.losi.create.graphics

import com.losi.create.graphics.gl.*
import com.losi.create.utility.ExpandedConsumer
import com.losi.create.utility.after
import com.losi.create.utility.orElse
import com.losi.create.utility.unaccessible
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
    private var _size: Vector2i? = null
    /**The monitor on which the window was set to be present*/
    private var monitor: Monitor? = null
    /**A flag, is the V-Sync mechanism enabled*/
    private var _vSync = false
    /**The stream to the icon of this window*/
    private var _icon: InputStream? = null
    /**Targeted frame rate for this window*/
    private var _targetFPS = 60u
    /**A collective lambda that it called every frame*/
    private val logicUpdate = ExpandedConsumer<Float>()
    /**A mechanism used to dynamically handle the content of this window*/
    private var usedScene: Scene? = null

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
    /**A setter for the [Window]'s icon the icon, it can only be set and not got back*/@get:Deprecated("The getter is unavailable as the Stream is disconnected after it is used", level = DeprecationLevel.ERROR)
    var icon: InputStream? get() = unaccessible(); set(it) = synchronized (sync) {
        if(window != NULL)
        {
            if(it != null)
                loadIcon(it)
            else
                glfwSetWindowIcon(window, null)
        }
        else
            _icon = it
    }
    /**The targeted frame rate of the window*/
    var targetFPS: UInt get() = _targetFPS; set(it) { _targetFPS = it }
    /**The handler of this window*/
    internal val handle: Long get() = window

    var size: Vector2i
        get() = _size.orElse {
            val mon = monitor?: Monitor.list.first()
            Vector2i(mon.width * 2 / 3, mon.height * 2 / 3) }
        set(value) {
            _size = value
            glfwSetWindowSize(window, value.x, value.y)
        }

    var scene: Scene? = null

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
        glViewport(0..width, 0..height)
        _size?.x = width; _size?.y = height
    }

    /**Used when a keyboard key is pressed or released*/
    private fun onKeyAction(key: KeyboardKey, action: KeyboardAction, mods: KeyMods) {
        usedScene?.let { scene ->
            scene.onKeyAction(key, action, mods)
            when (action) {
                KeyboardAction.Press -> scene.onKeyDown(key, mods)
                KeyboardAction.Release -> scene.onKeyUp(key, mods)
                KeyboardAction.Repeat -> scene.onKeyRepeat(key, mods)
            }
        }
    }



    /**Starts up the window logic
     *
     * Run's in the current thread*/
    fun run() {
        glfwShowWindow(window)
        glClearColor(Color.black)

        val timer = Timer()
        val targetTime = 1000L / targetFPS.toLong()

        glfwSwapInterval(if(vSync) 1 else 0)
        timer.init()
        while (!glfwWindowShouldClose(window)) {
            val startTime = timer.longTime
            val delta = timer.delta

            logicUpdate.accept(delta)

            if(usedScene !== scene)
            {
                usedScene?.unbindingScene()
                scene?.bindingScene(this)

                usedScene = scene
            }

            usedScene?.update(delta)

            glfwSwapBuffers(window)
            glfwPollEvents()

            val endTime = timer.longTime
            val timeOut = startTime + targetTime - endTime
            if(timeOut > 0)
                Thread.sleep(timeOut)
        }

        usedScene?.unbindingScene()
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
            _size = _size ?: Vector2i(monitor!!.width * 2 / 3, monitor!!.height * 2 / 3)

            window = glfwCreateWindow(_size!!.x, _size!!.y, _title ?: "", NULL, NULL)
            if ( window == NULL )
                throw RuntimeException("Failed to create the GLFW window")
        }

        val handler = window
        handleDestroyer = cleaner.register(this) { glfwDestroyWindow(handler) }
        val pos = monitor?.position ?: Vector2i(0, 0)
        val work = monitor!!.workArea
        glfwSetWindowPos(window, pos.x() + (work.width - _size!!.x) / 2, pos.y() + (work.height - _size!!.y) / 2)

        _icon?.let { loadIcon(it) }.after { _icon?.close(); _icon = null }

        glfwSetFramebufferSizeCallback(window) { _, width, height -> this.onResize(width, height) }
        glfwSetKeyCallback(window) { _, _, scancode, action, mods ->
            @Suppress("SpellCheckingInspection", "RedundantSuppression")
            this.onKeyAction(
                KeyboardKey(scancode),
                KeyboardAction.of(action)?: throw RuntimeException("Unknown action occurred in GLFWkeyfun"),
                KeyMods.of(mods))
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
        check(!threadBound) { "Window is already bound to a Thread" }
        check(currentContext.get() == null) { "Thread is already bound to a Window or Context" }
        check(window != NULL) { "The window was not yet created" }

        currentContext.set(this)
        glfwMakeContextCurrent(window)
        GL.createCapabilities()
        _size?.let{ glViewport(0..it.x, 0..it.y) }
        bindErrorCather()

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
