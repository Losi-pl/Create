package com.losi.create.utility;

import java.util.ArrayList;
import java.util.List;

@SuppressWarnings("unused")
public final class ExpandedRunnable implements Runnable {
    @SuppressWarnings({"SpellCheckingInspection", "typo"})
    private List<Runnable> runnables = List.of();
    private final Object sync = new Object();

    public void add(Runnable runnable)
    {
        synchronized (sync)
        {
            var temp = new ArrayList<>(runnables);
            temp.add(runnable);
            runnables = List.copyOf(temp);
        }
    }

    public boolean remove(Runnable runnable)
    {
        synchronized (sync)
        {
            var temp = new ArrayList<>(runnables);
            var rez = temp.remove(runnable);
            runnables = List.copyOf(temp);
            return rez;
        }
    }

    @Override
    public void run() {
        synchronized (sync)
        { for (var runnable : runnables) runnable.run(); }
    }
}
