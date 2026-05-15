package com.losi.create.utility;

@SuppressWarnings("unused")
public class CArrays {
    public static boolean findAny(String[] array, String look_for, boolean ignoreCase)
    {
        for (var s: array)
            if(ignoreCase ? s.equalsIgnoreCase(look_for) : s.equals(look_for))
                return true;
        return false;
    }

    public static boolean findAny(String[] array, String look_for)
    { return findAny(array, look_for, false); }
}
