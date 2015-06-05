uniform sampler2D texture;
uniform vec2 textureSize;
uniform vec2 resultSize;
uniform float borderSize;
uniform vec4 outlineColor;
uniform float outlineWidth;
uniform vec4 color;

varying vec3 modelPos;

vec4 getAt(vec2 pos) {
	return texture2D(texture, (pos * resultSize - vec2(borderSize)) / textureSize);
}

void main() {
	int r = int(ceil(textureSize.y * outlineWidth));
	gl_FragColor = getAt(modelPos.xy) * color;

	for (int x = -r; x <= r; x++)
		for (int y = -r; y <= r; y++) {
			if (length(vec2(x, y)) > outlineWidth * textureSize.y)
				continue;
			if (getAt(modelPos.xy + vec2(float(x) / textureSize.x, float(y) / textureSize.y)).w > 0.9) {
				gl_FragColor = gl_FragColor * gl_FragColor.w + outlineColor * (1.0 - gl_FragColor);
				return;
			}
		}
}