uniform vec4 color;
uniform sampler2D texture;

varying vec3 modelPos;

void main() {
    gl_FragColor = texture2D(texture, modelPos.xy) * color;
}