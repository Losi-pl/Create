#version 150 core

in vec3 vertexColor;
in vec2 uvPos;

out vec4 fragColor;

uniform sampler2D image;

void main() {

    fragColor = texture(image, uvPos);//vec4(vertexColor, 1.0);
}