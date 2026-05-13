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

    requires org.lwjgl.natives;
    requires org.lwjgl.glfw.natives;
    requires org.lwjgl.opengl.natives;

    exports com.losi.create;
    exports com.losi.create.registry;
}