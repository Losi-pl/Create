package com.losi.create.utility;

import java.util.ArrayList;
import java.util.List;

/**An implementation of the {@link Runnable} lambda meant to make a subscribtion type event where anyone can add their own lambda that will be called whenever this lambda is called*/
@SuppressWarnings({"unused", "SpellCheckingInspection", "typo"})
public final class ExpandedRunnable implements Runnable {
    private List<Runnable> runnables = List.of();
    private final Object sync = new Object();

    /**Meant for adding a new lambda to the subscription <p color="#83B035">TODO: Make the method return it's argument</p>
     * @param runnable The lambda to be added to the subscription*/
    public void add(Runnable runnable)
    {
        synchronized (sync)
        {
            var temp = new ArrayList<>(runnables);
            temp.add(runnable);
            runnables = List.copyOf(temp);
        }
    }

    /**Meant to remove a lambda from subscription <p>Will require the user to provide the user to provide the specific lambda to remove</p>
     * @param runnable The lambda to be removed*/
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
