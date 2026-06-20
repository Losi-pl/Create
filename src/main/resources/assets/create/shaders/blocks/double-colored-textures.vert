#version 130

in vec2 uvPos;
in vec3 pos;

in uint atlas1;
in uint atlas2;

in vec3 color1;
in vec3 color2;



out vec2 uv;

out vec3 fColor1;
out vec3 fColor2;

flat out uint ind1;
flat out uint ind2;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;

    ind1 = atlas1;
    ind2 = atlas2;

    fColor1 = color1;
    fColor2 = color2;

    mat4 mvp = projection * view * model;
    gl_Position = mvp * vec4(pos, 1.0);
}