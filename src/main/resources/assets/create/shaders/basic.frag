#version 150 core

in vec2 uvPos;

out vec4 fragColor;

uniform sampler2D image;
uniform sampler2DArray atlas;
uniform bool useAtlas = false;
uniform uint textureInd;

void main() {
    if(useAtlas)
    {
        uint ind = (uint(uvPos.y * 2) * 2u) + uint(uvPos.x * 2);
        vec2 uv = vec2(mod(uvPos.x * 2.0, 1.0), mod(uvPos.y * 2.0, 1.0));

        fragColor = texture(atlas, vec3(uv, ind + textureInd));
    }
    else
        fragColor = texture(image, uvPos);
}