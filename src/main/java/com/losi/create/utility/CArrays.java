package com.losi.create.utility;

@SuppressWarnings("unused")
public class CArrays {
    /**A variation of {@code findAny()}. <p>Unlike tha standard one, it's meant only for searching an array of Strings containing a specified sequence but with letter case ignored
     * @param array The array to be searched
     * @param look_for The sequence that is looked for
     * @param ignoreCase Specifies if it should ignore letter case or not
     * @return Will return {@code true} if there is any matching sequence, otherwise it will be {@code false}*/
    public static boolean findAny(String[] array, String look_for, boolean ignoreCase)
    {
        for (var s: array)
            if(ignoreCase ? s.equalsIgnoreCase(look_for) : s.equals(look_for))
                return true;
        return false;
    }

    /**A simplified version of {@link #findAny(String[], String, boolean)} but with the {@code ignoreCase} automatically set to {@code false}
     * @param array The array to be searched
     * @param look_for The sequence that is looked for
     * @return Will return {@code true} if there is any matching sequence, otherwise it will be {@code false}*/
    public static boolean findAny(String[] array, String look_for)
    { return findAny(array, look_for, false); }
}
