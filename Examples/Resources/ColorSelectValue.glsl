uniform vec4 color;

varying vec3 modelPos;

void main() {
	vec4 hsv = toHSV(color.r, color.g, color.b, 1.0);
	gl_FragColor = fromHSV(hsv.x, hsv.y, modelPos.y, 1.0);
}
