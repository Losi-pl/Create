package com.losi.create.internal;

import com.losi.create.registry.GameElement;
import org.jetbrains.annotations.NotNull;

import java.rmi.AccessException;

public abstract class InternalGameElement {
    public final GameElement gameElement;
    public InternalGameElement(GameElement gameElement) { this.gameElement = gameElement; }

    public abstract void SetName(String name);
    public abstract void SetUuid(int uuid);

    @NotNull
    public static InternalGameElement of(GameElement gameElement) {
        try { return (InternalGameElement)gameElement.internal(new InternalToken(InternalToken.projectToken)); }
        catch (AccessException e) { //noinspection DataFlowIssue
            return null; }
    }
}
