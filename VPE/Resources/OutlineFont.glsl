uniform sampler2D texture;
uniform vec2 textureSize;
uniform vec4 outlineColor;
uniform float outlineWidth;

varying vec3 modelPos;

void main() {
	int r = int(ceil(textureSize.y * outlineWidth));
	gl_FragColor = texture2D(texture, modelPos.xy);

	for (int x = -r; x <= r; x++)
		for (int y = -r; y <= r; y++) {
			if (length(vec2(x, y)) > outlineWidth * textureSize.y)
				continue;
			if (texture2D(texture, modelPos.xy + vec2(float(x) / textureSize.x, float(y) / textureSize.y)).w > 0.9) {
				gl_FragColor = gl_FragColor * gl_FragColor.w + outlineColor * (1.0 - gl_FragColor);
				return;
			}
		}
}