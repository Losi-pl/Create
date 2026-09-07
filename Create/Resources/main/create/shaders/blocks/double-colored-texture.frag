#version 330

in vec2 uv;
flat in uint indTop;
flat in uint indBott;
in vec3 colTop;
in vec3 colBott;

out vec4 fragColor;

uniform sampler2DArray atlas;

void main() {
    vec4 top = texture(atlas, vec3(uv, indTop)) * vec4(colTop, 1.0);
    vec4 bottom = texture(atlas, vec3(uv, indBott)) * vec4(colBott, 1.0);

    fragColor = vec4((top.rgb * top.a) + (bottom.rgb * (1 - top.a)), 1.0);
}