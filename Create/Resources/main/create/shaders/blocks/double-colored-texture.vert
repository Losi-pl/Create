#version 130

in vec2 uvPos;
in vec3 pos;
in uint atlasIndTop;
in uint atlasIndBottom;
in vec3 colorTop;
in vec3 colorBottom;

out vec2 uv;
flat out uint indTop;
flat out uint indBott;
out vec3 colTop;
out vec3 colBott;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    uv = uvPos;
    
    indTop = atlasIndTop;
    indBott = atlasIndBottom;

    colTop = colorTop;
    colBott = colorBottom;
    
    mat4 mvp = model * view * projection;
    gl_Position = vec4(pos, 1.0) * mvp;
}