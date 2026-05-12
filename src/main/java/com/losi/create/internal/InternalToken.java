package com.losi.create.internal;

import java.util.UUID;

public final class InternalToken
{
    public static final UUID projectToken;;
    static { projectToken = UUID.randomUUID(); }
    private final UUID callerToken;

    public InternalToken(UUID callerToken)
    {
        this.callerToken = callerToken;
    }

    public boolean valid()
    {
        return projectToken.equals(callerToken);
    }
}
