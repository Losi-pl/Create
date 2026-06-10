#version 150 core

in vec3 vertexColor;
in vec2 uvPos;

out vec4 fragColor;

uniform sampler2D image;
uniform sampler2DArray atlas;
uniform bool useAtlas = false;
uniform uint textureInd;

void main() {
    if(useAtlas)
        fragColor = texture(atlas, vec3(uvPos, textureInd));
    else
        fragColor = texture(image, uvPos);//vec4(vertexColor, 1.0)
}