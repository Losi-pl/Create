package com.losi.create.registry;

import com.koloboke.collect.map.hash.HashObjObjMaps;
import com.losi.create.internal.InternalToken;
import org.jetbrains.annotations.NotNull;

import java.util.HashMap;
import java.util.Map;

//TODO: Kotlinify
@SuppressWarnings("unused")
public class ElementRegister<T extends GameElement>
{
    private transient Map<String, T> elements_by_name_raw = new HashMap<>();
    private transient Map<String, T> elements_by_name = null;
    private transient final Map<Integer, T> elements_by_id = null;
    private transient final Object sync = new Object();

    public void register(@NotNull T element, @NotNull String name)
    {
        var token = new InternalToken(InternalToken.projectToken);
        element.setName$create(name);
        synchronized (sync)
        {
            if(elements_by_name_raw.putIfAbsent(name, element) != null)
                throw new IllegalArgumentException("Element \"" + name + "\" already exists");
        }
    }

    public T retrieve(@NotNull String name)
    { synchronized (sync) { return elements_by_name_raw != null ? elements_by_name_raw.get(name) : elements_by_name.get(name); } }

    public boolean completed()
    { return elements_by_name != null; }

    void complete()
    {
        synchronized (sync)
        {
            if(completed())
                return;

            elements_by_name = HashObjObjMaps.newImmutableMap(elements_by_name_raw);
            elements_by_name_raw = null;
        }
    }

    public int count()
    { return elements_by_name_raw != null ? elements_by_name_raw.size() : elements_by_name.size(); }
}
