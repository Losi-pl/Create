package com.losi.create.debug;

import java.nio.ByteBuffer;

//TODO: Correct
@SuppressWarnings("unused")
public class BufferDebugger {
    public static String toHex(ByteBuffer buf) {
        if (buf == null) return "null";
        ByteBuffer copy = buf.duplicate(); // doesn't affect original position
        StringBuilder sb = new StringBuilder();
        sb.append(String.format("ByteBuffer[pos=%d lim=%d cap=%d]%n",
                copy.position(), copy.limit(), copy.capacity()));
        sb.append("Content: ");
        while (copy.hasRemaining()) {
            sb.append(String.format("%02X ", copy.get()));
        }
        return sb.toString();
    }

}
