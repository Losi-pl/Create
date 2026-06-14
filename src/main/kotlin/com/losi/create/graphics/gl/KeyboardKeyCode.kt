@file:Suppress("unused")
package com.losi.create.graphics.gl

import com.losi.create.utility.OnMainThread
import org.lwjgl.glfw.GLFW

enum class KeyboardKeyCode(val glfw: Int, val glfwName: String) {
    /**`GLFW_KEY_SPACE` Key: ` `*/
    Space(GLFW.GLFW_KEY_SPACE, "GLFW_KEY_SPACE"),
    /**`GLFW_KEY_APOSTROPHE` Key's: `'` / `"`*/
    Apostrophe(GLFW.GLFW_KEY_APOSTROPHE, "GLFW_KEY_APOSTROPHE"),
    /**`GLFW_KEY_COMMA` Key's: `,` / `<`*/
    Comma(GLFW.GLFW_KEY_COMMA, "GLFW_KEY_COMMA"),
    /**`GLFW_KEY_MINUS` Key's: `-` / `_`*/
    Minus(GLFW.GLFW_KEY_MINUS, "GLFW_KEY_MINUS"),
    /**`GLFW_KEY_PERIOD` Key's: `.` / `>`*/
    Period(GLFW.GLFW_KEY_PERIOD, "GLFW_KEY_PERIOD"),
    /**`GLFW_KEY_SLASH` Key's: `/` / `?`*/
    Slash(GLFW.GLFW_KEY_SLASH, "GLFW_KEY_SLASH"),
    /**`GLFW_KEY_0` Upper key: `)`*/
    Key0(GLFW.GLFW_KEY_0, "GLFW_KEY_0"),
    /**`GLFW_KEY_1` Upper key: `!`*/
    Key1(GLFW.GLFW_KEY_1, "GLFW_KEY_1"),
    /**`GLFW_KEY_2` Upper key: `@`*/
    Key2(GLFW.GLFW_KEY_2, "GLFW_KEY_2"),
    /**`GLFW_KEY_3` Upper key: `#`*/
    Key3(GLFW.GLFW_KEY_3, "GLFW_KEY_3"),
    /**`GLFW_KEY_4` Upper key: `$`*/
    Key4(GLFW.GLFW_KEY_4, "GLFW_KEY_4"),
    /**`GLFW_KEY_5` Upper key: `%`*/
    Key5(GLFW.GLFW_KEY_5, "GLFW_KEY_5"),
    /**`GLFW_KEY_6` Upper key: `^`*/
    Key6(GLFW.GLFW_KEY_6, "GLFW_KEY_6"),
    /**`GLFW_KEY_7` Upper key: `&`*/
    Key7(GLFW.GLFW_KEY_7, "GLFW_KEY_7"),
    /**`GLFW_KEY_8` Upper key: `*`*/
    Key8(GLFW.GLFW_KEY_8, "GLFW_KEY_8"),
    /**`GLFW_KEY_9` Upper key: `(`*/
    Key9(GLFW.GLFW_KEY_9, "GLFW_KEY_9"),
    /**`GLFW_KEY_SEMICOLON` Key's: `;` / `:`*/
    Semicolon(GLFW.GLFW_KEY_SEMICOLON, "GLFW_KEY_SEMICOLON"),
    /**`GLFW_KEY_EQUAL` Key's: `=` / `+`*/
    Equal(GLFW.GLFW_KEY_EQUAL, "GLFW_KEY_EQUAL"),
    /**`GLFW_KEY_A`*/
    A(GLFW.GLFW_KEY_A, "GLFW_KEY_A"),
    /**`GLFW_KEY_B`*/
    B(GLFW.GLFW_KEY_B, "GLFW_KEY_B"),
    /**`GLFW_KEY_C`*/
    C(GLFW.GLFW_KEY_C, "GLFW_KEY_C"),
    /**`GLFW_KEY_D`*/
    D(GLFW.GLFW_KEY_D, "GLFW_KEY_D"),
    /**`GLFW_KEY_E`*/
    E(GLFW.GLFW_KEY_E, "GLFW_KEY_E"),
    /**`GLFW_KEY_F`*/
    F(GLFW.GLFW_KEY_F, "GLFW_KEY_F"),
    /**`GLFW_KEY_G`*/
    G(GLFW.GLFW_KEY_G, "GLFW_KEY_G"),
    /**`GLFW_KEY_H`*/
    H(GLFW.GLFW_KEY_H, "GLFW_KEY_H"),
    /**`GLFW_KEY_I`*/
    I(GLFW.GLFW_KEY_I, "GLFW_KEY_I"),
    /**`GLFW_KEY_J`*/
    J(GLFW.GLFW_KEY_J, "GLFW_KEY_J"),
    /**`GLFW_KEY_K`*/
    K(GLFW.GLFW_KEY_K, "GLFW_KEY_K"),
    /**`GLFW_KEY_L`*/
    L(GLFW.GLFW_KEY_L, "GLFW_KEY_L"),
    /**`GLFW_KEY_M`*/
    M(GLFW.GLFW_KEY_M, "GLFW_KEY_M"),
    /**`GLFW_KEY_N`*/
    N(GLFW.GLFW_KEY_N, "GLFW_KEY_N"),
    /**`GLFW_KEY_O`*/
    O(GLFW.GLFW_KEY_O, "GLFW_KEY_O"),
    /**`GLFW_KEY_P`*/
    P(GLFW.GLFW_KEY_P, "GLFW_KEY_P"),
    /**`GLFW_KEY_Q`*/
    Q(GLFW.GLFW_KEY_Q, "GLFW_KEY_Q"),
    /**`GLFW_KEY_R`*/
    R(GLFW.GLFW_KEY_R, "GLFW_KEY_R"),
    /**`GLFW_KEY_S`*/
    S(GLFW.GLFW_KEY_S, "GLFW_KEY_S"),
    /**`GLFW_KEY_T`*/
    T(GLFW.GLFW_KEY_T, "GLFW_KEY_T"),
    /**`GLFW_KEY_U`*/
    U(GLFW.GLFW_KEY_U, "GLFW_KEY_U"),
    /**`GLFW_KEY_V`*/
    V(GLFW.GLFW_KEY_V, "GLFW_KEY_V"),
    /**`GLFW_KEY_W`*/
    W(GLFW.GLFW_KEY_W, "GLFW_KEY_W"),
    /**`GLFW_KEY_X`*/
    X(GLFW.GLFW_KEY_X, "GLFW_KEY_X"),
    /**`GLFW_KEY_Y`*/
    Y(GLFW.GLFW_KEY_Y, "GLFW_KEY_Y"),
    /**`GLFW_KEY_Z`*/
    Z(GLFW.GLFW_KEY_Z, "GLFW_KEY_Z"),
    /**`GLFW_KEY_LEFT_BRACKET` Key's: `[` / `{`*/
    LeftBracket(GLFW.GLFW_KEY_LEFT_BRACKET, "GLFW_KEY_LEFT_BRACKET"),
    /**`GLFW_KEY_BACKSLASH` Key's: `\` / `|`*/
    Backslash(GLFW.GLFW_KEY_BACKSLASH, "GLFW_KEY_BACKSLASH"),
    /**`GLFW_KEY_RIGHT_BRACKET` Key's: `]` / `}`*/
    RightBracket(GLFW.GLFW_KEY_RIGHT_BRACKET, "GLFW_KEY_RIGHT_BRACKET"),
    /**`GLFW_KEY_GRAVE_ACCENT` Key's `` ` `` / `~`*/
    GraveAccent(GLFW.GLFW_KEY_GRAVE_ACCENT, "GLFW_KEY_GRAVE_ACCENT"), // ~
    /**`GLFW_KEY_WORLD_1` Depends on the layout: `§` / `±`*/
    World1(GLFW.GLFW_KEY_WORLD_1, "GLFW_KEY_WORLD_1"),
    /**`GLFW_KEY_WORLD_2` Depends on the layout: `€` / `²`*/
    World2(GLFW.GLFW_KEY_WORLD_2, "GLFW_KEY_WORLD_2"),
    /**`GLFW_KEY_ESCAPE`*/
    Escape(GLFW.GLFW_KEY_ESCAPE, "GLFW_KEY_ESCAPE"),
    /**`GLFW_KEY_ENTER`*/
    Enter(GLFW.GLFW_KEY_ENTER, "GLFW_KEY_ENTER"),
    /**`GLFW_KEY_BACKSPACE`*/
    Backspace(GLFW.GLFW_KEY_BACKSPACE, "GLFW_KEY_BACKSPACE"),
    /**`GLFW_KEY_INSERT`*/
    Insert(GLFW.GLFW_KEY_INSERT, "GLFW_KEY_INSERT"),
    /**`GLFW_KEY_DELETE`*/
    Delete(GLFW.GLFW_KEY_DELETE, "GLFW_KEY_DELETE"),
    /**`GLFW_KEY_RIGHT`*/
    RightArrow(GLFW.GLFW_KEY_RIGHT, "GLFW_KEY_RIGHT"),
    /**`GLFW_KEY_LEFT`*/
    LeftArrow(GLFW.GLFW_KEY_LEFT, "GLFW_KEY_LEFT"),
    /**`GLFW_KEY_DOWN`*/
    DownArrow(GLFW.GLFW_KEY_DOWN, "GLFW_KEY_DOWN"),
    /**`GLFW_KEY_UP`*/
    UpArrow(GLFW.GLFW_KEY_UP, "GLFW_KEY_UP"),
    /**`GLFW_KEY_PAGE_DOWN`*/
    PageDown(GLFW.GLFW_KEY_PAGE_DOWN, "GLFW_KEY_PAGE_DOWN"),
    /**`GLFW_KEY_PAGE_UP`*/
    PageUp(GLFW.GLFW_KEY_PAGE_UP, "GLFW_KEY_PAGE_UP"),
    /**`GLFW_KEY_HOME`*/
    Home(GLFW.GLFW_KEY_HOME, "GLFW_KEY_HOME"),
    /**`GLFW_KEY_END`*/
    End(GLFW.GLFW_KEY_END, "GLFW_KEY_END"),
    /**`GLFW_KEY_CAPS_LOCK`*/
    CapsLock(GLFW.GLFW_KEY_CAPS_LOCK, "GLFW_KEY_CAPS_LOCK"),
    /**`GLFW_KEY_SCROLL_LOCK`*/
    ScrollLock(GLFW.GLFW_KEY_SCROLL_LOCK, "GLFW_KEY_SCROLL_LOCK"),
    /**`GLFW_KEY_NUM_LOCK`*/
    NumLock(GLFW.GLFW_KEY_NUM_LOCK, "GLFW_KEY_NUM_LOCK"),
    /**`GLFW_KEY_PRINT_SCREEN`*/
    PrintScreen(GLFW.GLFW_KEY_PRINT_SCREEN, "GLFW_KEY_PRINT_SCREEN"),
    /**`GLFW_KEY_PAUSE`*/
    Pause(GLFW.GLFW_KEY_PAUSE, "GLFW_KEY_PAUSE"),
    /**`GLFW_KEY_F1`*/
    F1(GLFW.GLFW_KEY_F1, "GLFW_KEY_F1"),
    /**`GLFW_KEY_F2`*/
    F2(GLFW.GLFW_KEY_F2, "GLFW_KEY_F2"),
    /**`GLFW_KEY_F3`*/
    F3(GLFW.GLFW_KEY_F3, "GLFW_KEY_F3"),
    /**`GLFW_KEY_F4`*/
    F4(GLFW.GLFW_KEY_F4, "GLFW_KEY_F4"),
    /**`GLFW_KEY_F5`*/
    F5(GLFW.GLFW_KEY_F5, "GLFW_KEY_F5"),
    /**`GLFW_KEY_F6`*/
    F6(GLFW.GLFW_KEY_F6, "GLFW_KEY_F6"),
    /**`GLFW_KEY_F7`*/
    F7(GLFW.GLFW_KEY_F7, "GLFW_KEY_F7"),
    /**`GLFW_KEY_F8`*/
    F8(GLFW.GLFW_KEY_F8, "GLFW_KEY_F8"),
    /**`GLFW_KEY_F9`*/
    F9(GLFW.GLFW_KEY_F9, "GLFW_KEY_F9"),
    /**`GLFW_KEY_F10`*/
    F10(GLFW.GLFW_KEY_F10, "GLFW_KEY_F10"),
    /**`GLFW_KEY_F11`*/
    F11(GLFW.GLFW_KEY_F11, "GLFW_KEY_F11"),
    /**`GLFW_KEY_F12`*/
    F12(GLFW.GLFW_KEY_F12, "GLFW_KEY_F12"),
    /**`GLFW_KEY_F13`*/
    F13(GLFW.GLFW_KEY_F13, "GLFW_KEY_F13"),
    /**`GLFW_KEY_F14`*/
    F14(GLFW.GLFW_KEY_F14, "GLFW_KEY_F14"),
    /**`GLFW_KEY_F15`*/
    F15(GLFW.GLFW_KEY_F15, "GLFW_KEY_F15"),
    /**`GLFW_KEY_F16`*/
    F16(GLFW.GLFW_KEY_F16, "GLFW_KEY_F16"),
    /**`GLFW_KEY_F17`*/
    F17(GLFW.GLFW_KEY_F17, "GLFW_KEY_F17"),
    /**`GLFW_KEY_F18`*/
    F18(GLFW.GLFW_KEY_F18, "GLFW_KEY_F18"),
    /**`GLFW_KEY_F19`*/
    F19(GLFW.GLFW_KEY_F19, "GLFW_KEY_F19"),
    /**`GLFW_KEY_F20`*/
    F20(GLFW.GLFW_KEY_F20, "GLFW_KEY_F20"),
    /**`GLFW_KEY_F21`*/
    F21(GLFW.GLFW_KEY_F21, "GLFW_KEY_F21"),
    /**`GLFW_KEY_F22`*/
    F22(GLFW.GLFW_KEY_F22, "GLFW_KEY_F22"),
    /**`GLFW_KEY_F23`*/
    F23(GLFW.GLFW_KEY_F23, "GLFW_KEY_F23"),
    /**`GLFW_KEY_F24`*/
    F24(GLFW.GLFW_KEY_F24, "GLFW_KEY_F24"),
    /**`GLFW_KEY_F25`*/
    F25(GLFW.GLFW_KEY_F25, "GLFW_KEY_F25"),
    /**`GLFW_KEY_KP_0` Key pad: [Key0] / [Insert]*/
    Num0(GLFW.GLFW_KEY_KP_0, "GLFW_KEY_KP_0"),
    /**`GLFW_KEY_KP_1` Key pad: [Key1] / [End]*/
    Num1(GLFW.GLFW_KEY_KP_1, "GLFW_KEY_KP_1"),
    /**`GLFW_KEY_KP_2` Key pad: [Key2] / [DownArrow]*/
    Num2(GLFW.GLFW_KEY_KP_2, "GLFW_KEY_KP_2"),
    /**`GLFW_KEY_KP_3` Key pad: [Key3] / [PageDown]*/
    Num3(GLFW.GLFW_KEY_KP_3, "GLFW_KEY_KP_3"),
    /**`GLFW_KEY_KP_4` Key pad: [Key4] / [LeftArrow]*/
    Num4(GLFW.GLFW_KEY_KP_4, "GLFW_KEY_KP_4"),
    /**`GLFW_KEY_KP_5` Key pad: [Key5] / None*/
    Num5(GLFW.GLFW_KEY_KP_5, "GLFW_KEY_KP_5"),
    /**`GLFW_KEY_KP_6` Key pad: [Key6] / [RightArrow]*/
    Num6(GLFW.GLFW_KEY_KP_6, "GLFW_KEY_KP_6"),
    /**`GLFW_KEY_KP_7` Key pad: [Key7] / [Home]*/
    Num7(GLFW.GLFW_KEY_KP_7, "GLFW_KEY_KP_7"),
    /**`GLFW_KEY_KP_8` Key pad: [Key8] / [UpArrow]*/
    Num8(GLFW.GLFW_KEY_KP_8, "GLFW_KEY_KP_8"),
    /**`GLFW_KEY_KP_9` Key pad: [Key9] / [PageUp]*/
    Num9(GLFW.GLFW_KEY_KP_9, "GLFW_KEY_KP_9"),
    /**`GLFW_KEY_KP_DECIMAL`*/
    NumDecimal(GLFW.GLFW_KEY_KP_DECIMAL, "GLFW_KEY_KP_DECIMAL"),
    /**`GLFW_KEY_KP_DIVIDE`*/
    NumDivide(GLFW.GLFW_KEY_KP_DIVIDE, "GLFW_KEY_KP_DIVIDE"),
    /**`GLFW_KEY_KP_MULTIPLY`*/
    NumMultiply(GLFW.GLFW_KEY_KP_MULTIPLY, "GLFW_KEY_KP_MULTIPLY"),
    /**`GLFW_KEY_KP_SUBTRACT`*/
    NumSubtract(GLFW.GLFW_KEY_KP_SUBTRACT, "GLFW_KEY_KP_SUBTRACT"),
    /**`GLFW_KEY_KP_ADD`*/
    NumAdd(GLFW.GLFW_KEY_KP_ADD, "GLFW_KEY_KP_ADD"),
    /**`GLFW_KEY_KP_ENTER`*/
    NumEnter(GLFW.GLFW_KEY_KP_ENTER, "GLFW_KEY_KP_ENTER"),
    /**`GLFW_KEY_KP_EQUAL`*/
    NumEqual(GLFW.GLFW_KEY_KP_EQUAL, "GLFW_KEY_KP_EQUAL"),
    /**`GLFW_KEY_LEFT_SHIFT`*/
    LeftShift(GLFW.GLFW_KEY_LEFT_SHIFT, "GLFW_KEY_LEFT_SHIFT"),
    /**`GLFW_KEY_LEFT_CONTROL`*/
    LeftControl(GLFW.GLFW_KEY_LEFT_CONTROL, "GLFW_KEY_LEFT_CONTROL"),
    /**`GLFW_KEY_LEFT_ALT`*/
    LeftAlt(GLFW.GLFW_KEY_LEFT_ALT, "GLFW_KEY_LEFT_ALT"),
    /**`GLFW_KEY_LEFT_SUPER`*/
    LeftSuper(GLFW.GLFW_KEY_LEFT_SUPER, "GLFW_KEY_LEFT_SUPER"),
    /**`GLFW_KEY_RIGHT_SHIFT`*/
    RightShift(GLFW.GLFW_KEY_RIGHT_SHIFT, "GLFW_KEY_RIGHT_SHIFT"),
    /**`GLFW_KEY_RIGHT_CONTROL`*/
    RightControl(GLFW.GLFW_KEY_RIGHT_CONTROL, "GLFW_KEY_RIGHT_CONTROL"),
    /**`GLFW_KEY_RIGHT_ALT`*/
    RightAlt(GLFW.GLFW_KEY_RIGHT_ALT, "GLFW_KEY_RIGHT_ALT"),
    /**`GLFW_KEY_RIGHT_SUPER`*/
    RightSuper(GLFW.GLFW_KEY_RIGHT_SUPER, "GLFW_KEY_RIGHT_SUPER"),
    /**`GLFW_KEY_MENU`*/
    Menu(GLFW.GLFW_KEY_MENU, "GLFW_KEY_MENU"),

    /**`GLFW_KEY_UNKNOWN`*/
    UNKNOWN(GLFW.GLFW_KEY_UNKNOWN, "GLFW_KEY_UNKNOWN"),
    ;
    private var scan_code: Int = 0
    val scanCode: Int get() = scan_code

    companion object {
        /**Factory to get the format by name if needed,
         * or to validate if a constant is supported.
         */
        fun of(glfw: Int) = entries.find { it.glfw == glfw }?: UNKNOWN

        val FIRST: KeyboardKeyCode inline get() = Space
        val LAST: KeyboardKeyCode inline get() = Menu

        init {
            OnMainThread.query {
                entries.forEach {
                if(it.glfw == GLFW.GLFW_KEY_UNKNOWN)
                    it.scan_code = 0
                else
                    it.scan_code = GLFW.glfwGetKeyScancode(it.glfw)
            }}
        }
    }
}