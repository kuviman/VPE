using System;

namespace VitPro {

	partial struct Color {
		
		/// <summary>
		/// Create color from HSV components.
		/// </summary>
		/// <param name="h">Hue.</param>
		/// <param name="s">Saturation.</param>
		/// <param name="v">Value.</param>
		/// <param name="a">Alpha.</param>
		public static Color FromHSV(double h, double s, double v, double a = 1) {
			h -= Math.Floor(h);
			double r, g, b;
			double f = h * 6 - Math.Floor(h * 6);
			double p = v * (1 - s);
			double q = v * (1 - f * s);
			double t = v * (1 - (1 - f) * s);
			if (h * 6 < 1) {
				r = v; g = t; b = p;
			} else if (h * 6 < 2) {
				r = q; g = v; b = p;
			} else if (h * 6 < 3) {
				r = p; g = v; b = t;
			} else if (h * 6 < 4) {
				r = p; g = q; b = v;
			} else if (h * 6 < 5) {
				r = t; g = p; b = v;
			} else {
				r = v; g = p; b = q;
			}
			return new Color(r, g, b, a);
		}

	}

}
