#version 100

in vec2 uvPos;
in vec3 pos;

varying out vec2 uv;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    mat4 mvp = projection * view * model;
    gl_Position = mvp * vec4(pos, 1.0);
}