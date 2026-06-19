#version 130

in vec2 uvPos;
in vec3 pos;
in uint atlasInd;
in vec3 color;

out vec2 uv;
out vec3 oColor;
flat out uint ind;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    ind = atlasInd;
    oColor = color;
    mat4 mvp = projection * view * model;
    gl_Position = mvp * vec4(pos, 1.0);
}