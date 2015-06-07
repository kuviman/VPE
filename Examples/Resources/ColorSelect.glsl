uniform float valueWidth;
uniform vec4 color;

varying vec3 modelPos;

vec4 fromHSV(float h, float s, float v, float a) {
	h -= floor(h);
	float r, g, b;
	float f = h * 6.0 - floor(h * 6.0);
	float p = v * (1.0 - s);
	float q = v * (1.0 - f * s);
	float t = v * (1.0 - (1.0 - f) * s);
	if (h * 6.0 < 1.0) {
		r = v; g = t; b = p;
	} else if (h * 6.0 < 2.0) {
		r = q; g = v; b = p;
	} else if (h * 6.0 < 3.0) {
		r = p; g = v; b = t;
	} else if (h * 6.0 < 4.0) {
		r = p; g = q; b = v;
	} else if (h * 6.0 < 5.0) {
		r = t; g = p; b = v;
	} else {
		r = v; g = p; b = q;
	}
	return vec4(r, g, b, a);
}

vec4 toHSV(float r, float g, float b, float a) {
	float Cmax = max(r, max(g, b));
	float Cmin = min(r, min(g, b));
	float d = Cmax - Cmin;
	float h, s, v;
	if (d == 0.0)
		h = 0.0;
	else if (Cmax == r)
		h = mod(((g - b) / d + 6.0) / 6.0, 1.0);
	else if (Cmax == g)
		h = ((b - r) / d + 2.0) / 6.0;
	else
		h = ((r - g) / d + 4.0) / 6.0;
	if (Cmax == 0.0)
		s = 0.0;
	else
		s = d / Cmax;
	v = Cmax;
	return vec4(h, s, v, a);
}

void main() {
	if (modelPos.y < valueWidth) {
		gl_FragColor = vec4(color.xyz, 1.0);
	} else if (modelPos.x < 1.0 - valueWidth) {
		gl_FragColor = fromHSV(modelPos.x / (1.0 - valueWidth), (modelPos.y - valueWidth) / (1.0 - valueWidth), 1.0, 1.0);
	} else {
		vec4 hsv = toHSV(color.r, color.g, color.b, color.a);
		gl_FragColor = fromHSV(hsv.x, hsv.y, (modelPos.y - valueWidth) / (1.0 - valueWidth), 1.0);
	}
}
