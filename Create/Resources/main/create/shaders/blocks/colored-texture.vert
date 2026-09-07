#version 130

in vec2 uvPos;
in vec3 pos;
in uint atlasInd;
in vec3 color;

out vec2 uv;
out vec3 col;
flat out uint ind;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    ind = atlasInd;
    col = color;
    mat4 mvp = model * view * projection;
    gl_Position = vec4(pos, 1.0) * mvp;
}