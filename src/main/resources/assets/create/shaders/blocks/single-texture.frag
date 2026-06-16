#version 330

in vec2 uv;
flat in uint ind;

out vec4 fragColor;

uniform sampler2DArray atlas;

void main() {
    fragColor = texture(atlas, vec3(uv, float(ind)));
}