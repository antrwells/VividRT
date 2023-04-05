#version 410

in vec2 UV;
in vec4 col;

out vec4 color;

uniform sampler2D tR;


void main(){


    vec3 tc = texture2D(tR,UV).rgb;

    vec4 fc;

    fc.rgb = tc.rgb;
    fc.a = 1.0;

    color = fc;

}
