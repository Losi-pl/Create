#version 330

in vec2 uv;
in vec3 oColor;
flat in uint ind;

out vec4 fragColor;

uniform sampler2DArray atlas;

void main() {
    fragColor = texture(atlas, vec3(uv, ind)) * vec4(oColor, 1.0);
}