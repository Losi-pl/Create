package com.losi.create;

import org.lwjgl.Version;

public class Startup {
    static void main() {
        IO.println("Hello and welcome!, LWJGL: " + Version.getVersion());

        for (int i = 1; i <= 5; i++) {
            IO.println("i = " + i);
        }
    }
}
