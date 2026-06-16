#version 130

in vec2 uv;

out vec4 fragColor;

uniform sampler2DArray atlas;
uniform uint texIndex;

void main() {
    fragColor = texture(atlas, vec3(uv, texIndex));
}