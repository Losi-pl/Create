#version 330 core
in vec3 ourColor;
out vec4 FragColor;

uniform float border;

void main()
{
    FragColor = vec4(ourColor, 1.0);
    if(FragColor.r < border && FragColor.g < border && FragColor.b < border)
    FragColor = vec4(0.0, 0.0, 0.0, 1.0);
}