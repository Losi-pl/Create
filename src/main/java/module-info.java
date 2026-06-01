@SuppressWarnings("Java9RedundantRequiresStatement")
module com.losi.create {
    requires org.joml;
    requires steamworks4j;
    requires org.joml.primitives;
    requires static org.jetbrains.annotations;
    requires koloboke.api.jdk8;
    requires nbt.querz;
    requires java.rmi;
    requires kotlin.stdlib;
    requires java.desktop;
    requires com.twelvemonkeys.imageio.core;
    requires com.twelvemonkeys.common.io;
    requires com.twelvemonkeys.common.image;
    requires com.twelvemonkeys.common.lang;

    requires org.lwjgl.natives;
    requires org.lwjgl.glfw.natives;
    requires org.lwjgl.opengl.natives;
    requires com.google.errorprone.annotations;

    exports com.losi.create;
    exports com.losi.create.registry;
    exports com.losi.create.graphics;
    exports com.losi.create.utility;
}