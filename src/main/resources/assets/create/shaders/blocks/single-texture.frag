#version 100
precision highp float;

in vec2 uv;

varying out vec4 fragColor;

void main() {
    fragColor = vec4(uv, .0, 1.0);
}