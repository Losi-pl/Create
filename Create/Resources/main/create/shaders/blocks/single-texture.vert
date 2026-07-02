#version 130

in vec2 uvPos;
in vec3 pos;
in uint atlasInd;

out vec2 uv;
flat out uint ind;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    ind = atlasInd;
    mat4 mvp = projection * view * model;
    gl_Position = mvp * vec4(pos, 1.0);
}