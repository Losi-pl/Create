@file:JvmName("CLists")
package com.losi.create.utility

import java.util.Collections

/**Creates a reflection of a list that is read only but still reflects changes to the original*/
fun <T> List<T>.calcify(): List<T> = Collections.unmodifiableList(this)
/**Creates a reflection of a map that is read only but still reflects changes to the original*/
fun <T> Map<String, T>.calcify(): Map<String, T> = Collections.unmodifiableMap(this)