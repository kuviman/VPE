varying vec3 modelPos;

void main() {
	gl_FragColor = fromHSV(modelPos.x, modelPos.y, 1.0, 1.0);
}
