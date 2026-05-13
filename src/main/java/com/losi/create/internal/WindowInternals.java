package com.losi.create.internal;

import com.losi.create.graphics.Window;

public abstract class WindowInternals {
    public static Runnable createWindow;
    static { @SuppressWarnings("unused") var w = new Window(); }
}
