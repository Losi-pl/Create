#version 130

in vec2 uvPos;
in vec3 pos;

out vec2 uv;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    mat4 mvp = model * view * projection;
    gl_Position = vec4(pos, 1.0) * mvp;
}