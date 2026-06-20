#version 330

in vec2 uv;
in vec3 fColor1;
in vec3 fColor2;
flat in uint ind1;
flat in uint ind2;

out vec4 fragColor;

uniform sampler2DArray atlas;

void main() {
    vec4 samp1 = texture(atlas, vec3(uv, float(ind1))) * vec4(fColor1, 1.0);
    vec4 samp2 = texture(atlas, vec3(uv, float(ind2))) * vec4(fColor2, 1.0);

    samp1 *= vec4(1 - samp2.a, 1 - samp2.a, 1 - samp2.a, 1 - samp2.a);

    fragColor = vec4(samp1.rgb + samp2.rgb, 1.0);
}