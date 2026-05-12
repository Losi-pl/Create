module com.losi.create {
    requires org.joml;
    requires org.lwjgl;
    requires steamworks4j;
    requires org.joml.primitives;
    requires static org.jetbrains.annotations;
    requires koloboke.api.jdk8;
    requires nbt.querz;
    requires java.rmi;
    requires kotlin.stdlib;

    exports com.losi.create;
    exports com.losi.create.registry;
}