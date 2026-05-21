package com.losi.create.registry;

import com.losi.create.internal.InternalGameElement;
import com.losi.create.internal.InternalToken;
import org.jetbrains.annotations.ApiStatus.*;
import org.jetbrains.annotations.*;

import java.rmi.AccessException;
import java.util.Optional;

//TODO: Kotlinify
public abstract class GameElement
{
    private String name;
    private transient Integer uuid;

    public GameElement()
    {
        name = null;
        uuid = null;
    }

    @NotNull
    public String getName() { return name; }
    public Optional<Integer> getUuid() { return Optional.ofNullable(uuid); }
    public boolean registered() { return uuid != null; }

    @Internal @NotNull @Contract("!null -> new; null -> fail")
    public Object internal(Object token) throws AccessException {
        if(((InternalToken)token).valid())
            return new InternalGameElement(this) {
            @Override
                public void SetName(String name) {
                    gameElement.name = name;
                }

                @Override
                public void SetUuid(int uuid) {
                    gameElement.uuid = uuid;
                }
            };
        else
            throw new AccessException("You are trying to access an internal game logic");
    }
}
