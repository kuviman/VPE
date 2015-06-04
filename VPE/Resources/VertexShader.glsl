attribute vec4 position;

varying vec3 modelPos;
varying vec3 worldPos;
varying vec3 screenPos;

uniform mat4 modelMatrix, projectionMatrix;

vec3 toVec3(vec4 v) {
	return vec3(v.x / v.w, v.y / v.w, v.z / v.w);
}

void main() {
	modelPos = toVec3(position);
	vec4 wp = modelMatrix * position;
	worldPos = toVec3(wp);
	gl_Position = projectionMatrix * wp;
	screenPos = toVec3(gl_Position);
}