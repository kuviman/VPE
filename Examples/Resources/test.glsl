﻿uniform sampler2D texture;
uniform vec2 size;
uniform float doX;

varying vec3 modelPos;

void main() {
	const int n = 10;

	vec3 res1 = vec3(0, 0, 0);
	float res2 = 0.0;
	float w1 = 0.0, w2 = 0.0;

	vec4 res = vec4(0, 0, 0, 0);
	float w = 0.0;

	float gb = 0.0;

	for(int i = -n; i <= n; i++) {
		vec2 pos = modelPos.xy + vec2(1.0 / size.x * float(i) * doX, 1.0 / size.y * float(i) * (1.0 - doX));
		pos = clamp(pos, 0.0, 1.0);
		float k = exp(-abs(float(i)) / float(n));
		vec4 color = texture2D(texture, pos);
		res += color * k;
		w += k;
		float bright = color.w * (color.x + color.y + color.z) / 3.0;
		res1 += color.xyz * bright * k;
		w1 += bright * k;

		gb = max(gb, color.w * k);

		res2 += color.w * k;
		w2 += k;
	}
	const float eps = 1e-5;
	if (w1 < eps || w2 < eps)
		discard;
	gl_FragColor = vec4(res1 / w1, gb);
}