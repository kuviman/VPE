uniform vec4 color;
uniform sampler2D texture;

uniform mat3 textureMatrix;

varying vec3 modelPos;

void main() {
    gl_FragColor = texture2D(texture, (textureMatrix * modelPos).xy) * color;
}